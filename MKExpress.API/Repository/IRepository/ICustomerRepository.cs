using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface ICustomerRepository : ICrudRepository<Customer>
    {
        Task<List<Customer>> GetCustomers(string contactNo);
        Task<List<DropdownResponse>> GetCustomersDropdown();
    }
}
