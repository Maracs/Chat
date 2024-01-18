using PresentationLayer.Middlewares;
using Microsoft.AspNetCore.Builder;


namespace PresentationLayer.Extensions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
