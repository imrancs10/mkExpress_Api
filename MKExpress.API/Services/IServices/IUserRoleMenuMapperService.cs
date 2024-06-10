using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IUserRoleMenuMapperService
    {
        Task<bool> Add(List<UserRoleMenuMapperRequest> req);
        Task<bool> DeleteByRoleId(Guid RoleId);
        Task<bool> DeleteById(Guid id);
        Task<List<UserRoleMenuMapperResponse>> GetByRoleId(Guid id);
        Task<List<UserRoleMenuMapperResponse>> GetAll();
        Task<List<UserRoleMenuMapperResponse>> SearchRolesAsync(string searchTerm);
    }
}
