using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using ToDoApp1.Exceptions;

namespace ToDoApp1.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (ex is not NotFoundException)
                {
                    _logger.LogError(ex, "Sistemde beklenmeyen bir kriz oluştu! Hata: {ErrorMessage}", ex.Message);
                }
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";//JSON
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Sunucu tarafında beklenmeyen bir hata oluştu.";

            if (exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                message = exception.Message;
            }
            else if (exception is ValidationException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}