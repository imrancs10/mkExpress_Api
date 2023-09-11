using MKExpress.API.Constants;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IFileStorageRepository _fileStorageRepository;
        public FileStorageService(IFileStorageRepository fileStorageRepository)
        {
            _fileStorageRepository = fileStorageRepository;
        }
        public async Task<FileStorage> Add(FileStorage fileStorage)
        {
            return await _fileStorageRepository.Add(fileStorage);
        }

        public async Task<int> Delete(int storageId)
        {
            return await _fileStorageRepository.Delete(storageId);
        }

        public async Task<FileStorage> Get(int storageId)
        {
            return await _fileStorageRepository.Get(storageId);
        }

        public async Task<List<FileStorage>> GetByModuleIds(List<int> moduleIds, ModuleNameEnum moduleName)
        {
            return await _fileStorageRepository.GetByModuleIds(moduleIds, moduleName);
        }

        public async Task<List<FileStorage>> GetByModuleName(string moduleName)
        {
            ModuleNameEnum modName = Enum.Parse<ModuleNameEnum>(moduleName, true);
            if (modName == null)
            {
                throw new BusinessRuleViolationException(StaticValues.InvalidModuleNameError, StaticValues.InvalidModuleNameMessage);
            }
            return await _fileStorageRepository.GetByModuleName(modName);
        }

        public async Task<FileStorage> Update(FileStorage fileStorage)
        {
            return await _fileStorageRepository.Update(fileStorage);
        }
    }
}
