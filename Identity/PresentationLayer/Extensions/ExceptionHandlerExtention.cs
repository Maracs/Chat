using PresentationLayer.Middlewares;
using Microsoft.AspNetCore.Builder;


namespace PresentationLayer.Extentions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
