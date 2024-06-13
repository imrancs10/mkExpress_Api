namespace MKExpress.API.Enums
{
    public enum UserActivityType
    {
        Login=1, 
        Logout=2,
        ChangePassword=3,
        ResetPasswordLinkSend=4,
        ResetPassword = 5,
        VerifyEmail=6,
        LoginFailed,
        WrongCredential
    }
}
