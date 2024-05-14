using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxSolutions.Reclaim.ExcelWriter
{
    public interface IExcelWriter
    {
        string ExcelFromDataTable(DataTable dataTable, string filePath, string fileName, string tabName);
    }
}
