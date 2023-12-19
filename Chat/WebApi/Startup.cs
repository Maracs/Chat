using Application.Ports.Services;
using Application.Services;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;
using System.Reflection;
using System.Text.RegularExpressions;

namespace WebApi
{
    public static class Startup
    {
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

        public static string GetConfiguredConnectionString(this IConfiguration configuration, string variant="Default")
        {
            var connectionString = configuration.GetConnectionString(variant);

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("There is no such variant of connection string");
            }

            var matches = Regex.Matches(connectionString, @"\$\{\w+\}");

            foreach (Match match in matches)
            {
                var arg = match.Value;
                var envVariable = Environment.GetEnvironmentVariable(arg[2..^1]);

                if (envVariable is null)
                {
                    throw new ArgumentException($"There is no environment variable {arg[2..^1]}");
                }

                connectionString = connectionString.Replace(arg, envVariable);
            }

            return connectionString;
        }
    }
}
