using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
