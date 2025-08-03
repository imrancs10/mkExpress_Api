using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MKExpress.API.Config;
using System.Text;

namespace MKExpress.API.Middleware
{
    public static class JWTConfig
    {
        public static IServiceCollection AddJWTConfig(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = ConfigManager.AppSetting["JWT:ValidIssuer"],
                    //ValidAudience = ConfigManager.AppSetting["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigManager.AppSetting["JWT:Secret"]))
                };
            });
            return services;
        }
    }
}
