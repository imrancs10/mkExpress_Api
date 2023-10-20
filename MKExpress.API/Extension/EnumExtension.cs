using System.Text;
using System.Text.RegularExpressions;

namespace MKExpress.API.Extension
{
    public static class EnumExtension
    {
        public static string ToFormatString(this Enum input)
        {
            var str= input.ToString();
            var splitedStr = Regex.Split(str, @"(?<!^)(?=[A-Z])");
            return string.Join(",", splitedStr);
        }
        
    }
}
