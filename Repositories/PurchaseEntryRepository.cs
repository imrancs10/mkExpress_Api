using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class PurchaseEntryRepository : IPurchaseEntryRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IConfiguration _configuration;
        public PurchaseEntryRepository(MKExpressDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<PurchaseEntry> Add(PurchaseEntry purchaseEntry)
        {
            var entity = _context.PurchaseEntries.Attach(purchaseEntry);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            PurchaseEntry purchaseEntry = await _context.PurchaseEntries
              .Where(pe => pe.Id == Id)
              .FirstOrDefaultAsync();
            purchaseEntry.IsDeleted = true;
            EntityEntry<PurchaseEntry> entity = _context.PurchaseEntries.Update(purchaseEntry);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<PurchaseEntry> Get(int id)
        {
            return await _context.PurchaseEntries
                 .Include(x => x.Supplier)
                   .Include(x => x.PurchaseEntryDetails).ThenInclude(x => x.Product)
                .Where(pe => pe.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<PurchaseEntry>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context
               .PurchaseEntries
               .Include(x => x.Supplier)
                   .Include(x => x.PurchaseEntryDetails).ThenInclude(x => x.Product)
               .Where(x => !x.IsDeleted)
               .OrderByDescending(x => x.PurchaseNo)
               .ToListAsync();

            PagingResponse<PurchaseEntry> pagingResponse = new PagingResponse<PurchaseEntry>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<int> GetPurchaseNo()
        {
            int defaultOrderNo = _configuration.GetValue<int>("PurchaseNoStartFrom");
            PurchaseEntry purchaseEntry = await _context.PurchaseEntries.OrderByDescending(x => x.PurchaseNo).FirstOrDefaultAsync();
            return purchaseEntry == null ? defaultOrderNo : purchaseEntry.PurchaseNo + 1;
        }

        public async Task<PagingResponse<PurchaseEntry>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.PurchaseEntries
                .Include(x => x.Supplier)
                   .Include(x => x.PurchaseEntryDetails).ThenInclude(x => x.Product)
                .Where(pe => !pe.IsDeleted && (
                        searchTerm.Equals(string.Empty) ||
                        pe.Supplier.Contact.Contains(searchTerm) ||
                        pe.InvoiceNo.Contains(searchTerm) ||
                        pe.Supplier.CompanyName.Contains(searchTerm) ||
                         pe.Supplier.City.Contains(searchTerm) ||
                        pe.PurchaseNo.Equals(searchTerm))
                    )
                .OrderByDescending(x => x.PurchaseNo)
                .ToListAsync();
            PagingResponse<PurchaseEntry> pagingResponse = new PagingResponse<PurchaseEntry>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PurchaseEntry> Update(PurchaseEntry purchaseEntry)
        {
            var oldPurchaseEntryDetails = await _context.PurchaseEntries.Include(x=>x.PurchaseEntryDetails).Where(x => x.Id == purchaseEntry.Id).FirstOrDefaultAsync();

            _context.PurchaseEntryDetails.RemoveRange(oldPurchaseEntryDetails.PurchaseEntryDetails);
            _context.PurchaseEntries.Remove(oldPurchaseEntryDetails);
            if (await _context.SaveChangesAsync() > 0)
            {
               // _context.PurchaseEntries.Remove(purchaseEntry);
                //await _context.SaveChangesAsync();
                purchaseEntry.Id = 0;
                foreach (PurchaseEntryDetail purchaseEntryDetail in purchaseEntry.PurchaseEntryDetails)
                {
                    purchaseEntryDetail.Id = 0;
                }
                return await Add(purchaseEntry);
            }
            return purchaseEntry;
        }
    }
}
