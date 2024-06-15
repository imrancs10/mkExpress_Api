using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly IMasterDataRepository _masterDataRepository;
        private readonly IMasterDataTypeRepository _masterDataTypeRepository;
        private readonly IMapper _mapper;
        public MasterDataService(IMasterDataRepository masterDataRepository, IMapper mapper, IMasterDataTypeRepository masterDataTypeRepository)
        {
            _masterDataRepository = masterDataRepository;
            _masterDataTypeRepository = masterDataTypeRepository;
            _mapper = mapper;
        }
        public async Task<MasterDataResponse> Add(MasterDataRequest masterDataRequest)
        {
            MasterData masterData = _mapper.Map<MasterData>(masterDataRequest);
            return _mapper.Map<MasterDataResponse>(await _masterDataRepository.Add(masterData));
        }

        public async Task<int> Delete(Guid masterDataId)
        {
            return await _masterDataRepository.Delete(masterDataId);
        }

        public List<string> ShipmentStatusList()
        {
            return new List<string>(Enum.GetNames(typeof(ShipmentStatusEnum)));
        }

        public async Task<MasterDataResponse> Get(Guid masterDataId)
        {
            PagingResponse<MasterDataType> masterDataType = await _masterDataTypeRepository.GetAll(new PagingRequest() { PageNo = 1, PageSize = 10000 });
            Dictionary<string, string> masterDataTypeDictionary = masterDataType.Data.Distinct().ToDictionary(x => x.Code.ToLower(), x => x.Value);
            MasterDataResponse masterDataResponse = _mapper.Map<MasterDataResponse>(await _masterDataRepository.Get(masterDataId));
            masterDataResponse.MasterDataTypeValue = masterDataTypeDictionary[masterDataResponse.MasterDataTypeCode.ToLower()];
            return masterDataResponse;
        }

        public async Task<PagingResponse<MasterDataResponse>> GetAll(PagingRequest pagingRequest)
        {
            PagingResponse<MasterDataType> masterDataType = await _masterDataTypeRepository.GetAll(new PagingRequest() { PageNo = 1, PageSize = 10000 });
            Dictionary<string, string> masterDataTypeDictionary = masterDataType.Data.Distinct().ToDictionary(x => x.Code.ToLower(), x => x.Value);
            PagingResponse<MasterDataResponse> masterDataResponse = _mapper.Map<PagingResponse<MasterDataResponse>>(await _masterDataRepository.GetAll(pagingRequest));
            foreach (var item in masterDataResponse.Data)
            {
                item.MasterDataTypeValue = masterDataTypeDictionary[item.MasterDataTypeCode.ToLower()];
            }
            return masterDataResponse;
        }

        public async Task<List<MasterDataResponse>> GetByMasterDataType(string masterData)
        {
            PagingResponse<MasterDataType> masterDataType = await _masterDataTypeRepository.GetAll(new PagingRequest() { PageNo = 1, PageSize = 10000 });
            Dictionary<string, string> masterDataTypeDictionary = masterDataType.Data.Distinct().ToDictionary(x => x.Code.ToLower(), x => x.Value);
            List<MasterDataResponse> masterDataResponse = _mapper.Map<List<MasterDataResponse>>(await _masterDataRepository.GetByMasterDataType(masterData));
            foreach (var item in masterDataResponse)
            {
                item.MasterDataTypeValue = masterDataTypeDictionary[item.MasterDataTypeCode.ToLower()];
            }
            return masterDataResponse;
        }

        public async Task<List<MasterDataResponse>> GetByMasterDataTypes(List<string> masterData)
        {
            PagingResponse<MasterDataType> masterDataType = await _masterDataTypeRepository.GetAll(new PagingRequest() { PageNo = 1, PageSize = 10000 });
            Dictionary<string, string> masterDataTypeDictionary = masterDataType.Data.Distinct().ToDictionary(x => x.Code.ToLower(), x => x.Value);
            List<MasterDataResponse> masterDataResponse = _mapper.Map<List<MasterDataResponse>>(await _masterDataRepository.GetByMasterDataTypes(masterData));
            foreach (var item in masterDataResponse)
            {
                item.MasterDataTypeValue = masterDataTypeDictionary[item.MasterDataTypeCode.ToLower()];
            }
            return masterDataResponse;
        }

        public async Task<PagingResponse<MasterDataResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            PagingResponse<MasterDataType> masterDataType = await _masterDataTypeRepository.GetAll(new PagingRequest() { PageNo = 1, PageSize = 10000 });
            Dictionary<string, string> masterDataTypeDictionary = masterDataType.Data.Distinct().ToDictionary(x => x.Code.ToLower(), x => x.Value);
            PagingResponse<MasterDataResponse> masterDataResponse = _mapper.Map<PagingResponse<MasterDataResponse>>(await _masterDataRepository.Search(searchPagingRequest));
            foreach (var item in masterDataResponse.Data)
            {
                item.MasterDataTypeValue = masterDataTypeDictionary[item.MasterDataTypeCode.ToLower()];
            }
            return masterDataResponse;
        }

        public async Task<MasterDataResponse> Update(MasterDataRequest masterDataRequest)
        {
            MasterData masterData = _mapper.Map<MasterData>(masterDataRequest);
            return _mapper.Map<MasterDataResponse>(await _masterDataRepository.Update(masterData));
        }
    }
}
