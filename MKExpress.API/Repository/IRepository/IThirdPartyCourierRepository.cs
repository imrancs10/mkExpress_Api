using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IThirdPartyCourierRepository:ICrudRepository<ThirdPartyCourierCompany>
    {
        Task<List<ThirdPartyShipment>> GetShipments(Guid thirdPartyId,DateTime fromDate,DateTime toDate);
        Task<bool> AddShipmentToThirdParty(List<ThirdPartyShipment> request);
        Task<List<ThirdPartyShipment>> IsShipmentAddedInThirdParty(List<Guid> shipmentIds);
    }
}
