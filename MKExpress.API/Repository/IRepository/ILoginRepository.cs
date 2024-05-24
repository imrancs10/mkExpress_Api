using MKExpress.API.DTO.Request;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface ILoginRepository
    {
        Task<User> Login(LoginRequest request);
        Task<User> RegisterUser(User request);
        Task<bool> ChangePassword(PasswordChangeRequest request);
        Task<bool> ResetPassword(string userName);
        Task<bool> VerifyEmail(string token);
        Task<bool> UpdateProfile(User request);
        Task<bool> IsUserExist(string email);
        Task<bool> AssignRole(string email,Guid roleId);
        Task<bool> ResetEmailVerificationCode(string email);
        Task<bool> DeleteUser(string email);
        Task<bool> BlockUser(string email);
    }
}
