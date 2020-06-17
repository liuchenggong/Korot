﻿//MIT License
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
using HTAlt.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public static class VersionInfo
    {
        public static string CodeName => "Laika";
        public static bool IsPreRelease => true;
        public static int PreReleaseNumber => 2;
    }
    internal static class Program
    {
        public static string getOSInfo()
        {
            string fullName = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            //We only need the version number or name like 7,Vista,10
            //Remove any other unnecesary thing.
            fullName = fullName.Replace("Microsoft Windows", "")
                .Replace(" (PRODUCT) RED", "")
                .Replace(" Business", "")
                .Replace(" Education", "")
                .Replace(" Embedded", "")
                .Replace(" Enterprise LTSC", "")
                .Replace(" Enterprise", "")
                .Replace(" Home Basic", "")
                .Replace(" Home Premium", "")
                .Replace(" Home", "")
                .Replace(" Insider", "")
                .Replace(" IoT Core", "")
                .Replace(" IoT", "")
                .Replace(" KN", "")
                .Replace(" Media Center 2002", "")
                .Replace(" Media Center 2004", "")
                .Replace(" Media Center 2005", "")
                .Replace(" Mobile Enterprise", "")
                .Replace(" Mobile", "")
                .Replace(" N", "")
                .Replace(" Pro Education", "")
                .Replace(" Pro for Workstations", "")
                .Replace(" Professional x64", "")
                .Replace(" Professional", "")
                .Replace(" Pro", "")
                .Replace(" Signature Edition", "")
                .Replace(" Single Language", "")
                .Replace(" Starter", "")
                .Replace(" S", "")
                .Replace(" Tablet PC", "")
                .Replace(" Team", "")
                .Replace(" Ultimate", "")
                .Replace(" VL", "")
                .Replace(" X", "")
                .Replace(" with Bing", "")
                .Replace(" ", "");
            string versionNumber;
            switch (fullName)
            {
                case "XP":
                    versionNumber = "5.1";
                    break;
                case "Vista":
                    versionNumber = "6.0";
                    break;
                case "7":
                    versionNumber = "6.1";
                    break;
                default:
                    versionNumber = fullName;
                    break;
            }
            return "NT " + fullName;
        }
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Cef.EnableHighDPISupport();
            createFolders();
            createThemes();
            if (!File.Exists(Application.StartupPath + "\\Lang\\English.klf"))
            {
                FixDefaultLanguage();
            }
            Settings settings = new Settings(Properties.Settings.Default.LastUser);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool appStarted = false;
            List<frmNotification> notifications = new List<frmNotification>();
            try
            {
                    
                if (args.Contains("-update"))
                {
                    if (UACControl.IsProcessElevated)
                    {
                        Application.Run(new Form1(settings));
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
                else if (args.Contains("--make-ext"))
                {
                    Application.Run(new frmMakeExt());
                    appStarted = true;
                    return;
                }
                else if (args.Contains("-oobe") || settings.LoadedDefaults || !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
                {
                    Application.Run(new frmOOBE(settings));
                    appStarted = true;
                    return;
                }
                else
                {
                    frmMain testApp = new frmMain(settings)
                    {
                        notifications = notifications,
                        isIncognito = args.Contains("-incognito")
                    };
                    bool isIncognito = args.Contains("-incognito");
                    if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
                    foreach (string x in args)
                    {
                        if (x == Application.ExecutablePath || x == "-oobe" || x == "-update") { }
                        else if (x == "-incognito")
                        {
                            testApp.Tabs.Add(new HTTitleTab(testApp) { Content = new frmCEF(settings,true, "korot://incognito", Properties.Settings.Default.LastUser) { } });
                        }
                        else if (x.ToLower().EndsWith(".kef") || x.ToLower().EndsWith(".ktf"))
                        {
                            Application.Run(new frmInstallExt(settings,x));
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
new HTTitleTab(testApp)
{
    Content = new frmCEF(settings,isIncognito, settings.Startup, Properties.Settings.Default.LastUser)
});
                    }
                    testApp.SelectedTabIndex = 0;
                    HTTitleTabsApplicationContext applicationContext = new HTTitleTabsApplicationContext();
                    applicationContext.Start(testApp);
                    Application.Run(applicationContext);
                    appStarted = true;
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(" [Korot] FATAL_ERROR: " + ex.ToString());
                frmError form = new frmError(ex,settings);
                if (!appStarted) { Application.Run(form); } else { form.Show(); }
            }
        }
        public static void RemoveDirectory(string directory,bool displayresult = true)
        {
            List<FileFolderError> errors = new List<FileFolderError>();
            foreach (String x in Directory.GetFiles(directory)) { try { File.Delete(x); } catch (Exception ex) { errors.Add(new FileFolderError(x, ex, false)); } }
            foreach (String x in Directory.GetDirectories(directory)) { try { Directory.Delete(x, true); } catch (Exception ex) { errors.Add(new FileFolderError(x, ex, true)); } }
            if (displayresult){if (errors.Count == 0){Output.WriteLine(" [RemoveDirectory] Removed \"" + directory + "\" with no errors."); }else {Output.WriteLine(" [RemoveDirectory] Removed \"" + directory + "\" with " + errors.Count + " error(s).");foreach (FileFolderError x in errors) { Output.WriteLine(" [RemoveDirectory] " + (x.isDirectory ? "Directory" : "File") + " Error: " + x.Location + " [" + x.Error.ToString() + "]"); } }}
        }
        public static bool createThemes()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Light.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Light</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#ffffff</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#55b4d4</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Light.ktf", newTheme, Encoding.UTF8);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Dark.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Dark</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#000000</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#55b4d4</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Dark.ktf", newTheme, Encoding.UTF8);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Midnight.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Midnight</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#050024</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#55b4d4</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Midnight.ktf", newTheme, Encoding.UTF8);
            }
            return true;
        }
        public static bool createFolders()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Extensions\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Logs\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\IconStorage\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\IconStorage\\"); }
            return true;
        }
        public static bool FixDefaultLanguage()
        {
            if (!Directory.Exists(Application.StartupPath + "\\Lang\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
            }
            HTAlt.Tools.WriteFile(Application.StartupPath + "\\Lang\\English.klf", Properties.Resources.English);
            return true;
        }
    }
    public class Settings
    { 
        public Settings (string Profile)
        {
            ProfileName = Profile;
            Extensions.Settings = this;
            if (string.IsNullOrWhiteSpace(Profile))
            {
                LoadedDefaults = true;
                Output.WriteLine(" [Settings] Loaded defaults because profile name was empty." + Environment.NewLine + " ProfileName: " + Profile);
                return;
            }
            if (!File.Exists(ProfileDirectory + "settings.kpf"))
            {
                LoadedDefaults = true;
                Output.WriteLine(" [Settings] Loaded defaults because can't find settings file." + Environment.NewLine + " at " + ProfileDirectory + "settings.kpf");
                return;
            }
            if (!Directory.Exists(ProfileDirectory))
            {
                LoadedDefaults = true;
                Output.WriteLine(" [Settings] Loaded defaults because can't find profile directory." + Environment.NewLine + " at " + ProfileDirectory);
                return;
            }
            string ManifestXML = HTAlt.Tools.ReadFile(ProfileDirectory + "settings.kpf", Encoding.UTF8);
            XmlDocument document = new XmlDocument();
            document.LoadXml(ManifestXML);
            foreach (XmlNode node in document.FirstChild.NextSibling.ChildNodes)
            {
                if (node.Name.ToLower() == "homepage")
                {
                    Homepage = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower().ToLowerInvariant() == "languagefile")
                {
                    string lf = node.InnerText.Replace("[KOROTPATH]", Application.StartupPath).Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    LanguageSystem.ReadFromFile(string.IsNullOrWhiteSpace(lf) ? Application.StartupPath + "\\Lang\\English.klf" : lf, true);
                }
                else if (node.Name.ToLower() == "menusize")
                {
                    string w = node.InnerText.Substring(0, node.InnerText.IndexOf(";"));
                    string h = node.InnerText.Substring(node.InnerText.IndexOf(";"), node.InnerText.Length - node.InnerText.IndexOf(";"));
                    MenuSize = new Size(Convert.ToInt32(w.Replace(";", "")), Convert.ToInt32(h.Replace(";", "")));
                }
                else if (node.Name.ToLower() == "menupoint")
                {
                    string x = node.InnerText.Substring(0, node.InnerText.IndexOf(";"));
                    string y = node.InnerText.Substring(node.InnerText.IndexOf(";"), node.InnerText.Length - node.InnerText.IndexOf(";"));
                    MenuPoint = new Point(Convert.ToInt32(x.Replace(";", "")), Convert.ToInt32(y.Replace(";", "")));
                }
                else if (node.Name.ToLower() == "searchengine")
                {
                    SearchEngine = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower() == "startup")
                {
                    Startup = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower() == "lastproxy")
                {
                    LastProxy = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower() == "menuwasmaximized")
                {
                    MenuWasMaximized = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "donottrack")
                {
                    DoNotTrack = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "autorestore")
                {
                    AutoRestore = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "rememberlastproxy")
                {
                    RememberLastProxy = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "theme")
                {
                    string themeFile = node.Attributes["File"] != null ? node.Attributes["File"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                    if (!File.Exists(themeFile)) { themeFile = ""; }
                    Theme = new Theme(themeFile);
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "name")
                        {
                            Theme.Name = subnode.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        }
                        else if (subnode.Name.ToLower() == "author")
                        {
                            Theme.Author = subnode.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        }
                        else if (subnode.Name.ToLower() == "backcolor")
                        {
                            Theme.BackColor = HTAlt.Tools.HexToColor(subnode.InnerText);
                        }
                        else if (subnode.Name.ToLower() == "overlaycolor")
                        {
                            Theme.OverlayColor = HTAlt.Tools.HexToColor(subnode.InnerText);
                        }
                        else if (subnode.Name.ToLower() == "backgroundstyle")
                        {
                            Theme.BackgroundStyle = subnode.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                            Theme.BackgroundStyleLayout = subnode.Attributes["Layout"] != null ? Convert.ToInt32(subnode.Attributes["Layout"].Value) : 0;
                        }
                        else if (subnode.Name.ToLower() == "newtabcolor")
                        {
                            if (subnode.InnerText == "0")
                            {
                                Theme.NewTabColor = TabColors.BackColor;
                            }
                            else if (subnode.InnerText == "1")
                            {
                                Theme.NewTabColor = TabColors.ForeColor;
                            }
                            else if (subnode.InnerText == "2")
                            {
                                Theme.NewTabColor = TabColors.OverlayColor;
                            }
                            else if (subnode.InnerText == "3")
                            {
                                Theme.NewTabColor = TabColors.OverlayBackColor;
                            }
                        }
                        else if (subnode.Name.ToLower() == "closebuttoncolor")
                        {
                            if (subnode.InnerText == "0")
                            {
                                Theme.CloseButtonColor = TabColors.BackColor;
                            }
                            else if (subnode.InnerText == "1")
                            {
                                Theme.CloseButtonColor = TabColors.ForeColor;
                            }
                            else if (subnode.InnerText == "2")
                            {
                                Theme.CloseButtonColor = TabColors.OverlayColor;
                            }
                            else if (subnode.InnerText == "3")
                            {
                                Theme.CloseButtonColor = TabColors.OverlayBackColor;
                            }
                        }
                    }
                }
                if (node.Name.ToLower() == "autosilent")
                {
                    AutoSilent = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "silent")
                {
                    Silent = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "donotplaysound")
                {
                    DoNotPlaySound = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "quietmode")
                {
                    QuietMode = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "autosilentmode")
                {
                    AutoSilentMode = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower() == "sites")
                {
                    Sites = new List<Site>();
                    foreach (XmlNode sitenode in node.ChildNodes)
                    {
                        Site site = new Site();
                        site.AllowCookies = sitenode.Attributes["AllowCookies"] != null ? (sitenode.Attributes["AllowCookies"].Value == "true") : false;
                        site.AllowNotifications = sitenode.Attributes["AllowNotifications"] != null ? (sitenode.Attributes["AllowNotifications"].Value == "true") : false;
                        site.Name = sitenode.Attributes["Name"] != null ? sitenode.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                        site.Url = sitenode.Attributes["Url"] != null ? sitenode.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                        Sites.Add(site);
                    }
                }
                else if (node.Name.ToLower() == "extensions")
                {
                    Extensions = new Extensions(node.ChildNodes.Count > 0 ? node.OuterXml : "") { Settings = this };
                }
                else if (node.Name.ToLower() == "collections")
                {
                    CollectionManager.readCollections(node.OuterXml, true);
                }
                else if (node.Name.ToLower() == "history")
                {
                   foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "site")
                        {
                            Site newSite = new Site();
                            newSite.Date = subnode.Attributes["Date"] != null ? subnode.Attributes["Date"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            newSite.Name = subnode.Attributes["Name"] != null ? subnode.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            newSite.Url = subnode.Attributes["Url"] != null ? subnode.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            History.Add(newSite);
                        }
                    }
                }
                else if (node.Name.ToLower() == "downloads")
                {
                    Downloads.DownloadDirectory = node.Attributes["directory"] != null ? node.Attributes["directory"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                    Downloads.OpenDownload = node.Attributes["open"] != null ? (node.Attributes["open"].Value == "true") : false;
                    Downloads.UseDownloadFolder = node.Attributes["usedownloadfolder"] != null ? (node.Attributes["usedownloadfolder"].Value == "true") : false;
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "site")
                        {
                            Site newSite = new Site();
                            newSite.Name = subnode.Attributes["Name"] != null ? subnode.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            newSite.Url = subnode.Attributes["Url"] != null ? subnode.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            newSite.Date = subnode.Attributes["Date"] != null ? subnode.Attributes["Date"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            newSite.LocalUrl = subnode.Attributes["LocalUrl"] != null ? subnode.Attributes["LocalUrl"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "";
                            int status = Convert.ToInt32(subnode.Attributes["Status"] != null ? subnode.Attributes["Status"].Value : "0");
                            if (status == 0)
                            {
                                newSite.Status = DownloadStatus.None;
                            }
                            else if (status == 1)
                            {
                                newSite.Status = DownloadStatus.Cancelled;
                            }
                            else if (status == 2)
                            {
                                newSite.Status = DownloadStatus.Downloaded;
                            }
                            else if (status == 3)
                            {
                                newSite.Status = DownloadStatus.Error;
                            }
                            Downloads.Downloads.Add(newSite);
                        }
                    }
                }
                else if (node.Name.ToLower() == "favorites")
                {
                    Favorites = new FavoritesSettings(node.ChildNodes.Count > 0 ? node.OuterXml : "")
                    {
                        ShowFavorites = node.Attributes["Show"] != null ? (node.Attributes["Show"].Value == "true") : false,
                    };
                }
            }
            if (string.IsNullOrWhiteSpace(Downloads.DownloadDirectory))
            {
                Downloads.DownloadDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
            }
        }
        #region Defaults
        public bool LoadedDefaults = false;
        private bool _Silent = false;
        private List<Site> _Sites = new List<Site>();
        private bool _AutoSilent = false;
        private bool _DoNotPlaySound = false;
        private bool _QuietMode = false;
        private string _AutoSilentMode = "";
        private string _ProfileName = "skrillex";
        private bool _DismissUpdate = false;
        private string _Homepage = "korot://newtab";
        private Size _MenuSize = new Size(720, 720);
        private Point _MenuPoint = new Point(0, 0);
        private string _SearchEngine = "https://www.google.com/search?q=";
        private bool _RememberLastProxy = false;
        private string _LastProxy = "";
        private bool _DisableLanguageError = false;
        private bool _MenuWasMaximized = true;
        private string _Startup = "korot://newtab";
        private bool _AutoRestore = false;
        private bool _DoNotTrack = true;
        private Theme _Theme = new Theme("");
        private List<Site> _History = new List<Site>();
        private LanguageSystem _LanguageSystem = new LanguageSystem("");
        private DownloadSettings _DownloadSettings = new DownloadSettings() { DownloadDirectory = "", Downloads = new List<Site>(), OpenDownload = false, UseDownloadFolder = false };
        private CollectionManager _CollectionManager = new CollectionManager("") { Collections = new List<Collection>() };
        private FavoritesSettings _Favorites = new FavoritesSettings("") { Favorites = new List<Folder>(), ShowFavorites = true };
        private Extensions _Extensions = new Extensions("");
        #endregion
        #region Properties
        public bool Silent
        {
            get => _Silent;
            set => _Silent = value;
        }
        public List<Site> Sites
        {
            get => _Sites;
            set => _Sites = value;
        }
        public bool AutoSilent
        {
            get => _AutoSilent;
            set => _AutoSilent = value;
        }
        public bool DoNotPlaySound
        {
            get => _DoNotPlaySound;
            set => _DoNotPlaySound = value;
        }
        public bool QuietMode
        {
            get => _QuietMode;
            set => _QuietMode = value;
        }
        public string AutoSilentMode
        {
            get => _AutoSilentMode;
            set => _AutoSilentMode = value;
        }

        public string ProfileName
        {
            get => _ProfileName;
            set => _ProfileName = value;
        }

        public bool DismissUpdate
        {
            get => _DismissUpdate;
            set => _DismissUpdate = value;
        }
        public string Homepage
        {
            get => _Homepage;
            set => _Homepage = value;
        }
        public Size MenuSize
        {
            get => _MenuSize;
            set => _MenuSize = value;
        }
        public Point MenuPoint
        {
            get => _MenuPoint;
            set => _MenuPoint = value;
        }
        public string SearchEngine
        {
            get => _SearchEngine;
            set => _SearchEngine = value;
        }
        public bool RememberLastProxy
        {
            get => _RememberLastProxy;
            set => _RememberLastProxy = value;
        }
        public string LastProxy
        {
            get => _LastProxy;
            set => _LastProxy = value;
        }
        public bool DisableLanguageError
        {
            get => _DisableLanguageError;
            set => _DisableLanguageError = value;
        }
        public bool MenuWasMaximized
        {
            get => _MenuWasMaximized;
            set => _MenuWasMaximized = value;
        }
        public bool DoNotTrack
        {
            get => _DoNotTrack;
            set => _DoNotTrack = value;
        }
        public Theme Theme
        {
            get => _Theme;
            set => _Theme = value;
        }
        public DownloadSettings Downloads
        {
            get => _DownloadSettings;
            set => _DownloadSettings = value;
        }
        public LanguageSystem LanguageSystem
        {
            get => _LanguageSystem;
            set => _LanguageSystem = value;
        }
        public CollectionManager CollectionManager
        {
            get => _CollectionManager;
            set => _CollectionManager = value;
        }
        public List<Site> History 
        {
            get => _History;
            set => _History = value;
        }
        public FavoritesSettings Favorites
        {
            get => _Favorites;
            set => _Favorites = value;
        }
        public Extensions Extensions
        {
            get => _Extensions;
            set => _Extensions = value;
        }
        public string Startup
        {
            get => _Startup;
            set => _Startup = value;
        }
        public bool AutoRestore
        {
            get => _AutoRestore;
            set => _AutoRestore = value;
        }
        #endregion
        public void Save()
        {
            string x =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine +
            "<Profile>" + Environment.NewLine +
            "<Homepage>" + Homepage.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Homepage>" + Environment.NewLine +
            "<MenuSize>" + MenuSize.Width + ";" + MenuSize.Height + "</MenuSize>" + Environment.NewLine +
            "<MenuPoint>" + MenuPoint.X +";" + MenuPoint.Y + "</MenuPoint>" + Environment.NewLine +
            "<SearchEngine>" + SearchEngine.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</SearchEngine>" + Environment.NewLine +
            "<LanguageFile>" + LanguageSystem.LangFile.Replace(Application.StartupPath,"[KOROTPATH]").Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</LanguageFile>" + Environment.NewLine +
            "<Startup>" + Startup.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Startup>" + Environment.NewLine +
            "<LastProxy>" + LastProxy.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</LastProxy>" + Environment.NewLine +
            "<MenuWasMaximized>" + (MenuWasMaximized ? "true" : "false") + "</MenuWasMaximized>" + Environment.NewLine +
            "<DoNotTrack>" + (DoNotTrack ? "true" : "false") + "</DoNotTrack>" + Environment.NewLine +
            "<AutoRestore>" + (AutoRestore ? "true" : "false") + "</AutoRestore>" + Environment.NewLine +
            "<RememberLastProxy>" + (RememberLastProxy ? "true" : "false") + "</RememberLastProxy>" + Environment.NewLine +
            "<Silent>" + (Silent ? "true" : "false") + "</Silent>" + Environment.NewLine +
            "<AutoSilent>" + (AutoSilent ? "true" : "false") + "</AutoSilent> " + Environment.NewLine +
            "<DoNotPlaySound>" + (DoNotPlaySound ? "true" : "false") + "</DoNotPlaySound>" + Environment.NewLine +
            "<QuietMode>" + (QuietMode ? "true" : "false") + "</QuietMode>" + Environment.NewLine +
            "<AutoSilentMode>" + AutoSilentMode.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</AutoSilentMode>" + Environment.NewLine +
            "<Sites>" + Environment.NewLine;
            foreach (Site site in Sites)
            {
                x += "<Site Name=\""
                     + site.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Url=\""
                     + site.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" AllowNotifications=\""
                     + (site.AllowNotifications ? "true" : "false")
                     + "\" AllowCookies=\""
                     + (site.AllowCookies ? "true" : "false")
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "</Sites> " + Environment.NewLine +
            "<Theme File=\"" + (!string.IsNullOrWhiteSpace(Theme.ThemeFile) ? Theme.ThemeFile.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") : "") + "\">" + Environment.NewLine +
            "<Name>" + Theme.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Name>" + Environment.NewLine +
            "<Author>" + Theme.Author.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Author>" + Environment.NewLine +
            "<BackColor>" + HTAlt.Tools.ColorToHex(Theme.BackColor) + "</BackColor>" + Environment.NewLine +
            "<OverlayColor>" + HTAlt.Tools.ColorToHex(Theme.OverlayColor) + "</OverlayColor>" + Environment.NewLine +
            "<BackgroundStyle Layout=\"" + Theme.BackgroundStyleLayout + "\">" + Theme.BackgroundStyle.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</BackgroundStyle>" + Environment.NewLine +
            "<NewTabColor>" + (int)Theme.NewTabColor + "</NewTabColor>" + Environment.NewLine +
            "<CloseButtonColor>"+ (int)Theme.CloseButtonColor + "</CloseButtonColor>" + Environment.NewLine +
            "</Theme>" + Environment.NewLine + Extensions.ExtractList + CollectionManager.writeCollections + "<History>" + Environment.NewLine;
            foreach (Site site in History)
            {
                x += "<Site Name=\""
                     + site.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Url=\""
                     + site.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Date=\""
                     + site.Date.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "</History>" + Environment.NewLine +
                "<Downloads Directory=\"" + Downloads.DownloadDirectory.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Open=\"" + (Downloads.OpenDownload ? "true" : "false") + "\" UseDownloadFolder=\"" + (Downloads.UseDownloadFolder ? "true" : "false") + "\">" + Environment.NewLine;
            foreach (Site site in Downloads.Downloads)
            {
                x += "<Site Name=\""
                     + site.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Url=\""
                     + site.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Status=\""
                     + (int)site.Status
                     + "\" Date=\""
                     + site.Date.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" LocalUrl=\""
                     + site.LocalUrl.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "</Downloads>" + Environment.NewLine + Favorites.outXml + "</Profile>" + Environment.NewLine;
            HTAlt.Tools.WriteFile(ProfileDirectory + "settings.kpf", x, Encoding.UTF8);
        }
        public string ProfileDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + ProfileName + "\\";
            }
        }
        public Site GetSiteFromUrl(string Url)
        {
            return Sites.Find(i => i.Url == Url);
        }
        
    }
    public class Theme
    {
        public void SaveTheme()
        {
            string x = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                "<Version>" + Version.ToString() + "</Version>" + Environment.NewLine +
                "<MinimumKorotVersion>" + MininmumKorotVersion.ToString() + "</MinimumKorotVersion>" + Environment.NewLine +
                "<UseHaltroyUpdate>" + (UseHaltroyUpdate ? "true" : "false") + "</UseHaltroyUpdate>" + Environment.NewLine +
            "<Name>" + Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Name>" + Environment.NewLine +
            "<Author>" + Author.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Author>" + Environment.NewLine +
            "<BackColor>" + HTAlt.Tools.ColorToHex(BackColor) + "</BackColor>" + Environment.NewLine +
            "<OverlayColor>" + HTAlt.Tools.ColorToHex(OverlayColor) + "</OverlayColor>" + Environment.NewLine +
            "<BackgroundStyle Layout=\"" + BackgroundStyleLayout + "\">" + BackgroundStyle.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</BackgroundStyle>" + Environment.NewLine +
            "<NewTabColor>" + (int)NewTabColor + "</NewTabColor>" + Environment.NewLine +
            "<CloseButtonColor>" + (int)CloseButtonColor + "</CloseButtonColor>" + Environment.NewLine +
            "</Theme>";
            HTAlt.Tools.WriteFile(ThemeFile, x, Encoding.UTF8);
        }
        public void LoadFromFile(string themeFile)
        {
            if (string.IsNullOrWhiteSpace(themeFile))
            {
                Name = "Korot Midnight";
                Author = "Haltroy";
                UseHaltroyUpdate = false;
                Version = new Version(Application.ProductVersion);
                MininmumKorotVersion = Version;
                BackColor = Color.FromArgb(255, 5, 0, 36);
                OverlayColor = Color.FromArgb(255, 85, 180, 212);
                BackgroundStyle = "BACKCOLOR";
                BackgroundStyleLayout = 0;
                NewTabColor = TabColors.OverlayColor;
                CloseButtonColor = TabColors.OverlayColor;
                LoadedDefaults = true;
                return;
            }
            ThemeFile = themeFile;
            string ManifestXML = HTAlt.Tools.ReadFile(ThemeFile, Encoding.UTF8);
            XmlDocument document = new XmlDocument();
            document.LoadXml(ManifestXML);
            XmlNode workNode = document.FirstChild;
            if (document.FirstChild.Name.ToLower() == "xml") { workNode = document.FirstChild.NextSibling; }
            foreach (XmlNode node in workNode.ChildNodes)
            {
                if (node.Name.ToLower() == "name")
                {
                    Name = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower() == "author")
                {
                    Author = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                }
                else if (node.Name.ToLower() == "usehaltroyupdate")
                {
                    UseHaltroyUpdate = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "version")
                {
                    Version = new Version(node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"));
                }
                else if (node.Name.ToLower() == "minimumkorotversion")
                {
                    MininmumKorotVersion = new Version(node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"));
                }
                else if (node.Name.ToLower() == "backcolor")
                {
                    BackColor = HTAlt.Tools.HexToColor(node.InnerText);
                }
                else if (node.Name.ToLower() == "overlaycolor")
                {
                    OverlayColor = HTAlt.Tools.HexToColor(node.InnerText);
                }
                else if (node.Name.ToLower() == "backgroundstyle")
                {
                    BackgroundStyle = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                    BackgroundStyleLayout = node.Attributes["Layout"] != null ? Convert.ToInt32(node.Attributes["Layout"].Value) : 0;
                }
                else if (node.Name.ToLower() == "newtabcolor")
                {
                    if (node.InnerText == "0")
                    {
                        NewTabColor = TabColors.BackColor;
                    }
                    else if (node.InnerText == "1")
                    {
                        NewTabColor = TabColors.ForeColor;
                    }
                    else if (node.InnerText == "2")
                    {
                        NewTabColor = TabColors.OverlayColor;
                    }
                    else if (node.InnerText == "3")
                    {
                        NewTabColor = TabColors.OverlayBackColor;
                    }
                }
                else if (node.Name.ToLower() == "closebuttoncolor")
                {
                    if (node.InnerText == "0")
                    {
                        CloseButtonColor = TabColors.BackColor;
                    }
                    else if (node.InnerText == "1")
                    {
                        CloseButtonColor = TabColors.ForeColor;
                    }
                    else if (node.InnerText == "2")
                    {
                        CloseButtonColor = TabColors.OverlayColor;
                    }
                    else if (node.InnerText == "3")
                    {
                        CloseButtonColor = TabColors.OverlayBackColor;
                    }
                }
            }
        }
        public Theme(string themeFile)
        {
            LoadFromFile(themeFile);
        }
        public bool LoadedDefaults = false;
        public Version Version { get; set; }
        public bool UseHaltroyUpdate { get; set; }
        public Version MininmumKorotVersion { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public Color BackColor { get; set; }
        public Color OverlayColor { get; set; }
        public string ThemeFile { get; set; }
        public string BackgroundStyle { get; set; }
        public int BackgroundStyleLayout { get; set; }
        public TabColors NewTabColor { get; set; }
        public TabColors CloseButtonColor { get; set; }
    }
    public class DownloadSettings
    {
        public bool OpenDownload { get; set; }
        public string DownloadDirectory { get; set; }
        public bool UseDownloadFolder { get; set; }
        public List<Site> Downloads { get; set; }
    }

    public class Site
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string LocalUrl { get; set; }
        public bool AllowCookies { get; set; }
        public string Date { get; set; }
        public bool AllowNotifications { get; set; }
        public DownloadStatus Status { get; set; }
    }
    public enum DownloadStatus
    {
        None,
        Cancelled,
        Downloaded,
        Error
    }
    public class FavoritesSettings
    {
        public void DeleteFolder(Folder folder)
        {
            if (folder == null) { return; }
            if (folder.IsTopFavorite)
            {
                Favorites.Remove(folder);
            }else
            {
                if (folder is Favorite)
                {
                    folder.ParentFolder.Favorites.Remove(folder);
                }else
                {
                    folder.Favorites.Clear();
                    folder.ParentFolder.Favorites.Remove(folder);
                }
            }
        }
        public FavoritesSettings(string xmlString)
        {
            Favorites = new List<Folder>();
            if (string.IsNullOrWhiteSpace(xmlString))
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(xmlString))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xmlString);
                foreach (XmlNode node in document.FirstChild.ChildNodes)
                {
                    if (node.Name == "Folder")
                    {
                        Folder folder = new Folder()
                        {
                            Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                            Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                        };
                        folder.ParentFolder = null;
                        GenerateMenusFromXML(node, folder);
                        Favorites.Add(folder);
                    }
                    else if (node.Name == "Favorite")
                    {
                        Favorite favorite = new Favorite()
                        {
                            Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                            Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                            Url = node.Attributes["Url"] == null ? "" : node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                            IconPath = node.Attributes["Icon"] == null ? "" : node.Attributes["Icon"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'")
                        };
                        favorite.ParentFolder = null;
                        Favorites.Add(favorite);
                    }
                }
            }
        }
        private void GenerateMenusFromXML(XmlNode rootNode, Folder folder)
        {
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name == "Folder")
                {
                    Folder subfolder = new Folder()
                    {
                        Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                        Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                    };
                    subfolder.ParentFolder = folder;
                    GenerateMenusFromXML(node, subfolder);
                    folder.Favorites.Add(subfolder);
                }
                else if (node.Name == "Favorite")
                {
                    Favorite favorite = new Favorite()
                    {
                        Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                        Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : HTAlt.Tools.GenerateRandomText(),
                        Url = node.Attributes["Url"] == null ? "" : node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"),
                        IconPath = node.Attributes["Icon"] == null ? "" : node.Attributes["Icon"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'")
                    };
                    favorite.ParentFolder = folder;
                    folder.Favorites.Add(favorite);
                }
            }
        }
        public string outXml
        {
            get
            {
                string x = "<Favorites Show=\"" + (ShowFavorites ? "true" : "false") + "\">" + Environment.NewLine;
                foreach (Folder y in Favorites)
                {
                    x += y.outXml + Environment.NewLine;
                }
                x += "</Favorites>" + Environment.NewLine;
                return x;
            }
        }
        private void RecursiveFWNF(Folder folder,List<Favorite> list)
        {
            foreach (Folder x in folder.Favorites)
            {
                if (x is Favorite)
                {
                    list.Add(x as Favorite);
                }
                else
                {
                    RecursiveFWNF(x,list);
                }
            }
        }
        public List<Favorite> FavoritesWithNoFolders
        {
            get
            {
                List<Favorite> fav = new List<Favorite>();
                foreach (Folder x in Favorites)
                {
                    if (x is Favorite)
                    {
                        fav.Add(x as Favorite);
                    }else
                    {
                        RecursiveFWNF(x,fav);
                    }
                }
                return fav;
            }
        }
        public List<Folder> Favorites { get; set; }
        public bool ShowFavorites { get; set; }
    }
    public class Folder
    {
        public Folder ParentFolder { get; set; }
        public bool IsTopFavorite => ParentFolder == null;
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Folder> Favorites { get; set; }
        public string outXml
        {
            get
            {
                bool isNotFolder = (this is Favorite);
                string x = "<" + (isNotFolder ? "Favorite" : "Folder") + " Name=\"" + Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Text=\"" + Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\"";
                if (isNotFolder)
                {
                    var favorite = this as Favorite;
                    x += " Url=\"" + favorite.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" IconPath=\"" + favorite.IconPath.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                }else
                {
                    x += ">" + Environment.NewLine;
                    foreach (Folder y in Favorites)
                    {
                        x += y.outXml + Environment.NewLine;
                    }
                    x += "</Folder>" + Environment.NewLine;
                }
                return x;
            }
        }
    }
    public class Favorite : Folder
    {
        public new List<Folder> Favorites => null;
        public string Url { get; set; }
        public string IconPath { get; set; }
        public Image Icon => HTAlt.Tools.ReadFile(IconPath,"ignored"); 
    }
    public class FileFolderError
    {
        public FileFolderError(string _Location,Exception _Error,bool IsDirectory)
        {
            isDirectory = IsDirectory;
            Location = _Location;
            Error = _Error;
        }
        public bool isDirectory { get; set; }
        public string Location { get; set; }
        public Exception Error { get; set; }
    }
    public class LanguageSystem
    {
        public List<LanguageItem> LanguageItems { get; set; } = new List<LanguageItem>();
        public string GetItemText(string ID)
        {
            LanguageItem item = LanguageItems.Find(i => i.ID.Trim() == ID.Trim());
            if (item == null)
            {
                Output.WriteLine(" [Language] Missing Item [ID=\"" + ID + "\" LangFile=\"" + _LangFile + "\" ItemCount=\"" + LanguageItems.Count + "\"]");
                return "[MI] " + ID;
            }else
            {
                return item.Text.Replace("[NEWLINE]", Environment.NewLine);
            }
        }
        private string _LangFile = Application.StartupPath + "\\Lang\\English.klf";
        public string LangFile => _LangFile;
        public LanguageSystem(string fileLoc)
        {
            ReadFromFile(!string.IsNullOrWhiteSpace(fileLoc) ? fileLoc : (Application.StartupPath + "\\Lang\\English.klf"), true);
        }
        public void ForceReadFromFile(string fileLoc, bool clear = true)
        {
            _LangFile = fileLoc;
            string code = HTAlt.Tools.ReadFile(fileLoc, Encoding.UTF8);
            ReadCode(code, clear);
        }
        public void ReadFromFile(string fileLoc,bool clear = true)
        {
            if (_LangFile != fileLoc || LanguageItems.Count == 0)
            {
                ForceReadFromFile(fileLoc, clear);
            }
        }
        public void ReadCode(string xmlCode,bool clear = true)
        {
            if(clear) { LanguageItems.Clear(); }
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlCode);
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name == "Translate")
                {
                    string id = node.Attributes["ID"] != null ? node.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"") : HTAlt.Tools.GenerateRandomText(12);
                    string text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;","\"") : id;
                    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(text))
                    {
                        LanguageItems.Add(new LanguageItem() { ID = id, Text = text });
                    }
                }
            }
        }
    }
    public class LanguageItem
    {
        public string ID { get; set; }
        public string Text { get; set; }
    }
}