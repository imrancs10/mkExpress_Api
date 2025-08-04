using Microsoft.EntityFrameworkCore;
using MkExpress.MessageBroker.Services;
using MKExpress.API.Data;
using MKExpress.API.Logger;
using MKExpress.API.Repositories;
using MKExpress.API.Repository;
using MKExpress.API.Services;
using MKExpress.API.Utility;

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
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<IUserRoleService, UserRoleService>()
                .AddScoped<IUserRoleRepository, UserRoleRepository>()
                .AddScoped<IMenuRepository, MenuRepository>()
                .AddScoped<IMenuService, MenuService>()
                .AddScoped<IMasterJourneyRepository, MasterJourneyRepository>()
                .AddScoped<IMasterJourneyService, MasterJourneyService>()
                .AddScoped<IUserRoleMenuMapperRepository, UserRoleMenuMapperRepository>()
                .AddScoped<IUserRoleMenuMapperService, UserRoleMenuMapperService>()
                .AddScoped<IContainerRepository, ContainerRepository>()
                .AddScoped<IContainerService, ContainerService>()
                .AddScoped<IAppSettingRepository, AppSettingRepository>()
                .AddScoped<IAppSettingService, AppSettingService>()
                .AddScoped<IThirdPartyCourierRepository, ThirdPartyCourierRepository>()
                .AddScoped<IThirdPartyCourierService, ThirdPartyCourierService>()
                .AddScoped<IShipmentTrackingRepository, ShipmentTrackingRepository>()
                .AddScoped<IShipmentImageRepository,ShipmentImageRepository>()
                .AddScoped<ISystemActionRepository, SystemActionRepository>()
                .AddScoped<ISystemActionService, SystemActionService>()
                .AddScoped<IMobileApiRepository, MobileApiRepository>()
                .AddScoped<IMobileApiService, MobileApiService>()
                .AddScoped<IAssignShipmentMemberRepository, AssignShipmentMemberRepository>()
                .AddScoped<IAssignShipmentMemberService, AssignShipmentMemberService>()
                .AddScoped<IDashboardRepository, DashboardRepository>()
                .AddScoped<IDashboardService, DashboardService>()
                .AddScoped<IFileUploadService, FileUploadService>()
                .AddScoped<ICommonService, CommonService>()
                .AddScoped<IEmailService, EmailService>()
                .AddScoped<ITemplateService, TemplateService>()
                .AddScoped<ISmsService, SmsService>()
                .AddScoped<INotificationService, NotificationService>()
                .AddScoped<IExcelReader, ExcelReader>()
                .AddSingleton<ILoggerManager, LoggerManager>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services,IConfiguration configuration)
        {
            var DefaultConnection=configuration.GetConnectionString("DefaultConnection");
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