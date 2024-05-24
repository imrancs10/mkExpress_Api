using Microsoft.EntityFrameworkCore;
using MKExpress.API.Data;
using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public class UserRoleMenuMapperRepository : IUserRoleMenuMapperRepository
    {
        private readonly MKExpressContext _context;
        public UserRoleMenuMapperRepository(MKExpressContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(List<UserRoleMenuMapper> req)
        {
            if(req == null || req.Count==0) {
                throw new ArgumentNullException(nameof(req));
            }
            var roleId = req[0].Id;
            var menuIds=req.Select(x=>x.MenuId).ToList();
            var existingMenuRoles = await _context.UserRoleMenuMappers
                .Where(mr =>!mr.IsDeleted && mr.RoleId==roleId && menuIds.Contains( mr.MenuId))
                .ToListAsync();

            if (existingMenuRoles.Any())
            {
                throw new InvalidOperationException("One or more Menu are already associated with the given Role");
            }
            _context.AddRange(req);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> Delete(Guid RoleId)
        {
            var oldData=await _context.UserRoleMenuMappers.Where(mr => !mr.IsDeleted &&mr.RoleId==RoleId)
                .ToListAsync();
            if (!oldData.Any())
            {
                throw new InvalidOperationException("No record found for given role");
            }

            oldData.ForEach(x => x.IsDeleted = true);
            _context.UpdateRange(oldData);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<List<UserRoleMenuMapper>> GetAll()
        {
            return await _context.UserRoleMenuMappers
                .Include(x=>x.UserRole)
                .Include(x=>x.Menu)
                .Where(mr => !mr.IsDeleted)
                .OrderBy(x=>x.UserRole.Name)
                .ToListAsync();
        }

        public async Task<List<UserRoleMenuMapper>> GetByRoleId(Guid roleId)
        {
            return await _context.UserRoleMenuMappers
                .Include(x => x.UserRole)
                .Include(x => x.Menu)
                .Where(mr => !mr.IsDeleted && mr.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<List<UserRoleMenuMapper>> SearchRolesAsync(string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm;
            return await _context.UserRoleMenuMappers
                .Include(x => x.UserRole)
                .Include(x => x.Menu)
                .Where(mr => !mr.IsDeleted && (searchTerm=="all" || mr.UserRole.Name.Contains(searchTerm) || mr.Menu.Name.Contains(searchTerm) ))
                .ToListAsync();
        }
    }
}
