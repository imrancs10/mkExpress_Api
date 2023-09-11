using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IWorkTypeStatusRepository
    {
        Task<List<WorkTypeStatus>> Add(List<WorkTypeStatus> workTypeStatuses);
        Task<WorkTypeStatus> Update(WorkTypeStatus workTypeStatuses);
        Task<string> Update(int orderDetailId,string workType);
        Task<List<WorkTypeStatus>> GetByOrderDetailId(int orderDetailId);
        Task<List<WorkTypeStatus>> GetByOrderId(int orderId);
        Task<List<WorkTypeStatus>> RemoveAndAdd(List<WorkTypeStatus> workTypeStatuses);
        Task<bool> IsKandooraProcessing(int orderDetailId);
        Task<bool> IsKandooraCompleted(int orderDetailId);
        Task<bool> IsKandooraCompleted(List<int> orderDetailId);
        Task<bool> AddAdditionalNote(int orderDetailId,string AddtionalNote);
        Task<bool> IsAnyKandooraProcessing(int orderId);
        Task<int> GetOrderId(int orderDetailId);
        Task<bool> IsAllKandooraCompleted(int orderId);
        Task<bool> UpdateForCrystal(int orderDetailId,int completedBy,DateTime completedOn,decimal price,decimal extra);
        Task<List<WorkTypeSumAmountResponse>> WorkTypeSumAmount(DateTime fromDate, DateTime toDate);
        Task<int> DeleteByOrderDetailId(List<int> orderDetailIds);
        Task<int> DeleteByOrderDetailId(int orderDetailId);
    }
}
