using DocumentFormat.OpenXml.Office2010.Excel;
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

        public ContainerRepository(MKExpressContext context, ILoggerManager loggerManager, ICommonService commonService, IMasterJourneyRepository masterJourneyRepository)
        {
            _context = context;
            _loggerManager = loggerManager;
            _commonService = commonService;
            _masterJourneyRepository = masterJourneyRepository;
        }
        public async Task<Container> AddContainer(Container container)
        {
            var journey = await _masterJourneyRepository.Get(container.JourneyId)??throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.ContainerJourneyDetailsNotFound);

            List<ContainerJourney> containerJourneys = new List<ContainerJourney>();
            containerJourneys.Add(new ContainerJourney()
            {
                ContainerId = container.Id,
                Id = Guid.NewGuid(),
                StationId=journey.FromStationId,
                IsSourceStation=true,
                SequenceNo=1,                
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
                SequenceNo = containerJourneys.Count+1,
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
                .Where(x => !x.IsDeleted && x.Id == containerJourneyId && x.ContainerId == containerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            
            if (oldData.ArrivalAt != DateTime.MinValue)
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyCheckedInAtStation, StaticValues.Message_ContainerAlreadyCheckedInAtStation);
            
            if (oldData.IsSourceStation)
                throw new BusinessRuleViolationException(StaticValues.Error_CantCheckinAtSourceStation, StaticValues.Message_CantCheckinAtSourceStation);
            
            oldData.ArrivalAt = DateTime.Now;
            oldData.UpdatedBy = 0;// _commonService.GetLoggedInUserId();

            var entity = _context.ContainerJourneys.Attach(oldData);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CheckOutContainer(Guid containerId, Guid containerJourneyId)
        {
            var oldData = await _context.ContainerJourneys
                .Where(x => !x.IsDeleted && x.Id == containerJourneyId && x.ContainerId == containerId)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (oldData.DepartureOn != DateTime.MinValue)
                throw new BusinessRuleViolationException(StaticValues.Error_ContainerAlreadyCheckedOutAtStation, StaticValues.Message_ContainerAlreadyCheckedOutAtStation);

            if (oldData.IsSourceStation)
                throw new BusinessRuleViolationException(StaticValues.Error_CantCheckOutAtDestinationStation, StaticValues.Message_CantCheckOutAtDestinationStation);

            oldData.DepartureOn = DateTime.Now;
            oldData.UpdatedBy = 0;// _commonService.GetLoggedInUserId();

            var entity = _context.ContainerJourneys.Attach(oldData);
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
               .Include(x=>x.Journey)
               .ThenInclude(x=>x.FromStation)
               .Include(x => x.Journey)
               .ThenInclude(x => x.ToStation)
               .Include(x => x.Journey)
               .ThenInclude(x=>x.MasterJourneyDetails)
               .ThenInclude(x=>x.SubStation)
               .Include(x => x.ContainerType)
               .Include(x=>x.ClosedByMember)
               .Where(x => !x.IsDeleted)
               .AsQueryable();
            PagingResponse<Container> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data
               .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
               .Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<Container> GetContainer(Guid id)
        {
            var res= await _context.Containers
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               .ThenInclude(x => x.ShipmentDetail)
               .ThenInclude(x=>x.ShipperCity)
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               .ThenInclude(x => x.ShipmentDetail)
               .ThenInclude(x => x.ConsigneeCity)
               .Include(x=>x.ContainerTrackings)
               .ThenInclude(x=>x.CreatedMember)
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

        public async Task<List<ContainerJourney>> GetContainerJourney(int containerNo)
        {
            var data= await _context.Containers
                .Include(x=>x.ContainerJourneys)
                .ThenInclude(x=>x.Station)
                .Where(x=>!x.IsDeleted && x.ContainerNo== containerNo)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            return data.ContainerJourneys;

        }
    }
}
