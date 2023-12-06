using Application.Dtos;
using Application.Extentions;
using Application.Ports.Services;
using Application.Services;
using Application.Validators;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Application.AutoMapperProfiles;
using WebApi.Extentions;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {

                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    builder => builder.MigrationsAssembly("Infrastructure"));
                options.LogTo(Console.WriteLine);
            });
            builder.Services.AddScoped<CancellationTokenSource>();
            builder.Services.AddAutoMapper(typeof(ChatsProfile).Assembly);
            builder.Services.ConfigureRepositories();
            builder.Services.ConfigureServices();
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSwaggerService();
            builder.Services.AddControllers();
            builder.Services.ConfigureValidation();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseExceptionHandlerMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}