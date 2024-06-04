using Microsoft.EntityFrameworkCore;
using MKExpress.API.Contants;
using MKExpress.API.Data;
using MKExpress.API.Exceptions;
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
            var roleId = req[0].RoleId;
            var trans=_context.Database.BeginTransaction();
            if (await DeleteByRoleId(roleId))
            {
                //var menuIds = req.Select(x => x.MenuId).ToList();
                //var existingMenuRoles = await _context.UserRoleMenuMappers
                //    .Where(mr => !mr.IsDeleted && mr.RoleId == roleId && menuIds.Contains(mr.MenuId))
                //    .ToListAsync();

                //if (existingMenuRoles.Any())
                //{
                //    throw new InvalidOperationException("One or more Menu are already associated with the given Role");
                //}
                _context.AddRange(req);
                if(await _context.SaveChangesAsync() > 0)
                {
                    trans.Commit();
                    return true;
                }
            }
            trans.Rollback();
            return false;
        }

        public async Task<bool> DeleteByRoleId(Guid RoleId)
        {
            var oldData=await _context.UserRoleMenuMappers.Where(mr => !mr.IsDeleted &&mr.RoleId==RoleId)
                .ToListAsync();
            if (!oldData.Any())
            {
                return true;
            }

            oldData.ForEach(x => x.IsDeleted = true);
            _context.RemoveRange(oldData);
            return await _context.SaveChangesAsync()>0;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var oldData = await _context.UserRoleMenuMappers.Where(mr => !mr.IsDeleted && mr.Id == id)
                .FirstOrDefaultAsync()??throw new BusinessRuleViolationException(StaticValues.ErrorType_RecordNotFound,StaticValues.Error_RecordNotFound);

            oldData.IsDeleted= true;
            _context.Update(oldData);
            return await _context.SaveChangesAsync() > 0;
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
