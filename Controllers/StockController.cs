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
    public class StockController : ControllerBase
    {
        private readonly IProductStockService _productStockService;
        public StockController(IProductStockService productStockService)
        {
            _productStockService = productStockService;
        }

        [ProducesResponseType(typeof(List<OrderCrystalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.StockGetCrystalPath)]
        public async Task<List<OrderCrystalResponse>> GetCrystals()
        {
            return await _productStockService.GetCrystals();
        }

        [ProducesResponseType(typeof(List<OrderUsedCrystalResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.StockGetOrderUsedCrystalPath)]
        public async Task<List<OrderUsedCrystalResponse>> GetOrderUsedCrystals([FromQuery] int orderDetailId)
        {
            return await _productStockService.GetOrderUsedCrystals(orderDetailId);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.StockSaveOrderUsedCrystalPath)]
        public async Task<int> GetOrderUsedCrystals([FromBody] List<OrderUsedCrystalRequest> request)
        {
            return await _productStockService.SaveOrderUsedCrystals(request);
        }
    }
}
