using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Employee;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Employee;
using MKExpress.Web.API.Dto.Response.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponse>> GetEmployeeByJobIds(List<int> jobIds);
        Task<List<ExpireDocResponse>> GetExpireDocuments(int empId);
        Task<int> SendExpireDocumentsAlertEmail(int empId);
        Task<List<EmployeeResponse>> GetAllActiveDeactiveEmployee(bool allFixed = false);
        Task<bool> ActiveDeactiveEmployee(int empId, bool isActive);
        Task<List<EmployeeResponse>> GetAllActiveDeactiveEmployeeSearch(string searchTearm, bool allFixed = false);
        Task<EmployeeResponse> Add(EmployeeRequest request);
        Task<EmployeeResponse> Update(EmployeeRequest request);
        Task<int> Delete(int id);
        Task<EmployeeResponse> Get(int id);
        Task<PagingResponse<EmployeeResponse>> GetAll(EmployeePagingRequest pagingRequest);
        Task<PagingResponse<EmployeeResponse>> Search(EmployeeSearchPagingRequest searchPagingRequest);
        Task<List<EmployeeSalarySlipResponse>> GetEmployeeSalarySlip(int empId, int month, int year);
        Task<List<EmployeeSalaryLedgerResponse>> GetEmployeeSalaryLedger(int jobTitleId, int month, int year);
        Task<int> PayMonthlySalary(int id, DateTime paidOn, decimal salary);
    }
}
