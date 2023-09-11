using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class MasterHolidayTypeService : IMasterHolidayTypeService
    {
        private readonly IMasterHolidayTypeRepository _masterHolidayTypeRepository;
        private readonly IMapper _mapper;
        public MasterHolidayTypeService(IMasterHolidayTypeRepository masterHolidayTypeRepository, IMapper mapper)
        {
            _masterHolidayTypeRepository = masterHolidayTypeRepository;
            _mapper = mapper;
        }
        public async Task<MasterDataTypeResponse> Add(MasterDataTypeRequest request)
        {
            MasterHolidayType masterHolidayType = _mapper.Map<MasterHolidayType>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _masterHolidayTypeRepository.Add(masterHolidayType));
        }

        public async Task<int> Delete(int id)
        {
            return await _masterHolidayTypeRepository.Delete(id);
        }

        public async Task<MasterDataTypeResponse> Get(int id)
        {
            return _mapper.Map<MasterDataTypeResponse>(await _masterHolidayTypeRepository.Get(id));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _masterHolidayTypeRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _masterHolidayTypeRepository.Search(searchPagingRequest));
        }

        public async Task<MasterDataTypeResponse> Update(MasterDataTypeRequest request)
        {
            MasterHolidayType masterHolidayType = _mapper.Map<MasterHolidayType>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _masterHolidayTypeRepository.Update(masterHolidayType));
        }
    }
}
