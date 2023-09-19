using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class MasterDataTypeService : IMasterDataTypeService
    {
        private readonly IMasterDataTypeRepository _masterDataTypeRepository;
        private readonly IMapper _mapper;
        public MasterDataTypeService(IMasterDataTypeRepository masterDataTypeRepository, IMapper mapper)
        {
            _masterDataTypeRepository = masterDataTypeRepository;
            _mapper = mapper;
        }
        public async Task<MasterDataTypeResponse> Add(MasterDataTypeRequest request)
        {
            if (await _masterDataTypeRepository.IsMasterDataTypeExist(request.Code))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterDataTypeAlreadyExistError, StaticValues.MasterDataTypeAlreadyExistMessage);
            }
            MasterDataType masterData = _mapper.Map<MasterDataType>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _masterDataTypeRepository.Add(masterData));
        }

        public async Task<int> Delete(int id)
        {
            return await _masterDataTypeRepository.Delete(id);
        }

        public async Task<MasterDataTypeResponse> Get(int id)
        {
            return _mapper.Map<MasterDataTypeResponse>(await _masterDataTypeRepository.Get(id));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _masterDataTypeRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<MasterDataTypeResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<MasterDataTypeResponse>>(await _masterDataTypeRepository.Search(searchPagingRequest));
        }

        public async Task<MasterDataTypeResponse> Update(MasterDataTypeRequest request)
        {
            if (await _masterDataTypeRepository.IsMasterDataTypeExist(request.Code))
            {
                throw new BusinessRuleViolationException(StaticValues.MasterDataTypeAlreadyExistError, StaticValues.MasterDataTypeAlreadyExistMessage);
            }
            MasterDataType masterData = _mapper.Map<MasterDataType>(request);
            return _mapper.Map<MasterDataTypeResponse>(await _masterDataTypeRepository.Update(masterData));
        }
    }
}
