using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IMasterJourneyRepository:ICrudRepository<MasterJourney>
    {
        Task<List<MasterJourney>> GetJourneyList();
    }
}
