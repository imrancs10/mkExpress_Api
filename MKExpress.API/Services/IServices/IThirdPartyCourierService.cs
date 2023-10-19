using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Services.Interfaces;

namespace MKExpress.API.Services.IServices
{
    public interface IThirdPartyCourierService : ICrudService<ThirdPartyCourierCompanyRequest, ThirdPartyCourierResponse>
    {
        Task<List<ShipmentResponse>> GetShipments(Guid thirdPartyId);
    }
}
