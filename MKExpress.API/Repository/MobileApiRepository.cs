using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.Exceptions;
using MKExpress.API.Extension;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Repository
{
    public class MobileApiRepository : IMobileApiRepository
    {
        private readonly MKExpressContext _context;
        private readonly ICommonService _commonService;

        public MobileApiRepository(MKExpressContext context, ICommonService commonService)
        {
            _context = context;
            _commonService = commonService;
        }

        public async Task<List<Shipment>> GetShipmentByMember(Guid memberId, ShipmentStatusEnum shipmentStatus)
        {
            return await _context.AssignShipmentMembers
                .Include(x => x.Shipment)
                .Where(x => !x.IsDeleted && x.MemberId == memberId && x.Shipment.Status == shipmentStatus.ToFormatString())
                .Select(x => x.Shipment)
                .OrderBy(x => x.SchedulePickupDate)
                .ToListAsync();
        }

        public async Task<bool> MarkPickupDone(Guid memberId, Guid shipmentId)
        {
            var data = await _context.AssignShipmentMembers
                .Where(x => !x.IsDeleted && x.ShipmentId == shipmentId && x.MemberId == memberId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotAssignedToYou, StaticValues.Message_ShipmentNotAssignedToYou);

            var trans = _context.Database.BeginTransaction();
            var shipment = await _context.Shipments.Where(x => !x.IsDeleted && x.Id == shipmentId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotFound, StaticValues.Message_ShipmentNotFound);

            shipment.Status = _commonService.ValidateShipmentStatus(shipment.Status, ShipmentStatusEnum.PickedUp);
            shipment.UpdatedBy = 0;
            _context.Update(shipment);

            if (await _context.SaveChangesAsync() > 0)
            {
                var newShipmentTracking = new ShipmentTracking()
                {
                    Comment1 = ShipmentStatusEnum.PickedUp.ToFormatString(),
                    Activity = ShipmentStatusEnum.PickedUp.ToFormatString(),
                    ShipmentId = shipmentId,
                    CommentBy = memberId,
                    CreatedAt = DateTime.Now
                };

                _context.ShipmentTrackings.Add(newShipmentTracking);
                if (await _context.SaveChangesAsync() > 0)
                {
                    _context.Remove(data);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        trans.Commit();
                        return true;
                    }
                }
            }

            trans.Rollback();
            return false;
        }

        public async Task<bool> MarkPickupFailed(MarkPickupStatusRequest request)
        {
            var data = await _context.AssignShipmentMembers
                .Where(x => !x.IsDeleted && x.ShipmentId == request.ShipmentId && x.MemberId == request.MemberId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotAssignedToYou, StaticValues.Message_ShipmentNotAssignedToYou);

            var trans = _context.Database.BeginTransaction();
            var shipment = await _context.Shipments.Where(x => !x.IsDeleted && x.Id == request.ShipmentId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotFound, StaticValues.Message_ShipmentNotFound);

            shipment.Status = _commonService.ValidateShipmentStatus(shipment.Status, ShipmentStatusEnum.PickupFailed);
            shipment.UpdatedBy = 0;
            _context.Update(shipment);

            if (await _context.SaveChangesAsync() > 0)
            {

                var newShipmentTracking = new ShipmentTracking()
                {
                    Comment1 = request.Reason,
                    Activity = ShipmentStatusEnum.PickedUp.ToFormatString(),
                    ShipmentId = request.ShipmentId,
                    CommentBy = request.MemberId,
                    CreatedAt = DateTime.Now
                };

                _context.ShipmentTrackings.Add(newShipmentTracking);
                if (await _context.SaveChangesAsync() > 0)
                {
                    _context.Remove(data);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        trans.Commit();
                        return true;
                    }
                }
            }
        }

        public Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MarkReadyForPickup(Guid memberId, Guid shipmentId)
        {
            var data = await _context.AssignShipmentMembers
               .Where(x => !x.IsDeleted && x.ShipmentId == shipmentId && x.MemberId == memberId)
               .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotAssignedToYou, StaticValues.Message_ShipmentNotAssignedToYou);

            var trans = _context.Database.BeginTransaction();
            var shipment = await _context.Shipments.Where(x => !x.IsDeleted && x.Id == shipmentId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotFound, StaticValues.Message_ShipmentNotFound);

            shipment.Status = _commonService.ValidateShipmentStatus(shipment.Status, ShipmentStatusEnum.ReadyForPickup);
            shipment.UpdatedBy = 0;
            _context.Update(shipment);

            if (await _context.SaveChangesAsync() > 0)
            {
                var newShipmentTracking = new ShipmentTracking()
                {
                    Comment1 = ShipmentStatusEnum.ReadyForPickup.ToFormatString(),
                    Activity = ShipmentStatusEnum.ReadyForPickup.ToFormatString(),
                    ShipmentId = shipmentId,
                    CommentBy = memberId,
                    CreatedAt = DateTime.Now
                };

                _context.ShipmentTrackings.Add(newShipmentTracking);

                if (await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return true;
                }
            }

            trans.Rollback();
            return false;
        }
    }
}
