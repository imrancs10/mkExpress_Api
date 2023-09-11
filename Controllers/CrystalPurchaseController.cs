using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class CrystalPurchaseController : ControllerBase
    {
        private readonly ICrystalPurchaseService _crystalPurchaseService;
        private readonly ICrystalStockService _crystalStockService;
        public CrystalPurchaseController(ICrystalPurchaseService crystalPurchaseService, ICrystalStockService crystalStockService)
        {
            _crystalPurchaseService = crystalPurchaseService;
            _crystalStockService = crystalStockService;
        }

        [ProducesResponseType(typeof(CrystalPurchaseResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.CrystalPurchasePath)]
        public async Task<CrystalPurchaseResponse> AddMasterCrystal([FromBody] CrystalPurchaseRequest request)
        {
            return await _crystalPurchaseService.Add(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalPurchaseGetNumberPath)]
        public async Task<int> GetCrystalPurchaseNo()
        {
            return await _crystalPurchaseService.GetCrystalPurchaseNo();
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalPurchaseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalPurchasePath)]
        public async Task<PagingResponse<CrystalPurchaseResponse>> GetAllCrystalPurchase([FromQuery] PagingRequest pagingRequest)
        {
            return await _crystalPurchaseService.GetAll(pagingRequest );
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalPurchaseResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalPurchaseSearchPath)]
        public async Task<PagingResponse<CrystalPurchaseResponse>> SearchCrystalPurchase([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _crystalPurchaseService.Search(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalStockResponseExt>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalStockGetDetailPath)]
        public async Task<PagingResponse<CrystalStockResponseExt>> GetStockDetails([FromQuery] CrystalStockPagingRequest pagingRequest)
        {
            return await _crystalStockService.GetCrystalStockDetails(pagingRequest);
        }

        [ProducesResponseType(typeof(CrystalStockResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalStockByIdPath)]
        public async Task<CrystalStockResponse> GetStockDetail([FromRoute] int id)
        {
            return await _crystalStockService.GetCrystalStockDetail(id);
        }

        [ProducesResponseType(typeof(List<CrystalStockResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalStockGetAlertPath)]
        public async Task<List<CrystalStockResponse>> GetStockAlerts([FromQuery] CrystalStockPagingRequest pagingRequest)
        {
            return await _crystalStockService.GetCrystalStockAlert(pagingRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.CrystalStockPath)]
        public async Task<int> UpdateCrystalStock([FromBody] CrystalStockRequest crystalStockRequest,[FromQuery] string reason)
        {
            return await _crystalStockService.UpdateStock(crystalStockRequest,reason);
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalStockResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalStockSearchAlertPath)]
        public async Task<PagingResponse<CrystalStockResponse>> SearchCrystalStockAlert([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _crystalStockService.SearchCrystalStockAlert(pagingRequest);
        }

        [ProducesResponseType(typeof(PagingResponse<CrystalStockResponseExt>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.CrystalStockSearchDetailPath)]
        public async Task<PagingResponse<CrystalStockResponseExt>> SearchCrystalStockDetails([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _crystalStockService.SearchCrystalStockDetails(pagingRequest);
        }

    }
}
