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
using System;
using System.Threading.Tasks;

namespace MKExpress.API.Middleware
{
    public static class Registrar
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IJobTitleRepository, JobTitleRepository>()
                .AddScoped<IJobTitleService, JobTitleService>()
                .AddScoped<IDropdownRepository, DropdownRepository>()
                .AddScoped<IDropdownService, DropdownService>()
                .AddScoped<TokenConfig>()
                .AddTransient<DataSeeder>()
                .AddTransient<IMailService, MailService>()
                .AddScoped<IHealthService, HealthService>()
                .AddScoped<IHealthRepository, HealthRepository>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection RegisterDataServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var dbName = new string[] { "MKExpress_Db_Test", "MKExpress_Db", "local" };
            int dbIndex = 1;
            var connectionString = "";
            if (dbName[dbIndex] == "local")
                connectionString = "workstation id=mssql-88450-0.cloudclusters.net,12597;packet size=4096;user id=mkExpress_User;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=mkExpress_Db";
            else
                connectionString = $"workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;packet size=4096;user id=mkExpress_User;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog={dbName[dbIndex]}";

            services.AddDbContext<MKExpressDbContext>(
                options =>
                {
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
            //db.Database.Migrate();
        }

        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            //scope.ServiceProvider.GetRequiredService<MKExpressDbContext>().Database.Migrate();
        }
    }
}