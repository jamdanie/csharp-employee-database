/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description

// 03-02-2026 - KSuy          -- Integrated Invoice class into EmpDB payable system
// 03-02-2026 - JDaniels      -- Implemented IPayable interface for payroll processing
// 03-04-2026 - SBalamurugan  -- Implemented GetPaymentAmount() calculation
// 03-04-2026 - KSuy          -- Implemented console display formatting via ToString()
// 03-04-2026 - JDaniels      -- Implemented ToStringForOutputFile() for database persistence
// 03-05-2026 - SBalamurugan  -- Set PartNumber as read-only primary identifier
//
// Notes:
//
// The Invoice class represents external vendor charges that the company must pay.
// Examples include equipment purchases, services, or supplies.
//
// Unlike employee payroll objects, invoices do NOT inherit from Employee.
// Instead they directly implement the IPayable interface.
//
// This demonstrates polymorphism because the payroll system can process both:
//
// Employee payments
// Vendor invoices
//
// using the same List<IPayable> collection.
//
// Payment Formula:
//
// PaymentAmount = Quantity × PricePerItem
//
// The PartNumber acts as the primary identifier for invoice records when
// searching, updating, or deleting invoices in the EmpDB database.
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    // Represents a payable vendor invoice
    public class Invoice : IPayable
    {
        // Unique identifier for the invoice
        public string PartNumber { get; }

        // Description of the product or service
        public string PartDescription { get; set; }

        // Quantity of items purchased
        public int Quantity { get; set; }

        // Price for each item
        public decimal PricePerItem { get; set; }

        // Constructor initializes invoice data
        public Invoice(string partNumber, string partDescription, int quantity, decimal pricePerItem)
        {
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if (pricePerItem < 0) throw new ArgumentOutOfRangeException(nameof(pricePerItem));

            PartNumber = partNumber;
            PartDescription = partDescription;
            Quantity = quantity;
            PricePerItem = pricePerItem;
        }

        // Calculates how much must be paid for the invoice
        public decimal GetPaymentAmount() => Quantity * PricePerItem;

        // Console display formatting
        public override string ToString()
        {
            string str = string.Empty;

            str += "Invoice:\n";
            str += $"Part Number: {PartNumber} ({PartDescription})\n";
            str += $"Quantity: {Quantity}\n";
            str += $"Price Per Item: {PricePerItem:C}";

            return str;
        }

        // File output formatting for database persistence
        public string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";   // Invoice
            str += $"{PartNumber}\n";
            str += $"{PartDescription}\n";
            str += $"{Quantity}\n";
            str += $"{PricePerItem:F2}";

            return str;
        }
    }
}
