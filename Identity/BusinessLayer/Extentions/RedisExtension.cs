using DataAccessLayer.Contracts;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Extentions
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddUserRequestRepository(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration["Redis:ConnectionString"];
            services.AddSingleton<IUserRequestRepository>(
                provider => new UserRequestRepository(connectionString)
            );

            return services;
        }
    }
}
