using Application.AutoMapperProfiles;
using Application.Extensions;
using Application.Extentions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Extentions;
using DotNetEnv;
using DotNetEnv.Configuration;
using System.Reflection;
using WebApi.Middlewares;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Env.Load();
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsAssembly("Infrastructure"));
                options.LogTo(Console.WriteLine);
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped(typeof(CancellationToken), serviceProvider =>
            {
                IHttpContextAccessor httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                return httpContext.HttpContext?.RequestAborted ?? CancellationToken.None;
            });
            builder.Services.AddAutoMapper(typeof(ChatsProfile).Assembly);
            builder.Services.ConfigureRepositories();
            builder.Services.ConfigureServices();
            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSwaggerService();
            builder.Services.AddControllers();
            builder.Services.ConfigureValidation();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureRedis(builder.Configuration["Redis:ConnectionString"]);
            builder.Services.ConfigureMassTransit(builder.Configuration);

            builder.Services.RegisterGrpcClient(builder.Configuration);
            builder.Services.ConfigureHttpClient(builder.Configuration);
            builder.Services.ConfigureLogging(builder, Assembly.GetExecutingAssembly().GetName().Name!);

            var app = builder.Build();

            app.UseCors();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseExceptionHandlerMiddleware();
            app.UseMiddleware<UserCacheMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}