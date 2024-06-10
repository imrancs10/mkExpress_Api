using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IUserRoleRepository
    {
        Task<UserRole> AddRoleAsync(UserRole role);
        Task<bool> DeleteRoleAsync(Guid id);
        Task<UserRole> GetRoleByCode(string roleCode);
        Task<bool> UpdateRoleAsync(UserRole role);
        Task<UserRole> GetRoleByIdAsync(Guid id);
        Task<PagingResponse<UserRole>> GetAllRolesAsync(PagingRequest request);
        Task<PagingResponse<UserRole>> SearchRolesAsync(SearchPagingRequest pagingRequest);
    }
}
