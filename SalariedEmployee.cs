/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description

// 03-02-2026 - KSuy          -- Integrated SalariedEmployee into EmpDB payroll system
// 03-02-2026 - JDaniels      -- Implemented Earnings() override for salary calculation
// 03-02-2026 - SBalamurugan  -- Linked class inheritance to Employee base class
// 03-04-2026 - KSuy          -- Updated constructor to include EmailAddress primary key
// 03-04-2026 - JDaniels      -- Added formatted ToString() display for EmpDB records
// 03-04-2026 - SBalamurugan  -- Implemented ToStringForOutputFile() for database persistence
//
// Original Source:
// (C) Copyright 1992-2017 by Deitel & Associates, Inc.
// Pearson Education, Inc.
// Fig. 12.5: SalariedEmployee.cs
//
// Notes:
// This class represents employees who earn a fixed weekly salary.
// It inherits common employee properties from the Employee base class:
//
// FirstName
// LastName
// SocialSecurityNumber
// EmailAddress
//
// Payroll calculation:
// Earnings() simply returns the WeeklySalary value.
//
// Because Employee implements IPayable, this class automatically
// participates in the polymorphic payroll system.
//
/////////////////////////////////////////////////////////////////////////////////

namespace EmpDB
{
    using System;

    // Represents a salaried employee paid a fixed weekly salary
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;

        // Constructor initializes employee information and salary
        public SalariedEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress, decimal weeklySalary)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            WeeklySalary = weeklySalary; // validated through property
        }

        // Property that gets and sets the employee's weekly salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                // Validation prevents negative salary values
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(WeeklySalary)} must be >= 0");
                }

                weeklySalary = value;
            }
        }

        // Calculates earnings for a salaried employee
        // Overrides the abstract Earnings() method from Employee
        public override decimal Earnings() => WeeklySalary;

        // Console display formatting for employee record
        public override string ToString()
        {
            string str = string.Empty;

            str += base.ToString();
            str += $"Employee Type: Salaried\n";
            str += $"Weekly Salary: {WeeklySalary:C}\n";

            return str;
        }

        // Converts object to file format used for saving the database
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";     // SalariedEmployee
            str += base.ToStringForOutputFile();         // first/last/ssn/email
            str += $"{WeeklySalary:F2}";

            return str;
        }
    }
}


