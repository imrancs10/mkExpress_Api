using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxSolutions.Reclaim.ExcelReader
{
    public interface IExcelReader
    {
        DataTable ReadExcelAsDataTable(string excelFilePath, string tabName, int skipRows = 0);
        char[] RemoveCharFromColName { get; set; }
    }
}
