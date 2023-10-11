using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Logger;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

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

        public async Task<PagingResponse<ContainerResponse>> GetAllContainer(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<ContainerResponse>>(await _repository.GetAllContainer(pagingRequest));
        }

        public async Task<ContainerResponse> GetContainer(Guid id)
        {
            return _mapper.Map<ContainerResponse>(await _repository.GetContainer(id));
        }

        public async Task<List<ContainerJourneyResponse>> GetContainerJourney(int containerNo)
        {
           return _mapper.Map<List<ContainerJourneyResponse>>(await _repository.GetContainerJourney(containerNo));
        }
    }
}
