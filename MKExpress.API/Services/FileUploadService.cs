using AutoMapper;
using MKExpress.API.Contants;
using MKExpress.API.DTO.Request;
using MKExpress.API.DTO.Request.User;
using MKExpress.API.DTO.Response;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repository;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MKExpress.API.Services
{
    public class FileUploadService(IConfiguration configuration, IMapper mapper, IShipmentImageRepository shipmentImageRepository, IUserRepository userRepository) : IFileUploadService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IMapper _mapper = mapper;
        private readonly IShipmentImageRepository _shipmentImageRepository = shipmentImageRepository;
        private readonly IUserRepository _userRepository = userRepository;

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

        private static Bitmap ResizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size)
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
            Bitmap b = new(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return b;
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

        public async Task<string> UploadUserProfileImage(UserProfileImageUploadRequest request)
        {
            if (request==null || request.File == null || request.File.Length == 0)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidParameters, StaticValues.Error_InvalidImageUploaded);

            var imageExt = Path.GetExtension(request.File.FileName);
            if (imageExt != ".png" && imageExt != ".jpg" && imageExt != "jpeg")
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidImageExtension, StaticValues.Error_InvalidImageExtension);

            if (request.File.Length/1000>200)
                throw new BusinessRuleViolationException(StaticValues.ErrorType_InvalidImageSize, StaticValues.Error_InvalidImageSize);

            string? ImagePath = _configuration.GetSection("UserProfileImagePath").Value;
            string newFileName = GetFileName(request.File);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImagePath);
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fullFileName=Path.Combine(path, newFileName);
            using (var stream = new FileStream(fullFileName, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var relativePath = Path.Combine(ImagePath, newFileName);
            var oldProfileImagePath = await _userRepository.UpdateProfileImagePath(relativePath);
            if(oldProfileImagePath== "ImageNotSaved")
            {               
                throw new BusinessRuleViolationException(StaticValues.ErrorType_ProfileImageNotUpdated, StaticValues.Error_ProfileImageNotUpdated);
            }
            else DeleteFile(Path.GetFileName(oldProfileImagePath));
            return relativePath.Replace("\\", "/");
        }

        public bool DeleteFile(string fileName)
        {
            string? ImagePath = _configuration.GetSection("UserProfileImagePath").Value;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImagePath, fileName);
            if(File.Exists(path))
                File.Delete(path);
            return true;
        }
    }
}