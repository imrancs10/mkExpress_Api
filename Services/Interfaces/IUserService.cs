using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{

    public interface IUserService
    {
        Task<UserResponse> CreateUser(UserRegistrationRequest customerRegistrationRequest);
        Task<UserResponse> UpdateUser(UpdateUserRequest updateCustomerRequest);
        Task<UserResponse> GetUser(string email = null);
        Task<List<UserResponse>> GetUsers();
        Task<int> DeleteUser(int empId);
    }
}