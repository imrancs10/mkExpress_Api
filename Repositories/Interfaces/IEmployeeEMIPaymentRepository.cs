using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IEmployeeEMIPaymentRepository
    {
        Task<List<EmployeeEMIPayment>> Add(List<EmployeeEMIPayment> employeeEMIPayments);
        Task<List<EmployeeEMIPayment>> GetByEmployeeId(int employeeId);
        Task<int> DeleteByAdvancePaymentId(int advancePaymentId);
    }
}
