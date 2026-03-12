/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description

// 03-02-2026 - KSuy          -- Integrated BasePlusCommissionEmployee into EmpDB payroll system
// 03-02-2026 - JDaniels      -- Implemented multi-level inheritance from CommissionEmployee
// 03-02-2026 - SBalamurugan  -- Implemented Earnings() calculation using base commission + base salary

// 03-04-2026 - KSuy          -- Updated constructor to include EmailAddress primary key
// 03-04-2026 - JDaniels      -- Implemented custom ToString() to prevent duplicate commission output
// 03-04-2026 - SBalamurugan  -- Implemented custom ToStringForOutputFile() to prevent duplicate file records
//
// Original Source:
// (C) Copyright 1992-2017 by Deitel & Associates, Inc.
// Pearson Education, Inc.
// Fig. 12.8: BasePlusCommissionEmployee.cs
//
// Notes:
// This class demonstrates MULTI-LEVEL INHERITANCE:
//
// BasePlusCommissionEmployee → CommissionEmployee → Employee → IPayable
//
// It extends the commission-based employee model by adding a guaranteed
// base salary on top of commission earnings.
//
// Earnings Formula:
//
// Earnings = BaseSalary + (GrossSales × CommissionRate)
//
// Important Implementation Note:
//
// CommissionEmployee already implements ToString() and
// ToStringForOutputFile(). Calling base.ToString() would cause the
// commission employee output to print first, which would duplicate
// commission information when adding BaseSalary.
//
// To prevent duplicate output, the formatting here manually reconstructs
// the Employee base information instead of calling base.ToString().
//
/////////////////////////////////////////////////////////////////////////////////

namespace EmpDB
{
    using System;

    // Represents a commission employee with an additional base salary
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // fixed weekly base salary

        // Constructor initializes employee, commission data, and base salary
        public BasePlusCommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress,
           decimal grossSales, decimal commissionRate, decimal baseSalary)
           : base(firstName, lastName, socialSecurityNumber, emailAddress,
                grossSales, commissionRate)
        {
            BaseSalary = baseSalary; // validated via property
        }

        // Property for base salary
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(BaseSalary)} must be >= 0");
                }

                baseSalary = value;
            }
        }

        // Calculates total earnings for the employee
        public override decimal Earnings() => BaseSalary + base.Earnings();

        // Console display formatting
        // Manual formatting avoids duplicate commission information
        public override string ToString()
        {
            string str = string.Empty;

            str += $"**** Employee Record ************\n";
            str += $"First: {FirstName}\n";
            str += $"Last: {LastName}\n";
            str += $"Email: {EmailAddress}\n";
            str += $"SSN: {SocialSecurityNumber}\n";

            str += $"Employee Type: Base + Commission\n";
            str += $"Gross Sales: {GrossSales:C}\n";
            str += $"Commission Rate: {CommissionRate:F2}\n";
            str += $"Base Salary: {BaseSalary:C}\n";

            return str;
        }

        // File persistence formatting
        // Manual formatting prevents duplicated commission records
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";   // BasePlusCommissionEmployee
            str += $"{FirstName}\n";
            str += $"{LastName}\n";
            str += $"{SocialSecurityNumber}\n";
            str += $"{EmailAddress}\n";
            str += $"{GrossSales:F2}\n";
            str += $"{CommissionRate:F2}\n";
            str += $"{BaseSalary:F2}";

            return str;
        }
    }
}



