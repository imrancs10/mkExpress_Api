using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IShipmentImageRepository
    {
        Task<List<ShipmentImage>> AddImages(List<ShipmentImage> shipmentImages);
    }
}
