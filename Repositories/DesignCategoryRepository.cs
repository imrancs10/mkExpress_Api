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
    public class DesignCategoryRepository : IDesignCategoryRepository
    {
        private readonly MKExpressDbContext _context;
        public DesignCategoryRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<MasterDesignCategory> Add(MasterDesignCategory masterDesignCategory)
        {
            var oldData = await _context.MasterDesignCategories
                .Where(x => !x.IsDeleted &&
                 x.Value.Equals(masterDesignCategory.Value))
                .CountAsync();
            if (oldData > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage(masterDesignCategory.Value));
            }
            var entity = _context.MasterDesignCategories.Attach(masterDesignCategory);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int masterDesignCategoryId)
        {
            MasterDesignCategory masterDesignCategory = await _context.MasterDesignCategories
               .Where(mdc => mdc.Id == masterDesignCategoryId)
               .FirstOrDefaultAsync();
            if (masterDesignCategory == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterDesignCategory.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterDesignCategory.IsDeleted = true;
            var entity = _context.MasterDesignCategories.Update(masterDesignCategory);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterDesignCategory> Get(int designCategoryId)
        {
            return await _context.MasterDesignCategories
               .Where(mdc => mdc.Id == designCategoryId)
               .AsNoTracking()
               .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterDesignCategory>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterDesignCategories
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Value)
                .ToListAsync();

            PagingResponse<MasterDesignCategory> pagingResponse = new PagingResponse<MasterDesignCategory>()
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

        public async Task<PagingResponse<MasterDesignCategory>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterDesignCategories
                .Where(mdc => !mdc.IsDeleted &&
                        searchTerm == string.Empty ||
                        mdc.Value.Contains(searchTerm)
                    )
                .OrderBy(x => x.Value)
                    .ToListAsync();
            PagingResponse<MasterDesignCategory> pagingResponse = new PagingResponse<MasterDesignCategory>()
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

        public async Task<MasterDesignCategory> Update(MasterDesignCategory masterDesignCategory)
        {
            EntityEntry<MasterDesignCategory> oldMdc = _context.Update(masterDesignCategory);
            oldMdc.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMdc.Entity;
        }
    }
}
