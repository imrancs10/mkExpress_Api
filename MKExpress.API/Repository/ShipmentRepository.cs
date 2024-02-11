using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Extension;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly MKExpressContext _context;
        private readonly IShipmentTrackingRepository _shipmentTrackingRepository;
        private readonly ICommonService _commonService;
        private readonly IAppSettingRepository _appSettingRepository;

        public ShipmentRepository(MKExpressContext context, IShipmentTrackingRepository shipmentTrackingRepository, ICommonService commonService,IAppSettingRepository appSettingRepository)
        {
            _context = context;
            _shipmentTrackingRepository = shipmentTrackingRepository;
            _commonService = commonService;
            _appSettingRepository = appSettingRepository;
        }

        public async Task<bool> AssignForPickup(List<AssignShipmentMember> requests)
        {
            if (!requests.Any())
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidParameters);

            var shipmentIds = requests.Select(x => x.ShipmentId).ToList();
            var shipments = await _context.Shipments.Where(x => !x.IsDeleted && shipmentIds.Contains(x.Id)).ToDictionaryAsync(x => x.Id, y => y);
            if (!shipments.Any())
                throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNumberNotFound, StaticValues.Message_ShipmentNumberNotFound);
            if (shipments.Count != requests.Count)
                throw new BusinessRuleViolationException(StaticValues.Error_SomeShipmentNoNotFound, StaticValues.Message_SomeShipmentNoNotFound);
            var memberId = requests.First().MemberId;
            var oldMemberData = await _context.AssignShipmentMembers
                .Where(x => !x.IsDeleted && x.MemberId == memberId && shipmentIds.Contains(x.ShipmentId))
                .ToListAsync();

            var trans = _context.Database.BeginTransaction();
            var userId = _commonService.GetLoggedInUserId();
            if ((oldMemberData.Any() && await _context.SaveChangesAsync() > 0) || !oldMemberData.Any())
                {
                    var shipmentMember = new List<AssignShipmentMember>();
                    requests.ForEach(res =>
                    {
                        shipmentMember.Add(new AssignShipmentMember()
                        {
                            AssignAt = DateTime.UtcNow,
                            ShipmentId = res.ShipmentId,
                            Id = Guid.NewGuid(),
                            AssignById = userId,
                            MemberId = res.MemberId,
                            Status = _commonService.ValidateShipmentStatus(shipments[res.ShipmentId].Status, ShipmentStatusEnum.AssignedForPickup),
                        });
                    });
                    _context.AssignShipmentMembers.AddRange(shipmentMember);
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        if (await UpdateShipmentStatus(shipmentIds, ShipmentStatusEnum.AssignedForPickup))
                        {
                            trans.Commit();
                            return true;
                        }
                    }
                }
            trans.Rollback();
            return false;
        }

        public async Task<Shipment> CreateShipment(Shipment shipment)
        {
            shipment.Status = ShipmentStatusEnum.Created.ToString();
            shipment.LastStatusUpdate = DateTime.Now;
            shipment.StatusReason = string.Empty;
            var trans = _context.Database.BeginTransaction();

            var scheduleShipmentAfterHour = await _appSettingRepository.GetAppSettingValueByKey<int>("autoScheduleShipmentInHour");
            shipment.SchedulePickupDate=DateTime.Now.AddHours(scheduleShipmentAfterHour);

            var entity = _context.Shipments.Add(shipment);
            entity.State = EntityState.Added;
            if (await _context.SaveChangesAsync() > 0)
            {
                var tracking = new ShipmentTracking()
                {
                    Id = Guid.NewGuid(),
                    Activity = ShipmentStatusEnum.Created.ToString(),
                    ShipmentId = shipment.Id
                };
                if (await _shipmentTrackingRepository.AddTracking(tracking)!=null)
                {
                    trans.Commit();
                    return entity.Entity;
                }
            }
            trans.Rollback();
            return new Shipment();
        }

        public async Task<PagingResponse<Shipment>> GetAllShipment(PagingRequest pagingRequest)
        {
            var data = _context.Shipments
                .Include(x => x.Customer)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.FromStore)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ToStore)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ShipperCity)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ConsigneeCity)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();
            PagingResponse<Shipment> response = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return response;
        }

        public async Task<Shipment> GetShipment(Guid id)
        {
            return await _context.Shipments
                  .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.FromStore)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ToStore)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ShipperCity)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ConsigneeCity)
                  .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Shipment>> GetShipment(List<Guid> ids)
        {
            return await _context.Shipments
                 .Include(x => x.ShipmentDetail)
               .ThenInclude(x => x.FromStore)
               .Include(x => x.ShipmentDetail)
               .ThenInclude(x => x.ToStore)
               .Include(x => x.ShipmentDetail)
               .ThenInclude(x => x.ShipperCity)
               .Include(x => x.ShipmentDetail)
            .ThenInclude(x => x.ConsigneeCity)
                 .Where(x => !x.IsDeleted && ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<Shipment>> GetShipmentByUser(string userName, ShipmentEnum shipment, ShipmentStatusEnum shipmentStatus)
        {
            return await _context.Shipments
                 .Include(x => x.ShipmentDetail)
               .ThenInclude(x => x.FromStore)
               .Include(x => x.ShipmentDetail)
               .ThenInclude(x => x.ToStore)
               .Include(x => x.ShipmentDetail)
               .ThenInclude(x => x.ShipperCity)
               .Include(x => x.ShipmentDetail)
            .ThenInclude(x => x.ConsigneeCity)
                 .Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<bool> IsShipmentExists(string shipmentNo)
        {
            return await _context.Shipments.Where(x=>!x.IsDeleted && x.ShipmentNumber== shipmentNo).AnyAsync();
        }

        public async Task<bool> UpdateShipmentStatus(List<Guid> shipmentIds, ShipmentStatusEnum newStatus, string comment = "")
        {
            comment = string.IsNullOrEmpty(comment) ? newStatus.ToFormatString() : comment;

            var shipments = await _context.Shipments.Where(x => !x.IsDeleted && shipmentIds.Contains(x.Id))
                 .ToListAsync();

            if (!shipments.Any())
                return false;

            shipments.ForEach(res =>
            {
                var isValidCurrentStatus = Enum.TryParse(res.Status, out ShipmentStatusEnum currentStatus);
                if (!isValidCurrentStatus)
                    throw new BusinessRuleViolationException(StaticValues.Error_InvalidCurrentShipmentStatus, $"{StaticValues.Message_InvalidCurrentShipmentStatus} {res.ShipmentNumber}");
                res.Status = _commonService.ValidateShipmentStatus(currentStatus, newStatus);
                res.LastStatusUpdate = DateTime.Now;
                res.UpdatedBy = 0;// _commonService.GetLoggedInUserId();
            });

            _context.Shipments.AttachRange(shipments);

            if (await _context.SaveChangesAsync() > 0)
            {
                List<ShipmentTracking> shipmentTrackings = new();
                shipments.ForEach((shipment) =>
                {
                    shipmentTrackings.Add(new()
                    {
                        ShipmentId = shipment.Id,
                        Activity = newStatus.ToFormatString(),
                        Comment1 = comment,
                        CommentBy = _commonService.GetLoggedInUserId(),
                        CreatedAt = DateTime.UtcNow,
                        Id = Guid.NewGuid(),
                    });
                });

                _context.ShipmentTrackings.AddRange(shipmentTrackings);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<Shipment>> ValidateShipment(List<string> shipmentNo)
        {
            return await _context.Shipments
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ConsigneeCity)
                  .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ShipperCity)
                .Where(x => !x.IsDeleted && shipmentNo.Contains(x.ShipmentNumber))
                .ToListAsync();
        }

        public async Task<Shipment?> ValidateShipmentStatus(string shipmentNo, ShipmentStatusEnum status)
        {
            return await _context.Shipments
                .Include (x => x.Customer)
                .Include(x => x.ShipmentDetail)
                .ThenInclude(x=>x.ConsigneeCity)

                .Include(x => x.ShipmentDetail)
                .ThenInclude(x => x.ShipperCity)

                .Where(x => !x.IsDeleted && x.ShipmentNumber == shipmentNo)
                .FirstOrDefaultAsync();
        }
    }
}
