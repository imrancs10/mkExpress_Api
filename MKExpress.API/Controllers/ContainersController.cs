using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class ContainersController : ControllerBase
    {
        private readonly IContainerService _containerService;
        public ContainersController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        [ProducesResponseType(typeof(ContainerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ContainerPath)]
        public async Task<ContainerResponse> AddContainer([FromBody]ContainerRequest container)
        {
            return await _containerService.AddContainer(container);
        }

        [ProducesResponseType(typeof(ContainerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ContainerPath)]
        public async Task<PagingResponse<ContainerResponse>> GetAllContainer([FromQuery] PagingRequest pagingRequest)
        {
          return await _containerService.GetAllContainer(pagingRequest);
        }

        [ProducesResponseType(typeof(ContainerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ContainerGetByIdPath)]
        public async Task<ContainerResponse> GetContainer([FromRoute]Guid id)
        {
           return await _containerService.GetContainer(id);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ContainerCheckOutPath)]
        public async Task<bool> CheckOutContainer([FromRoute] Guid containerId, [FromRoute] Guid containerJourneyId)
        {
            return await _containerService.CheckOutContainer(containerId,containerJourneyId);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ContainerCheckInPath)]
        public async Task<bool> CheckInContainer([FromRoute] Guid containerId, [FromRoute] Guid containerJourneyId)
        {
            return await _containerService.CheckInContainer(containerId, containerJourneyId);
        }

        [ProducesResponseType(typeof(ContainerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ContainerGetJourneyPath)]
        public async Task<ContainerResponse> CheckInContainer([FromRoute] int containerNo)
        {
            return await _containerService.GetContainerJourney(containerNo);
        }

        [ProducesResponseType(typeof(PagingResponse<ContainerResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ContainerSearchPath)]
        public async Task<PagingResponse<ContainerResponse>> SearchContainer([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _containerService.SearchContainer(searchPagingRequest);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ContainerDeletePath)]
        public async Task<bool> DeleteContainer([FromRoute] Guid id, [FromQuery] string note)
        {
            return await _containerService.DeleteContainer(id,note);
        }

        [ProducesResponseType(typeof(ShipmentValidateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ContainerAddShipmentPath)]
        public async Task<ShipmentValidateResponse> ValidateAndAddShipmentInContainer([FromRoute] Guid containerId, [FromRoute] string shipmentNo)
        {
            return await _containerService.ValidateAndAddShipmentInContainer(containerId,shipmentNo);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ContainerRemoveShipmentPath)]
        public async Task<bool> RemoveShipmentInContainer([FromRoute] Guid containerId, [FromRoute] string shipmentNo)
        {
            return await _containerService.RemoveShipmentFromContainer(containerId, shipmentNo);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ContainerClosePath)]
        public async Task<bool> CloseContainer([FromRoute] Guid containerId)
        {
            return await _containerService.CloseContainer(containerId);
        }
    }
}
