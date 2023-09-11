using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class DesignCategoryService : IDesignCategoryService
    {
        private readonly IDesignCategoryRepository _designCategoryRepository;
        private readonly IMapper _mapper;
        public DesignCategoryService(IDesignCategoryRepository designCategoryRepository, IMapper mapper)
        {
            _designCategoryRepository = designCategoryRepository;
            _mapper = mapper;
        }
        public async Task<DesignCategoryResponse> Add(DesignCategoryRequest supplierRequest)
        {
            MasterDesignCategory masterDesignCategory = _mapper.Map<MasterDesignCategory>(supplierRequest);
            return _mapper.Map<DesignCategoryResponse>(await _designCategoryRepository.Add(masterDesignCategory));
        }

        public async Task<int> Delete(int designCategoryId)
        {
            return await _designCategoryRepository.Delete(designCategoryId);
        }

        public async Task<DesignCategoryResponse> Get(int designCategoryId)
        {
            return _mapper.Map<DesignCategoryResponse>(await _designCategoryRepository.Get(designCategoryId));
        }

        public async Task<PagingResponse<DesignCategoryResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<DesignCategoryResponse>>(await _designCategoryRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<DesignCategoryResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<DesignCategoryResponse>>(await _designCategoryRepository.Search(searchPagingRequest));
        }

        public async Task<DesignCategoryResponse> Update(DesignCategoryRequest designCategoryRwquest)
        {
            MasterDesignCategory masterDesignCategory = _mapper.Map<MasterDesignCategory>(designCategoryRwquest);
            return _mapper.Map<DesignCategoryResponse>(await _designCategoryRepository.Update(masterDesignCategory));
        }
    }
}
