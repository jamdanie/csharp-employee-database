/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer ------ Description
//
// 03-02-2026 - KSuy         -- Created DbApp controller and initialized in-memory
//                              database using List<IPayable> to store employees
//                              and invoices in a single polymorphic collection.
//
// 03-02-2026 - KSuy         -- Implemented DisplayStartupBanner() and constructor
//                              logic to initialize the system and load the database
//                              from file on startup.
//
// 03-02-2026 - KSuy         -- Implemented ReadDataFromFile() to dynamically
//                              construct Employee and Invoice objects based on
//                              record type identifiers found in the database file.
//
// 03-02-2026 - KSuy         -- Implemented SaveDataToFile() to persist database
//                              records using each object's ToStringForOutputFile()
//                              method to maintain object-specific formatting.
//
// 03-04-2026 - KSuy         -- Implemented GoDatabase() main application loop
//                              with switch-based menu dispatcher for system actions.
//
// 03-04-2026 - KSuy         -- Implemented CreateNewPayable() dispatcher and
//                              creation methods:
//
//                                  CreateSalariedEmployee()
//                                  CreateHourlyEmployee()
//                                  CreateCommissionEmployee()
//                                  CreateBasePlusCommissionEmployee()
//                                  CreateInvoice()
//
//                              Creation methods gather user input, validate
//                              values, construct objects, and insert them into
//                              the database collection.
//
//
// 03-04-2026 - SBalamurugan -- Implemented FindRecord() dispatcher to allow
//                              searching by record type.
//
//                              Added:
//                                  FindEmployeeByEmail()
//                                  FindInvoiceByPartNumber()
//
//                              Email is used as the unique identifier for
//                              Employee records and PartNumber for Invoice records.
//
//
// 03-04-2026 - JDaniels     -- Implemented UpdateRecord() dispatcher and
//                              record modification methods:
//
//                                  UpdateEmployeeRecord()
//                                  UpdateInvoiceRecord()
//                                  UpdatePayInformation()
//
//                              UpdatePayInformation() uses runtime type checks
//                              to determine the correct payroll fields to modify
//                              for each Employee subclass.
//
//
// 03-04-2026 - SBalamurugan -- Implemented DeleteRecord() dispatcher with
//                              confirmation prompts to prevent accidental deletion.
//
//                              Added:
//                                  DeleteEmployeeRecord()
//                                  DeleteInvoiceRecord()
//
//
// 03-05-2026 - KSuy         -- Implemented PrintAllPayables() to display all
//                              records currently stored in the database.
//
//
// 03-05-2026 - JDaniels     -- Implemented RunPayroll() using IPayable
//                              polymorphism so employees and invoices can be
//                              processed in a single loop while calculating
//                              separate totals for payroll and vendor payments.
//
//
// 03-05-2026 - KSuy         -- Added input validation helper methods:
//
//                                  GetInput()
//                                  GetDecimalInput()
//                                  GetIntInput()
//
//                              These helpers standardize input validation and
//                              allow operations to be cancelled using 'Q'.
//
//
// 03-05-2026 - SBalamurugan -- Added record lookup helper methods:
//
//                                  FindEmployeeByEmail()
//                                  FindInvoiceByPartNumber()
//
//                              These helpers support search operations by using
//                              the primary identifiers for employee and invoice
//                              records.
//
//
// 03-05-2026 - KSuy         -- Added duplicate validation helpers:
//
//                                  EmployeeEmailExists()
//                                  InvoicePartExists()
//
//                              These enforce uniqueness of primary keys before
//                              allowing records to be inserted.
//
//
// 03-05-2026 - SBalamurugan -- Added delete confirmation helper logic to ensure
//                              records are not removed from the database without
//                              explicit user confirmation.
//
//
// 03-05-2026 - KSuy         -- Added database change tracking using
//                              hasUnsavedChanges flag so the system can warn
//                              users before exiting with unsaved modifications.
//
//
// 03-05-2026 - KSuy         -- Implemented safe exit handling:
//
//                                  ExitWithSavePrompt()
//                                  QuitWithWarning()
//
//                              Users are prompted to save changes before exiting
//                              if unsaved modifications exist.
//
//
// 03-05-2026 - KSuy         -- Improved console UI and navigation flow.
//
//                              Added helper utilities:
//                                  GetUserSelection()
//                                  Pause()
//                                  DisplayMainMenu()
//
//                              Menu now displays database status indicator
//                              showing whether unsaved changes are present.
//
//
// 03-05-2026 - KSuy         -- Modified ReadDataFromFile() to skip blank lines
//                              allowing readable spacing between records in
//                              the output file.
//
//
// 03-06-2026 - KSuy         -- Enhanced user interface navigation across CRUD
//                              operations so users remain inside the current
//                              operation (Create, Find, Update, Delete) until
//                              they explicitly choose to cancel or finish.
//
//
// 03-06-2026 - KSuy         -- Added consistent cancellation behavior using
//                              'Q' across all input prompts allowing users to
//                              cancel the current operation without returning
//                              unexpectedly to the main menu.
//
//
// 03-06-2026 - JDaniels     -- Improved UpdateEmployeeRecord() workflow by
//                              implementing an internal update loop that allows
//                              multiple fields to be modified during a single
//                              update session before exiting.
//
//
// 03-06-2026 - JDaniels     -- Added "Keep Current Value" behavior during update
//                              operations. Entering 'Q' during a field update now
//                              preserves the existing value instead of cancelling
//                              the entire update operation.
//
//
// 03-06-2026 - KSuy         -- Implemented duplicate validation for employee SSN
//                              values using EmployeeSSNExists() to enforce unique
//                              social security numbers across employee records.
//
//
// 03-06-2026 - KSuy         -- Refactored employee identity input logic into
//                              reusable helper method GetEmployeeIdentity() to
//                              centralize validation for name, SSN, and email.
//
//
// 03-06-2026 - JDaniels     -- Improved invoice update workflow to display the
//                              current invoice record before modifications and
//                              allow multiple update actions before exiting.
//
//
// 03-06-2026 - KSuy         -- Enhanced PrintAllPayables() output formatting by
//                              grouping records by type (Salaried, Hourly,
//                              Commission, BasePlusCommission, Invoice) to
//                              improve readability and reporting clarity.
//
//
// 03-06-2026 - KSuy         -- Improved SaveDataToFile() organization so records
//                              are written to the output file grouped by employee
//                              type followed by invoices for easier file review.
//
//
// 03-06-2026 - KSuy         -- Added additional validation messaging to prevent
//                              empty inputs and guide users when invalid data is
//                              entered during record operations.
//
// 03-09-2026 - KSuy         -- Updated RunPayroll() method to organize payroll output
//                              by record type (SalariedEmployee, HourlyEmployee,
//                              CommissionEmployee, BasePlusCommissionEmployee,
//                              and Invoice) to match the structured display used
//                              in PrintAllPayables() and improve report readability.
//
// 03-09-2026 - JDaniels      -- Updated startup initialization display to provide a
//                          detailed database summary after loading records.
//                          Added runtime counting of Employee and Invoice
//                          objects stored in the List<IPayable> collection.
//                          Startup banner now displays Employees Loaded,
//                          Invoices Loaded, and Total Records for improved
//                          visibility and debugging of database state.
//
// Note: Development tasks were completed collaboratively by the project team.
// Developer labels indicate primary responsibility for the feature area.
//
// Purpose:
// Main controller for the EmpDB payroll system. This class manages the
// in-memory database of payable objects (employees and invoices),
// handles user interaction through the console menu, and performs
// CRUD operations, payroll processing, and file persistence.
//
// Design:
// All records are stored in a List<IPayable> allowing polymorphic
// processing of both Employee objects and Invoice objects.
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;

