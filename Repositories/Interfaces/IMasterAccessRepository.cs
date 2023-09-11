using MKExpress.API.Repositories.Interfaces;
using MKExpress.Web.API.Dto.Request.MasterAccess;
using MKExpress.Web.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Repositories.Interfaces
{
    public interface IMasterAccessRepository:ICrudRepository<MasterAccess>
    {
        Task<bool> IsUsernameExist(string username);
        Task<bool> ChangePassword(MasterAccessChangePasswordRequest masterAccessChangePasswordRequest);
        Task<List<MasterMenu>> GetMenus();
        Task<MasterAccess> MasterAccessLogin(MasterAccessLoginRequest accessLoginRequest);
    }
}
