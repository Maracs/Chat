using BusinessLayer.AutoMapperProfiles;
using BusinessLayer.DTOs;
using BusinessLayer.Extensions;
using BusinessLayer.Extentions;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using BusinessLayer.Validators;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Grpc.Configuration;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Env.Load();
            var connectionString = builder.Configuration.GetConnectionString("Default");

            // Add services to the container.
            builder.Services.AddDbContext<DatabaseContext>(options =>
            {

                options.UseSqlServer(
                    connectionString,
                    builder => builder.MigrationsAssembly("DataAccessLayer"));
                options.LogTo(Console.WriteLine);
            });

            builder.Services.AddAutoMapper(typeof(BlockingsProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(FriendsProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(ImagesProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(StatusesProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(UsersProfile).Assembly);

            builder.Services.AddScoped<BlockingsRepository>();
            builder.Services.AddScoped<FriendsRepository>();
            builder.Services.AddScoped<RolesRepository>();
            builder.Services.AddScoped<StatusesRepository>();
            builder.Services.AddScoped<UsersRepository>();

            builder.Services.AddScoped<IBlockingsService, BlockingsService>();
            builder.Services.AddScoped<IFriendsService, FriendsService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IStatusesService,StatusesService>();
            builder.Services.AddScoped<ITokensService,TokensService>();
            builder.Services.AddScoped<IUsersService,UsersService>();

            builder.Services.AddIdentityService(builder.Configuration);
            builder.Services.AddSwaggerService();

            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddScoped<IValidator<LoginDto>, LoginValidator>();
            builder.Services.AddScoped<IValidator<RoleDto>, RoleValidator>();
            builder.Services.AddScoped<IValidator<SignupDto>, SignupValidator>();
            builder.Services.AddScoped<IValidator<StatusDto>, StatusValidator>();
            builder.Services.AddScoped<IValidator<FullUserInfoWithoutIdDto>, UserValidator>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ConfigureMassTransit(builder.Configuration);
            builder.Services.ReristerRrpcService();

            var app = builder.Build();

            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseGrpcService();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName=="Docker")
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