using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class MasterCrystalService : IMasterCrystalService
    {
        private readonly IMasterCrystalRepository _crystalRepository;
        private readonly IMapper _mapper;
        public MasterCrystalService(IMasterCrystalRepository crystalRepository, IMapper mapper)
        {
            _crystalRepository = crystalRepository;
            _mapper = mapper;
        }
        public async Task<MasterCrystalResponse> Add(MasterCrystalRequest request)
        {
            if (request == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            var crystal = _mapper.Map<MasterCrystal>(request);
            return _mapper.Map<MasterCrystalResponse>(await _crystalRepository.Add(crystal));
        }

        public async Task<int> Delete(int id)
        {
            return await _crystalRepository.Delete(id);
        }

        public async Task<MasterCrystalResponse> Get(int id)
        {
            return _mapper.Map<MasterCrystalResponse>(await _crystalRepository.Get(id));
        }

        public async Task<PagingResponse<MasterCrystalResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterCrystalResponse>>(await _crystalRepository.GetAll(pagingRequest));
        }

        public async Task<int> GetNextCrystalNo()
        {
            return await _crystalRepository.GetNextCrystalNo();
        }

        public async Task<PagingResponse<MasterCrystalResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterCrystalResponse>>(await _crystalRepository.Search(searchPagingRequest));
        }

        public async Task<MasterCrystalResponse> Update(MasterCrystalRequest request)
        {
            if (request == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            var crystal = _mapper.Map<MasterCrystal>(request);
            return _mapper.Map<MasterCrystalResponse>(await _crystalRepository.Update(crystal));
        }
    }
}
