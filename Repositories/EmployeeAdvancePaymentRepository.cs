using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public class EmployeeAdvancePaymentRepository : IEmployeeAdvancePaymentRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IEmployeeEMIPaymentRepository _employeeEMIPaymentRepository;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IExpenseNameRepository _expenseNameRepository;
        private readonly string EmpAdvanceExpenseNameCode = "employee_advance";
        public EmployeeAdvancePaymentRepository(MKExpressDbContext context, IExpenseNameRepository expenseNameRepository, IExpenseRepository expenseRepository, IEmployeeEMIPaymentRepository employeeEMIPaymentRepository)
        {
            _context = context;
            _employeeEMIPaymentRepository = employeeEMIPaymentRepository;
            _expenseRepository = expenseRepository;
            _expenseNameRepository = expenseNameRepository;
        }
        public async Task<EmployeeAdvancePayment> Add(EmployeeAdvancePayment employeeAdvancePayment)
        {
            var trans = _context.Database.BeginTransaction();
            var entity = _context.EmployeeAdvancePayments.Attach(employeeAdvancePayment);
            entity.State = EntityState.Added;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                if (await ConvertAdvanceAmountToEMI(entity.Entity) > 0)
                {
                    var expResult=await AddEmployeeAdvanceExpense(employeeAdvancePayment.Amount, employeeAdvancePayment.AdvanceDate, employeeAdvancePayment.EmployeeId,entity.Entity.Id);
                    if (expResult > 0)
                    {
                        await trans.CommitAsync();
                        return entity.Entity;
                    }
                }
            }
            trans.RollbackAsync();
            return default;
        }

        public async Task<int> Delete(int Id)
        {
            EmployeeAdvancePayment employeeAdvancePayment = await _context.EmployeeAdvancePayments
               .Where(emp => emp.Id == Id)
               .FirstOrDefaultAsync();
            if (employeeAdvancePayment == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (employeeAdvancePayment.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            employeeAdvancePayment.IsDeleted = true;
            var entity = _context.EmployeeAdvancePayments.Update(employeeAdvancePayment);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<EmployeeAdvancePayment> Get(int Id)
        {
            var data = await _context.EmployeeAdvancePayments
                .Include(x => x.EmployeeEMIPayments)
                .Include(x => x.Employee)
                .ThenInclude(x => x.MasterJobTitle)
                .Where(emp => emp.Id == Id && !emp.IsDeleted)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            if (data.Employee == null)
                return data;
            data.Employee.EmployeeAdvancePayments = null;
            return data;
        }

        public async Task<PagingResponse<EmployeeAdvancePayment>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.EmployeeAdvancePayments
               .Where(x => !x.IsDeleted)
               .Include(x => x.EmployeeEMIPayments)
               .Include(x => x.Employee)
                .ThenInclude(x => x.MasterJobTitle)
               .OrderByDescending(x => x.CreatedAt)
               .ToListAsync();
            foreach (var item in data)
            {
                if (item.Employee == null)
                    continue;
                item.Employee.EmployeeAdvancePayments = null;
                item.EmployeeEMIPayments = item.EmployeeEMIPayments.OrderBy(x => x.DeductionYear).ThenBy(x => x.DeductionMonth).ToList();
            }
            PagingResponse<EmployeeAdvancePayment> pagingResponse = new PagingResponse<EmployeeAdvancePayment>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<List<EmployeeAdvancePayment>> GetStatement(int empId)
        {
            return await _context.EmployeeAdvancePayments.Where(x => !x.IsDeleted && x.EmployeeId == empId).ToListAsync();
        }

        public async Task<PagingResponse<EmployeeAdvancePayment>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.EmployeeAdvancePayments
                .Include(x => x.EmployeeEMIPayments)
                 .Include(x => x.Employee)
                .ThenInclude(x => x.MasterJobTitle)
                .Where(emp => !emp.IsDeleted &&
                        searchTerm.Equals(string.Empty) ||
                        emp.Employee.FirstName.Contains(searchTerm) ||
                        emp.Employee.LastName.Contains(searchTerm) ||
                        emp.Reason.Contains(searchTerm) ||
                        emp.Amount.ToString().Contains(searchTerm)
                    )
                .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();
            foreach (var item in data)
            {
                if (item.Employee == null)
                    continue;
                item.Employee.EmployeeAdvancePayments = null;
            }
            PagingResponse<EmployeeAdvancePayment> pagingResponse = new PagingResponse<EmployeeAdvancePayment>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<EmployeeAdvancePayment> Update(EmployeeAdvancePayment employeeAdvancePayment)
        {
            var trans = _context.Database.BeginTransaction();
            EntityEntry<EmployeeAdvancePayment> oldEmp = _context.Update(employeeAdvancePayment);
            oldEmp.State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                if (await ConvertAdvanceAmountToEMI(oldEmp.Entity, true) > 0)
                {
                    await trans.CommitAsync();
                    return oldEmp.Entity;
                }
            }
            trans.RollbackAsync();
            return default;
        }

        private async Task<int> ConvertAdvanceAmountToEMI(EmployeeAdvancePayment employeeAdvancePayment, bool isUpdate = false)
        {
            if (employeeAdvancePayment.Id > 0 && employeeAdvancePayment.Amount > 0)
            {
                var addEmiResult = new List<EmployeeEMIPayment>();
                List<EmployeeEMIPayment> employeeEMIPayments = new List<EmployeeEMIPayment>();
                if (employeeAdvancePayment.EMI == 0)
                {
                    var currentDate = DateTime.UtcNow;
                    employeeEMIPayments.Add(new EmployeeEMIPayment()
                    {
                        AdvancePaymentId = employeeAdvancePayment.Id,
                        Amount = employeeAdvancePayment.Amount,
                        DeductionMonth = currentDate.AddMonths(1).Month,
                        DeductionYear = currentDate.AddMonths(1).Year,
                        Remark = $"Advance deduction for month {currentDate.Month}-{currentDate.Year}"
                    });
                    addEmiResult = await _employeeEMIPaymentRepository.Add(employeeEMIPayments);
                }
                else
                {
                    var emiStartDate = DateTime.UtcNow.AddMonths(1);
                    if (employeeAdvancePayment.EMIStartMonth > 0 & employeeAdvancePayment.EMIStartYear > 0)
                    {
                        emiStartDate = new DateTime(employeeAdvancePayment.EMIStartYear, employeeAdvancePayment.EMIStartMonth, 1);
                        if (emiStartDate <= DateTime.UtcNow)
                        {
                            throw new BusinessRuleViolationException(StaticValues.EMIStartMonthYearError, StaticValues.EMIStartMonthYearMessage);
                        }
                    }
                    for (int i = 0; i < employeeAdvancePayment.EMI; i++)
                    {
                        DateTime currentEMI = emiStartDate.AddMonths(i);
                        employeeEMIPayments.Add(new EmployeeEMIPayment()
                        {
                            AdvancePaymentId = employeeAdvancePayment.Id,
                            Amount = employeeAdvancePayment.Amount / employeeAdvancePayment.EMI,
                            DeductionMonth = currentEMI.Month,
                            DeductionYear = currentEMI.Year,
                            Remark = $"{i + 1}/{employeeAdvancePayment.EMI} Advance EMI deduction for month {currentEMI.Month}-{currentEMI.Year}"
                        });
                    }

                    if (isUpdate)
                    {
                        await _employeeEMIPaymentRepository.DeleteByAdvancePaymentId(employeeAdvancePayment.Id);
                    }
                    addEmiResult = await _employeeEMIPaymentRepository.Add(employeeEMIPayments);
                }
                return addEmiResult.FirstOrDefault()?.Id ?? 0;
            }
            return default;
        }

        private async Task<int> AddEmployeeAdvanceExpense(decimal amount, DateTime epenseDate, int empId,int advanceId)
        {
            var expenseNameId = await _expenseNameRepository.GetExpenseNameByCode(EmpAdvanceExpenseNameCode);
            if(expenseNameId<1)
            {
                throw new BusinessRuleViolationException(StaticValues.ExpenseNameNotFoundError, StaticValues.ExpenseNameNotFoundMessage + EmpAdvanceExpenseNameCode);
            }
            var expenseNo = await _expenseRepository.GetExpenseNo();
            Expense expense = new Expense()
            {
                ExpenseNameId = expenseNameId,
                EmplopeeId = empId,
                Amount = amount,
                Name = StaticValues.EmpAdvanceExpenseDescription,
                Description = StaticValues.EmpAdvanceExpenseDescription,
                ExpenseDate = epenseDate,
                ExpenseNo = expenseNo,
                PaymentMode = PaymentModeEnum.Cash.ToString(),
                ReferenceId=advanceId,
                ReferenceName= ExpenseReferenceTableEnum.EmployeeAddvancePayment.ToString(),
            };
            var result = await _expenseRepository.Add(expense);
            return result.Id;
        }
    }
}
