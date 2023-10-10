using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ContainerRepository : IContainerRepository
    {
        private readonly MKExpressContext _context;

        public ContainerRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<Container> AddContainer(Container container)
        {
            var entity = _context.Containers.Add(container);
            entity.State = EntityState.Added;
            if (await _context.SaveChangesAsync() > 0)
            {
                return entity.Entity;
            }
            return default(Container);
        }

        public async Task<ContainerJourney> CheckInContainer(Guid containerId, Guid containerJourneyId)
        {
            var oldData=await _context.ContainerJourneys.Where(x=>!x.IsDeleted &&) 
        }

        public Task<ContainerJourney> CheckOutContainer(Guid containerId, Guid containerJourneyId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagingResponse<Container>> GetAllContainer(PagingRequest pagingRequest)
        {
            var data= _context.Containers
               .Include(x => x.ContainerDetails)
               .ThenInclude(x => x.Shipment)
               .ThenInclude(x => x.ShipmentDetail)
               .Include(x => x.ContainerJourneys)
            .Include(x => x.ContainerType)
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
            return await _context.Containers
                .Include(x=>x.ContainerDetails)
                .ThenInclude(x=>x.Shipment)
                .ThenInclude(x => x.ShipmentDetail)
                .Include(x=>x.ContainerJourneys)
                .Include(x=>x.ContainerType)
                 .Where(x => !x.IsDeleted && x.Id == id)
                 .FirstOrDefaultAsync();
        }

        public Task<ContainerJourney> GetContainerJourney(Guid containerId)
        {
            throw new NotImplementedException();
        }
    }
}
