using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.Web.API.Dto.Request.MasterAccess;
using MKExpress.Web.API.Dto.Response.MasterAccess;
using MKExpress.Web.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{

    [ApiController]
    public class MasterAccessController : ControllerBase
    {
        private readonly IMasterAccessService _masterAccessService;
        public MasterAccessController(IMasterAccessService masterAccessService)
        {
            _masterAccessService = masterAccessService;
        }

        [ProducesResponseType(typeof(MasterAccessResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.MasterAccessPath)]
        public async Task<MasterAccessResponse> Add([FromBody] MasterAccessRequest request)
        {
            return await _masterAccessService.Add(request);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterAccessResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterAccessPath)]
        public async Task<PagingResponse<MasterAccessResponse>> GetAll([FromQuery] PagingRequest request)
        {
            return await _masterAccessService.GetAll(request);
        }

        [ProducesResponseType(typeof(MasterAccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterAccessPath)]
        public async Task<MasterAccessResponse> Update([FromBody] MasterAccessRequest request)
        {
            return await _masterAccessService.Update(request);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.MasterAccessDeletePath)]
        public async Task<int> Delete([FromRoute] int id)
        {
            return await _masterAccessService.Delete(id);
        } 
        
        [ProducesResponseType(typeof(MasterAccessResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterAccessGetByIdPath)]
        public async Task<MasterAccessResponse> Get([FromRoute] int id)
        {
            return await _masterAccessService.Get(id);
        }

        [ProducesResponseType(typeof(PagingResponse<MasterAccessResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterAccessSearchPath)]
        public async Task<PagingResponse<MasterAccessResponse>> Search([FromQuery] SearchPagingRequest pagingRequest)
        {
            return await _masterAccessService.Search(pagingRequest);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterAccessCheckUserNamePath)]
        public async Task<bool> IsUsernameExist([FromQuery] string userName)
        {
            return await _masterAccessService.IsUsernameExist(userName);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterAccessChanePasswordPath)]
        public async Task<bool> ChangePassword([FromBody] MasterAccessChangePasswordRequest request)
        {
            return await _masterAccessService.ChangePassword(request);
        }

        [ProducesResponseType(typeof(List<MenuResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.MasterAccessGetMenusPath)]
        public async Task<List<MenuResponse>> GetMenus()
        {
            return await _masterAccessService.GetMenus();
        }

        [ProducesResponseType(typeof(List<MasterAccessResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.MasterAccessLoginPath)]
        public async Task<MasterAccessResponse> MasterAccessLogin([FromBody] MasterAccessLoginRequest accessLoginRequest)
        {
            return await _masterAccessService.MasterAccessLogin(accessLoginRequest);
        }
    }
}
