using DataAccessLayer.Data;
using DotNetEnv;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DataAccessLayer.Repositories;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;

namespace PresentationLayer
{
    public static class Startup
    {
        public static void ConfigureDataBase(this IServiceCollection services, ConfigurationManager configuration)
        {

            Env.Load();
            var connectionString = configuration.GetConnectionString("Default");

            // Add services to the container.
            services.AddDbContext<DatabaseContext>(options =>
            {

                options.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsAssembly("DataAccessLayer"));
                options.LogTo(Console.WriteLine);
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBlockingsService, BlockingsService>();
            services.AddScoped<IFriendsService, FriendsService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IStatusesService, StatusesService>();
            services.AddScoped<ITokensService, TokensService>();
            services.AddScoped<IUsersService, UsersService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<BlockingsRepository>();
            services.AddScoped<FriendsRepository>();
            services.AddScoped<RolesRepository>();
            services.AddScoped<StatusesRepository>();
            services.AddScoped<UsersRepository>();
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
