using MKExpress.API.Services.Interfaces;
using MKExpress.Web.API.Dto.Request.MasterAccess;
using MKExpress.Web.API.Dto.Response.MasterAccess;
using MKExpress.Web.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Services.Interfaces
{
    public interface IMasterAccessService:ICrudService<MasterAccessRequest,MasterAccessResponse>
    {
        Task<bool> IsUsernameExist(string username);
        Task<bool> ChangePassword(MasterAccessChangePasswordRequest masterAccessChangePasswordRequest);
        Task<List<MenuResponse>> GetMenus(); 
        Task<MasterAccessResponse> MasterAccessLogin(MasterAccessLoginRequest accessLoginRequest);
    }
}
