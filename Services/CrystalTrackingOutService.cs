using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using MKExpress.Web.API.Dto.Request;
using MKExpress.Web.API.Dto.Response.Crystal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Services
{
    public class CrystalTrackingOutService : ICrystalTrackingOutService
    {
        private readonly ICrystalTrackingOutRepository _crystalTrackingOutRepository;
        private readonly IWorkTypeStatusRepository _workTypeStatusRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CrystalTrackingOutService(ICrystalTrackingOutRepository crystalTrackingOutRepository, IMapper mapper, IOrderRepository orderRepository,IWorkTypeStatusRepository workTypeStatusRepository)
        {
            _crystalTrackingOutRepository = crystalTrackingOutRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _workTypeStatusRepository = workTypeStatusRepository;
        }
        public async Task<int> Add(CrystalTrackingOutRequest request)
        {
            var crystalTrackingOut = _mapper.Map<CrystalTrackingOut>(request);
            var result= await _crystalTrackingOutRepository.Add(crystalTrackingOut);
            if(result>0)
            {
                decimal price = request.CrystalTrackingOutDetails.Sum(x => x.IsAlterWork ? 0:x.CrystalLabourCharge);
                decimal extra = request.CrystalTrackingOutDetails.Sum(x => x.IsAlterWork? x.CrystalLabourCharge:0);
                if (await _workTypeStatusRepository.UpdateForCrystal(request.OrderDetailId, request.EmployeeId, request.ReleaseDate, price, extra)) 
                {
                    if(await _workTypeStatusRepository.IsKandooraCompleted(request.OrderDetailId)){
                        var orderId=await _workTypeStatusRepository.GetOrderId(request.OrderDetailId);
                        await _orderRepository.ChangeOrderDetailStatus(request.OrderDetailId, OrderStatusEnum.Completed, string.Empty);
                        if (await _workTypeStatusRepository.IsAllKandooraCompleted(orderId))
                        {
                            await _orderRepository.ChangeOrderStatus(orderId, OrderStatusEnum.Completed, string.Empty);
                        }
                    }
                    else
                    await _orderRepository.ChangeOrderDetailStatus(request.OrderDetailId, OrderStatusEnum.Processing, string.Empty);


                }
            }
            return result;
        }

        public async Task<int> AddNote(int Id, string note)
        {
            return await _crystalTrackingOutRepository.AddNote(Id, note);
        }

        public async Task<int> Delete(int Id)
        {
            return await _crystalTrackingOutRepository.Delete(Id);
        }

        public async Task<int> DeleteDetail(int Id)
        {
            return await _crystalTrackingOutRepository.DeleteDetail(Id);
        }

        public async Task<CrystalTrackingOutResponse> Get(int Id)
        {
            return _mapper.Map<CrystalTrackingOutResponse>(await _crystalTrackingOutRepository.Get(Id));
        }

        public async Task<PagingResponse<CrystalTrackingOutResponse>> GetAll(PagingRequest pagingRequest)
        {
            return _mapper.Map<PagingResponse<CrystalTrackingOutResponse>>(await _crystalTrackingOutRepository.GetAll(pagingRequest));
        }

        public async Task<List<CrystalTrackingOutResponse>> GetByOrderDetailId(int Id)
        {
            return _mapper.Map<List<CrystalTrackingOutResponse>>(await _crystalTrackingOutRepository.GetByOrderDetailId(Id));
        }

        public async Task<List<CrystalConsumeDetailResponse>> GetCrystalConsumedDetails(CrystalStockPagingRequest pagingRequest)
        {
            var res =_mapper.Map<List<CrystalTrackingOutDetailResponse>>(await _crystalTrackingOutRepository.GetCrystalConsumedDetails(pagingRequest));
            var output= new List<CrystalConsumeDetailResponse>();
            var grpByReleaseDate = res.GroupBy(x => x.ReleaseDate);
            foreach (var dateGrp in grpByReleaseDate)
            {
                var grpByCrystal = dateGrp.GroupBy(x => x.CrystalId);
                foreach (var crystalGrp in grpByCrystal)
                {
                    output.Add(new CrystalConsumeDetailResponse()
                    {
                        CrystalId = crystalGrp.Key,
                        CrystalName=crystalGrp.FirstOrDefault()?.CrystalName ?? string.Empty,
                        ReleaseDate=crystalGrp.FirstOrDefault()?.ReleaseDate??dateGrp.Key,
                        LoosePieces=crystalGrp.Sum(x => x.LoosePieces),
                        ReleasePacketQty=crystalGrp.Sum(x => x.ReleasePacketQty),
                        ReleasePieceQty= crystalGrp.Sum(x => x.ReleasePieceQty),
                        CrystalBrand= crystalGrp.FirstOrDefault()?.CrystalBrand,
                        CrystalShape = crystalGrp.FirstOrDefault()?.CrystalShape,
                        CrystalSize= crystalGrp.FirstOrDefault()?.CrystalSize,
                        AlterPackets= crystalGrp.Where(x=>x.IsAlterWork).Sum(x => x.ReleasePacketQty),
                        TotalOrders = crystalGrp.Count()
                    });
                }
            }
            return output;

        }

        public async Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime releaseDate)
        {
           return await _crystalTrackingOutRepository.GetOrderUsedCrystalsByReleaseDateAndCrystalId(crystalId, releaseDate);
        }

        public async Task<List<CrystalUsedInOrderResponse>> GetOrderUsedCrystalsByReleaseDateAndCrystalId(int crystalId, DateTime fromDate, DateTime toDate)
        {
            return await _crystalTrackingOutRepository.GetOrderUsedCrystalsByReleaseDateAndCrystalId(crystalId, fromDate,toDate);
        }

        public async Task<PagingResponse<CrystalTrackingOutResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            return _mapper.Map<PagingResponse<CrystalTrackingOutResponse>>(await _crystalTrackingOutRepository.Search(searchPagingRequest));
        }
    }
}
