using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MKExpress.API.Config;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MKExpress.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private static string _userId;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            // Skip JWT validation if endpoint has [AllowAnonymous] attribute
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var path = context.Request.Path;
        if (token == null || !ValidateToken(token))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token invalid");
                return;
            }

            await _next(context);
        }

        private static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(ConfigManager.AppSetting["JWT:Secret"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
              //      ValidateIssuer = true,
               //     ValidateAudience = true,
                ValidIssuer = ConfigManager.AppSetting["JWT:ValidIssuer"],
                   ValidAudience = ConfigManager.AppSetting["JWT:ValidAudience"],
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                _userId = jwtToken.Claims.First(x => x.Type == "userId").Value;

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static string GetUserId()
        {
            return _userId;
        }
    }

}
