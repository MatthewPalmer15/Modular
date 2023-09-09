namespace Modular.Core.Utility
{
    public static class ObjectTypes
    {
        public enum ObjectType
        {
            Unknown                 = 000000,

            //  MODULAR.CORE
            Base                    = 000001,
            BindableBase            = 000002,
            Document                = 000003,
            DocumentPack            = 000004,
            Email                   = 000005,
            EmailLog                = 000006,
            Contact                 = 000007,
            Organisation            = 000008,
            Account                 = 000009,
            AccountProfile          = 000010,
            AccountRole             = 000011,
            AccountRolePermission   = 000012,
            Department              = 000013,
            Industry                = 000014,
            Occupation              = 000015,
            Country                 = 000016,
            Region                  = 000017,
            Credit                  = 000018,
            CreditItem              = 000019,
            CreditPayment           = 000020,
            DiscountVoucher         = 000021,
            Invoice                 = 000022,
            InvoiceItem             = 000023,
            InvoicePayment          = 000024,
            AppConfig               = 000025,
            SystemConfig            = 000026,
            Exception               = 000027,
            ExceptionLog            = 000028,
            Owner                   = 000029,
            Notification            = 000030,
            AuditLog                = 000031,
            System                  = 000032,
            SystemPolicy            = 000033,
            ApplicationPage         = 000034,
            ApplicationMultiPage    = 000035,
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
