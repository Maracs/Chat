using BusinessLayer.AutoMapperProfile;
using BusinessLayer.Contracts;
using BusinessLayer.Extensions;
using PresentationLayer.Middlewares;
using BusinessLayer.Services;
using DataAccessLayer.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using PresentationLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(typeof(CancellationToken), serviceProvider =>
{
    IHttpContextAccessor httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    return httpContext.HttpContext?.RequestAborted ?? CancellationToken.None;
});
builder.Services.ConfigureMongoDb(builder.Configuration);
builder.Services.AddAutoMapper(typeof(SettingsProfile).Assembly);
builder.Services.AddIdentityService(builder.Configuration);
builder.Services.AddSwaggerService();
builder.Services.AddScoped<ISettingsService,SettingsService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.ConfigureLogging(builder, Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.ConfigureRedis(builder.Configuration["Redis:ConnectionString"]);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
