using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IMenuRepository
    {
        Task<Menu> AddMenuAsync(Menu menu);
        Task<bool> DeleteMenuAsync(Guid id);
        Task<bool> UpdateMenuAsync(Menu menu);
        Task<Menu> GetMenuByIdAsync(Guid id);
        Task<PagingResponse<Menu>> GetAllMenusAsync(PagingRequest request);
        Task<PagingResponse<Menu>> SearchMenusAsync(SearchPagingRequest pagingRequest);
    }
}
