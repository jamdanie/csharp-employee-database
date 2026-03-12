/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description
// 03-02-2026 - KSuy          -- Integrated CommissionEmployee into EmpDB payroll system
// 03-02-2026 - JDaniels      -- Implemented commission-based Earnings() calculation
// 03-04-2026 - SBalamurugan  -- Connected class to Employee inheritance hierarchy
// 03-04-2026 - KSuy          -- Updated constructor to include EmailAddress primary key
// 03-04-2026 - JDaniels      -- Added formatted ToString() display for EmpDB records
// 03-04-2026 - SBalamurugan  -- Implemented ToStringForOutputFile() for database persistence
//
// Original Source:
// (C) Copyright 1992-2017 by Deitel & Associates, Inc.
// Pearson Education, Inc.
// Fig. 12.7: CommissionEmployee.cs
//
// Notes:
// This class represents employees whose earnings are calculated
// as a percentage of their gross weekly sales.
//
// Earnings Formula:
//
// Earnings = GrossSales × CommissionRate
//
// Validation Rules:
//
// GrossSales must be >= 0
// CommissionRate must be between 0 and 1
//
// Inheritance Structure:
//
// CommissionEmployee → Employee → IPayable
//
// This class also serves as the base class for:
//
// BasePlusCommissionEmployee
//
// which adds an additional base salary on top of commission.
//
/////////////////////////////////////////////////////////////////////////////////

namespace EmpDB
{
    using System;

    // Represents an employee paid by commission
    public class CommissionEmployee : Employee
    {
        private decimal grossSales;       // gross weekly sales
        private decimal commissionRate;   // commission percentage

        // Constructor initializes employee and commission information
        public CommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress,
           decimal grossSales, decimal commissionRate)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            GrossSales = grossSales;           // validated through property
            CommissionRate = commissionRate;   // validated through property
        }

        // Gross sales property
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(GrossSales)} must be >= 0");
                }

                grossSales = value;
            }
        }

        // Commission rate property
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            }
            set
            {
                if (value <= 0 || value >= 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(CommissionRate)} must be > 0 and < 1");
                }

                commissionRate = value;
            }
        }

        // Calculates commission earnings
        public override decimal Earnings() => CommissionRate * GrossSales;

        // Console display formatting for employee record
        public override string ToString()
        {
            string str = string.Empty;

            str += base.ToString();
            str += $"Employee Type: Commission\n";
            str += $"Gross Sales: {GrossSales:C}\n";
            str += $"Commission Rate: {CommissionRate:F2}\n";

            return str;
        }

        // Converts object to database file format
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";  // CommissionEmployee
            str += base.ToStringForOutputFile();
            str += $"{GrossSales:F2}\n";
            str += $"{CommissionRate:F2}";

            return str;
        }
    }
}

