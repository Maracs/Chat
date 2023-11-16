using BusinessLayer.Extentions;
using BusinessLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {

                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("Default"),
                    builder => builder.MigrationsAssembly("Modsenfy.DataAccessLayer"));
                options.LogTo(Console.WriteLine);
            });

            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

          
            builder.Services.AddScoped<BlockingsRepository>();
            builder.Services.AddScoped<FriendsRepository>();
            builder.Services.AddScoped<RolesRepository>();
            builder.Services.AddScoped<StatusesRepository>();
            builder.Services.AddScoped<UsersRepository>();


            builder.Services.AddScoped<BlockingsService>();
            builder.Services.AddScoped<FriendsService>();
            builder.Services.AddScoped<RolesService>();
            builder.Services.AddScoped<StatusesService>();
            builder.Services.AddScoped<TokensService>();
            builder.Services.AddScoped<UsersService>();

            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSwaggerService();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}