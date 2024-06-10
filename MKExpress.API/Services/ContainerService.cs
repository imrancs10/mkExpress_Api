using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IContainerRepository _repository;
        private readonly IMapper _mapper;
        public ContainerService(IContainerRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ContainerResponse> AddContainer(ContainerRequest container)
        {
            var request=_mapper.Map<Container>(container);
            return _mapper.Map<ContainerResponse>(await _repository.AddContainer(request));
        }

        public async Task<bool> CheckInContainer(Guid containerId, Guid containerJourneyId)
        {
            return await _repository.CheckInContainer(containerId, containerJourneyId);
        }

        public async Task<bool> CheckOutContainer(Guid containerId, Guid containerJourneyId)
        {
            return await _repository.CheckOutContainer(containerId, containerJourneyId);
        }

        public async Task<bool> CloseContainer(Guid containerId)
        {
           return await _repository.CloseContainer(containerId);
        }

        public async Task<bool> DeleteContainer(Guid containerId, string deleteReason)
        {
           return await _repository.DeleteContainer(containerId, deleteReason);
        }

        public async Task<PagingResponse<ContainerResponse>> GetAllContainer(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ContainerResponse>>(await _repository.GetAllContainer(pagingRequest));
        }

        public async Task<ContainerResponse> GetContainer(Guid id)
        {
            return _mapper.Map<ContainerResponse>(await _repository.GetContainer(id));
        }

        public async Task<ContainerResponse> GetContainerJourney(int containerNo)
        {
           return _mapper.Map<ContainerResponse>(await _repository.GetContainerJourney(containerNo));
        }

        public async Task<bool> RemoveShipmentFromContainer(Guid containerId, string shipmentNo)
        {
            return await _repository.RemoveShipmentFromContainer(containerId, shipmentNo);
        }

        public async Task<PagingResponse<ContainerResponse>> SearchContainer(SearchPagingRequest pagingRequest)
        {
            var data= _mapper.Map<PagingResponse<ContainerResponse>>(await _repository.SearchContainer(pagingRequest));
            return data;
        }

        public async Task<ShipmentValidateResponse> ValidateAndAddShipmentInContainer(Guid containerId, string shipmentNo)
        {
           return await _repository.ValidateAndAddShipmentInContainer(containerId, shipmentNo);
        }
    }
}
