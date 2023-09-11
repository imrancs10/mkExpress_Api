using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IDesignSampleService : ICrudService<DesignSampleRequest, DesignSampleResponse>
    {
        Task<List<DesignSampleResponse>> GetByCategory(int categotyId);
        Task<int> AddQuantity(int quantity, int designSampleId);
        Task<int> AddQuantity(Dictionary<int, int> sampleQuantity);
    }
}
