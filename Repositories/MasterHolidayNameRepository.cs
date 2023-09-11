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
    public class MasterHolidayNameRepository : IMasterHolidayNameRepository
    {
        private readonly MKExpressDbContext _context;
        public MasterHolidayNameRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<MasterHolidayName> Add(MasterHolidayName masterHolidayName)
        {
            var oldType = await _context.MasterHolidayNames.Where(x => x.Value == masterHolidayName.Value && x.Code == masterHolidayName.Code).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayNameAlreadyExistError, StaticValues.HolidayNameAlreadyExistMessage);
            }
            var entity = _context.MasterHolidayNames.Attach(masterHolidayName);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            MasterHolidayName masterHolidayName = await _context.MasterHolidayNames
               .Where(mht => mht.Id == Id)
               .FirstOrDefaultAsync();
            if (masterHolidayName == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterHolidayName.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterHolidayName.IsDeleted = true;
            var entity = _context.MasterHolidayNames.Update(masterHolidayName);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterHolidayName> Get(int Id)
        {
            return await _context.MasterHolidayNames
                 .Include(x => x.HolidayType)
                .Where(customer => customer.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterHolidayName>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterHolidayNames
                .Include(x => x.HolidayType)
               .Where(x => !x.IsDeleted)
               .OrderBy(x => x.Value)
               .ToListAsync();
            PagingResponse<MasterHolidayName> pagingResponse = new PagingResponse<MasterHolidayName>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<MasterHolidayName>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterHolidayNames
                .Include(x => x.HolidayType)
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm.Contains(string.Empty) ||
                        searchTerm.Contains(mht.Value))
                    )
                .OrderBy(x => x.Value)
                    .ToListAsync();
            PagingResponse<MasterHolidayName> pagingResponse = new PagingResponse<MasterHolidayName>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MasterHolidayName> Update(MasterHolidayName entity)
        {
            var oldType = await _context.MasterHolidayNames.Where(x => x.Value == entity.Value || x.Code == entity.Code).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayTypeAlreadyExistError, StaticValues.HolidayTypeAlreadyExistMessage);
            }
            EntityEntry<MasterHolidayName> oldMasterHolidayType = _context.Update(entity);
            oldMasterHolidayType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMasterHolidayType.Entity;
        }
    }
}
