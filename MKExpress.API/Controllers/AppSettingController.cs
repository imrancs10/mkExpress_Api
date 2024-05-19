using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class AppSettingController : ControllerBase
    {
        private readonly IAppSettingService _settingService;
        public AppSettingController(IAppSettingService settingService)
        {
            _settingService = settingService;
        }

        [ProducesResponseType(typeof(AppSettingResponse), StatusCodes.Status200OK)]
        [HttpPut(StaticValues.AppSettingPath)]
        public async Task<AppSettingResponse> Add([FromBody] AppSettingRequest request)
        {
            return await _settingService.Add(request);
        }

        [ProducesResponseType(typeof(PagingResponse<AppSettingResponse>), StatusCodes.Status200OK)]
        [HttpGet(StaticValues.AppSettingPath)]
        public async Task<PagingResponse<AppSettingResponse>> GetAll([FromQuery] PagingRequest request)
        {
            return await _settingService.GetAll(request);
        }
        
        [ProducesResponseType(typeof(List<AppSettingGroupResponse>), StatusCodes.Status200OK)]
        [HttpGet(StaticValues.AppSettingGroupPath)]
        public async Task<List<AppSettingGroupResponse>> GetAllAppSettingGroup()
        {
            return await _settingService.GetAllAppSettingGroup();
        }

        [ProducesResponseType(typeof(AppSettingResponse), StatusCodes.Status200OK)]
        [HttpPost(StaticValues.AppSettingPath)]
        public async Task<AppSettingResponse> Update([FromBody] AppSettingRequest request)
        {
            return await _settingService.Update(request);
        }

        [ProducesResponseType(typeof(AppSettingResponse), StatusCodes.Status200OK)]
        [HttpGet(StaticValues.AppSettingByIdPath)]
        public async Task<AppSettingResponse> Get([FromRoute] Guid id)
        {
            return await _settingService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<AppSettingResponse>), StatusCodes.Status200OK)]
        [HttpGet(StaticValues.AppSettingSearchPath)]
        public async Task<PagingResponse<AppSettingResponse>> Search([FromQuery] SearchPagingRequest request)
        {
            return await _settingService.Search(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HttpDelete(StaticValues.AppSettingDeletePath)]
        public async Task<int> Delete([FromRoute] Guid id)
        {
            return await _settingService.Delete(id);
        }
    }
}
