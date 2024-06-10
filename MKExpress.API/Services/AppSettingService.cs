using AutoMapper;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IAppSettingRepository _appSettingRepository;
        private readonly IMapper _mapper;
        public AppSettingService(IAppSettingRepository appSettingRepository,IMapper mapper)
        {
            _appSettingRepository = appSettingRepository;
            _mapper = mapper;
        }
        public async Task<AppSettingResponse> Add(AppSettingRequest request)
        {
            var appSetting=_mapper.Map<AppSetting>(request);
            return _mapper.Map<AppSettingResponse>(await _appSettingRepository.Add(appSetting));
        }

        public async Task<int> Delete(Guid id)
        {
            return await _appSettingRepository.Delete(id);
        }

        public async Task<AppSettingResponse> Get(Guid id)
        {
            return _mapper.Map<AppSettingResponse>(await _appSettingRepository.Get(id));
        }

        public async Task<PagingResponse<AppSettingResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<AppSettingResponse>>(await _appSettingRepository.GetAll(pagingRequest));
        }

        public async Task<List<AppSettingGroupResponse>> GetAllAppSettingGroup()
        {
            return _mapper.Map<List<AppSettingGroupResponse>>(await _appSettingRepository.GetAllAppSettingGroup());
        }

        public async Task<PagingResponse<AppSettingResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<AppSettingResponse>>(await _appSettingRepository.Search(searchPagingRequest));
        }

        public async Task<AppSettingResponse> Update(AppSettingRequest request)
        {
            var appSetting = _mapper.Map<AppSetting>(request);
            return _mapper.Map<AppSettingResponse>(await _appSettingRepository.Update(appSetting));
        }
    }
}
