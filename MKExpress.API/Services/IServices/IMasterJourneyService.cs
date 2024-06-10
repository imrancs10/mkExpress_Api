using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IMasterJourneyService:ICrudService<MasterJourneyRequest,MasterJourneyResponse>
    {
        Task<List<DropdownResponse>> GetJourneyList();
    }
}
