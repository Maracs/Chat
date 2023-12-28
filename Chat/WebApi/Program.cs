using Application.AutoMapperProfiles;
using Application.Extensions;
using Application.Extentions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
            builder.Services.ConfigureMassTransit(builder.Configuration);
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