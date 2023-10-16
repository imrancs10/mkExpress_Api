using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Logger;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services.IServices;

namespace MKExpress.API.Repository
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly MKExpressContext _context;
        private readonly ILoggerManager _loggerManager;
        private readonly ICommonService _commonService;
        private readonly IMasterJourneyRepository _masterJourneyRepository;
        private readonly IShipmentService _shipmentService;
        private readonly IShipmentRepository _shipmentRepository;
        public ContainerRepository(MKExpressContext context, ILoggerManager loggerManager, ICommonService commonService, IMasterJourneyRepository masterJourneyRepository, IShipmentService shipmentService, IShipmentRepository shipmentRepository)
        {
            _context = context;
            _loggerManager = loggerManager;
            _commonService = commonService;
            _masterJourneyRepository = masterJourneyRepository;
            _shipmentService = shipmentService;
            _shipmentRepository = shipmentRepository;
        }
        public async Task<Container> AddContainer(Container container)
        {
            var journey = await _masterJourneyRepository.Get(container.JourneyId) ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.ContainerJourneyDetailsNotFound);

            List<ContainerJourney> containerJourneys = new List<ContainerJourney>();
            containerJourneys.Add(new ContainerJourney()
            {
                ContainerId = container.Id,
                Id = Guid.NewGuid(),
                StationId = journey.FromStationId,
                IsSourceStation = true,
                SequenceNo = 1,
            });

            journey.MasterJourneyDetails.ForEach(res =>
            {
                containerJourneys.Add(new ContainerJourney()
                {
                    ContainerId = container.Id,
                    Id = Guid.NewGuid(),
                    StationId = res.SubStationId,
                    SequenceNo = containerJourneys.Count + 1,
                });
            });
            containerJourneys.Add(new ContainerJourney()
            {
                ContainerId = container.Id,
                Id = Guid.NewGuid(),
                StationId = journey.ToStationId,
                IsDestinationStation = true,
                SequenceNo = containerJourneys.Count + 1,
            });
            container.ContainerDetails.ForEach(res =>
            {
                res.Id = Guid.NewGuid();
                res.ContainerId = container.Id;
            });
            container.ContainerJourneys = containerJourneys;
            var entity = _context.Containers.Add(container);
            entity.State = EntityState.Added;
            if (await _context.SaveChangesAsync() > 0)
            {
                return entity.Entity;
            }
            return default(Container);
        }

        public async Task<bool> CheckInContainer(Guid containerId, Guid containerJourneyId)
        {
            var oldData = await _context.ContainerJourneys
                .Include(x => x.Container)
                .Where(x => !x.IsDeleted && x.Id == containerJourneyId && x.ContainerId == containerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (!oldData.Container.IsClosed)
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerIsNotClosed, StaticValues.Message_ContainerIsNotClosed);

            if (oldData.ArrivalAt != null && oldData.ArrivalAt != DateTime.MinValue)
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyCheckedInAtStation, StaticValues.Message_ContainerAlreadyCheckedInAtStation);

            if (oldData.IsSourceStation)
                throw new BusinessRuleViolationException(StaticValues.Error_CantCheckinAtSourceStation, StaticValues.Message_CantCheckinAtSourceStation);

            oldData.ArrivalAt = DateTime.Now;
            oldData.UpdatedBy = 0;// _commonService.GetLoggedInUserId();
            var trans = _context.Database.BeginTransaction();
            var entity = _context.ContainerJourneys.Attach(oldData);
            entity.State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
            {
                var tracking = new ContainerTracking()
                {
                    Code = GetTrackingCode(ContainerTrackingCodeEnum.CheckedIn),
                    ContainerId = containerId,
                    ContainerJourneyId = containerJourneyId,
                    CreatedById = _commonService.GetLoggedInUserId(),
                    Id = Guid.NewGuid()
                };
                _context.ContainerTrackings.Add(tracking);
                if (await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return true;
                }
            }
            trans.Rollback();
            return false;
        }

        public async Task<bool> CheckOutContainer(Guid containerId, Guid containerJourneyId)
        {
            var oldData = await _context.ContainerJourneys.Include(x => x.Container)
                .Where(x => !x.IsDeleted && x.Id == containerJourneyId && x.ContainerId == containerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (!oldData.Container.IsClosed)
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerIsNotClosed, StaticValues.Message_ContainerIsNotClosed);

            if (oldData.DepartureOn != null && oldData.DepartureOn != DateTime.MinValue)
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyCheckedOutAtStation, StaticValues.Message_ContainerAlreadyCheckedOutAtStation);

            if (oldData.IsDestinationStation)
                throw new BusinessRuleViolationException(StaticValues.Error_CantCheckOutAtDestinationStation, StaticValues.Message_CantCheckOutAtDestinationStation);

            oldData.DepartureOn = DateTime.Now;
            oldData.UpdatedBy = 0;// _commonService.GetLoggedInUserId();

            var trans = _context.Database.BeginTransaction();
            var entity = _context.ContainerJourneys.Attach(oldData);
            entity.State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
            {
                var tracking = new ContainerTracking()
                {
                    Code = GetTrackingCode(ContainerTrackingCodeEnum.CheckedOut),
                    ContainerId = containerId,
                    ContainerJourneyId = containerJourneyId,
                    CreatedById = _commonService.GetLoggedInUserId(),
                    Id = Guid.NewGuid()
                };
                _context.ContainerTrackings.Add(tracking);
                if (await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return true;
                }
            }
            trans.Rollback();
            return false;
        }

        public async Task<bool> CloseContainer(Guid containerId)
        {
            var oldData = await _context.Containers
              .Where(x => !x.IsDeleted && x.Id == containerId)
              .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (oldData.IsClosed)
            {
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyClosed, StaticValues.Message_ContainerAlreadyClosed);
            }

            oldData.IsClosed = true;
            oldData.ClosedOn = DateTime.Now;
            oldData.ClosedBy = _commonService.GetLoggedInUserId();
            _context.Containers.Attach(oldData);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteContainer(Guid containerId, string deleteReason)
        {
            var oldData = await _context.Containers
                .Include(x=>x.ContainerDetails)
                .Include(x => x.ContainerTrackings)
                .Include(x => x.ContainerJourneys)
              .Where(x => !x.IsDeleted && x.Id == containerId)
              .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (oldData.IsClosed)
            {
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerClosedCantDelete, StaticValues.Message_ContainerClosedCantDelete);
            }
            var deletedBy = 0;// _commonService.GetLoggedInUserId();
            var deletedAt = DateTime.UtcNow;

            oldData.IsDeleted = true;
            oldData.DeletedAt = deletedAt;
            oldData.DeletedBy = deletedBy;
            oldData.DeleteNote = deleteReason;

            oldData.ContainerDetails.ForEach(res =>
            {
                res.IsDeleted = true;
                res.DeletedAt = deletedAt;
                res.DeletedBy = deletedBy;
                res.DeleteNote = deleteReason;
            });

            oldData.ContainerJourneys.ForEach(res =>
            {
                res.IsDeleted = true;
                res.DeletedAt = deletedAt;
                res.DeletedBy = deletedBy;
                res.DeleteNote = deleteReason;
            });

            oldData.ContainerTrackings.ForEach(res =>
            {
                res.IsDeleted = true;
                res.DeletedAt = deletedAt;
                res.DeletedBy = deletedBy;
                res.DeleteNote = deleteReason;
            });


            var entity=_context.Containers.Update(oldData);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PagingResponse<Container>> GetAllContainer(PagingRequest pagingRequest)
        {
            var data = _context.Containers
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               //.ThenInclude(x => x.ShipmentDetail)
               //.Include(x => x.ContainerJourneys)
               .Include(x => x.Journey)
               .ThenInclude(x => x.FromStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.ToStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.MasterJourneyDetails)
               .ThenInclude(x => x.SubStation)
               .Include(x => x.ContainerType)
               .Include(x => x.ClosedByMember)
               .Where(x => !x.IsDeleted &&
               x.CreatedAt.Date >= pagingRequest.FromDate.Date &&
                x.CreatedAt.Date <= pagingRequest.ToDate.Date)
               .AsQueryable();
            PagingResponse<Container> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data
               .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
               .Take(pagingRequest.PageSize)
               .OrderByDescending(x=>x.ContainerNo)
               .ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<Container> GetContainer(Guid id)
        {
            var res = await _context.Containers
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               .ThenInclude(x => x.ShipmentDetail)
               .ThenInclude(x => x.ShipperCity)
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               .ThenInclude(x => x.ShipmentDetail)
               .ThenInclude(x => x.ConsigneeCity)
               .Include(x => x.ContainerTrackings)
               .ThenInclude(x => x.CreatedMember)
               .Include(x => x.ContainerJourneys)
               .Include(x => x.Journey)
               .ThenInclude(x => x.FromStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.ToStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.MasterJourneyDetails)
               .ThenInclude(x => x.SubStation)
               .Include(x => x.ContainerType)
               .Include(x => x.ClosedByMember)
                 .Where(x => !x.IsDeleted && x.Id == id)
                 .FirstOrDefaultAsync();
            return res;
        }

        public async Task<Container> GetContainerJourney(int containerNo)
        {
            var data = await _context.Containers
                .Include(x => x.ContainerJourneys)
                .ThenInclude(x => x.Station)
                .Where(x => !x.IsDeleted && x.ContainerNo == containerNo)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            return data;

        }

        public async Task<bool> RemoveShipmentFromContainer(Guid containerId, string shipmentNo)
        {
            var oldData = await _context.ContainerDetails
                .Include(x => x.Shipment)
               .Include(x => x.Container)
            .Where(x => !x.IsDeleted && x.ContainerId == containerId && x.Shipment.ShipmentNumber == shipmentNo)
            .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (oldData.Container.IsClosed)
            {
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyClosed, StaticValues.Message_ContainerAlreadyClosed);
            }

            var validateShipmentResponse = await _shipmentRepository.ValidateShipment(new List<string> { shipmentNo });


            if (validateShipmentResponse.Count > 0)
            {
                var entity = _context.ContainerDetails.Remove(oldData);
                entity.State = EntityState.Deleted;
                return await _context.SaveChangesAsync() > 0;
            }
            return false;

        }

        public async Task<PagingResponse<Container>> SearchContainer(SearchPagingRequest pagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? "" : pagingRequest.SearchTerm.ToLower();
            pagingRequest.FromDate = pagingRequest.FromDate == DateTime.MinValue ? DateTime.Now.AddYears(-1).Date : pagingRequest.FromDate;
            pagingRequest.ToDate = pagingRequest.ToDate == DateTime.MinValue ? DateTime.Now.Date : pagingRequest.ToDate;
            var data = _context.Containers
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               //.ThenInclude(x => x.ShipmentDetail)
               //.Include(x => x.ContainerJourneys)
               .Include(x => x.Journey)
               .ThenInclude(x => x.FromStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.ToStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.MasterJourneyDetails)
               .ThenInclude(x => x.SubStation)
               .Include(x => x.ContainerType)
               .Include(x => x.ClosedByMember)
               .ThenInclude(x => x.Role)
               .Where(x => !x.IsDeleted && 
               x.CreatedAt.Date>=pagingRequest.FromDate.Date &&
                x.CreatedAt.Date <= pagingRequest.ToDate.Date &&
               (searchTerm == "" ||
               x.ContainerNo.ToString().Contains(searchTerm) ||
               x.ClosedByMember.FirstName.Contains(searchTerm) ||
                x.ClosedByMember.LastName.Contains(searchTerm) ||
                 x.ClosedByMember.Role.Value.Contains(searchTerm) ||
                  x.ContainerType.Value.Contains(searchTerm) ||
                    x.Journey.FromStation.Value.Contains(searchTerm) ||
                     x.Journey.ToStation.Value.Contains(searchTerm)
               ))
               .AsQueryable();
            PagingResponse<Container> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data
               .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
               .Take(pagingRequest.PageSize)
               .OrderByDescending(x => x.ContainerNo)
               .ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<ShipmentValidateResponse> ValidateAndAddShipmentInContainer(Guid containerId, string shipmentNo)
        {

            var oldData = await _context.Containers
                .Include(x => x.ContainerJourneys)
             .Where(x => !x.IsDeleted && x.Id == containerId)
             .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (oldData.IsClosed)
            {
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyClosed, StaticValues.Message_ContainerAlreadyClosed);
            }

            var validateShipmentResponse = await _shipmentService.ValidateContainerShipment(new List<string> { shipmentNo }, oldData.JourneyId);


            if (validateShipmentResponse.Errors.FirstOrDefault()?.IsValid ?? false)
            {
                _context.ContainerDetails.Add(new ContainerDetail()
                {
                    ContainerId = containerId,
                    Id = Guid.NewGuid(),
                    ShipmentId = validateShipmentResponse.Shipments.First().Id,
                });
                if (await _context.SaveChangesAsync() == 0)
                {
                    _loggerManager.LogWarn(StaticValues.Message_UnableToSaveData, "ContainerRepository", "ValidateAndAddShipmentInContainer");
                    throw new BusinessRuleViolationException(StaticValues.Error_UnableToSaveData, StaticValues.Message_UnableToSaveData);
                }
            }
            return validateShipmentResponse;
        }

        private string GetTrackingCode(ContainerTrackingCodeEnum codeEnum)
        {
            return codeEnum switch
            {
                ContainerTrackingCodeEnum.CheckedIn => "Checked-In",
                ContainerTrackingCodeEnum.CheckedOut => "Checked-Out",
                _ => throw new NotImplementedException()
            };
        }
    }
}
