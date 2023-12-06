﻿using FluentValidation.AspNetCore;
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
