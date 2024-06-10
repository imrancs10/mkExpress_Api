using MKExpress.API.Models;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Request.ImageStore;
using MKExpress.API.DTO.Response.Image;
using MKExpress.API.Enums;

namespace MKExpress.API.Services
{
    public interface IFileUploadService
    {
        Task<List<ShipmentImageResponse>> UploadPhoto(List<FileUploadRequest> fileUploadRequest);
        Task<bool> DeleteFile(Guid id);
    }
}