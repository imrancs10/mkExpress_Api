using MKExpress.API.Models;

namespace MKExpress.API.Repository
{
    public interface IAppSettingRepository:ICrudRepository<AppSetting>
    {
        Task<T> GetAppSettingValueByKey<T>(string code);
        Task<List<AppSettingGroup>> GetAllAppSettingGroup();
    }
}
