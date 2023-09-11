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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.SupplierPath)]
        public async Task<SupplierResponse> AddSupplier([FromBody] SupplierRequest supplierRequest)
        {
            return await _supplierService.Add(supplierRequest);
        }

        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.SupplierPath)]
        public async Task<SupplierResponse> UpdateSupplier([FromBody] SupplierRequest supplierRequest)
        {
            return await _supplierService.Update(supplierRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.SupplierDeletePath)]
        public async Task<int> DeleteSupplier([FromRoute(Name = "id")] int supplierId)
        {
            return await _supplierService.Delete(supplierId);
        }

        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.SupplierByIdPath)]
        public async Task<SupplierResponse> GetSupplier([FromRoute] int id)
        {
            return await _supplierService.Get(id);
        }

        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<SupplierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.SupplierPath)]
        public async Task<PagingResponse<SupplierResponse>> GetAllSupplier([FromQuery] PagingRequest pagingRequest)
        {
            return await _supplierService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(SupplierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.SupplierSearchPath)]
        public async Task<PagingResponse<SupplierResponse>> SearchSupplier([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _supplierService.Search(searchPagingRequest);
        }
    }
}
