using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.DTO.BaseDto;

namespace MKExpress.API.Repository.IRepository
{
    public interface IMasterJourneyRepository:ICrudRepository<MasterJourney>
    {
        Task<List<MasterJourney>> GetJourneyList();
    }
}
