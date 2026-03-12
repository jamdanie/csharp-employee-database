/////////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 03-02-2026 - KSuy      -- Created program entry point for EmpDB application
// 03-02-2026 - KSuy      -- Instantiates DbApp object which manages database logic
// 03-02-2026 - KSuy      -- Starts main database loop through GoDatabase()
//
// Notes:
// Program.cs serves as the entry point for the EmpDB payroll system.
// The Main method initializes the application by creating a DbApp object,
// which contains all database functionality (CRUD operations, payroll,
// file loading/saving, and user interface).
//
// Only one DbApp instance exists during execution because it controls the
// entire database lifecycle.
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Optional debug mode hook
            // Used during development to run test routines instead of the database UI
            // if (Program._DEBUG_MODE_) TestMain();


            // Create the main database application controller
            // DbApp manages:
            // - File loading/saving
            // - Employee and Invoice records
            // - Payroll processing
            // - User interface menu system
            DbApp db = new DbApp();


            // Start the main application loop
            // This method continuously displays the menu and processes user commands
            db.GoDatabase();
        }
    }
}
