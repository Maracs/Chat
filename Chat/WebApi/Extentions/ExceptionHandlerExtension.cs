using WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;


namespace WebApi.Extentions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
