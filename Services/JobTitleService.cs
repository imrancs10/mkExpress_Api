using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _jobTitleRepository;
        private readonly IMapper _mapper;
        public JobTitleService(IJobTitleRepository jobTitleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _jobTitleRepository = jobTitleRepository;
        }
        public async Task<JobTitleResponse> Add(JobTitleRequest request)
        {
            var masterJobTitle = _mapper.Map<MasterJobTitle>(request);
            return _mapper.Map<JobTitleResponse>(await _jobTitleRepository.Add(masterJobTitle));
        }

        public async Task<JobTitleResponse> Update(JobTitleRequest request)
        {
            var masterJobTitle = _mapper.Map<MasterJobTitle>(request);
            return _mapper.Map<JobTitleResponse>(await _jobTitleRepository.Update(masterJobTitle));
        }

        public async Task<int> Delete(int id)
        {
            return await _jobTitleRepository.Delete(id);
        }

        public async Task<JobTitleResponse> Get(int id)
        {
            return _mapper.Map<JobTitleResponse>(await _jobTitleRepository.Get(id));
        }

        public async Task<PagingResponse<JobTitleResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<JobTitleResponse>>(await _jobTitleRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<JobTitleResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<JobTitleResponse>>(await _jobTitleRepository.Search(searchPagingRequest));
        }
    }
}