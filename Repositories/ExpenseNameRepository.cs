using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class ExpenseNameRepository : IExpenseNameRepository
    {
        private readonly MKExpressDbContext _context;
        public ExpenseNameRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<ExpenseName> Add(ExpenseName expenseName)
        {
            var oldType = await _context.ExpenseNames.Where(x => !x.IsDeleted && (x.Value == expenseName.Value && x.ExpenseTypeId == expenseName.ExpenseTypeId)).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.ExpenseNameAlreadyExistError, StaticValues.ExpenseNameAlreadyExistMessage);
            }
            var entity = _context.ExpenseNames.Attach(expenseName);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            if(Id==0)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);

           ExpenseName expenseName = await _context.ExpenseNames
             .Where(mht => mht.Id == Id)
             .FirstOrDefaultAsync();
            if (expenseName == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (expenseName.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            if (await IsExpenseNameInUse(expenseName.Id))
            {
                throw new BusinessRuleViolationException(StaticValues.DataIsInUseDeleteError, StaticValues.DataIsInUseDeleteMessage);
            }
            expenseName.IsDeleted = true;
            var entity = _context.ExpenseNames.Update(expenseName);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<ExpenseName> Get(int Id)
        {
            return await _context.ExpenseNames
                .Include(x => x.ExpenseType)
                .Where(expName => expName.Id == Id && !expName.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<ExpenseName>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.ExpenseNames
             .Include(x => x.ExpenseType)
             .Where(x => !x.IsDeleted)
             .OrderBy(x => x.Value)
             .ToListAsync();

            PagingResponse<ExpenseName> pagingResponse = new PagingResponse<ExpenseName>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<int> GetExpenseNameByCode(string nameCode)
        {
            return await _context.ExpenseNames.Where(x => x.Code == nameCode.ToLower()).Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExpenseTypeInUse(int expenseTypeId)
        {
            return await _context.ExpenseNames.Where(x => !x.IsDeleted && x.ExpenseTypeId == expenseTypeId).CountAsync() > 0;
        }

        public async Task<PagingResponse<ExpenseName>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.ExpenseNames
                .Include(x => x.ExpenseType)
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm==string.Empty ||
                        mht.Value.Contains(searchTerm) ||
                         mht.ExpenseType.Value.Contains(searchTerm))
                    )
                .OrderBy(x => x.Value)
                    .ToListAsync();
            PagingResponse<ExpenseName> pagingResponse = new PagingResponse<ExpenseName>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<ExpenseName> Update(ExpenseName entity)
        {
            var oldType = await _context.ExpenseNames.Where(x => !x.IsDeleted && (x.Value == entity.Value && x.Code == entity.Code)).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.ExpenseTypeAlreadyExistError, StaticValues.ExpenseTypeAlreadyExistMessage);
            }

            EntityEntry<ExpenseName> oldExpenseType = _context.Update(entity);
            oldExpenseType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldExpenseType.Entity;
        }

        private async Task<bool> IsExpenseNameInUse(int expenseNameId)
        {
            return await _context.Expenses.Where(x => !x.IsDeleted && x.ExpenseNameId == expenseNameId).CountAsync() > 0;
        }
    }
}
