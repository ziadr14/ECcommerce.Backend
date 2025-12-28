using System.Net;
using System.Text.Json;
using ECom.API.Helper;
using Microsoft.Extensions.Caching.Memory;

namespace ECom.API.Middleware
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

                ApplySecurityHeaders(context);

                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleException(context, ex);
            }
        }


        private Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            string message;

            switch (ex)
            {
                case ArgumentException:
                    statusCode = StatusCodes.Status400BadRequest;
                    message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    message = ex.Message;
                    break;

                case InvalidOperationException:
                    statusCode = StatusCodes.Status403Forbidden;
                    message = ex.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    message = ex.Message;
                    break;

                default:
                    statusCode = StatusCodes.Status500InternalServerError;
                    message = "An unexpected error occurred!";
                    break;
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode,
                message
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
            );
        }


        private void ApplySecurityHeaders(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["Referrer-Policy"] = "no-referrer";
            context.Response.Headers["Permissions-Policy"] =
                "geolocation=(), microphone=(), camera=()";

            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                context.Response.Headers["Content-Security-Policy"] =
                    "default-src 'self' https:; " +
                    "script-src 'self' 'unsafe-inline' 'unsafe-eval' https:; " +
                    "style-src 'self' 'unsafe-inline' https:; " +
                    "img-src 'self' data: https:; " +
                    "font-src 'self' data: https:; " +
                    "connect-src 'self' https: ws: wss:;";
                return;
            }

            context.Response.Headers["Content-Security-Policy"] =
                "default-src 'self'; " +
                "img-src 'self' data: https:; " +
                "style-src 'self'; " +
                "script-src 'self'; " +
                "font-src 'self' data:; " +
                "frame-ancestors 'none';";
        }
    }
}
