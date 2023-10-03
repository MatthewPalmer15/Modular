using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace Modular.Core.Interfaces
{
    public interface IModularClass
    {

        protected static readonly string MODULAR_DATABASE_TABLE;
        protected static readonly string MODULAR_DATABASE_STOREDPROCEDURE_PREFIX;
        protected static readonly Type MODULAR_OBJECTTYPE;

        public IModularClass Create();
        public IModularClass Load(Guid ID);
        public List<IModularClass> LoadAll();
        public IModularClass Clone();
        public IModularClass GetOrdinals(SqlDataReader DataReader);
        public IModularClass GetOrdinals(SqliteDataReader DataReader);
    }
}
