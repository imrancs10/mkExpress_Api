using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface ICustomerService : ICrudService<CustomerRequest, CustomerResponse>
    {
        Task<List<CustomerResponse>> GetCustomers(string contactNo);
        Task<List<DropdownResponse>> GetCustomersDropdown();
    }
}
