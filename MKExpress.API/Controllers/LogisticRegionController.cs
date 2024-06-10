using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class LogisticRegionController : ControllerBase
    {
        private readonly ILogisticRegionSerivce _serivce;
        public LogisticRegionController(ILogisticRegionSerivce serivce)
        {
            _serivce = serivce;
        }

        [ProducesResponseType(typeof(LogisticRegionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.LogisticRegionPath)]
        public async Task<LogisticRegionResponse> Add([FromBody] LogisticRegionRequest request)
        {
            return await _serivce.Add(request);
        }

        [ProducesResponseType(typeof(LogisticRegionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.LogisticRegionDeletePath)]
        public async Task<int> Delete([FromRoute] Guid id)
        {
            return await _serivce.Delete(id);
        }

        [ProducesResponseType(typeof(LogisticRegionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.LogisticRegionByIdPath)]
        public async Task<LogisticRegionResponse> Get([FromRoute] Guid id)
        {
            return await _serivce.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<LogisticRegionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.LogisticRegionPath)]
        public async Task<PagingResponse<LogisticRegionResponse>> GetAll([FromQuery]PagingRequest pagingRequest)
        {
           return await _serivce.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(LogisticRegionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.LogisticRegionSearchPath)]
        public async Task<PagingResponse<LogisticRegionResponse>> Search([FromQuery] SearchPagingRequest searchPagingRequest)
        {
           return await _serivce.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(LogisticRegionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.LogisticRegionPath)]
        public Task<LogisticRegionResponse> Update([FromBody] LogisticRegionRequest request)
        {
           return _serivce.Update(request);
        }
    }
}
