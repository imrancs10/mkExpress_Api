using MKExpress.API.Config;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Contants;

namespace MKExpress.API.Data
{
    public class MKExpressContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private readonly string _defaultConnection;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MKExpressContext(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            //_defaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=KaashiYatri";
            _defaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=mkExpress_User;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=mkExpress_Db_test";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer(_defaultConnection);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MasterData> MasterDatas { get; set; }
        public DbSet<MasterDataType> MasterDataTypes { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public DbSet<ShipmentImage>? ShipmentImages { get; set; }
        public DbSet<ShipmentTracking> ShipmentTrackings { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<LogisticRegion> LogisticRegions { get; set; }
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
                if (entityEntry.Entity is not BaseModel baseModel) continue;
                var now = DateTime.Now;
                Guid? userId = null;
                if ((bool)_httpContextAccessor.HttpContext?.Request.Headers.ContainsKey(StaticValues.ConstValue_UserId))
                {
                    string? value = _httpContextAccessor.HttpContext?.Request.Headers[StaticValues.ConstValue_UserId].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        if (Guid.TryParse(value, out Guid newUserId))
                        {
                            userId = newUserId;
                        }
                    }
                }
                if (entityEntry.State is EntityState.Added)
                {
                    baseModel.CreatedBy = 0;
                    baseModel.CreatedAt = now;
                }
                else
                {
                    baseModel.UpdatedBy = 0;
                    entityEntry.Property(StaticValues.ConstValue_CreatedAt).IsModified = false;
                    baseModel.UpdatedAt = now;
                }
            }
        }
    }
}
