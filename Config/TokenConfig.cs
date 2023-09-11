using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MKExpress.API.Config
{

    public class TokenConfig
    {
        private readonly IConfiguration _configuration;

        public TokenConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetJwtToken(ClaimsIdentity claimsIdentity)
        {
            var jwtSecret = _configuration["JwtSecret"];
            if (string.IsNullOrEmpty(jwtSecret)) throw new ArgumentException();
            var key = Encoding.UTF8.GetBytes(jwtSecret);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = _configuration["JwtIssuer"],
                Audience = _configuration["JwtAudience"],
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}