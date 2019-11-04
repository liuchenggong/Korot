using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmMain testform = null;
            frmSettings formsetting = new frmSettings(testform);
            testform = new frmMain(args, formsetting);
            formsetting.Visible = false;
            Application.Run(testform);
        }

      
    }
}
