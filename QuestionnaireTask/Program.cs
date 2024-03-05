namespace QuestionnaireTask
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ExecuteLottery lottery = new ExecuteLottery();
            lottery.Exexute();

            //1
            //StressTesting_MemberUpdate memberUpdate=new StressTesting_MemberUpdate();
            //memberUpdate.StressTest();
            //2
            //StressTesting_CreateQuestionnaire createSurvey = new StressTesting_CreateQuestionnaire();
            //createSurvey.StressTest();
            //3
            //StressTesting_FillSurvey fillSurvey = new StressTesting_FillSurvey();
            //fillSurvey.StressTest();
            ////4
            //StressTesting_Lottery closeSurvey = new StressTesting_Lottery();
            //closeSurvey.StressTest();
        }
    }
}