using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ComputerCare.Login;
using ComputerCare.Masters;
using ComputerCare.Operations;
using ComputerCare.Reports;

namespace ComputerCare
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmReceipt());
        }
    }
}
