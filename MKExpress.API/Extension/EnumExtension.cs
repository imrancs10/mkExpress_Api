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

        [GeneratedRegex("(?<!^)(?=[A-Z])")]
        private static partial Regex SplitByUpperCase();
    }
}
