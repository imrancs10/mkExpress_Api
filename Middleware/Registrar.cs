using CatalogService.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MKExpress.API.Config;
using MKExpress.API.Data;
using MKExpress.API.Repositories;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services;
using MKExpress.API.Services.Interfaces;
using MKExpress.Web.API.Repositories;
using MKExpress.Web.API.Repositories.Interfaces;
using MKExpress.Web.API.Services;
using MKExpress.Web.API.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MKExpress.API.Middleware
{
    public static class Registrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IDashboardRepository, DashboardRepository>()
                .AddScoped<IDashboardService, DashboardService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IDesignSampleRepository, DesignSampleRepository>()
                .AddScoped<IDesignSampleService, DesignSampleService>()
                .AddScoped<IJobTitleRepository, JobTitleRepository>()
                .AddScoped<IJobTitleService, JobTitleService>()
                .AddScoped<IFileStorageRepository, FileStorageRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IPurchaseEntryRepository, PurchaseEntryRepository>()
                .AddScoped<IPurchaseEntryService, PurchaseEntryService>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IFileUploadService, FileUploadService>()
                .AddScoped<IFileStorageService, FileStorageService>()
                .AddScoped<IDesignCategoryRepository, DesignCategoryRepository>()
                .AddScoped<IDesignCategoryService, DesignCategoryService>()
                .AddScoped<ISupplierRepository, SupplierRepository>()
                .AddScoped<ISupplierService, SupplierService>()
                .AddScoped<IDropdownRepository, DropdownRepository>()
                .AddScoped<IDropdownService, DropdownService>()
                .AddScoped<IMonthlyAttendenceRepository, MonthlyAttendenceRepository>()
                .AddScoped<IMonthlyAttendenceService, MonthlyAttendenceService>()
                .AddScoped<IMasterDataRepository, MasterDataRepository>()
                .AddScoped<IMasterDataService, MasterDataService>()
                .AddScoped<ICustomerAccountStatementRespository, CustomerAccountStatementRespository>()
                .AddScoped<ICustomerAccountStatementService, CustomerAccountStatementService>()
                .AddScoped<ICustomerMeasurementRepository, CustomerMeasurementRepository>()
                .AddScoped<ICustomerMeasurementService, CustomerMeasurementService>()
                .AddScoped<IMasterDataTypeRepository, MasterDataTypeRepository>()
                .AddScoped<IMasterDataTypeService, MasterDataTypeService>()
                .AddScoped<IKandooraExpenseService, KandooraExpenseService>()
                .AddScoped<IKandooraExpenseRepository, KandooraExpenseRepository>()
                .AddScoped<IWorkTypeStatusRepository, WorkTypeStatusRepository>()
                .AddScoped<IWorkTypeStatusService, WorkTypeStatusService>()
                .AddScoped<IEmployeeAdvancePaymentRepository, EmployeeAdvancePaymentRepository>()
                .AddScoped<IEmployeeEMIPaymentRepository, EmployeeEMIPaymentRepository>()
                .AddScoped<IEmployeeAdvancePaymentService, EmployeeAdvancePaymentService>()
                .AddScoped<IUserPermissionRepository, UserPermissionRepository>()
                .AddScoped<IUserPermissionService, UserPermissionService>()
                .AddScoped<IProductTypeRepository, ProductTypeRepository>()
                .AddScoped<IProductTypeService, ProductTypeService>()
                .AddScoped<IProductTypeService, ProductTypeService>()
                .AddScoped<IProductStockRepository, ProductStockRepository>()
                .AddScoped<IProductStockService, ProductStockService>()
                .AddScoped<IMasterHolidayNameRepository, MasterHolidayNameRepository>()
                .AddScoped<IMasterHolidayTypeRepository, MasterHolidayTypeRepository>()
                .AddScoped<IMasterHolidayRepository, MasterHolidayRepository>()
                .AddScoped<IMasterHolidayNameService, MasterHolidayNameService>()
                .AddScoped<IMasterHolidayTypeService, MasterHolidayTypeService>()
                .AddScoped<IMasterHolidayService, MasterHolidayService>()
                .AddScoped<IAccountRepository, AccountRepository>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IExpenseNameRepository, ExpenseNameRepository>()
                .AddScoped<IExpenseShopCompanyRepository, ExpenseShopCompanyRepository>()
                .AddScoped<IExpenseShopCompanyService, ExpenseShopCompanyService>()
                .AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>()
                .AddScoped<IExpenseRepository, ExpenseRepository>()
                .AddScoped<IExpenseNameService, ExpenseNameService>()
                .AddScoped<IExpenseTypeService, ExpenseTypeService>()
                .AddScoped<IExpenseService, ExpenseService>()
                .AddScoped<IRentDetailRepository,RentDetailRepository>()
                .AddScoped<IRentDetailService,RentDetailService>()
                .AddScoped<IRentLocationRepository, RentLocationRepository>()
                .AddScoped<IRentLocationService, RentLocationService>()
                .AddScoped<IReportRepository,ReportRepository>()
                .AddScoped<IReportService, ReportService>()
                .AddScoped<IMasterWorkDescriptionRepository,MasterWorkDescriptionRepository>()
                .AddScoped<IMasterWorkDescriptionService, MasterWorkDescriptionService>()
                 .AddScoped<IMasterAccessRepository, MasterAccessRepository>()
                .AddScoped<IMasterAccessService, MasterAccessService>()
                .AddScoped<TokenConfig>()
                .AddTransient<DataSeeder>()
                .AddTransient<IMailService, MailService>()
                .AddScoped<IMasterCrystalRepository, MasterCrystalRepository>()
                .AddScoped<IMasterCrystalService, MasterCrystalService>()
                .AddScoped<ICrystalPurchaseRepository, CrystalPurchaseRepository>()
                .AddScoped<ICrystalPurchaseService, CrystalPurchaseService>()
                .AddScoped<ICrystalPurchaseEmiRepository, CrystalPurchaseEmiRepository>()
                .AddScoped<ICrystalStockRepository, CrystalStockRepository>()
                .AddScoped<ICrystalStockService, CrystalStockService>()
                .AddScoped<ICrystalTrackingOutService, CrystalTrackingOutService>()
                .AddScoped<ICrystalTrackingOutRepository, CrystalTrackingOutRepository>()
                .AddScoped<IHealthService, HealthService>()
                .AddScoped<IHealthRepository, HealthRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<ICsvFileReaderService, CsvFileReaderService>();
            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbName = new string[] { "LaBeach", "GreenTower", "MKExpress", "AbuMansour", "local" };
            int dbIndex =0;
            var connectionString = "";
            if (dbName[dbIndex] == "local")
                connectionString = "workstation id=MKExpress.mssql.somee.com;packet size=4096;user id=amendutt_SQLLogin_1;pwd=4wwnhqgv8b;data source=MKExpress.mssql.somee.com;persist security info=False;initial catalog=MKExpress";
            else
                connectionString = $"workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;packet size=4096;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog={dbName[dbIndex]}";

            services.AddDbContext<MKExpressDbContext>(
                options => {
                    options.UseSqlServer(connectionString);
                }
            );
            var serviceProvider = services.BuildServiceProvider();
            ApplyMigrations(serviceProvider);
            return services;
        }

        public static IServiceCollection RegisterAuthServices(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MKExpressDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
            });
            return services;
        }

        public static async Task SeedSampleData(this IServiceProvider serviceProvider)
        {
            var dataSeeder = serviceProvider.GetRequiredService<DataSeeder>();
            await dataSeeder.Seed();
        }

        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MKExpressDbContext>();
           // db.Database.Migrate();
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            //scope.ServiceProvider.GetRequiredService<MKExpressDbContext>().Database.Migrate();
        }
    }
}