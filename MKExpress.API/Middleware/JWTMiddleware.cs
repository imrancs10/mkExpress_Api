using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace MKExpress.API.Middleware
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachAccountToContext(context, token);
                
            await _next(context);
        }

        private void attachAccountToContext(HttpContext context, string token)
        {
            try
            {
                token = token.Replace("\"", "");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidAudiences= _configuration["JWT:ValidAudience"].Split(","),
                    ValidIssuers = _configuration["JWT:ValidIssuer"].Split(","),
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidIssuer= _configuration["JWT:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var role = jwtToken.Claims.First(x => x.Type == "role").Value;

                // attach account to context on successful jwt validation
                context.Items["role"] = role;
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}
