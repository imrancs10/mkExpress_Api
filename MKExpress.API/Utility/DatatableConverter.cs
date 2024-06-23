using DocumentFormat.OpenXml.Vml;
using MKExpress.API.Config;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MKExpress.API.DTO.Response;
using System.Text.Json.Serialization;
using System.Text.Json;

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
                        catch (Exception ex) {
                            throw;
                        }
                    }
                }
                return objT;
            }).ToList();
        }
    }
    public static class Utility
    {
        public static string GenerateAccessToken(LoginResponse response)
        {
            var _secretKey= ConfigManager.AppSetting["JWT:Secret"]??string.Empty;
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(issuer: ConfigManager.AppSetting["JWT:ValidIssuer"],
                                                    audience: ConfigManager.AppSetting["JWT:ValidAudience"],
                                                    claims: new List<Claim>()
                                                    {
                                                        new Claim("role",response.UserResponse.Role),
                                                        new Claim("roleCode",response.UserResponse.RoleCode),
                                                        new Claim("email",response.UserResponse.Email),
                                                        new Claim("firstName",response.UserResponse.FirstName),
                                                        new Claim("lastName",response.UserResponse.LastName),
                                                        new Claim("userId",response.UserResponse.Id.ToString()),
                                                        new Claim("roleId",response.UserResponse.RoleId.ToString()),
                                                        new Claim("memberId",response.UserResponse?.MemberId==null?string.Empty:response?.UserResponse?.MemberId?.ToString()),
                                                    },
                                                    expires: DateTime.Now.AddDays(7),
                                                    signingCredentials: signinCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
    }
}
