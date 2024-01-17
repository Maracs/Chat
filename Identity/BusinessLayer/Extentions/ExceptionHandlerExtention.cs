using BusinessLayer.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace BusinessLayer.Extentions
{
    public static class ExceptionHandlerExtension
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
