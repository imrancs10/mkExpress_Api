using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IUserRoleMenuMapperRepository
    {
        Task<bool> Add(List<UserRoleMenuMapper> req);
        Task<bool> Delete(Guid RoleId);
        Task<List<UserRoleMenuMapper>> GetByRoleId(Guid id);
        Task<List<UserRoleMenuMapper>> GetAll();
        Task<List<UserRoleMenuMapper>> SearchRolesAsync(string searchTerm);
    }
}
