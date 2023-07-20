using Microsoft.Data.SqlClient;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Modular.Core.Databases;
using Modular.Core.Configuration;
using Modular.Core.Utility;

namespace Modular.Core.Exporter
{

    public static class DataExporter
    {

        #region "  Variables  "

        private readonly static string _ScriptPath = AppConfig.GetValue("FolderPath:DataExporterScript");

        private readonly static string _ExportPath = AppConfig.GetValue("FolderPath:DataExporterExport");

        #endregion

        #region "  Properties  "

        public static DirectoryInfo ScriptFolder
        {
            get
            {
                return new DirectoryInfo(_ScriptPath);
            }
        }

        public static DirectoryInfo ExportFolder
        {
            get
            {
                if (!string.IsNullOrEmpty(_ExportPath))
                {
                    return new DirectoryInfo(_ExportPath);
                }
                else
                {
                    return new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads");
                }
            }
        }

        #endregion

        #region "  Public Methods  "

        public static void ExcecuteScript(DataExporterItem Item)
        {
            if (Database.CheckDatabaseConnection())
            {
                switch (Database.ConnectionMode)
                {
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection())
                        {
                            Connection.Open();

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = File.ReadAllText(Item.Name);

                                SqlDataReader DataReader = Command.ExecuteReader();
                                ExportToFile(Item, DataReader);
                            }

                            Connection.Close();
                        }

                        break;

                    case Database.DatabaseConnectivityMode.Local:
                        break;
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "Database connection is not available.");
            }
        }

        public static List<DataExporterItem> GetAllScripts()
        {
           if (ScriptFolder.Exists)
            {
                List<DataExporterItem> AllDataExportScripts = new List<DataExporterItem>();

                foreach (FileInfo ScriptFile in ScriptFolder.GetFiles("*.sql"))
                {
                    AllDataExportScripts.Add(new DataExporterItem(ScriptFile));
                }
                return AllDataExportScripts;
            }
           else
            {
                ScriptFolder.Create();
                return new List<DataExporterItem>();
            }
        }

        #endregion

        #region "  Private Methods  "

        private static void ExportToFile(DataExporterItem Script, SqlDataReader DataReader)
        {
            using (ExcelEngine ExcelEngine = new ExcelEngine())
            {
                Syncfusion.XlsIO.IApplication ExcelApplication = ExcelEngine.Excel;
                ExcelApplication.DefaultVersion = ExcelVersion.Xlsx;


                // Create a new workbook
                IWorkbook ExcelWorkbook = ExcelApplication.Workbooks.Create();

                // Access first worksheet from the workbook instance.
                IWorksheet ExcelWorksheet = ExcelWorkbook.Worksheets[0];

                int RowIndex = 1;

                // Set the column headers
                for (int ColumnIndex = 0; ColumnIndex < DataReader.FieldCount; ColumnIndex++)
                {
                    ExcelWorksheet[RowIndex, ColumnIndex + 1].Text = DataReader.GetName(ColumnIndex);
                }

                // Set the data
                while (DataReader.Read())
                {
                    RowIndex++;
                    for (int ColumnIndex = 0; ColumnIndex < DataReader.FieldCount; ColumnIndex++)
                    {
                        ExcelWorksheet[RowIndex, ColumnIndex + 1].Text = DataReader.GetValue(ColumnIndex).ToString();
                    }
                }

                // Save the workbook to disk in xlsx format
                ExcelWorkbook.SaveAs(ModularUtils.ConvertFileToStream($"{ExportFolder.FullName}\\{DateTime.Now:yyyy-MM-dd HH:mm} {Script.Name}.xlsx"));
                ExcelWorkbook.Close();
            }
        }

        #endregion
    }
}
