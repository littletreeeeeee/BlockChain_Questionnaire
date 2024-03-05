using MongoDB.Driver;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using Newtonsoft.Json.Linq;
using NLog;
using QuestionnaireLibrary.Managers;
using QuestionnaireLibrary.ViewModels;
using System.Text;


namespace QuestionnaireTask
{
    internal class ExecuteLottery
    {
        public string FromAddress = @"0x2F70eB4B99201d48B3C4335Fc082cE1E510F3CF3"; //Web3AccountAddress
        public const string MongoDb = "QuestionnaireProject";
        public string bcUrl = "HTTP://127.0.0.1:8545";      //block chain url.
        public const string MongoAddress = "mongodb://localhost:27017";

        public ExecuteLottery()
        {

        }
        public void Exexute()
        {



            Logger logger = LogManager.GetCurrentClassLogger();


            // 設定 log 檔案要寫入的路徑

            logger.Trace("------- Start ---------");


            MongoClient client = new MongoClient(MongoAddress);
            QuestionManager manager = new QuestionManager(client, MongoDb);
            var survey = manager.GetQuestionnaires(null);

            var deadlindata = survey.Where(x => x.Deadline <= DateTime.Now && !x.IsClose ).ToList();
            logger.Trace(" Deadline data count : " + deadlindata.Count);
            foreach (var item in survey)
            {
                try
                {
                    logger.Trace(" - Doc Id : " + item._id);
                    var surveyInfo = new QuestionnaireViewModel();
                    Web3 web3 = new Web3(bcUrl);
                    Contract accountContract = web3.Eth.GetContract(CloseSurveyAbi, contractAddress);

                    logger.Trace("      > SendTransactionAsync : closeSurvey ");

                    accountContract.GetFunction("closeSurvey").SendTransactionAsync(FromAddress, new HexBigInteger(new System.Numerics.BigInteger(400000)), new HexBigInteger(new System.Numerics.BigInteger(0)), item._id).Wait();

                    manager.UpdateQuestion(new QuestionnaireViewModel { Process= QuestionProcess.Closed, _id=item._id });
                }
                catch (Exception ex)
                {
                    logger.Trace("      > Exception :  " + ex.ToString());
                }
            }

            logger.Trace("------- End ---------");


        }


        private const string contractAddress = @"0x952Feb77b9986D1cC64799C6721D6459eAae7b94";

        private const string CloseSurveyAbi = @"[{
      'inputs': [
        {
          'internalType': 'string',
          'name': 'docId',
          'type': 'string'
        }
      ],
      'name': 'closeSurvey',
      'outputs': [],
      'stateMutability': 'nonpayable',
      'type': 'function'
    }]";
    }
}
