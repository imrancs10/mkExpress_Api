using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Expense;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Expense;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;
        private readonly IExpenseNameService _expenseNameService;
        private readonly IExpenseTypeService _expenseTypeService;
        private readonly IExpenseShopCompanyService _expenseShopCompanyService;
        public ExpenseController(IExpenseService expenseService, IExpenseNameService expenseNameService, IExpenseTypeService expenseTypeService, IExpenseShopCompanyService expenseShopCompanyService)
        {
            _expenseNameService = expenseNameService;
            _expenseService = expenseService;
            _expenseShopCompanyService = expenseShopCompanyService;
            _expenseTypeService = expenseTypeService;
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ExpenseTypePath)]
        public async Task<MasterDataTypeResponse> ExpenseTypeAdd([FromBody] MasterDataTypeRequest request)
        {
            return await _expenseTypeService.Add(request);
        }

        [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ExpensePath)]
        public async Task<ExpenseResponse> AddExpense([FromBody] ExpenseRequest request)
        {
            return await _expenseService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ExpenseTypeDeletePath)]
        public async Task<int> DeleteTypeExpense([FromRoute] int id)
        {
            return await _expenseTypeService.Delete(id);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ExpenseDeletePath)]
        public async Task<int> DeleteExpense([FromRoute] int id)
        {
            return await _expenseService.Delete(id);
        }

        [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseByIdPath)]
        public async Task<ExpenseResponse> ExpenseGet([FromRoute] int id)
        {
            return await _expenseService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpensePath)]
        public async Task<PagingResponse<ExpenseResponse>> ExpenseGetAll([FromQuery] ExpensePagingRequest pagingRequest)
        {
            return await _expenseService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<ExpenseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseSearchPath)]
        public async Task<PagingResponse<ExpenseResponse>> ExpenseSearch([FromQuery] ExpenseSearchPagingRequest searchPagingRequest)
        {
            return await _expenseService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(ExpenseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ExpensePath)]
        public async Task<ExpenseResponse> ExpenseUpdate([FromBody] ExpenseRequest request)
        {
            return await _expenseService.Update(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseGetNumberPath)]
        public async Task<int> GetExpenseNo()
        {
            return await _expenseService.GetExpenseNo();
        }

        [ProducesResponseType(typeof(List<HeadWiseExpenseSumResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.GetHeadWiseExpenseSum)]
        public async Task<List<HeadWiseExpenseSumResponse>> GetHeadWiseExpenseSum([FromQuery] DateTime fromDate,[FromQuery] DateTime toDate)
        {
            return await _expenseService.GetHeadWiseExpenseSum(fromDate,toDate);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseTypeByIdPath)]
        public async Task<MasterDataTypeResponse> ExpenseTypeGet([FromRoute] int id)
        {
            return await _expenseTypeService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseTypePath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> ExpenseTypeGetAll([FromQuery] PagingRequest pagingRequest)
        {
            return await _expenseTypeService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterDataTypeResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseTypeSearchPath)]
        public async Task<PagingResponse<MasterDataTypeResponse>> ExpenseTypeSearch([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _expenseTypeService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(MasterDataTypeResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ExpenseTypePath)]
        public async Task<MasterDataTypeResponse> ExpenseTypeUpdate([FromBody] MasterDataTypeRequest request)
        {
            return await _expenseTypeService.Update(request);
        }

        [ProducesResponseType(typeof(ExpenseNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ExpenseNamePath)]
        public async Task<ExpenseNameResponse> ExpenseNameAdd([FromBody] ExpenseNameRequest request)
        {
            return await _expenseNameService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ExpenseNameDeletePath)]
        public async Task<int> ExpenseNameDelete([FromRoute] int id)
        {
            return await _expenseNameService.Delete(id);
        }

        [ProducesResponseType(typeof(ExpenseNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseNameByIdPath)]
        public async Task<ExpenseNameResponse> ExpenseNameGet([FromRoute] int id)
        {
            return await _expenseNameService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ExpenseNameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseNamePath)]
        public async Task<PagingResponse<ExpenseNameResponse>> ExpenseNameGetAll([FromQuery] PagingRequest pagingRequest)
        {
            return await _expenseNameService.GetAll(pagingRequest);
        }


        [ProducesResponseType(typeof(PagingResponse<ExpenseNameResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseNameSearchPath)]
        public async Task<PagingResponse<ExpenseNameResponse>> ExpenseNameSearch([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _expenseNameService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(ExpenseNameResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ExpenseNamePath)]
        public async Task<ExpenseNameResponse> ExpenseNameUpdate([FromBody] ExpenseNameRequest request)
        {
            return await _expenseNameService.Update(request);
        }

        [ProducesResponseType(typeof(ExpenseShopCompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.ExpenseCompanyPath)]
        public async Task<ExpenseShopCompanyResponse> ExpenseShopCompanyAdd([FromBody] ExpenseShopCompanyRequest request)
        {
            return await _expenseShopCompanyService.Add(request);
        }

        [ProducesResponseType(typeof(ExpenseShopCompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.ExpenseCompanyPath)]
        public async Task<ExpenseShopCompanyResponse> ExpenseShopCompanyUpdate([FromBody] ExpenseShopCompanyRequest request)
        {
            return await _expenseShopCompanyService.Update(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.ExpenseCompanyDeletePath)]
        public async Task<int> ExpenseShopCompanyDelete([FromRoute] int id)
        {
            return await _expenseShopCompanyService.Delete(id);
        }

        [ProducesResponseType(typeof(ExpenseShopCompanyResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseCompanyByIdPath)]
        public async Task<ExpenseShopCompanyResponse> ExpenseShopCompanyGet([FromRoute] int id)
        {
            return await _expenseShopCompanyService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<ExpenseShopCompanyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseCompanyPath)]
        public async Task<PagingResponse<ExpenseShopCompanyResponse>> ExpenseShopCompanyGetAll([FromQuery] PagingRequest pagingRequest)
        {
            return await _expenseShopCompanyService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<ExpenseShopCompanyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.ExpenseCompanySearchPath)]
        public async Task<PagingResponse<ExpenseShopCompanyResponse>> ExpenseShopCompanySearch([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return await _expenseShopCompanyService.Search(searchPagingRequest);
        }
    }
}
