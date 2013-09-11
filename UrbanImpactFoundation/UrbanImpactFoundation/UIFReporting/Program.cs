using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UIFReporting
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmReport1());
        }
    }
}
