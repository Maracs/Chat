﻿using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FluentValidation;

namespace WebApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private static async Task HandleApiExceptionMessageAsync(HttpContext context, ApiException exception)
        {
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = exception.StatusCode,
                ErrorMessage = exception.Message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ((int)exception.StatusCode);
            await context.Response.WriteAsync(result);
        }

        private static async Task HandleInternalServerErrorAsync(HttpContext context)
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

        private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var message = exception.Errors.Select(err => err.ErrorMessage)
                .Aggregate((a, c) => $"{a}{c}");

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = ExceptionStatus.BadRequest,
                ErrorMessage = message
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)ExceptionStatus.BadRequest;
            await context.Response.WriteAsync(result);
        }
    }
}
