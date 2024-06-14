using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.RegularExpressions;

namespace MKExpress.API.Extension
{
    public static class StringExtensions
    {
        public static string DecodeBase64(this String str)
        {
            byte[] data = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(data);
        }

        public static string EncodeBase64(this String str)
        {
            if(string.IsNullOrEmpty(str))
                throw new ArgumentNullException(nameof(str));
            var plainTextBytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(plainTextBytes);
        } 
        public static bool MinimumLength(this String str,int length)
        {
           if(string.IsNullOrEmpty(str))
                return false;
            return str.Length >= length;    
        }
        public static bool MaximumLength(this String str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return str.Length <= length;
        } 
        public static bool Matches(this String str, string pattern)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(pattern))
                return false;
            Regex rg = new(pattern);
            return rg.IsMatch(str);

        }

        public static bool IsBase64(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                // Handle the exception
            }
            return false;
        }
    }
}
