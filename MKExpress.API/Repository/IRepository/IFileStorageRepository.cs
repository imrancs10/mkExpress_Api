using MKExpress.API.Enums;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repository.IRepository
{
    public interface IFileStorageRepository
    {
        Task<List<ImageStore>> Add(List<ImageStore> ImageStores);
        Task<ImageStore> Update(ImageStore ImageStore);
        Task<int> Delete(Guid storageId);
        Task<ImageStore> Get(Guid storageId);
        Task<List<ImageStore>> GetByModuleIds(List<Guid> moduleIds, ModuleNameEnum moduleName);
        Task<List<ImageStore>> GetByModuleName(ModuleNameEnum moduleName);
    }
}