namespace EmpDB
{
    internal class DbApp
    {
        // Central in-memory database.
        // Stores all payable objects (employees and invoices)
        // using the IPayable interface for polymorphic processing.
        private List<IPayable> payables = new List<IPayable>();

        private bool hasUnsavedChanges = false;

        private const string INPUT_FILE = "__EMP_INPUTFILE__.txt";
        private const string OUTPUT_FILE = "__EMP_OUTPUTFILE__.txt";

        // Displays the startup banner when the program launches.
        // Clears the console and shows the system title to provide
        // a clean interface before loading the database.
        private void DisplayStartupBanner()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("          EmpDB Payroll System");
            Console.WriteLine("========================================");
            Console.WriteLine();
        }

        // Constructor
        // Initializes the EmpDB application.
        // Displays startup banner, loads the database from either the saved
        // output file or the starter input file, and pauses before entering
        // the main application loop.
        public DbApp()
        {
            Console.WriteLine("Loading database...\n");

            if (File.Exists(OUTPUT_FILE))
            {
                Console.WriteLine("Source: Saved Output File\n");
                ReadDataFromFile(OUTPUT_FILE);
            }
            else
            {
                Console.WriteLine("Source: Starter Input File\n");
                ReadDataFromFile(INPUT_FILE);
            }

            // After loading the database we summarize the record types
            // so the user can immediately see how many employees and
            // invoices were successfully loaded into memory.

            // Because the database stores all records in a single
            // List<IPayable>, we must iterate through the collection
            // and determine the actual object type using runtime
            // type checking (Employee or Invoice).

            // Count employees and invoices
            int employeeCount = 0;
            int invoiceCount = 0;

            // Loop through the database and count each record type
            foreach (IPayable p in payables)
            {
                // If the object is an Employee (any subclass of Employee)
                // we increment the employee counter.
                if (p is Employee)
                    employeeCount++;

                // If the object is an Invoice we increment the invoice counter.
                else if (p is Invoice)
                    invoiceCount++;
            }

            // Display a clear startup summary so the user knows exactly
            // how many records were loaded into the database.
            Console.WriteLine($"Employees Loaded : {employeeCount}");
            Console.WriteLine($"Invoices Loaded  : {invoiceCount}");
            Console.WriteLine($"Total Records    : {payables.Count}\n");

            Console.WriteLine("Initialization complete.");
            Console.WriteLine("----------------------------------------");

            Pause();
        }

        // Main application loop.
        // Continuously displays the main menu and processes user selections
        // until the program exits. Each menu option routes to the appropriate
        // database operation such as CRUD actions, payroll processing, or saving.
        public void GoDatabase()
        {
            while (true)
            {
                DisplayMainMenu();

                char selection = GetUserSelection("CFUDPRSEQ");
                Console.WriteLine();

                switch (selection)
                {
                    case 'C':
                    case 'c':
                        CreateNewPayable();
                        break;

                    case 'F':
                    case 'f':
                        FindRecord();
                        break;

                    case 'U':
                    case 'u':
                        UpdateRecord();
                        break;

                    case 'D':
                    case 'd':
                        DeleteRecord();
                        break;

                    case 'P':
                    case 'p':
                        PrintAllPayables();
                        break;

                    case 'R':
                    case 'r':
                        RunPayroll();
                        break;

                    case 'S':
                    case 's':
                        SaveDataToFile();
                        break;

                    case 'E':
                    case 'e':
                        ExitWithSavePrompt();
                        break;

                    case 'Q':
                    case 'q':
                        QuitWithWarning();
                        break;

                    default:
                        Console.WriteLine($"ERROR: {selection} is not a valid choice.\n");
                        break;
                }
            }
        }


