using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Configuration;
using System.Configuration;

namespace Modular.Core.Databases
{
    public static class Database
    {
        #region "  Enums  "

        public enum DatabaseConnectivityMode
        {
            Unknown,
            Local,
            Remote
        }

        #endregion

        #region "  Properties  "

        public static string ConnectionString
        {
            get
            {
                switch (ConnectionMode)
                {
                    case DatabaseConnectivityMode.Local:
                        return Local.LocalConnectionString;

                    case DatabaseConnectivityMode.Remote:
                        return Remote.RemoteConnectionString;

                    default:
                        return string.Empty;
                }
            }
        }

        public static DatabaseConnectivityMode ConnectionMode
        {
            get
            {
                string ConfigValue = SystemConfig.GetValue("DatabaseConnectivityMode");
                return ConfigValue.Trim().ToUpper() switch
                {
                    "LOCAL" => DatabaseConnectivityMode.Local,
                    "REMOTE" => DatabaseConnectivityMode.Remote,
                    //"LOCALREMOTE" => DatabaseConnectivityMode.LocalThenRemote,
                    //"REMOTELOCAL" => DatabaseConnectivityMode.RemoteThenLocal,
                    //"BOTH"          => DatabaseConnectivityMode.Both,
                    _ => DatabaseConnectivityMode.Unknown,
                };
            }
        }

        public static bool EnableStoredProcedures
        {
            get
            {
                string ConfigValue = AppConfig.GetValue("EnableStoredProcedures");
                return ConfigValue.Trim().ToUpper() == "TRUE" && ConnectionMode == DatabaseConnectivityMode.Remote;
            }
        }

        #endregion

        #region "  Methods  "

        /// <summary>
        /// Checks if database can be connected.
        /// </summary>
        /// <returns></returns>
        public static bool CheckDatabaseConnection()
        {
            switch (ConnectionMode)
            {
                case DatabaseConnectivityMode.Local:
                    return Local.CheckLocalDatabaseConnection();

                case DatabaseConnectivityMode.Remote:
                    return Remote.CheckRemoteDatabaseConnection();

                default:
                    return false;
            }
        }


        /// <summary>
        /// Checks if the database exists.
        /// </summary>
        /// <param name="DatabaseName"></param>
        /// <returns></returns>
        public static bool CheckDatabaseExists(string DatabaseName)
        {
            switch (ConnectionMode)
            {
                case DatabaseConnectivityMode.Local:
                    return Local.CheckLocalDatabaseExists(DatabaseName);

                case DatabaseConnectivityMode.Remote:
                    return Remote.CheckRemoteDatabaseExists(DatabaseName);

                default:
                    return false;
            }
        }


        /// <summary>
        /// Checks if table exists within the database.
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static bool CheckDatabaseTableExists(string TableName)
        {
            switch (ConnectionMode)
            {
                case DatabaseConnectivityMode.Local:
                    return Local.CheckLocalDatabaseTableExists(TableName);

                case DatabaseConnectivityMode.Remote:
                    return Remote.CheckRemoteDatabaseTableExists(TableName);

                default:
                    return false;
            }
        }


        public static bool CheckStoredProcedureExists(string StoredProcedureName)
        {
            return ConnectionMode.Equals(DatabaseConnectivityMode.Remote) && Remote.CheckRemoteStoredProcedureExists(StoredProcedureName);
        }

        #endregion

        #region "  Local Database  "

        public static class Local
        {

            #region "  Variables  "

            private readonly static string DatabaseName = $"{SystemConfig.GetValue("DatabaseName")}.db";

            public readonly static string LocalConnectionString = $"Data Source={DatabaseName};";

            #endregion

            #region "  Methods  "

