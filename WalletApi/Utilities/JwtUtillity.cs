using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WalletApi.Domain;

namespace WalletApi.Utilities
{
    public class JwtUtility : IJwtUtility
    {
        private readonly IConfiguration _config;
        private readonly string _jwtSecret;

        public JwtUtility(IConfiguration config)
        {
            _config = config;
            _jwtSecret = _config.GetValue<string>("JwtSecret");
        }

        public string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IJwtUtility
    {
        string GenerateJwtToken(Users user);
        bool ValidateJwtToken(string token);
    }
}