        // Reads database records from the specified file.
        // Each record begins with a record type which determines
        // which object should be constructed (Employee subtype or Invoice).
        // Records are loaded into the List<IPayable> database.
        private void ReadDataFromFile(string fileName)
        {
            payables.Clear();

            using (StreamReader inFile = new StreamReader(fileName))
            {
                string recordType;

                // The database file is structured so that each record begins with
                // a record type identifier. Based on this value we dynamically
                // construct the correct object type and load its fields.

                while ((recordType = inFile.ReadLine()) != null)
                {
                    recordType = recordType.Trim();

                    // Skip blank lines between records
                    if (string.IsNullOrWhiteSpace(recordType))
                        continue;

                    if (recordType == "SalariedEmployee")
                    {
                        string first = inFile.ReadLine();
                        string last = inFile.ReadLine();
                        string ssn = inFile.ReadLine();
                        string email = inFile.ReadLine();
                        decimal weeklySalary = decimal.Parse(inFile.ReadLine());

                        payables.Add(new SalariedEmployee(first, last, ssn, email, weeklySalary));
                    }
                    else if (recordType == "HourlyEmployee")
                    {
                        string first = inFile.ReadLine();
                        string last = inFile.ReadLine();
                        string ssn = inFile.ReadLine();
                        string email = inFile.ReadLine();
                        decimal wage = decimal.Parse(inFile.ReadLine());
                        decimal hours = decimal.Parse(inFile.ReadLine());

                        payables.Add(new HourlyEmployee(first, last, ssn, email, wage, hours));
                    }
                    else if (recordType == "CommissionEmployee")
                    {
                        string first = inFile.ReadLine();
                        string last = inFile.ReadLine();
                        string ssn = inFile.ReadLine();
                        string email = inFile.ReadLine();
                        decimal grossSales = decimal.Parse(inFile.ReadLine());
                        decimal rate = decimal.Parse(inFile.ReadLine());

                        payables.Add(new CommissionEmployee(first, last, ssn, email, grossSales, rate));
                    }
                    else if (recordType == "BasePlusCommissionEmployee")
                    {
                        string first = inFile.ReadLine();
                        string last = inFile.ReadLine();
                        string ssn = inFile.ReadLine();
                        string email = inFile.ReadLine();
                        decimal grossSales = decimal.Parse(inFile.ReadLine());
                        decimal rate = decimal.Parse(inFile.ReadLine());
                        decimal baseSalary = decimal.Parse(inFile.ReadLine());

                        payables.Add(new BasePlusCommissionEmployee(first, last, ssn, email, grossSales, rate, baseSalary));
                    }
                    else if (recordType == "Invoice")
                    {
                        string part = inFile.ReadLine();
                        string desc = inFile.ReadLine();
                        int qty = int.Parse(inFile.ReadLine());
                        decimal price = decimal.Parse(inFile.ReadLine());

                        payables.Add(new Invoice(part, desc, qty, price));
                    }
                    else
                    {
                        Console.WriteLine($"ERROR: Unknown record type '{recordType}'. Skipping...");
                    }
                }
            }
        }

        // Records are written to the output file grouped by record type.
        // This makes the database file easier for humans to read and review.
        // Each object formats its own data using ToStringForOutputFile().

        private void SaveDataToFile()
        {
            using (StreamWriter outFile = new StreamWriter(OUTPUT_FILE))
            {
                // Salaried Employees
                foreach (IPayable p in payables)
                {
                    if (p is SalariedEmployee se)
                    {
                        outFile.WriteLine(se.ToStringForOutputFile());
                        outFile.WriteLine();
                    }
                }

                // Hourly Employees
                foreach (IPayable p in payables)
                {
                    if (p is HourlyEmployee he)
                    {
                        outFile.WriteLine(he.ToStringForOutputFile());
                        outFile.WriteLine();
                    }
                }

                // Commission Employees
                foreach (IPayable p in payables)
                {
                    if (p is CommissionEmployee ce && !(p is BasePlusCommissionEmployee))
                    {
                        outFile.WriteLine(ce.ToStringForOutputFile());
                        outFile.WriteLine();
                    }
                }

                // Base Plus Commission Employees
                foreach (IPayable p in payables)
                {
                    if (p is BasePlusCommissionEmployee bce)
                    {
                        outFile.WriteLine(bce.ToStringForOutputFile());
                        outFile.WriteLine();
                    }
                }

                // Invoices
                foreach (IPayable p in payables)
                {
                    if (p is Invoice inv)
                    {
                        outFile.WriteLine(inv.ToStringForOutputFile());
                        outFile.WriteLine();
                    }
                }
            }

            hasUnsavedChanges = false;
            Console.WriteLine("\nDatabase saved successfully.\n");

            Pause();
        }

