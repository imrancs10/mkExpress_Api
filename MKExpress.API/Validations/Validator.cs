using FluentValidation;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.Enums;
using MKExpress.API.Extension;
using System;

namespace MKExpress.API.Validations
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.FirstName).NotNull().MinimumLength(3).NotEmpty();
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.IsTcAccepted).Equal(true);
            RuleFor(x => x.Gender).IsInEnum().WithMessage(ValidationMessage.InvalidGender);
            RuleFor(p => p.Password).Custom(CommonValidator.CustomPasswordValidator);
            RuleFor(p => p.Mobile).Matches(@"^[6789]\d{9}").WithMessage(ValidationMessage.InvalidMobileNo);
        }
    }

    public class ChangePasswordValidator : AbstractValidator<PasswordChangeRequest>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().NotNull();
            RuleFor(x => x.ConfirmNewPassword).NotEmpty().NotNull();
            RuleFor(p => p.NewPassword)
                   .NotEmpty()
                   .NotNull();
            RuleFor(p => p.NewPassword).Custom(CommonValidator.CustomPasswordValidator);
            RuleFor(p => p).Must(x => x.NewPassword.Equals(x.ConfirmNewPassword)).WithMessage(ValidationMessage.NewPasswordNotMatch);
        }
    }

    public static class RegisterValidators
    {
        public static IServiceCollection RegisterValidator(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserRequest>, UserRequestValidator>()
                 .AddScoped<IValidator<PasswordChangeRequest>, ChangePasswordValidator>();
            return services;
        }
    }

    public static class CommonValidator
    {
       public static void  CustomPasswordValidator<T>(string password,ValidationContext<T> context)
        {
            if (!password.IsBase64())
            {
                context.AddFailure(ValidationMessage.InvalidPasswordFormat);
                return;
            }
            else
            password = password.DecodeBase64();

            if (!password.MinimumLength(8))
                context.AddFailure(ValidationMessage.InvalidPasswordMinLength);
            if (!password.MaximumLength(16))
                context.AddFailure(ValidationMessage.InvalidPasswordMaxLength);
            if (!password.Matches(@"[A-Z]+"))
                context.AddFailure(ValidationMessage.PasswordNotContainUpperCase);
            if (!password.Matches(@"[a-z]+"))
                context.AddFailure(ValidationMessage.PasswordNotContainLowerCase);
            if (!password.Matches(@"[0-9]+")) 
                context.AddFailure(ValidationMessage.PasswordNotContainNumber);
            if (!password.Matches(@"[\!\?\*\.\@_-]+")) 
                context.AddFailure(ValidationMessage.PasswordNotContainSpecialChar);
        }
    }


}