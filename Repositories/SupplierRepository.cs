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
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly MKExpressDbContext _context;
        public SupplierRepository(MKExpressDbContext context)
        {
            _context = context;
        }
        public async Task<Supplier> Add(Supplier supplier)
        {
            var oldRecord = await _context.Suppliers
                .Where(x => x.CompanyName.Equals(supplier.CompanyName))
                .CountAsync();
            if (oldRecord > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage("company name"));
            }
            var entity = _context.Suppliers.Attach(supplier);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int supplierId)
        {
            Supplier supplier = await _context.Suppliers
               .Where(supp => supp.Id == supplierId)
               .FirstOrDefaultAsync();

            if (supplier == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }

            supplier.IsDeleted = true;
            var entity = _context.Suppliers.Update(supplier);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Supplier> Get(int supplierId)
        {
            return await _context.Suppliers
                .Where(supp => supp.Id == supplierId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<Supplier>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.Suppliers
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.CompanyName)
                .ToListAsync();

            PagingResponse<Supplier> pagingResponse = new PagingResponse<Supplier>()
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

        public async Task<PagingResponse<Supplier>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Suppliers
                .Where(supp => !supp.IsDeleted &&
                        (supp.City.Contains(searchTerm) ||
                        supp.CompanyName.Contains(searchTerm) ||
                         supp.Address.Contains(searchTerm) ||
                          supp.Title.Contains(searchTerm) ||
                           supp.Contact.Contains(searchTerm))
                    )
                .OrderBy(x => x.CompanyName)
                    .ToListAsync();
            PagingResponse<Supplier> pagingResponse = new PagingResponse<Supplier>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<Supplier> Update(Supplier supplier)
        {
            EntityEntry<Supplier> oldSupp = _context.Update(supplier);
            oldSupp.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldSupp.Entity;
        }
    }
}
