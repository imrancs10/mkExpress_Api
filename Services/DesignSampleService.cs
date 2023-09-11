using AutoMapper;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Enums;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class DesignSampleService : IDesignSampleService
    {
        private readonly IDesignSampleRepository _designSampleRepository;
        private readonly IMapper _mapper;
        private readonly IFileUploadService _fileUploadService;
        private readonly IFileStorageRepository _fileStorageRepository;
        public DesignSampleService(IDesignSampleRepository designSampleRepository, IMapper mapper, IFileUploadService fileUploadService, IFileStorageRepository fileStorageRepository)
        {
            _designSampleRepository = designSampleRepository;
            _mapper = mapper;
            _fileUploadService = fileUploadService;
            _fileStorageRepository = fileStorageRepository;
        }

        public async Task<DesignSampleResponse> Add(DesignSampleRequest request)
        {
            var designSample = _mapper.Map<DesignSample>(request);
            var result = _mapper.Map<DesignSampleResponse>(await _designSampleRepository.Add(designSample));
            return await UploadFile(request, result);
        }

        public async Task<int> AddQuantity(int quantity, int designSampleId)
        {
            return await _designSampleRepository.AddQuantity(quantity, designSampleId);
        }

        public async Task<int> AddQuantity(Dictionary<int, int> sampleQuantity)
        {
            return await _designSampleRepository.AddQuantity(sampleQuantity);
        }

        public async Task<int> Delete(int designSampleId)
        {
            return await _designSampleRepository.Delete(designSampleId);
        }

        public async Task<DesignSampleResponse> Get(int designSampleId)
        {
            var result = _mapper.Map<DesignSampleResponse>(await _designSampleRepository.Get(designSampleId));
            var filePath = await _fileStorageRepository.GetByModuleIds(new List<int>() { result.Id }, ModuleNameEnum.DesignSample);
            result.PicturePath = filePath?.FirstOrDefault()?.FilePath ?? string.Empty;
            result.ThumbPath = filePath?.FirstOrDefault()?.ThumbPath ?? string.Empty;
            return result;
        }

        public async Task<PagingResponse<DesignSampleResponse>> GetAll(PagingRequest pagingRequest)
        {
            var result = _mapper.Map<PagingResponse<DesignSampleResponse>>(await _designSampleRepository.GetAll(pagingRequest));
            var moduleIds = result.Data.Select(x => x.Id).ToList();
            var filePath = await _fileStorageRepository.GetByModuleIds(moduleIds, ModuleNameEnum.DesignSample);
            foreach (var sample in result.Data)
            {
                sample.PicturePath = filePath?.FirstOrDefault(x => x.ModuleId == sample.Id)?.FilePath ?? string.Empty;
                sample.ThumbPath = filePath?.FirstOrDefault(x => x.ModuleId == sample.Id)?.ThumbPath ?? string.Empty;
            }
            return result;
        }

        public async Task<List<DesignSampleResponse>> GetByCategory(int categotyId)
        {
            return _mapper.Map<List<DesignSampleResponse>>(await _designSampleRepository.GetByCategory(categotyId));
        }

        public async Task<PagingResponse<DesignSampleResponse>> Search(SearchPagingRequest searchPagingRequest)
        {
            var result = _mapper.Map<PagingResponse<DesignSampleResponse>>(await _designSampleRepository.Search(searchPagingRequest));
            var moduleIds = result.Data.Select(x => x.Id).ToList();
            var filePath = await _fileStorageRepository.GetByModuleIds(moduleIds, ModuleNameEnum.DesignSample);
            foreach (var sample in result.Data)
            {
                sample.PicturePath = filePath?.FirstOrDefault(x => x.ModuleId == sample.Id)?.FilePath ?? string.Empty;
                sample.ThumbPath = filePath?.FirstOrDefault(x => x.ModuleId == sample.Id)?.ThumbPath ?? string.Empty;
            }
            return result;
        }

        public async Task<DesignSampleResponse> Update(DesignSampleRequest request)
        {
            var designSample = _mapper.Map<DesignSample>(request);
            var result = _mapper.Map<DesignSampleResponse>(await _designSampleRepository.Update(designSample));
            await UploadFile(request, result);
            return result;
        }

        private async Task<DesignSampleResponse> UploadFile(DesignSampleRequest request, DesignSampleResponse result)
        {
            var filePath = await _fileUploadService.UploadDesignSamplePhoto(request.File, result.Id);
            if (filePath == null)
                return result;
            string thumbPath = filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
            thumbPath += "thumb_" + filePath.Substring(filePath.LastIndexOf("\\") + 1);

            FileStorage fileStorage = new FileStorage()
            {
                FilePath = filePath,
                ModuleId = result.Id,
                ModuleName = ModuleNameEnum.DesignSample.ToString(),
                ThumbPath = thumbPath
            };
            await _fileStorageRepository.Add(fileStorage);
            result.PicturePath = filePath;
            return result;

        }

    }
}
