﻿using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Services.IServices
{
    public interface IContainerService
    {
        Task<ContainerResponse> AddContainer(ContainerRequest container);
        Task<ContainerResponse> GetContainer(Guid id);
        Task<PagingResponse<ContainerResponse>> GetAllContainer(PagingRequest pagingRequest);
        Task<List<ContainerJourneyResponse>> GetContainerJourney(int containerNo);
        Task<bool> CheckInContainer(Guid containerId, Guid containerJourneyId);
        Task<bool> CheckOutContainer(Guid containerId, Guid containerJourneyId);
    }
}
