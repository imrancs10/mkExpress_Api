using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class SystemActionController : ControllerBase
    {
        private readonly ISystemActionService _systemActionService;
        public SystemActionController(ISystemActionService systemActionService)
        {
            _systemActionService = systemActionService;
        }

        [ProducesResponseType(typeof(PagingResponse<SystemActionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.SystemActionGetAllPath)]
        public async Task<PagingResponse<SystemActionResponse>> GetSystemActions([FromQuery] SystemActionRequest request)
        {
            return await _systemActionService.GetSystemActions(request);
        }
    }
}
