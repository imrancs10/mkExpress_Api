namespace MKExpress.API.Contants
{
    public static class ValidationMessage
    {
        public const string InvalidGender = "Gender should be numeric. IPreferNotToSay=0, Male=1, Female=2";
        public const string InvalidMobileNo = "Your mobile number is invalid.";
        public const string InvalidPasswordMinLength = "Password must contain atleast 8 char.";
        public const string InvalidPasswordMaxLength = "Your password length must not exceed 16.";
        public const string InvalidPasswordFormat = "Password should be in base64 format.";
        public const string NewPasswordNotMatch = "New password and confirm password should be same";
        public const string PasswordNotContainUpperCase = "Your password must contain at least one uppercase letter.";
        public const string PasswordNotContainLowerCase = "Your password must contain at least one lowercase letter.";
        public const string PasswordNotContainNumber = "Your password must contain at least one number.";
        public const string ResetPasswordEmailSentSuccess = "We will send an email to the register email ID.";
        public const string EmailVerificationSuccess = "You have verified your email ID.";
        public const string EmailVerificationFail = "Email reset link is invalid or  expired.";
        public const string PasswordNotContainSpecialChar = "Your password must contain at least one (!?@*._-)";
    }
}
