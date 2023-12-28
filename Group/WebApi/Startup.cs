using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Application.Ports.Services;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using DotNetEnv;
using DotNetEnv.Configuration;

namespace WebApi
{
    public static class Startup
    {
        public static void ConfigureDataBase(this IServiceCollection services,ConfigurationManager configuration)
        {

            Env.Load();

            var connectionString = configuration.GetConfiguredConnectionString("Local");

            services.AddDbContext<DatabaseContext>(options =>
             {
                 options.UseSqlServer(
                     connectionString,
                     builder => builder.MigrationsAssembly("Infrastructure"));
                 options.LogTo(Console.WriteLine);
             });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserGroupService, UserGroupService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUserGroupRepository, UserGroupRepository>();
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