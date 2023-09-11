using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Modular.Core.Databases
{

    public static class DatabaseUtils
    {

        #region "  Public Methods  "

        /// <summary>
        /// Creates Database Table using Dynamic Database
        /// </summary>
        /// <param name="DatabaseTableName"></param>
        /// <param name="AllFields"></param>
        public static void CreateDatabaseTable(string DatabaseTableName, FieldInfo[] AllFields)
        {
            RunQuery(DatabaseQueryUtils.CreateNewTableQuery(DatabaseTableName, AllFields));
        }

        /// <summary>
        /// Creates Database Table using Dynamic Database
        /// </summary>
        /// <param name="DatabaseTableName"></param>
        /// <param name="AllProperties"></param>
        public static void CreateDatabaseTable(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            RunQuery(DatabaseQueryUtils.CreateNewTableQuery(DatabaseTableName, AllProperties));
        }

        public static void AlterDatabaseTable(string DatabaseTableName, PropertyInfo[] AllProperties)
        {
            RunQuery(DatabaseQueryUtils.CreateAlterTableQuery(DatabaseTableName, AllProperties));
        }


        /// <summary>
        /// Creates a Stored Procedure using Dynamic Database
        /// </summary>
        /// <param name="DatabaseQuery"></param>
        /// <param name="StoredProcedureName"></param>
        /// <returns></returns>
        public static void CreateStoredProcedure(string DatabaseQuery, string StoredProcedureName)
        {
            CreateStoredProcedure(DatabaseQuery, StoredProcedureName, new List<DatabaseParameter>());
        }

        public static void CreateStoredProcedure(string DatabaseQuery, string StoredProcedureName, DatabaseParameter Parameter)
        {
            CreateStoredProcedure(DatabaseQuery, StoredProcedureName, new List<DatabaseParameter>() { Parameter });
        }

        public static void CreateStoredProcedure(string DatabaseQuery, string StoredProcedureName, List<DatabaseParameter> Parameters)
        {
            string StoredProcedureQuery = DatabaseQueryUtils.CreateStoredProcedureQuery(DatabaseQuery, StoredProcedureName, Parameters);

            if (Database.CheckStoredProcedureExists(StoredProcedureName))
            {
                StoredProcedureQuery = StoredProcedureQuery.Replace("CREATE PROCEDURE", "ALTER PROCEDURE");
            }
            RunQuery(StoredProcedureQuery);
        }

        #endregion

        #region "  Private Methods  "

        private static void RunQuery(string DatabaseQuery)
        {
            if (Database.CheckDatabaseConnection())
            {
                switch (Database.ConnectionMode)
                {
                    // Runs the query in the remote database
                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqlCommand Command = new SqlCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQuery;

                                Command.ExecuteNonQuery();
                            }

                            Connection.Close();
                        }
                        break;

                    // Run the query in the local database
                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            using (SqliteCommand Command = new SqliteCommand())
                            {
                                Command.Connection = Connection;
                                Command.CommandType = CommandType.Text;
                                Command.CommandText = DatabaseQuery;

                                Command.ExecuteNonQuery();
                            }

                            Connection.Close();
                        }
                        break;

                    default:
                        break;
                }
            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "Database connection error. Please check your database connection settings.");
            }
        }

    #endregion

    }
}
