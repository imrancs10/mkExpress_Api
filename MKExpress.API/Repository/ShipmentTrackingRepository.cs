using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.Exceptions;
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

        public async Task<bool> AddTracking(ShipmentTracking shipmentTracking)
        {
            if (shipmentTracking == null)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);
            }
            shipmentTracking.CommentBy = _commonService.GetLoggedInUserId();
            var entity = _context.ShipmentTrackings.Add(shipmentTracking);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<ShipmentTracking>> GetTrackingByShipmentId(Guid shipmentId)
        {
            return await _context.ShipmentTrackings
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
                .Where(x=>!x.IsDeleted && x.ShipmentId==shipmentId).ToListAsync();
        }
    }
}
