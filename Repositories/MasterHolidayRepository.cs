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
    public class MasterHolidayRepository : IMasterHolidayRepository
    {
        private readonly MKExpressDbContext _context;
        public MasterHolidayRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<MasterHoliday> Add(MasterHoliday masterHoliday)
        {
            var oldType = await _context.MasterHolidays.Where(x => (x.HolidayNameId == masterHoliday.HolidayNameId && x.Year == masterHoliday.Year) || (x.HolidayNameId == masterHoliday.HolidayNameId && x.HolidayDate == masterHoliday.HolidayDate)).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayAlreadyExistError, StaticValues.HolidayAlreadyExistMessage);
            }
            var entity = _context.MasterHolidays.Attach(masterHoliday);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            MasterHoliday masterHoliday = await _context.MasterHolidays
              .Where(mht => mht.Id == Id)
              .FirstOrDefaultAsync();
            if (masterHoliday == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (masterHoliday.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            masterHoliday.IsDeleted = true;
            var entity = _context.MasterHolidays.Update(masterHoliday);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterHoliday> Get(int Id)
        {
            return await _context.MasterHolidays
                .Include(x => x.HolidayName)
                .ThenInclude(x => x.HolidayType)
                .Where(mh => mh.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterHoliday>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.MasterHolidays
                .Include(x => x.HolidayName)
                .ThenInclude(x => x.HolidayType)
               .Where(x => !x.IsDeleted)
               .OrderBy(x => x.HolidayDate)
               .ToListAsync();
            PagingResponse<MasterHoliday> pagingResponse = new PagingResponse<MasterHoliday>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MasterHoliday> GetHolidayByDate(DateTime holidayDate)
        {
            return await _context.MasterHolidays
                .Include(x => x.HolidayName)
                .ThenInclude(x => x.HolidayType)
                .Where(x => !x.IsDeleted && x.HolidayDate == holidayDate).FirstOrDefaultAsync();
        }

        public async Task<List<MasterHoliday>> GetHolidayByMonthYear(int month, int year)
        {
            return await _context.MasterHolidays
                 .Include(x => x.HolidayName)
                 .ThenInclude(x => x.HolidayType)
                 .Where(x => !x.IsDeleted && x.HolidayDate.Month == month && x.HolidayDate.Year == year).ToListAsync();
        }

        public async Task<bool> IsHoliday(System.DateTime holidayDate)
        {
            return await _context.MasterHolidays.Where(x => !x.IsDeleted && x.HolidayDate == holidayDate).CountAsync() > 0;
        }

        public async Task<PagingResponse<MasterHoliday>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.MasterHolidays
                .Include(x => x.HolidayName)
                .ThenInclude(x => x.HolidayType)
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm.Equals(string.Empty) ||
                        searchTerm.Contains(mht.HolidayName.Value) ||
                        searchTerm.Contains(mht.HolidayName.HolidayType.Value))
                    )
                .OrderBy(x => x.HolidayDate)
                    .ToListAsync();
            PagingResponse<MasterHoliday> pagingResponse = new PagingResponse<MasterHoliday>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<MasterHoliday> Update(MasterHoliday masterHoliday)
        {
            var oldType = await _context.MasterHolidays.Where(x => x.HolidayNameId == masterHoliday.HolidayNameId && x.Year == masterHoliday.Year).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayAlreadyExistError, StaticValues.HolidayAlreadyExistMessage);
            }
            EntityEntry<MasterHoliday> oldMasterHolidayType = _context.Update(masterHoliday);
            oldMasterHolidayType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMasterHolidayType.Entity;
        }
    }
}
