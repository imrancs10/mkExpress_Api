using DocumentFormat.OpenXml.Packaging;
using System.Data;

namespace TaxSolutions.Reclaim.ExcelWriter
{
    public class ExcelWriter : IExcelWriter
    {
        private SpreadsheetDocument _workBook;
        private string _fileName;

        public string ExcelFromDataTable(DataTable dataTable, string filePath, string fileName, string tabName)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("filePath is empty.");

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("fileName is empty.");

            if (string.IsNullOrEmpty(tabName))
                throw new ArgumentException("tabName is empty.");

            if (dataTable.Rows.Count == 0)
                throw new ArgumentException("DataTable is empty.");

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            _fileName = Path.Combine(filePath, fileName);
            _workBook = SpreadsheetDocument.Create(_fileName, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
            using (_workBook)
            {              
                var workbookPart = _workBook.AddWorkbookPart();

                _workBook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook
                {
                    Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets()
                };

                var sheetPart = _workBook.WorkbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = _workBook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                string relationshipId = _workBook.WorkbookPart.GetIdOfPart(sheetPart);

                uint sheetId = 1;
                if (sheets?.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                {
                    sheetId = sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId?.Value??0).Max() + 1;
                }

                DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new() { Id = relationshipId, SheetId = sheetId, Name = tabName };
                sheets?.Append(sheet);

                DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new();

                List<string> columns = new();
                foreach (DataColumn column in dataTable.Columns)
                {
                    columns.Add(column.ColumnName);

                    DocumentFormat.OpenXml.Spreadsheet.Cell cell = new()
                    {
                        DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                        CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName)
                    };
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);

                foreach (DataRow dr in dataTable.Rows)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Row newRow = new();
                    foreach (string col in columns)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new()
                        {
                            DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String,
                            CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dr[col].ToString()) //
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
            }
            return _fileName;
        }
    }
}
