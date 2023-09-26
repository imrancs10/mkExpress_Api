using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class LogisticRegionSerivce : ILogisticRegionSerivce
    {
        private readonly ILogisticRegionRepository _repository;
        private readonly IMapper _mapper;
        public LogisticRegionSerivce(ILogisticRegionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<LogisticRegionResponse> Add(LogisticRegionRequest request)
        {
            LogisticRegion logisticRegion = _mapper.Map<LogisticRegion>(request);
            return _mapper.Map<LogisticRegionResponse>(await _repository.Add(logisticRegion));
        }

        public async Task<int> Delete(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<LogisticRegionResponse> Get(Guid id)
        {
            return _mapper.Map<LogisticRegionResponse>(await _repository.Get(id));
        }

        public async Task<PagingResponse<LogisticRegionResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<LogisticRegionResponse>>(await _repository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<LogisticRegionResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<LogisticRegionResponse>>(await _repository.Search(searchPagingRequest));
        }

        public async Task<LogisticRegionResponse> Update(LogisticRegionRequest request)
        {
            LogisticRegion logisticRegion = _mapper.Map<LogisticRegion>(request);
            return _mapper.Map<LogisticRegionResponse>(await _repository.Update(logisticRegion));
        }
    }
}
