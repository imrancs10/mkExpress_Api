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
    public class MastersController : ControllerBase
    {
        private readonly IDesignCategoryService _designCategoryService;
        private readonly IJobTitleService _jobTitleService;
        public MastersController(IDesignCategoryService designCategoryService, IJobTitleService jobTitleService)
        {
            _designCategoryService = designCategoryService;
            _jobTitleService = jobTitleService;
        }

        [ProducesResponseType(typeof(DesignCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.DesignCategoryPath)]
        public async Task<DesignCategoryResponse> AddDesignCategory([FromBody] DesignCategoryRequest designCategoryRequest)
        {
            return await _designCategoryService.Add(designCategoryRequest);
        }

        [ProducesResponseType(typeof(DesignCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.DesignCategoryPath)]
        public async Task<DesignCategoryResponse> UpdateDesignCategory([FromBody] DesignCategoryRequest designCategoryRequest)
        {
            return await _designCategoryService.Update(designCategoryRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.DesignCategoryDeletePath)]
        public async Task<int> DeleteDesignCategory([FromRoute(Name = "id")] int designCategoryId)
        {
            return await _designCategoryService.Delete(designCategoryId);
        }

        [ProducesResponseType(typeof(DesignCategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DesignCategoryResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignCategoryByIdPath)]
        public async Task<DesignCategoryResponse> GetDesignCategory([FromRoute] int id)
        {
            return await _designCategoryService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<DesignCategoryResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<DesignCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignCategoryPath)]
        public async Task<PagingResponse<DesignCategoryResponse>> GetAllDesignCategory([FromQuery] PagingRequest pagingRequest)
        {
            return await _designCategoryService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<DesignCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DesignCategorySearchPath)]
        public async Task<PagingResponse<DesignCategoryResponse>> SearchDesignCategory([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _designCategoryService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(JobTitleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.JobTitlePath)]
        public async Task<JobTitleResponse> AddJobTitle([FromBody] JobTitleRequest jobTitleRequest)
        {
            return await _jobTitleService.Add(jobTitleRequest);
        }

        [ProducesResponseType(typeof(JobTitleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.JobTitlePath)]
        public async Task<JobTitleResponse> UpdateJobTitle([FromBody] JobTitleRequest jobTitleRequest)
        {
            return await _jobTitleService.Update(jobTitleRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.JobTitleDeletePath)]
        public async Task<int> DeleteJobTitle([FromRoute(Name = "id")] int id)
        {
            return await _jobTitleService.Delete(id);
        }

        [ProducesResponseType(typeof(JobTitleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(JobTitleResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.JobTitleByIdPath)]
        public async Task<JobTitleResponse> GetJobTitle([FromRoute] int id)
        {
            return await _jobTitleService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<JobTitleResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<JobTitleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.JobTitlePath)]
        public async Task<PagingResponse<JobTitleResponse>> GetAllJobTitle([FromQuery] PagingRequest pagingRequest)
        {
            return await _jobTitleService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<JobTitleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.JobTitleSearchPath)]
        public async Task<PagingResponse<JobTitleResponse>> SearchJobTitle([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _jobTitleService.Search(searchPagingRequest);
        }
    }
}
