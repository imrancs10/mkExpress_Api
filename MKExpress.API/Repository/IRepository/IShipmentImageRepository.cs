using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IShipmentImageRepository
    {
        Task<List<ShipmentImage>> AddImages(List<ShipmentImage> shipmentImages);
    }
}
