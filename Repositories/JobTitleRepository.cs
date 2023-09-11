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
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly MKExpressDbContext _context;

        public JobTitleRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<MasterJobTitle> Add(MasterJobTitle masterJobTitle)
        {
            var oldData = await _context.MasterJobTitles
                .Where(x => !x.IsDeleted &&
                 x.Value.Equals(masterJobTitle.Value))
                .CountAsync();
            if (oldData > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage(masterJobTitle.Value));
            }
            var entity = _context.MasterJobTitles.Attach(masterJobTitle);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<MasterJobTitle> Update(MasterJobTitle masterJobTitle)
        {
            EntityEntry<MasterJobTitle> oldJobTitle = _context.Update(masterJobTitle);
            oldJobTitle.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldJobTitle.Entity;
        }

        public async Task<int> Delete(int masterJobTitleId)
        {
            MasterJobTitle masterJobTitle = await _context.MasterJobTitles
                .Where(mjt => mjt.Id == masterJobTitleId)
                .FirstOrDefaultAsync();
            if (masterJobTitle == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterJobTitle.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterJobTitle.IsDeleted = true;
            var entity = _context.MasterJobTitles.Update(masterJobTitle);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterJobTitle> Get(int masterJobTitleId)
        {
            return await _context.MasterJobTitles
                .Where(mjt => mjt.Id == masterJobTitleId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterJobTitle>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterJobTitles
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Value)
                .ToListAsync();

            PagingResponse<MasterJobTitle> pagingResponse = new PagingResponse<MasterJobTitle>()
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

        public async Task<PagingResponse<MasterJobTitle>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterJobTitles
                .Where(mdc => !mdc.IsDeleted &&
                    searchTerm == string.Empty ||
                    mdc.Value.Contains(searchTerm)
                )
                .OrderBy(x => x.Value)
                .ToListAsync();
            PagingResponse<MasterJobTitle> pagingResponse = new PagingResponse<MasterJobTitle>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data
                    .Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1))
                    .Take(searchPagingRequest.PageSize)
                    .ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }
    }
}