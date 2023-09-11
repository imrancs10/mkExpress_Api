using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICustomerService : ICrudService<CustomerRequest, CustomerResponse>
    {
        Task<List<CustomerResponse>> GetCustomers(string contactNo);
    }
}
