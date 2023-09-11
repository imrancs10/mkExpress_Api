using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICustomerMeasurementService
    {
        Task<CustomerMeasurement> AddUpdateMeasurement(CustomerMeasurement customerMeasurement);
        Task<CustomerMeasurementResponse> GetMeasurement(int customerId);
        Task<List<CustomerMeasurementResponse>> GetMeasurements(string contactNo);
        Task<int> UpdateMeasurement(List<UpdateMeasurementRequest> updateMeasurementRequest);
    }
}
