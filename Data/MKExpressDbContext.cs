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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<MasterJobTitle> MasterJobTitles { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }
        public DbSet<MasterData> MasterDatas { get; set; }
        public DbSet<MasterDataType> MasterDataTypes { get; set; }
        public DbSet<PermissionResource> PermissionResources { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public DbSet<MasterAccess> MasterAccesses { get; set; }
        public DbSet<MasterAccessDetail> MasterAccessDetail { get; set; }
        public DbSet<MasterMenu> MasterMenus { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Station> Stations { get; set; }
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