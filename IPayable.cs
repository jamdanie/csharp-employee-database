/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description
// 03-02-2026 - KSuy          -- Created IPayable interface for payroll system
// 03-02-2026 - JDaniels      -- Defined GetPaymentAmount() method for payroll calculation
// 03-02-2026 - SBalamurugan  -- Added ToStringForOutputFile() method for file persistence
//
// Notes:
// This interface enables polymorphism in the EmpDB payroll system.
//
// Any class that represents something that must be paid implements IPayable.
// In this project that includes:
//
// Employee classes
// Invoice objects
//
// Because both implement IPayable, they can be stored together in:
//
// List<IPayable>
//
// This allows the payroll system to process both employees and invoices
// using the same loop without needing separate lists.
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    // Interface representing anything that can receive payment
    public interface IPayable
    {
        // Calculates the payment amount for the object
        // Employees return their payroll amount
        // Invoices return quantity * price
        decimal GetPaymentAmount();

        // Returns a string representation formatted for saving
        // the object back into the database file
        string ToStringForOutputFile();
    }
}