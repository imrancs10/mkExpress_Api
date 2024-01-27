using System.Text;
using System.Text.RegularExpressions;

namespace MKExpress.API.Extension
{
    public static partial class EnumExtension
    {
        public static string ToFormatString(this Enum input)
        {
            var str = input.ToString();
            var splitedStr = SplitByUpperCase().Split(str);
            return string.Join(" ", splitedStr);
        }

        public static string ToShipmentStatusEnumString(this string input)
        {
            return AllWhiteSpace().Replace(input, string.Empty);
        }

        [GeneratedRegex("(?<!^)(?=[A-Z])")]
        private static partial Regex SplitByUpperCase();

        [GeneratedRegex("\\s")]
        private static partial Regex AllWhiteSpace();
    }
}
