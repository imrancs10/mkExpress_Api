using MKExpress.API.Enums;
using MKExpress.API.Models;

namespace MKExpress.API.Repository.IRepository
{
    public interface IImageStoreRepository
    {
        Task<List<ImageStore>> GetImageStore(ModuleNameEnum moduleName, int moduleId, string imageType = "image");
        Task<List<ImageStore>> GetImageStore(Dictionary<ModuleNameEnum,List<int>> moduleNameAndIds, string imageType = "image");
        Task<List<ImageStore>> GetImageStore(ModuleNameEnum? moduleName, List<int> moduleIds, string imageType = "image");
        Task<List<ImageStore>> GetImageStore(ModuleNameEnum moduleName, int moduleId,int sequenceNo, string imageType = "image");
        Task<List<ImageStore>> GetImageStore(ModuleNameEnum? moduleName, List<int> moduleIds, bool allImage);
        Task<List<ImageStoreWithName>> GetImageStore(ModuleNameEnum? moduleName,int pageNo,int pageSize, bool allImage = false, string imageType = "image");
        Task<ImageStore?> DeleteFile(int id);
    }
}
