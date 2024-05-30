using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Services
{
    public interface IUserRoleService
    {
        Task<UserRoleResponse> AddRoleAsync(UserRoleRequest role);
        Task<bool> DeleteRoleAsync(Guid id);
        Task<bool> UpdateRoleAsync(UserRoleRequest role);
        Task<UserRoleResponse> GetRoleByIdAsync(Guid id);
        Task<PagingResponse<UserRoleResponse>> GetAllRolesAsync(PagingRequest request);
        Task<PagingResponse<UserRoleResponse>> SearchRolesAsync(SearchPagingRequest pagingRequest);
    }
}
