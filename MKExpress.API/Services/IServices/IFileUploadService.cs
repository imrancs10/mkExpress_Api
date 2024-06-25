using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IFileUploadService
    {
        Task<List<ShipmentImageResponse>> UploadPhoto(List<FileUploadRequest> fileUploadRequest);
        Task<string> UploadUserProfileImage(IFormFile file);
        Task<bool> DeleteFile(Guid id);
        bool DeleteFile(string fileName);
    }
}