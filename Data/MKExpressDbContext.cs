using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Models;
using MKExpress.API.Models.BaseModels;
using MKExpress.Web.API.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MKExpress.API.Data
{

    public class MKExpressDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MKExpressDbContext(DbContextOptions<MKExpressDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<CustomerAccountStatement> CustomerAccountStatements { get; set; }
        public DbSet<MasterHoliday> MasterHolidays { get; set; }
        public DbSet<MasterHolidayName> MasterHolidayNames { get; set; }
        public DbSet<MasterHolidayType> MasterHolidayTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CuttingMaster> CuttingMasters { get; set; }
        public DbSet<CancelledOrder> CancelledOrders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<ExpenseName> ExpenseNames { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseShopCompany> ExpenseShopCompanies { get; set; }
        public DbSet<MasterJobTitle> MasterJobTitles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MonthlyAttendence> MonthlyAttendences { get; set; }
        public DbSet<MasterDesignCategory> MasterDesignCategories { get; set; }
        public DbSet<DesignSample> DesignSamples { get; set; }  
        public DbSet<FileStorage> FileStorages { get; set; }
        public DbSet<MasterData> MasterDatas { get; set; }
        public DbSet<MasterDataType> MasterDataTypes { get; set; }
        public DbSet<CustomerMeasurement> CustomerMeasurements { get; set; }
        public DbSet<PurchaseEntry> PurchaseEntries { get; set; }
        public DbSet<PurchaseEntryDetail> PurchaseEntryDetails { get; set; }
        public DbSet<WorkTypeStatus> WorkTypeStatuses { get; set; }
        public DbSet<EachKandooraExpenseHead> EachKandooraExpenseHeads { get; set; }
        public DbSet<EachKandooraExpense> EachKandooraExpenses { get; set; }
        public DbSet<EmployeeAdvancePayment> EmployeeAdvancePayments { get; set; }
        public DbSet<EmployeeEMIPayment> EmployeeEMIPayments { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<PermissionResource> PermissionResources { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<OrderUsedCrystal> OrderUsedCrystals { get; set; }
        public DbSet<RentLocation> RentLocations { get; set; }
        public DbSet<RentDetail> RentDetails { get; set; }
        public DbSet<RentTransation> RentTransations { get; set; }
        public DbSet<MasterWorkDescription> MasterWorkDescriptions { get; set; }
        public DbSet<OrderWorkDescription> OrderWorkDescriptions { get; set; } 
        public DbSet<MasterCrystal> MasterCrystals { get; set; }
        public DbSet<CrystalPurchase> CrystalPurchases { get; set; }
        public DbSet<CrystalPurchaseDetail> CrystalPurchaseDetails { get; set; }
        public DbSet<CrystalPurchaseInstallment> CrystalPurchaseInstallments { get; set; }
        public DbSet<CrystalStock> CrystalStocks { get; set; }
        public DbSet<CrystalStockUpdateHistory> CrystalStockUpdateHistories { get; set; }
        public DbSet<CrystalTrackingOut> CrystalTrackingOuts { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<CrystalTrackingOutDetail> CrystalTrackingOutDetails { get; set; }
        public DbSet<MasterAccess> MasterAccesses { get; set; }
        public DbSet<MasterAccessDetail> MasterAccessDetail { get; set; }
        public DbSet<MasterMenu> MasterMenus { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var _connectionString =
                "workstation id=mssql-88450-0.cloudclusters.net,12597;packet size=4096;user id=mkExpress_User;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=mkExpress_Db";
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public override int SaveChanges()
        {
            AddDateTimeStamp();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddDateTimeStamp();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        private void AddDateTimeStamp()
        {
            foreach (var entityEntry in ChangeTracker.Entries())
            {
                var hasChange = entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified;
                if (!hasChange) continue;
                if (!(entityEntry.Entity is BaseModel baseModel)) continue;
                var now = DateTime.Now;
                int? userId = null;
                if ((bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("userId"))
                {
                    string value = _httpContextAccessor.HttpContext?.Request.Headers["userId"].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (int.TryParse(value, out int newUserId))
                        {
                            userId = newUserId;
                        }
                    }
                }
                if (entityEntry.State is EntityState.Added)
                {
                    baseModel.CreatedBy = null;
                    baseModel.CreatedAt = now;
                }
                else
                {
                    baseModel.UpdatedBy = userId;
                    entityEntry.Property("CreatedAt").IsModified = false;
                    baseModel.UpdatedAt = now;
                }
            }
        }
    }
}