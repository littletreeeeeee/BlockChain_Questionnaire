using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecryptAnswer
{
    public class QuestionnaireViewModel
    {
        public class QuestionnaireViewModel
        {
            public string? _id { get; set; }
            public string? Title { get; set; }
            public string? Desc { get; set; }
            public string? Reward { get; set; }
            public string? OpenReward { get; set; }
            public string? Circulation { get; set; } //發行數量
            public string? LotteryQty { get; set; } //抽獎數量
            public string? Lottery { get; set; } //抽獎獎勵
            public string? CreateUser { get; set; }

            public DateTime? StartDate { get; set; } //開始填答時間
            public DateTime? Deadline { get; set; } //填答期限
            public bool IsPublish { get; set; } //是否已經發布
            public bool IsClose { get; set; } //是否已經發布
            public DateTime? CreateDate { get; set; } //是否已經發布
            public DateTime? PublishDate { get; set; } //發布時間
            public List<QuestionViewModel> Questions { get; set; } = new List<QuestionViewModel>();
            public string? CryptoKey { get; set; }
            public string? CryptoIv { get; set; }
            public QuestionProcess Process { get; set; } = QuestionProcess.Normal;
        }

        public class FinalExportAnswer
        {
            public QuestionnaireViewModel? Questionnaire { get; set; }
            public List<string>? Answers { get; set; }
        }
        public enum QuestionProcess
        {
            Normal,
            UpdateCryptoKey,
            Published
        }



        public class QuestionViewModel
        {
            public int? Seq { get; set; }
            public string? Question { get; set; }
            public string? QuestionType { get; set; }
            public List<string>? ItemDesc { get; set; }

        }
        public class QuestionBCViewModel
        {
            public string? reward { get; set; }
            public string? openReward { get; set; }
            public string? publishQty { get; set; }
            public string? lotteryCount { get; set; }
            public string? lottery { get; set; }
            public string? deadline { get; set; }
            public string? ipfsAddress { get; set; }
            public string? isClosed { get; set; }

        }

        public class AnswerBCViewModel
        {
            public bool isWinner { get; set; }
            public string? ipfsAddress { get; set; }
        }
    }
}
