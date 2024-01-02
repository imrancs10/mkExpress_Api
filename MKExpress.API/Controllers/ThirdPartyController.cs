using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Middleware;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class ThirdPartyController : ControllerBase
    {
        private readonly IThirdPartyCourierService _thirdPartyCourierService;
        public ThirdPartyController(IThirdPartyCourierService thirdPartyCourierService)
        {
            _thirdPartyCourierService = thirdPartyCourierService;
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(ThirdPartyCourierResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ThirdPartyPath)]
        public async Task<ThirdPartyCourierResponse> Add([FromBody] ThirdPartyCourierCompanyRequest request)
        {
           return await _thirdPartyCourierService.Add(request);
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ThirdPartyDeletePath)]
        public async Task<int> Delete([FromRoute] Guid id)
        {
            return await _thirdPartyCourierService.Delete(id);
        }

        [ProducesResponseType(typeof(ThirdPartyCourierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartyByIdPath)]
        public async Task<ThirdPartyCourierResponse> Get([FromRoute] Guid id)
        {
            return await _thirdPartyCourierService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ThirdPartyCourierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartyPath)]
        public async Task<PagingResponse<ThirdPartyCourierResponse>> GetAll([FromQuery] PagingRequest pagingRequest)
        {
            return await _thirdPartyCourierService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<ThirdPartyCourierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartyGetShipmentPath)]
        public async Task<List<ShipmentResponse>> GetShipments([FromRoute]Guid id, [FromRoute] DateTime fromDate, [FromRoute] DateTime toDate)
        {
            return await _thirdPartyCourierService.GetShipments(id, fromDate, toDate);
        }

        [ProducesResponseType(typeof(PagingResponse<ThirdPartyCourierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartySearchPath)]
        public async Task<PagingResponse<ThirdPartyCourierResponse>> Search([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _thirdPartyCourierService.Search(searchPagingRequest);
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(ThirdPartyCourierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ThirdPartyPath)]
        public async Task<ThirdPartyCourierResponse> Update([FromBody] ThirdPartyCourierCompanyRequest request)
        {
            return await _thirdPartyCourierService.Update(request);
        }


        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ThirdPartyAddShipmentPath)]
        public async Task<bool> GetShipments([FromBody] List<ThirdPartyShipmentRequest> request)
        {
            return await _thirdPartyCourierService.AddShipmentToThirdParty(request);
        }
    }
}
