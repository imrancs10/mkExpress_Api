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
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IExpenseNameRepository _expenseNameRepository;
        public ExpenseRepository(MKExpressDbContext context, IConfiguration configuration,IExpenseNameRepository expenseNameRepository)
        {
            _context = context;
            _configuration = configuration;
            _expenseNameRepository = expenseNameRepository;
        }

        public async Task<Expense> Add(Expense expense)
        {
            var oldexpense = await _context.Expenses.Where(x => !x.IsDeleted && x.ExpenseNo == expense.ExpenseNo).CountAsync();
            if (oldexpense > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.ExpenseNoAlreadyExistError, StaticValues.ExpenseNoAlreadyExistMessage);
            }
            if (expense.EmpJobTitleId == 0)
                expense.EmpJobTitleId = null;
            if (expense.EmplopeeId == 0)
                expense.EmplopeeId = null;
            var entity = _context.Expenses.Attach(expense);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> AddCancelOrderExpense(decimal amount, string remark, string paymentMode="Cash")
        {
            var expenseNameId = await _expenseNameRepository.GetExpenseNameByCode("order_cancel");
            var expenseNo = await GetExpenseNo();
            Expense expense = new Expense()
            {
                ExpenseNameId = expenseNameId,
                Amount = amount,
                Name = remark,
                ExpenseDate = DateTime.Today.Date,
                ExpenseNo = expenseNo,
                PaymentMode = paymentMode,
            };

            Expense data = await Add(expense);
            return data.Id > 0 ? 1 : 0;
        }

        public Task<int> AddCancelOrderExpense(decimal amount, string remark)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Delete(int Id)
        {
            Expense expense = await _context.Expenses
            .Where(mht => mht.Id == Id)
            .FirstOrDefaultAsync();
            if (expense == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (expense.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            expense.IsDeleted = true;
            var entity = _context.Expenses.Update(expense);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Expense> Get(int Id)
        {
            return await _context.Expenses
                .Include(x => x.Employee)
                .Include(x => x.ExpenseShopCompany)
                .Include(x => x.JobTitle)
                .Include(x => x.ExpenseName)
                .ThenInclude(x => x.ExpenseType)
                .Where(exp => exp.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<Expense>> GetAll(ExpensePagingRequest pagingRequest)
        {
            var data = await _context.Expenses
            .Include(x => x.Employee)
            .ThenInclude(x => x.MasterJobTitle)
                .Include(x => x.ExpenseShopCompany)
                .Include(x => x.ExpenseName)
                .ThenInclude(x => x.ExpenseType)
            .Where(x => !x.IsDeleted && 
            (pagingRequest.ExpenseNameId==0 || x.ExpenseNameId==pagingRequest.ExpenseNameId) &&
                x.ExpenseDate.Date >= pagingRequest.FromDate.Date && 
                x.ExpenseDate.Date<=pagingRequest.ToDate)
            .OrderByDescending(x => x.ExpenseNo)
            .ToListAsync();

            PagingResponse<Expense> pagingResponse = new PagingResponse<Expense>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<int> GetExpenseNo()
        {
            int defaultOrderNo = _configuration.GetValue<int>("ExpenseNoStartFrom");
            var expense = await _context.Expenses.OrderByDescending(x => x.ExpenseNo).FirstOrDefaultAsync();
            return expense == null ? defaultOrderNo : expense.ExpenseNo + 1;
        }

        public async Task<List<HeadWiseExpenseSumResponse>> GetHeadWiseExpenseSum(DateTime fromDate, DateTime toDate)
        {
           var result=await _context.Expenses
                    .Include(x => x.ExpenseName)
                    .ThenInclude(x=>x.ExpenseType)
                    .Where(x => !x.IsDeleted &&
                    x.ExpenseDate.Date >= fromDate &&
                    x.ExpenseDate.Date <= toDate)
                    .ToListAsync();
            var resultGrp = result.GroupBy(x => x.ExpenseNameId);
            var response = new List<HeadWiseExpenseSumResponse>();
            foreach (var item in resultGrp)
            {
                response.Add(new HeadWiseExpenseSumResponse()
                {
                    Amount=item.Sum(x=>x.Amount),
                    ExpenseName=item.FirstOrDefault()?.ExpenseName.Value,
                    ExpenseType= item.FirstOrDefault()?.ExpenseName.ExpenseType.Value,
                });
            }
            return response;
        }

        public async Task<decimal> GetTotalCancelOrderExpenseByDate(DateTime date)
        {
            var expenseNameId = await _expenseNameRepository.GetExpenseNameByCode("order_cancel");
            return await _context.Expenses
                .Where(x => !x.IsDeleted && 
                            x.ExpenseDate.Date == date.Date && 
                            x.ExpenseNameId==expenseNameId)
                .SumAsync(x => x.Amount);
        }

        public async Task<PagingResponse<Expense>> Search(ExpenseSearchPagingRequest pagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var data = await _context.Expenses
               .Include(x => x.Employee)
                .Include(x => x.ExpenseShopCompany)
                .Include(x => x.JobTitle)
                .Include(x => x.ExpenseName)
                .ThenInclude(x => x.ExpenseType)
                .Where(x => !x.IsDeleted && 
                x.ExpenseDate.Date>= pagingRequest.FromDate.Date && 
                x.ExpenseDate.Date<= pagingRequest.ToDate.Date &&
                (pagingRequest.ExpenseNameId==0 || x.ExpenseNameId==pagingRequest.ExpenseNameId) &&
                        (searchTerm.Equals(string.Empty) ||
                        x.ExpenseNo.ToString().Contains(searchTerm) ||
                        x.ExpenseName.Value.Contains(searchTerm) ||
                          x.Name.Contains(searchTerm) ||
                          x.Description.Contains(searchTerm) ||
                          x.PaymentMode.Contains(searchTerm) ||
                        (x.Employee.FirstName+" "+x.Employee.LastName).Contains(searchTerm) ||
                         x.ExpenseName.ExpenseType.Value.Contains(searchTerm))
                    )
                .OrderByDescending(x => x.ExpenseNo)
                    .ToListAsync();
            PagingResponse<Expense> pagingResponse = new PagingResponse<Expense>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<Expense> Update(Expense entity)
        {
            if((entity?.CompanyId??0)==0)
            {
                entity.CompanyId = null;
            }
            if (entity.EmpJobTitleId == 0)
                entity.EmpJobTitleId = null;
            if (entity.EmplopeeId == 0)
                entity.EmplopeeId = null;
            EntityEntry<Expense> oldExpenseType = _context.Update(entity);
            oldExpenseType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldExpenseType.Entity;
        }
    }
}
