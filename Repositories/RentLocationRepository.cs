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
    public class RentLocationRepository : IRentLocationRepository
    {
        private readonly MKExpressDbContext _context;
        public RentLocationRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<RentLocation> Add(RentLocation rentLocation)
        {
            var oldType = await _context.RentLocations.Where(x => x.LocationName == rentLocation.LocationName).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RentLocationAlreadyExistError, StaticValues.RentLocationAlreadyExistMessage);
            }
            var entity = _context.RentLocations.Attach(rentLocation);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            RentLocation rentLocation = await _context.RentLocations
              .Where(mht => mht.Id == Id)
              .FirstOrDefaultAsync();
            if (rentLocation == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (rentLocation.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            rentLocation.IsDeleted = true;
            var entity = _context.RentLocations.Update(rentLocation);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<RentLocation> Get(int Id)
        {
            return await _context.RentLocations
                .Where(rentLoc => rentLoc.Id == Id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<RentLocation>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.RentLocations
               .Where(x => !x.IsDeleted)
               .OrderBy(x => x.LocationName)
               .ToListAsync();
            PagingResponse<RentLocation> pagingResponse = new PagingResponse<RentLocation>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<RentLocation>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.RentLocations
                .Where(mht => !mht.IsDeleted &&
                        (searchTerm.Contains(string.Empty) ||
                        searchTerm.Contains(mht.LocationName) ||
                         searchTerm.Contains(mht.Address)||
                          searchTerm.Contains(mht.Remark))
                    )
                .OrderBy(x => x.LocationName)
                    .ToListAsync();
            PagingResponse<RentLocation> pagingResponse = new PagingResponse<RentLocation>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<RentLocation> Update(RentLocation entity)
        {
            var oldType = await _context.RentLocations.Where(x => x.LocationName == entity.LocationName && entity.Id!=x.Id).CountAsync();
            if (oldType > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.HolidayTypeAlreadyExistError, StaticValues.HolidayTypeAlreadyExistMessage);
            }
            EntityEntry<RentLocation> oldMasterHolidayType = _context.Update(entity);
            oldMasterHolidayType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldMasterHolidayType.Entity;
        }
    }
}
