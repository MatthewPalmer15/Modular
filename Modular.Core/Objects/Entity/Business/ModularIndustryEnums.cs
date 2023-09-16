using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Entity
{
    public partial class Industry
    {

        public static readonly Guid MODULAR_INDUSTRY_ID_AGRICULTURE = new Guid("e009fdf4-b344-4b94-80ec-6d6227ed8727");
        public static readonly Guid MODULAR_INDUSTRY_ID_ART_AND_CULTURE = new Guid("35397759-107c-4e16-8ea5-b6e40701ef9d");
        public static readonly Guid MODULAR_INDUSTRY_ID_AUTOMOTIVE = new Guid("64128dd5-04ab-4d34-98d5-973034a2fa6e");
        public static readonly Guid MODULAR_INDUSTRY_ID_BANKING = new Guid("52b40e51-94e3-4360-8d71-a428287dd564");
        public static readonly Guid MODULAR_INDUSTRY_ID_CONSTRUCTION = new Guid("7936b5bf-a4ad-42d6-9b3e-18fa4a8ab5e8");
        public static readonly Guid MODULAR_INDUSTRY_ID_CONSULTING = new Guid("07e88f61-a40f-4e9c-a8c5-e2905d915e20");
        public static readonly Guid MODULAR_INDUSTRY_ID_EDUCATION = new Guid("510dd663-0b47-4f38-aa60-724ee9da95d1");
        public static readonly Guid MODULAR_INDUSTRY_ID_ENERGY = new Guid("da4fbcd9-5c2e-45e0-8d05-23e7a8838f2d");
        public static readonly Guid MODULAR_INDUSTRY_ID_ENTERTAINMENT = new Guid("69200f95-959e-4b28-be26-44171db67b35");
        public static readonly Guid MODULAR_INDUSTRY_ID_ENVIRONMENTAL = new Guid("36dd7f20-faf4-4dbb-a578-32ec362e7aa6");
        public static readonly Guid MODULAR_INDUSTRY_ID_FASHION = new Guid("05d3f204-db21-4288-861b-7daafcf28627");
        public static readonly Guid MODULAR_INDUSTRY_ID_FINANCE = new Guid("a99d7054-3302-4f89-888f-00fe01f14dce");
        public static readonly Guid MODULAR_INDUSTRY_ID_FOOD_AND_BEVERAGE = new Guid("7df5f34e-db0b-4edb-9d94-517780fc6869");
        public static readonly Guid MODULAR_INDUSTRY_ID_GOVERNMENT = new Guid("fe5055b7-1985-4503-9118-b173f4ba5508");
        public static readonly Guid MODULAR_INDUSTRY_ID_HEALTHCARE = new Guid("b7541b30-e410-432c-a9ca-5a8e57ff7400");
        public static readonly Guid MODULAR_INDUSTRY_ID_HOSPITALITY = new Guid("78796a3e-c7b5-4b5b-8efb-88731b3fad6f");
        public static readonly Guid MODULAR_INDUSTRY_ID_INSURANCE = new Guid("53a77db5-5ddd-46fd-b3d3-cd1147c6adbf");
        public static readonly Guid MODULAR_INDUSTRY_ID_LEGAL = new Guid("5af33855-26ba-4f9e-a901-072a7765f4ac");
        public static readonly Guid MODULAR_INDUSTRY_ID_MANUFACTURING = new Guid("25b7b2a8-da99-4735-9dba-dbcd6ae3e64a");
        public static readonly Guid MODULAR_INDUSTRY_ID_MEDIA = new Guid("a0f3181b-1526-4481-a502-35b5fe881e93");
        public static readonly Guid MODULAR_INDUSTRY_ID_NON_PROFIT = new Guid("b8e7d44d-f1d5-47b8-92aa-c86f8075da32");
        public static readonly Guid MODULAR_INDUSTRY_ID_PHARMACEUTICALS = new Guid("fbfd54cb-45b6-4cf4-8508-8d536a6166cb");
        public static readonly Guid MODULAR_INDUSTRY_ID_REAL_ESTATE = new Guid("d268136d-3cc7-4389-a049-655d3623a2f2");
        public static readonly Guid MODULAR_INDUSTRY_ID_RETAIL = new Guid("320034bb-6dc4-44d0-b951-8ee03cb45885");
        public static readonly Guid MODULAR_INDUSTRY_ID_SPORTS = new Guid("d0f3cfa3-1bad-43c1-963a-cd48e6964b74");
        public static readonly Guid MODULAR_INDUSTRY_ID_TECHNOLOGY = new Guid("676e98d1-3549-4adf-8bdd-4a7be36258e3");
        public static readonly Guid MODULAR_INDUSTRY_ID_TELECOMMUNICATIONS = new Guid("1ebf9ddc-115e-477b-845f-4721bdcf09bf");
        public static readonly Guid MODULAR_INDUSTRY_ID_TOURISM = new Guid("2eb2627a-7053-4716-be0f-cded0b29e6f3");
        public static readonly Guid MODULAR_INDUSTRY_ID_TRANSPORTATION = new Guid("c86b7711-b844-4ffa-bbb6-aa6e857e7c65");



    }
}
