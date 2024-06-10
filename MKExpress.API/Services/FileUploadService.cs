using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MKExpress.API.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IShipmentImageRepository _shipmentImageRepository;

        public FileUploadService(IConfiguration configuration,IMapper mapper, IShipmentImageRepository shipmentImageRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _shipmentImageRepository=shipmentImageRepository;
        }

        private static string GetFileName(IFormFile file)
        {
            return $"{GetTimestamp(DateTime.Now)}{Path.GetExtension(file.FileName)}";
        }
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public async Task FileCleanup(Guid id, ModuleNameEnum moduleName = ModuleNameEnum.Shipment, int threshhold = 10, string remark = "")
        {
            //var ImageStore = await _fileStorageRepository.GetByModuleIds(new List<Guid>() { id }, moduleName);

            //var deleteResult = 0;
            //if (remark != "" && ImageStore.Where(x => x.Remark == remark).Count() > 0)
            //{
            //    deleteResult = await _fileStorageRepository.Delete(ImageStore.First().Id);
            //    return;
            //}
            //if (ImageStore == null || ImageStore.Count < 1 || ImageStore.Count <= threshhold)
            //    return;

            //deleteResult = await _fileStorageRepository.Delete(ImageStore.First().Id);
            //if (deleteResult > 0)
            //{
            //    var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImageStore[0].FilePath);
            //    if (File.Exists(fullFilePath))
            //    {
            //        File.Delete(fullFilePath);
            //    }

            //    var fullthumbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImageStore[0].ThumbPath);
            //    if (fullthumbFilePath != null && File.Exists(fullthumbFilePath))
            //    {
            //        File.Delete(fullthumbFilePath);
            //    }
            //}
        }

        private static System.Drawing.Image ResizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        private static void CreateThumbnail(string basePath, string fileName)
        {
            var filePath = Path.Combine(basePath, fileName);
            System.Drawing.Image img = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                img = System.Drawing.Image.FromStream(fs);
            }
            catch (Exception ec)
            {
                throw ec;
            }
            finally
            {
                fs.Close();
            }
            Bitmap bitmap = new Bitmap(img);
            System.Drawing.Image resizedImage = ResizeImage(bitmap, new System.Drawing.Size(100, 100));
            resizedImage.Save(Path.Combine(basePath, "thumb_" + fileName));
        }

        public async Task<List<ShipmentImageResponse>> UploadPhoto(List<FileUploadRequest> requests)
        {
            if (requests.Count == 0)
            { 
               throw new NotFoundException(StaticValues.ErrorType_ImageNotSelected, StaticValues.Message_ImageNotSelected);
            }

            List<ShipmentImage> images = new();
            foreach (FileUploadRequest request in requests)
            {
                var newFileName = $"{request.TrackingId??request.ShipmentId}-{GetFileName(request.File)}";
                string? ImagePath = _configuration.GetSection("ImagePath").Value;
                var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImagePath);
                if (!Directory.Exists(basePath))
                    Directory.CreateDirectory(basePath);

                var filePath = Path.Combine(basePath, newFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyTo(stream);
                }

                await FileCleanup(request.TrackingId ?? request.ShipmentId, request.ModuleName, 20);

                if (request.CreateThumbnail && request.File.ContentType.StartsWith("image/"))
                {
                    CreateThumbnail(basePath, newFileName);
                }

                var absoluteFilePath = Path.Combine(ImagePath, newFileName);
                string thumbPath = absoluteFilePath.Substring(0, absoluteFilePath.LastIndexOf("\\") + 1);
                thumbPath += string.Concat("thumb_", absoluteFilePath.AsSpan(absoluteFilePath.LastIndexOf("\\") + 1));
                images.Add(new ShipmentImage()
                {
                    Url= absoluteFilePath,
                    FileType=request.FileType,
                    ModuleName=request.ModuleName.ToString(),
                    ThumbnailUrl=thumbPath,
                    Remark=request.Remark??string.Empty,
                    SequenceNo=request.SequenceNo,
                    ShipmentId=request.ShipmentId,
                    TrackingId=request.TrackingId                    
                });
            }
            
           return _mapper.Map<List<ShipmentImageResponse>>(await _shipmentImageRepository.AddImages(images));
        }

        public async Task<bool> DeleteFile(Guid id)
        {
            //var res=await _imageStoreRepository.DeleteFile(id);
            //if (res == null) return false;

            //var filePath= Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", res.FilePath);
            //var thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", res.ThumbPath);
            //if (File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}
            //if (File.Exists(thumbPath))
            //{
            //    File.Delete(thumbPath);
            //}
            return true;

        }
    }
}