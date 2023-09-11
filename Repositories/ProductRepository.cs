using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
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
    public class ProductRepository : IProductRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IConfiguration _configuration;
        public ProductRepository(MKExpressDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<Product> Add(Product product)
        {
            product.SizeId = product.SizeId != 0 ? product.SizeId : null;
            product.WidthId = product.WidthId == 0 ? null : product.WidthId;
            var entity = _context.Products.Attach(product);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> Delete(int productId)
        {
            Product product = await _context.Products
                .Where(pro => pro.Id == productId)
                .FirstOrDefaultAsync();
            if (product == null)
            {
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            }
            product.IsDeleted = true;

            var entity = _context.Products.Update(product);
            entity.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public async Task<Product> Get(int productId)
        {
            return await _context.Products
                .Include(x => x.Size)
                .Include(x => x.FabricWidth)
                .Include(x => x.ProductType)
                .Where(pro => pro.Id == productId)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<PagingResponse<Product>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.Products
                .Include(x => x.Size)
                .Include(x => x.FabricWidth)
                .Include(x => x.ProductType)
                .Where(x => !x.IsDeleted)
               .OrderBy(x => x.ProductName)
               .ToListAsync();

            PagingResponse<Product> pagingResponse = new PagingResponse<Product>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<PagingResponse<Product>> Search(SearchPagingRequest searchPagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(searchPagingRequest.SearchTerm) ? string.Empty : searchPagingRequest.SearchTerm.ToLower();
            var data = await _context.Products
                .Include(x => x.Size)
                .Include(x => x.FabricWidth)
                .Include(x => x.ProductType)
                .Where(pro => !pro.IsDeleted &&
                        searchTerm.Equals(string.Empty)
                    )
                    .ToListAsync();
            PagingResponse<Product> pagingResponse = new PagingResponse<Product>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                Data = data.Skip(searchPagingRequest.PageSize * (searchPagingRequest.PageNo - 1)).Take(searchPagingRequest.PageSize).ToList(),
                TotalRecords = data.Count
            };
            return pagingResponse;
        }

        public async Task<Product> Update(Product product)
        {
            product.SizeId = product.SizeId != 0 ? product.SizeId : null;
            product.WidthId = product.WidthId == 0 ? null : product.WidthId;
            EntityEntry<Product> oldProduct = _context.Update(product);
            oldProduct.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldProduct.Entity;
        }
    }
}
