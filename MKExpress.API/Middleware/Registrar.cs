using MKExpress.API.Config;
using MKExpress.API.Data;
using MKExpress.API.Repository;
using MKExpress.API.Repository.IRepository;
using MKExpress.API.Services;
using MKExpress.API.Services.Interfaces;
using MKExpress.API.Services.IServices;
using MKExpress.API.Utility;
using Microsoft.EntityFrameworkCore;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Repositories;
using SalehGarib.API.Repositories;

namespace MKExpress.API.Middleware
{
    public static class Registrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginRepository, LoginRepository>()
                .AddScoped<ILoginService, LoginService>()
                .AddScoped<IMasterDataService, MasterDataService>()
                .AddScoped<IMasterDataRepository, MasterDataRepository>()
                .AddScoped<IMasterDataTypeService, MasterDataTypeService>()
                .AddScoped<IMasterDataTypeRepository, MasterDataTypeRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<ILogisticRegionRepository, LogisticRegionRepository>()
                .AddScoped<ILogisticRegionSerivce, LogisticRegionSerivce>()
                .AddScoped<IShipmentRepository, ShipmentRepository>()
                .AddScoped<IShipmentService, ShipmentService>()
                 .AddScoped<IShipmentTrackingRepository, ShipmentTrackingRepository>()
                .AddScoped<IMailService, MailService>()
                .AddScoped<IExcelReader, ExcelReader>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            //var DefaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=KaashiYatri";
            var DefaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=mkExpress_User;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=mkExpress_Db_test";
            services.AddDbContext<MKExpressContext>(
                options => { options.UseSqlServer(DefaultConnection); }
            );
            var serviceProvider = services.BuildServiceProvider();
            //ApplyMigrations(serviceProvider);
            return services;
        }


        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MKExpressContext>();
            //db.Database.Migrate();
        }
    }
}