using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class DesignSampleController : ControllerBase
    {
        private readonly IDesignSampleService _designSampleService;

        public DesignSampleController(IDesignSampleService designSampleService)
        {
            _designSampleService = designSampleService;
        }

        [ProducesResponseType(typeof(DesignSampleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.DesignSamplePath)]
        public async Task<DesignSampleResponse> AddDesignSample([FromForm] DesignSampleRequest designSampleRequest)
        {
            return await _designSampleService.Add(designSampleRequest);
        }

        [ProducesResponseType(typeof(DesignSampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.DesignSamplePath)]
        public async Task<DesignSampleResponse> UpdateDesignSample([FromForm] DesignSampleRequest designSampleRequest)
        {
            return await _designSampleService.Update(designSampleRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.DesignSampleDeletePath)]
        public async Task<int> DeleteDesignSample([FromRoute(Name = "id")] int designSampleId)
        {
            return await _designSampleService.Delete(designSampleId);
        }

        [ProducesResponseType(typeof(DesignSampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DesignSampleResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignSampleByIdPath)]
        public async Task<DesignSampleResponse> GetDesignSample([FromRoute(Name = "id")] int designSampleId)
        {
            return await _designSampleService.Get(designSampleId);
        }

        [ProducesResponseType(typeof(PagingResponse<DesignSampleResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<DesignSampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignSamplePath)]
        public async Task<PagingResponse<DesignSampleResponse>> GetAllDesignSample([FromQuery] PagingRequest pagingRequest)
        {
            return await _designSampleService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(List<DesignSampleResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(List<DesignSampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignSampleByCategoryIdPath)]
        public async Task<List<DesignSampleResponse>> GetByCategoryDesignSample([FromRoute] int categoryId)
        {
            return await _designSampleService.GetByCategory(categoryId);
        }

        [ProducesResponseType(typeof(PagingResponse<DesignSampleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignSampleSearchPath)]
        public async Task<PagingResponse<DesignSampleResponse>> SearchDesignSample([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _designSampleService.Search(searchPagingRequest);
        }
    }
}
