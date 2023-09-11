using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable

namespace MKExpress.API.Repositories.Interfaces
{

    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<int> DeleteUser(int empId);
        Task<User> GetUser(string? email = null);
        Task<List<User>> GetUsers();
        Task<int> AddRoles(List<string> roles);
    }
}