//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
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
            if (!File.Exists(Properties.Settings.Default.LangFile)) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }
            if (!File.Exists(Application.StartupPath + "\\Lang\\English.lang"))
            {
                Application.Run(new frmTamir());
            }
            else
            {
                if (args.Contains("-update"))
                {
                    Application.Run(new Form1());
                }
                else if (args.Contains("-oobe") || !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
                {
                    Application.Run(new frmOOBE());
                }
                else
                {
                    frmMain testApp = new frmMain();
                    testApp.isIncognito = args.Contains("-incognito");
                    bool isIncognito = args.Contains("-incognito");
                    if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
                    testApp.Tabs.Add(
                        new TitleBarTab(testApp)
                        {
                            Content = new frmCEF(testApp, isIncognito, Properties.Settings.Default.Homepage, Properties.Settings.Default.LastUser) { }
                        });
                    foreach (string x in args)
                    {
                        if (x == Application.ExecutablePath || x == "-oobe") { }
                        else if (x == "-incognito")
                        {
                            testApp.CreateTab("korot://incognito");
                        }
                        else if (x.ToLower().EndsWith(".kef"))
                        {
                            Application.Run(new frmInstallExt(x));
                        }
                        else
                        {
                            testApp.CreateTab(x);
                        }
                    }
                    testApp.SelectedTabIndex = 0;
                    TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
                    applicationContext.Start(testApp);
                    Application.Run(applicationContext);
                }
            }
        }
    }
}
