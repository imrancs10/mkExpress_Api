using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository.IRepository;

namespace MKExpress.API.Repository
{
    public class ThirdPartyCourierRepository : IThirdPartyCourierRepository
    {
        private readonly MKExpressContext _context;
        public ThirdPartyCourierRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<ThirdPartyCourierCompany> Add(ThirdPartyCourierCompany request)
        {
            var oldCompany = await _context.ThirdPartyCourierCompanies
                .Where(x => x.Name.Trim() == request.Name || x.Email.Trim() == request.Email.Trim()).CountAsync();
            if (oldCompany > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.ErrorType_CustomerAlreadyExist, StaticValues.Error_CustomerAlreadyExist);
            }
            var entity = _context.ThirdPartyCourierCompanies.Attach(request);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<bool> AddShipmentToThirdParty(List<ThirdPartyShipmentRequest> request)
        {
           _context.AddRange(request);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> Delete(Guid thirdPartyCourierId)
        {
            ThirdPartyCourierCompany thirdPartyCourier = await _context.ThirdPartyCourierCompanies
               .Where(customer => customer.Id == thirdPartyCourierId)
               .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            if (thirdPartyCourier.IsDeleted)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyDeletedError, StaticValues.RecordAlreadyDeletedMessage);
            }
            thirdPartyCourier.IsDeleted = true;
            var entity = _context.ThirdPartyCourierCompanies.Update(thirdPartyCourier);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<ThirdPartyCourierCompany> Get(Guid Id)
        {
            return await _context.ThirdPartyCourierCompanies
                 .Where(x => x.Id == Id)
                 .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<ThirdPartyCourierCompany>> GetAll(PagingRequest pagingRequest)
        {
            var data = _context.ThirdPartyCourierCompanies
            .Where(x => !x.IsDeleted && (pagingRequest.FetchAll || x.IsActive)) 
               .OrderBy(x => x.Name)
               .AsQueryable();
            PagingResponse<ThirdPartyCourierCompany> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<List<ThirdPartyShipment>> GetShipments(Guid thirdPartyId, DateTime fromDate, DateTime toDate)
        {
            return await _context.ThirdPartyShipments
                .Where(x => x.ThirdPartyCourierCompanyId == thirdPartyId &&
                !x.IsDeleted &&
                x.AssignAt.Date >= fromDate.Date &&
                x.AssignAt <= toDate.Date)
                .OrderByDescending(x => x.CreatedBy)
                .ToListAsync();
        }

        public async Task<PagingResponse<ThirdPartyCourierCompany>> Search(SearchPagingRequest pagingRequest)
        {
            var searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? "" : pagingRequest.SearchTerm;
            var data = _context.ThirdPartyCourierCompanies
           .Where(x => !x.IsDeleted && (searchTerm == "" ||
                    x.Name.Contains(searchTerm) ||
                    x.Email.Contains(searchTerm) ||
                    x.Mobile.Contains(searchTerm) ||
                    x.Contact.Contains(searchTerm) ||
                    x.TrackingUrl.Contains(searchTerm))
                    )
              .OrderBy(x => x.Name)
              .AsQueryable();
            PagingResponse<ThirdPartyCourierCompany> pagingResponse = new()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = await data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToListAsync(),
                TotalRecords = await data.CountAsync()
            };
            return pagingResponse;
        }

        public async Task<ThirdPartyCourierCompany> Update(ThirdPartyCourierCompany entity)
        {
            EntityEntry<ThirdPartyCourierCompany> oldCompany = _context.Update(entity);
            oldCompany.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldCompany.Entity;
        }
    }
}
