
using IronXL;
using System.Data;

namespace MKExpress.API.Utility
{
    public class ExcelReader : IExcelReader
    {
        public DataTable ReadExcelasDataTable(string excelFilePath)
        {
            try
            {
                WorkBook workBook = WorkBook.Load(excelFilePath);

                // Select default sheet
                WorkSheet workSheet = workBook.DefaultWorkSheet;

                // Convert the worksheet to DataTable
                return workSheet.ToDataTable(true);
            }
            catch
            {
                throw;
            }
        }
    }
}
