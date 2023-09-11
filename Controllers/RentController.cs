using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Rents;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Rents;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentLocationService _rentLocationService;
        private readonly IRentDetailService _rentDetailService;
        public RentController(IRentLocationService rentLocationService, IRentDetailService rentDetailService)
        {
            _rentDetailService = rentDetailService;
            _rentLocationService = rentLocationService;
        }

        [ProducesResponseType(typeof(RentLocationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.RentLocationPath)]
        public async Task<RentLocationResponse> AddLocation([FromBody] RentLocationRequest request)
        {
            return await _rentLocationService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.RentLocationDeletePath)]
        public async Task<int> DeleteLocation([FromRoute] int id)
        {
            return await _rentLocationService.Delete(id);
        }

        [ProducesResponseType(typeof(RentLocationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentLocationByIdPath)]
        public async Task<RentLocationResponse> GetLocation([FromRoute] int id)
        {
            return await _rentLocationService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<RentLocationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentLocationPath)]
        public async Task<PagingResponse<RentLocationResponse>> GetAllLocation([FromQuery] PagingRequest pagingRequest)
        {
            return await _rentLocationService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<RentLocationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentLocationSearchPath)]
        public async Task<PagingResponse<RentLocationResponse>> SearchLocation([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _rentLocationService.Search(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<RentLocationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.RentLocationPath)]
        public async Task<RentLocationResponse> UpdateLocation([FromBody] RentLocationRequest request)
        {
            return await _rentLocationService.Update(request);
        }

        [ProducesResponseType(typeof(RentDetailResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.RentDetailPath)]
        public async Task<RentDetailResponse> AddDetail([FromBody] RentDetailRequest request)
        {
            return await _rentDetailService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.RentDetailDeletePath)]
        public async Task<int> DeleteDetail([FromRoute] int id)
        {
            return await _rentDetailService.Delete(id);
        }

        [ProducesResponseType(typeof(RentDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentDetailByIdPath)]
        public async Task<RentDetailResponse> GetDetail([FromRoute] int id)
        {
            return await _rentDetailService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<RentDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentDetailPath)]
        public async Task<PagingResponse<RentDetailResponse>> GetAllDetail([FromQuery] PagingRequest pagingRequest)
        {
            return await _rentDetailService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<RentDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentDetailSearchPath)]
        public async Task<PagingResponse<RentDetailResponse>> SearchDetail([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _rentDetailService.Search(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<RentDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.RentDetailPath)]
        public async Task<RentDetailResponse> UpdateDetail([FromBody] RentDetailRequest request)
        {
            return await _rentDetailService.Update(request);
        }

        [ProducesResponseType(typeof(List<RentTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentDetailTransactionPath)]
        public async Task<List<RentTransactionResponse>> SearchDetail([FromQuery] int id)
        {
            return await _rentDetailService.GetRentTransations(id);
        }

        [ProducesResponseType(typeof(PagingResponse<RentTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentGetDueRentPath)]
        public async Task<PagingResponse<RentTransactionResponse>> GetDueRents([FromQuery] bool isPaid,[FromQuery] PagingRequest pagingRequest)
        {
            return await _rentDetailService.GetDueRents(isPaid,pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<RentTransactionResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.RentSearchDeuRentPath)]
        public async Task<PagingResponse<RentTransactionResponse>> SearchDeuRents([FromQuery] bool isPaid, [FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _rentDetailService.SearchDeuRents(isPaid, pagingRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.RentPayDeuRentPath)]
        public async Task<int> PayDeuRents([FromBody] RentPayRequest rentPayRequest, [FromHeader] int userId)
        {
            return await _rentDetailService.PayDeuRents(rentPayRequest, userId);
        }
    }
}
