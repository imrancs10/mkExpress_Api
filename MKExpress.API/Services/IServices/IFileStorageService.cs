﻿using MKExpress.API.DTO.Response.Image;
using MKExpress.API.Enums;
using MKExpress.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.API.Services.IServices
{
    public interface IFileStorageService
    {
        Task<List<ImageStoreResponse>> Add(List<ImageStore> imageStore);
        Task<ImageStoreResponse> Update(ImageStore imageStore);
        Task<int> Delete(Guid storageId);
        Task<ImageStoreResponse> Get(Guid storageId);
        Task<List<ImageStoreResponse>> GetByModuleIds(List<Guid> moduleIds, ModuleNameEnum moduleName);
        Task<List<ImageStoreResponse>> GetByModuleName(string moduleName);
        Task<List<ImageStoreResponse>> GetImageStore(ModuleNameEnum moduleName, Guid moduleId, string imageType = "image");
        Task<List<ImageStoreResponse>> GetImageStore(ModuleNameEnum moduleName, List<Guid> moduleIds, string imageType = "image");
        Task<List<ImageStoreResponse>> GetImageStore(ModuleNameEnum moduleName, Guid moduleId, int sequenceNo, string imageType = "image");
        Task<List<ImageStoreWithNameResponse>> GetImageStore(ModuleNameEnum? moduleName, int pageNo, int pageSize, bool allImage = false, string imageType = "image");
    }
}
