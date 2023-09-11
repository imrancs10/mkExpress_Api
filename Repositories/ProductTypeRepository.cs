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
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly MKExpressDbContext _context;
        public ProductTypeRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<ProductType> Add(ProductType productType)
        {
            if (await _context.ProductTypes.Where(x => x.Value == productType.Value && !x.IsDeleted).CountAsync() > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.RecordAlreadyExistError, StaticValues.RecordAlreadyExistMessage("Product type"));
            }

            var entity = _context.ProductTypes.Attach(productType);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int Id)
        {
            ProductType productType = await _context.ProductTypes
                .Where(pro => pro.Id == Id)
                .FirstOrDefaultAsync();
            if (productType == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }

            productType.IsDeleted = true;
            var entity = _context.ProductTypes.Update(productType);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<ProductType> Get(int Id)
        {
            return await _context.ProductTypes.Where(x => !x.IsDeleted && x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<ProductType>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.ProductTypes
                .Where(x => !x.IsDeleted)
               .OrderBy(x => x.Value)
               .ToListAsync();

            PagingResponse<ProductType> pagingResponse = new PagingResponse<ProductType>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<ProductType>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.ProductTypes
                .Where(pro => !pro.IsDeleted &&
                        searchTerm.Equals(string.Empty)
                    )
                    .ToListAsync();
            PagingResponse<ProductType> pagingResponse = new PagingResponse<ProductType>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<ProductType> Update(ProductType entity)
        {
            EntityEntry<ProductType> oldProduct = _context.Update(entity);
            oldProduct.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldProduct.Entity;
        }
    }
}
