namespace Modular.Core.Utility
{
    public static class ObjectTypes
    {
        public enum ObjectType
        {
            Unknown = 000000,

            //  MODULAR.CORE
            Base = 000001,
            BindableBase = 000002,
            Document = 000003,
            DataExporter = 000004,
            DocumentPack = 000005,
            Email = 000006,
            EmailLog = 000007,
            Contact = 000008,
            Organisation = 000009,
            Account = 000010,
            AccountProfile = 000011,
            AccountRole = 000012,
            AccountRolePermission = 000013,
            Department = 000014,
            Industry = 000015,
            Occupation = 000016,
            Country = 000017,
            Region = 000018,
            Credit = 000019,
            CreditItem = 000020,
            CreditPayment = 000021,
            DiscountVoucher = 000022,
            Invoice = 000023,
            InvoiceItem = 000024,
            InvoicePayment = 000025,
            AppConfig = 000026,
            SystemConfig = 000027,
            Exception = 000028,
            ExceptionLog = 000029,
            Owner = 000030,
            Notification = 000031,
            AuditLog = 000032,
            System = 000033,
            SystemPolicy = 000034,
            ApplicationPage = 000035,
            ApplicationMultiPage = 000036,
            ScheduledTask = 000037,
        }

        public static ObjectType ConvertStringToObjectType(string ObjectTypeString)
        {
            switch (ObjectTypeString.Trim().ToUpper())
            {
                // MODULAR.CORE

                case "BASE":
                    return ObjectType.Base;

                case "BINDABLEBASE":
                    return ObjectType.BindableBase;

                case "DOCUMENT":
                    return ObjectType.Document;

                case "DOCUMENTPACK":
                    return ObjectType.DocumentPack;

                case "EMAIL":
                    return ObjectType.Email;

                case "EMAILLOG":
                    return ObjectType.EmailLog;

                case "CONTACT":
                    return ObjectType.Contact;

                case "ORGANISATION":
                    return ObjectType.Organisation;

                case "ACCOUNT":
                    return ObjectType.Account;

                case "ACCOUNTPROFILE":
                    return ObjectType.AccountProfile;

                case "ACCOUNTROLE":
                    return ObjectType.AccountRole;

                case "ACCOUNTROLEPERMISSION":
                    return ObjectType.AccountRolePermission;

                case "DEPARTMENT":
                    return ObjectType.Department;

                case "INDUSTRY":
                    return ObjectType.Industry;

                case "OCCUPATION":
                    return ObjectType.Occupation;

                case "COUNTRY":
                    return ObjectType.Country;

                case "REGION":
                    return ObjectType.Region;

                case "CREDIT":
                    return ObjectType.Credit;

                case "CREDITITEM":
                    return ObjectType.CreditItem;

                case "CREDITPAYMENT":
                    return ObjectType.CreditPayment;

                case "DISCOUNTVOUCHER":
                    return ObjectType.DiscountVoucher;

                case "INVOICE":
                    return ObjectType.Invoice;

                case "INVOICEITEM":
                    return ObjectType.InvoiceItem;

                case "INVOICEPAYMENT":
                    return ObjectType.InvoicePayment;

                case "APPCONFIG":
                    return ObjectType.AppConfig;

                case "SYSTEMCONFIG":
                    return ObjectType.SystemConfig;

                case "EXCEPTION":
                    return ObjectType.Exception;

                case "EXCEPTIONLOG":
                    return ObjectType.ExceptionLog;

                case "OWNER":
                    return ObjectType.Owner;

                case "NOTIFICATION":
                    return ObjectType.Notification;

                case "AUDITLOG":
                    return ObjectType.AuditLog;

                case "SYSTEM":
                    return ObjectType.System;

                case "SYSTEMPOLICY":
                    return ObjectType.SystemPolicy;

                case "APPLICATIONPAGE":
                    return ObjectType.ApplicationPage;

                case "APPLICATIONMULTIPAGE":
                    return ObjectType.ApplicationMultiPage;

                default:
                    return ObjectType.Unknown;
            }
        }



    }
}
