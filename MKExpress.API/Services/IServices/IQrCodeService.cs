namespace MKExpress.API.Services.IServices
{
    public interface IQrCodeService
    {
        void GenerateQrCode(string qrCodeText, string fileName);
        Task<bool> GenerateTemplesQrCode();
    }
}
