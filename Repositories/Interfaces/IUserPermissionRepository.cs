using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IUserPermissionRepository
    {
        Task<List<UserPermission>> GetAll(int roleId);
        Task<List<UserPermission>> GetPermissionByRoleName(string roleName);
        Task<List<UserPermission>> Search(string searchTerm, int userId);
        Task<int> Update(List<UserPermission> userPermissions);
        Task<ResourceType> AddResourceType(ResourceType resourceType);
        Task<List<ResourceType>> GetResourceTypes();
        Task<List<PermissionResource>> GetPermissionResources();
        Task<ResourceType> UpdateResourceType(ResourceType resourceType);
        Task<int> DeleteResourceType(int resourceTypeId);
        Task<PermissionResource> AddPermissionResource(PermissionResource permissionResource);
        Task<PermissionResource> UpdatePermissionResource(PermissionResource permissionResource);
        Task<int> DeletePermissionResource(int permissionResourceId);
        Task<int> SetDefaultUserPermission(int userId);
        Task<bool> HasUserPermission(int userId);
        Task<List<UserRole>> GetUserRoles();
    }
}
