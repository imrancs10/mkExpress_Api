using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IEmployeeAdvancePaymentService : ICrudService<EmployeeAdvancePaymentRequest, EmployeeAdvancePaymentResponse>
    {
        Task<List<EmployeeEMIPaymentResponse>> GetByEmployeeId(int employeeId);
        Task<List<EmployeeAdvancePaymentResponse>> GetStatement(int empId);
    }
}
