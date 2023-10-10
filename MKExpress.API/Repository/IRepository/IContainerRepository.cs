using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IContainerRepository
    {
        Task<Container> AddContainer(Container container);
        Task<ContainerJourney> GetContainerJourney(Guid containerId);
        Task<ContainerJourney> CheckInContainer(Guid containerId,Guid containerJourneyId);
        Task<ContainerJourney> CheckOutContainer(Guid containerId, Guid containerJourneyId);
        Task<Container> GetContainer(Guid id);
        Task<PagingResponse<Container>> GetAllContainer(PagingRequest pagingRequest);
    }
}
