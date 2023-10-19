using System.Data;

namespace MKExpress.API.Services.IServices
{
    public interface IExcelService
    {
        DataTable ReadExcelAsDataTable(string fileName);
        byte[] DownloadExcel(DataSet dataSet);
    }
}
