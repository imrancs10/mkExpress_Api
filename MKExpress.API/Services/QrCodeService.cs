using MKExpress.API.Services.IServices;
using Microsoft.Extensions.Configuration;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MKExpress.API.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly IConfiguration _configuration;
        public QrCodeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void GenerateQrCode(string qrCodeText, string fileName)
        {
            string basePath = GetImageBasePath();
            var fullFilePath = Path.Combine(basePath, fileName + ".png");

            if (File.Exists(fullFilePath))
                return;

            QRCodeGenerator QrGenerator = new();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new(QrCodeInfo);
            Bitmap QrBitmap = qrCode.GetGraphic(60);
            QrBitmap.Save(fullFilePath, ImageFormat.Png);
        }

        public async Task<bool> GenerateTemplesQrCode()
        {
            var templeIds = new List<int>();// await _templeService.GetTempleIds();
            string qrCodeUrl = _configuration.GetSection("QrCodeUrlBasePath").Value;
            foreach (var templeId in templeIds)
            {
                var qrCodeFileName = $"TempleId-{templeId}";
                GenerateQrCode(qrCodeUrl + templeId, qrCodeFileName);
            }

            return default(bool);
        }

        private string GetImageBasePath()
        {
            string? ImagePath = _configuration.GetSection("QrCodesPath").Value;
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ImagePath);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            return basePath;
        }
    }
}
