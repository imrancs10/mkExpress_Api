using MKExpress.API.DTO.Response;
using MKExpress.API.Repository;

namespace MKExpress.API.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repository;
        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<DashboardShipmentStatusResponse> GetDashboardShipmentStatusCount(string status, int year)
        {
            return await _repository.GetDashboardShipmentStatusCount(status, year);
        }

        public async Task<List<DashboardShipmentStatusWiseCountResponse>> GetDashboardShipmentStatusWiseCount(int year)
        {
            return await _repository.GetDashboardShipmentStatusWiseCount(year);
        }
    }
}
