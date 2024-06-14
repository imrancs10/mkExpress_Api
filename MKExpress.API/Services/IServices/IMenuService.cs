using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IMenuService
    {
        Task<bool> AddMenuAsync(MenuRequest menu);
        Task<bool> DeleteMenuAsync(Guid id);
        Task<bool> UpdateMenuAsync(MenuRequest menu);
        Task<MenuResponse> GetMenuByIdAsync(Guid id);
        Task<PagingResponse<MenuResponse>> GetAllMenusAsync(PagingRequest request);
        Task<PagingResponse<MenuResponse>> SearchMenusAsync(SearchPagingRequest pagingRequest);
    }
}
