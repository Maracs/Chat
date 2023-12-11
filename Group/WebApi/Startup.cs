using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;
using Application.Ports.Services;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace WebApi
{
    public static class Startup
    {
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
    }
}