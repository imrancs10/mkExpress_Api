using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Data
{

    public class DataSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly MKExpressDbContext _MKExpressDbContext;

        public DataSeeder( RoleManager<IdentityRole> roleManager,
            MKExpressDbContext MKExpressDbContext)
        {
            _roleManager = roleManager;
            _MKExpressDbContext = MKExpressDbContext;
        }

        public async Task Seed()
        {
            await SeedRoles();
        }

        private async Task SeedRoles()
        {
            if (!_roleManager.Roles.Any())
            {
                // var roles =
                //     _csvFileReaderService.ReadCsvFile<User, RoleMap>(StaticValues
                //         .RoleResourceName).Select(role=>new IdentityUserRole<string>(){RoleId = role.Role});
                //
                //foreach (var role in roles)
                //{
                // var con=  _MKExpressDbContext.Database.GetDbConnection();
                //await _roleManager.CreateAsync(role);
                //await _MKExpressDbContext.UserRoles.AddRangeAsync(roles);
                //await _MKExpressDbContext.SaveChangesAsync();
                //}
            }
        }
    }
}