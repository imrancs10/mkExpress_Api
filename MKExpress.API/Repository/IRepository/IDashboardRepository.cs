using MKExpress.API.DTO.Response;

namespace MKExpress.API.Repository
{
    public interface IDashboardRepository
    {
        Task<DashboardShipmentStatusResponse> GetDashboardShipmentStatusCount(string status, int year);
        Task<List<DashboardShipmentStatusWiseCountResponse>> GetDashboardShipmentStatusWiseCount(int year);
    }
}
