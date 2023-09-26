using System.Text.RegularExpressions;

namespace MKExpress.API.Utility
{
    public static class GlobalMethods
    {
        public static string CreateMasterCode(this string str)
        {

            try
            {
                return Regex.Replace(str, @"(\s+|@|&|'|\(|\)|<|>|#|\?|\\|\/|!|~|\+|=|%|\$|\^|\.|,)", "").Trim().ToLower();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
