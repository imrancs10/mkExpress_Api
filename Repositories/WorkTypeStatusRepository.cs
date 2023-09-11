using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Constants;
using MKExpress.API.Data;
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
    public class WorkTypeStatusRepository : IWorkTypeStatusRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IMasterDataRepository _masterDataRepository;
        public WorkTypeStatusRepository(MKExpressDbContext context, IMasterDataRepository masterDataRepository)
        {
            _context = context;
            _masterDataRepository = masterDataRepository;
        }

        public async Task<List<WorkTypeStatus>> Add(List<WorkTypeStatus> workTypeStatuses)
        {
            if (workTypeStatuses != null && workTypeStatuses.Count > 0)
            {
                workTypeStatuses.ForEach(res => res.VoucherNo = res.OrderDetailId.ToString());
            }
            _context.WorkTypeStatuses.AttachRange(workTypeStatuses);
            await _context.SaveChangesAsync();
            return workTypeStatuses;
        }

        public async Task<bool> AddAdditionalNote(int orderDetailId, string addtionalNote)
        {
            var oldData=await _context.WorkTypeStatuses.Where(x=>!x.IsDeleted && x.OrderDetailId==orderDetailId).ToListAsync();
            if (oldData.Count == 0)
                throw new BusinessRuleViolationException(StaticValues.DataNotFoundError, StaticValues.DataNotFoundMessage);
            oldData.ForEach(res => res.AddtionalNote = addtionalNote);
            _context.AttachRange(oldData);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> DeleteByOrderDetailId(List<int> orderDetailIds)
        {
            var workStatuses = await _context.WorkTypeStatuses
                                     .Where(x => orderDetailIds.Contains(x.OrderDetailId))
                                     .ToListAsync();
            if (workStatuses.Count == 0)
                return default;
            workStatuses.ForEach(res => res.IsDeleted = true);
            _context.AttachRange(workStatuses);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteByOrderDetailId(int orderDetailId)
        {
            var ids = new List<int>() { orderDetailId };
            return await DeleteByOrderDetailId(ids);
        }

        public async Task<List<WorkTypeStatus>> GetByOrderDetailId(int orderDetailId)
        {
            return await _context.WorkTypeStatuses
                .Include(x => x.CompletedByEmployee)
                .Include(x => x.CreatedByEmployee)
                .Include(x => x.WorkType)
                .Where(x => x.OrderDetailId == orderDetailId && !x.IsDeleted)
                .OrderBy(x => x.WorkType.Code)
                .ToListAsync();
        }

        public async Task<List<WorkTypeStatus>> GetByOrderId(int orderId)
        {
            try
            {
                var result= await _context.WorkTypeStatuses
               .Include(x => x.CompletedByEmployee)
               .Include(x => x.OrderDetail)
               .ThenInclude(x => x.Order)
               .Include(x => x.CreatedByEmployee)
               .Include(x => x.WorkType)
               .Where(x => x.OrderDetail.OrderId == orderId &&
                   !x.IsDeleted &&
                   !x.OrderDetail.IsCancelled &&
                   !x.OrderDetail.IsDeleted &&
                   !x.OrderDetail.Order.IsDeleted &&
                   !x.OrderDetail.Order.IsCancelled
               )
               .OrderByDescending(x => x.OrderDetailId)
               .ToListAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> GetOrderId(int orderDetailId)
        {
            var data= await _context.OrderDetails.Where(x => x.Id == orderDetailId).FirstOrDefaultAsync();
            return data?.OrderId??0;
        }

        public async Task<bool> IsAllKandooraCompleted(int orderId)
        {
            var orderDetailIds = await _context.OrderDetails
               .Where(x => !x.IsCancelled && !x.IsDeleted && x.OrderId == orderId)
               .Select(x => x.Id)
               .ToListAsync();

            return await _context.WorkTypeStatuses
               .Where(x => orderDetailIds.Contains(x.OrderDetailId) && x.CompletedOn == DateTime.MinValue && !x.IsDeleted)
               .CountAsync() == 0;
        }

        public async Task<bool> IsAnyKandooraProcessing(int orderId)
        {
            var orderDetailIds = await _context.OrderDetails
                .Where(x => !x.IsCancelled && !x.IsDeleted && x.OrderId == orderId)
                .Select(x => x.Id)
                .ToListAsync();

            return await _context.WorkTypeStatuses
               .Where(x => orderDetailIds.Contains(x.OrderDetailId) && x.CompletedOn != DateTime.MinValue && !x.IsDeleted)
               .CountAsync() > 0;

        }

        public async Task<bool> IsKandooraCompleted(int orderDetailId)
        {
            return await _context.WorkTypeStatuses
               .Where(x => x.OrderDetailId == orderDetailId && x.CompletedOn == DateTime.MinValue && !x.IsDeleted)
               .CountAsync() == 0;
        }
        public async Task<bool> IsKandooraCompleted(List<int> orderDetailIds)
        {
            return await _context.WorkTypeStatuses
               .Where(x => orderDetailIds.Contains(x.OrderDetailId) && x.CompletedOn == DateTime.MinValue && !x.IsDeleted)
               .CountAsync() == 0;
        }

        public async Task<bool> IsKandooraProcessing(int orderDetailId)
        {
            return await _context.WorkTypeStatuses
                .Where(x => x.OrderDetailId == orderDetailId && x.CompletedOn != DateTime.MinValue && !x.IsDeleted)
                .CountAsync() > 0;
        }

        public async Task<List<WorkTypeStatus>> RemoveAndAdd(List<WorkTypeStatus> workTypeStatuses)
        {
            if (workTypeStatuses.Count == 0)
                return default;
            var oldData = await _context.WorkTypeStatuses.Where(x => x.OrderDetailId == workTypeStatuses.First().OrderDetailId).ToListAsync();
            if (oldData.Count > 0)
            {
                _context.WorkTypeStatuses.RemoveRange(oldData);
                await _context.SaveChangesAsync();
            }

            return await Add(workTypeStatuses);
        }

        public async Task<WorkTypeStatus> Update(WorkTypeStatus workTypeStatuses)
        {
            var extraAmountWorkStatus = new List<WorkTypeStatus>();
            if (workTypeStatuses.Extra > 0)
            {
                var oldExtraAmount = await _context.WorkTypeStatuses
                    .Where(x => x.OrderDetailId == workTypeStatuses.OrderDetailId && !x.IsDeleted && x.WorkTypeId == workTypeStatuses.WorkTypeId && x.Extra > 0)
                    .FirstOrDefaultAsync();
                if (oldExtraAmount != null)
                {
                    oldExtraAmount.Extra = workTypeStatuses.Extra;
                    oldExtraAmount.Note = workTypeStatuses.Note;
                    oldExtraAmount.CompletedBy = workTypeStatuses.CompletedBy;
                    oldExtraAmount.CompletedOn = workTypeStatuses.CompletedOn;
                    oldExtraAmount.IsSaved = true;
                    extraAmountWorkStatus.Add(oldExtraAmount);
                }
                else
                {
                    extraAmountWorkStatus.Add(new WorkTypeStatus()
                    {
                        Note = workTypeStatuses.Note,
                        Extra = workTypeStatuses.Extra,
                        CompletedBy = workTypeStatuses.CompletedBy,
                        OrderDetailId = workTypeStatuses.OrderDetailId,
                        CompletedOn = workTypeStatuses.CompletedOn,
                        Price = 0,
                        IsSaved = true,
                        VoucherNo = workTypeStatuses.VoucherNo,
                        WorkTypeId = workTypeStatuses.WorkTypeId
                    }); ;
                }
            }
            else
            {
                extraAmountWorkStatus.Add(workTypeStatuses);
            }
            _context.UpdateRange(extraAmountWorkStatus);
            await _context.SaveChangesAsync();
            return workTypeStatuses;
        }

        public async Task<string> Update(int orderDetailId, string workType)
        {
            if (string.IsNullOrEmpty(workType) || orderDetailId < 1)
            {
                throw new BusinessRuleViolationException(StaticValues.WorkTypeNotEnteredError, StaticValues.WorkTypeNotEnteredMessage);
            }
            var groups = workType.GroupBy(c => c).Where(g => g.Count() > 1).ToList();
            var workTypeList = workType.ToCharArray().ToList();
            if (groups.Count > 0)
            {
                throw new BusinessRuleViolationException(StaticValues.DuplicateWorkTypeError, StaticValues.DuplicateWorkTypeMessage);
            }

            var workTypes = await _masterDataRepository.GetByMasterDataType("work_type");

            var oldWorkStatuses = await _context.WorkTypeStatuses.Include(x=>x.WorkType).Include(x=>x.OrderDetail).Where(x => x.OrderDetailId == orderDetailId && !x.IsDeleted).ToListAsync();
            var orderDetail = oldWorkStatuses.FirstOrDefault()?.OrderDetail;
            if (orderDetail!=null && (orderDetail.Status!=OrderStatusEnum.Processing.ToString() && orderDetail.Status != OrderStatusEnum.Active.ToString()))
            {
                throw new BusinessRuleViolationException(StaticValues.WorkTypeUpdateError, StaticValues.WorkTypeUpdateMessage);
            }
            var returnWorkType = "";
            oldWorkStatuses.ForEach(res =>
            {
                if (res.CompletedOn == DateTime.MinValue)
                    res.IsDeleted = true;
                else
                    returnWorkType += res.WorkType.Code;
            });

            workTypeList.ForEach(res =>
            {
                var oldData = oldWorkStatuses.Where(x => (x.WorkType?.Code??"") == res.ToString()).FirstOrDefault();
                if (oldData != null)
                {
                    oldWorkStatuses.Where(x => x.WorkType.Code == res.ToString()).FirstOrDefault().IsDeleted = false;
                    if(!returnWorkType.Contains(oldData.WorkType.Code))
                    returnWorkType += oldData.WorkType.Code;
                }
                else
                {
                    var selectWorkType = workTypes.Where(x => x.Code == res.ToString()).FirstOrDefault();
                    if (selectWorkType != null)
                    {
                        oldWorkStatuses.Add(new WorkTypeStatus()
                        {
                            CompletedBy = null,
                            Extra = 0,
                            IsSaved = false,
                            OrderDetailId = orderDetailId,
                            Price = 0,
                            WorkTypeId = workTypes.Where(x => x.Code == res.ToString()).First().Id,
                            VoucherNo = orderDetailId.ToString()
                        });
                        if (!returnWorkType.Contains(selectWorkType.Code))
                            returnWorkType += selectWorkType.Code;
                    }
                }
            });

            _context.WorkTypeStatuses.UpdateRange(oldWorkStatuses);
            if (await _context.SaveChangesAsync() > 0)
                return returnWorkType;
            else
                return "";
        }

        public async Task<bool> UpdateForCrystal(int orderDetailId, int completedBy, DateTime completedOn, decimal price, decimal extra)
        {
            int workTypeId = await _masterDataRepository.GetWorkTypeIdByCode("work_type","4");
            if(workTypeId>0)
            {
               var oldData= await _context.WorkTypeStatuses.Where(x => !x.IsDeleted && x.OrderDetailId == orderDetailId && x.WorkTypeId == workTypeId).FirstOrDefaultAsync();
                if(oldData!=null)
                {
                    oldData.CompletedBy = completedBy;
                    oldData.CompletedOn = completedOn;
                    oldData.Price = price;
                    oldData.Extra = extra;
                    _context.Attach(oldData);
                   return await _context.SaveChangesAsync()>0;
                }
            }
            return false;
        }

        public async Task<List<WorkTypeSumAmountResponse>> WorkTypeSumAmount(DateTime fromDate, DateTime toDate)
        {
            var result = await _context.WorkTypeStatuses.Where(x => x.CompletedOn.Date >= fromDate && x.CompletedOn.Date <= toDate)
                .Include(x => x.WorkType)
                .ToListAsync();
            return result
                .GroupBy(x => x.WorkTypeId)
                .Select(x => new WorkTypeSumAmountResponse
                {
                    WorkType = x.First().WorkType.Value,
                    Qty = x.Count(),
                    Amount = (decimal)x.Sum(y => y.Price + y.Extra)
                }).ToList();
        }
    }
}
