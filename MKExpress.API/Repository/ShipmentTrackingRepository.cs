using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ShipmentTrackingRepository: IShipmentTrackingRepository
    {
        private readonly MKExpressContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShipmentTrackingRepository(MKExpressContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> AddTracking(ShipmentTracking shipmentTracking)
        {
            if (shipmentTracking == null)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);
            }
            shipmentTracking.CommentBy = GetUpdatedBy();
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

        private Guid GetUpdatedBy()
        {
            if (!(bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey(StaticValues.ConstValue_UserId))
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_UserIdNotPresentInHeader, StaticValues.Error_UserIdNotPresentInHeader);
            }
            else
            {
                string? value = _httpContextAccessor.HttpContext?.Request.Headers[StaticValues.ConstValue_UserId].ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    if (Guid.TryParse(value, out Guid newUserId))
                    {
                        return newUserId;
                    }
                }
            }
            throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidGUID, StaticValues.Error_InvalidGUID);
        }
    }
}
