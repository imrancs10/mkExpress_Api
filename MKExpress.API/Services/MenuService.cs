using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;
        private readonly IMapper _mapper;
        public MenuService(IMenuRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> AddMenuAsync(MenuRequest req)
        {
            var Menu = _mapper.Map<Menu>(req);
            return await _repository.AddMenuAsync(Menu);
        }

        public async Task<PagingResponse<MenuResponse>> GetAllMenusAsync(PagingRequest request)
        {
            return _mapper.Map<PagingResponse<MenuResponse>>(await _repository.GetAllMenusAsync(request));
        }

        public async Task<MenuResponse> GetMenuByIdAsync(Guid id)
        {
           return _mapper.Map<MenuResponse>(await _repository.GetMenuByIdAsync(id));
        }

        public async Task<PagingResponse<MenuResponse>> SearchMenusAsync(SearchPagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MenuResponse>>(await _repository.SearchMenusAsync(pagingRequest));
        }

        public async Task<bool> DeleteMenuAsync(Guid id)
        {
            return await _repository.DeleteMenuAsync(id);
        }

        public async Task<bool> UpdateMenuAsync(MenuRequest req)
        {
            var Menu = _mapper.Map<Menu>(req);
            return await _repository.UpdateMenuAsync(Menu);
        }
    }
}
