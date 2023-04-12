using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WalletApi.ExentsionsMethods
{
    public static class SecurityMethods 
    {
     
        public static void AddCustomSecurity(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddCustomAuthentication(configuration);
        }

        public static void AddCustomAuthentication(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                string key = configuration["JwtSecret"];
                option.SaveToken = true;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true
                };
            });
        }
    }
}
