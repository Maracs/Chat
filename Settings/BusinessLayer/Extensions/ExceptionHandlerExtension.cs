using BusinessLayer.Middlewares;
using Microsoft.AspNetCore.Builder;


namespace BusinessLayer.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
