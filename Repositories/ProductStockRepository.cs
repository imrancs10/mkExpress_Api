using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly MKExpressDbContext _context;
        const int CRYSTAL_PER_PACKET = 1440;
        public ProductStockRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewStocks(List<ProductStock> productStocks)
        {
            // var productIds = productStocks.Select(x => x.ProductId).ToList();
            var oldProducts = await ExistingProductIds(productStocks);
            //var oldProductIds = oldProducts.Select(x => x.ProductId).ToList();
            // var freshProductIds = productIds.Where(x => !oldProductIds.Any(x2 => x2 == x)).ToList();
            var notAddedStocks = new List<ProductStock>();

            oldProducts.ForEach(res =>
            {
                res.AvailableQty += productStocks.Where(x => x.ProductId == res.ProductId).First().AvailableQty;
            });

            productStocks.ForEach(res =>
            {
                if (oldProducts.Where(x => x.ProductId == res.ProductId).Count() == 0)
                {
                    notAddedStocks.Add(res);
                }
            });
            _context.UpdateRange(oldProducts);
            await _context.ProductStocks.AddRangeAsync(notAddedStocks);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DecreaseStocks(List<ProductStock> productStocks)
        {
            var productIds = productStocks.Select(x => x.ProductId).ToList();
            var oldProducts = await ExistingProductIds(productStocks);
            oldProducts.ForEach(res =>
            {
                res.AvailableQty -= productStocks.Where(x => x.ProductId == res.ProductId).First().AvailableQty;
            });
            _context.UpdateRange(oldProducts);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductStock>> GetCrystals()
        {
            return await _context.ProductStocks
                .Include(x => x.Product)
                .ThenInclude(x => x.ProductType)
                .Include(x => x.Product)
                .ThenInclude(x => x.Size)
                 .Include(x => x.Product)
                .ThenInclude(x => x.FabricWidth)
                .Where(x => !x.IsDeleted && x.Product.ProductType.Value.ToLower().Contains("crystal"))
                .ToListAsync();
        }

        public async Task<int> SaveOrderUsedCrystals(List<OrderUsedCrystal> orderUsedCrystals)
        {
            _context.OrderUsedCrystals.AttachRange(orderUsedCrystals);
           var trans= await _context.Database.BeginTransactionAsync();
            if(await _context.SaveChangesAsync()>0)
            {
                if(await DecreaseStocks(orderUsedCrystals.First().ProductStockId, orderUsedCrystals.First().UsedQty)>0)
                {
                   await trans.CommitAsync();
                    return 1;
                }
            }
            return 0;

        }

        public async Task<int> IncreaseStocks(List<ProductStock> productStocks)
        {
            var productIds = productStocks.Select(x => x.ProductId).ToList();
            var oldProducts = await ExistingProductIds(productStocks);
            oldProducts.ForEach(res =>
            {
                res.AvailableQty += productStocks.Where(x => x.ProductId == res.ProductId).First().AvailableQty;
            });
            _context.UpdateRange(oldProducts);
            return await _context.SaveChangesAsync();
        }

        private async Task<List<ProductStock>> ExistingProductIds(List<ProductStock> productStocks)
        {
            var data = await _context.ProductStocks
                 .Where(x => !x.IsDeleted)
                 .ToListAsync();
            return data.Where(x => productStocks.Contains(x, new BoxEqualityComparer())).ToList();
        }

        public async Task<List<OrderUsedCrystal>> GetOrderUsedCrystals(int orderDetailId)
        {
            return await _context.OrderUsedCrystals
                .Include(x=>x.ProductStock)
                .ThenInclude(x=>x.Product)
                .ThenInclude(x => x.Size)
                .Include(x=>x.ProductStock)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.FabricWidth)
                .Include(x => x.ProductStock)
                .Where(x => x.OrderDetailId == orderDetailId).ToListAsync();
        }

        public async Task<int> DecreaseStocks(int productStockId, int qty)
        {
            var oldData = await _context.ProductStocks.Include(x => x.Product).ThenInclude(x => x.ProductType).Where(x => x.Id == productStockId).FirstOrDefaultAsync();
            if (oldData != null)
            {

                if (oldData.Product.ProductType.Code.IndexOf("crystal") > -1)
                {
                    int qt = qty / CRYSTAL_PER_PACKET;
                    int remainingPieces = qty % CRYSTAL_PER_PACKET;
                    qt = remainingPieces >= (CRYSTAL_PER_PACKET / 2) ? qt + 1 : qt;
                    oldData.UsedQty += qt;
                    oldData.UsedPiece += qty;
                }
                else
                    oldData.UsedQty += qty;
                var entity = _context.ProductStocks.Attach(oldData);
                entity.State = EntityState.Modified;
                return await _context.SaveChangesAsync();

            }
            return 0;
        }

        public async Task<int> IncreaseStocks(int productStockId, int qty)
        {
            var oldData = await _context.ProductStocks.Include(x => x.Product).ThenInclude(x => x.ProductType).Where(x => x.Id == productStockId).FirstOrDefaultAsync();
            if (oldData != null)
            {

                if (oldData.Product.ProductType.Code.IndexOf("crystal") > -1)
                {
                    int qt = qty / CRYSTAL_PER_PACKET;
                    int remainingPieces = qty % CRYSTAL_PER_PACKET;
                    qt = remainingPieces >= (CRYSTAL_PER_PACKET / 2) ? qt + 1 : qt;
                    oldData.UsedQty -= qt;
                    oldData.UsedPiece -= qty;
                }
                else
                    oldData.UsedQty -= qty;
                var entity = _context.ProductStocks.Attach(oldData);
                entity.State = EntityState.Modified;
                return await _context.SaveChangesAsync();

            }
            return 0;
        }

        public async Task<List<OrderUsedCrystal>> GetOrderUsedCrystalsByEmployee(int empId)
        {
            return await _context.OrderUsedCrystals.Where(x => x.EmployeeId == empId).ToListAsync();
        }
    }

    class BoxEqualityComparer : IEqualityComparer<ProductStock>
    {
        public bool Equals(ProductStock b1, ProductStock b2)
        {
            if (b2 == null && b1 == null)
                return true;
            else if (b1 == null || b2 == null)
                return false;
            else if (b1.ProductId == b2.ProductId)
                return true;
            else
                return false;
        }

        public int GetHashCode(ProductStock bx)
        {
            int hCode = bx.ProductId;
            return hCode.GetHashCode();
        }
    }
}
