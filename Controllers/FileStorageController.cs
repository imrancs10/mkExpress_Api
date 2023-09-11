using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Dto.Response;
using MKExpress.API.Enums;
using MKExpress.API.Models;
using MKExpress.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MKExpress.Web.API.Controllers
{
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileUploadService _fileUploadService;
        public FileStorageController(IFileStorageService fileStorageService, IFileUploadService fileUploadService)
        {
            _fileStorageService = fileStorageService;
            _fileUploadService = fileUploadService;
        }

        [ProducesResponseType(typeof(DesignSampleResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.FileStoragePath)]
        public async Task<FileStorage> AddFile([FromForm] FileStorage fileStorage)
        {
            return await _fileStorageService.Add(fileStorage);
        }

        [ProducesResponseType(typeof(DesignSampleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPost(StaticValues.FileStoragePath)]
        public async Task<FileStorage> UpdateFile([FromForm] FileStorage fileStorage)
        {
            return await _fileStorageService.Update(fileStorage);
        }

        [ProducesResponseType(typeof(FileStorage), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpPut(StaticValues.FileUploadPath)]
        public async Task<FileStorage> UploadFile([FromForm] FileUploadRequest fileUploadRequest)
        {
            return await _fileUploadService.UploadPhoto(fileUploadRequest);
        }

        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpDelete(StaticValues.FileStorageDeletePath)]
        public async Task<int> DeleteFile([FromRoute(Name = "id")] int fileId)
        {
            return await _fileStorageService.Delete(fileId);
        }

        [ProducesResponseType(typeof(FileStorage), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FileStorage), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.FileStorageByIdPath)]
        public async Task<FileStorage> GetFile([FromRoute(Name = "id")] int fileId)
        {
            return await _fileStorageService.Get(fileId);
        }

        [ProducesResponseType(typeof(List<FileStorage>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<FileStorage>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.FileStorageByModuleIdsPath)]
        public async Task<List<FileStorage>> GetFileByModuleIds([FromQuery] List<int> moduleIds, [FromRoute] ModuleNameEnum moduleName)
        {
            return await _fileStorageService.GetByModuleIds(moduleIds, moduleName);
        }

        [ProducesResponseType(typeof(List<FileStorage>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<FileStorage>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status503ServiceUnavailable)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet(StaticValues.FileStorageByModuleNamePath)]
        public async Task<List<FileStorage>> GetFileByModuleName([FromRoute] string moduleName)
        {
            return await _fileStorageService.GetByModuleName(moduleName);
        }
    }
}
