using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IContainerService
    {
        Task<ContainerResponse> AddContainer(ContainerRequest container);
        Task<ContainerResponse> GetContainer(Guid id);
        Task<PagingResponse<ContainerResponse>> GetAllContainer(PagingRequest pagingRequest);
        Task<ContainerResponse> GetContainerJourney(int containerNo); 
        Task<bool> DeleteContainer(Guid containerId, string deleteReason);
        Task<bool> CheckInContainer(Guid containerId, Guid containerJourneyId);
        Task<bool> CheckOutContainer(Guid containerId, Guid containerJourneyId);
        Task<bool> CloseContainer(Guid containerId);
        Task<ShipmentValidateResponse> ValidateAndAddShipmentInContainer(Guid containerId, string shipmentNo);
        Task<bool> RemoveShipmentFromContainer(Guid containerId, string shipmentNo);
        Task<PagingResponse<ContainerResponse>> SearchContainer(SearchPagingRequest pagingRequest);
    }
}