            /// <summary>
            /// Checks if the database can be connected to.
            /// </summary>
            /// <returns></returns>
            public static bool CheckLocalDatabaseConnection()
            {
                bool SuccessfulConnection;

                SqliteConnection Connection = new SqliteConnection(ConnectionString);
                try
                {
                    Connection.Open();
                    SuccessfulConnection = true;
                    Connection.Close();
                }
                catch
                {
                    SuccessfulConnection = false;
                }

                return SuccessfulConnection;
            }

            /// <summary>
            /// Checks if the database exists.
            /// </summary>
            /// <param name="DatabaseName"></param>
            /// <returns></returns>
            public static bool CheckLocalDatabaseExists(string DatabaseName)
            {
                return File.Exists(DatabaseName);
            }

            /// <summary>
            /// Checks if table exists within the database.
            /// </summary>
            /// <param name="TableName"></param>
            /// <returns></returns>
            public static bool CheckLocalDatabaseTableExists(string TableName)
            {
                int DatabaseTableCount = 0;

                using (SqliteConnection Connection = new SqliteConnection(ConnectionString))
                {
                    Connection.Open();

                    using (SqliteCommand Command = new SqliteCommand())
                    {
                        Command.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{TableName}'";
                        DatabaseTableCount = Convert.ToInt32(Command.ExecuteScalar());
                    }

                    Connection.Close();
                }

                return DatabaseTableCount > 0;
            }

            #endregion

        }

        #endregion

        #region "  Remote Database  "

        public static class Remote
        {

            #region "  Variables  "

            public readonly static string RemoteConnectionString = ConfigurationManager.ConnectionStrings["ModularDatabase"].ConnectionString;

            #endregion

            #region "  Methods  "

            /// <summary>
            /// Checks if database can be connected.
            /// </summary>
            /// <returns></returns>
            public static bool CheckRemoteDatabaseConnection()
            {
                bool SuccessfulConnection;

                SqlConnection Connection = new SqlConnection(ConnectionString);
                try
                {
                    Connection.Open();
                    SuccessfulConnection = true;
                    Connection.Close();
                }
                catch
                {
                    SuccessfulConnection = false;
                }

                return SuccessfulConnection;
            }


            /// <summary>
            /// Checks if the database exists.
            /// </summary>
            /// <param name="DatabaseName"></param>
            /// <returns></returns>
            public static bool CheckRemoteDatabaseExists(string DatabaseName)
            {
                int DatabaseCount = 0;

                using (SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    Connection.Open();

                    string Query = $"SELECT COUNT(*) FROM sys.databases WHERE name = '{DatabaseName}'";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        DatabaseCount = Convert.ToInt32(Command.ExecuteScalar());
                    }

                    Connection.Close();
                }

                return DatabaseCount > 0;
            }

            /// <summary>
            /// Checks if table exists within the database.
            /// </summary>
            /// <param name="TableName"></param>
            /// <returns></returns>
            public static bool CheckRemoteDatabaseTableExists(string TableName)
            {
                int DatabaseTableCount = 0;

                using (SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    Connection.Open();

                    string Query = $"SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{TableName}'";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        DatabaseTableCount = Convert.ToInt32(Command.ExecuteScalar());
                    }
                    Connection.Close();
                }

                return DatabaseTableCount > 0;
            }


            /// <summary>
            /// Checks stored procedure exists within the database.
            /// </summary>
            /// <param name="StoredProcedureName"></param>
            /// <returns></returns>
            public static bool CheckRemoteStoredProcedureExists(string StoredProcedureName)
            {
                int StoredProcedureCount = 0;

                using (SqlConnection Connection = new SqlConnection(ConnectionString))
                {
                    Connection.Open();

                    string Query = $"SELECT COUNT(*) FROM sys.procedures WHERE name = '{StoredProcedureName}'";
                    using (SqlCommand Command = new SqlCommand(Query, Connection))
                    {
                        StoredProcedureCount = Convert.ToInt32(Command.ExecuteScalar());
                    }

                    Connection.Close();
                }

                return StoredProcedureCount > 0;
            }


            #endregion
        }

        #endregion

    }
}
