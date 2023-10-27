using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Middleware;
using MKExpress.API.Services.Interfaces;
using MKExpress.API.Services.IServices;

namespace MKExpress.Web.API.Controllers
{
    
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;
        private readonly IMasterDataTypeService _masterDataTypeService;
        private readonly IMasterJourneyService _masterJourneyService;
        public MasterDataController(IMasterDataService masterDataService, IMasterDataTypeService masterDataTypeService, IMasterJourneyService masterJourneyService)
        {
            _masterDataService = masterDataService;
            _masterDataTypeService = masterDataTypeService;
            _masterJourneyService = masterJourneyService;
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterDataPath)]
        public async Task<MasterDataResponse> AddMasterData([FromBody] MasterDataRequest masterDataRequest)
        {
            return await _masterDataService.Add(masterDataRequest);
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterDataPath)]
        public async Task<MasterDataResponse> UpdateMasterData([FromBody] MasterDataRequest masterDataRequest)
        {
            return await _masterDataService.Update(masterDataRequest);
        }

        [Authorize("Admin")]
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

        [Authorize("Admin")]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterDataTypePath)]
        public async Task<MasterDataTypeResponse> AddMasterDataType([FromBody] MasterDataTypeRequest masterDataTypeRequest)
        {
            return await _masterDataTypeService.Add(masterDataTypeRequest);
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterDataTypePath)]
        public async Task<MasterDataTypeResponse> UpdateMasterDataType([FromBody] MasterDataTypeRequest masterDataTypeRequest)
        {
            return await _masterDataTypeService.Update(masterDataTypeRequest);
        }

        [Authorize("Admin")]
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

        [Authorize("Admin")]
        [ProducesResponseType(typeof(MasterJourneyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterJourneyPath)]
        public async Task<MasterJourneyResponse> AddJourney([FromBody] MasterJourneyRequest request)
        {
            return await _masterJourneyService.Add(request);
        }

        [Authorize("Admin")]
        [ProducesResponseType(typeof(MasterJourneyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterJourneyPath)]
        public async Task<MasterJourneyResponse> Update([FromBody] MasterJourneyRequest request)
        {
            return await _masterJourneyService.Update(request);
        }

        [Authorize("Admin")]
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
    }
}
