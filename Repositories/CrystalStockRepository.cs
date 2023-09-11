using Microsoft.EntityFrameworkCore;
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
    public class CrystalStockRepository : ICrystalStockRepository
    {
        private readonly MKExpressDbContext _context;
        public CrystalStockRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<PagingResponse<CrystalStock>> GetCrystalStockDetails(CrystalStockPagingRequest pagingRequest)
        {
            var data = await _context.CrystalStocks
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Brand)
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Shape)
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Size)
                                    .Where(x => !x.IsDeleted && (
                                    (pagingRequest.ShapeId == 0 || x.MasterCrystal.ShapeId == pagingRequest.ShapeId) &&
                                    (pagingRequest.SizeId == 0 || x.MasterCrystal.SizeId == pagingRequest.SizeId) &&
                                    (pagingRequest.BrandId == 0 || x.MasterCrystal.BrandId == pagingRequest.BrandId)))
                                    .ToListAsync();
            PagingResponse<CrystalStock> pagingResponse = new PagingResponse<CrystalStock>()
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
        public async Task<CrystalStock> GetCrystalStockDetail(int Id)
        {
            return await _context.CrystalStocks
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Brand)
                                      .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Shape)
                                     .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Size)
                                    .Where(x => !x.IsDeleted && x.Id == Id)
                                    .FirstOrDefaultAsync();
        }

        public async Task<List<CrystalStock>> GetCrystalStockAlert(CrystalStockPagingRequest pagingRequest)
        {
            return await _context.CrystalStocks
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Brand)
                                      .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Shape)
                                     .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Size)
                                    .Where(x => x.InStock <= x.MasterCrystal.AlertQty &&
                                    !x.IsDeleted &&
                                     (pagingRequest.ShapeId == 0 || x.MasterCrystal.ShapeId == pagingRequest.ShapeId) &&
                                    (pagingRequest.SizeId == 0 || x.MasterCrystal.SizeId == pagingRequest.SizeId) &&
                                    (pagingRequest.BrandId == 0 || x.MasterCrystal.BrandId == pagingRequest.BrandId))
                                    .ToListAsync();
        }

        public async Task<int> UpdateStock(CrystalStock crystalStock, string reason)
        {
            if (crystalStock == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            var oldStock = await _context.CrystalStocks.Where(x => !x.IsDeleted && x.CrystalId == crystalStock.CrystalId).FirstOrDefaultAsync();
            var trans = _context.Database.BeginTransaction();
            var result = 0;
            var oldPkt = 0;
            var oldPieces = 0;
            if (oldStock == null)
            {
                await _context.CrystalStocks.AddAsync(crystalStock);
                result = await _context.SaveChangesAsync();
            }
            else
            {
                oldPieces = oldStock.BalanceStockPieces;
                oldPkt = oldStock.BalanceStock;
                oldStock.UpdatedAt = DateTime.Now;
                oldStock.BalanceStock = crystalStock.BalanceStock;
                oldStock.BalanceStockPieces = crystalStock.BalanceStockPieces;

                var entity = _context.Attach(oldStock);
                entity.State = EntityState.Modified;
                result = await _context.SaveChangesAsync();
            }
            if (result > 0)
            {
                CrystalStockUpdateHistory crystalStockUpdateHistory = new CrystalStockUpdateHistory()
                {
                    CrystalId = crystalStock.CrystalId,
                    OldPackets = oldStock == null ? 0 : oldPkt,
                    OldPieces = oldStock == null ? 0 : oldPieces,
                    NewPackets = crystalStock.BalanceStock,
                    NewPieces = crystalStock.BalanceStockPieces,
                    Reason = reason,
                    Remark = "Manually updated from stock update page."
                };
                _context.CrystalStockUpdateHistories.Add(crystalStockUpdateHistory);
                var response = await _context.SaveChangesAsync();
                if (response > 0)
                {
                    trans.Commit();
                    return response;
                }
            }
            trans.Rollback();
            return 0;
        }

        public async Task<int> UpdateStock(List<CrystalStock> crystalStocks)
        {
            if (crystalStocks == null || crystalStocks.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            var newCrystalIdsInStock = new List<int>();
            var requestedCrystalIds = crystalStocks
                                        .Select(x => x.CrystalId)
                                        .ToList();

            var oldStock = await _context.CrystalStocks
                                            .Where(x => !x.IsDeleted && requestedCrystalIds.Contains(x.CrystalId))
                                            .ToListAsync();
            if (oldStock.Count == 0)
            {
                await _context.CrystalStocks.AddRangeAsync(crystalStocks);
                return await _context.SaveChangesAsync();
            }
            var existingCrystalIdsInStock = oldStock
                                                .Select(x => x.CrystalId)
                                                .ToList();
            var newStocks = crystalStocks
                                        .Where(x => !existingCrystalIdsInStock.Contains(x.CrystalId))
                                        .ToList();
            newStocks.ForEach(res => res.Id = 0);
            await _context.CrystalStocks.AddRangeAsync(newStocks);
            await _context.SaveChangesAsync();
            var requestStockDic = crystalStocks.ToDictionary(x => x.CrystalId, y => y);
            foreach (var stk in oldStock)
            {
                var temp = requestStockDic[stk.CrystalId];
                stk.InStock += temp.InStock;
                stk.InStockPieces += temp.InStockPieces;
                stk.UpdatedAt = DateTime.Now;
                stk.BalanceStock += temp.BalanceStock;
                stk.BalanceStockPieces += temp.BalanceStockPieces;
            }

            _context.CrystalStocks.AttachRange(oldStock);
            return await _context.SaveChangesAsync();
        }

        public async Task<PagingResponse<CrystalStock>> SearchCrystalStockAlert(SearchPagingRequest pagingRequest)
        {
            var searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower().Trim();
            var data = await _context.CrystalStocks
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Brand)
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Shape)
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Size)
                                    .Where(x => x.BalanceStock < x.MasterCrystal.AlertQty && !x.IsDeleted &&
                                            (searchTerm == string.Empty ||
                                                x.MasterCrystal.Name.Contains(searchTerm) ||
                                                x.MasterCrystal.Brand.Value.Contains(searchTerm) ||
                                                x.MasterCrystal.Shape.Value.Contains(searchTerm) ||
                                                 x.MasterCrystal.Size.Value.Contains(searchTerm) ||
                                                 x.MasterCrystal.AlertQty.ToString().Equals(searchTerm) ||
                                                 x.BalanceStock.ToString().Equals(searchTerm) ||
                                                 x.BalanceStockPieces.ToString().Equals(searchTerm)
                                            )
                                        )
                                        .ToListAsync();
            PagingResponse<CrystalStock> pagingResponse = new PagingResponse<CrystalStock>()
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

        public async Task<PagingResponse<CrystalStock>> SearchCrystalStockDetails(SearchPagingRequest pagingRequest)
        {
            var searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower().Trim();
            var data = await _context.CrystalStocks
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Brand)
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Shape)
                                    .Include(x => x.MasterCrystal)
                                    .ThenInclude(x => x.Size)
                                    .Where(x => !x.IsDeleted &&
                                            (searchTerm == string.Empty ||
                                                x.MasterCrystal.Name.Contains(searchTerm) ||
                                                x.MasterCrystal.Brand.Value.Contains(searchTerm) ||
                                                x.MasterCrystal.Shape.Value.Contains(searchTerm) ||
                                                 x.MasterCrystal.Size.Value.Contains(searchTerm) ||
                                                 x.MasterCrystal.AlertQty.ToString().Equals(searchTerm) ||
                                                 x.BalanceStock.ToString().Equals(searchTerm) ||
                                                 x.BalanceStockPieces.ToString().Equals(searchTerm)
                                            )
                                        )
                                        .ToListAsync();
            PagingResponse<CrystalStock> pagingResponse = new PagingResponse<CrystalStock>()
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

        public async Task<int> DecreaseStock(List<CrystalStock> crystalStocks)
        {
            if (crystalStocks == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            if (crystalStocks.Count == 0)
                return 0;
            var crystalIds = crystalStocks.Select(x => x.CrystalId).ToList();
            var oldStocks = await _context.CrystalStocks.Where(x => !x.IsDeleted && crystalIds.Contains(x.CrystalId)).ToListAsync();

            if (oldStocks.Count == 0)
                return 0;
            var newDataDic = crystalStocks.ToDictionary(x => x.CrystalId, y => y);
            oldStocks.ForEach(res =>
            {
                if (newDataDic.ContainsKey(res.CrystalId))
                {
                    res.OutStock += newDataDic[res.CrystalId].OutStock;
                    res.OutStockPieces += newDataDic[res.CrystalId].OutStockPieces;
                    res.BalanceStock = res.InStock - res.OutStock;
                    res.BalanceStockPieces = res.InStockPieces - res.OutStockPieces;
                    res.UpdatedAt = DateTime.Now;
                }
            });
            _context.AttachRange(oldStocks);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> IncreaseStock(List<CrystalStock> crystalStocks)
        {
            if (crystalStocks == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);
            if (crystalStocks.Count == 0)
                return 0;
            var crystalIds = crystalStocks.Select(x => x.CrystalId).ToList();
            var oldStocks = await _context.CrystalStocks.Where(x => !x.IsDeleted && crystalIds.Contains(x.CrystalId)).ToListAsync();

            if (oldStocks.Count == 0)
                return 0;
            var newDataDic = crystalStocks.ToDictionary(x => x.CrystalId, y => y);
            oldStocks.ForEach(res =>
            {
                if (newDataDic.ContainsKey(res.CrystalId))
                {
                   // res.InStock += newDataDic[res.CrystalId].OutStock;
                    //res.InStockPieces += newDataDic[res.CrystalId].OutStockPieces;
                    res.OutStock -= newDataDic[res.CrystalId].OutStock;
                    res.OutStockPieces-= newDataDic[res.CrystalId].OutStockPieces;
                    res.BalanceStock = res.InStock - res.OutStock;
                    res.BalanceStockPieces = res.InStockPieces - res.OutStockPieces;
                    res.UpdatedAt = DateTime.Now;
                }
            });
            _context.AttachRange(oldStocks);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> IncreaseStock(CrystalStock crystalStock)
        {
            if (crystalStock == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);

            var stocks = new List<CrystalStock>() { crystalStock };
            return await IncreaseStock(stocks);
        }

        public async Task<bool> AddNewCrystalInStockWithZeroQty(int crystalId)
        {
            var oldData = await _context.CrystalStocks
                                        .Where(x => !x.IsDeleted && x.CrystalId == crystalId)
                                        .CountAsync();
            if (oldData > 0)
                return false;

            _context.CrystalStocks.Add(new CrystalStock()
            {
                CrystalId = crystalId,
                BalanceStock = 0,
                BalanceStockPieces = 0,
                InStock = 0,
                InStockPieces = 0,
                OutStock = 0,
                OutStockPieces = 0,
            });
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
