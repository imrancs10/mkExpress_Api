using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Text.RegularExpressions;

namespace TaxSolutions.Reclaim.ExcelReader
{
    public class ExcelReader : IExcelReader
    {
        private static readonly List<char> Letters = new() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', ' ' };
        private char[] _removeCharFromColName;
        public char[] RemoveCharFromColName { get => _removeCharFromColName; set => _removeCharFromColName = value; }

        public DataTable ReadExcelAsDataTable(string excelFilePath, string tabName, int skipRows = 0)
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
                    if (rowId < skipRows)
                    {
                        rowId++;
                        continue;
                    }

                    if (skipRows == rowId)
                    {
                        foreach (Cell cell in cells.Cast<Cell>())
                        {
                            var colName = GetValueAsString(cell, sst);
                            foreach (char c in _removeCharFromColName)
                            {
                                if (colName.Contains(c))
                                {
                                    colName = colName.Replace(c, ' ').Trim();
                                }
                            }
                            if (colName.ToLower().Contains("date"))
                            {
                                dt.Columns.Add(new DataColumn(colName, typeof(DateTime)));
                            }
                            else
                            {
                                dt.Columns.Add(new DataColumn(colName, typeof(string)));
                            }
                        }
                    }
                    else
                    {
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
                    }

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