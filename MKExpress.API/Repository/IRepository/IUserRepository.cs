using MKExpress.API.DTO.Request;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IUserRepository
    {
       Task<User> Add(User user);
        Task<bool> ActiveDeactivate(string email);
        Task<bool> ChangePassword(PasswordChangeRequest changeRequest);
        Task<bool> ResetPassword(string userName);
        Task<bool> DeleteUser(string userName);
    }
}
