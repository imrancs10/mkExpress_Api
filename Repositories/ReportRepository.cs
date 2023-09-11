using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Customer;
using MKExpress.API.Dto.Response.Orders;
using MKExpress.API.Dto.Response.Report;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly MKExpressDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMasterDataRepository _masterDataRepository;
        private readonly IKandooraExpenseRepository _kandooraExpenseRepository;
        private readonly ICrystalTrackingOutRepository _crystalTrackingOutRepository;
        public ReportRepository(MKExpressDbContext context, IMapper mapper, IExpenseRepository expenseRepository, IMasterDataRepository masterDataRepository, IKandooraExpenseRepository kandooraExpenseRepository, ICrystalTrackingOutRepository crystalTrackingOutRepository)
        {
            _context = context;
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _masterDataRepository = masterDataRepository;
            _kandooraExpenseRepository = kandooraExpenseRepository;
            _crystalTrackingOutRepository = crystalTrackingOutRepository;
        }
        public async Task<List<CustomerAccountStatement>> BillingCancelTaxReport(DateTime fromDate, DateTime toDate)
        {
            return await _context.CustomerAccountStatements
               .Include(x => x.Order)
               .Where(x =>
                            !x.IsDeleted &&
                            x.Order.AdvanceAmount > 0 &&
                            x.PaymentDate.Date >= fromDate.Date &&
                            x.PaymentDate.Date <= toDate.Date &&
                            x.Reason == AccountStatementReasonEnum.OrderCancelled.ToString())
               .OrderBy(x => x.PaymentDate)
               .ToListAsync();
        }
        public async Task<List<CustomerAccountStatement>> BillingTaxReport(DateTime fromDate, DateTime toDate)
        {
            var data = await _context.Orders
                .Include(x => x.OrderDetails.Where(y => !y.IsCancelled && !y.IsDeleted && y.Status == OrderStatusEnum.Delivered.ToString()))
                .Include(x => x.AccountStatements.Where(y => y.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString() || y.Reason == AccountStatementReasonEnum.PaymentReceived.ToString()).OrderByDescending(y => y.PaymentDate))
                .Where(x =>
                            !x.IsDeleted &&
                            !x.IsCancelled &&
                            x.Status == OrderStatusEnum.Delivered.ToString()).ToListAsync();

            var response = data.Select(x => new CustomerAccountStatement()
            {
                Credit = x.AccountStatements.Sum(y => y.Credit),
                PaymentDate = x.AccountStatements.First().PaymentDate,
                PaymentMode = x.AccountStatements.First().PaymentMode,
                DeliveredQty = x.AccountStatements.First().DeliveredQty,
                OrderId = x.Id,
                Order = x,
            }).Where(x => x.PaymentDate.Date >= fromDate.Date && x.PaymentDate.Date <= toDate.Date).ToList();
            return response.OrderBy(x => x.PaymentDate.Date).ToList();

        }
        public async Task<DailyStatusResponse> DailyStatusReport(DateTime date)
        {
            var data = new DailyStatusResponse() { };
            data.Orders = _mapper.Map<List<OrderResponse>>(
                            await _context.Orders
                                            .Include(x => x.OrderDetails)
                                            .Where(x => !x.IsCancelled &&
                                                        !x.IsDeleted &&
                                                        x.OrderDate.Date == date.Date
                                                  ).ToListAsync()
                                               );
            data.CustomerAccountStatements = _mapper.Map<List<CustomerAccountStatementResponse>>(
                await _context.CustomerAccountStatements
                .Include(x => x.Order)
                .Where(x => !x.IsDeleted &&
                            x.PaymentDate.Date == date.Date &&
                            !x.Order.IsCancelled &&
                            !x.Order.IsDeleted &&
                            (x.Reason == AccountStatementReasonEnum.PaymentReceived.ToString() ||
                                        x.Reason == AccountStatementReasonEnum.AdvancedPaid.ToString())
                            ).ToListAsync()
                       );
            data.ExpenseAmount = await _expenseRepository.GetTotalCancelOrderExpenseByDate(date.Date);
            data.CustomerAccountStatements.ForEach(res =>
            {
                res.Order.AccountStatements = null;
                res.Order.OrderDetails = null;
            });
            return data;
        }

        public async Task<PagingResponse<KandooraExpenseResponse>> EachKandooraExpenseReport(KandooraExpensePagingRequest pagingRequest)
        {
            var kandooraExpense = await _context.WorkTypeStatuses
                                                .Include(x => x.WorkType)
                                                .Include(x => x.OrderDetail)
                                                .ThenInclude(x => x.Order)
                                                .ThenInclude(x => x.Customer)
                                                 .Include(x => x.OrderDetail)
                                                .ThenInclude(x => x.Order)
                                                .ThenInclude(x => x.Employee)
                                                .Include(x => x.OrderDetail)
                                                .Where(x => !x.IsDeleted &&
                                                !x.OrderDetail.IsCancelled &&
                                                !x.OrderDetail.IsDeleted &&
                                                x.OrderDetail.Order.OrderDate.Date >= pagingRequest.FromDate.Date &&
                                                x.OrderDetail.Order.OrderDate.Date <= pagingRequest.ToDate.Date &&
                                                (pagingRequest.SalesmanId == 0 || x.OrderDetail.Order.EmployeeId == pagingRequest.SalesmanId) &&
                                                 (pagingRequest.OrderStatus.ToUpper() == StaticValues.TextZeroInt || pagingRequest.OrderStatus.ToUpper() == StaticValues.TextAll || x.OrderDetail.Status == pagingRequest.OrderStatus))
                                                .ToArrayAsync();
            return await FilterEachKandooraExpense(pagingRequest, kandooraExpense, pagingRequest.ProfitPercentageFilter);

        }

        private async Task<PagingResponse<KandooraExpenseResponse>> FilterEachKandooraExpense(PagingRequest pagingRequest, WorkTypeStatus[] kandooraExpense, int profitPercentageFilter = 0)
        {
            var worktypes = await _masterDataRepository.GetByMasterDataType(StaticValues.WorkTypeCode);
            var fixedExpense = await _kandooraExpenseRepository.GetTotalOfExpense();
            var kandooraExpenseGrp = kandooraExpense.GroupBy(x => x.OrderDetailId);
            var orderDetailIds=kandooraExpenseGrp.Select(x=>x.Key).ToList();
            //   var crystalCharges = await _crystalTrackingOutRepository.GetKandooraWiseExpense(orderDetailIds); // Enable when price used from crystal tracking
            // var crystalChargesDic = crystalCharges.ToDictionary(x => x.OrderDetailId, y => y.ExpenseAmount);
            List<KandooraExpenseResponse> outputList = new List<KandooraExpenseResponse>();
            var workTypeDic = GetWorkTypeDic(worktypes);
            foreach (var grpItem in kandooraExpenseGrp)
            {
                var orderDetail = grpItem.FirstOrDefault()?.OrderDetail;
                KandooraExpenseResponse eachKandoora = new KandooraExpenseResponse
                {
                    OrderNo = orderDetail?.OrderNo,
                    Amount = orderDetail?.SubTotalAmount ?? 0,
                    ModalNo = orderDetail?.ModelNo ?? "",
                    OrderDate = orderDetail?.Order.OrderDate ?? DateTime.MinValue,
                    CustomerName = orderDetail?.Order?.Customer?.Firstname,
                    FixAmount = fixedExpense,
                    Salesman=orderDetail?.Order?.Employee?.FirstName+" "+ orderDetail?.Order?.Employee?.LastName,
                    Status=orderDetail.Status,
                    //CrystalUsed = crystalChargesDic.Keys.Contains(grpItem.Key) ? crystalChargesDic[grpItem.Key]:0,
                };
                foreach (var workTypeStatus in grpItem)
                {
                    var code = StaticValues.TextZeroInt;
                    if (workTypeDic.ContainsKey(workTypeStatus.WorkTypeId))
                    {
                        code = workTypeDic[workTypeStatus.WorkTypeId];
                        switch (code)
                        {
                            case StaticValues.TextOneInt:
                                eachKandoora.Design = workTypeStatus.Price ?? 0; break;
                            case StaticValues.TextTwoInt:
                                eachKandoora.Cutting = workTypeStatus.Price ?? 0; break;
                            case StaticValues.TextThreeInt:
                                eachKandoora.MEmb = workTypeStatus.Price ?? 0; break;
                            case StaticValues.TextFourInt:
                                {
                                    eachKandoora.HFix = workTypeStatus.Price ?? 0;
                                    eachKandoora.CrystalUsed = (eachKandoora.HFix / 17) * 100; // remove when price used from crystal tracking
                                    break;
                                }
                            case StaticValues.TextFiveInt:
                                eachKandoora.HEmb = workTypeStatus.Price ?? 0; break;
                            case StaticValues.TextSixInt:
                                eachKandoora.Apliq = workTypeStatus.Price ?? 0; break;
                            case StaticValues.TextSevenInt:
                                eachKandoora.Stitch = workTypeStatus.Price ?? 0;
                                break;
                            default:
                                break;
                        }
                    }


                }
                eachKandoora.TotalAmount = grpItem.Sum(x => x.Price ?? 0) + fixedExpense+eachKandoora.CrystalUsed;// + (crystalChargesDic.Keys.Contains(grpItem.Key) ? crystalChargesDic[grpItem.Key] : 0);
                eachKandoora.Profit = eachKandoora.Amount - eachKandoora.TotalAmount;
                eachKandoora.ProfitPercentage = eachKandoora.Profit / eachKandoora.Amount * 100;
                if (eachKandoora.ProfitPercentage > 100)
                {
                    eachKandoora.ProfitPercentage = 100 - eachKandoora.ProfitPercentage;
                }
                if (profitPercentageFilter == 0)
                    outputList.Add(eachKandoora);
                else if (profitPercentageFilter == 1)
                {
                    if (eachKandoora.ProfitPercentage < 0)
                        outputList.Add(eachKandoora);
                }
                else if (profitPercentageFilter == 2)
                {
                    if (eachKandoora.ProfitPercentage >= 0 && eachKandoora.ProfitPercentage <= 20)
                        outputList.Add(eachKandoora);
                }
                else if (profitPercentageFilter == 3)
                {
                    if (eachKandoora.ProfitPercentage > 20 && eachKandoora.ProfitPercentage <= 40)
                        outputList.Add(eachKandoora);
                }
                else if (profitPercentageFilter == 4)
                {
                    if (eachKandoora.ProfitPercentage > 40 && eachKandoora.ProfitPercentage <= 60)
                        outputList.Add(eachKandoora);
                }
                else if (profitPercentageFilter == 5)
                {
                    if (eachKandoora.ProfitPercentage > 60 && eachKandoora.ProfitPercentage <= 80)
                        outputList.Add(eachKandoora);
                }
                else if (profitPercentageFilter == 6)
                {
                    if (eachKandoora.ProfitPercentage > 80)
                        outputList.Add(eachKandoora);
                }
            }
            PagingResponse<KandooraExpenseResponse> response = new PagingResponse<KandooraExpenseResponse>()
            {
                Data = outputList.Skip(pagingRequest.PageSize * (pagingRequest.PageNo - 1)).Take(pagingRequest.PageSize).ToList(),
                TotalRecords = outputList.Count,
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize
            };
            return response;
        }

        public async Task<List<WorkerPerformanceReportResponse>> GetWorkerPerformance(int workType, DateTime fromDate, DateTime toDate)
        {
            var data = await _context.WorkTypeStatuses
                .Include(x => x.WorkType)
                .Include(x => x.CompletedByEmployee)
                .Where(x => !x.IsDeleted && x.CompletedOn.Date >= fromDate.Date && x.CompletedOn.Date <= toDate.Date && x.WorkTypeId == workType)
                .OrderBy(x => x.CompletedBy)
                .ToListAsync();

            var groupData = data.GroupBy(x => x.CompletedBy).AsEnumerable();
            List<WorkerPerformanceReportResponse> lst = new List<WorkerPerformanceReportResponse>();
            foreach (var ele in groupData)
            {
                decimal amount = (decimal)ele.Sum(x => x.Extra + x.Price);
                int qty = ele.Count();
                lst.Add(new WorkerPerformanceReportResponse()
                {
                    Amount = amount,
                    Qty = qty,
                    AvgAmount = amount / qty,                   
                    WorkerId = ele.First().CompletedByEmployee.Id,
                    WorkerName = ele.First().CompletedByEmployee.FirstName + " " + ele.First().CompletedByEmployee.LastName
                });
            }
            return lst.OrderByDescending(x=>x.Amount).ToList();
        }

        public async Task<PagingResponse<KandooraExpenseResponse>> SearchEachKandooraExpenseReport(SearchPagingRequest pagingRequest)
        {
            var searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var kandooraExpense = await _context.WorkTypeStatuses
                                                .Include(x => x.WorkType)
                                                .Include(x => x.OrderDetail)
                                                .ThenInclude(x => x.Order)
                                                .ThenInclude(x => x.Customer)
                                                .Include(x => x.OrderDetail)
                                                .ThenInclude(x => x.Order)
                                                .ThenInclude(x => x.Employee)
                                                .Include(x => x.OrderDetail)
                                                .ThenInclude(x => x.CrystalTrackingOutDetails)
                                                .Where(x => !x.IsDeleted &&
                                                !x.OrderDetail.IsCancelled &&
                                                !x.OrderDetail.IsDeleted &&
                                                            (
                                                            searchTerm == string.Empty ||
                                                            (x.CompletedByEmployee.FirstName + " " + x.CompletedByEmployee.LastName).Contains(searchTerm) ||
                                                             (x.OrderDetail.Order.Customer.Firstname + " " + x.OrderDetail.Order.Customer.Lastname).Contains(searchTerm) ||
                                                            x.Note.Contains(searchTerm) ||
                                                            x.OrderDetail.OrderNo.Contains(searchTerm) ||
                                                            x.OrderDetail.ModelNo.Contains(searchTerm) ||
                                                            x.WorkType.Value.Contains(searchTerm)
                                                            ))
                                                .ToArrayAsync();
            PagingRequest paging = new PagingRequest()
            {
                FromDate = pagingRequest.FromDate,
                ToDate = pagingRequest.ToDate,
                PageNo = pagingRequest.PageNo,
                PageSize = pagingRequest.PageSize
            };
            return await FilterEachKandooraExpense(paging, kandooraExpense);
        }

        private Dictionary<int, string> GetWorkTypeDic(List<MasterData> data)
        {
            return data.ToDictionary(x => x.Id, y => y.Code);
        }

        public async Task<List<DailyWorkStatementResponse>> DailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest)
        {
            var output = await _context.WorkTypeStatuses
                                     .Include(x => x.OrderDetail)
                                     .Include(x => x.CompletedByEmployee)
                                     .Where(x => !x.IsDeleted &&
                                                    x.WorkTypeId == pagingRequest.WorkType &&
                                                    x.CompletedOn.Date >= pagingRequest.FromDate &&
                                                    x.CompletedOn.Date <= pagingRequest.ToDate &&
                                                    (pagingRequest.ReportType == 0 || (pagingRequest.ReportType == 1 && x.Extra > 0)))
                                     .ToListAsync();
            return output.Select(x => new DailyWorkStatementResponse()
            {
                Amount = pagingRequest.ReportType == 0 ? x.Price ?? 0 : x.Extra ?? 0,
                Date = x.CompletedOn.Date,
                EmployeeName = x.CompletedByEmployee.FirstName + " " + x.CompletedByEmployee.LastName,
                EmployeeId = x.CompletedBy ?? 0,
                ModalNo = x.OrderDetail.ModelNo,
                Note = x.Note,
                OrderNo = x.OrderDetail.OrderNo
            })
                .OrderBy(x => x.OrderNo)
                .ToList();
        }

        public async Task<List<DailyWorkStatementResponse>> SearchDailyWorkStatement(DailyWorkStatementPagingRequest pagingRequest)
        {
            var searchTerm = string.IsNullOrEmpty(pagingRequest.SearchTerm) ? string.Empty : pagingRequest.SearchTerm.ToLower();
            var output = await _context.WorkTypeStatuses
                                    .Include(x => x.OrderDetail)
                                    .Include(x => x.CompletedByEmployee)
                                    .Where(x => !x.IsDeleted &&
                                                   x.WorkTypeId == pagingRequest.WorkType &&
                                                   (x.OrderDetail.OrderNo.Contains(searchTerm) ||
                                                    (x.CompletedByEmployee.FirstName + " " + x.CompletedByEmployee.LastName).Contains(searchTerm)) &&
                                                   (pagingRequest.ReportType == 0 || (pagingRequest.ReportType == 1 && x.Extra > 0)))
                                    .ToListAsync();
            return output.Select(x => new DailyWorkStatementResponse()
            {
                Amount = pagingRequest.ReportType == 0 ? x.Price ?? 0 : x.Extra ?? 0,
                Date = x.CompletedOn.Date,
                EmployeeName = x.CompletedByEmployee.FirstName + " " + x.CompletedByEmployee.LastName,
                EmployeeId = x.CompletedBy ?? 0,
                ModalNo = x.OrderDetail.ModelNo,
                Note = x.Note,
                OrderNo = x.OrderDetail.OrderNo
            })
                .OrderBy(x => x.OrderNo)
                .ToList();
        }
    }
}
