using System.Diagnostics;

namespace StudentManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // Request information
            _logger.LogInformation(
                "Request Started: {Method} {Path}",
                context.Request.Method,
                context.Request.Path
            );

            // Continue request pipeline
            await _next(context);

            stopwatch.Stop();

            // Response information
            _logger.LogInformation(
                "Request Completed: {StatusCode} in {ElapsedMilliseconds} ms",
                context.Response.StatusCode,
                stopwatch.ElapsedMilliseconds
            );
        }
    }
}