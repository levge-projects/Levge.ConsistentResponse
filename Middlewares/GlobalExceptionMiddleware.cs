using Levge.ConsistentResponse.Models;
using Levge.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Levge.ConsistentResponse.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (LevgeValidationException ex)
            {
                await WriteResponse(context, 400, ApiResponse<object>.Fail("Validation failed.", ex.Errors.ToDictionary()));
            }
            catch (LevgeException ex)
            {
                await WriteResponse(context, 400, ApiResponse<object>.Fail(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Ticks}-{ThreadId}] Internal Server Error", DateTime.UtcNow.Ticks, Environment.CurrentManagedThreadId);
                await WriteResponse(context, 500, ApiResponse<object>.Fail("An unexpected error occurred."));
            }
        }

        private static async Task WriteResponse(HttpContext context, int statusCode, ApiResponse<object> response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
