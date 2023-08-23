using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Interfaces
{
    public interface IModularBase
    {
        public IModularBase Create();
        public IModularBase Load(Guid ID);
        public List<IModularBase> LoadAll();
        public IModularBase Clone();

        public IModularBase GetOrdinals(SqlDataReader DataReader);
        public IModularBase GetOrdinals(SqliteDataReader DataReader);

    }
}
