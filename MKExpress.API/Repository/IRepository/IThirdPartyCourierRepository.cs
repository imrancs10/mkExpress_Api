using MKExpress.API.DTO.Request;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;

namespace MKExpress.API.Repository.IRepository
{
    public interface IThirdPartyCourierRepository:ICrudRepository<ThirdPartyCourierCompany>
    {
        Task<List<ThirdPartyShipment>> GetShipments(Guid thirdPartyId,DateTime fromDate,DateTime toDate);
        Task<bool> AddShipmentToThirdParty(List<ThirdPartyShipment> request);
        Task<List<ThirdPartyShipment>> IsShipmentAddedInThirdParty(List<Guid> shipmentIds);
    }
}
