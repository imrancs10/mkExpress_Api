using MKExpress.API.Config;
using MKExpress.API.DTO.Response;
using MKExpress.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Contants;
using MKExpress.API.Services.IServices;
using Microsoft.EntityFrameworkCore.Metadata;
using MKExpress.API.Middleware;

namespace MKExpress.API.Data
{
    public class MKExpressContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private readonly string _defaultConnection;

        public MKExpressContext(IConfiguration configuration)
        {
            Configuration = configuration;
            //_defaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=LaBeachUser;pwd=Gr8@54321;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=KaashiYatri";
            _defaultConnection = "workstation id=mssql-88450-0.cloudclusters.net,12597;TrustServerCertificate=true;user id=mkExpress_User;pwd=Gr8@12345;data source=mssql-88450-0.cloudclusters.net,12597;persist security info=False;initial catalog=mkExpress_Db_test";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

            options.UseSqlServer(_defaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder
                .Entity<Container>()
                .Property(x=>x.ContainerNo)
                .ValueGeneratedOnAdd()// this will add Autogenerate Contain No. on add
                .Metadata
                .SetAfterSaveBehavior(PropertySaveBehavior.Ignore);  // this will prevent EF to insert new value in Container No. on update
           
            modelBuilder
                .Entity<Shipment>()
                .Property(x => x.CODAmount)
                .HasColumnType("decimal")
                .HasPrecision(5);

            modelBuilder
               .Entity<ShipmentDetail>()
               .Property(x => x.Weight)
               .HasColumnType("decimal")
               .HasPrecision(5);
            modelBuilder.Entity<ShipmentTracking>()
                .Property(x => x.CommentBy)
                .IsRequired(false);

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
        public DbSet<Container> Containers { get; set; }
        public DbSet<ContainerDetail> ContainerDetails { get; set; }
        public DbSet<ContainerJourney> ContainerJourneys { get; set; }
        public DbSet<LogisticRegion> LogisticRegions { get; set; }
        public DbSet<MasterJourney> MasterJouneys { get; set; }
        public DbSet<MasterJourneyDetail> MasterJourneyDetails { get; set; }
        public DbSet<ContainerTracking> ContainerTrackings { get; set; }
        public DbSet<ThirdPartyCourierCompany> ThirdPartyCourierCompanies { get; set; }
        public DbSet<ThirdPartyShipment> ThirdPartyShipments { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<AssignShipmentMember> AssignShipmentMembers { get; set; }
        public DbSet<AppSettingGroup> AppSettingGroups { get; set; }
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
                Guid? userId = JwtMiddleware.GetUserId();
               
                if (entityEntry.State is EntityState.Added)
                {
                    baseModel.CreatedBy = userId;
                    baseModel.CreatedAt = DateTime.Now;
                }
                else
                {
                    baseModel.UpdatedBy = userId;
                    entityEntry.Property(StaticValues.ConstValue_CreatedAt).IsModified = false;
                    baseModel.UpdatedAt = DateTime.Now;
                }
            }
        }
    }
}
