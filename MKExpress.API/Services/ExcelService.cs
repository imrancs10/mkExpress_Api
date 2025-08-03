using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MKExpress.API.Services.IServices;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MKExpress.API.Services
{
    public class ExcelService : IExcelService
    {
        private static readonly List<char> Letters = new() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };
        private char[] _removeCharFromColName;
        public char[] RemoveCharFromColName { get => _removeCharFromColName; set => _removeCharFromColName = value; }
        private SpreadsheetDocument _workBook;
        private string _fileName;

        public byte[] DownloadExcel(DataTable dataTable, string filePath, string fileName, string tabName)
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
                    sheetId = sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId?.Value ?? 0).Max() + 1;
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
            return default;// _fileName;
        }

        public DataTable ReadExcelAsDataTable(string excelFilePath,string tabName)
        {
            SpreadsheetDocument? _document = null;
            try
            {
                _document = SpreadsheetDocument.Open(excelFilePath, false);

                if (string.IsNullOrEmpty(excelFilePath))
                    throw new ArgumentNullException(nameof(excelFilePath));

                if (string.IsNullOrEmpty(tabName))
                    throw new ArgumentNullException(nameof(tabName));

                DataTable dt = new();
                _document = SpreadsheetDocument.Open(excelFilePath, false);


                WorkbookPart? bkPart = _document.WorkbookPart;
                Workbook? workbook = bkPart?.Workbook;
                Sheet? sheet = workbook?.Descendants<Sheet>().FirstOrDefault(sht => sht?.Name?.Value?.ToLower() == tabName.ToLower()) ?? throw new Exception($"Excel file doesn't contain {tabName} tab.");
                WorksheetPart? wsPart = (WorksheetPart)bkPart.GetPartById(sheet?.Id?.Value);
                SheetData? sheetData = wsPart?.Worksheet?.Elements<SheetData>()?.FirstOrDefault();


                SharedStringTablePart? sstp = _document?.WorkbookPart?.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
                SharedStringTable? sst = sstp?.SharedStringTable ?? new();

                IEnumerable<Row>? rows = sheetData?.Elements<Row>();

                int rowId = 0;

                if (rows == null)
                    return dt;

                foreach (Row row in rows)
                {
                    OpenXmlElementList cells = row.ChildElements;
                   
                    DataRow dr = dt.NewRow();

                    int cellId = 0;
                    foreach (Cell cell in cells.Cast<Cell>())
                        {
                            if (cell == null) continue;

                            int cellColumnIndex = 0;
                            if (cell?.CellReference != null)
                            {
                                cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(cell?.CellReference?.Value ?? string.Empty));
                            }

                            if (cellId < cellColumnIndex)
                            {
                                do
                                {
                                    dr[cellId] = string.Empty;
                                    cellId++;
                                }
                                while (cellId < cellColumnIndex);
                                var cellValue = GetValueAsString(cell, sst);
                                if (cellValue.Trim() == "Total" || cellValue.Trim() == "Count") break;
                                if (dt.Columns[cellId].DataType == typeof(DateTime))
                                {
                                    if (double.TryParse(cellValue, out double dateTimeInt))
                                        dr[cellId] = DateTime.FromOADate(dateTimeInt);
                                }
                                else
                                    dr[cellId] = cellValue;
                            }
                            else
                            {
                                var cellValue = GetValueAsString(cell, sst);
                                if (cellValue.Trim() == "Total" || cellValue.Trim() == "Count") break;
                                if (dt.Columns[cellId].DataType == typeof(DateTime))
                                {
                                    if (double.TryParse(cellValue, out double dateTimeInt))
                                        dr[cellId] = DateTime.FromOADate(dateTimeInt);
                                }
                                else
                                    dr[cellId] = cellValue;
                            }

                            cellId++;
                        }
                    if (!dr.IsEmpty())
                       dt.Rows.Add(dr);

                    rowId++;
                }
                _document?.Dispose();
                return dt;
            }
            catch (Exception ex)
            {
                _document?.Dispose();
                throw;
            }
        }
        private static string GetValueAsString(Cell cell, SharedStringTable sst)
        {
            if (cell == null) return string.Empty;

            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                if (int.TryParse(cell?.CellValue?.Text, out int ind))
                {
                    return sst.ChildElements[ind].InnerText;
                }
                else
                {
                    return cell?.InnerText ?? string.Empty;
                }
            }
            else
            {
                return cell?.CellValue?.Text ?? string.Empty;
            }
        }

        private static string GetColumnName(string cellReference)
        {
            Regex regex = new("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        private static int? GetColumnIndexFromName(string columnName)
        {
            int? columnIndex = null;

            string[] colLetters = Regex.Split(columnName, "([A-Z]+)");
            colLetters = colLetters.Where(s => !string.IsNullOrEmpty(s)).ToArray();

            if (colLetters.Length <= 2)
            {
                int index = 0;
                foreach (string col in colLetters)
                {
                    List<char> col1 = colLetters.ElementAt(index).ToCharArray().ToList();
                    int? indexValue = Letters.IndexOf(col1.ElementAt(index));

                    if (indexValue != -1)
                    {
                        // The first letter of a two digit column needs some extra calculations
                        if (index == 0 && colLetters.Length == 2)
                        {
                            columnIndex = columnIndex == null ? (indexValue + 1) * 26 : columnIndex + ((indexValue + 1) * 26);
                        }
                        else
                        {
                            columnIndex = columnIndex == null ? indexValue : columnIndex + indexValue;
                        }
                    }

                    index++;
                }
            }

            return columnIndex;
        }
        
    }
    public static class DataTableExtenstion
    {
        public static bool IsEmpty(this DataRow row)
        {
            return row == null || row.ItemArray.All(i => i.IsNullEquivalent());
        }

        public static bool IsNullEquivalent(this object value)
        {
            return value == null
                   || value is DBNull
                   || string.IsNullOrWhiteSpace(value.ToString());
        }
    }
}
