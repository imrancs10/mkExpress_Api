using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Logging;
using MKExpress.API.Config;
using MKExpress.API.Data;
using MKExpress.API.Dto;
using MKExpress.API.Middleware;
using MKExpress.API.SignalR;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;

var _policyName = "ImkExpressPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.RegisterDataServices(builder.Configuration);
builder.Services.RegisterValidators();
builder.Services.AddControllers();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddHealthChecks()
    .AddCheck("api_alive", () => HealthCheckResult.Healthy("API is running"))
    .AddDbContextCheck<MKExpressContext>("db_context_reachable")
    .AddCheck<AllowedDomainHealthCheck>("allowed_domain")
    .AddCheck<DbInfoHealthCheck>("db_info");
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddJWTConfig();
builder.Services.AddHealthChecks();
var allowedHost = builder.Configuration.GetSection("AllowedHosts").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(_policyName,
        policy =>
        {
            policy
            .WithOrigins(allowedHost ?? ["*"])
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        }
    );
});
builder.Services.AddSignalR();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    IdentityModelEventSource.ShowPII = true;
}
app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    if (app.Environment.IsDevelopment())
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "IMK Express Web API Development");
    else
        options.SwaggerEndpoint("/swagger/V1/swagger.json", "IMK Express Web API Production");
    options.DocExpansion(DocExpansion.None);
});
app.UseHttpsRedirection();
app.UseRouting();

app.UseMiddleware<ValidationMiddleware>();
app.UseCors(_policyName);
app.UseCustomExceptionHandler();
app.AddHealthCheck();
app.UseMiddleware<JwtMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ShipmentTrackingSingleRHub>("/shipment/tracking/live")
    .RequireCors(_policyName);
});
app.Run();
