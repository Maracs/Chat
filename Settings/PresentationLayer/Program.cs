using BusinessLayer.AutoMapperProfile;
using BusinessLayer.Contracts;
using BusinessLayer.Extensions;
using BusinessLayer.Services;
using DataAccessLayer.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

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
