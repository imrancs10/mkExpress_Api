using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Services.IServices
{
    public interface ILoginService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<UserResponse> RegisterUser(UserRequest request);
        Task<bool> ChangePassword(PasswordChangeRequest request);
        Task<string> ResetPassword(string userName);
        Task<string> VerifyEmail(string token);
        Task<bool> UpdateProfile(UserRequest request);
        Task<bool> AssignRole(string email, Guid roleId);
        Task<bool> ResetEmailVerificationCode(string email);
        Task<bool> DeleteUser(string email);
        Task<bool> BlockUser(string email);
    }
}
