﻿using Microsoft.AspNetCore.Http;
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

        [ProducesResponseType(typeof(List<ContainerJourneyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ContainerGetJourneyPath)]
        public async Task<List<ContainerJourneyResponse>> CheckInContainer([FromRoute] int id)
        {
            return await _containerService.GetContainerJourney(id);
        }
    }
}
