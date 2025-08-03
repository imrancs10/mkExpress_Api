using FluentValidation;
using MKExpress.API.DTO.Request;
using MKExpress.API.Validations;

namespace MKExpress.API.Middleware
{
    public static class RegisterValidator
    {
        public static IServiceCollection RegisterValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserRequest>, UserRequestValidator>()
                .AddScoped<IValidator<PasswordChangeRequest>, ChangePasswordValidator>();

            return services;

        }
    }
}
