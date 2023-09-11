using MKExpress.API.Enums;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<FileStorage> Add(FileStorage fileStorage);
        Task<FileStorage> Update(FileStorage fileStorage);
        Task<int> Delete(int storageId);
        Task<FileStorage> Get(int storageId);
        Task<List<FileStorage>> GetByModuleIds(List<int> moduleIds, ModuleNameEnum moduleName);
        Task<List<FileStorage>> GetByModuleName(string moduleName);
    }
}
