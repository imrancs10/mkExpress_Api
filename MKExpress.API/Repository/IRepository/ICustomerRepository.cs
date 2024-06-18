using MKExpress.API.DTO.Response;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface ICustomerRepository : ICrudRepository<Customer>
    {
        Task<List<Customer>> GetCustomers(string contactNo);
        Task<List<DropdownResponse>> GetCustomersDropdown();
        Task<bool> ResetPassword(Guid customerId);
        Task<bool> BlockUnblockCustomer(Guid customerId,bool isBlocked);
    }
}
