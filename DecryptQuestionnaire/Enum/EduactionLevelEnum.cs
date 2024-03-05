using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DecryptQuestionnaire.Enum
{
    public class EduactionLevelEnum
    {
        public const string Elementary = "1";
        public const string Secondary = "2";
        public const string HighSchool = "3";
        public const string Bachelor = "4";
        public const string Master = "5";
        public const string PhD = "ˊ";

        public static string GetDesc(string level)
        {
            switch (level)
            {
                case Elementary:
                    return "國小";
                case Secondary:
                    return "國中";
                case HighSchool:
                    return "高中";
                case Bachelor:
                    return "學士";
                case Master:
                    return "碩士";
                case PhD:
                    return "博士";
                default:
                    return level;
            }
        }
    }
}
