using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IMapper _mapper;
        public UserPermissionService(IUserPermissionRepository userPermissionRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userPermissionRepository = userPermissionRepository;
        }
        public async Task<PermissionResourceResponse> AddPermissionResource(PermissionResourceRequest permissionResourceRequest)
        {
            PermissionResource permissionResource = _mapper.Map<PermissionResource>(permissionResourceRequest);
            return _mapper.Map<PermissionResourceResponse>(await _userPermissionRepository.AddPermissionResource(permissionResource));
        }

        public async Task<ResourceTypeResponse> AddResourceType(ResourceTypeRequest resourceTypeRequest)
        {
            ResourceType resourceType = _mapper.Map<ResourceType>(resourceTypeRequest);
            return _mapper.Map<ResourceTypeResponse>(await _userPermissionRepository.AddResourceType(resourceType));
        }

        public async Task<int> DeletePermissionResource(int permissionResourceId)
        {
            return await _userPermissionRepository.DeletePermissionResource(permissionResourceId);
        }

        public async Task<int> DeleteResourceType(int resourceTypeId)
        {
            return await _userPermissionRepository.DeleteResourceType(resourceTypeId);
        }

        public async Task<List<UserPermissionResponse>> GetAll(int roleId = 0)
        {
            if (!await _userPermissionRepository.HasUserPermission(roleId) && roleId > 0)
            {
                await _userPermissionRepository.SetDefaultUserPermission(roleId);
            }
            var result = await _userPermissionRepository.GetAll(roleId);
            var permissionGrp = result.GroupBy(x => x.PermissionResourceId);
            List<UserPermissionResponse> userPermissionResponses = new List<UserPermissionResponse>();
            foreach (var item in permissionGrp)
            {
                var firstItem = item.First();
                userPermissionResponses.Add(new UserPermissionResponse()
                {
                    PermissionResourceId = firstItem.PermissionResourceId,
                    Id = 0,
                    RoleId = item.Select(x => x.UserRoleId).ToList(),
                    PermissionResourceCode = firstItem.PermissionResource.Code,
                    PermissionResourceName = firstItem.PermissionResource.Name,
                    PermissionResourceType = firstItem.PermissionResource.ResourceType.Name
                });
            }
            return userPermissionResponses;
        }

        public async Task<List<UserPermissionResponse>> GetPermissionByRoleName(string roleName)
        {
            return _mapper.Map<List<UserPermissionResponse>>(await _userPermissionRepository.GetPermissionByRoleName(roleName));
        }

        public async Task<List<PermissionResourceResponse>> GetPermissionResources()
        {
            return _mapper.Map<List<PermissionResourceResponse>>(await _userPermissionRepository.GetPermissionResources());
        }

        public async Task<List<ResourceTypeResponse>> GetResourceTypes()
        {
            return _mapper.Map<List<ResourceTypeResponse>>(await _userPermissionRepository.GetResourceTypes());
        }

        public async Task<List<UserRoleResponse>> GetUserRoles()
        {
            return _mapper.Map<List<UserRoleResponse>>(await _userPermissionRepository.GetUserRoles());
        }

        public async Task<List<UserPermissionResponse>> Search(string searchTerm, int roleId)
        {
            return _mapper.Map<List<UserPermissionResponse>>(await _userPermissionRepository.Search(searchTerm, roleId));
        }

        public async Task<int> SetDefaultUserPermission(int roleId)
        {
            return await _userPermissionRepository.SetDefaultUserPermission(roleId);
        }

        public async Task<int> Update(List<UserPermissionRequest> userPermissionRequest)
        {
            var userPermissions = _mapper.Map<List<UserPermission>>(userPermissionRequest);
            return await _userPermissionRepository.Update(userPermissions);
        }

        public async Task<PermissionResourceResponse> UpdatePermissionResource(PermissionResourceRequest permissionResourceRequest)
        {
            PermissionResource permissionResource = _mapper.Map<PermissionResource>(permissionResourceRequest);
            return _mapper.Map<PermissionResourceResponse>(await _userPermissionRepository.UpdatePermissionResource(permissionResource));
        }

        public async Task<ResourceTypeResponse> UpdateResourceType(ResourceTypeRequest resourceTypeRequest)
        {
            ResourceType resourceType = _mapper.Map<ResourceType>(resourceTypeRequest);
            return _mapper.Map<ResourceTypeResponse>(await _userPermissionRepository.UpdateResourceType(resourceType));
        }
    }
}
