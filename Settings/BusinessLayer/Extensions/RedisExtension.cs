using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace BusinessLayer.Extensions
{
    public static class RedisExtension
    {
        public static void ConfigureRedis(this IServiceCollection services, string connectionString)
        {
            var configurationOptions = ConfigurationOptions.Parse(connectionString);

            services.AddScoped<IDatabase>(options =>
            {
                IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect(configurationOptions);
                return multiplexer.GetDatabase();
            });
        }
    }
}
