using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class MasterHolidayNameService : IMasterHolidayNameService
    {
        private readonly IMasterHolidayNameRepository _masterHolidayNameRepository;
        private readonly IMapper _mapper;
        public MasterHolidayNameService(IMasterHolidayNameRepository masterHolidayNameRepository, IMapper mapper)
        {
            _masterHolidayNameRepository = masterHolidayNameRepository;
            _mapper = mapper;
        }
        public async Task<MasterHolidayNameResponse> Add(MasterHolidayNameRequest request)
        {
            MasterHolidayName masterHolidayName = _mapper.Map<MasterHolidayName>(request);
            return _mapper.Map<MasterHolidayNameResponse>(await _masterHolidayNameRepository.Add(masterHolidayName));
        }

        public async Task<int> Delete(int id)
        {
            return await _masterHolidayNameRepository.Delete(id);
        }

        public async Task<MasterHolidayNameResponse> Get(int id)
        {
            return _mapper.Map<MasterHolidayNameResponse>(await _masterHolidayNameRepository.Get(id));
        }

        public async Task<PagingResponse<MasterHolidayNameResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterHolidayNameResponse>>(await _masterHolidayNameRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<MasterHolidayNameResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterHolidayNameResponse>>(await _masterHolidayNameRepository.Search(searchPagingRequest));
        }

        public async Task<MasterHolidayNameResponse> Update(MasterHolidayNameRequest request)
        {
            MasterHolidayName masterHolidayName = _mapper.Map<MasterHolidayName>(request);
            return _mapper.Map<MasterHolidayNameResponse>(await _masterHolidayNameRepository.Update(masterHolidayName));
        }
    }
}
