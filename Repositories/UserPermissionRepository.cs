using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MKExpress.API.Data;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly MKExpressDbContext _context;
        public UserPermissionRepository(MKExpressDbContext context)
        {
            _context = context;
        }

        public async Task<PermissionResource> AddPermissionResource(PermissionResource permissionResource)
        {
            if (permissionResource == null || string.IsNullOrEmpty(permissionResource.Name)) return permissionResource;
            List<string> resources = permissionResource.Name.Split(",").ToList();
            List<PermissionResource> permissionResourceList = new List<PermissionResource>();

            resources.ForEach(x =>
            {
                permissionResourceList.Add(new PermissionResource()
                {
                    Code = x.ToLower(),
                    Name = x,
                    ResourceTypeId = permissionResource.ResourceTypeId
                });
            });
            await _context.PermissionResources.AddRangeAsync(permissionResourceList);
            await _context.SaveChangesAsync();
            return permissionResource;
        }

        public async Task<ResourceType> AddResourceType(ResourceType resourceType)
        {
            var entity = _context.ResourceTypes.Attach(resourceType);
            entity.State = EntityState.Added;
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<int> DeletePermissionResource(int permissionResourceId)
        {
            PermissionResource permissionResource = await _context.PermissionResources
                .Where(pr => pr.Id == permissionResourceId)
                .FirstOrDefaultAsync();

            if (permissionResource != null)
            {
                permissionResource.IsDeleted = true;
                var entity = _context.PermissionResources.Update(permissionResource);
                entity.State = EntityState.Modified;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    var permissionResources = await _context.UserPermissions.Where(x => x.PermissionResourceId == permissionResourceId).ToListAsync();
                    if (permissionResources.Count > 0)
                    {
                        permissionResources.ForEach(x => x.IsDeleted = true);
                        _context.UserPermissions.UpdateRange(permissionResources);
                        await _context.SaveChangesAsync();
                    }
                }
                return result;
            }
            return 0;
        }

        public async Task<int> DeleteResourceType(int resourceTypeId)
        {
            ResourceType resourceType = await _context.ResourceTypes
              .Where(pr => pr.Id == resourceTypeId)
              .FirstOrDefaultAsync();

            if (resourceType != null)
            {
                resourceType.IsDeleted = true;
                var entity = _context.ResourceTypes.Update(resourceType);
                entity.State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<List<UserPermission>> GetAll(int roleId)
        {
            var result = await _context.UserPermissions
                .Include(x => x.UserRole)
                .Include(x => x.PermissionResource)
                .ThenInclude(x => x.ResourceType)
                .Where(x => (roleId == 0 || x.UserRoleId == roleId) && !x.IsDeleted)
                .OrderBy(x => x.PermissionResource.Name)
                .ToListAsync();
            return result;
        }

        public async Task<List<UserPermission>> GetPermissionByRoleName(string roleName)
        {
            return await _context.UserPermissions
                 .Include(x => x.UserRole)
                 .Include(x => x.PermissionResource)
                 .ThenInclude(x => x.ResourceType)
                 .Where(x => x.UserRole.Name == roleName && !x.IsDeleted)
                 .OrderBy(x => x.PermissionResource.Name)
                 .ToListAsync();
        }

        public async Task<List<PermissionResource>> GetPermissionResources()
        {
            return await _context.PermissionResources.Where(x => !x.IsDeleted)
                .Include(x => x.ResourceType)
                .OrderBy(x => x.ResourceType.Name)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<List<ResourceType>> GetResourceTypes()
        {
            return await _context.ResourceTypes.Where(x => !x.IsDeleted)
               .OrderBy(x => x.Name)
               .ToListAsync();
        }

        public async Task<List<UserRole>> GetUserRoles()
        {
            return await _context.UserRoles
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<bool> HasUserPermission(int roleId)
        {
            return await _context.UserPermissions.Where(x => !x.IsDeleted && x.UserRoleId == roleId).CountAsync() > 0;
        }

        public async Task<List<UserPermission>> Search(string searchTerm, int userId)
        {
            var result = await _context.UserPermissions
                 .Include(x => x.UserRole)
                .Include(x => x.PermissionResource)
                .ThenInclude(x => x.ResourceType)
                .Where(x => x.UserRoleId == userId && x.PermissionResource.Name.Contains(searchTerm) && !x.IsDeleted)
                .OrderBy(x => x.PermissionResource.Name)
                .ToListAsync();
            return result;
        }

        public async Task<int> SetDefaultUserPermission(int roleId)
        {
            var hasRole = await _context.UserRoles.Where(x => x.Id == roleId).FirstOrDefaultAsync();
            if (hasRole == null) return 0;
            var permissionResources = await _context
                .PermissionResources
                .Where(x => !x.IsDeleted)
                .ToListAsync();
            if (permissionResources.Count > 0)
            {
                var userPermissions = new List<UserPermission>();
                permissionResources.ForEach(x =>
                {
                    userPermissions.Add(new UserPermission()
                    {
                        UserRoleId = roleId,
                        PermissionResourceId = x.Id
                    });
                });
                _context.AttachRange(userPermissions);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> Update(List<UserPermission> userPermissions)
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE UserPermissions");


            await _context.UserPermissions.AddRangeAsync(userPermissions);
            return await _context.SaveChangesAsync();
        }

        public async Task<PermissionResource> UpdatePermissionResource(PermissionResource permissionResource)
        {
            EntityEntry<PermissionResource> oldPermissionResource = _context.Update(permissionResource);
            oldPermissionResource.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldPermissionResource.Entity;
        }

        public async Task<ResourceType> UpdateResourceType(ResourceType resourceType)
        {
            EntityEntry<ResourceType> oldResourceType = _context.Update(resourceType);
            oldResourceType.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return oldResourceType.Entity;
        }
    }
}
