using System;
using System.Linq;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Pdf.Core
{
    public static partial class BooleanExtensions
    {
        public static string ToYesNo(this bool source)
        {
            return source ? "Oui" : "Non";
        }

        public static string ToYesBlank(this bool source)
        {
            return source ? "Oui" : "";
        }

        public static string ToCheckBlank(this bool source, bool isCsvFriendly = false)
        {
            return source
                       ? (isCsvFriendly ? "X" : "&#10003;")
                       : "";
        }

        public static int ToZeroOne(this bool source)
        {
            return source
                       ? 1
                       : 0;
        }

        public static bool OnlyOneIsTrue(params bool[] terms)
        {
            return terms.Select(Convert.ToInt32).Sum() == 1;
        }

        public static int AsInt(this bool b)
        {
            return b ? 1 : 0;
        }

        public static string BlankIfZero(this int i)
        {
            return i == 0 ? "" : i.ToString();
        }
    }
}
