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
    public class MasterHolidayTypeRepository : IMasterHolidayTypeRepository
    {
        private readonly MKExpressDbContext _context;
        public MasterHolidayTypeRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<MasterHolidayType> Add(MasterHolidayType masterHolidayType)
        {
            var oldType = await _context.MasterHolidayTypes.Where(x => x.Value == masterHolidayType.Value || x.Code == masterHolidayType.Code).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayTypeAlreadyExistError, StaticValues.HolidayTypeAlreadyExistMessage);
            }
            var entity = _context.MasterHolidayTypes.Attach(masterHolidayType);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            MasterHolidayType masterHolidayType = await _context.MasterHolidayTypes
               .Where(mht => mht.Id == Id)
               .FirstOrDefaultAsync();
            if (masterHolidayType == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterHolidayType.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterHolidayType.IsDeleted = true;
            var entity = _context.MasterHolidayTypes.Update(masterHolidayType);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterHolidayType> Get(int Id)
        {
            return await _context.MasterHolidayTypes.Where(customer => customer.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterHolidayType>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterHolidayTypes
               .Where(x => !x.IsDeleted)
               .OrderBy(x => x.Value)
               .ToListAsync();
            PagingResponse<MasterHolidayType> pagingResponse = new PagingResponse<MasterHolidayType>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<MasterHolidayType>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterHolidayTypes
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm.Contains(string.Empty) ||
                        searchTerm.Contains(mht.Value))
                    )
                .OrderBy(x => x.Value)
                    .ToListAsync();
            PagingResponse<MasterHolidayType> pagingResponse = new PagingResponse<MasterHolidayType>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MasterHolidayType> Update(MasterHolidayType entity)
        {
            var oldType = await _context.MasterHolidayTypes.Where(x => x.Value == entity.Value && x.Code == entity.Code).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayTypeAlreadyExistError, StaticValues.HolidayTypeAlreadyExistMessage);
            }
            EntityEntry<MasterHolidayType> oldMasterHolidayType = _context.Update(entity);
            oldMasterHolidayType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMasterHolidayType.Entity;
        }
    }
}
