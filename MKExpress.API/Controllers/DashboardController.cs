using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [ProducesResponseType(typeof(DashboardShipmentStatusResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardShipmenyStatusCountPath)]
        public async Task<DashboardShipmentStatusResponse> GetDashboardShipmentStatusCount([FromQuery] int? year, [FromQuery] string? status)
        {
            return await _dashboardService.GetDashboardShipmentStatusCount(status,year??DateTime.Now.Year);
        }

        [ProducesResponseType(typeof(List<DashboardShipmentStatusWiseCountResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardShipmenyStatusWiseCountPath)]
        public async Task<List<DashboardShipmentStatusWiseCountResponse>> GetDashboardShipmentStatusWiseCount([FromQuery] int? year)
        {
            return await _dashboardService.GetDashboardShipmentStatusWiseCount(year ?? DateTime.Now.Year);
        }
    }
}