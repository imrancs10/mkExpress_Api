using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response.Image;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Extension;
using MKExpress.API.Middleware;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Repository
{
    public class MobileApiRepository : IMobileApiRepository
    {
        private readonly MKExpressContext _context;
        private readonly ICommonService _commonService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IShipmentTrackingRepository _shipmentTrackingRepository;
        private readonly IAppSettingRepository _appSettingRepository;

        public MobileApiRepository(MKExpressContext context,IAppSettingRepository appSettingRepository, ICommonService commonService,IFileUploadService fileUploadService, IShipmentTrackingRepository shipmentTrackingRepository)
        {
            _context = context;
            _commonService = commonService;
            _fileUploadService = fileUploadService;
            _shipmentTrackingRepository = shipmentTrackingRepository;
            _appSettingRepository = appSettingRepository;
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
            shipment.LastStatusUpdate = DateTime.Now;
            shipment.PickupDate = DateTime.Now;
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
            var imagePathList = new List<ShipmentImageResponse>();
            try
            {
                var data = await _context.AssignShipmentMembers
                       .Where(x => !x.IsDeleted && x.ShipmentId == request.ShipmentId && x.MemberId == request.MemberId)
                       .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotAssignedToYou, StaticValues.Message_ShipmentNotAssignedToYou);

                var shipment = await _context.Shipments.Where(x => !x.IsDeleted && x.Id == request.ShipmentId)
                     .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotFound, StaticValues.Message_ShipmentNotFound);

                var trans = _context.Database.BeginTransaction();

                var newShipmentTracking = new ShipmentTracking()
                {
                    Comment1 = request.Reason,
                    Activity = ShipmentStatusEnum.PickupFailed.ToFormatString(),
                    ShipmentId = request.ShipmentId,
                    CommentBy = request.MemberId,
                    CreatedAt = DateTime.Now
                };
                var trackResult = await _shipmentTrackingRepository.AddTracking(newShipmentTracking);
                if (trackResult == null)
                {
                    trans.Rollback();
                    return false;
                }

                List<FileUploadRequest> imageRequest = new();
                var index = 0;
                request.PodImages.ForEach(res =>
                {
                    imageRequest.Add(new FileUploadRequest()
                    {
                        CreateThumbnail = true,
                        File = res,
                        FileType = FileTypeEnum.Image.ToString(),
                        ModuleName = ModuleNameEnum.Shipment,
                        Remark = request.Reason,
                        SequenceNo = index++,
                        ShipmentId = request.ShipmentId,
                        TrackingId = trackResult.Id
                    });
                });

                imagePathList = await _fileUploadService.UploadPhoto(imageRequest);
                if (!imagePathList.Any())
                {
                    trans.Rollback();
                    return false;
                }

                shipment.Status = ShipmentStatusEnum.PickupFailed.ToFormatString();
                shipment.LastStatusUpdate = DateTime.Now;
                shipment.FailedDelivery++;
                _context.Update(shipment);

                if (await _context.SaveChangesAsync() < 1)
                {
                    trans.Rollback();
                    return false;
                }


                _context.Remove(data);
                if (await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                imagePathList.ForEach(res =>
                {
                    if (!string.IsNullOrEmpty(res.Url))
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", res.Url);
                        File.Delete(path);
                        if (!string.IsNullOrEmpty(res.ThumbnailUrl))
                        {
                            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", res.ThumbnailUrl);
                            File.Delete(path);
                        }
                    }
                });
                throw ex;
            }
        }

        public async Task<bool> MarkPickupReschedule(MarkPickupStatusRequest request)
        {
            var imagePathList = new List<ShipmentImageResponse>();
            try
            {
                var data = await _context.AssignShipmentMembers
                       .Where(x => !x.IsDeleted && x.ShipmentId == request.ShipmentId && x.MemberId == request.MemberId)
                       .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotAssignedToYou, StaticValues.Message_ShipmentNotAssignedToYou);

                var shipment = await _context.Shipments.Where(x => !x.IsDeleted && x.Id == request.ShipmentId)
                     .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotFound, StaticValues.Message_ShipmentNotFound);

                var trans = _context.Database.BeginTransaction();

                var newShipmentTracking = new ShipmentTracking()
                {
                    Comment1 = request.Reason,
                    Activity = ShipmentStatusEnum.PickupRescheduled.ToFormatString(),
                    ShipmentId = request.ShipmentId,
                    CommentBy = request.MemberId,
                    CreatedAt = DateTime.Now
                };

                var trackResult = await _shipmentTrackingRepository.AddTracking(newShipmentTracking);

                if (trackResult == null)
                {
                    trans.Rollback();
                    return false;
                }

                if (request.PodImages.Any())
                {
                    List<FileUploadRequest> imageRequest = new();
                    var index = 0;
                    request.PodImages.ForEach(res =>
                    {
                        imageRequest.Add(new FileUploadRequest()
                        {
                            CreateThumbnail = true,
                            File = res,
                            FileType = FileTypeEnum.Image.ToString(),
                            ModuleName = ModuleNameEnum.Shipment,
                            Remark = request.Reason,
                            SequenceNo = index++,
                            ShipmentId = request.ShipmentId,
                            TrackingId = trackResult.Id
                        });
                    });

                    imagePathList = await _fileUploadService.UploadPhoto(imageRequest);
                    if (!imagePathList.Any())
                    {
                        trans.Rollback();
                        return false;
                    }
                }
                var rescheduleShipmentAfterHour = await _appSettingRepository.GetAppSettingValueByKey<int>("recheduleShipmentInHour");
                shipment.Status = ShipmentStatusEnum.PickupRescheduled.ToFormatString();
                shipment.SchedulePickupDate = DateTime.Now.AddHours(rescheduleShipmentAfterHour);
                _context.Update(shipment);

                if (await _context.SaveChangesAsync() < 1)
                {
                    trans.Rollback();
                    return false;
                }


                _context.Remove(data);
                if (await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                imagePathList.ForEach(res =>
                {
                    if (!string.IsNullOrEmpty(res.Url))
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", res.Url);
                        File.Delete(path);
                        if (!string.IsNullOrEmpty(res.ThumbnailUrl))
                        {
                            path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", res.ThumbnailUrl);
                            File.Delete(path);
                        }
                    }
                });
                throw ex;
            }
        }

        public async Task<bool> MarkReadyForPickup(Guid memberId, Guid shipmentId)
        {
            var data = await _context.AssignShipmentMembers
               .Where(x => !x.IsDeleted && x.ShipmentId == shipmentId && x.MemberId == memberId)
               .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotAssignedToYou, StaticValues.Message_ShipmentNotAssignedToYou);

            var trans = _context.Database.BeginTransaction();
            var shipment = await _context.Shipments.Where(x => !x.IsDeleted && x.Id == shipmentId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.Error_ShipmentNotFound, StaticValues.Message_ShipmentNotFound);

            shipment.Status = _commonService.ValidateShipmentStatus(shipment.Status.ToShipmentStatusEnumString(), ShipmentStatusEnum.ReadyForPickup);
            shipment.LastStatusUpdate = DateTime.Now;
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
