using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class LogisticRegionRepository : ILogisticRegionRepository
    {
        private readonly MKExpressContext _context;

        public LogisticRegionRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<LogisticRegion> Add(LogisticRegion request)
        {
            var oldRegion = await _context.LogisticRegions.Where(x => !x.IsDeleted &&
            x.CountryId == request.CountryId &&
             x.ProvinceId == request.ProvinceId &&
              x.DistrictId == request.DistrictId &&
               x.CityId == request.CityId &&
                x.StationId == request.StationId &&
                 x.ParentStationId == request.ParentStationId
            ).CountAsync();

            if (oldRegion > 0) throw new BusinessRuleViolationException(StaticValues.Error_LogisiticReasonAlreadyExist, StaticValues.Message_LogisiticReasonAlreadyExist);
            
            var entity = _context.LogisticRegions.Attach(request);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(Guid logisticRegionId)
        {
            LogisticRegion logisticRegion = await _context.LogisticRegions
                .Where(x => x.Id == logisticRegionId)
                .FirstOrDefaultAsync();

            if (logisticRegion == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (logisticRegion.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            logisticRegion.IsDeleted = true;
            var entity = _context.LogisticRegions.Update(logisticRegion);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<LogisticRegion> Get(Guid Id)
        {
            return await _context
                .LogisticRegions
                .Include(x=>x.City)
                 .Include(x => x.Country)
                  .Include(x => x.Province)
                   .Include(x => x.District)
                    .Include(x => x.Station)
                     .Include(x => x.ParentStation)
                .Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<LogisticRegion>> GetAll(PagingRequest pagingRequest)
        {
                var data = _context.LogisticRegions
                       .Include(x => x.City)
                     .Include(x => x.Country)
                      .Include(x => x.Province)
                       .Include(x => x.District)
                        .Include(x => x.Station)
                         .Include(x => x.ParentStation)
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.Country.Value)
                    .AsQueryable();
                PagingResponse<LogisticRegion> pagingResponse = new()
                {
                    PageNo = pagingRequest.PageNo,
                    PageSize = pagingRequest.PageSize,
                    Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToListAsync(),
                    TotalRecords = await data.CountAsync()
                };
                return pagingResponse;
        }

        public async Task<PagingResponse<LogisticRegion>> Search(SearchPagingRequest pagingRequest)
        {
            pagingRequest.SearchTerm=string.IsNullOrEmpty(pagingRequest.SearchTerm)?string.Empty:pagingRequest.SearchTerm.ToLower();
            var data = _context.LogisticRegions
                 .Include(x => x.City)
               .Include(x => x.Country)
                .Include(x => x.Province)
                 .Include(x => x.District)
                  .Include(x => x.Station)
                   .Include(x => x.ParentStation)
              .Where(x => !x.IsDeleted && (
              x.Country.Value.ToLower().Contains(pagingRequest.SearchTerm) ||
               x.City.Value.ToLower().Contains(pagingRequest.SearchTerm) ||
                x.District.Value.ToLower().Contains(pagingRequest.SearchTerm) ||
                 x.Province.Value.ToLower().Contains(pagingRequest.SearchTerm) ||
                  x.Station.Value.ToLower().Contains(pagingRequest.SearchTerm) ||
                   x.ParentStation.Value.ToLower().Contains(pagingRequest.SearchTerm)
              ))
              .OrderBy(x => x.Country.Value)
              .AsQueryable();
            PagingResponse<LogisticRegion> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<LogisticRegion> Update(LogisticRegion request)
        {
            LogisticRegion logisticRegion = await _context.LogisticRegions
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync();

            if (logisticRegion == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            if (logisticRegion.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            logisticRegion.CityId = request.CityId;
            logisticRegion.ProvinceId = request.ProvinceId;
            logisticRegion.ParentStationId = request.ParentStationId;
            logisticRegion.DistrictId = request.DistrictId;
            logisticRegion.StationId = request.StationId;
            logisticRegion.CountryId = request.CountryId;
            var entity = _context.LogisticRegions.Update(logisticRegion);
            entity.State = EntityState.Modified;
            if (await _context.SaveChangesAsync() > 0)
                return entity.Entity;
            return new LogisticRegion();
        }
    }
}
