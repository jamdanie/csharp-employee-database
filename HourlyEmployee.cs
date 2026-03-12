/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------- Description
// 03-02-2026 - KSuy          -- Integrated HourlyEmployee into EmpDB payroll system
// 03-02-2026 - JDaniels      -- Implemented overtime earnings calculation
// 03-02-2026 - SBalamurugan  -- Connected class to Employee inheritance hierarchy
// 03-04-2026 - KSuy          -- Updated constructor to include EmailAddress primary key
// 03-04-2026 - JDaniels      -- Added formatted ToString() display for EmpDB records
// 03-04-2026 - SBalamurugan  -- Implemented ToStringForOutputFile() for database persistence
//
// Original Source:
// (C) Copyright 1992-2017 by Deitel & Associates, Inc.
// Pearson Education, Inc.
// Fig. 12.6: HourlyEmployee.cs
//
// Notes:
// This class represents employees paid based on hourly wage.
//
// Payroll rules implemented:
//
// Up to 40 hours → normal wage
// Over 40 hours → overtime (1.5 × wage)
//
// Validation rules:
//
// Wage cannot be negative
// Hours must be between 0 and 168 (hours in a week)
//
// Inheritance:
//
// HourlyEmployee > Employee > IPayable
//
// Because Employee implements IPayable, hourly employees automatically
// participate in the polymorphic payroll system.
//
/////////////////////////////////////////////////////////////////////////////////

namespace EmpDB
{
    using System;

    // Represents an hourly employee
    public class HourlyEmployee : Employee
    {
        private decimal wage;   // hourly pay rate
        private decimal hours;  // hours worked in the week

        // Constructor initializes employee information and hourly data
        public HourlyEmployee(string firstName, string lastName,
           string socialSecurityNumber, string emailAddress,
           decimal hourlyWage, decimal hoursWorked)
           : base(firstName, lastName, socialSecurityNumber, emailAddress)
        {
            Wage = hourlyWage;     // validated via property
            Hours = hoursWorked;   // validated via property
        }

        // Hourly wage property
        public decimal Wage
        {
            get
            {
                return wage;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Wage)} must be >= 0");
                }

                wage = value;
            }
        }

        // Hours worked property
        public decimal Hours
        {
            get
            {
                return hours;
            }
            set
            {
                // Prevent impossible values
                if (value < 0 || value > 168)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Hours)} must be >= 0 and <= 168");
                }

                hours = value;
            }
        }

        // Earnings calculation with overtime
        public override decimal Earnings()
        {
            if (Hours <= 40)   // normal work hours
            {
                return Wage * Hours;
            }
            else               // overtime calculation
            {
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
            }
        }

        // Console display formatting for employee record
        public override string ToString()
        {
            string str = string.Empty;

            str += base.ToString();
            str += $"Employee Type: Hourly\n";
            str += $"Hourly Wage: {Wage:C}\n";
            str += $"Hours Worked: {Hours:F2}\n";

            return str;
        }

        // Converts object to file format for saving database
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";   // HourlyEmployee
            str += base.ToStringForOutputFile();
            str += $"{Wage:F2}\n";
            str += $"{Hours:F2}";

            return str;
        }
    }
}


