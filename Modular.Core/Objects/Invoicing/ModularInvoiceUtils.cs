using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modular.Core.Sequences;

namespace Modular.Core.Invoicing
{ 
    public static class InvoiceUtils
    {

        public static void ProcessPayment(Guid InvoiceID, decimal Amount, InvoicePayment.PaymentMethodType PaymentMethod, string Reference = "")
        {
            InvoicePayment NewInvoicePayment = InvoicePayment.Create(InvoiceID);
            NewInvoicePayment.Amount = Amount;
            NewInvoicePayment.PaymentMethod = PaymentMethod;
            NewInvoicePayment.Reference = Reference;
            NewInvoicePayment.PaymentDate = DateTime.Now;
            NewInvoicePayment.IsSuccessful = true;
            NewInvoicePayment.Save();
        }

        public static string GenerateInvoiceNumber()
        {
            Sequence InvoiceNumberSequence = Sequence.Load("InvoiceNumber");
            InvoiceNumberSequence.Count++;
            InvoiceNumberSequence.Save();

            return (InvoiceNumberSequence.Count).ToString();
        }

        // TODO: Add support for this.
        // public static string GenerateInvoice(Membership Membership)
        // { 
        // }

    }
}
