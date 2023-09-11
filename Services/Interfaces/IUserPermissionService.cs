using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IUserPermissionService
    {
        Task<List<UserPermissionResponse>> GetAll(int roleId);
        Task<List<UserPermissionResponse>> Search(string searchTerm, int roleId);
        Task<int> Update(List<UserPermissionRequest> userPermissions);
        Task<ResourceTypeResponse> AddResourceType(ResourceTypeRequest resourceType);
        Task<List<ResourceTypeResponse>> GetResourceTypes();
        Task<List<PermissionResourceResponse>> GetPermissionResources();
        Task<ResourceTypeResponse> UpdateResourceType(ResourceTypeRequest resourceType);
        Task<int> DeleteResourceType(int resourceTypeId);

        Task<PermissionResourceResponse> AddPermissionResource(PermissionResourceRequest permissionResource);
        Task<PermissionResourceResponse> UpdatePermissionResource(PermissionResourceRequest permissionResource);
        Task<int> DeletePermissionResource(int permissionResourceId);
        Task<int> SetDefaultUserPermission(int roleId);
        Task<List<UserRoleResponse>> GetUserRoles();
        Task<List<UserPermissionResponse>> GetPermissionByRoleName(string roleName);
    }
}
