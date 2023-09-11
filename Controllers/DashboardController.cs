using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Dashboard;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [ProducesResponseType(typeof(DashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardPath)]
        public async Task<DashboardResponse> GetDashboardData([FromHeader] int userId)
        {
            return await _dashboardService.GetDashboardData(userId);
        }

        [ProducesResponseType(typeof(List<DashboardSalesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetWeeklySalePath)]
        public async Task<List<DashboardSalesResponse>> GetWeeklySales()
        {
            return await _dashboardService.GetWeeklySales();
        }

        [ProducesResponseType(typeof(DashboardSalesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetDailySalePath)]
        public async Task<DashboardSalesResponse> GetDailySales()
        {
            return await _dashboardService.GetDailySales();
        }

        [ProducesResponseType(typeof(List<DashboardSalesResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetMonthlySalePath)]
        public async Task<List<DashboardSalesResponse>> GetMonthlySales()
        {
            return await _dashboardService.GetMonthlySales();
        }

        [ProducesResponseType(typeof(EmployeeDashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetEmployeePath)]
        public async Task<EmployeeDashboardResponse> GetEmployeeDashboard()
        {
            return await _dashboardService.GetEmployeeDashboard();
        }

        [ProducesResponseType(typeof(OrderDashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetOrderPath)]
        public async Task<OrderDashboardResponse> GetOrderDashboard()
        {
            return await _dashboardService.GetOrderDashboard();
        }

        [ProducesResponseType(typeof(ExpenseDashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetExpensePath)]
        public async Task<ExpenseDashboardResponse> GetExpenseDashboard()
        {
            return await _dashboardService.GetExpenseDashboard();
        }

        [ProducesResponseType(typeof(CrystalDeashboardResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.DashboardGetCrystalPath)]
        public async Task<CrystalDeashboardResponse> GetCrystalDashboard()
        {
            return await _dashboardService.GetCrystalDashboard();
        }
    }
}
