using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductResponse> Add(ProductRequest productRequest)
        {
            Product product = _mapper.Map<Product>(productRequest);
            return _mapper.Map<ProductResponse>(await _productRepository.Add(product));
        }

        public async Task<int> Delete(int productId)
        {
            return await _productRepository.Delete(productId);
        }

        public async Task<ProductResponse> Get(int productId)
        {
            return _mapper.Map<ProductResponse>(await _productRepository.Get(productId));
        }

        public async Task<PagingResponse<ProductResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ProductResponse>>(await _productRepository.GetAll(pagingRequest));
        }

        public async Task<int> GetInvoiceNo()
        {
            //return await _productRepository.GetInvoiceNo();
            return 0;
        }

        public async Task<PagingResponse<ProductResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<ProductResponse>>(await _productRepository.Search(searchPagingRequest));
        }

        public async Task<ProductResponse> Update(ProductRequest productRequest)
        {
            Product product = _mapper.Map<Product>(productRequest);
            return _mapper.Map<ProductResponse>(await _productRepository.Update(product));
        }
    }
}
