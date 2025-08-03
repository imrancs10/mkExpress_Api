using FluentValidation;
using System.Text.Json;
namespace MKExpress.API.Middleware
{
    public class ValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        private readonly List<string> _allowedMethods =
        [
            HttpMethods.Post,
            HttpMethods.Put,
            HttpMethods.Get,
            HttpMethods.Delete
        ];

        public async Task InvokeAsync(HttpContext context, IServiceProvider services)
        {
            if (_allowedMethods.Contains(context.Request.Method))
            {
                var endpoint = context.GetEndpoint();
                var parameters = endpoint?.Metadata
                    .OfType<Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor>()
                    .FirstOrDefault()?.Parameters;

                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                var routeEndpoint = endpoint as RouteEndpoint;
                var parameterType = routeEndpoint?
                    .Metadata
                    .OfType<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()
                    .FirstOrDefault()?
                    .Parameters
                    .FirstOrDefault()
                    ?.ParameterType;

                if (parameterType != null)
                {
                    var model = JsonSerializer.Deserialize(body, parameterType);

                    var validatorType = typeof(IValidator<>).MakeGenericType(parameterType);

                    if (services.GetService(validatorType) is IValidator validator)
                    {
                        var result = await validator.ValidateAsync(new ValidationContext<object>(model));

                        if (!result.IsValid)
                        {
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            context.Response.ContentType = "application/json";

                            var errors = result.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(e => e.ErrorMessage).ToArray()
                                );

                            await context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                message = "Validation failed.",
                                errors
                            }));

                            return;
                        }
                    }
                }
            }

            await _next(context);
        }
    }
}
