using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modular.Core.Objects
{
    public static partial class DocumentGenerator
    {

        public static void GenerateInvoiceDocument(Invoice Invoice, FileInfo Template)
        {
            GenerateInvoiceDocument(Invoice, Template.FullName);
        }

        public static void GenerateInvoiceDocument(Invoice Invoice, string TemplatePath)
        {
            using (WordprocessingDocument WordDocument = WordprocessingDocument.Open(TemplatePath, true))
            {
                Body WordContent = WordDocument.MainDocumentPart.Document.Body;

                foreach (var WordElements in WordContent.Elements())
                {

                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#InvoiceNumber#]", Invoice.InvoiceNumber);
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#InvoiceDate#]", Invoice.InvoiceDate.ToString("dd MMMM yyyy"));
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#DueDate#]", Invoice.DueDate.ToString("dd MMMM yyyy"));
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#ContactFullName#]", Invoice.Contact.FullName);
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#ContactEmail#]", Invoice.Contact.Email);
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#ContactFullAddressOneLine#]", Invoice.Contact.FullAddressOneLine);
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#ContactFullAddressMultiLine#]", Invoice.Contact.FullAddressMultiLine);
                    WordElements.InnerXml = WordElements.InnerXml.Replace("[#InvoiceItemsTable#]", CreateInvoiceItemsTable(Invoice).OuterXml);

                }
            }
        }

        private static Table CreateInvoiceItemsTable(Invoice Invoice)
        {
            // Create an empty table.
            Table InvoiceItemsTable = new Table();

            // Create a Header Row for the Invoice Items Table.
            TableRow InvoiceItemsHeaderRow = new TableRow();

            TableCell InvoiceItemsHeaderCellName = CreateTableCell("Name");
            TableCell InvoiceItemsHeaderCellDescription = CreateTableCell("Description");
            TableCell InvoiceItemsHeaderCellUnitPrice = CreateTableCell("Unit Price");
            TableCell InvoiceItemsHeaderCellUnitPriceVAT = CreateTableCell("Unit Price VAT");
            TableCell InvoiceItemsHeaderCellQuantity = CreateTableCell("Quantity");
            TableCell InvoiceItemsHeaderCellTotalPrice = CreateTableCell("Total Price");

            InvoiceItemsHeaderRow.Append(
                InvoiceItemsHeaderCellName, InvoiceItemsHeaderCellDescription, 
                InvoiceItemsHeaderCellUnitPrice, InvoiceItemsHeaderCellUnitPriceVAT, 
                InvoiceItemsHeaderCellQuantity, InvoiceItemsHeaderCellTotalPrice
            );

            InvoiceItemsTable.Append(InvoiceItemsHeaderRow);


            // Create a row for each Invoice Item.
            foreach (InvoiceItem Item in Invoice.InvoiceItems)
            {
                TableRow InvoiceItemRow = new TableRow();

                TableCell InvoiceItemCellName = CreateTableCell(Item.Name);
                TableCell InvoiceItemCellDescription = CreateTableCell(Item.Description);
                TableCell InvoiceItemCellUnitPrice = CreateTableCell(Item.UnitPrice.ToString("£#.00"));
                TableCell InvoiceItemCellUnitPriceVAT = CreateTableCell(Item.UnitPriceVAT.ToString("£#.00"));
                TableCell InvoiceItemCellQuantity = CreateTableCell(Item.Quantity.ToString());
                TableCell InvoiceItemCellTotalPrice = CreateTableCell(Item.TotalPrice.ToString("£#.00"));

                InvoiceItemRow.Append(InvoiceItemCellName, InvoiceItemCellDescription, InvoiceItemCellUnitPrice, InvoiceItemCellUnitPriceVAT, InvoiceItemCellQuantity, InvoiceItemCellTotalPrice);

                InvoiceItemsTable.Append(InvoiceItemRow);
            }

            return InvoiceItemsTable;
        }

        private static TableCell CreateTableCell(string Text)
        {
            return new TableCell(new Paragraph(new Run(Text)));
        }
    }
}
