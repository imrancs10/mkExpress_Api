using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;

namespace MKExpress.API.Repository.IRepository
{
    public interface IThirdPartyCourierRepository:ICrudRepository<ThirdPartyCourierCompany>
    {
        Task<List<ThirdPartyShipment>> GetShipments(Guid thirdPartyId);
    }
}
