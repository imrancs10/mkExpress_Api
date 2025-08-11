using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using MKExpress.API.Config;
using MKExpress.API.Extension;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MKExpress.API.Middleware
{
    public class JwtMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private ILogger<JwtMiddleware> _logger;
        private static Guid _userId;
        private static string? _userRole;
        private static Guid? _memberId;
        private static List<string> _skippedRoutes = [];
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var endpoint = context.GetEndpoint();

                _logger = context.RequestServices.GetRequiredService<ILogger<JwtMiddleware>>();
                _skippedRoutes = ConfigManager.AppSetting["JWT:SkippedRoutes"].Get<string>();
                // Skip JWT validation if endpoint has [AllowAnonymous] attribute
                if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                {
                    await _next(context);
                    return;
                }
                var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
                var path = context.Request.Path;
                if ((token == null || !ValidateToken(token)) && !_skippedRoutes.Contains(path.Value ?? string.Empty))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token invalid");
                    _logger.LogWarning("JwtMiddleware:Invoke: Unauthorized access attempt to {Path} with token: {Token}", path, token);
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError("JwtMiddleware:Invoke: Error occurred while  validating token :=>{msg}", ex.Message);
                throw;
            }
        }

        private static bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecret = ConfigManager.AppSetting["JWT:SecretKey"]??string.Empty;

            var key = Encoding.UTF8.GetBytes(jwtSecret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = ConfigManager.AppSetting["JWT:ValidIssuer"],
                    ValidAudience = ConfigManager.AppSetting["JWT:ValidAudience"],
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.First(x => x.Type == "userId").Value;
                var memberId = jwtToken.Claims.FirstOrDefault(x => x.Type == "memberId")?.Value??null;
                _userRole =jwtToken.Claims.First(x=>x.Type=="roleCode").Value;
                if (!Guid.TryParse(userId, out _userId))
                    return false;

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Guid GetUserId()
        {
            return _userId;
        }
        public static Guid? GetMemberId()
        {
            return _memberId;
        }

        public static string? GetUserRole()
        {
            return _userRole;
        }
    }

}
