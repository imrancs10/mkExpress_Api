using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using MKExpress.Web.API.Dto.Request;
using MKExpress.Web.API.Dto.Response.Crystal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class CrystalTrackingController : ControllerBase
    {
        private readonly ICrystalTrackingOutService _crystalTrackingOutService;
        public CrystalTrackingController(ICrystalTrackingOutService crystalTrackingOutService)
        {
            _crystalTrackingOutService = crystalTrackingOutService;
        }


        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.CrystalTrackingOutPath)]
        public async Task<int> Add([FromBody] CrystalTrackingOutRequest request)
        {
            return await _crystalTrackingOutService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.CrystalTrackingOutDeletePath)]
        public Task<int> Delete([FromRoute] int Id)
        {
            return _crystalTrackingOutService.Delete(Id);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.CrystalTrackingOutDetailDeletePath)]
        public Task<int> DeleteDetail([FromRoute] int Id)
        {
            return _crystalTrackingOutService.DeleteDetail(Id);
        }

        [ProducesResponseType(typeof(CrystalTrackingOutResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutByIdPath)]
        public Task<CrystalTrackingOutResponse> Get([FromRoute]int Id)
        {
            return _crystalTrackingOutService.Get(Id);
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalTrackingOutResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutPath)]
        public Task<PagingResponse<CrystalTrackingOutResponse>> GetAll([FromQuery]PagingRequest pagingRequest)
        {
            return _crystalTrackingOutService.GetAll(pagingRequest);
        }

        [ProducesResponseType(typeof(List<CrystalConsumeDetailResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutConsumedDetailsPath)]
        public Task<List<CrystalConsumeDetailResponse>> GetCrystalConsumedDetails([FromQuery] CrystalStockPagingRequest pagingRequest)
        {
            return _crystalTrackingOutService.GetCrystalConsumedDetails(pagingRequest);
        }

        [ProducesResponseType(typeof(List<CrystalTrackingOutResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutByOrderDetailIdPath)]
        public Task<List<CrystalTrackingOutResponse>> GetByOrderDetailId([FromRoute] int Id)
        {
            return _crystalTrackingOutService.GetByOrderDetailId(Id);
        }
        
        [ProducesResponseType(typeof(List<CrystalUsedInOrderResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutGetOrderNoUsedInTrackingPath)]
        public Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId([FromRoute] int crystalId, [FromRoute] DateTime releaseDate)
        {
            return _crystalTrackingOutService.GetOrderUsedCrystalsByReleaseDateAndCrystalId(crystalId,releaseDate);
        }

        [ProducesResponseType(typeof(List<CrystalUsedInOrderResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutGetRangeOrderNoUsedInTrackingPath)]
        public Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId([FromRoute] int crystalId, [FromRoute] DateTime fromDate, [FromRoute] DateTime toDate)
        {
            return _crystalTrackingOutService.GetOrderUsedCrystalsByReleaseDateAndCrystalId(crystalId, fromDate,toDate);
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalTrackingOutResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalTrackingOutSearchPath)]
        public Task<PagingResponse<CrystalTrackingOutResponse>> Search([FromQuery] SearchPagingRequest searchPagingRequest)
        {
            return _crystalTrackingOutService.Search(searchPagingRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.CrystalTrackingOutAddNotePath)]
        public Task<int> AddNote([FromBody] CrystalTrackingOutRequest request)
        {
            return _crystalTrackingOutService.AddNote(request.Id,request.Note);
        }
    }
}
