using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response.User;
using MKExpress.API.Models;

namespace MKExpress.API.Services
{
    public interface IUserRoleMenuMapperService
    {
        Task<bool> Add(List<UserRoleMenuMapperRequest> req);
        Task<bool> Delete(Guid RoleId);
        Task<List<UserRoleMenuMapperResponse>> GetByRoleId(Guid id);
        Task<List<UserRoleMenuMapperResponse>> GetAll();
        Task<List<UserRoleMenuMapperResponse>> SearchRolesAsync(string searchTerm);
    }
}
