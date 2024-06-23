using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.SignalR;

namespace MKExpress.API.Repository
{
    public class ShipmentTrackingRepository: IShipmentTrackingRepository
    {
        private readonly MKExpressContext _context; 
        private readonly IHubContext<ShipmentTrackingSingleRHub> _hubContext;
        public ShipmentTrackingRepository(MKExpressContext context, IHubContext<ShipmentTrackingSingleRHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
            if (await _context.SaveChangesAsync()>0)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveShipmentUpdate", "New shipment added");
                return entity.Entity;
            }
            return default(ShipmentTracking);

        }

        public async Task<bool> AddTracking(List<ShipmentTracking> shipmentTrackings)
        {
            if (shipmentTrackings == null)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);
            }
            //shipmentTracking.CommentBy = JwtMiddleware.GetUserId();
            _context.ShipmentTrackings.AddRange(shipmentTrackings);
            if (await _context.SaveChangesAsync() < 1)
                return default;

            await _hubContext.Clients.All.SendAsync("ReceiveShipmentUpdate", "New shipment added");
            return true;
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
