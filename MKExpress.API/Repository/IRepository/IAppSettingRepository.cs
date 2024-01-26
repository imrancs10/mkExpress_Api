using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;

namespace MKExpress.API.Repository.IRepository
{
    public interface IAppSettingRepository:ICrudRepository<AppSetting>
    {
        Task<T> GetAppSettingValueByKey<T>(string code);
    }
}
