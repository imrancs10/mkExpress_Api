using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class WorkDescriptionController : ControllerBase
    {
        private readonly IMasterWorkDescriptionService _workDescriptionService;
        public WorkDescriptionController(IMasterWorkDescriptionService workDescriptionService)
        {
            _workDescriptionService = workDescriptionService;
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.WorkDescriptionPath)]
        public async Task<MasterDataTypeResponse> AddWorkDescription([FromBody] MasterDataTypeRequest request)
        {
            return await _workDescriptionService.Add(request);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.WorkDescriptionPath)]
        public async Task<MasterDataTypeResponse> UpdateWorkDescription([FromBody] MasterDataTypeRequest request)
        {
            return await _workDescriptionService.Update(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.WorkDescriptionDeletePath)]
        public async Task<int> DeleteWorkDescription([FromRoute(Name = "id")] int id)
        {
            return await _workDescriptionService.Delete(id);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkDescriptionByIdPath)]
        public async Task<MasterDataTypeResponse> GetWorkDescription([FromRoute] int id)
        {
            return await _workDescriptionService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkDescriptionPath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> GetAllWorkDescription([FromQuery] PagingRequest pagingRequest)
        {
            return await _workDescriptionService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkDescriptionSearchPath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> SearchWorkDescription([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _workDescriptionService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(List<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkDescriptionByWorkTypePath)]
        public async Task<List<MasterDataTypeResponse>> GetWorkDescriptionByWorkType([FromQuery] string workType)
        {
            return await _workDescriptionService.GetByWorkTypes(workType);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.WorkDescriptionSaveOrderPath)]
        public async Task<int> SaveOrderWorkDescription([FromBody] List<OrderWorkDescriptionRequest> request)
        {
            return await _workDescriptionService.SaveOrderWorkDescription(request);
        }

        [ProducesResponseType(typeof(List<OrderWorkDescriptionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkDescriptionGetOrderPath)]
        public async Task<List<OrderWorkDescriptionResponse>> GetOrderWorkDescription([FromQuery] int orderDetailId)
        {
            return await _workDescriptionService.GetOrderWorkDescription(orderDetailId);
        }
    }
}
