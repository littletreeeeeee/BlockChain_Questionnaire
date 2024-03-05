using MongoDB.Driver;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using NLog;
using QuestionnaireLibrary.Managers;
using QuestionnaireLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionnaireTask
{
    internal class StressTesting_Lottery
    {
        public string QuestionnaireAddress = QuestionnaireAbi.ContractAddress;   //The ABI for the contract.
        public string FromAddress = @"0x2F70eB4B99201d48B3C4335Fc082cE1E510F3CF3"; //Web3AccountAddress
        public string bcUrl = "HTTP://127.0.0.1:8545";      //block chain url.
        public const string MongoAddress = "mongodb://localhost:27017";
        public const string MongoDb = "QuestionnaireProject";
        public const string IpfsClientPath = "http://127.0.0.1:5001";
        public Logger logger = LogManager.GetCurrentClassLogger();
        int errorCount = 0;
        public void StressTest()
        {
            int repeatTimes = 1000;

            // 設定 log 檔案要寫入的路徑
            logger.Trace($"------- Start Lottery 執行次數: {repeatTimes}---------");

            Stopwatch stopwatch = new Stopwatch();

            // 開始計時
            stopwatch.Start();


            logger.Trace("------- Start ---------");


            string ID = $"TEST-T4NO{repeatTimes}I";
            MongoClient client = new MongoClient(MongoAddress);
            QuestionManager manager = new QuestionManager(client, MongoDb);
            var survey = manager.GetQuestionnaires(null);

            var deadlindata = survey.Where(x => x.Deadline <= DateTime.Now && x._id.StartsWith(ID)).ToList();
            logger.Trace(" Deadline data count : " + deadlindata.Count);
            foreach (var item in deadlindata)
            {
                try
                {
                    var surveyInfo = new QuestionnaireViewModel();
                    Web3 web3 = new Web3(bcUrl);
                    Contract accountContract = web3.Eth.GetContract(QuestionnaireAbi.CloseSurveyAbi, QuestionnaireAddress);
                    accountContract.GetFunction("closeSurvey").SendTransactionAsync(FromAddress, new HexBigInteger(new System.Numerics.BigInteger(400000)), new HexBigInteger(new System.Numerics.BigInteger(0)), item._id).Wait();
                }
                catch (Exception ex)
                {
                    errorCount=errorCount+1;
                    logger.Trace($"      > [{item._id}]Exception :  " + ex.Message);
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
            logger.Trace($"------- End Lottery 執行次數: {repeatTimes} Error Count :{errorCount}---------");
        }

    }
}
