using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.Exceptions;
using MKExpress.API.Middleware;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Repository
{
    public class ShipmentTrackingRepository: IShipmentTrackingRepository
    {
        private readonly MKExpressContext _context;
        private readonly ICommonService _commonService;
        public ShipmentTrackingRepository(MKExpressContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }

        public async Task<ShipmentTracking> AddTracking(ShipmentTracking shipmentTracking)
        {
            if (shipmentTracking == null)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);
            }
            //shipmentTracking.CommentBy = JwtMiddleware.GetUserId();
            var entity = _context.ShipmentTrackings.Add(shipmentTracking);
            entity.State = EntityState.Added;
            if(await _context.SaveChangesAsync()<1)
                return default;
            return entity.Entity;

        }

        public async Task<List<ShipmentTracking>> GetTrackingByShipmentId(Guid shipmentId)
        {
            var data= await _context.ShipmentTrackings
                .Include(x=>x.ShipmentImages)
                .Include(x=>x.Shipment)
                .ThenInclude(x=>x.Customer)
                .Include(x => x.Shipment)
                .ThenInclude(x=>x.ShipmentDetail)
                .ThenInclude(x=>x.FromStore)
                 .Include(x => x.Shipment)
                .ThenInclude(x => x.ShipmentDetail)
                .ThenInclude(x => x.ToStore)
                  .Include(x => x.Shipment)
                .ThenInclude(x => x.ShipmentDetail)
                .ThenInclude(x => x.ShipperCity)
                  .Include(x => x.Shipment)
                .ThenInclude(x => x.ShipmentDetail)
                .ThenInclude(x => x.ConsigneeCity)
                .Include(x=>x.CommentByMember)
                .Where(x=>!x.IsDeleted && x.ShipmentId==shipmentId)
                .OrderByDescending(x=>x.CreatedAt)
                .ToListAsync();
            return data;
        }
    }
}
