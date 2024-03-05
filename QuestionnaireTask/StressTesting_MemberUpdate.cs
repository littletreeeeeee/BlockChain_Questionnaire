using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Ipfs.Http;
using MongoDB.Driver;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json;
using NLog;
using QuestionnaireLibrary.Managers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace QuestionnaireTask
{
    internal class StressTesting_MemberUpdate
    {
        public string AccountContractAddress = UserAccountAbI.ContractAddress;   //The ABI for the contract.
        public string MiaoContractAddress = MiaoTokenAbi.ContractAddress;   //The ABI for the contract.
        public string FromAddress = @"0x2F70eB4B99201d48B3C4335Fc082cE1E510F3CF3"; //Web3AccountAddress
        public string bcUrl = "HTTP://127.0.0.1:8545";      //block chain url.
        public const string MongoAddress = "mongodb://localhost:27017";
        public const string MongoDb = "QuestionnaireProject";
        public const string IpfsClientPath = "http://127.0.0.1:5001";
        public Logger logger = LogManager.GetCurrentClassLogger();


        public void StressTest()
        {

            int repeatTimes = 1000;

            // 設定 log 檔案要寫入的路徑
            logger.Trace($"------- Start MemberUpdate 執行次數: {repeatTimes}---------");

            Stopwatch stopwatch = new Stopwatch();

            // 開始計時
            stopwatch.Start();



            for (int i = 0; i<repeatTimes; i++)
            {
                string encryptInfo = GetEncryptInfo();
                MongoClient client = new MongoClient(MongoAddress);
                UserManager manager = new UserManager(client, MongoDb);
                var userInfo = manager.GetUserAccounts(new QuestionnaireLibrary.Models.UserAccount { Email = "littletree04240@gmail.com" }).FirstOrDefault();

                IpfsClient ipfs = new IpfsClient(IpfsClientPath);
                var fsn = ipfs.FileSystem.AddTextAsync(encryptInfo);
                while (fsn.Status != TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, fsn.Status);
                    System.Threading.Tasks.Task.Delay(100).Wait();
                }
                SaveToBC(JsonConvert.SerializeObject(userInfo.Id), fsn.Result.Id);

            }

            // 停止計時
            stopwatch.Stop();

            // 計算經過的時間
            TimeSpan elapsed = stopwatch.Elapsed;

            // 轉換成分鐘和秒的格式
            int minutes = elapsed.Minutes;
            int seconds = elapsed.Seconds;

            // 輸出結果
            logger.Trace($"經過的時間：{minutes}分{seconds}秒");
            logger.Trace($"------- End MemberUpdate 執行次數: {repeatTimes}---------");

        }
        private string GetEncryptInfo()
        {
            return "0x7b2276657273696f6e223a227832353531392d7873616c736132302d706f6c7931333035222c226e6f6e6365223a226e44746863442b376a596e305a483963637669504a2b6d58367250335648514a222c22657068656d5075626c69634b6579223a2250387a4e596651577675705045516f2b3063636241724c5942667938334d2b703355776e4978664c7857593d222c2263697068657274657874223a224a2b304b4733614c53585971496548684e38586a787643742f75566a7768594b32464e2b4654514f6f3356355978766c43716d3547627468586d417a6c34436a57696f33485372766c527563654a616e2b4a30575a4666437a6a326739465332393948474e4a6c5673535878326c4d694a534a64767a412b796149786d4d384154564d3d227d";
        }
        private void SaveToBC(string id, string ipfsAddress)
        {
            //The URL endpoint for the blockchain network.
            string ABI = UserAccountAbI.CreateAccount;


            //Creates the connecto to the network and gets an instance of the contract.
            Web3 web3 = new Web3(bcUrl);
            Contract accountContract = web3.Eth.GetContract(ABI, AccountContractAddress);

            try
            {
                accountContract.GetFunction("CreateAccount").SendTransactionAsync(FromAddress, new HexBigInteger(new BigInteger(400000)), new HexBigInteger(new BigInteger(0)), id, ipfsAddress).Wait();
            }
            catch (Exception ex)
            {
                logger.Trace($"Exception : "+ex.ToString());

            }
        }

    }
}
