using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;
        public UserPermissionController(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        [ProducesResponseType(typeof(PermissionResourceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.UserPermissionResourcePath)]
        public async Task<PermissionResourceResponse> AddPermissionResource([FromBody] PermissionResourceRequest permissionResourceRequest)
        {
            return await _userPermissionService.AddPermissionResource(permissionResourceRequest);
        }

        [ProducesResponseType(typeof(ResourceTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.UserPermissionResourceTypePath)]
        public async Task<ResourceTypeResponse> AddResourceType([FromBody] ResourceTypeRequest resourceTypeRequest)
        {
            return await _userPermissionService.AddResourceType(resourceTypeRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.UserPermissionResourcePath)]
        public async Task<int> DeletePermissionResource([FromQuery] int resourceId)
        {
            return await _userPermissionService.DeletePermissionResource(resourceId);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.UserPermissionResourceTypePath)]
        public async Task<int> DeleteResourceType([FromQuery] int resourceTypeId)
        {
            return await _userPermissionService.DeleteResourceType(resourceTypeId);
        }

        [ProducesResponseType(typeof(List<PermissionResourceResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.UserPermissionResourcePath)]
        public async Task<List<PermissionResourceResponse>> GetPermissionResources()
        {
            return await _userPermissionService.GetPermissionResources();
        }

        [ProducesResponseType(typeof(List<ResourceTypeResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.UserPermissionResourceTypePath)]
        public async Task<List<ResourceTypeResponse>> GetResourceTypes()
        {
            return await _userPermissionService.GetResourceTypes();
        }

        [ProducesResponseType(typeof(List<UserPermissionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.UserPermissionSearchPath)]
        public async Task<List<UserPermissionResponse>> SearchUserPermission([FromQuery] string searchTerm, [FromQuery] int roleId)
        {
            return await _userPermissionService.Search(searchTerm, roleId);
        }


        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.UserPermissionSetDefaultPath)]
        public async Task<int> SetDefaultUserPermission([FromQuery] int roleId)
        {
            return await _userPermissionService.SetDefaultUserPermission(roleId);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.UserPermissionPath)]
        public async Task<int> UpdateUserPermission([FromBody] List<UserPermissionRequest> userPermissionRequests)
        {
            return await _userPermissionService.Update(userPermissionRequests);
        }

        [ProducesResponseType(typeof(List<UserPermissionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.UserPermissionPath)]
        public async Task<List<UserPermissionResponse>> GetUserPermission([FromQuery] int roleId)
        {
            return await _userPermissionService.GetAll(roleId);
        }

        [ProducesResponseType(typeof(List<UserPermissionResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.UserPermissionByRoleNamePath)]
        public async Task<List<UserPermissionResponse>> GetUserPermissionByRoleName([FromRoute] string roleName)
        {
            return await _userPermissionService.GetPermissionByRoleName(roleName);
        }

        [ProducesResponseType(typeof(List<UserRoleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.UserPermissionGetRolesPath)]
        public async Task<List<UserRoleResponse>> GetUserRoles()
        {
            return await _userPermissionService.GetUserRoles();
        }

        [ProducesResponseType(typeof(PermissionResourceResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.UserPermissionResourcePath)]
        public async Task<PermissionResourceResponse> UpdatePermissionResource([FromBody] PermissionResourceRequest permissionResourceRequest)
        {
            return await _userPermissionService.UpdatePermissionResource(permissionResourceRequest);
        }


        [ProducesResponseType(typeof(ResourceTypeResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.UserPermissionResourceTypePath)]
        public async Task<ResourceTypeResponse> UpdateResourceType([FromBody] ResourceTypeRequest resourceTypeRequest)
        {
            return await _userPermissionService.UpdateResourceType(resourceTypeRequest);
        }
    }
}
