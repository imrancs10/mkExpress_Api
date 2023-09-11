using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterDataService _masterDataService;
        private readonly IMasterDataTypeService _masterDataTypeService;
        private readonly IKandooraExpenseService _kandooraExpenseService;
        public MasterDataController(IMasterDataService masterDataService, IMasterDataTypeService masterDataTypeService, IKandooraExpenseService kandooraExpenseService)
        {
            _masterDataService = masterDataService;
            _masterDataTypeService = masterDataTypeService;
            _kandooraExpenseService = kandooraExpenseService;
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
        public async Task<int> DeleteMasterData([FromRoute(Name = "id")] int masterDataId)
        {
            return await _masterDataService.Delete(masterDataId);
        }

        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataByIdPath)]
        public async Task<MasterDataResponse> GetMasterData([FromRoute] int id)
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
        public async Task<int> DeleteMasterTypeData([FromRoute(Name = "id")] int masterDataTypeId)
        {
            return await _masterDataTypeService.Delete(masterDataTypeId);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataTypeByIdPath)]
        public async Task<MasterDataTypeResponse> GetMasterDataType([FromRoute] int id)
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

        [ProducesResponseType(typeof(EachKandooraExpenseHeadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterDataKandooraExpenseHeadPath)]
        public async Task<EachKandooraExpenseHeadResponse> AddExpenseHead([FromBody] EachKandooraExpenseHeadRequest eachKandooraExpenseHead)
        {
            return await _kandooraExpenseService.AddExpenseHead(eachKandooraExpenseHead);
        }

        [ProducesResponseType(typeof(List<EachKandooraExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterDataKandooraExpensePath)]
        public async Task<List<EachKandooraExpenseResponse>> AddExpense([FromBody] List<EachKandooraExpenseRequest> eachKandooraExpense)
        {
            return await _kandooraExpenseService.AddExpense(eachKandooraExpense);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterDataKandooraExpenseHeadDeletePath)]
        public async Task<int> DeleteExpenseHead([FromRoute] int id)
        {
            return await _kandooraExpenseService.DeleteExpenseHead(id);
        }

        [ProducesResponseType(typeof(EachKandooraExpenseHeadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataKandooraExpenseHeadByIdPath)]
        public async Task<EachKandooraExpenseHeadResponse> GetExpenseHead([FromRoute] int id)
        {
            return await _kandooraExpenseService.GetExpenseHead(id);
        }

        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataKandooraExpenseGetSumPath)]
        public async Task<decimal> KandooraExpenseGetSumPath()
        {
            return await _kandooraExpenseService.GetTotalOfExpense();
        }

        [ProducesResponseType(typeof(PagingResponse<EachKandooraExpenseHeadResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataKandooraExpenseHeadPath)]
        public async Task<PagingResponse<EachKandooraExpenseHeadResponse>> GetAllExpenseHead([FromQuery] PagingRequest pagingRequest)
        {
            return await _kandooraExpenseService.GetAllExpenseHead(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<EachKandooraExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataKandooraExpensePath)]
        public async Task<PagingResponse<EachKandooraExpenseResponse>> GetAllExpense([FromQuery] PagingRequest pagingRequest)
        {
            return await _kandooraExpenseService.GetAllExpense(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<EachKandooraExpenseHeadResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterDataKandooraExpenseHeadSearchPath)]
        public async Task<PagingResponse<EachKandooraExpenseHeadResponse>> SearchExpenseHead([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _kandooraExpenseService.SearchExpenseHead(searchPagingRequest);
        }

        [ProducesResponseType(typeof(EachKandooraExpenseHeadResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterDataKandooraExpenseHeadPath)]
        public async Task<EachKandooraExpenseHeadResponse> UpdateExpenseHead([FromBody] EachKandooraExpenseHeadRequest eachKandooraExpenseHead)
        {
            return await _kandooraExpenseService.UpdateExpenseHead(eachKandooraExpenseHead);
        }
    }
}
