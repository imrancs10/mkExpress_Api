using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;

namespace MKExpress.API.Services
{
    public interface IFileUploadService
    {
        Task<List<ShipmentImageResponse>> UploadPhoto(List<FileUploadRequest> fileUploadRequest);
        Task<bool> DeleteFile(Guid id);
    }
}