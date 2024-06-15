using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public class SystemActionRepository : ISystemActionRepository
    {
        private readonly MKExpressContext _context;
        public SystemActionRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<PagingResponse<ShipmentTracking>> GetSystemActions(SystemActionRequest request)
        {
           var query=_context.ShipmentTrackings
                .Include(x=>x.Shipment)
                .ThenInclude(x=>x.Customer)
                .Include(x=>x.Shipment)
                .ThenInclude(x=>x.ShipmentDetail)
                .ThenInclude(x=>x.ConsigneeCity)
                .Include(x=>x.CreatedByUser)
                 .Include(x => x.Shipment)
                .ThenInclude(x => x.ShipmentDetail)
                .ThenInclude(x=>x.ToStore)
                .Where(x=>!x.IsDeleted && 
                (string.IsNullOrEmpty(request.ActionType) || x.Activity==request.ActionType) &&
                (request.ConsigneeCityId==null || x.Shipment.ShipmentDetail.ConsigneeCityId == request.ConsigneeCityId) &&
                (request.StationId == null || x.Shipment.ShipmentDetail.ToStoreId == request.StationId) &&
                (x.CreatedAt.Date>=request.ActionFrom.Value.Date && x.CreatedAt.Date<=request.ActionTo.Value.Date)
                )
                .OrderByDescending(x=>x.CreatedAt)
                .AsQueryable();

            var data= new PagingResponse<ShipmentTracking>()
            {
                Data=await query.Skip((request.PageNo-1)*request.PageSize).Take(request.PageSize).ToListAsync(),
                PageSize=request.PageSize,
                PageNo=request.PageNo,
                TotalRecords=await query.CountAsync(),
            };
            return data;
        }
    }
}
