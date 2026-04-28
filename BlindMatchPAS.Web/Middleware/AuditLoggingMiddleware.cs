using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using BlindMatchPAS.Infrastructure.Data;
using BlindMatchPAS.Core.Entities;
using System.Diagnostics;
using System.Security.Claims;

namespace BlindMatchPAS.Web.Middleware
{
    public class AuditLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditLoggingMiddleware> _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger, IServiceScopeFactory scopeFactory)
        {
            _next = next;
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            
            var user = context.User?.Identity?.Name ?? "Anonymous";
            var role = context.User?.FindFirst(ClaimTypes.Role)?.Value ?? "None";
            var path = context.Request.Path;
            var method = context.Request.Method;

            _logger.LogInformation("Processing {Method} request for {Path} by {User} (Role: {Role})", method, path, user, role);

            await _next(context);

            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation("Finished processing {Method} request for {Path} with status {StatusCode} in {ElapsedMs}ms", 
                method, path, statusCode, stopwatch.ElapsedMilliseconds);
            
            if (method != "GET" || statusCode >= 400)
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                    var auditLog = new AuditLog
                    {
                        UserEmail = user,
                        Action = method,
                        Path = path,
                        StatusCode = statusCode,
                        ElapsedMilliseconds = stopwatch.ElapsedMilliseconds,
                        Timestamp = DateTime.UtcNow,
                        Details = $"IP: {context.Connection.RemoteIpAddress}"
                    };

                    dbContext.AuditLogs.Add(auditLog);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to persist audit log to database.");
                }
            }
        }
    }

    public static class AuditLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditLoggingMiddleware>();
        }
    }
}
