using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core
{
    public static partial class DataExporter
    {


        private static void ExportToExcel(DataExporterItem Script, SqlDataReader DataReader)
        {
            using (SpreadsheetDocument ExcelDocument = SpreadsheetDocument.Create(_ExportPath, SpreadsheetDocumentType.Workbook))
            {
                // Create the workbook part
                WorkbookPart ExcelDocumentPart = ExcelDocument.AddWorkbookPart();
                ExcelDocumentPart.Workbook = new Workbook();

                // Create the worksheet part
                WorksheetPart ExcelWorksheetPart = ExcelDocumentPart.AddNewPart<WorksheetPart>();
                ExcelWorksheetPart.Worksheet = new Worksheet(new SheetData());

                // Create the sheets
                Sheets ExcelSheets = ExcelDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

                // Create a new sheet and associate it with the worksheet part
                Sheet ExcelSheet = new Sheet()
                {
                    Id = ExcelDocument.WorkbookPart.GetIdOfPart(ExcelWorksheetPart),
                    SheetId = 1,
                    Name = Script.Name
                };

                ExcelSheets.Append(ExcelSheet);


                // Get the sheet data
                SheetData sheetData = ExcelWorksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Add headers to the first row
                Row headerRow = new Row();
                int columnIndex = 1;
                for (int i = 0; i < DataReader.FieldCount; i++)
                {
                    DocumentFormat.OpenXml.Spreadsheet.Cell cell = CreateTextCell(GetColumnName(columnIndex), DataReader.GetName(i));
                    headerRow.AppendChild(cell);
                    columnIndex++;
                }
                sheetData.AppendChild(headerRow);

                // Add data rows
                while (DataReader.Read())
                {
                    Row dataRow = new Row();
                    columnIndex = 1;
                    for (int i = 0; i < DataReader.FieldCount; i++)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = CreateTextCell(GetColumnName(columnIndex), DataReader.GetValue(i).ToString());
                        dataRow.AppendChild(cell);
                        columnIndex++;
                    }
                    sheetData.AppendChild(dataRow);
                }


                ExcelDocument.Save();
            }
        }

        private static DocumentFormat.OpenXml.Spreadsheet.Cell CreateTextCell(string columnName, string cellValue)
        {
            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                DataType = CellValues.String,
                CellReference = columnName,
                CellValue = new CellValue(cellValue)
            };
            return cell;
        }

        private static string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }


    }
}
