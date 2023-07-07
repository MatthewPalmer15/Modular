﻿using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Reflection;

namespace Modular.Core
{

    public class RolePermission : ModularBase
    {
        #region "  Constructors  "

        public RolePermission()
        {
        }

        #endregion

        #region "  Constants  "

        protected static new readonly string MODULAR_DATABASE_TABLE = "tbl_Modular_Role_Permission";

        #endregion

        #region "  Variables  "

        private Guid _AccountID;

        private Enum _Permission;

        #endregion

        #region "  Properties  "

        public Guid AccountID
        {
            get
            {
                return _AccountID;
            }
            set
            {
                if (_AccountID != value)
                {
                    _AccountID = value;
                    OnPropertyChanged("AccountID");
                }
            }
        }

        public Enum Permission
        {
            get
            {
                return _Permission;
            }
            set
            {
                if (_Permission != value)
                {
                    // Need to check to see if Permission actually exists
                    _Permission = value;
                    OnPropertyChanged("Permission");
                }
            }
        }

        #endregion

        #region "  Static Methods  "

        public static new RolePermission Create()
        {
            RolePermission obj = new RolePermission();
            obj.SetDefaultValues();
            return obj;
        }
        public static new List<RolePermission> LoadInstances()
        {
            return FetchAll();
        }

        public static new RolePermission Load(Guid ID)
        {
            RolePermission obj = new RolePermission();
            obj.Fetch(ID);
            return obj;
        }

        public static RolePermission Load(Guid AccountID, Enum Permission)
        {
            RolePermission obj = new RolePermission();
            obj.DataFetch(AccountID, Permission);
            return obj;
        }




        #endregion

        #region "  Instance Methods  "

        public override string ToString()
        {
            return Permission.ToString();
        }

        #endregion

        #region "  Data Methods  "

        protected void DataFetch(Guid prAccountID, Enum prPermission)
        {
            SqlConnection cn = new SqlConnection(Database.ConnectionString);
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.CommandText = $"_Fetch"; //todo: add stored procedure name

            cm.Parameters.AddWithValue("@ContactID", prAccountID);
            cm.Parameters.AddWithValue("@Permission", prPermission);

            SqlDataReader dr = cm.ExecuteReader();

            dr.Read();

            ID = dr.GetGuid(dr.GetOrdinal("ID"));
            CreatedDate = dr.GetDateTime(dr.GetOrdinal("CreatedDate"));
            CreatedBy = dr.GetGuid(dr.GetOrdinal("CreatedBy"));
            ModifiedDate = dr.GetDateTime(dr.GetOrdinal("ModifiedDate"));
            ModifiedBy = dr.GetGuid(dr.GetOrdinal("ModifiedBy"));

            AccountID = dr.GetGuid(dr.GetOrdinal("AccountID"));
        }


        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static List<RolePermission> FetchAll()
        {
            List<RolePermission> AllPermissions = new List<RolePermission>();

            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;

                PropertyInfo[]? AllProperties = GetProperties();

                switch (DatabaseConnectionMode)
                {

                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                            {
                                using (SqlCommand Command = new SqlCommand())
                                {
                                    string StoredProcedureName = $"_FetchAll"; ///todo: add stored procedure name

                                    if (Database.CheckStoredProcedureExists(StoredProcedureName))
                                    {
                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.StoredProcedure;
                                        Command.CommandText = StoredProcedureName;
                                    }
                                    else
                                    {
                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.Text;
                                        Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);
                                    }

                                    using (SqlDataReader DataReader = Command.ExecuteReader())
                                    {
                                        RolePermission obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllPermissions.Add(obj);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                            }

                            Connection.Close();
                        }
                        break;

                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                            {
                                using (SqliteCommand Command = new SqliteCommand())
                                {

                                    Command.Connection = Connection;
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                    using (SqliteDataReader DataReader = Command.ExecuteReader())
                                    {
                                        RolePermission obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllPermissions.Add(obj);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                            }
                            Connection.Close();
                        }
                        break;
                }

            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "There was an issue trying to connect to the database.");
            }

            return AllPermissions;
        }

        /// <summary>
        /// Fetches all the contacts from the database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ModularException"></exception>
        protected static List<RolePermission> FetchAll(Guid RoleID)
        {
            List<RolePermission> AllPermissions = new List<RolePermission>();

            if (Database.CheckDatabaseConnection())
            {
                Database.DatabaseConnectivityMode DatabaseConnectionMode = Database.ConnectionMode;

                PropertyInfo[] AllProperties = GetProperties();

                switch (DatabaseConnectionMode)
                {

                    case Database.DatabaseConnectivityMode.Remote:
                        using (SqlConnection Connection = new SqlConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                            {
                                using (SqlCommand Command = new SqlCommand())
                                {
                                    string StoredProcedureName = $"_FetchAll"; ///todo: add stored procedure name

                                    if (Database.CheckStoredProcedureExists(StoredProcedureName))
                                    {
                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.StoredProcedure;
                                        Command.CommandText = StoredProcedureName;
                                        Command.Parameters.Add(new SqlParameter("@RoleID", RoleID));
                                    }
                                    else
                                    {
                                        Command.Connection = Connection;
                                        Command.CommandType = CommandType.Text;
                                        Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);
                                        Command.Parameters.Add(new SqlParameter("@RoleID", RoleID));
                                    }

                                    using (SqlDataReader DataReader = Command.ExecuteReader())
                                    {
                                        RolePermission obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllPermissions.Add(obj);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                            }

                            Connection.Close();
                        }
                        break;

                    case Database.DatabaseConnectivityMode.Local:
                        using (SqliteConnection Connection = new SqliteConnection(Database.ConnectionString))
                        {
                            Connection.Open();

                            if (Database.CheckDatabaseTableExists(MODULAR_DATABASE_TABLE))
                            {
                                using (SqliteCommand Command = new SqliteCommand())
                                {

                                    Command.Connection = Connection;
                                    Command.CommandType = CommandType.Text;
                                    Command.CommandText = DatabaseQueryUtils.CreateFetchQuery(MODULAR_DATABASE_TABLE);

                                    using (SqliteDataReader DataReader = Command.ExecuteReader())
                                    {
                                        RolePermission obj = GetOrdinals(DataReader);

                                        while (DataReader.Read())
                                        {
                                            AllPermissions.Add(obj);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                DatabaseUtils.CreateDatabaseTable(MODULAR_DATABASE_TABLE, AllProperties);
                            }
                            Connection.Close();
                        }

                        // This should run the function again, but this time it should be able to find the table
                        break;
                }

            }
            else
            {
                throw new ModularException(ExceptionType.DatabaseConnectionError, "There was an issue trying to connect to the database.");
            }

            return AllPermissions;
        }

        protected static RolePermission GetOrdinals(SqlDataReader DataReader)
        {
            RolePermission obj = new RolePermission();

            PropertyInfo[]? AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        protected static RolePermission GetOrdinals(SqliteDataReader DataReader)
        {
            RolePermission obj = new RolePermission();

            PropertyInfo[]? AllProperties = GetProperties();
            if (AllProperties != null)
            {
                obj.SetPropertyValues(AllProperties, DataReader);
            }

            return obj;
        }

        #endregion
    }
}