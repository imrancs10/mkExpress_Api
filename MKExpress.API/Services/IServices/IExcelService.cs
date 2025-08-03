using System.Data;

namespace MKExpress.API.Services.IServices
{
    public interface IExcelService
    {
        DataTable ReadExcelAsDataTable(string fileName, string tabName);
        byte[] DownloadExcel(DataTable dataTable, string filePath, string fileName, string tabName);
    }
}
