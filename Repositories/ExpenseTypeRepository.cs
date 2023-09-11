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
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly MKExpressDbContext _context;
        public ExpenseTypeRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<ExpenseType> Add(ExpenseType expenseType)
        {
            var oldType = await _context.ExpenseTypes.Where(x => !x.IsDeleted && (x.Value == expenseType.Value || x.Code == expenseType.Code)).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.ExpenseTypeAlreadyExistError, StaticValues.ExpenseTypeAlreadyExistMessage);
            }
            var entity = _context.ExpenseTypes.Attach(expenseType);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            ExpenseType expenseType = await _context.ExpenseTypes
              .Where(mht => mht.Id == Id)
              .FirstOrDefaultAsync();
            if (expenseType == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (expenseType.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            if (await IsExpenseTypeInUse(expenseType.Id))
            {
                throw new BusinessRuleViolationException(StaticValues.DataIsInUseDeleteError, StaticValues.DataIsInUseDeleteMessage);
            }
            expenseType.IsDeleted = true;
            var entity = _context.ExpenseTypes.Update(expenseType);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<ExpenseType> Get(int Id)
        {
            return await _context.ExpenseTypes.Where(expType => !expType.IsDeleted && expType.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<ExpenseType>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.ExpenseTypes
             .Where(x => !x.IsDeleted)
             .OrderBy(x => x.Value)
             .ToListAsync();

            PagingResponse<ExpenseType> pagingResponse = new PagingResponse<ExpenseType>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<ExpenseType>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.ExpenseTypes
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm==string.Empty ||
                        mht.Value.Contains(searchTerm))
                    )
                .OrderBy(x => x.Value)
                    .ToListAsync();
            PagingResponse<ExpenseType> pagingResponse = new PagingResponse<ExpenseType>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<ExpenseType> Update(ExpenseType entity)
        {
            var oldType = await _context.ExpenseTypes
                .Where(x => !x.IsDeleted && (x.Value == entity.Value && x.Code == entity.Code))
                .CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.ExpenseTypeAlreadyExistError, StaticValues.ExpenseTypeAlreadyExistMessage);
            }
            EntityEntry<ExpenseType> oldExpenseType = _context.Update(entity);
            oldExpenseType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldExpenseType.Entity;
        }

        private async Task<bool> IsExpenseTypeInUse(int expenseTypeId)
        {
            return await _context.ExpenseNames.Where(x => !x.IsDeleted && x.ExpenseTypeId == expenseTypeId).CountAsync() > 0;
        }
    }
}
