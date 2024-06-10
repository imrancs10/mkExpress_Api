using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class AssignMemberController : ControllerBase
    {
        private readonly IAssignShipmentMemberService _service;
        public AssignMemberController(IAssignShipmentMemberService service)
        {
            _service = service;
        }

        [ProducesResponseType(typeof(List<CustomerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.GetCourierRunsheetPath)]
        public async Task<PagingResponse<AssignShipmentMemberResponse>> GetCourierRunsheet([FromQuery]PagingRequest pagingRequest, [FromQuery]Guid memberId)
        {
            return await _service.GetCourierRunsheet(pagingRequest, memberId);
        }
    }
}
