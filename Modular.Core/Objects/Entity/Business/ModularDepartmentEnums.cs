using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Modular.Core.Databases;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Reflection;

namespace Modular.Core.Entity
{
    public partial class Department
    {

        public static readonly Guid MODULAR_DEPARTMENT_ID_ACCOUNTING = new Guid("47104f06-4568-4490-889b-5a4b33ba3162");
        public static readonly Guid MODULAR_DEPARTMENT_ID_ADMINISTRATION = new Guid("c53aa86f-bfdc-45d4-ba23-4967db736c85");
        public static readonly Guid MODULAR_DEPARTMENT_ID_BUSINESS_DEVELOPMENT = new Guid("fe925e62-b09c-402b-a7d5-daa875de1508");
        public static readonly Guid MODULAR_DEPARTMENT_ID_CLIENT_SERVICES = new Guid("57d7a666-62fb-4d3b-8311-8b16e8afc12a");
        public static readonly Guid MODULAR_DEPARTMENT_ID_COMPLIANCE = new Guid("4cbc6449-24a6-4c72-87dd-704dc7bd916b");
        public static readonly Guid MODULAR_DEPARTMENT_ID_CONTENT_CREATION = new Guid("9f46356f-c1f3-4f5f-a34e-4f0573c3742f");
        public static readonly Guid MODULAR_DEPARTMENT_ID_CUSTOMER_SERVICE = new Guid("ffda88bb-76d5-4e83-ba37-b1081f1c6f7e");
        public static readonly Guid MODULAR_DEPARTMENT_ID_CUSTOMER_SUPPORT = new Guid("f6d54d57-e0b0-4b16-9f67-1b3eddfd2c30");
        public static readonly Guid MODULAR_DEPARTMENT_ID_DATA_ANALYSIS = new Guid("89d193d1-d3de-4d67-bffd-4f4cf9f5de44");
        public static readonly Guid MODULAR_DEPARTMENT_ID_ECOMMERCE  = new Guid("557d0c1f-ffd0-40d5-837e-6dd24c36fc1c");
        public static readonly Guid MODULAR_DEPARTMENT_ID_ENGINEERING = new Guid("2bf50ced-7a9f-41c3-8594-f52e2f6dac91");
        public static readonly Guid MODULAR_DEPARTMENT_ID_ENVIRONMENTAL_HEALTH_AND_SAFETY = new Guid("92da2007-3684-469b-ba35-5baae4e9a1c9");
        public static readonly Guid MODULAR_DEPARTMENT_ID_EVENT_PLANNING = new Guid("b733525c-38fc-46b9-a3e6-6c9b265c61c1");
        public static readonly Guid MODULAR_DEPARTMENT_ID_FACILITIES_MANAGEMENT = new Guid("9397030d-dcc2-4bf8-88d5-3951dfa83c0a");
        public static readonly Guid MODULAR_DEPARTMENT_ID_FINANCE = new Guid("f29d348a-6bd2-4830-b061-a483c459f0d7");
        public static readonly Guid MODULAR_DEPARTMENT_ID_GRAPHIC_DESIGN = new Guid("61f235dc-8418-42c9-92e9-dad7d492d2fc");
        public static readonly Guid MODULAR_DEPARTMENT_ID_HEALTHCARE_SERVICES = new Guid("89218a34-2f6a-46fa-a7f1-b2cdece15a82");
        public static readonly Guid MODULAR_DEPARTMENT_ID_HOSPITALITY = new Guid("53c4fb3d-eefe-414a-8a88-2d46b13dd67a");
        public static readonly Guid MODULAR_DEPARTMENT_ID_HUMAN_RESOURCES = new Guid("533a88cc-b810-4d4a-80a2-b6f35207830f");
        public static readonly Guid MODULAR_DEPARTMENT_ID_INFORMATION_TECHNOLOGY = new Guid("271fb472-2d0e-46f0-bbf6-4ab59dda73d4");
        public static readonly Guid MODULAR_DEPARTMENT_ID_INTERNAL_AUDIT = new Guid("75b5e088-13d3-40de-94c1-0bb02398ebd3");
        public static readonly Guid MODULAR_DEPARTMENT_ID_INTERNATIONAL_SALES = new Guid("e7b7bce2-46b8-4420-8383-f274a328cf70");
        public static readonly Guid MODULAR_DEPARTMENT_ID_INVESTOR_RELATIONS = new Guid("8089c4b4-8f5e-4f72-8896-93583239bfc0");
        public static readonly Guid MODULAR_DEPARTMENT_ID_LEGAL = new Guid("8de7ddfc-529f-46cd-bc0f-670a897d7553");
        public static readonly Guid MODULAR_DEPARTMENT_ID_MARKET_RESEARCH = new Guid("113ec207-853f-4eab-aec7-a61ea6c1ebc3");
        public static readonly Guid MODULAR_DEPARTMENT_ID_MARKETING = new Guid("796e922a-45f0-4c69-9352-0bfe401ad18d");
        public static readonly Guid MODULAR_DEPARTMENT_ID_MERCHANDISING = new Guid("d3d51ed7-20eb-47d9-8994-be1990b5ad63");
        public static readonly Guid MODULAR_DEPARTMENT_ID_NETWORK_ADMINISTRATION = new Guid("5952c6d1-6a3f-4af5-b039-2026276d7f42");
        public static readonly Guid MODULAR_DEPARTMENT_ID_OPERATIONS = new Guid("b18bb99e-ba9d-4219-972c-82bcdc031117");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PAYROLL = new Guid("c9c2c38d-bacb-4ec1-8f4d-15e244d26264");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PRODUCT_MANAGEMENT = new Guid("02abd055-2624-47fa-b3b5-48ead9ad0b9d");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PRODUCTION = new Guid("161b7046-537f-4021-98f2-16a4991cc65d");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PROJECT_MANAGEMENT = new Guid("679234a4-3f1a-4a77-ae5d-c561366c18b0");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PUBLIC_AFFAIRS = new Guid("87a76956-58b0-4702-8ef6-6999fe2a7b29");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PUBLIC_RELATIONS = new Guid("ff78c1a2-f7b0-400e-b627-941d8234c1c3");
        public static readonly Guid MODULAR_DEPARTMENT_ID_PURCHASING  = new Guid("81b62d02-ec60-4ab8-84ff-66bc62213dd6");
        public static readonly Guid MODULAR_DEPARTMENT_ID_QUALITY_ASSURANCE = new Guid("1645b533-371d-4e3e-856d-4c69b7881694");
        public static readonly Guid MODULAR_DEPARTMENT_ID_REAL_ESTATE = new Guid("5fa1a99f-6ee7-434e-b749-1cc8015e6d3b");
        public static readonly Guid MODULAR_DEPARTMENT_ID_REGULATORY_AFFAIRS = new Guid("ac6065a1-0f0d-4dc0-a7c0-9d41bb53023b");
        public static readonly Guid MODULAR_DEPARTMENT_ID_RESEARCH_AND_DEVELOPMENT = new Guid("6c66e834-9bfa-45f5-80f1-229081079ec2");
        public static readonly Guid MODULAR_DEPARTMENT_ID_RETAIL = new Guid("5851d184-e667-4164-97ab-5a3757369456");
        public static readonly Guid MODULAR_DEPARTMENT_ID_RISK_MANAGEMENT = new Guid("44d332b5-2f5f-42cc-84fc-95534593d501");
        public static readonly Guid MODULAR_DEPARTMENT_ID_SAFETY_AND_SECURITY = new Guid("11f30b57-b009-4b26-b454-e446e8aa51bb");
        public static readonly Guid MODULAR_DEPARTMENT_ID_SALES = new Guid("92a5428c-efe7-4b7f-9307-5ca5ec9ab568");
        public static readonly Guid MODULAR_DEPARTMENT_ID_SOCIAL_MEDIA = new Guid("46f14bfd-37d7-4fd3-87b5-ee630bcdd4e8");
        public static readonly Guid MODULAR_DEPARTMENT_ID_SOFTWARE_DEVELOPMENT = new Guid("3a874903-fdf1-4820-83cb-c3b3b4dd92f9");
        public static readonly Guid MODULAR_DEPARTMENT_ID_STRATEGIC_PLANNING = new Guid("a6835de0-7c62-4904-9886-c48a40a52e63");
        public static readonly Guid MODULAR_DEPARTMENT_ID_LOGISTICS  = new Guid("b8901d23-3f16-40f9-a8cc-c6674b4ef784");
        public static readonly Guid MODULAR_DEPARTMENT_ID_TELECOMMUNICATIONS = new Guid("4f444ffd-bbd6-480a-9c8a-5d4e84b579fb");
        public static readonly Guid MODULAR_DEPARTMENT_ID_TRAINING = new Guid("7e9f2b63-c2b9-429c-900f-09272175182f");
        public static readonly Guid MODULAR_DEPARTMENT_ID_TRAINING_AND_DEVELOPMENT = new Guid("0397b16a-897d-465e-a15b-7e5eba1b65c3");
        public static readonly Guid MODULAR_DEPARTMENT_ID_TRAVEL_AND_EXPENSE = new Guid("ce7de472-d6b4-4c63-8865-7717ac02793c");
        public static readonly Guid MODULAR_DEPARTMENT_ID_WAREHOUSING = new Guid("4ec87011-5c9a-43cc-914a-08b18bde2b42");


    }
}
