using BusinessLayer.AutoMapperProfiles;
using BusinessLayer.Extensions;
using BusinessLayer.Extentions;
using PresentationLayer.Extentions;
using PresentationLayer.Middlewares;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.ConfigureDataBase(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(BlockingsProfile).Assembly);

            builder.Services.ConfigureRepositories();
            builder.Services.AddUserRequestRepository(builder.Configuration);
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
            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName=="Docker")
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