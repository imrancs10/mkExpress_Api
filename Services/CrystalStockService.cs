using AutoMapper;
using Org.BouncyCastle.Math.EC.Rfc7748;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class CrystalStockService : ICrystalStockService
    {
        private readonly ICrystalStockRepository _crystalStockRepository;
        private readonly ICrystalPurchaseRepository _crystalPurchaseRepository;
        private readonly ICrystalTrackingOutRepository _crystalTrackingOutRepository;
        private readonly IMapper _mapper;
        public CrystalStockService(ICrystalStockRepository crystalStockRepository, IMapper mapper, ICrystalPurchaseRepository crystalPurchaseRepository, ICrystalTrackingOutRepository crystalTrackingOutRepository)
        {
            _crystalStockRepository = crystalStockRepository;
            _mapper = mapper;
            _crystalPurchaseRepository = crystalPurchaseRepository;
            _crystalTrackingOutRepository = crystalTrackingOutRepository;
        }

        public async Task<PagingResponse<CrystalStockResponseExt>> GetCrystalStockDetails(CrystalStockPagingRequest pagingRequest)
        {
            var stock = await _crystalStockRepository.GetCrystalStockDetails(pagingRequest);
            var oldStock = await _crystalPurchaseRepository.GetPurchaseCrystalCounts(DateTime.MinValue, pagingRequest.FromDate);
            var newStock = await _crystalPurchaseRepository.GetPurchaseCrystalCounts(pagingRequest.FromDate, pagingRequest.ToDate);
            var consumeStock = await _crystalTrackingOutRepository.GetCrystalConsumedDetails(new CrystalStockPagingRequest()
            {
                FromDate=pagingRequest.FromDate,
                ToDate=pagingRequest.ToDate,
                PageNo=1,
                PageSize=int.MaxValue
            });
            var res = new PagingResponse<CrystalStockResponseExt>()
            {
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize,
                TotalRecords = stock.TotalRecords,
                Data=new List<CrystalStockResponseExt>()
            };

            foreach (var item in stock.Data)
            {
                var newItem = _mapper.Map<CrystalStockResponseExt>(item);
                if(oldStock.ContainsKey(item.CrystalId))
                {
                    newItem.OldStock = oldStock[item.CrystalId];
                    newItem.TotalStock += newItem.OldStock;
                }
                if (newStock.ContainsKey(item.CrystalId))
                {
                    newItem.NewStock = newStock[item.CrystalId];
                    newItem.TotalStock += newItem.NewStock;
                }
                newItem.ConsumeStock = consumeStock.Where(x => x.CrystalId == item.CrystalId).Sum(x => x.ReleasePacketQty);
                newItem.TotalStock -= newItem.ConsumeStock;
                newItem.BalanceStockPieces = (int)newItem.TotalStock * newItem.QtyPerPacket;
                res.Data.Add(newItem);
            }
            return res;
        }

        public async Task<List<CrystalStockResponse>> GetCrystalStockAlert(CrystalStockPagingRequest pagingRequest)
        {
            return _mapper.Map<List<CrystalStockResponse>>(await _crystalStockRepository.GetCrystalStockAlert(pagingRequest));
        }

        public async Task<int> UpdateStock(CrystalStockRequest crystalStockRequest, string reason)
        {
            CrystalStock crystalStock = _mapper.Map<CrystalStock>(crystalStockRequest);
            return await _crystalStockRepository.UpdateStock(crystalStock, reason);
        }

        public async Task<CrystalStockResponse> GetCrystalStockDetail(int Id)
        {
            return _mapper.Map<CrystalStockResponse>(await _crystalStockRepository.GetCrystalStockDetail(Id));
        }

        public async Task<PagingResponse<CrystalStockResponse>> SearchCrystalStockAlert(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<CrystalStockResponse>>(await _crystalStockRepository.SearchCrystalStockAlert(searchPagingRequest));
        }

        public async Task<PagingResponse<CrystalStockResponseExt>> SearchCrystalStockDetails(SearchPagingRequest searchPagingRequest)
        {
           // return _mapper.Map<PagingResponse<CrystalStockResponse>>(await );
            var stock = await _crystalStockRepository.SearchCrystalStockDetails(searchPagingRequest);
            var oldStock = await _crystalPurchaseRepository.GetPurchaseCrystalCounts(DateTime.MinValue, DateTime.MaxValue);
            var newStock = await _crystalPurchaseRepository.GetPurchaseCrystalCounts(DateTime.MinValue, DateTime.MaxValue);
            var consumeStock = await _crystalTrackingOutRepository.GetCrystalConsumedDetails(new CrystalStockPagingRequest()
            {
                FromDate = DateTime.MinValue,
                ToDate = DateTime.MaxValue,
                PageNo = 1,
                PageSize = int.MaxValue
            });
            var res = new PagingResponse<CrystalStockResponseExt>()
            {
                PageNo = searchPagingRequest.PageNo,
                PageSize = searchPagingRequest.PageSize,
                TotalRecords = stock.TotalRecords,
                Data = new List<CrystalStockResponseExt>()
            };

            foreach (var item in stock.Data)
            {
                var newItem = _mapper.Map<CrystalStockResponseExt>(item);
                if (oldStock.ContainsKey(item.CrystalId))
                {
                    newItem.OldStock = oldStock[item.CrystalId];
                    newItem.TotalStock += newItem.OldStock;
                }
                if (newStock.ContainsKey(item.CrystalId))
                {
                    newItem.NewStock = newStock[item.CrystalId];
                    newItem.TotalStock += newItem.NewStock;
                }
                newItem.ConsumeStock = consumeStock.Where(x => x.CrystalId == item.CrystalId).Sum(x => x.ReleasePacketQty);
                newItem.TotalStock -= newItem.ConsumeStock;
                newItem.BalanceStockPieces = (int)newItem.TotalStock * newItem.QtyPerPacket;
                res.Data.Add(newItem);
            }
            return res;
        }
    }
}
