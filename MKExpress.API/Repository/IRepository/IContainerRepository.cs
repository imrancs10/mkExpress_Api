using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IContainerRepository
    {
        Task<Container> AddContainer(Container container);
        Task<List<ContainerJourney>> GetContainerJourney(int containerNo);
        Task<bool> CheckInContainer(Guid containerId, Guid containerJourneyId);
        Task<bool> CheckOutContainer(Guid containerId, Guid containerJourneyId);
        Task<bool> CloseContainer(Guid containerId);
        Task<ShipmentValidateResponse> ValidateAndAddShipmentInContainer(Guid containerId, string shipmentNo);
        Task<bool> RemoveShipmentFromContainer(Guid containerId, string shipmentNo);
        Task<Container> GetContainer(Guid id);
        Task<PagingResponse<Container>> GetAllContainer(PagingRequest pagingRequest);
        Task<PagingResponse<Container>> SearchContainer(SearchPagingRequest pagingRequest);
    }
}
