using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class KandooraExpenseRepository : IKandooraExpenseRepository
    {
        private readonly MKExpressDbContext _context;
        public KandooraExpenseRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<EachKandooraExpenseHead> AddExpenseHead(EachKandooraExpenseHead eachKandooraExpenseHead)
        {
            if (await IsExpenseHeadExist(eachKandooraExpenseHead.HeadName))
            {
                throw new BusinessRuleViolationException(StaticValues.KandooraExpenseHeadAlreadyExistError, StaticValues.KandooraExpenseHeadAlreadyExistMessage);
            }
            var entity = _context.EachKandooraExpenseHeads.Attach(eachKandooraExpenseHead);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<List<EachKandooraExpense>> AddExpense(List<EachKandooraExpense> eachKandooraExpense)
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE EachKandooraExpenses");
            _context.EachKandooraExpenses.AttachRange(eachKandooraExpense);
            await _context.SaveChangesAsync();
            return eachKandooraExpense;
        }

        public async Task<int> DeleteExpenseHead(int Id)
        {
            EachKandooraExpenseHead oldEachKandooraExpenseHeads = await _context.EachKandooraExpenseHeads
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();

            oldEachKandooraExpenseHeads.IsDeleted = true;
            EntityEntry<EachKandooraExpenseHead> entity = _context.EachKandooraExpenseHeads.Update(oldEachKandooraExpenseHeads);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<EachKandooraExpenseHead> GetExpenseHead(int Id)
        {
            return await _context.EachKandooraExpenseHeads
                .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<EachKandooraExpenseHead>> GetAllExpenseHead(PagingRequest pagingRequest)
        {
            var data = await _context
                .EachKandooraExpenseHeads
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();

            PagingResponse<EachKandooraExpenseHead> pagingResponse = new PagingResponse<EachKandooraExpenseHead>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<EachKandooraExpenseHead>> SearchExpenseHead(SearchPagingRequest searchPagingRequest)
        {
            var data = await _context
               .EachKandooraExpenseHeads
               .Where(x => (searchPagingRequest.SearchTerm == "" || x.HeadName.Contains(searchPagingRequest.SearchTerm)) && !x.IsDeleted)
               .OrderBy(x => x.DisplayOrder)
               .ToListAsync();

            PagingResponse<EachKandooraExpenseHead> pagingResponse = new PagingResponse<EachKandooraExpenseHead>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<EachKandooraExpenseHead> UpdateExpenseHead(EachKandooraExpenseHead eachKandooraExpenseHead)
        {
            if (await IsExpenseHeadExist(eachKandooraExpenseHead.HeadName))
            {
                throw new BusinessRuleViolationException(StaticValues.KandooraExpenseHeadAlreadyExistError, StaticValues.KandooraExpenseHeadAlreadyExistMessage);
            }

            EntityEntry<EachKandooraExpenseHead> oldProduct = _context.Update(eachKandooraExpenseHead);
            oldProduct.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldProduct.Entity;
        }
        public async Task<EachKandooraExpense> GetExpense(int Id)
        {
            return await _context.EachKandooraExpenses
                .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<EachKandooraExpense>> GetAllExpense(PagingRequest pagingRequest)
        {
            var data = await _context
               .EachKandooraExpenses
               .Where(x => !x.IsDeleted)
               .OrderByDescending(x => x.KandooraHeadId)
               .ToListAsync();

            PagingResponse<EachKandooraExpense> pagingResponse = new PagingResponse<EachKandooraExpense>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<decimal> GetTotalOfExpense()
        {
            return await _context.EachKandooraExpenses.SumAsync(x => x.Amount);
        }

        private async Task<bool> IsExpenseHeadExist(string headName)
        {
            return await _context.EachKandooraExpenseHeads.Where(x => x.HeadName.Equals(headName)).CountAsync() > 0;
        }
    }
}
