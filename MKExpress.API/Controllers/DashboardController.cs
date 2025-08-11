using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services;

namespace MKExpress.API.Controllers
{
    [ApiController]
    public class DashboardController(IDashboardService dashboardService,ILogger<DashboardController> logger) : ControllerBase
    {
        private readonly IDashboardService _dashboardService = dashboardService;
        private readonly ILogger<DashboardController> _logger = logger;

        [ProducesResponseType(typeof(DashboardShipmentStatusResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardShipmenyStatusCountPath)]
        public async Task<DashboardShipmentStatusResponse> GetDashboardShipmentStatusCount([FromQuery] int? year, [FromQuery] string? status)
        {
            try
            {
                throw new Exception("Status cannot be null or empty.");
                return await _dashboardService.GetDashboardShipmentStatusCount(status, year ?? DateTime.Now.Year);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching dashboard shipment status count.");
                throw;
            }
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