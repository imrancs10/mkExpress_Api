using Microsoft.EntityFrameworkCore;
using MKExpress.API.Constants;
using MKExpress.API.Data;
using MKExpress.API.Dto.Response;
using MKExpress.API.Dto.Response.Dashboard;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MKExpressDbContext _context;
        public DashboardRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSalesResponse> GetDailySales()
        {
            var orders = await _context.Orders
                .Where(x => !x.IsCancelled &&
                !x.IsDeleted &&
                x.OrderDate.Date == DateTime.Today)
                .ToListAsync();
            return new DashboardSalesResponse()
            {
                Name = "Orders",
                Count = orders.Count,
                Amount = orders.Sum(x => x.TotalAmount)
            };
        }

        public async Task<DashboardResponse> GetDashboardData(int userId)
        {
            var result = new DashboardResponse();
            var orders = await _context.Orders.Where(x => !x.IsCancelled && !x.IsDeleted).CountAsync();
            var emps = await _context.Employees.Where(x => !x.IsDeleted && x.IsActive).CountAsync();
            var customers = await _context.Customers.Where(x => !x.IsDeleted).Select(x=>x.Contact1).Distinct().CountAsync();
            var design = await _context.DesignSamples.Where(x => !x.IsDeleted).CountAsync();
            var products = await _context.Products.Where(x => !x.IsDeleted).CountAsync();
            var supplires = await _context.Suppliers.Where(x => !x.IsDeleted).CountAsync();
            result.Orders = orders;
            result.Customers = customers;
            result.Employees = emps;
            result.Designs = design;
            result.Suppliers = supplires;
            result.Products = products;
            return result;
        }

        public async Task<List<DashboardSalesResponse>> GetMonthlySales()
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            List<DashboardSalesResponse> dashboardSalesResponses = new List<DashboardSalesResponse>();
            var orders = await _context.Orders
                .Where(x => !x.IsCancelled &&
                !x.IsDeleted &&
                x.OrderDate.Date >= firstDayOfMonth.Date &&
                x.OrderDate.Date <= lastDayOfMonth.Date)
                .ToListAsync();
            var grpData = orders.GroupBy(c => c.OrderDate);
            foreach (var item in grpData)
            {
                dashboardSalesResponses.Add(new DashboardSalesResponse()
                {
                    Name = item.First().OrderDate.ToShortDateString(),
                    Count = item.Count(),
                    Amount = item.Sum(x => x.TotalAmount)
                });
            }
           
            return dashboardSalesResponses;
        }

        public async Task<List<DashboardSalesResponse>> GetWeeklySales()
        {
            DayOfWeek currentDay = DateTime.Now.DayOfWeek;
            int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
            DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);

            List<DashboardSalesResponse> dashboardSalesResponses = new List<DashboardSalesResponse>();
            var orders = await _context.Orders
                .Where(x => !x.IsCancelled &&
                !x.IsDeleted &&
                x.OrderDate.Date >= currentWeekStartDate.Date &&
                x.OrderDate.Date <= DateTime.Today.Date)
                .ToListAsync();
            var grpData = orders.GroupBy(x => x.OrderDate);
            foreach (var item in grpData)
            {
                dashboardSalesResponses.Add(new DashboardSalesResponse()
                {
                    Name = item.First().OrderDate.ToShortDateString(),
                    Count = item.Count(),
                    Amount = item.Sum(x => x.TotalAmount)
                });
            }
           
            return dashboardSalesResponses;
        }

        public async Task<EmployeeDashboardResponse> GetEmployeeDashboard()
        {
            var data = await _context.Employees.Include(x => x.MasterJobTitle).Where(x => !x.IsDeleted).ToListAsync();
            EmployeeDashboardResponse employeeDashboardResponse = new EmployeeDashboardResponse
            {
                Deactives = data.Where(x => !x.IsActive).Count(),
                Employees = data.Where(x => !x.IsFixedEmployee).Count(),
                Staffs = data.Where(x => x.IsFixedEmployee).Count(),
                Members = data.Count(),
                Designers = data.Where(x => x.MasterJobTitle.Code.ToLower().Contains("design")).Count(),
                HandEmbs = data.Where(x => x.MasterJobTitle.Code.ToLower().StartsWith("h") && x.MasterJobTitle.Code.Contains("_emb")).Count(),
                MEmbs = data.Where(x => x.MasterJobTitle.Code.ToLower().StartsWith("m") && x.MasterJobTitle.Code.Contains("_emb")).Count(),
                Apliqs = data.Where(x => x.MasterJobTitle.Code.ToLower().Contains("apli")).Count(),
                HotFixers = data.Where(x => x.MasterJobTitle.Code.ToLower().Contains("hot_fixer")).Count(),
                Stitchers = data.Where(x => x.MasterJobTitle.Code.ToLower().Contains("sticher")).Count(),
            };
            return employeeDashboardResponse;
        }

        public async Task<OrderDashboardResponse> GetOrderDashboard()
        {
            var orderDetails = await _context.OrderDetails
                       .Include(x => x.Order)
                       .Where(x => !x.IsCancelled &&
                       !x.IsDeleted &&
                       !x.Order.IsCancelled &&
                       !x.Order.IsDeleted)
                       .ToListAsync();
                OrderDashboardResponse orderDashboardResponse = new OrderDashboardResponse
                {
                    Orders = orderDetails.Select(x => x.OrderId).Distinct().Count(),
                    Kandoors = orderDetails.Count(),
                    Designs = GetWorkTypeCount(orderDetails,"1"),
                    Cuttings = GetWorkTypeCount(orderDetails, "2"),
                    MEmbs = GetWorkTypeCount(orderDetails, "3"),
                    HFixs = GetWorkTypeCount(orderDetails, "4"),
                    HEmbs = GetWorkTypeCount(orderDetails, "5"),
                    Apliqs = GetWorkTypeCount(orderDetails, "7"),
                    Stitches = GetWorkTypeCount(orderDetails, "6")
                };
                return orderDashboardResponse;
        }

        public async Task<ExpenseDashboardResponse> GetExpenseDashboard()
        {
            var expenses = await _context.Expenses
                       .Include(x => x.Employee)
                       .Include(x=>x.ExpenseShopCompany)
                       .Include(x=>x.JobTitle)
                       .Include(x => x.ExpenseName)
                       .ThenInclude(x=>x.ExpenseType)
                       .Where(x => !x.IsDeleted)
                       .ToListAsync();
            var suppliers = await _context.Suppliers.Where(x => !x.IsDeleted).CountAsync();
            var purchases = await _context.PurchaseEntryDetails.Where(x => !x.IsDeleted).SumAsync(x=>x.TotalPrice);
            ExpenseDashboardResponse expenseDashboardResponse = new ExpenseDashboardResponse()
            {
                ExpCash = expenses.Where(x => string.IsNullOrEmpty(x.PaymentMode) || x.PaymentMode.ToUpper() == PaymentModeEnum.Cash.ToString().ToUpper()).Sum(x => x.Amount),
                ExpVisa = expenses.Where(x => !string.IsNullOrEmpty(x.PaymentMode) && x.PaymentMode.ToUpper() == PaymentModeEnum.Visa.ToString().ToUpper()).Sum(x => x.Amount),
                ExpCheque = expenses.Where(x => !string.IsNullOrEmpty(x.PaymentMode) && x.PaymentMode.ToUpper() == PaymentModeEnum.Cheque.ToString().ToUpper()).Sum(x => x.Amount),
                Expenses = expenses.Sum(x => x.Amount),
                Suppliers = suppliers,
                Rent = (int)expenses.Where(x => x.ExpenseName.ExpenseType.Value.ToLower().Contains("rent")).Sum(x => x.Amount),
                Purchases = (int)purchases
            };

            return expenseDashboardResponse;
        }

        private int GetWorkTypeCount(List<OrderDetail> orderDetails,string workType)
        {
            return orderDetails.Where(x => x.WorkType.Contains(workType)).Count();
        }

        public async Task<CrystalDeashboardResponse> GetCrystalDashboard()
        {
           var data=await _context.CrystalStocks
                .Include(x=>x.MasterCrystal)
                .Where(x => !x.IsDeleted).ToListAsync();
            var todayStock = await _context.CrystalPurchaseDetails
                .Include(x => x.CrystalPurchase)
                .Where(x => !x.IsCancelled && !x.IsDeleted && x.CrystalPurchase.CreatedAt.Date == DateTime.Now.Date)
                .ToListAsync();
            var todayUsedStock = await _context.CrystalTrackingOutDetails
                .Include(x=>x.CrystalTrackingOut)
               .Where(x =>  !x.IsDeleted && x.CrystalTrackingOut.ReleaseDate.Date == DateTime.Now.Date)
               .ToListAsync();
            return new CrystalDeashboardResponse()
            {
                BalancePiece = data.Sum(x => x.BalanceStockPieces),
                BalanceQty = data.Sum(x => x.BalanceStock),
                BuyPiece = data.Sum(x => x.InStockPieces),
                BuyQty = data.Sum(x => x.InStock),
                UsedPiece = data.Sum(x => x.OutStockPieces),
                UsedQty = data.Sum(x => x.OutStock),
                LowCrystalStockCount = data.Count(x => x.BalanceStock <= x.MasterCrystal.AlertQty),
                TodayBuyPiece= todayStock.Sum(x => x.TotalPiece),
                TodayBuyQty = todayStock.Sum(x => x.Qty),
                TodayUsedPiece= todayUsedStock.Sum(x => x.ReleasePieceQty),
                TodayUsedQty = todayUsedStock.Sum(x => x.ReleasePacketQty),
            };
        }
    }
}
