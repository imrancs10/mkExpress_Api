using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;

namespace MKExpress.Web.API.Controllers
{

    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;
        private readonly IMasterDataTypeService _masterDataTypeService;
        private readonly IMasterJourneyService _masterJourneyService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMenuService _menuService;
        private readonly IUserRoleMenuMapperService _userRoleMenuMapper;
        public MasterDataController(IMasterDataService masterDataService, 
            IMasterDataTypeService masterDataTypeService, 
            IMasterJourneyService masterJourneyService,
            IUserRoleService userRoleService,
            IUserRoleMenuMapperService userRoleMenuMapper,
            IMenuService menuService)
        {
            _masterDataService = masterDataService;
            _masterDataTypeService = masterDataTypeService;
            _masterJourneyService = masterJourneyService;
            _userRoleService = userRoleService;
            _menuService = menuService;
            _userRoleMenuMapper = userRoleMenuMapper;
        }
        
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterDataPath)]
        public async Task<MasterDataResponse> AddMasterData([FromBody] MasterDataRequest masterDataRequest)
        {
            return await _masterDataService.Add(masterDataRequest);
        }

        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterDataPath)]
        public async Task<MasterDataResponse> UpdateMasterData([FromBody] MasterDataRequest masterDataRequest)
        {
            return await _masterDataService.Update(masterDataRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterDataDeletePath)]
        public async Task<int> DeleteMasterData([FromRoute(Name = "id")] Guid masterDataId)
        {
            return await _masterDataService.Delete(masterDataId);
        }

        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataByIdPath)]
        public async Task<MasterDataResponse> GetMasterData([FromRoute] Guid id)
        {
            return await _masterDataService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataPath)]
        public async Task<PagingResponse<MasterDataResponse>> GetAllMasterData([FromQuery] PagingRequest pagingRequest)
        {
            return await _masterDataService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataSearchPath)]
        public async Task<PagingResponse<MasterDataResponse>> SearchMasterData([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _masterDataService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(List<MasterDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<MasterDataResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataGetByTypePath)]
        public async Task<List<MasterDataResponse>> GetByTypeMasterData([FromQuery] string masterDataType)
        {
            return await _masterDataService.GetByMasterDataType(masterDataType);
        }

        [ProducesResponseType(typeof(List<MasterDataResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<MasterDataResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataGetByTypesPath)]
        public async Task<List<MasterDataResponse>> GetByTypesMasterData([FromQuery] List<string> masterDataTypes)
        {
            return await _masterDataService.GetByMasterDataTypes(masterDataTypes);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterDataTypePath)]
        public async Task<MasterDataTypeResponse> AddMasterDataType([FromBody] MasterDataTypeRequest masterDataTypeRequest)
        {
            return await _masterDataTypeService.Add(masterDataTypeRequest);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterDataTypePath)]
        public async Task<MasterDataTypeResponse> UpdateMasterDataType([FromBody] MasterDataTypeRequest masterDataTypeRequest)
        {
            return await _masterDataTypeService.Update(masterDataTypeRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterDataTypeDeletePath)]
        public async Task<int> DeleteMasterTypeData([FromRoute(Name = "id")] Guid masterDataTypeId)
        {
            return await _masterDataTypeService.Delete(masterDataTypeId);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataTypeByIdPath)]
        public async Task<MasterDataTypeResponse> GetMasterDataType([FromRoute] Guid id)
        {
            return await _masterDataTypeService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataTypePath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> GetAllMasterDataType([FromQuery] PagingRequest pagingRequest)
        {
            return await _masterDataTypeService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataTypeSearchPath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> SearchMasterDataType([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _masterDataTypeService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(MasterJourneyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterJourneyPath)]
        public async Task<MasterJourneyResponse> AddJourney([FromBody] MasterJourneyRequest request)
        {
            return await _masterJourneyService.Add(request);
        }

        [ProducesResponseType(typeof(MasterJourneyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterJourneyPath)]
        public async Task<MasterJourneyResponse> Update([FromBody] MasterJourneyRequest request)
        {
            return await _masterJourneyService.Update(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterJourneyDeletePath)]
        public async Task<int> Delete([FromRoute] Guid id)
        {
            return await _masterJourneyService.Delete(id);
        }

        [ProducesResponseType(typeof(MasterJourneyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterJourneyByIdPath)]
        public async Task<MasterJourneyResponse> Get([FromRoute] Guid id)
        {
            return await _masterJourneyService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterJourneyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterJourneyPath)]
        public async Task<PagingResponse<MasterJourneyResponse>> GetAll([FromQuery]PagingRequest pagingRequest)
        {
           return await _masterJourneyService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterJourneyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterJourneySearchPath)]
        public async Task<PagingResponse<MasterJourneyResponse>> Search([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _masterJourneyService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(List<DropdownResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterJourneyDropdownPath)]
        public async Task<List<DropdownResponse>> GetJourneyList()
        {
            return await _masterJourneyService.GetJourneyList();
        }

        [ProducesResponseType(typeof(UserRoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterRolePath)]
        public async Task<UserRoleResponse> AddRoleAsync([FromBody] UserRoleRequest role)
        {
            return await _userRoleService.AddRoleAsync(role);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterRoleDeletePath)]
        public async Task<bool> DeleteRoleAsync([FromRoute] Guid id)
        {
           return await _userRoleService.DeleteRoleAsync(id);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterRolePath)]
        public async Task<bool> UpdateRoleAsync([FromBody] UserRoleRequest role)
        {
            return await _userRoleService.UpdateRoleAsync(role);
        }

        [ProducesResponseType(typeof(UserRoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterRoleByIdPath)]
        public async Task<UserRoleResponse> GetRoleByIdAsync([FromRoute] Guid id)
        {
           return await _userRoleService.GetRoleByIdAsync(id);
        }

        [ProducesResponseType(typeof(PagingResponse<UserRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterRolePath)]
        public async Task<PagingResponse<UserRoleResponse>> GetAllRolesAsync([FromQuery]PagingRequest request)
        {
            return await _userRoleService.GetAllRolesAsync(request);
        }

        [ProducesResponseType(typeof(PagingResponse<UserRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterRoleSearchPath)]
        public async Task<PagingResponse<UserRoleResponse>> SearchRolesAsync([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _userRoleService.SearchRolesAsync(pagingRequest);
        }

        [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterMenuPath)]
        public async Task<MenuResponse> AddMenuAsync([FromBody] MenuRequest menu)
        {
            return await _menuService.AddMenuAsync(menu);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterMenuDeletePath)]
        public async Task<bool> DeleteMenuAsync([FromRoute] Guid id)
        {
            return await _menuService.DeleteMenuAsync(id);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterMenuPath)]
        public async Task<bool> UpdateMenuAsync([FromBody] MenuRequest menu)
        {
           return await _menuService.UpdateMenuAsync(menu);
        }

        [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterMenuByIdPath)]
        public async Task<MenuResponse> GetMenuByIdAsync([FromRoute] Guid id)
        {
            return await _menuService.GetMenuByIdAsync(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MenuResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterMenuPath)]
        public async Task<PagingResponse<MenuResponse>> GetAllMenusAsync([FromQuery] PagingRequest request)
        {
            return await _menuService.GetAllMenusAsync(request);
        }

        [ProducesResponseType(typeof(PagingResponse<MenuResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterMenuSearchPath)]
        public async Task<PagingResponse<MenuResponse>> SearchMenusAsync([FromQuery] SearchPagingRequest pagingRequest)
        {
           return await _menuService.SearchMenusAsync(pagingRequest);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterRoleMenuMapperPath)]
        public async Task<bool> AddRoleMenuMapper([FromBody] List<UserRoleMenuMapperRequest> req)
        {
            return await _userRoleMenuMapper.Add(req);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterRoleMenuMapperDeleteByRolePath)]
        public async Task<bool> DeleteRoleMenuMapperByRoleId([FromRoute] Guid roleId)
        {
            return await _userRoleMenuMapper.DeleteByRoleId(roleId);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterRoleMenuMapperDeleteByIdPath)]
        public async Task<bool> DeleteRoleMenuMapperById([FromRoute] Guid id)
        {
            return await _userRoleMenuMapper.DeleteById(id);
        }

        [ProducesResponseType(typeof(List<UserRoleMenuMapperResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterRoleMenuMapperByIdPath)]
        public async Task<List<UserRoleMenuMapperResponse>> GetByRoleId([FromRoute] Guid id)
        {
            return await _userRoleMenuMapper.GetByRoleId(id);
        }

        [ProducesResponseType(typeof(List<UserRoleMenuMapperResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterRoleMenuMapperGetAllPath)]
        public async Task<List<UserRoleMenuMapperResponse>> GetAll()
        {
            return await _userRoleMenuMapper.GetAll();
        }

        [ProducesResponseType(typeof(List<UserRoleMenuMapperResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterRoleMenuMapperSearchPath)]
        public async Task<List<UserRoleMenuMapperResponse>> SearchRolesAsync([FromQuery] string searchTerm)
        {
            return await _userRoleMenuMapper.SearchRolesAsync(searchTerm);
        }
    }
}
