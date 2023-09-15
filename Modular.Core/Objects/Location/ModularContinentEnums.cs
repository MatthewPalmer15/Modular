using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.Data;
using System.Reflection;

namespace Modular.Core.Geo
{
    public partial class Continent
    {

        public static readonly Guid MODULAR_CONTINENT_ID_ASIA = new Guid("551d53d9-b231-47ff-817a-5d1131617114");
        public static readonly Guid MODULAR_CONTINENT_ID_AFRICA = new Guid("ec6766f9-7998-4594-8ab2-fb2c7e34436f");
        public static readonly Guid MODULAR_CONTINENT_ID_EUROPE = new Guid("a4f60ae5-cc00-4b76-a9f9-abe604400125");
        public static readonly Guid MODULAR_CONTINENT_ID_NORTH_AMERICA = new Guid("7d50ac44-692a-4d95-b2ad-0a2e73cedf75");
        public static readonly Guid MODULAR_CONTINENT_ID_SOUTH_AMERICA = new Guid("dbde19c6-2607-4c6d-8073-a28a20b1d86f");
        public static readonly Guid MODULAR_CONTINENT_ID_OCEANIA = new Guid("57cbde2e-5ed5-4d0f-acd3-3e53570b0c82");

    }
}
