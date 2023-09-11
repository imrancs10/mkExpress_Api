using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IDesignSampleRepository : ICrudRepository<DesignSample>
    {
        Task<List<DesignSample>> GetByCategory(int categotyId);
        Task<int> AddQuantity(int quantity, int designSampleId);
        Task<int> AddQuantity(Dictionary<int, int> sampleQuantity);
    }
}
