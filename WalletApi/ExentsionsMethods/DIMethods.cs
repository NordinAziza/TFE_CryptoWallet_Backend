using WalletApi.Domain;
using WalletApi.Infrastructure.Repositories;
using WalletApi.Utilities;

namespace WalletApi.ExentsionsMethods
{
    public static class DIMethods
    {
        #region Public Methods
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IHashService, SHA256HashService>();
            services.AddScoped<IJwtUtility ,JwtUtility>();
        }
        #endregion
    }
}
