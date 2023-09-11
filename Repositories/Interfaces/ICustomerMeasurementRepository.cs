using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICustomerMeasurementRepository
    {
        Task<CustomerMeasurement> AddUpdateMeasurement(CustomerMeasurement customerMeasurement);
        Task<List<CustomerMeasurement>> AddUpdateMeasurement(List<CustomerMeasurement> customerMeasurements);
        Task<CustomerMeasurement> GetMeasurement(int customerId);
        Task<List<CustomerMeasurement>> GetMeasurements(string contactNo);
        Task<int> UpdateMeasurement(List<UpdateMeasurementRequest> updateMeasurement);
    }
}
