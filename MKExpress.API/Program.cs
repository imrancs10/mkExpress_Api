using Microsoft.IdentityModel.Logging;
using MKExpress.API.Config;
using MKExpress.API.Dto;
using MKExpress.API.Middleware;
using MKExpress.API.SignalR;
using Swashbuckle.AspNetCore.SwaggerUI;

var _policyName = "ImkExpressPolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterServices();
builder.Services.RegisterDataServices();
builder.Services.RegisterValidators();
builder.Services.AddControllers();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddJWTConfig();

builder.Services.AddCors(options =>
{
    options.AddPolicy(_policyName,
        policy =>
        {
            policy
            .WithOrigins("http://imkexpressksa.com", 
                            "http://localhost:3000", 
                            "http://localhost:3001", 
                            "http://imkexpress.com", 
                            "http://web.imkexpress.com"
            )
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
