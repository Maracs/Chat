using ApiGateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Reflection;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Configuration
            .AddJsonFile(builder.Configuration["Ocelot:JsonFile"], optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddSwaggerForOcelot(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureCors(config);
            var app = builder.Build();
            app.UseCors();
            app.UseRouting();

            app.UseSwaggerForOcelotUI(opt =>
            {
                app.UseOcelotSwagger(config);
            });

            app.UseOcelot().Wait();
            app.Run();
        }
    }
}