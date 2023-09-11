using AutoMapper;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Request.Employee;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Employee;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using MKExpress.Web.API.Dto.Response.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IMonthlyAttendenceRepository _monthlyAttendenceRepository;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly IUserRepository _userRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper,
            IMonthlyAttendenceRepository monthlyAttendenceRepository,
            IConfiguration configuration,
            IAuthService authService, 
            IMailService mailService,
            IExpenseNameRepository expenseNameRepository,
            IExpenseRepository expenseRepository,
            IUserRepository userRepository)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _monthlyAttendenceRepository = monthlyAttendenceRepository;
            _authService = authService;
            _configuration = configuration;
            _mailService = mailService;
            _userRepository = userRepository;
        }

        public async Task<EmployeeResponse> Add(EmployeeRequest employeeRequest)
        {
            ValidateJobTitle(employeeRequest.JobTitleId);
            Employee employee = _mapper.Map<Employee>(employeeRequest);
            var result = await _employeeRepository.Add(employee);
            if (result != null && result.Id > 0 && employeeRequest.IsFixedEmployee)
            {
                MonthlyAttendence monthlyAttendence = new MonthlyAttendence()
                {
                    EmployeeId = result.Id,
                    Month = result.HireDate.Value.Month,
                    Year = result.HireDate.Value.Year
                };
                await _monthlyAttendenceRepository.Add(monthlyAttendence);

                string userName = string.IsNullOrEmpty(employee.Email) ? $"{employee.FirstName}.{employee.LastName}" : employee.Email;
                UserRegistrationRequest userRegistrationRequest = new UserRegistrationRequest
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Mobile = employee.Contact,
                    Email = userName,
                    Role = employeeRequest.Role,
                    Password = _configuration.GetSection("DefaultPassword").Value
                };
                await _authService.CreateIdentityUser(userRegistrationRequest);
            }
            return _mapper.Map<EmployeeResponse>(result);
        }

        public async Task<int> Delete(int employeeId)
        {
            var emp=await ValidateEmployee(employeeId);
            if (emp.IsFixedEmployee)
            {
                await _authService.DeleteIdentityUser(emp.Email);
                await _userRepository.DeleteUser(employeeId);
            }
            return await _employeeRepository.Delete(employeeId);
        }

        public async Task<EmployeeResponse> Get(int employeeId)
        {
            Employee employee = await _employeeRepository.Get(employeeId);

            if (employee == null)
                throw new BusinessRuleViolationException(StaticValues.EmployeeNotFoundError, StaticValues.EmployeeNotFoundMessage);

            return _mapper.Map<EmployeeResponse>(employee);
        }

        public async Task<PagingResponse<EmployeeResponse>> GetAll(EmployeePagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<EmployeeResponse>>(await _employeeRepository.GetAll(pagingRequest));
        }

        public async Task<PagingResponse<EmployeeResponse>> Search(EmployeeSearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<EmployeeResponse>>(await _employeeRepository.Search(searchPagingRequest));
        }

        public async Task<EmployeeResponse> Update(EmployeeRequest employeeRequest)
        {
            await ValidateEmployee(employeeRequest.Id);
            ValidateJobTitle(employeeRequest.JobTitleId);
            Employee employee = _mapper.Map<Employee>(employeeRequest);
            return _mapper.Map<EmployeeResponse>(await _employeeRepository.Update(employee));
        }

        private async Task<Employee> ValidateEmployee(int employeeId)
        {
            Employee employee = await _employeeRepository.Get(employeeId);
            if (employee == null || employee.Id != employeeId)
            {
                throw new BusinessRuleViolationException(StaticValues.EmployeeNotFoundError, StaticValues.EmployeeNotFoundMessage);
            }
            return employee;
        }
        private static void ValidateJobTitle(int jobTitleId)
        {
            if (jobTitleId < 1) throw new NotFoundException(StaticValues.JobTitleNotFoundError, StaticValues.JobTitleNotFoundMessage);
        }

        public async Task<List<EmployeeResponse>> GetEmployeeByJobIds(List<int> jobIds)
        {
            return _mapper.Map<List<EmployeeResponse>>(await _employeeRepository.GetEmployeeByJobIds(jobIds));
        }

        public async Task<List<ExpireDocResponse>> GetExpireDocuments(int empId)
        {
            return await _employeeRepository.GetExpireDocuments(empId);
        }

        public async Task<int> SendExpireDocumentsAlertEmail(int empId)
        {
            try
            {
                var emp = await _employeeRepository.Get(empId);
                if (emp == null || emp.Email == null)
                    return 0;
                var docList = await _employeeRepository.GetExpireDocuments(empId);
                if (docList.Count == 0)
                    return 0;
                var emailTemplate = await _mailService.GetMailTemplete(EmailTemplateEnum.EmployeeDocAlert);
                var docListHtmlString = string.Empty;
                int count = 0;
                foreach (var doc in docList)
                {
                    count++;
                    docListHtmlString += "<tr>" +
                            $"<td>{count}</td>" +
                            $"<td>{doc.DocumentName}</td>" +
                              $"<td>{doc.DocumentNumber}</td>" +
                                 $"<td>{doc.ExpireAt}</td>" +
                                    $"<td>{doc.Status}</td>" +
                        "</tr>";
                }

                emailTemplate = emailTemplate.Replace("{{userName}}", $"{emp.FirstName} {emp.LastName}")
                    .Replace("{{docList}}", docListHtmlString);
                var mailRequest = new MailRequest()
                {
                    Subject = "Employee Document Expiry Alert",
                    ToEmail = emp.Email,
                    Body = emailTemplate
                };
                await _mailService.SendEmailAsync(mailRequest);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeResponse>> GetAllActiveDeactiveEmployee(bool allFixed)
        {
            return _mapper.Map<List<EmployeeResponse>>(await _employeeRepository.GetAllActiveDeactiveEmployee(allFixed));
        }

        public async Task<bool> ActiveDeactiveEmployee(int empId, bool isActive)
        {
            return await _employeeRepository.ActiveDeactiveEmployee(empId, isActive);
        }

        public async Task<List<EmployeeResponse>> GetAllActiveDeactiveEmployeeSearch(string searchTearm, bool allFixed)
        {
            return _mapper.Map<List<EmployeeResponse>>(await _employeeRepository.GetAllActiveDeactiveEmployeeSearch(searchTearm, allFixed));
        }

        public async Task<List<EmployeeSalarySlipResponse>> GetEmployeeSalarySlip(int empId, int month, int year)
        {
            return await _employeeRepository.GetEmployeeSalarySlip(empId, month, year);
        }

        public async Task<int> PayMonthlySalary(int id, DateTime paidOn, decimal salary)
        {
            var result= await _monthlyAttendenceRepository.PayMonthlySalary(id, paidOn,salary);
            return result;
        }

        public async Task<List<EmployeeSalaryLedgerResponse>> GetEmployeeSalaryLedger(int jobTitleId, int month, int year)
        {
            return await _employeeRepository.GetEmployeeSalaryLedger(jobTitleId, month, year);
        }
    }
}
