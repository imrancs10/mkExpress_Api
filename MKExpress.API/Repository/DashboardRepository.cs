using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.DTO.Response;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MKExpressContext _context;

        public DashboardRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<DashboardShipmentStatusResponse> GetDashboardShipmentStatusCount(string status, int year)
        {
            year = year < 2020 ? DateTime.Now.Year : year;
            var res = new DashboardShipmentStatusResponse()
            {
                Data = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            var shipmentGroup = await _context.Shipments
                .Where(x => !x.IsDeleted && x.CreatedAt.Year == year && (status.ToLower()=="all" || x.Status.ToLower()==status))
                .OrderBy(x => x.CreatedAt)
                .GroupBy(x => x.CreatedAt.Month)
                .ToListAsync();
            res.Label = "Shipment Counts for "+year;
            shipmentGroup.ForEach(ele =>
            {
                res.Data[ele.Key - 1] = ele.Count();
            });
            return res;
        }

        public async Task<List<DashboardShipmentStatusWiseCountResponse>> GetDashboardShipmentStatusWiseCount(int year)
        {
            year = year < 2020 ? DateTime.Now.Year : year;
            var res = new List<DashboardShipmentStatusWiseCountResponse>();
            var shipmentGroup = await _context.Shipments
                .Where(x => !x.IsDeleted && x.CreatedAt.Year == year)
                .GroupBy(x => x.Status)
                .ToListAsync();
            return shipmentGroup.Select(x => new DashboardShipmentStatusWiseCountResponse()
            {
                Count=x.Count(),
                Status=x.Key
            }).ToList();
        }
    }
}
