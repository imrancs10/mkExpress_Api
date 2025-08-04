using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MKExpress.API.Data;


namespace MKExpress.API.Middleware
{
    public static class HealthCheck
    {
        public static WebApplication AddHealthCheck(this WebApplication app)
        {
            _ = app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = static async (context, report) =>
                {
                    context.Response.ContentType = "application/json";

                    var result = new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(e => new
                        {
                            name = e.Key,
                            status = e.Value.Status.ToString(),
                            description = e.Value.Description,
                            data = e.Value.Data.Any() ? e.Value.Data : null,
                            error = e.Value.Exception?.Message
                        })
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(result,
                        new JsonSerializerOptions { WriteIndented = true }));
                }
            });
            return app;
        }
    }
    public class DbInfoHealthCheck(MKExpressContext context,IConfiguration configuration) : IHealthCheck
    {
        private readonly MKExpressContext _context = context;
        private readonly IConfiguration _configuration = configuration;

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
                var conn = _context.Database.GetDbConnection();
                var data = new Dictionary<string, object?>
                {
                    { "Database", conn.Database },
                    { "DataSource", conn.DataSource },
                    { "ConnectionString", conn.ConnectionString },
                    {"AllowedDomain", _configuration.GetSection("AllowedHosts").Get<string[]>()}
                };

                return canConnect
                    ? HealthCheckResult.Healthy("Database connected", data)
                    : HealthCheckResult.Unhealthy("Database not reachable", null, data);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Exception occurred", ex);
            }
        }
    }
    public class AllowedDomainHealthCheck(IConfiguration configuration) : IHealthCheck
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var data = new Dictionary<string, object?>
                {
                    {"AllowedDomain", _configuration.GetSection("AllowedHosts").Get<string[]>()}
                };

                return HealthCheckResult.Healthy("Allowed Domain for CORS", data);
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Exception occurred", ex);
            }
        }
    }

}
