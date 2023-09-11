using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IWorkTypeStatusService
    {
        Task<List<WorkTypeStatusResponse>> Add(List<WorkTypeStatusRequest> workTypeStatuses);
        Task<WorkTypeStatusResponse> Update(WorkTypeStatusRequest workTypeStatuses);
        Task<List<WorkTypeStatusResponse>> GetByOrderDetailId(int orderDetailId);
        Task<List<WorkTypeStatusResponse>> GetByOrderId(int orderId);
        Task<bool> AddAdditionalNote(int orderDetailId, string addtionalNote);
        Task<List<WorkTypeSumAmountResponse>> WorkTypeSumAmount(DateTime fromDate, DateTime toDate);
        Task<int> Update(int orderDetailId, string workType);
    }
}
