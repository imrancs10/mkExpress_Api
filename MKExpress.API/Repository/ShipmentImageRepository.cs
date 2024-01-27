using MKExpress.API.Data;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ShipmentImageRepository : IShipmentImageRepository
    {
        private readonly MKExpressContext _context;
        public ShipmentImageRepository(MKExpressContext context)
        {
            _context = context;   
        }
        public async Task<List<ShipmentImage>> AddImages(List<ShipmentImage> shipmentImages)
        {
            _context.ShipmentImages.AddRange(shipmentImages);
            if(await _context.SaveChangesAsync()>0)
                return shipmentImages;
            return new List<ShipmentImage>();
        }
    }
}
