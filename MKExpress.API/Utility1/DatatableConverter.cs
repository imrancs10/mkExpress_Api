using DocumentFormat.OpenXml.Vml;
using MKExpress.API.Config;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MKExpress.API.Utility
{
    public static class DatatableConverter
    {
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();

            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name.ToLower()))
                    {
                        try
                        {
                            if (pro.PropertyType.Name == "Int32")
                            {
                                var result= int.TryParse(row[pro.Name].ToString(), out int val);
                                pro.SetValue(objT, result?val:0);
                            }
                            else if (pro.PropertyType.Name == "decimal")
                            {
                                var result = decimal.TryParse(row[pro.Name].ToString(), out decimal val);
                                pro.SetValue(objT, result ? val : 0);
                            }
                            else
                            {
                                pro.SetValue(objT, row[pro.Name]);
                            }

                        }
                        catch (Exception ex) { }
                    }
                }
                return objT;
            }).ToList();
        }
    }
    public static class Utility
    {
        public static string GenerateAccessToken(string role)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigManager.AppSetting["JWT:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ConfigManager.AppSetting["JWT:ValidIssuer"],
                                                    audience: ConfigManager.AppSetting["JWT:ValidAudience"],
                                                    claims: new List<Claim>()
                                                    {
                                                        new Claim("role",role)
                                                    },
                                                    expires: DateTime.Now.AddMinutes(6),
                                                    signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
