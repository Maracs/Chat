using DataAccessLayer.Contracts;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.Repositories.Config;
using DataAccessLayer.Seeders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DataAccessLayer.Extensions 
{
    public static class MongoRegistrationExtensions
    {
        public static IServiceCollection ConfigureMongoDb(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var section = configuration.GetSection("MongoDb");
            services.Configure<MongoDbSettings<SettingsInfo>>(section);
            services.AddSingleton<IMongoSeeder<SettingsInfo>, SettingsSeeder>();
            services.AddScoped<SettingsRepository>();

            return services;
        }
    }
}
