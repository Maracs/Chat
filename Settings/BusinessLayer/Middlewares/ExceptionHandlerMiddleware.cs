using BusinessLayer.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace BusinessLayer.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private RequestDelegate _next;

        private ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ApiException ex)
            {
                await HandleApiExceptionMessageAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception)
            {
                await HandleInternalServerErrorAsync(context);
            }
        }

        private async Task HandleApiExceptionMessageAsync(HttpContext context, ApiException exception)
        {
            _logger.LogWarning("Error occured: {ErrorMessage}", exception.Message);

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = exception.StatusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ((int)exception.StatusCode);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleInternalServerErrorAsync(HttpContext context)
        {
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = 500,
                ErrorMessage = "Internal Server Error"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(result);
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var message = exception.Errors.Select(err => err.ErrorMessage)
                .Aggregate((a, c) => $"{a}{c}");

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode =ApiException.ExceptionStatus.BadRequest,
                ErrorMessage = message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ApiException.ExceptionStatus.BadRequest;
            await context.Response.WriteAsync(result);
        }
    }
}
