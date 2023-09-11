namespace Modular.Core.Utility
{

    public static class EnumUtils
    {
        public enum YesNoOption
        {
            Unknown = 0,
            Yes = 1,
            No = 2
        }

        public enum StatusType
        {
            Unknown = 0,
            Unverified = 1,
            Verified = 2,
            Cancelled = 3,
            Deleted = 4,
        }

        public enum PaymentMethodType
        {
            Unknown = 0,
            Cash = 1,
            Cheque = 2,
            CreditCard = 3,
            DirectDebit = 4,
            EFT = 5,
            PayPal = 6
        }


        public enum SourceType
        {
            Unknown = 0,
            Website = 1,
            BackOffice = 2,
            Telephone = 3,
            Email = 4,
            InPerson = 5
        }

        public enum TimeType
        {
            Seconds,
            Minutes,
            Hours,
            Days,
            Weeks,
            Months,
            Years
        }
    }
}
