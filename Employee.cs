/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description

// 03-02-2026 - KSuy          -- Integrated Employee class into EmpDB payroll system
// 03-02-2026 - JDaniels      -- Implemented IPayable interface for payroll processing
// 03-02-2026 - SBalamurugan  -- Connected Earnings() to GetPaymentAmount()
// 03-04-2026 - KSuy          -- Added EmailAddress as database primary key
// 03-04-2026 - JDaniels      -- Updated constructor to include email
// 03-04-2026 - SBalamurugan  -- Added ToString() formatting for console database display
// 03-04-2026 - KSuy          -- Implemented ToStringForOutputFile() for database persistence
//
// Original Source:
// (C) Copyright 1992-2017 by Deitel & Associates, Inc. and
// Pearson Education, Inc. All Rights Reserved.
// Fig. 12.4: Employee.cs
//
// Notes:
// This class is an ABSTRACT base class and cannot be instantiated directly.
// It provides common properties and behavior for all employee types.
//
// Derived classes include:
//
// SalariedEmployee
// HourlyEmployee
// CommissionEmployee
// BasePlusCommissionEmployee
//
// All employee types inherit:
//
// FirstName
// LastName
// SocialSecurityNumber
// EmailAddress
//
// The EmailAddress is used as the PRIMARY KEY in the EmpDB database
// when searching, updating, and deleting employee records.
//
// Because Employee implements IPayable, all employee objects can be
// stored inside:
//
//     List<IPayable>
//
// This allows payroll to process both employees and invoices
// using a single polymorphic loop.
//
/////////////////////////////////////////////////////////////////////////////////

namespace EmpDB
{
    // Abstract base class representing a generic employee
    public abstract class Employee : IPayable
    {
        // Basic employee identity fields
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }

        // Database primary key for employees
        // Read-only after creation to prevent accidental key changes
        public string EmailAddress { get; }

        // Constructor initializes common employee data
        public Employee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            EmailAddress = emailAddress;
        }

        // IPayable implementation
        // Payroll system uses this method to determine how much
        // should be paid to this employee.
        //
        // Each derived class implements Earnings() differently.
        public decimal GetPaymentAmount() => Earnings();

        // Console-friendly display of employee information
        public override string ToString()
        {
            string str = string.Empty;

            str += "**** Employee Record ************\n";
            str += $"First: {FirstName}\n";
            str += $"Last: {LastName}\n";
            str += $"Email: {EmailAddress}\n";
            str += $"SSN: {SocialSecurityNumber}\n";

            return str;
        }

        // Converts employee data into a raw file format
        // used for saving records back into the database file.
        //
        // Derived classes append their additional data fields.
        public virtual string ToStringForOutputFile()
        {
            string str = string.Empty;

            str += $"{FirstName}\n";
            str += $"{LastName}\n";
            str += $"{SocialSecurityNumber}\n";
            str += $"{EmailAddress}\n";

            return str;
        }

        // Abstract earnings calculation
        //
        // Each derived employee type implements this differently:
        //
        // SalariedEmployee to weekly salary
        // HourlyEmployee to hourly wage * hours
        // CommissionEmployee to gross sales * commission rate
        // BasePlusCommissionEmployee to base salary + commission
        public abstract decimal Earnings();
    }
}


