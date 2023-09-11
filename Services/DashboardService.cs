using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Dashboard;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<CrystalDeashboardResponse> GetCrystalDashboard()
        {
            return await _dashboardRepository.GetCrystalDashboard();
        }

        public async Task<DashboardSalesResponse> GetDailySales()
        {
            return await _dashboardRepository.GetDailySales();
        }

        public async Task<DashboardResponse> GetDashboardData(int userId)
        {
            return await _dashboardRepository.GetDashboardData(userId);
        }

        public async Task<EmployeeDashboardResponse> GetEmployeeDashboard()
        {
            return await _dashboardRepository.GetEmployeeDashboard();
        }

        public async Task<ExpenseDashboardResponse> GetExpenseDashboard()
        {
            return await _dashboardRepository.GetExpenseDashboard();
        }

        public async Task<List<DashboardSalesResponse>> GetMonthlySales()
        {
            return await _dashboardRepository.GetMonthlySales();
        }

        public async Task<OrderDashboardResponse> GetOrderDashboard()
        {
            return await _dashboardRepository.GetOrderDashboard();
        }

        public async Task<List<DashboardSalesResponse>> GetWeeklySales()
        {
            return await _dashboardRepository.GetWeeklySales();
        }
    }
}
