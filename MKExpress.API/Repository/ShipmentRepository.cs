using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly MKExpressContext _context;
        private readonly IShipmentTrackingRepository _shipmentTrackingRepository;
        public ShipmentRepository(MKExpressContext context, IShipmentTrackingRepository shipmentTrackingRepository)
        {
            _context = context;
            _shipmentTrackingRepository = shipmentTrackingRepository;
        }

        public async Task<Shipment> CreateShipment(Shipment shipment)
        {
            shipment.Status = ShipmentStatusEnum.Created.ToString();
            shipment.StatusReason = string.Empty;
            var trans = _context.Database.BeginTransaction();
            var entity = _context.Shipments.Add(shipment);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            if (await _context.SaveChangesAsync() > 0)
            {
                var tracking = new ShipmentTracking()
                {
                    Id = Guid.NewGuid(),
                    Activity = ShipmentStatusEnum.Created.ToString(),
                    ShipmentId = shipment.Id
                };
                if (await _shipmentTrackingRepository.AddTracking(tracking))
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
                .Include(x=>x.Customer)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.FromStore)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.ToStore)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.ShipperCity)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.ConsigneeCity)
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.ShipmentNumber)
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
                  .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.FromStore)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.ToStore)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.ShipperCity)
                .Include(x => x.ShipmentDetails)
                .ThenInclude(x => x.ConsigneeCity)
                  .Where(x => !x.IsDeleted && x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Shipment>> GetShipment(List<Guid> ids)
        {
            return await _context.Shipments
                 .Include(x => x.ShipmentDetails)
               .ThenInclude(x => x.FromStore)
               .Include(x => x.ShipmentDetails)
               .ThenInclude(x => x.ToStore)
               .Include(x => x.ShipmentDetails)
               .ThenInclude(x => x.ShipperCity)
               .Include(x => x.ShipmentDetails)
            .ThenInclude(x => x.ConsigneeCity)
                 .Where(x => !x.IsDeleted && ids.Contains(x.Id)).ToListAsync();
        }
    }
}
