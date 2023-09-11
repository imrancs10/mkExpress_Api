using Microsoft.AspNetCore.Http;
using MKExpress.API.Dto.Request;
using MKExpress.API.Models;
using System.Threading.Tasks;

namespace MKExpress.API.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task<string> UploadDesignSamplePhoto(IFormFile files, int sampleId);
        Task<FileStorage> UploadPhoto(FileUploadRequest fileUploadRequest);
    }
}