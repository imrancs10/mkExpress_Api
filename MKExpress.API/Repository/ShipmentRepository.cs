using MKExpress.API.Data;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly MKExpressContext _context;
        public ShipmentRepository(MKExpressContext context)
        {
            _context = context;
        }

        public async Task<Shipment> CreateShipment(Shipment shipment)
        {
            var entity = _context.Shipments.Add(shipment);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Added;
            if (await _context.SaveChangesAsync() > 0)
            {
                return entity.Entity;
            }
            return new Shipment();
        }
    }
}
