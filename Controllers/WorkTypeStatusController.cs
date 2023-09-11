using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class WorkTypeStatusController : ControllerBase
    {
        private readonly IWorkTypeStatusService _workTypeStatusService;

        public WorkTypeStatusController(IWorkTypeStatusService workTypeStatusService)
        {
            _workTypeStatusService = workTypeStatusService;
        }

        [ProducesResponseType(typeof(List<WorkTypeStatusResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<WorkTypeStatusResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkTypeStatusPath)]
        public async Task<List<WorkTypeStatusResponse>> GetByOrderDetailId([FromQuery] int orderDetailId)
        {
            return await _workTypeStatusService.GetByOrderDetailId(orderDetailId);
        }

        [ProducesResponseType(typeof(List<WorkTypeStatusResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<WorkTypeStatusResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkTypeStatusByOrderIdPath)]
        public async Task<List<WorkTypeStatusResponse>> GetByOrderId([FromQuery] int orderId)
        {
            return await _workTypeStatusService.GetByOrderId(orderId);
        }

        [ProducesResponseType(typeof(WorkTypeSumAmountResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.WorkTypeSumAmountPath)]
        public async Task<List<WorkTypeSumAmountResponse>> WorkTypeSumAmount([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            return await _workTypeStatusService.WorkTypeSumAmount(fromDate, toDate);
        }


        [ProducesResponseType(typeof(WorkTypeStatusResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.WorkTypeStatusPath)]
        public async Task<WorkTypeStatusResponse> Update([FromBody] WorkTypeStatusRequest workTypeStatusRequest)
        {
            return await _workTypeStatusService.Update(workTypeStatusRequest);
        }


        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.WorkTypeStatusUpdateNotePath)]
        public async Task<bool> Update([FromQuery] string note, [FromRoute] int orderDetailId)
        {
            return await _workTypeStatusService.AddAdditionalNote(orderDetailId, note);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.WorkTypeStatusUpdateExistingPath)]
        public async Task<int> UpdateExisting([FromQuery] int orderDetailId,[FromQuery] string workType)
        {
            return await _workTypeStatusService.Update(orderDetailId,workType);
        }
    }
}
