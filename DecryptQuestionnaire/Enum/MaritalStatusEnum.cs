using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecryptQuestionnaire.Enum
{
    public class MaritalStatusEnum
    {
        /*
         single
Married
unmarried
married with children
unmarried with children
Not available
         */
        public const string Single = "S";
        public const string Married = "M";
        public const string MarriedWithChildren = "MC";
        public const string Unmarried = "U";
        public const string UnmarriedWithChildren = "UC";
        public const string NotAvailable = "X";

        public static string GetDesc(string level)
        {
            switch (level)
            {
                case Single:
                    return "單身";
                case Married:
                    return "已婚";
                case MarriedWithChildren:
                    return "以婚有小孩";
                case Unmarried:
                    return "未婚";
                case UnmarriedWithChildren:
                    return "未婚有小孩";
                case NotAvailable:
                    return "不提供";
                default:
                    return level;
            }
        }
    }
}
