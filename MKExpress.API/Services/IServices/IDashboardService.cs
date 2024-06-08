using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IDashboardService
    {
        Task<DashboardShipmentStatusResponse> GetDashboardShipmentStatusCount(string status, int year);
        Task<List<DashboardShipmentStatusWiseCountResponse>> GetDashboardShipmentStatusWiseCount(int year);
    }
}
