using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Services
{
    public class MasterJourneyService : IMasterJourneyService
    {
        private readonly IMapper _mapper;
        private readonly IMasterJourneyRepository _repository;

        public MasterJourneyService(IMasterJourneyRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<MasterJourneyResponse> Add(MasterJourneyRequest request)
        {
            MasterJourney masterJourney=_mapper.Map<MasterJourney>(request);
            return _mapper.Map<MasterJourneyResponse>(await _repository.Add(masterJourney));
        }

        public async Task<int> Delete(Guid id)
        {
            return await _repository.Delete(id);    
        }

        public async Task<MasterJourneyResponse> Get(Guid id)
        {
            return _mapper.Map<MasterJourneyResponse>(await _repository.Get(id));
        }

        public async Task<PagingResponse<MasterJourneyResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterJourneyResponse>>(await _repository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<MasterJourneyResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterJourneyResponse>>(await _repository.Search(searchPagingRequest));
        }

        public async Task<MasterJourneyResponse> Update(MasterJourneyRequest request)
        {
            MasterJourney masterJourney = _mapper.Map<MasterJourney>(request);
            return _mapper.Map<MasterJourneyResponse>(await _repository.Update(masterJourney));
        }
    }
}
