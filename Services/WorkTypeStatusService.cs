using AutoMapper;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class WorkTypeStatusService : IWorkTypeStatusService
    {
        private readonly IWorkTypeStatusRepository _workTypeStatusRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public WorkTypeStatusService(IWorkTypeStatusRepository workTypeStatusRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            _workTypeStatusRepository = workTypeStatusRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<List<WorkTypeStatusResponse>> Add(List<WorkTypeStatusRequest> workTypeStatusRequest)
        {
            List<WorkTypeStatus> workTypeStatuses = _mapper.Map<List<WorkTypeStatus>>(workTypeStatusRequest);
            return _mapper.Map<List<WorkTypeStatusResponse>>(await _workTypeStatusRepository.Add(workTypeStatuses));
        }

        public async Task<bool> AddAdditionalNote(int orderDetailId, string addtionalNote)
        {
            return await _workTypeStatusRepository.AddAdditionalNote(orderDetailId, addtionalNote);
        }

        public async Task<List<WorkTypeStatusResponse>> GetByOrderDetailId(int orderDetailId)
        {
            List<WorkTypeStatusResponse> result = _mapper.Map<List<WorkTypeStatusResponse>>(await _workTypeStatusRepository.GetByOrderDetailId(orderDetailId));
            return result.OrderBy(x => x.WorkTypeSequence).ToList();
        }

        public async Task<List<WorkTypeStatusResponse>> GetByOrderId(int orderId)
        {
            List<WorkTypeStatusResponse> result = _mapper.Map<List<WorkTypeStatusResponse>>(await _workTypeStatusRepository.GetByOrderId(orderId));
            if (result.Count == 0)
                return result;
            return result.OrderBy(x => x.OrderDetailId).ThenBy(x=>x.WorkTypeSequence).ToList();
        }

        public async Task<WorkTypeStatusResponse> Update(WorkTypeStatusRequest workTypeStatusRequest)
        {
            WorkTypeStatus workTypeStatuse = _mapper.Map<WorkTypeStatus>(workTypeStatusRequest);
            var result = _mapper.Map<WorkTypeStatusResponse>(await _workTypeStatusRepository.Update(workTypeStatuse));
            if (workTypeStatusRequest.Extra == 0)
            {
                if (await _workTypeStatusRepository.IsKandooraCompleted(workTypeStatusRequest.OrderDetailId))
                {
                    await _orderRepository.ChangeOrderDetailStatus(workTypeStatusRequest.OrderDetailId, OrderStatusEnum.Completed, "Status Change - " + OrderStatusEnum.Completed.ToString());
                }
                else
                {
                    await _orderRepository.ChangeOrderDetailStatus(workTypeStatusRequest.OrderDetailId, OrderStatusEnum.Processing, "Status Change - " + OrderStatusEnum.Processing.ToString());
                }
            }
            return result;
        }

        public async Task<int> Update(int orderDetailId, string workType)
        {
            var result= await _workTypeStatusRepository.Update(orderDetailId, workType);
            if(result!="")
            {
                if(await _workTypeStatusRepository.IsKandooraCompleted(orderDetailId))
                {
                    await _orderRepository.ChangeOrderDetailStatus(orderDetailId, OrderStatusEnum.Completed, "Work Type Updated");
                }

               return await _orderRepository.UpdateWorkType(orderDetailId, result);
            }
            return 0;
        }

        public async Task<List<WorkTypeSumAmountResponse>> WorkTypeSumAmount(DateTime fromDate, DateTime toDate)
        {
            return await _workTypeStatusRepository.WorkTypeSumAmount(fromDate, toDate);
        }
    }
}
