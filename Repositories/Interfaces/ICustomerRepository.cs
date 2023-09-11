using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface ICustomerRepository : ICrudRepository<Customer>
    {
        Task<List<Customer>> GetCustomers(string contactNo);
    }
}