        // Helper method to read string input from the user.
        // Supports the ability to cancel the current operation
        // by entering 'Q'. Returns null if the user cancels.
        private string GetInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim();

                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nOperation cancelled.\n");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("ERROR: Input cannot be empty.");
                    continue;
                }

                return input;
            }
        }

        // Helper method for safely reading decimal numbers.
        // Validates that the input is a valid decimal value and
        // allows the user to cancel the operation with 'Q'.
        // Uses TryParse to prevent runtime exceptions that would occur
        // if invalid numeric input were entered.
        private decimal? GetDecimalInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim();

                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nOperation cancelled.\n");
                    return null;
                }

                if (decimal.TryParse(input, out decimal value) && value >= 0)
                    return value;

                Console.WriteLine("ERROR: Please enter a positive number or Q to cancel.");
            }
        }

        // Helper method for safely reading integer values.
        // Ensures the input is a valid whole number and
        // allows the user to cancel the operation with 'Q'.
        // Uses TryParse to prevent runtime exceptions that would occur
        // if invalid numeric input were entered.
        private int? GetIntInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim();

                if (input.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nOperation cancelled.\n");
                    return null;
                }

                if (int.TryParse(input, out int value) && value > 0)
                    return value;

                Console.WriteLine("ERROR: Please enter a positive whole number or Q to cancel.");
            }
        }

        // Checks if an employee email already exists in the database.
        // Used to enforce the email address as a unique primary key
        // for employee records.
        private bool EmployeeEmailExists(string email)
        {
            foreach (IPayable p in payables)
            {
                if (p is Employee emp)
                {
                    if (emp.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        // Checks if an employee SSN already exists in the database.
        private bool EmployeeSSNExists(string ssn)
        {
            foreach (IPayable p in payables)
            {
                if (p is Employee emp)
                {
                    if (emp.SocialSecurityNumber.Equals(ssn))
                        return true;
                }
            }

            return false;
        }

        // Checks if an invoice part number already exists in the database.
        // Ensures each invoice has a unique identifier.
        private bool InvoicePartExists(string partNumber)
        {
            foreach (IPayable p in payables)
            {
                if (p is Invoice inv)
                {
                    if (inv.PartNumber.Equals(partNumber, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        private bool IsValidName(string name)
        {
            foreach (char c in name)
            {
                if (!char.IsLetter(c))
                    return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }

        private bool IsValidSSN(string ssn)
        {
            if (ssn.Length != 11)
                return false;

            if (ssn[3] != '-' || ssn[6] != '-')
                return false;

            for (int i = 0; i < ssn.Length; i++)
            {
                if (i == 3 || i == 6) continue;

                if (!char.IsDigit(ssn[i]))
                    return false;
            }

            return true;
        }

        // This helper gathers all employee identity fields in one place.
        // It performs validation for name, SSN, and email before allowing
        // the employee object to be created. Returning a tuple keeps the
        // calling code clean and avoids repeating validation logic.
        private (string first, string last, string ssn, string email)? GetEmployeeIdentity()
        {
            string first;

            while (true)
            {
                first = GetInput("\nFirst name: (Q to cancel record type): ");
                if (first == null) return null;

                if (IsValidName(first))
                    break;

                Console.WriteLine("ERROR: Name must contain letters only.");
            }

            string last;

            while (true)
            {
                last = GetInput("Last name: (Q to cancel record type): ");
                if (last == null) return null;

                if (IsValidName(last))
                    break;

                Console.WriteLine("ERROR: Name must contain letters only.");
            }

            string ssn;

            while (true)
            {
                ssn = GetInput("SSN: (Q to cancel record type): ");
                if (ssn == null) return null;

                if (!IsValidSSN(ssn))
                {
                    Console.WriteLine("ERROR: SSN must be in format XXX-XX-XXXX.");
                    continue;
                }

                if (EmployeeSSNExists(ssn))
                {
                    Console.WriteLine($"ERROR: An employee with SSN '{ssn}' already exists.");
                    continue;
                }

                break;
            }

            string email;

            while (true)
            {
                email = GetInput("Email: (Q to cancel record type): ");
                if (email == null) return null;

                if (!IsValidEmail(email))
                {
                    Console.WriteLine("ERROR: Invalid email format.");
                    continue;
                }

                if (EmployeeEmailExists(email))
                {
                    Console.WriteLine($"ERROR: Employee with email '{email}' already exists.");
                    continue;
                }

                break;
            }

            return (first, last, ssn, email);
        }


        // Displays the create menu and allows the user to choose
        // which type of record to create. The selected option calls
        // the appropriate creation method.
        private void CreateNewPayable()
        {
            while (true)
            {
                Console.WriteLine("\nCreate New Record");
                Console.WriteLine("------------------");
                Console.WriteLine("[1] Salaried Employee");
                Console.WriteLine("[2] Hourly Employee");
                Console.WriteLine("[3] Commission Employee");
                Console.WriteLine("[4] Base + Commission Employee");
                Console.WriteLine("[5] Invoice");
                Console.WriteLine("[Q]uit to return to the Main Menu");

                Console.Write("\nSelect record type: ");

                char choice = GetUserSelection("12345Q");
                Console.WriteLine();

                switch (choice)
                {
                    case '1':
                        CreateSalariedEmployee();
                        break;

                    case '2':
                        CreateHourlyEmployee();
                        break;

                    case '3':
                        CreateCommissionEmployee();
                        break;

                    case '4':
                        CreateBasePlusCommissionEmployee();
                        break;

                    case '5':
                        CreateInvoice();
                        break;

                    case 'Q':
                    case 'q':
                        return;
                }
            }
        }

        // Creates a new SalariedEmployee record.
        // Collects employee identity information and weekly salary,
        // validates input, and adds the record to the database.
        private void CreateSalariedEmployee()
        {
            var identity = GetEmployeeIdentity();
            if (identity == null) return;

            decimal? salary = GetDecimalInput("Weekly salary: (Q to cancel record type): ");
            if (salary == null) return;

            SalariedEmployee emp = new SalariedEmployee(
                identity.Value.first,
                identity.Value.last,
                identity.Value.ssn,
                identity.Value.email,
                salary.Value
            );

            payables.Add(emp);
            hasUnsavedChanges = true;

            Console.WriteLine("\nEmployee successfully created:\n");
            Console.WriteLine(emp);
            Console.WriteLine($"\nTotal records in database: {payables.Count}\n");

            Pause();
        }

        // Creates a new HourlyEmployee record.
        // Prompts for wage and hours worked, validates inputs,
        // and adds the employee to the database.
        private void CreateHourlyEmployee()
        {
            var identity = GetEmployeeIdentity();
            if (identity == null) return;

            decimal? wage = GetDecimalInput("Hourly Wage: (Q to cancel record type): ");
            if (wage == null) return;

            decimal? hours = GetDecimalInput("Hours Worked: (Q to cancel record type): ");
            if (hours == null) return;

            HourlyEmployee emp = new HourlyEmployee(
                identity.Value.first,
                identity.Value.last,
                identity.Value.ssn,
                identity.Value.email,
                wage.Value,
                hours.Value
            );

            payables.Add(emp);
            hasUnsavedChanges = true;

            Console.WriteLine("\nEmployee successfully created:\n");
            Console.WriteLine(emp);
            Console.WriteLine($"\nTotal records in database: {payables.Count}\n");

            Pause();
        }

        // Creates a new CommissionEmployee record.
        // Collects gross sales and commission rate and stores
        // the employee in the database.
        private void CreateCommissionEmployee()
        {
            var identity = GetEmployeeIdentity();
            if (identity == null) return;

            decimal? sales = GetDecimalInput("Gross Sales: (Q to cancel record type): ");
            if (sales == null) return;

            decimal? rate = GetDecimalInput("Commission Rate: (Q to cancel record type): ");
            if (rate == null) return;

            CommissionEmployee emp = new CommissionEmployee(
                identity.Value.first,
                identity.Value.last,
                identity.Value.ssn,
                identity.Value.email,
                sales.Value,
                rate.Value
            );

            payables.Add(emp);
            hasUnsavedChanges = true;

            Console.WriteLine("\nEmployee successfully created:\n");
            Console.WriteLine(emp);
            Console.WriteLine($"\nTotal records in database: {payables.Count}\n");

            Pause();
        }

        // Creates a BasePlusCommissionEmployee record.
        // This employee receives a base salary plus commission
        // calculated from gross sales.
        private void CreateBasePlusCommissionEmployee()
        {
            var identity = GetEmployeeIdentity();
            if (identity == null) return;

            decimal? sales = GetDecimalInput("Gross Sales: (Q to cancel record type): ");
            if (sales == null) return;

            decimal? rate = GetDecimalInput("Commission Rate: (Q to cancel record type): ");
            if (rate == null) return;

            decimal? baseSalary = GetDecimalInput("Base Salary: (Q to cancel record type): ");
            if (baseSalary == null) return;

            BasePlusCommissionEmployee emp = new BasePlusCommissionEmployee(
                identity.Value.first,
                identity.Value.last,
                identity.Value.ssn,
                identity.Value.email,
                sales.Value,
                rate.Value,
                baseSalary.Value
            );

            payables.Add(emp);
            hasUnsavedChanges = true;

            Console.WriteLine("\nEmployee successfully created:\n");
            Console.WriteLine(emp);
            Console.WriteLine($"\nTotal records in database: {payables.Count}\n");

            Pause();
        }

        // Creates a new Invoice record.
        // Prompts for part number, description, quantity,
        // and price per item before adding the invoice to the database.
        private void CreateInvoice()
        {
            string part;
            while (true)
            {
                part = GetInput("\nPart number: or (Q to cancel record type): ");
                if (part == null) return;
                if (InvoicePartExists(part))
                {
                    Console.WriteLine($"\nERROR: An invoice with part number '{part}' already exists.\n");
                    continue;
                }
                break;
            }

            string desc = GetInput("Part description: or (Q to cancel record type): ");
            if (desc == null) return;

            int? qty = GetIntInput("Quantity: or (Q to cancel record type): ");
            if (qty == null) return;

            decimal? price = GetDecimalInput("Price per item: or (Q to cancel record type): ");
            if (price == null) return;

            Invoice inv = new Invoice(part, desc, qty.Value, price.Value);

            payables.Add(inv);

            hasUnsavedChanges = true;

            Console.WriteLine("\nInvoice successfully created:\n");
            Console.WriteLine(inv);
            Console.WriteLine($"\nTotal records in database: {payables.Count}\n");

            Pause();
        }

        // Displays the find menu allowing the user to search
        // for either an employee (by email) or an invoice
        // (by part number).
        private void FindRecord()
        {
            while (true)
            {

                Console.WriteLine("=====================================");
                Console.WriteLine("            Find Record");
                Console.WriteLine("=====================================\n");

                Console.WriteLine("\nChoose Record to Find");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("[E]mployee by Email");
                Console.WriteLine("[I]nvoice by Part Number");
                Console.WriteLine("[Q]uit to return to the main menu");

                Console.Write("\nSelect record type: ");

                char choice = GetUserSelection("EIQ");
                Console.WriteLine();

                switch (choice)
                {
                    case 'E':
                    case 'e':
                        FindEmployeeByEmail();
                        break;

                    case 'I':
                    case 'i':
                        FindInvoiceByPartNumber();
                        break;

                    case 'Q':
                    case 'q':
                        return;

                    default:
                        Console.WriteLine("\nERROR: Invalid selection.\n");
                        break;
                }
            }
        }

        // Searches the database for an employee using their email address.
        // Email acts as the primary key for employee records.
        private void FindEmployeeByEmail()
        {
            while (true)
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("            Find Employee");
                Console.WriteLine("====================================\n");

                Console.Write("\nEnter employee email address to search or (Q to cancel search): ");
                string email = Console.ReadLine().Trim();

                if (email.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nSearch cancelled.\n");
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("\nERROR: Email cannot be empty.\n");
                    continue;
                }

                foreach (IPayable p in payables)
                {
                    if (p is Employee emp)
                    {
                        if (emp.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nEmployee found:\n");
                            Console.WriteLine(emp);
                            Pause();
                            return;
                        }
                    }
                }

                Console.WriteLine($"\nERROR: No employee found with email '{email}'. Try again.\n");
            }
        }

        // Searches the database for an invoice using its part number.
        // Displays the invoice if found.
        private void FindInvoiceByPartNumber()
        {
            while (true)
            {
                Console.WriteLine("\n=====================================");
                Console.WriteLine("            Find Invoice");
                Console.WriteLine("=====================================\n");

                Console.Write("\nEnter invoice part number or (Q to cancel search): ");
                string part = Console.ReadLine().Trim();

                if (part.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nSearch cancelled.\n");
                    return;
                }

                if (string.IsNullOrWhiteSpace(part))
                {
                    Console.WriteLine("\nERROR: Part number cannot be empty.\n");
                    continue;
                }

                foreach (IPayable p in payables)
                {
                    if (p is Invoice inv)
                    {
                        if (inv.PartNumber.Equals(part, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nInvoice Found:\n");
                            Console.WriteLine(inv);
                            Pause();
                            return;
                        }
                    }
                }

                Console.WriteLine($"\nERROR: No invoice found with part number '{part}'. Try again.\n");
            }
        }

        // Displays the update menu allowing the user to choose
        // whether to update an employee or invoice record.
        private void UpdateRecord()
        {
            while (true)
            {

                Console.WriteLine("=====================================");
                Console.WriteLine("            Update Record");
                Console.WriteLine("=====================================\n");

                Console.WriteLine("\nChoose Record to Update");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("[E]mployee record");
                Console.WriteLine("[I]nvoice record");
                Console.WriteLine("[Q]uit to return to the main menu");

                Console.Write("\nSelect record type to update: ");

                char choice = GetUserSelection("EIQ");
                Console.WriteLine();

                switch (choice)
                {
                    case 'E':
                    case 'e':
                        UpdateEmployeeRecord();
                        break;

                    case 'I':
                    case 'i':
                        UpdateInvoiceRecord();
                        break;

                    case 'Q':
                    case 'q':
                        return;

                    default:
                        Console.WriteLine("\nERROR: Invalid update selection.\n");
                        break;
                }
            }
        }

        // Updates an employee record identified by email.
        // Allows modification of basic identity fields
        // or the employee's pay information.
        private void UpdateEmployeeRecord()
        {

            // Once the employee is located we enter an inner update loop.
            // This allows the user to modify multiple fields in a single
            // update session without having to search for the employee again.
            while (true)
            {
                Console.WriteLine("\n====================================");
                Console.WriteLine("            Update Employee");
                Console.WriteLine("====================================\n");

                Console.Write("\nEnter employee email address to update or (Q to cancel update of employee): ");
                string email = Console.ReadLine().Trim();

                if (email.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nUpdate cancelled.\n");
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("\nERROR: Email cannot be empty.\n");
                    continue;
                }

                foreach (IPayable p in payables)
                {
                    if (p is Employee emp &&
                        emp.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase))
                    {
                        while (true)
                        {
                            Console.WriteLine("\nEmployee Found:\n");
                            Console.WriteLine(emp);

                            Console.WriteLine("\nUpdate Employee Menu");
                            Console.WriteLine("--------------------");
                            Console.WriteLine("[1] First Name");
                            Console.WriteLine("[2] Last Name");
                            Console.WriteLine("[3] SSN");
                            Console.WriteLine("[4] Pay Information");
                            Console.WriteLine("[5] Done Updating");

                            Console.Write("\nSelect field to update: ");

                            char choice = GetUserSelection("12345");
                            Console.WriteLine();

                            switch (choice)
                            {
                                case '1':
                                    {
                                        string first;

                                        while (true)
                                        {
                                            first = GetInput("Enter new first name or (Q to keep current): ");

                                            if (first == null)
                                            {
                                                Console.WriteLine("Keeping current first name.");
                                                break;
                                            }

                                            if (IsValidName(first))
                                            {
                                                emp.FirstName = first;
                                                hasUnsavedChanges = true;
                                                Console.WriteLine("First name updated.");
                                                break;
                                            }

                                            Console.WriteLine("ERROR: Name must contain letters only.");
                                        }

                                        break;
                                    }

                                case '2':
                                    {
                                        string last;

                                        while (true)
                                        {
                                            last = GetInput("Enter new last name or (Q to keep current): ");

                                            if (last == null)
                                            {
                                                Console.WriteLine("Keeping current last name.");
                                                break;
                                            }

                                            if (IsValidName(last))
                                            {
                                                emp.LastName = last;
                                                hasUnsavedChanges = true;
                                                Console.WriteLine("Last name updated.");
                                                break;
                                            }

                                            Console.WriteLine("ERROR: Name must contain letters only.");
                                        }

                                        break;
                                    }

                                case '3':
                                    {
                                        string ssn;

                                        while (true)
                                        {
                                            ssn = GetInput("Enter new SSN or (Q to keep current): ");

                                            if (ssn == null)
                                            {
                                                Console.WriteLine("Keeping current SSN.");
                                                break;
                                            }

                                            if (!IsValidSSN(ssn))
                                            {
                                                Console.WriteLine("ERROR: SSN must be in format XXX-XX-XXXX.");
                                                continue;
                                            }

                                            if (EmployeeSSNExists(ssn) && ssn != emp.SocialSecurityNumber)
                                            {
                                                Console.WriteLine("ERROR: Another employee already has this SSN.");
                                                continue;
                                            }

                                            emp.SocialSecurityNumber = ssn;
                                            hasUnsavedChanges = true;

                                            Console.WriteLine("SSN updated.");
                                            break;
                                        }

                                        break;
                                    }

                                case '4':
                                    UpdatePayInformation(emp);
                                    hasUnsavedChanges = true;
                                    break;

                                case '5':
                                    Console.WriteLine("\nFinished updating employee.\n");
                                    Pause();
                                    return;

                                default:
                                    Console.WriteLine("\nERROR: Invalid selection.\n");
                                    break;
                            }
                        }
                    }
                }

                Console.WriteLine($"\nERROR: No employee found with email '{email}'. Try again.\n");
            }
        }

        // Updates an invoice record identified by its part number.
        // Allows modification of quantity or price per item.
        private void UpdateInvoiceRecord()
        {

            // Similar to employee updates, the invoice update process allows
            // multiple modifications during one session while repeatedly
            // displaying the current invoice data.
            while (true)
            {
                Console.WriteLine("\n=====================================");
                Console.WriteLine("            Update Invoice");
                Console.WriteLine("=====================================\n");

                Console.Write("Enter invoice part number to update (or Q to cancel update of invoice): ");
                string part = Console.ReadLine().Trim();

                if (part.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nUpdate cancelled.\n");
                    return;
                }

                if (string.IsNullOrWhiteSpace(part))
                {
                    Console.WriteLine("\nERROR: Part number cannot be empty.\n");
                    continue;
                }

                foreach (IPayable p in payables)
                {
                    if (p is Invoice inv &&
                        inv.PartNumber.Equals(part, StringComparison.OrdinalIgnoreCase))
                    {
                        while (true)
                        {
                            Console.WriteLine("\nInvoice Found:\n");
                            Console.WriteLine(inv);   // ← Shows the current invoice

                            Console.WriteLine("\nUpdate Invoice Menu");
                            Console.WriteLine("-------------------");
                            Console.WriteLine("[1] Quantity");
                            Console.WriteLine("[2] Price Per Item");
                            Console.WriteLine("[3] Done Updating");

                            Console.Write("\nSelect field to update: ");

                            char choice = GetUserSelection("123");
                            Console.WriteLine();

                            switch (choice)
                            {
                                case '1':
                                    {
                                        int? qty = GetIntInput("Enter new quantity or (Q to keep current): ");

                                        if (qty == null)
                                        {
                                            Console.WriteLine("Keeping current quantity.");
                                            break;
                                        }

                                        inv.Quantity = qty.Value;
                                        hasUnsavedChanges = true;

                                        Console.WriteLine("\nQuantity updated.\n");
                                        break;
                                    }

                                case '2':
                                    {
                                        decimal? price = GetDecimalInput("Enter new price per item or (Q to keep current): ");

                                        if (price == null)
                                        {
                                            Console.WriteLine("Keeping current price.");
                                            break;
                                        }

                                        inv.PricePerItem = price.Value;
                                        hasUnsavedChanges = true;

                                        Console.WriteLine("\nPrice updated.\n");
                                        break;
                                    }

                                case '3':
                                    {
                                        Console.WriteLine("\nFinished updating invoice.\n");
                                        Pause();
                                        return;
                                    }

                                default:
                                    Console.WriteLine("\nERROR: Invalid selection.\n");
                                    break;
                            }
                        }
                    }
                }

                Console.WriteLine($"\nERROR: No invoice found with part number '{part}'. Try again.\n");
            }
        }

        // Updates the pay information for an employee.
        // Uses runtime type checking to determine which
        // pay fields are applicable for each employee type.
        private void UpdatePayInformation(Employee emp)
        {
            if (emp is SalariedEmployee se)
            {
                decimal? salary = GetDecimalInput("Enter new weekly salary or (Q to keep current): ");

                if (salary == null)
                {
                    Console.WriteLine("Keeping current salary.");
                    return;
                }

                se.WeeklySalary = salary.Value;
                Console.WriteLine("Salary updated.");
            }

            else if (emp is HourlyEmployee he)
            {
                decimal? wage = GetDecimalInput("Enter new hourly wage or (Q to keep current): ");

                if (wage == null)
                {
                    Console.WriteLine("Keeping current wage.");
                    return;
                }

                decimal? hours = GetDecimalInput("Enter new hours worked or (Q to keep current): ");

                if (hours == null)
                {
                    Console.WriteLine("Keeping current hours.");
                    return;
                }

                he.Wage = wage.Value;
                he.Hours = hours.Value;

                Console.WriteLine("Hourly pay updated.");
            }

            else if (emp is CommissionEmployee ce)
            {
                decimal? sales = GetDecimalInput("Enter new gross sales or (Q to keep current): ");

                if (sales == null)
                {
                    Console.WriteLine("Keeping current sales.");
                    return;
                }

                decimal? rate = GetDecimalInput("Enter new commission rate or (Q to keep current): ");

                if (rate == null)
                {
                    Console.WriteLine("Keeping current commission rate.");
                    return;
                }

                ce.GrossSales = sales.Value;
                ce.CommissionRate = rate.Value;

                Console.WriteLine("Commission information updated.");
            }

            else if (emp is BasePlusCommissionEmployee bce)
            {
                decimal? sales = GetDecimalInput("Enter new gross sales or (Q to keep current): ");

                if (sales == null)
                {
                    Console.WriteLine("Keeping current sales.");
                    return;
                }

                decimal? rate = GetDecimalInput("Enter new commission rate or (Q to keep current): ");

                if (rate == null)
                {
                    Console.WriteLine("Keeping current commission rate.");
                    return;
                }

                decimal? baseSalary = GetDecimalInput("Enter new base salary or (Q to keep current): ");

                if (baseSalary == null)
                {
                    Console.WriteLine("Keeping current base salary.");
                    return;
                }

                bce.GrossSales = sales.Value;
                bce.CommissionRate = rate.Value;
                bce.BaseSalary = baseSalary.Value;

                Console.WriteLine("Base + commission pay updated.");
            }
        }

        // Displays the delete menu and allows the user to choose
        // whether to delete an employee or invoice record.
        private void DeleteRecord()
        {
            while (true)
            {

                Console.WriteLine("=====================================");
                Console.WriteLine("            Delete Record");
                Console.WriteLine("=====================================\n");

                Console.WriteLine("\nChoose Type of Record to Delete");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("[E]mployee by Email");
                Console.WriteLine("[I]nvoice by Part Number");
                Console.WriteLine("[Q]uit to return to the main menu");

                Console.Write("\nSelect record type: ");

                char choice = GetUserSelection("EIQ");
                Console.WriteLine();

                switch (choice)
                {
                    case 'E':
                    case 'e':
                        DeleteEmployeeRecord();
                        break;

                    case 'I':
                    case 'i':
                        DeleteInvoiceRecord();
                        break;

                    case 'Q':
                    case 'q':
                        return;

                    default:
                        Console.WriteLine("\nERROR: Invalid selection.\n");
                        break;
                }
            }
        }

        // Deletes an employee record identified by email.
        // The user must confirm the deletion before the
        // record is removed from the database.
        private void DeleteEmployeeRecord()
        {
            while (true)
            {
                Console.Write("\nEnter employee email address to delete or (Q to cancel deletion type): ");
                string email = Console.ReadLine().Trim();

                if (email.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nDelete cancelled.\n");
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("\nERROR: Email cannot be empty.\n");
                    continue;
                }

                for (int i = 0; i < payables.Count; i++)
                {
                    if (payables[i] is Employee emp)
                    {
                        if (emp.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nEmployee found:\n");
                            Console.WriteLine(emp);

                            Console.Write("\nDelete this employee? (Y/N): ");
                            char confirm = GetUserSelection("YN");
                            Console.WriteLine();

                            if (confirm == 'Y' || confirm == 'y')
                            {
                                payables.RemoveAt(i);
                                hasUnsavedChanges = true;
                                Console.WriteLine("\nEmployee deleted.\n");
                                Console.WriteLine($"Total records in database: {payables.Count}\n");
                            }
                            else
                            {
                                Console.WriteLine("\nDelete cancelled.\n");
                            }

                            Pause();
                            return;
                        }
                    }
                }

                Console.WriteLine($"\nERROR: No employee found with email '{email}'. Try again.\n");
            }
        }

        // Deletes an invoice record identified by its part number.
        // Requires user confirmation before removal.
        private void DeleteInvoiceRecord()
        {
            while (true)
            {
                Console.Write("\nEnter invoice part number to delete or (Q to cancel deletion type): ");
                string part = Console.ReadLine().Trim();

                if (part.Equals("Q", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("\nDelete cancelled.\n");
                    return;
                }

                if (string.IsNullOrWhiteSpace(part))
                {
                    Console.WriteLine("\nERROR: Part number cannot be empty.\n");
                    continue;
                }

                for (int i = 0; i < payables.Count; i++)
                {
                    if (payables[i] is Invoice inv)
                    {
                        if (inv.PartNumber.Equals(part, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("\nInvoice found:\n");
                            Console.WriteLine(inv);

                            Console.Write("\nDelete this invoice? (Y/N): ");
                            char confirm = GetUserSelection("YN");
                            Console.WriteLine();

                            if (confirm == 'Y' || confirm == 'y')
                            {
                                payables.RemoveAt(i);
                                hasUnsavedChanges = true;
                                Console.WriteLine("\nInvoice deleted.\n");
                                Console.WriteLine($"Total records in database: {payables.Count}\n");
                            }
                            else
                            {
                                Console.WriteLine("\nDelete cancelled.\n");
                            }

                            Pause();
                            return;
                        }
                    }
                }

                Console.WriteLine($"\nERROR: No invoice found with part number '{part}'. Try again.\n");
            }
        }

        // Displays all records currently stored in the database.
        // This includes all employee types and invoices.
        // Records are grouped by type to make the output easier
        // to read for users reviewing the database contents.
        private void PrintAllPayables()
        {
            Console.WriteLine("\n=========== DATABASE RECORDS ===========\n");

            if (payables.Count == 0)
            {
                Console.WriteLine("Database is empty.\n");
                Pause();
                return;
            }

            // Salaried Employees
            Console.WriteLine("===== SALARIED EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is SalariedEmployee)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }

            // Hourly Employees
            Console.WriteLine("===== HOURLY EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is HourlyEmployee)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }

            // Commission Employees
            Console.WriteLine("===== COMMISSION EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is CommissionEmployee && !(p is BasePlusCommissionEmployee))
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }

            // Base + Commission Employees
            Console.WriteLine("===== BASE + COMMISSION EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is BasePlusCommissionEmployee)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }

            // Invoices
            Console.WriteLine("===== INVOICES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is Invoice)
                {
                    Console.WriteLine(p);
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Total records: {payables.Count}\n");

            Pause();
        }

        // Processes payroll for all payable objects.
        // Uses polymorphism through the IPayable interface so both
        // employees and invoices can be processed in a single loop.
        // Records are grouped by type to make the output easier
        // to read for users reviewing the database payroll contents.
        private void RunPayroll()
        {
            Console.WriteLine("\n================ PAYROLL REPORT ================\n");

            decimal employeeTotal = 0m;
            decimal invoiceTotal = 0m;
            int count = 1;

            // Salaried Employees
            Console.WriteLine("===== SALARIED EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is SalariedEmployee)
                {
                    Console.WriteLine($"Record #{count++}");

                    decimal payment = p.GetPaymentAmount();
                    employeeTotal += payment;

                    Console.WriteLine(p);
                    Console.WriteLine($"Payment Due: {payment:C}");
                    Console.WriteLine("----------------------------------------------\n");
                }
            }

            // Hourly Employees
            Console.WriteLine("===== HOURLY EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is HourlyEmployee)
                {
                    Console.WriteLine($"Record #{count++}");

                    decimal payment = p.GetPaymentAmount();
                    employeeTotal += payment;

                    Console.WriteLine(p);
                    Console.WriteLine($"Payment Due: {payment:C}");
                    Console.WriteLine("----------------------------------------------\n");
                }
            }

            // Commission Employees
            Console.WriteLine("===== COMMISSION EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is CommissionEmployee && !(p is BasePlusCommissionEmployee))
                {
                    Console.WriteLine($"Record #{count++}");

                    decimal payment = p.GetPaymentAmount();
                    employeeTotal += payment;

                    Console.WriteLine(p);
                    Console.WriteLine($"Payment Due: {payment:C}");
                    Console.WriteLine("----------------------------------------------\n");
                }
            }

            // Base + Commission Employees
            Console.WriteLine("===== BASE + COMMISSION EMPLOYEES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is BasePlusCommissionEmployee)
                {
                    Console.WriteLine($"Record #{count++}");

                    decimal payment = p.GetPaymentAmount();
                    employeeTotal += payment;

                    Console.WriteLine(p);
                    Console.WriteLine($"Payment Due: {payment:C}");
                    Console.WriteLine("----------------------------------------------\n");
                }
            }

            // Invoices
            Console.WriteLine("===== INVOICES =====\n");
            foreach (IPayable p in payables)
            {
                if (p is Invoice)
                {
                    Console.WriteLine($"Record #{count++}");

                    decimal payment = p.GetPaymentAmount();
                    invoiceTotal += payment;

                    Console.WriteLine(p);
                    Console.WriteLine($"Payment Due: {payment:C}");
                    Console.WriteLine("----------------------------------------------\n");
                }
            }

            Console.WriteLine("=============== PAYROLL SUMMARY TOTAL DUE OUT ===============");
            Console.WriteLine($"Employee Payments: {employeeTotal,15:C}");
            Console.WriteLine($"Invoice Payments : {invoiceTotal,15:C}");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"GRAND TOTAL      : {(employeeTotal + invoiceTotal),15:C}");
            Console.WriteLine("=============================================================\n");

            Pause();
        }


        // EXIT / QUIT behavior 

        // Exits the application with an option to save changes.
        // If unsaved changes exist, the user is prompted to save
        // before the program terminates.
        private void ExitWithSavePrompt()
        {
            Console.Write("\nAre you sure you want to exit the application? (Y/N): ");
            char confirm = GetUserSelection("YN");
            Console.WriteLine();

            if (confirm != 'Y' && confirm != 'y')
            {
                Console.WriteLine("\nExit cancelled.\n");
                Pause();
                return;
            }

            if (hasUnsavedChanges)
            {
                Console.Write("\nYou have unsaved changes. Save before exit? (Y/N): ");
                char save = GetUserSelection("YN");
                Console.WriteLine();

                if (save == 'Y' || save == 'y')
                {
                    SaveDataToFile();
                }
                else
                {
                    Console.WriteLine("\nExiting without saving. Changes will be lost.");
                }
            }

            Environment.Exit(0);
        }

        // Allows the user to quit the program immediately.
        // If unsaved changes exist, a warning message is displayed
        // before exiting.
        private void QuitWithWarning()
        {
            if (hasUnsavedChanges)
                Console.Write("\nQuit without saving? Unsaved changes will be LOST. (Y/N): ");
            else
                Console.Write("\nQuit the application? (Y/N): ");

            char c = GetUserSelection("YN");
            Console.WriteLine();

            if (c == 'Y' || c == 'y')
                Environment.Exit(0);

            Console.WriteLine("Quit cancelled.\n");

            Pause();
        }

        // UI helpers

        // Reads a single keypress and validates it against the
        // allowed option characters passed to the method.
        private char GetUserSelection(string validOptions)
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                char selection = key.KeyChar;

                if (validOptions.IndexOf(selection.ToString(), StringComparison.OrdinalIgnoreCase) >= 0)
                    return selection;

                Console.WriteLine("\nERROR: Invalid selection. Please choose one of the menu options.\n");
                Console.Write("Selection: ");
            }
        }

        // Pauses program execution until the user presses a key.
        // Allows the user time to read output before returning
        // to the main menu.
        private void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        // Displays the main EmpDB menu and current database status.
        // Provides access to all database operations.
        private void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n*************************************************");
            Console.WriteLine("******** Emp Database Main Menu *****************");
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

            if (hasUnsavedChanges)
                Console.WriteLine("         STATUS: * Unsaved Changes Present *"); // Indicates that there are unsaved changes in the database
            else
                Console.WriteLine("         STATUS: Database Saved"); // Indicates that there are no unsaved changes and the database is currently saved

            Console.WriteLine($"         Records in Database: {payables.Count}"); 

            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("[C]reate a new record (Employee/Invoice)");
            Console.WriteLine("[F]ind a record (Employee by email / Invoice by part number)");
            Console.WriteLine("[U]pdate a record (Employee / Invoice)");
            Console.WriteLine("[D]elete a record (Employee / Invoice)");
            Console.WriteLine("[P]rint all records");
            Console.WriteLine("[R]un payroll (pay employees + invoices)");
            Console.WriteLine("[S]ave all changes and continue");
            Console.WriteLine("[E]xit saving changes");
            Console.WriteLine("[Q]uit discard changes (with warning)");
            Console.WriteLine("************************************************");
            Console.Write("Enter Selection: ");
        }
    }
}