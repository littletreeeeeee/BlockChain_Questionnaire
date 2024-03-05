using Ipfs.Http;
using MongoDB.Driver;
using Nethereum.Contracts;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using NLog;
using QuestionnaireLibrary.Managers;
using QuestionnaireLibrary.Models;
using QuestionnaireLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireTask
{
    internal class StressTesting_FillSurvey
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
            logger.Trace($"------- Start FillSurvey 執行次數: {repeatTimes}---------");

            Stopwatch stopwatch = new Stopwatch();

            // 開始計時
            stopwatch.Start();



            for (int i = 0; i<repeatTimes; i++)
            {
                string ID = $"TEST-T4NO{repeatTimes}I"+i;
                string encryptInfo = GetEncryptInfo();
                MongoClient client = new MongoClient(MongoAddress);
                QuestionManager manager = new QuestionManager(client, MongoDb);
                UserManager uManager = new UserManager(client, MongoDb);
                var userInfo = uManager.GetUserAccounts(new UserAccount { Email = "littletree04240@gmail.com" }).FirstOrDefault();
                var vm = manager.GetQuestionnaires(new QuestionnaireViewModel { _id =ID}).FirstOrDefault();


                var ipfsAddress = SaveAnswerToIPFS(GetEncryptInfo());
              var rtn=  SaveToBC(vm._id, ipfsAddress, userInfo.WalletAddress, false);
                if (rtn.IsSuccess)
                {
                    AnswerManager ansManager = new AnswerManager(client, MongoDb);
                    ansManager.InsertAnswer(new UserAnswer { _id = Guid.NewGuid().ToString(), UserEmail ="littletree04240@gmail.com", Doc_Id = ID, IsWinner = false, CreateDate = DateTime.Now });
                }

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
            logger.Trace($"------- End FillSurvey 執行次數: {repeatTimes}---------");

        }
        private string GetEncryptInfo()
        {
            return "0x7b2276657273696f6e223a227832353531392d7873616c736132302d706f6c7931333035222c226e6f6e6365223a226e4749633537672b4944436d433935326a77676f4b395331736e70714d326537222c22657068656d5075626c69634b6579223a226b365839795977763366786c4b4e3033672f374171515069694b2f4d49476239486738306e6c625134326b3d222c2263697068657274657874223a22534e41444b42525a57564c69744d4b6a6f33323647696b44334d317742427a427944763541504672526f302b624b67372f5937636f6256647253684d3476522f61614735514c456c4848745a7463356b4d6b772b6a31546b426e6855662f6436546e32426977684c41326d532b7579357a4f6943666256322b32527a6c336d2f38755969395375764978333674717741433068704a79464455537a6e2b787369394d49544a6a7433704e6670325759773473504773474a6f5546763868356b4856764456374570694f707a3342325a4d42464d52726557784e4331753643534475636e666132634841343967382f576d6d63796b2f6139687a454e68366b466b3658676d325a6e5141586632517a323757774e764a4a337063704b76446f676f686c49574c377448554335517a6b7461496b3774227d";
        }

        private string SaveAnswerToIPFS(string inputAns)
        {

            IpfsClient ipfs = new IpfsClient(IpfsClientPath);
            //存入IPFS
            var fsn = ipfs.FileSystem.AddTextAsync(inputAns);
            while (fsn.Status != TaskStatus.RanToCompletion)
            {
                Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, fsn.Status);
                System.Threading.Tasks.Task.Delay(100).Wait();
            }
            var encryptAddress = fsn.Result.Id;

            return encryptAddress;

        }

        private ReturnModel SaveToBC(string id, string ipfsAddress, string fillUserAddress, bool isOpen)
        {
            ReturnModel rtn = new ReturnModel();
            //The URL endpoint for the blockchain network.
            string ABI = QuestionnaireAbi.FillSurvey;

            //Creates the connecto to the network and gets an instance of the contract.
            Web3 web3 = new Web3(bcUrl);
            Contract accountContract = web3.Eth.GetContract(ABI, QuestionnaireAbi.ContractAddress);

            try
            {
                accountContract.GetFunction("fillSurvey").SendTransactionAsync(FromAddress, new HexBigInteger(new BigInteger(400000)), new HexBigInteger(new BigInteger(0)), ToSolidityAddress(fillUserAddress), id, ipfsAddress, isOpen).Wait();
                rtn.AddMessage("提交成功!");
                rtn.IsSuccess = true;
            }
            catch (Exception ex)
            {
                logger.Trace("Exception : "+ex.ToString());
   
            }

            return rtn;
        }

        public string ToSolidityAddress(string address)
        {
            // 去掉前缀 "0x"
            if (address.StartsWith("0x"))
            {
                address = address.Substring(2);
            }
            // 补全到 40 个字符
            address = address.PadLeft(40, '0');
            // 转换为 Solidity 的地址格式
            return "0x" + address.HexToByteArray().ToHex(false).ToLower();
        }

    }
}
