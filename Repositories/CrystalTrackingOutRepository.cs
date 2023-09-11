using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.Web.API.Dto.Response;
using MKExpress.Web.API.Dto.Response.Crystal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class CrystalTrackingOutRepository : ICrystalTrackingOutRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly ICrystalStockRepository _crystalStockRepository;
        private readonly IMapper _mapper;

        public CrystalTrackingOutRepository(MKExpressDbContext context, ICrystalStockRepository crystalStockRepository, IMapper mapper)
        {
            _context = context;
            _crystalStockRepository = crystalStockRepository;
            _mapper = mapper;
        }

        public async Task<int> Add(CrystalTrackingOut crystalTrackingOut)
        {
            if (crystalTrackingOut == null)
                throw new BusinessRuleViolationException(StaticValues.InvalidDataError, StaticValues.InvalidDataMessage);

            if (crystalTrackingOut.CrystalTrackingOutDetails == null || crystalTrackingOut.CrystalTrackingOutDetails.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.CrystalTrackingDetailNotFoundError, StaticValues.CrystalTrackingDetailNotFoundMessage);

            //if (await IsOrderDetailExist(crystalTrackingOut.OrderDetailId))
            //   throw new BusinessRuleViolationException(StaticValues.CrystalTrackingAlreadyExistError, StaticValues.CrystalTrackingAlreadyExistMessage);
            var trans = _context.Database.BeginTransaction();
            if (crystalTrackingOut.Id > 0) // Update tracking Details
            {

                if (await AdjustStockWhileUpdateTracking(crystalTrackingOut.OrderDetailId))
                {
                    crystalTrackingOut.CrystalTrackingOutDetails.ForEach(res =>
                    {
                        res.Id = 0;
                        res.TrackingOutId = crystalTrackingOut.Id;
                    });

                    _context.CrystalTrackingOutDetails.AddRange(crystalTrackingOut.CrystalTrackingOutDetails);
                    var saveResilt = await _context.SaveChangesAsync();
                    if (saveResilt > 0)
                    {
                        var stockList = _mapper.Map<List<CrystalStock>>(crystalTrackingOut.CrystalTrackingOutDetails);
                        var stockResult = await _crystalStockRepository.DecreaseStock(stockList);
                        if (stockResult > 0)
                        {
                            trans.Commit();
                            return saveResilt;
                        }
                    }
                }
                return 0;
            }

            var entity = _context.CrystalTrackingOuts.Add(crystalTrackingOut);
            entity.State = EntityState.Added;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var stockList = _mapper.Map<List<CrystalStock>>(crystalTrackingOut.CrystalTrackingOutDetails);
                var stockResult = await _crystalStockRepository.DecreaseStock(stockList);
                if (stockResult > 0)
                {
                    trans.Commit();
                    return result;
                }
            }
            trans.Rollback();
            return result;
        }

        public async Task<int> Delete(int Id)
        {
            var oldData = await _context.CrystalTrackingOuts
                                        .Include(x => x.CrystalTrackingOutDetails)
                                        .Include(x => x.OrderDetail)
                                            .Where(x => !x.IsDeleted && x.Id == Id)
                                            .FirstOrDefaultAsync();
            if (oldData == null)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            if (oldData.OrderDetail.Status == OrderStatusEnum.Delivered.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);

            oldData.IsDeleted = true;

            oldData.CrystalTrackingOutDetails.ForEach(res => { res.IsDeleted = true; });

            var trans = _context.Database.BeginTransaction();
            var entity = _context.Attach(oldData);
            entity.State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var stock = _mapper.Map<List<CrystalStock>>(oldData.CrystalTrackingOutDetails);
                var stockResult = await _crystalStockRepository.IncreaseStock(stock);
                if (stockResult > 0)
                {
                    trans.Commit();
                    return result;
                }
            }
            trans.Rollback();
            return result;
        }

        public async Task<int> DeleteDetail(int Id)
        {
            var oldData = await _context.CrystalTrackingOutDetails
                                        .Include(x => x.CrystalTrackingOut)
                                        .ThenInclude(x => x.OrderDetail)
                                            .Where(x => !x.IsDeleted && x.Id == Id)
                                            .FirstOrDefaultAsync();
            if (oldData == null)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            if (oldData.CrystalTrackingOut.OrderDetail.Status == OrderStatusEnum.Delivered.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);

            oldData.IsDeleted = true;

            var trans = _context.Database.BeginTransaction();
            var entity = _context.Attach(oldData);
            entity.State = EntityState.Modified;

            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                CrystalStock crystalStock = _mapper.Map<CrystalStock>(oldData);
                var stockResult = await _crystalStockRepository.IncreaseStock(crystalStock);
                if (stockResult > 0)
                {
                    trans.Commit();
                    return result;
                }
            }
            trans.Rollback();
            return result;
        }

        public async Task<CrystalTrackingOut> Get(int Id)
        {
            var oldData = await _context.CrystalTrackingOuts
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Order)
                .Include(x => x.CrystalTrackingOutDetails)
                .ThenInclude(x => x.Crystal)
                .ThenInclude(x => x.Brand)
                .Include(x => x.CrystalTrackingOutDetails)
                .ThenInclude(x => x.Crystal)
                .ThenInclude(x => x.Size)
               .Include(x => x.CrystalTrackingOutDetails)
                .ThenInclude(x => x.Crystal)
                .ThenInclude(x => x.Shape)
                .Include(x => x.Employee)
                .Where(x => !x.IsDeleted && x.Id == Id)
                .FirstOrDefaultAsync();

            if (oldData == null)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);

            oldData.CrystalTrackingOutDetails = oldData.CrystalTrackingOutDetails.Where(x => !x.IsDeleted).ToList();
            return oldData;
        }

        public async Task<PagingResponse<CrystalTrackingOut>> GetAll(PagingRequest pagingRequest)
        {
            var data = await _context.CrystalTrackingOuts
                                .Include(x => x.OrderDetail)
                                .ThenInclude(x => x.Order)
                                .Include(x => x.CrystalTrackingOutDetails)
                                .ThenInclude(x => x.Crystal)
                                .ThenInclude(x => x.Brand)
                                .Include(x => x.CrystalTrackingOutDetails)
                                .ThenInclude(x => x.Crystal)
                                .ThenInclude(x => x.Size)
                                .Include(x => x.CrystalTrackingOutDetails)
                                .ThenInclude(x => x.Crystal)
                                .ThenInclude(x => x.Shape)
                                .Include(x => x.Employee)
                                .Where(x => !x.IsDeleted &&
                                 x.ReleaseDate.Date >= pagingRequest.FromDate.Date &&
                                 x.ReleaseDate.Date <= pagingRequest.ToDate.Date)
                                .OrderByDescending(x => x.ReleaseDate)
                                .ToListAsync();

            data.ForEach(res =>
            {
                res.CrystalTrackingOutDetails = res.CrystalTrackingOutDetails.Where(x => !x.IsDeleted).ToList();
            });

            PagingResponse<CrystalTrackingOut> pagingResponse = new PagingResponse<CrystalTrackingOut>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };
            return pagingResponse;
        }

        public async Task<List<CrystalTrackingOut>> GetByOrderDetailId(int Id)
        {
            return await _context.CrystalTrackingOuts
                                 .Include(x => x.OrderDetail)
                                 .ThenInclude(x => x.Order)
                                 .Include(x => x.CrystalTrackingOutDetails)
                                 .ThenInclude(x => x.Crystal)
                                 .ThenInclude(x => x.Brand)
                                 .Include(x => x.CrystalTrackingOutDetails)
                                 .ThenInclude(x => x.Crystal)
                                 .ThenInclude(x => x.Size)
                                 .Include(x => x.CrystalTrackingOutDetails)
                                 .ThenInclude(x => x.Crystal)
                                 .ThenInclude(x => x.Shape)
                                 .Include(x => x.Employee)
                                 .Where(x => !x.IsDeleted && x.OrderDetailId == Id)
                                 .OrderByDescending(x => x.OrderDetail.OrderNo)
                                 .ToListAsync();
        }

        public async Task<List<CrystalTrackingOutDetail>> GetCrystalConsumedDetails(CrystalStockPagingRequest pagingRequest)
        {
            return await _context.CrystalTrackingOutDetails
                                    .Include(x => x.CrystalTrackingOut)
                                    .Include(x => x.Crystal)
                                    .ThenInclude(x => x.Brand)
                                    .Include(x => x.Crystal)
                                    .ThenInclude(x => x.Shape)
                                    .Include(x => x.Crystal)
                                    .ThenInclude(x => x.Size)
                                    .Where(x => !x.IsDeleted &&
                                    x.CrystalTrackingOut.ReleaseDate.Date >= pagingRequest.FromDate.Date &&
                                    x.CrystalTrackingOut.ReleaseDate.Date <= pagingRequest.ToDate.Date &&
                                    (pagingRequest.ShapeId == 0 || x.Crystal.ShapeId == pagingRequest.ShapeId) &&
                                    (pagingRequest.SizeId == 0 || x.Crystal.SizeId == pagingRequest.SizeId) &&
                                    (pagingRequest.BrandId == 0 || x.Crystal.BrandId == pagingRequest.BrandId))
                                    .OrderBy(x => x.CrystalTrackingOut.ReleaseDate)
                                    .ToListAsync();
        }

        public async Task<List<CrystalTrackingOut>> GetOrderUsedCrystalsByEmployee(int empId, int month, int year)
        {
            return await _context.CrystalTrackingOuts
                .Include(x => x.Employee)
                .ThenInclude(x => x.MasterJobTitle)
                .Include(x => x.OrderDetail)
                 .Include(x => x.CrystalTrackingOutDetails)
                 .Where(x => !x.IsDeleted && (empId == 0 || x.EmployeeId == empId) && x.ReleaseDate.Month == month && x.ReleaseDate.Year == year).ToListAsync();
        }

        public async Task<PagingResponse<CrystalTrackingOut>> Search(SearchPagingRequest pagingRequest)
        {
            string searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var data = await _context.CrystalTrackingOuts
                                .Include(x => x.OrderDetail)
                                .ThenInclude(x => x.Order)
                                .Include(x => x.CrystalTrackingOutDetails)
                                .ThenInclude(x => x.Crystal)
                                .ThenInclude(x => x.Brand)
                                .Include(x => x.CrystalTrackingOutDetails)
                                .ThenInclude(x => x.Crystal)
                                .ThenInclude(x => x.Size)
                                .Include(x => x.CrystalTrackingOutDetails)
                                .ThenInclude(x => x.Crystal)
                                .ThenInclude(x => x.Shape)
                                .Include(x => x.Employee)
                                .Where(x => !x.IsDeleted)
                                .OrderByDescending(x => x.OrderDetail.OrderNo)
                                .ToListAsync();

            PagingResponse<CrystalTrackingOut> pagingResponse = new PagingResponse<CrystalTrackingOut>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                Data = data.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = data.Count()
            };

            return pagingResponse;
        }

        private async Task<bool> IsOrderDetailExist(int orderDetailId)
        {
            return await _context.CrystalTrackingOuts.Where(x => !x.IsDeleted && x.OrderDetailId == orderDetailId).CountAsync() > 0;
        }

        private async Task<bool> AdjustStockWhileUpdateTracking(int orderDetailId)
        {
            var data = await _context.CrystalTrackingOuts
                .Include(x => x.CrystalTrackingOutDetails)
                .Where(x => !x.IsDeleted && x.OrderDetailId == orderDetailId).FirstOrDefaultAsync();

            var stockList = _mapper.Map<List<CrystalStock>>(data.CrystalTrackingOutDetails);
            if (stockList.Count == 0)
                return true;
            //var trans = _context.Database.BeginTransaction();
            if (await _crystalStockRepository.IncreaseStock(stockList) > 0)
            {
                _context.CrystalTrackingOutDetails.RemoveRange(data.CrystalTrackingOutDetails);
                if (await _context.SaveChangesAsync() > 0)
                {
                    //trans.Commit();
                    return true;
                }
            }

            //trans.Rollback();
            return false;
        }

        public async Task<int> AddNote(int Id, string note)
        {
            var oldData = await _context.CrystalTrackingOuts
                .Include(x => x.OrderDetail)
                .Where(x => !x.IsDeleted && x.Id == Id)
                                            .FirstOrDefaultAsync() ?? throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            if (oldData.OrderDetail.Status == OrderStatusEnum.Delivered.ToString())
                throw new BusinessRuleViolationException(StaticValues.OrderAlreadyDeliveredError, StaticValues.OrderAlreadyDeliverdMessage);

            oldData.Note = note;
            var entity = _context.Attach(oldData);
            entity.State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public async Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime releaseDate)
        {
            return await _context.CrystalTrackingOutDetails
                                    .Include(x => x.CrystalTrackingOut)
                                    .ThenInclude(x => x.OrderDetail)
                                    .Where(x => !x.IsDeleted && x.CrystalId == crystalId && x.ReleaseDate == releaseDate)
                                    .Select(x => new CrystalUsedInOrderResponse()
                                    {
                                        OrderNo = x.CrystalTrackingOut.OrderDetail.OrderNo,
                                        Packets = x.ReleasePacketQty,
                                        AlterPackets = x.IsAlterWork ? x.ReleasePacketQty : 0,
                                        Pieces = x.ReleasePieceQty,
                                        LoosePieces = x.LoosePieces
                                    })
                                    .ToListAsync();
        }

        public async Task<List<KandooraWiseExpenseResponse>> GetKandooraWiseExpense(List<int> orderDetailIds)
        {

            var data = await _context.CrystalTrackingOuts
                 .Include(x => x.CrystalTrackingOutDetails)
                 .Where(x => !x.IsDeleted && orderDetailIds.Contains(x.OrderDetailId)).ToListAsync();
            if (data.Count == 0)
                return new List<KandooraWiseExpenseResponse>();
            var response = new List<KandooraWiseExpenseResponse>();
            data.ForEach(res =>
            {
                var expAmount = res.CrystalTrackingOutDetails.Where(x => !x.IsDeleted && !x.IsAlterWork).Sum(x => x.CrystalLabourCharge + x.ArticalLabourCharge);
                if (response.Where(x => x.OrderDetailId == res.OrderDetailId).Count() == 0)
                {
                    response.Add(new KandooraWiseExpenseResponse()
                    {
                        OrderDetailId = res.OrderDetailId,
                        ExpenseAmount = expAmount
                    });
                }
                else
                {
                   var oldData= response.Where(x => x.OrderDetailId == res.OrderDetailId).FirstOrDefault();
                    if(oldData != null)
                    {
                        oldData.ExpenseAmount += expAmount;
                    }
                }
            });
            return response;
        }

        public async Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime fromDate, DateTime toDate)
        {
            return await _context.CrystalTrackingOutDetails
                                   .Include(x => x.CrystalTrackingOut)
                                   .ThenInclude(x => x.OrderDetail)
                                   .Where(x => !x.IsDeleted && x.CrystalId == crystalId && x.ReleaseDate.Date >=fromDate && x.ReleaseDate.Date<=toDate)
                                   .Select(x => new CrystalUsedInOrderResponse()
                                   {
                                       OrderNo = x.CrystalTrackingOut.OrderDetail.OrderNo,
                                       Packets = x.ReleasePacketQty,
                                       AlterPackets = x.IsAlterWork?x.ReleasePacketQty:0,
                                       Pieces = x.ReleasePieceQty,
                                       LoosePieces=x.LoosePieces
                                   })
                                   .ToListAsync();
        }
    }
}
