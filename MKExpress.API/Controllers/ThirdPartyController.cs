using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
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


        [ProducesResponseType(typeof(ThirdPartyCourierResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ThirdPartyPath)]
        public async Task<ThirdPartyCourierResponse> Add(ThirdPartyCourierCompanyRequest request)
        {
           return await _thirdPartyCourierService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ThirdPartyDeletePath)]
        public async Task<int> Delete(Guid id)
        {
            return await _thirdPartyCourierService.Delete(id);
        }

        [ProducesResponseType(typeof(ThirdPartyCourierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartyByIdPath)]
        public async Task<ThirdPartyCourierResponse> Get(Guid id)
        {
            return await _thirdPartyCourierService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ThirdPartyCourierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartyPath)]
        public async Task<PagingResponse<ThirdPartyCourierResponse>> GetAll(PagingRequest pagingRequest)
        {
            return await _thirdPartyCourierService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<ThirdPartyCourierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartyGetShipmentPath)]
        public async Task<List<ShipmentResponse>> GetShipments(Guid id)
        {
            return await _thirdPartyCourierService.GetShipments(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ThirdPartyCourierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ThirdPartySearchPath)]
        public async Task<PagingResponse<ThirdPartyCourierResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return await _thirdPartyCourierService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(ThirdPartyCourierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ThirdPartyPath)]
        public async Task<ThirdPartyCourierResponse> Update(ThirdPartyCourierCompanyRequest request)
        {
            return await _thirdPartyCourierService.Update(request);
        }
    }
}
