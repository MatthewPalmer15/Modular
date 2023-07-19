namespace Modular.Core.Structures
{
    public struct InvoiceLine
    {

        #region "  Constructors  "

        public InvoiceLine(Guid InvoiceID, string Name, string Description, decimal UnitPrice, decimal Quantity, decimal TotalPrice)
        {
            this.InvoiceID = InvoiceID;
            this.Name = Name;
            this.Description = Description;
            this.UnitPrice = UnitPrice;
            this.Quantity = Quantity;
            this.TotalPrice = TotalPrice;
        }

        #endregion

        #region "  Properties  "

        public Guid InvoiceID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        #endregion

    }
}
