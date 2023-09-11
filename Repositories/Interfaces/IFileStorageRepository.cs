using MKExpress.API.Enums;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Repositories.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<FileStorage> Add(FileStorage fileStorage);
        Task<FileStorage> Update(FileStorage fileStorage);
        Task<int> Delete(int storageId);
        Task<FileStorage> Get(int storageId);
        Task<List<FileStorage>> GetByModuleIds(List<int> moduleIds, ModuleNameEnum moduleName);
        Task<List<FileStorage>> GetByModuleName(ModuleNameEnum moduleName);
    }
}
