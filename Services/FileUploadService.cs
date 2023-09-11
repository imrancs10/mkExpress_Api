using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MKExpress.API.Constants;
using MKExpress.API.Dto.Request;
using MKExpress.API.Enums;
using MKExpress.API.Exceptions;
using MKExpress.API.Models;
using MKExpress.API.Repositories.Interfaces;
using MKExpress.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MKExpress.API.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IConfiguration _configuration;
        private readonly IFileStorageRepository _fileStorageRepository;

        public FileUploadService(IConfiguration configuration, IFileStorageRepository fileStorageRepository)
        {
            _configuration = configuration;
            _fileStorageRepository = fileStorageRepository;
        }
        public async Task<string> UploadDesignSamplePhoto(IFormFile iFormFile, int sampleId)
        {
            if (iFormFile == null)
                return default;
            if (iFormFile?.Length == 0)
                throw new NotFoundException(StaticValues.PhotoNotSelectedError, StaticValues.PhotoNotSelectedMessage);

            var newFileName = $"{sampleId}-{GetFileName(iFormFile)}";
            var designSamplePhotoPath = _configuration.GetSection("DesignSamplePhotoPath").Value;
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", designSamplePhotoPath);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            var filePath = Path.Combine(basePath, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                iFormFile.CopyTo(stream);
            }

            await FileCleanup(sampleId);
            CreateThumbnail(basePath, newFileName);
            return Path.Combine(designSamplePhotoPath, newFileName);
        }

        private static string GetFileName(IFormFile file)
        {
            return $"{GetTimestamp(DateTime.Now)}{Path.GetExtension(file.FileName)}";
        }
        private static string GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public async Task FileCleanup(int id, ModuleNameEnum moduleName = ModuleNameEnum.DesignSample, int threshhold = 1, string remark = "")
        {
            var fileStorage = await _fileStorageRepository.GetByModuleIds(new List<int>() { id }, moduleName);
            var deleteResult = 0;
            if (remark != "" && fileStorage.Where(x => x.Remark == remark).Count() > 0)
            {
                deleteResult = await _fileStorageRepository.Delete(fileStorage.First().Id);
                return;
            }
            if (fileStorage == null || fileStorage.Count < 1 || fileStorage.Count <= threshhold)
                return;

            deleteResult = await _fileStorageRepository.Delete(fileStorage.First().Id);
            if (deleteResult > 0)
            {
                var fullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileStorage[0].FilePath);
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                }

                var fullthumbFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileStorage[0].ThumbPath);
                if (fullthumbFilePath != null && File.Exists(fullthumbFilePath))
                {
                    File.Delete(fullthumbFilePath);
                }
            }
        }

        private static Image ResizeImage(Image imgToResize, Size size)
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
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }

        private static void CreateThumbnail(string basePath, string fileName)
        {
            var filePath = Path.Combine(basePath, fileName);
            Image img = null;
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                img = Image.FromStream(fs);
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
            Image resizedImage = ResizeImage(bitmap, new Size(100, 100));
            resizedImage.Save(Path.Combine(basePath, "thumb_" + fileName));
        }

        public async Task<FileStorage> UploadPhoto(FileUploadRequest fileUploadRequest)
        {
            if (fileUploadRequest.File == null || fileUploadRequest.File.Length == 0)
                throw new NotFoundException(StaticValues.PhotoNotSelectedError, StaticValues.PhotoNotSelectedMessage);

            var newFileName = $"{fileUploadRequest.ModuleId}-{GetFileName(fileUploadRequest.File)}";
            var photoPath = _configuration.GetSection("PhotoPath").Value;
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoPath);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            var filePath = Path.Combine(basePath, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                fileUploadRequest.File.CopyTo(stream);
            }

            await FileCleanup(fileUploadRequest.ModuleId, fileUploadRequest.ModuleName, 2, fileUploadRequest.Remark);

            if (fileUploadRequest.CreateThumbnail)
            {
                CreateThumbnail(basePath, newFileName);
            }

            var absoluteFilePath = Path.Combine(photoPath, newFileName);
            return await UpdateFileStorage(absoluteFilePath, fileUploadRequest.ModuleId, fileUploadRequest.ModuleName, fileUploadRequest.Remark);
        }

        private async Task<FileStorage> UpdateFileStorage(string filePath, int moduleId, ModuleNameEnum moduleNameEnum, string remark = "")
        {
            string thumbPath = filePath.Substring(0, filePath.LastIndexOf("\\") + 1);
            thumbPath += "thumb_" + filePath.Substring(filePath.LastIndexOf("\\") + 1);

            FileStorage fileStorage = new FileStorage()
            {
                FilePath = filePath,
                ModuleId = moduleId,
                ModuleName = moduleNameEnum.ToString(),
                ThumbPath = thumbPath,
                Remark = remark
            };
            return await _fileStorageRepository.Add(fileStorage);
        }
    }
}