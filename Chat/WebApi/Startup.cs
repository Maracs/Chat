using Application.Ports.Services;
using Application.Services;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;
using StackExchange.Redis;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WebApi
{
    public static class Startup
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

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IUserChatService, UserChatService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
