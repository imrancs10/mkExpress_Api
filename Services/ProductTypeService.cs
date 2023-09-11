using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IMapper _mapper;
        public ProductTypeService(IProductTypeRepository productTypeRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepository = productTypeRepository;
        }
        public async Task<MasterDataResponse> Add(MasterDataRequest request)
        {
            ProductType productType = _mapper.Map<ProductType>(request);
            return _mapper.Map<MasterDataResponse>(await _productTypeRepository.Add(productType));
        }

        public async Task<int> Delete(int id)
        {
            return await _productTypeRepository.Delete(id);
        }

        public async Task<MasterDataResponse> Get(int id)
        {
            return _mapper.Map<MasterDataResponse>(await _productTypeRepository.Get(id));
        }

        public async Task<PagingResponse<MasterDataResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataResponse>>(await _productTypeRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<MasterDataResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataResponse>>(await _productTypeRepository.Search(searchPagingRequest));
        }

        public async Task<MasterDataResponse> Update(MasterDataRequest request)
        {
            ProductType productType = _mapper.Map<ProductType>(request);
            return _mapper.Map<MasterDataResponse>(await _productTypeRepository.Update(productType));
        }
    }
}
