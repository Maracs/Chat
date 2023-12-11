using WebApi.Middlewares;


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
