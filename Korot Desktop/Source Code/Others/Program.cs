using HaltroyTabs;
using System;
using System.IO;
using System.Linq;
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
            if (args.Contains("-oobe") || !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
            {
                Application.Run(new frmOOBE());
            }else 
            {
                frmMain testApp = new frmMain();
                bool isIncognito = args.Contains("-incognito");
                if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
                testApp.Tabs.Add(
                    new TitleBarTab(testApp)
                    {
                        Content = new frmCEF(testApp, isIncognito, Properties.Settings.Default.Homepage, Properties.Settings.Default.LastUser) { }
                    });
                foreach (string x in args)
                {
                    if (x == Application.ExecutablePath || x == "-incognito" || x == "-oobe"){}
                    else if (x.ToLower().EndsWith(".kef"))
                    {
                        Application.Run(new frmInstallExt(x));
                    }
                    else
                    {
                        testApp.CreateTab(x);
                    }
                }
                testApp.isIncognito = args.Contains("-incognito");
                testApp.SelectedTabIndex = 0;
                TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
                applicationContext.Start(testApp);
                Application.Run(applicationContext);
            }
        }
    }
}
