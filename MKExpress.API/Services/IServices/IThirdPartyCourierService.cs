using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IThirdPartyCourierService : ICrudService<ThirdPartyCourierCompanyRequest, ThirdPartyCourierResponse>
    {
        Task<List<ShipmentResponse>> GetShipments(Guid thirdPartyId,DateTime fromDate, DateTime toDate);
        Task<bool> AddShipmentToThirdParty(List<ThirdPartyShipmentRequest> request);
    }
}
