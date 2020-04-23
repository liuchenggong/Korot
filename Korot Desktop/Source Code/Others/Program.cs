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
using CefSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Korot
{
    internal static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            bool isPreRelease = true;
            int preVer = 4;
            Cef.EnableHighDPISupport();
            CollectionManager colman = new CollectionManager();
            Properties.Settings.Default.dismissUpdate = false;
            Properties.Settings.Default.alreadyUpdatedThemes = false;
            Properties.Settings.Default.alreadyUpdatedExt = false;
            Properties.Settings.Default.disableLangErrors = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool appStarted = false;
            List<frmNotification> notifications = new List<frmNotification>();
            try
            {
                if (!File.Exists(Properties.Settings.Default.LangFile)) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }
                if (!File.Exists(Application.StartupPath + "\\Lang\\English.lang"))
                {
                    Tools.FixDefaultLanguage();
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DownloadFolder))
                    {
                        Properties.Settings.Default.DownloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
                    }
                    if (args.Contains("-update"))
                    {
                        if (UACControl.IsProcessElevated)
                        {
                            Application.Run(new Form1() { isPreRelease = isPreRelease, preVer = preVer,});
                            appStarted = true;
                        }
                        else
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo(Application.ExecutablePath)
                            {
                                Verb = "runas",
                                Arguments = "-update"
                            };
                            Process.Start(startInfo);
                            Application.Exit();
                        }
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
                        frmMain testApp = new frmMain
                        {
                            notifications = notifications,
                            isPreRelease = isPreRelease, preVer = preVer,
                            isIncognito = args.Contains("-incognito")
                        };
                        bool isIncognito = args.Contains("-incognito");
                        if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
                        foreach (string x in args)
                        {
                            if (x == Application.ExecutablePath || x == "-oobe" || x == "-update") { }
                            else if (x == "-incognito")
                            {
                                testApp.Tabs.Add(new TitleBarTab(testApp) { Content = new frmCEF(true, "korot://incognito", Properties.Settings.Default.LastUser) { } });
                            }
                            else if (x == "-debug" && !isIncognito)
                            {
                                frmDebugSettings frmDebug = new frmDebugSettings();
                                frmDebug.Show();
                            }
                            else if (x.ToLower().EndsWith(".kef"))
                            {
                                if (Properties.Settings.Default.allowUnknownResources)
                                {
                                    Application.Run(new frmInstallExt(x));
                                    appStarted = true;
                                }
                                else
                                {
                                    frmError form = new frmError(new InvalidOperationException(x + " could not be installed because \"Allow Unknown Resources\" was disabled. You can enable it from the settings page. Enabling this may be dangerous and Haltroy does not going to take responsibility for this."));
                                    Application.Run(form);
                                    appStarted = true;
                                }
                                return;
                            }
                            else if (x.ToLower().EndsWith(".ktf"))
                            {
                                Application.Run(new frmInstallExt(x));
                                appStarted = true;
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
        Content = new frmCEF(isIncognito, Properties.Settings.Default.StartupURL, Properties.Settings.Default.LastUser) { isPreRelease = isPreRelease, preVer = preVer, colManager = colman, }
    });
                        }
                        testApp.SelectedTabIndex = 0;
                        testApp.colman = colman;
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
