using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Dashboard;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardResponse> GetDashboardData(int userId);
        Task<List<DashboardSalesResponse>> GetWeeklySales();
        Task<List<DashboardSalesResponse>> GetMonthlySales();
        Task<DashboardSalesResponse> GetDailySales();
        Task<EmployeeDashboardResponse> GetEmployeeDashboard();
        Task<OrderDashboardResponse> GetOrderDashboard();
        Task<ExpenseDashboardResponse> GetExpenseDashboard();
        Task<CrystalDeashboardResponse> GetCrystalDashboard();
    }
}
