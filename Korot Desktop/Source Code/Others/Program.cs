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
            bool appStarted = false;
            try
            {
                if (!File.Exists(Properties.Settings.Default.LangFile)) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }
                if (!File.Exists(Application.StartupPath + "\\Lang\\English.lang"))
                {
                    Output.WriteLine(" [Korot] LANG_FATAL_ERROR: \"English.lang\" not found." + Environment.NewLine + " [Korot] Running Language Repair module.");
                    Application.Run(new frmTamirLang(args));
                    appStarted = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DownloadFolder))
                    {
                        Properties.Settings.Default.DownloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
                    }
                    if (args.Contains("-update"))
                    {
                        Application.Run(new Form1());
                        appStarted = true;
                        return;
                    }
                    else if (args.Contains("-oobe") || !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
                    {
                        Application.Run(new frmOOBE());
                        appStarted = true;
                        return;
                    }
                    else
                    {
                        frmMain testApp = new frmMain();
                        testApp.isIncognito = args.Contains("-incognito");
                        bool isIncognito = args.Contains("-incognito");
                        if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
                        foreach (string x in args)
                        {
                            if (x == Application.ExecutablePath || x == "-oobe" || x == "-update") { }
                            else if (x == "-incognito")
                            {
                                testApp.Tabs.Add(new TitleBarTab(testApp){Content = new frmCEF(testApp, true, "korot://incognito", Properties.Settings.Default.LastUser) { }});
                            }
                            else if (x == "-debug" && !isIncognito)
                            {
                                frmDebugSettings frmDebug = new frmDebugSettings();
                                frmDebug.Show();
                            }
                            else if (x.ToLower().EndsWith(".kef") || x.ToLower().EndsWith(".ktf"))
                            {
                                Application.Run(new frmInstallExt(x));
                                appStarted = true;
                                return;
                            }
                            else
                            {
                                testApp.CreateTab(x);
                            }
                        }
                        if (testApp.Tabs.Count < 1)
                        {
                            testApp.Tabs.Add(
    new TitleBarTab(testApp)
    {
        Content = new frmCEF(testApp, isIncognito, Properties.Settings.Default.StartupURL, Properties.Settings.Default.LastUser) { }
    });
                        }
                        testApp.SelectedTabIndex = 0;
                        TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
                        applicationContext.Start(testApp);
                        Application.Run(applicationContext);
                        appStarted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(" [Korot] FATAL_ERROR: " + ex.ToString());
                frmError form = new frmError(ex);
                if (!appStarted) { Application.Run(form); } else { form.Show(); }
            }
            }
        }
    }
