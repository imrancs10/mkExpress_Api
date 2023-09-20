using System.Data;

namespace MKExpress.API.Utility
{
    public interface IExcelReader
    {
        DataTable ReadExcelasDataTable(string excelFilePath);
    }
}
