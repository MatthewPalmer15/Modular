using Modular.Core.Sequences;
using Modular.Core.Utility;

namespace Modular.Core.Invoicing
{
    public static class InvoiceUtils
    {

        public static void ProcessPayment(Guid InvoiceID, decimal Amount, EnumUtils.PaymentMethodType PaymentMethod, string Reference = "")
        {
            InvoicePayment NewInvoicePayment = InvoicePayment.Create(InvoiceID);
            NewInvoicePayment.Amount = Amount;
            NewInvoicePayment.PaymentMethod = PaymentMethod;
            NewInvoicePayment.Reference = Reference;
            NewInvoicePayment.PaymentDate = DateTime.Now;
            NewInvoicePayment.IsSuccessful = true;
            NewInvoicePayment.Save();
        }

        public static int GenerateInvoiceNumber()
        {
            return Sequence.GetNextNumber("InvoiceNumber");
        }

    }
}
