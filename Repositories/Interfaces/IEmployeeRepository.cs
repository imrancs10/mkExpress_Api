using MKExpress.API.Dto.Request.Employee;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Employee;
using MKExpress.API.Models;
using MKExpress.Web.API.Dto.Response.Employee;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IEmployeeRepository 
    {
        Task<List<Employee>> GetEmployeeByJobIds(List<int> jobIds);
        Task<List<Employee>> GetAllActiveDeactiveEmployee(bool allFixed = false);
        Task<List<Employee>> GetAllActiveDeactiveEmployeeSearch(string searchTearm, bool allFixed = false);
        Task<bool> ActiveDeactiveEmployee(int empId, bool isActive);
        Task<List<ExpireDocResponse>> GetExpireDocuments(int empId);
        Task<PagingResponse<Employee>> GetAll(EmployeePagingRequest pagingRequest);
        Task<PagingResponse<Employee>> Search(EmployeeSearchPagingRequest searchPagingRequest);
        Task<Employee> Add(Employee entity);
        Task<Employee> Update(Employee entity);
        Task<int> Delete(int Id);
        Task<Employee> Get(int Id);
        Task<List<EmployeeSalarySlipResponse>> GetEmployeeSalarySlip(int empId, int month, int year);
        Task<List<EmployeeSalaryLedgerResponse>> GetEmployeeSalaryLedger(int jobTitleId, int month, int year);

    }
}
