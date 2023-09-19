using MKExpress.API.Models;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Request.ImageStore;
using MKExpress.API.DTO.Response.Image;
using MKExpress.API.Enums;

namespace MKExpress.API.Services.IServices
{
    public interface IFileUploadService
    {
        Task<string> UploadDesignSamplePhoto(IFormFile files, int sampleId);
        Task<List<ImageStoreResponse>> UploadPhoto(List<FileUploadRequest> fileUploadRequest);
        Task<bool> DeleteFile(int id);
    }
}