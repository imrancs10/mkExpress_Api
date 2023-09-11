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
    public class ExpenseShopCompanyRepository : IExpenseShopCompanyRepository
    {
        private readonly MKExpressDbContext _context;
        public ExpenseShopCompanyRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<ExpenseShopCompany> Add(ExpenseShopCompany expenseShopCompany)
        {
            var oldCompanyName = await _context.ExpenseShopCompanies.Where(x => x.CompanyName == expenseShopCompany.CompanyName && !x.IsDeleted).CountAsync();
            if (oldCompanyName > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.CompanyNameAlreadyExistError, StaticValues.CompanyNameAlreadyExistMessage);
            }
            var entity = _context.ExpenseShopCompanies.Attach(expenseShopCompany);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            ExpenseShopCompany expenseName = await _context.ExpenseShopCompanies
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
            expenseName.IsDeleted = true;
            var entity = _context.ExpenseShopCompanies.Update(expenseName);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<ExpenseShopCompany> Get(int Id)
        {
            return await _context.ExpenseShopCompanies.Where(expName => expName.Id == Id && expName.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<ExpenseShopCompany>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.ExpenseShopCompanies
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.CompanyName)
            .ToListAsync();

            PagingResponse<ExpenseShopCompany> pagingResponse = new PagingResponse<ExpenseShopCompany>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<ExpenseShopCompany>> Search(SearchPagingRequest searchPagingRequest)
        {

            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.ExpenseShopCompanies
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm.Contains(string.Empty) ||
                        searchTerm.Contains(mht.CompanyName) ||
                        searchTerm.Contains(mht.ContactNo))
                    )
                .OrderBy(x => x.CompanyName)
                    .ToListAsync();
            PagingResponse<ExpenseShopCompany> pagingResponse = new PagingResponse<ExpenseShopCompany>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<ExpenseShopCompany> Update(ExpenseShopCompany entity)
        {

            var oldType = await _context.ExpenseShopCompanies.AsNoTracking().Where(x => !x.IsDeleted && x.Id == entity.Id).FirstOrDefaultAsync();
            if (oldType.CompanyName != entity.CompanyName)
            {
                var existed = await _context.ExpenseShopCompanies.AsNoTracking().Where(x => !x.IsDeleted && x.CompanyName == entity.CompanyName).CountAsync();
                if (existed > 0)
                {
                    throw new BusinessRuleViolationException(StaticValues.ExpenseCompanyAlreadyExistError, StaticValues.ExpenseCompanyAlreadyExistMessage);
                }
            }

            EntityEntry<ExpenseShopCompany> oldExpenseType = _context.Update(entity);
            oldExpenseType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldExpenseType.Entity;
        }
    }
}
