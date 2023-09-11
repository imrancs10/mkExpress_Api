using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class MonthlyAttendenceRepository : IMonthlyAttendenceRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseNameRepository _expenseNameRepository;
        private readonly IConfiguration _configuration;

        public MonthlyAttendenceRepository(MKExpressDbContext context, IExpenseNameRepository expenseNameRepository,
            IExpenseRepository expenseRepository,IConfiguration configuration)
        {
            _context = context;
            _expenseNameRepository = expenseNameRepository;
            _expenseRepository = expenseRepository;
            _configuration = configuration;
        }
        public async Task<MonthlyAttendence> Add(MonthlyAttendence monthlyAttendence)
        {
            var entity = _context
                .MonthlyAttendences
                .Attach(monthlyAttendence);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> AddUpdateDailyAttendence(List<MonthlyAttendence> dailyAttendence, int day)
        {
            if (dailyAttendence == null || dailyAttendence.Count == 0)
                return default;

            List<int> requestedEmployeeIds = dailyAttendence.Select(x => x.EmployeeId).ToList();
            int requestedMonth = dailyAttendence.First().Month;
            int requestedYear = dailyAttendence.First().Year;
            int currentDay = day;
            List<MonthlyAttendence> existingRequestAttenedence = new List<MonthlyAttendence>();
            List<MonthlyAttendence> newRequestAttenedence = new List<MonthlyAttendence>();

            var existingAttendence = await _context.MonthlyAttendences
                .Where(x => requestedEmployeeIds.Contains(x.EmployeeId) && x.Month.Equals(requestedMonth) && x.Year.Equals(requestedYear))
                .ToListAsync();

            if (existingAttendence != null)
            {
                var requestAttedenceDic = dailyAttendence.ToDictionary(x => x.EmployeeId, y => y);
                foreach (MonthlyAttendence monthlyAttendence in existingAttendence)
                {
                    requestedEmployeeIds.Remove(monthlyAttendence.EmployeeId);
                    var requestDayAttendence = requestAttedenceDic[monthlyAttendence.EmployeeId].GetType().GetProperty("Day" + currentDay).GetValue(requestAttedenceDic[monthlyAttendence.EmployeeId]);
                    monthlyAttendence.GetType().GetProperty("Day" + currentDay).SetValue(monthlyAttendence, requestDayAttendence);
                }
                _context.UpdateRange(existingAttendence);
                await _context.SaveChangesAsync();

            }
            var nonExistingAttendence = dailyAttendence.Where(x => requestedEmployeeIds.Contains(x.EmployeeId)).ToList();
            if (nonExistingAttendence.Count > 0)
            {
                await _context
                    .MonthlyAttendences
                    .AddRangeAsync(nonExistingAttendence);
                await _context.SaveChangesAsync();
            }
            return default;
        }

        public async Task<int> Delete(int attendenceId)
        {
            MonthlyAttendence oldAttendence = await _context
                .MonthlyAttendences
              .Where(att => att.Id == attendenceId)
              .FirstOrDefaultAsync();
            _context.MonthlyAttendences.Remove(oldAttendence);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByEmpIdMonthYear(int employeeId, int month, int year)
        {
            MonthlyAttendence oldAttendence = await _context
                .MonthlyAttendences
              .Where(att => att.EmployeeId == employeeId && att.Month == month && att.Year == year)
              .FirstOrDefaultAsync();
            if (oldAttendence == null)
                return 1;
            _context.MonthlyAttendences.Remove(oldAttendence);
            return await _context.SaveChangesAsync();
        }

        public async Task<MonthlyAttendence> Get(int Id)
        {
            return await _context
                .MonthlyAttendences
               .Include(x => x.Employee)
               .Where(att => att.Id == Id)
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MonthlyAttendence>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MonthlyAttendences
                .Include(x => x.Employee)
                .ThenInclude(x => x.EmployeeAdvancePayments)
                .ThenInclude(x => x.EmployeeEMIPayments)
                .OrderByDescending(x => x.Year)
                .OrderByDescending(x => x.Month)
                .ToListAsync();
            PagingResponse<MonthlyAttendence> pagingResponse = new PagingResponse<MonthlyAttendence>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<MonthlyAttendence>> GetByEmpId(int employeeId, PagingRequest pagingRequest)
        {
            var data = await _context.MonthlyAttendences
                  .Include(x => x.Employee)
                .ThenInclude(x => x.EmployeeAdvancePayments)
                .ThenInclude(x => x.EmployeeEMIPayments)
                  .Where(x => x.EmployeeId == employeeId)
                  .OrderBy(x => x.Year)
                  .OrderBy(x => x.Month)
                  .ToListAsync();
            foreach (var item in data)
            {
                foreach (var advancePayment in item.Employee.EmployeeAdvancePayments)
                {
                    advancePayment.Employee = null;
                }
            }

            PagingResponse<MonthlyAttendence> pagingResponse = new PagingResponse<MonthlyAttendence>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize)
                .ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MonthlyAttendence> GetByEmpIdMonthYear(int employeeId, int month, int year)
        {
            return await _context
                .MonthlyAttendences
               .Include(x => x.Employee)
               .Where(att => att.EmployeeId == employeeId && att.Month == month && att.Year == year && !att.IsDeleted)
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MonthlyAttendence>> GetByMonthYear(PagingRequest pagingRequest, int month, int year)
        {
            var data = await _context
               .MonthlyAttendences
              .Include(x => x.Employee)
              .ThenInclude(x=>x.EmployeeAdvancePayments)
              .ThenInclude(x=>x.EmployeeEMIPayments)
              .Where(att => att.Month == month && att.Year == year)
              .AsNoTracking()
              .ToListAsync();

            PagingResponse<MonthlyAttendence> pagingResponse = new PagingResponse<MonthlyAttendence>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data
               .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
               .Take(pagingRequest.PageSize)
               .ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<MonthlyAttendence>> GetDailyAttendence(DateTime attendenceDate)
        {
            return await _context.MonthlyAttendences
                .Where(x => x.Month == attendenceDate.Month && x.Year == attendenceDate.Year).ToListAsync();
        }

        public async Task<int> PayMonthlySalary(int id, DateTime paidOn, decimal salary)
        {
            var salaryExpenseId = 0;
            int expenseNo = 0;
            string defaultPaymentMode = _configuration.GetValue<string>("DefaultPaymentMode");
            var oldData = await _context.MonthlyAttendences
                                         .Include(x=>x.Employee)
                                         .Where(x => !x.IsDeleted && x.Id == id)
                                         .FirstOrDefaultAsync();
            if (oldData == null)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            if (oldData.IsPaid)
                throw new BusinessRuleViolationException(StaticValues.SalaryAlreadyPaidError, StaticValues.SalaryAlreadyPaidMessage);
            if(oldData.Employee.IsFixedEmployee)
                salaryExpenseId = await _expenseNameRepository.GetExpenseNameByCode("staff_salary");
            else
                salaryExpenseId = await _expenseNameRepository.GetExpenseNameByCode("workers_salary");

            expenseNo = await _expenseRepository.GetExpenseNo();

            oldData.IsPaid = true;
            oldData.PaidOn = paidOn;
            var trans = _context.Database.BeginTransaction();
            var entity = _context.Attach(oldData);
            entity.State = EntityState.Modified;
            var result= await _context.SaveChangesAsync();
            if(result>0)
            {
                Expense expense = new Expense()
                {
                    EmplopeeId = oldData.EmployeeId,
                    EmpJobTitleId = oldData.Employee.JobTitleId,
                    ExpenseNameId = salaryExpenseId,
                    ExpenseDate = paidOn,
                    ExpenseNo = expenseNo,
                    Amount = salary,
                    PaymentMode = defaultPaymentMode,
                    Name = oldData.Employee.IsFixedEmployee ? "Staff Salary" : "Worker Salary",
                    Description = "Auto generated expense from employee monthly attendance page."                    
                };
                var response = await _expenseRepository.Add(expense);
                if(response.Id>0)
                {
                    trans.Commit();
                    return response.Id;
                }
                trans.Rollback();
            }
            return default;
        }

        public async Task<PagingResponse<MonthlyAttendence>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MonthlyAttendences
                .Include(x => x.Employee)
                .ThenInclude(x => x.EmployeeAdvancePayments)
                .ThenInclude(x => x.EmployeeEMIPayments)
                .OrderBy(x => x.Year)
                .OrderBy(x => x.Month)
                .Where(att =>
                        att.Employee.FirstName.Contains(searchTerm) ||
                         att.Employee.LastName.Contains(searchTerm) ||
                         att.Year.ToString().Contains(searchTerm) ||
                        att.Month.ToString().Contains(searchTerm)
                    )
                    .ToListAsync();
            foreach (var item in data)
            {
                foreach (var advancePayment in item.Employee.EmployeeAdvancePayments)
                {
                    advancePayment.Employee = null;
                }
            }
            PagingResponse<MonthlyAttendence> pagingResponse = new PagingResponse<MonthlyAttendence>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MonthlyAttendence> Update(MonthlyAttendence monthlyAttendence)
        {
            EntityEntry<MonthlyAttendence> oldOrder = _context.Update(monthlyAttendence);
            oldOrder.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldOrder.Entity;
        }
    }
}
