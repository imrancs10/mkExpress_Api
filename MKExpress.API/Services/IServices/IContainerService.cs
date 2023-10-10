using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services.IServices
{
    public interface IContainerService
    {
        Task<ContainerResponse> AddContainer(ContainerRequest container);
        Task<ContainerResponse> GetContainer(Guid id);
        Task<PagingResponse<ContainerResponse>> GetAllContainer(PagingRequest pagingRequest);
    }
}
