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
    public class PurchaseEntryController : ControllerBase
    {
        private readonly IPurchaseEntryService _purchaseEntryService;

        public PurchaseEntryController(IPurchaseEntryService purchaseEntryService)
        {
            _purchaseEntryService = purchaseEntryService;
        }

        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.PurchaseEntryPath)]
        public async Task<PurchaseEntryResponse> AddPurchaseEntry([FromBody] PurchaseEntryRequest purchaseEntryRequest)
        {
            return await _purchaseEntryService.Add(purchaseEntryRequest);
        }

        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.PurchaseEntryPath)]
        public async Task<PurchaseEntryResponse> UpdatePurchaseEntry([FromBody] PurchaseEntryRequest PurchaseEntryRequest)
        {
            return await _purchaseEntryService.Update(PurchaseEntryRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.PurchaseEntryDeletePath)]
        public async Task<int> DeletePurchaseEntry([FromRoute(Name = "id")] int PurchaseEntryId)
        {
            return await _purchaseEntryService.Delete(PurchaseEntryId);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.PurchaseEntryGetPurchaseNoPath)]
        public async Task<int> GetPurchaseNo()
        {
            return await _purchaseEntryService.GetPurchaseNo();
        }

        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.PurchaseEntryByIdPath)]
        public async Task<PurchaseEntryResponse> GetPurchaseEntry([FromRoute] int id)
        {
            return await _purchaseEntryService.Get(id);
        }

        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<PurchaseEntryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.PurchaseEntryPath)]
        public async Task<PagingResponse<PurchaseEntryResponse>> GetAllPurchaseEntry([FromQuery] PagingRequest pagingRequest)
        {
            return await _purchaseEntryService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PurchaseEntryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.PurchaseEntrySearchPath)]
        public async Task<PagingResponse<PurchaseEntryResponse>> SearchPurchaseEntry([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _purchaseEntryService.Search(searchPagingRequest);
        }
    }
}
