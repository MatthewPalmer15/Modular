using Modular.Core.System;
using System.Linq;

namespace Modular.Core.Security
{
    public static class Security
    {

        /// <summary>
        /// Checks to see if the current user has the specified security permission. Returns true if the user has the permission, false if not.
        /// </summary>
        /// <param name="SecurityPermission"></param>
        /// <returns></returns>
        public static bool CheckSecurity(Enum SecurityPermission)
        {
            return ModularSystem.Context.Identity.Role.Permissions.First(RolePermission => RolePermission.Permission == SecurityPermission) != null;
        }


        #region "  Enums  "

        ////////////////////////////////////////////
        //  MODULAR.CORE
        
        // Document
        public enum Document
        {
            View        = 100000,
            Fetch       = 100001,
            Insert      = 100002,
            Update      = 100003,
            Delete      = 100004,
        }

        public enum DocumentPack
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        // Email

        public enum Email
        {
            Create  = 200000,
            Send    = 200001,
        }

        public enum EmailLog
        {             
            View = 200005,
            Fetch = 200006,
            Insert = 200007,
            Update = 200008,
            Delete = 200009,      
        }

        public enum SMTPClient
        {
            Access = 200010,
        }

        // Entity

        public enum Contact
        {
            View = 100000,
            Fetch = 100001,
            Insert = 100002,
            Update = 100003,
            Delete = 100004,
        }

        public enum Organisation
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Account
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum AccountProfile
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum AccountRole
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum AccountRolePermission
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Department
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Industry
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Occupation
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        // Location

        public enum Country
        {
            View = 100005, 
            Fetch = 100006, 
            Insert = 100007, 
            Update = 100008, 
            Delete = 100009,
        }

        public enum Region
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        // Payment

        public enum Credit
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum CreditItem
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum CreditPayment
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009, 
        }

        public enum DiscountVoucher
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Invoice
        {
            View = 100005, 
            Fetch = 100006, 
            Insert = 100007, 
            Update = 100008, 
            Delete = 100009,
        }

        public enum InvoiceItem
        {
            View = 100005, 
            Fetch = 100006, 
            Insert = 100007, 
            Update = 100008, 
            Delete = 100009,
        }

        public enum InvoicePayment
        {
            View = 100005, 
            Fetch = 100006, 
            Insert = 100007, 
            Update = 100008, 
            Delete = 100009,
        }

        // System

        public enum AppConfig
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum SystemConfig
        {
            View = 100005, 
            Fetch = 100006, 
            Insert = 100007, 
            Update = 100008, 
            Delete = 100009,
        }

        public enum Encryption
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum AuditLog
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum SystemPolicy
        {
            View = 100005,
            Fetch = 100006,
            Insert = 100007,
            Update = 100008,
            Delete = 100009,
        }

        public enum Printer
        {
            Print = 200001,
        }
        ////////////////////////////////////////////

        #endregion

    }
}