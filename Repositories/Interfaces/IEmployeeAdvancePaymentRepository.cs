using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IEmployeeAdvancePaymentRepository : ICrudRepository<EmployeeAdvancePayment>
    {
        Task<List<EmployeeAdvancePayment>> GetStatement(int empId);
    }
}
