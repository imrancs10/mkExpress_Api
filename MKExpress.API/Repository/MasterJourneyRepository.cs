using DocumentFormat.OpenXml.Office2010.Excel;
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
    public class MasterJourneyRepository : IMasterJourneyRepository
    {
        private readonly MKExpressContext _context;
        public MasterJourneyRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<MasterJourney> Add(MasterJourney request)
        {
            request.Id = Guid.NewGuid();
            request.MasterJourneyDetails.ForEach(res =>
            {
                res.MasterJourneyId = request.Id;
            });
            var entity = _context.Add(request);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(Guid Id)
        {
            var oldData = await _context.MasterJouneys
                .Where(x => x.Id == Id && !x.IsDeleted)
                .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);
            oldData.IsDeleted = true;
            oldData.MasterJourneyDetails.ForEach(_ => _.IsDeleted = true);
            _context.Attach(oldData);
            return await _context.SaveChangesAsync();
        }

        public async Task<MasterJourney> Get(Guid Id)
        {
            return await _context.MasterJouneys
                   .Include(x => x.MasterJourneyDetails)
                .ThenInclude(x => x.SubStation)
                .Include(x => x.FromStation)
                .Include(x => x.ToStation)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.FromStation.Value)
                .Where(x => x.Id == Id && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<MasterJourney>> GetAll(PagingRequest pagingRequest)
        {
            var data = _context.MasterJouneys
                .Include(x => x.MasterJourneyDetails)
                .ThenInclude(x => x.SubStation)
                .Include(x => x.FromStation)
                .Include(x => x.ToStation)
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.FromStation.Value)
                .ThenBy(x => x.ToStation.Value)
                .AsQueryable();

            PagingResponse<MasterJourney> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<MasterJourney>> Search(SearchPagingRequest pagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var data = _context.MasterJouneys
                 .Include(x => x.MasterJourneyDetails)
                 .ThenInclude(x => x.SubStation)
                 .Include(x => x.FromStation)
                 .Include(x => x.ToStation)
                 .Where(x => !x.IsDeleted && (
                 x.FromStation.Value.Contains(searchTerm) ||
                  x.FromStation.Code.Contains(searchTerm) ||
                   x.ToStation.Value.Contains(searchTerm) ||
                  x.ToStation.Code.Contains(searchTerm) ||
                    x.MasterJourneyDetails.Where(y => y.SubStation.Value.Contains(searchTerm) || y.SubStation.Code.Contains(searchTerm)).Any())
                 )
                 .OrderBy(x => x.FromStation.Value)
                 .ThenBy(x => x.ToStation.Value)
                 .AsQueryable();

            PagingResponse<MasterJourney> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data
                .Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1))
                .Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<MasterJourney> Update(MasterJourney request)
        {
            var oldData = await _context.MasterJouneys
               .Where(x => x.Id == request.Id && !x.IsDeleted)
               .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound, StaticValues.Error_RecordNotFound);
            var trans=_context.Database.BeginTransaction();
            _context.masterJourneyDetails.RemoveRange(oldData.MasterJourneyDetails);
            if(await _context.SaveChangesAsync()>0)
            {
                oldData.MasterJourneyDetails=request.MasterJourneyDetails;
                oldData.FromStationId = request.FromStationId;
                oldData.ToStationId= request.ToStationId;
                
                var entity = _context.Attach(oldData);
                entity.State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return entity.Entity;
                }
            }
            trans.Rollback();
            return default;
         
        }
    }
}
