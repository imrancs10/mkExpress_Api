using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{

    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductTypeService _productTypeService;
        public ProductController(IProductService productService, IProductTypeService productTypeService)
        {
            _productService = productService;
            _productTypeService = productTypeService;
        }

        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ProductPath)]
        public async Task<ProductResponse> AddProduct([FromBody] ProductRequest productRequest)
        {
            return await _productService.Add(productRequest);
        }

        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ProductPath)]
        public async Task<ProductResponse> UpdateProduct([FromBody] ProductRequest productRequest)
        {
            return await _productService.Update(productRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ProductDeletePath)]
        public async Task<int> DeleteProduct([FromRoute(Name = "id")] int productId)
        {
            return await _productService.Delete(productId);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductGetInvoiceNoPath)]
        public async Task<int> GetInvoiceNo()
        {
            return await _productService.GetInvoiceNo();
        }

        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductByIdPath)]
        public async Task<ProductResponse> GetProduct([FromRoute] int id)
        {
            return await _productService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ProductResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductPath)]
        public async Task<PagingResponse<ProductResponse>> GetAllProduct([FromQuery] PagingRequest pagingRequest)
        {
            return await _productService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductSearchPath)]
        public async Task<PagingResponse<ProductResponse>> SearchProduct([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _productService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ProductTypePath)]
        public async Task<MasterDataResponse> AddProductType([FromBody] MasterDataRequest masterDataRequest)
        {
            return await _productTypeService.Add(masterDataRequest);
        }

        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ProductTypePath)]
        public async Task<MasterDataResponse> UpdateProductType([FromBody] MasterDataRequest masterDataRequest)
        {
            return await _productTypeService.Update(masterDataRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ProductTypeDeletePath)]
        public async Task<int> DeleteProductType([FromRoute(Name = "id")] int productId)
        {
            return await _productTypeService.Delete(productId);
        }

        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductTypeByIdPath)]
        public async Task<MasterDataResponse> GetProductType([FromRoute] int id)
        {
            return await _productTypeService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductTypePath)]
        public async Task<PagingResponse<MasterDataResponse>> GetAllProductType([FromQuery] PagingRequest pagingRequest)
        {
            return await _productTypeService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ProductTypeSearchPath)]
        public async Task<PagingResponse<MasterDataResponse>> SearchTypeProduct([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _productTypeService.Search(searchPagingRequest);
        }
    }
}
