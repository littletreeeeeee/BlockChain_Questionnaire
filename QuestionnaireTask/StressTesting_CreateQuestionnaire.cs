using Google.Protobuf.WellKnownTypes;
using Ipfs.Http;
using MongoDB.Driver;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Newtonsoft.Json;
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
    internal class StressTesting_CreateQuestionnaire
    {
        public string QuestionnaireAddress = QuestionnaireAbi.ContractAddress;   //The ABI for the contract.
        public string FromAddress = @"0xd1B959B237D01f43eAbE4314F7F9b3fF53FD44e4"; //Web3AccountAddress
        //public string FromAddress = @"0x2F70eB4B99201d48B3C4335Fc082cE1E510F3CF3"; //Web3AccountAddress
        public string bcUrl = "HTTP://127.0.0.1:8545";      //block chain url.
        public const string MongoAddress = "mongodb://localhost:27017";
        public const string MongoDb = "QuestionnaireProject";
        public const string IpfsClientPath = "http://127.0.0.1:5001";
        public Logger logger = LogManager.GetCurrentClassLogger();


        public void StressTest()
        {

            int repeatTimes = 1000;

            // 設定 log 檔案要寫入的路徑
            logger.Trace($"------- Start CreateSurvey 執行次數: {repeatTimes}---------");

            Stopwatch stopwatch = new Stopwatch();

            // 開始計時
            stopwatch.Start();



            for (int i = 0; i<repeatTimes; i++)
            {
                MongoClient client = new MongoClient(MongoAddress);
                QuestionManager manager = new QuestionManager(client, MongoDb);

                string ID = $"TEST-T4NO{repeatTimes}I"+i;
                var survey = new QuestionnaireViewModel
                {
                    _id=ID,
                    Title="Title : "+ID,
                    Desc="Desc : "+ID,
                    Reward="1",
                    OpenReward="1",
                    Circulation="2",
                    LotteryQty="1",
                    Lottery="1",
                    CreateUser="littletree04240@gmail.com",
                    StartDate=DateTime.Now.Date.AddDays(-2).AddHours(8),
                    Deadline=DateTime.Now.Date.AddDays(1).AddHours(8),
                    IsPublish=true,
                    IsClose=false,
                    CreateDate=DateTime.Now,
                    PublishDate=DateTime.Now,

                };
                survey.Questions=new List<QuestionViewModel>();
                for (int k = 0; k<5; k++)
                {
                    survey.Questions.Add(new QuestionViewModel { Seq=k, Question="test", QuestionType="text", ItemDesc=new List<string>() });
                }
                manager.InsertQuestion(survey);
                string address = PublishQuestionnaireToIPFS(survey);
                PublishToSurveyContract(address, survey);
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
            logger.Trace($"------- End CreateSurvey 執行次數: {repeatTimes}---------");

        }
        private string PublishQuestionnaireToIPFS(QuestionnaireViewModel vm)
        {

            IpfsClient ipfs = new IpfsClient(IpfsClientPath);
            //存入IPFS
            var fsn = ipfs.FileSystem.AddTextAsync(JsonConvert.SerializeObject(vm));
            while (fsn.Status != TaskStatus.RanToCompletion)
            {
                Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, fsn.Status);
                System.Threading.Tasks.Task.Delay(100).Wait();
            }
            var encryptAddress = fsn.Result.Id;

            return encryptAddress;

        }



    //    private void PublishToSurveyContract(string ipfsAddress, QuestionnaireViewModel vm)
    //    {
    //        MongoClient client = new MongoClient(MongoAddress);
    //        UserManager manager = new UserManager(client, MongoDb);
    //        var userInfo = manager.GetUserAccounts(new UserAccount { Email = "littletree04240@gmail.com" }).FirstOrDefault();
    //        //Creates the connecto to the network and gets an instance of the contract.
    //        Web3 web3 = new Web3(bcUrl);
    //        Contract createSurveyContract = web3.Eth.GetContract(QuestionnaireAbi.CreateSurvey, QuestionnaireAbi.ContractAddress);


    //        // 准备合约函数参数
    //        var function = createSurveyContract.GetFunction("createSurvey");



    //        // 创建合约交易
    //        var txInput = function.CreateTransactionInput(FromAddress, new HexBigInteger(200000), null, new HexBigInteger(3000000), vm._id, Convert.ToInt32(vm.Reward), Convert.ToInt32(vm.OpenReward), Convert.ToInt32(vm.Circulation), Convert.ToInt32(vm.LotteryQty), Convert.ToInt32(vm.Lottery), Convert.ToInt32(vm.Deadline?.Subtract(new DateTime(1970, 1, 1)).TotalSeconds), ipfsAddress);

    //        // 使用 Netherum 套件发送交易
    //        var createSureyToBC = System.Threading.Tasks.Task.Run(async () => await web3.TransactionManager
    //.SendTransactionAndWaitForReceiptAsync(txInput));


    //        while (createSureyToBC.Status != TaskStatus.RanToCompletion && createSureyToBC.Status != TaskStatus.Faulted)
    //        {
    //            Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, createSureyToBC.Status);
    //            System.Threading.Tasks.Task.Delay(100).Wait();

    //        }
    //        if (createSureyToBC.Status==TaskStatus.Faulted)
    //        {
    //            logger.Trace($"Error : {createSureyToBC.Result.ToString()}");

    //            Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, createSureyToBC.Status);

    //        }



    //    }
        private void PublishToSurveyContract(string ipfsAddress, QuestionnaireViewModel vm)
        {
            MongoClient client = new MongoClient(MongoAddress);
            UserManager manager = new UserManager(client, MongoDb);
            var userInfo = manager.GetUserAccounts(new UserAccount { Email = "littletree04240@gmail.com" }).FirstOrDefault();
            //Creates the connecto to the network and gets an instance of the contract.
            var account = new Account("0xaeb5725fbb2db486a91cbb8ebb92087cbc5835a034ea9cc67131d7d3e890d382");
            Web3 web3 = new Web3(account,bcUrl);
            

            string contractAddress = @QuestionnaireAbi.ContractAddress;
            var contract = web3.Eth.GetContract(QuestionnaireAbi.CreateSurvey, contractAddress);

            var createSurveyFunction = contract.GetFunction("createSurvey");
            var transactionInput = createSurveyFunction.CreateTransactionInput(account.Address, new HexBigInteger(new BigInteger(400000)), new HexBigInteger(new BigInteger(0)),
                vm._id, (ulong)(Convert.ToInt32(vm.Reward)), (ulong)(Convert.ToInt32(vm.OpenReward)), (ulong)(Convert.ToInt32(vm.Circulation)),
                (ulong)(Convert.ToInt32(vm.LotteryQty)), (ulong)(Convert.ToInt32(vm.Lottery)), (ulong)(Convert.ToInt32(vm.Deadline?.Subtract(new DateTime(1970, 1, 1)).TotalSeconds)), ipfsAddress);

            var createSureyToBC = System.Threading.Tasks.Task.Run(async () => await web3.Eth.Transactions.SendTransaction.SendRequestAsync(transactionInput));
            while (createSureyToBC.Status != TaskStatus.RanToCompletion && createSureyToBC.Status != TaskStatus.Faulted)
            {
                Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, createSureyToBC.Status);
                System.Threading.Tasks.Task.Delay(100).Wait();

            }
            if (createSureyToBC.Status==TaskStatus.Faulted)
            {
                logger.Trace($"Error : {createSureyToBC.Exception?.InnerException?.Message.ToString()}");

                Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, createSureyToBC.Status);

            }

        }



    }
}
