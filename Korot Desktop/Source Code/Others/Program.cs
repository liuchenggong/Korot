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
            Tools.createFolders();
            Tools.createThemes();
            // hallo? salut.
            if(string.IsNullOrWhiteSpace(Properties.Settings.Default.LastUser)) { Properties.Settings.Default.LastUser = "o-zone"; }

            Settings settings = new Settings(Properties.Settings.Default.LastUser);

            if (!File.Exists(settings.LanguageFile)) { settings.LanguageFile = Application.StartupPath + "\\Lang\\English.lang"; }
            if (!File.Exists(Application.StartupPath + "\\Lang\\English.lang"))
            {
                Tools.FixDefaultLanguage();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool appStarted = false;
            List<frmNotification> notifications = new List<frmNotification>();
            try
            {
                if (string.IsNullOrWhiteSpace(settings.Downloads.DownloadDirectory))
                {
                    settings.Downloads.DownloadDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\";
                }
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
                else if (args.Contains("-oobe") || !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
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
    }
    public class Settings
    { 
        public Settings (string Profile)
        {
            ProfileName = Profile;
            // Read the file
            // If its not available then load defaults
            if (string.IsNullOrWhiteSpace(Profile) || !File.Exists(ProfileDirectory + "settings.kpf") || !Directory.Exists(ProfileDirectory))
            {
                Homepage = "korot://newtab";
                MenuSize = Screen.GetWorkingArea(new Point(0, 0)).Size;
                MenuPoint = new Point(0, 0);
                SearchEngine = "https://www.google.com/search?q=";
                Startup = "korot://homepage";
                LastProxy = "";
                MenuWasMaximized = true;
                DoNotTrack = true;
                AutoRestore = false;
                RememberLastProxy = false;
                Theme = new ThemeSettings("");
                Notification = new NotificationSettings() { AutoSilent = false, AutoSilentMode = "", DoNotPlaySound = false, QuietMode = false, Silent = false, Sites = new List<Site>() };
                Extensions = new Extensions("");
                CollectionManager = new CollectionManager();
                History = new HistorySettings("");
                Downloads = new DownloadSettings() { UseDownloadFolder = false, DownloadDirectory = "", OpenDownload = false, Downloads = new List<Site>() };
                Favorites = new FavoritesSettings("") { ShowFavorites = false};
                return;
            }
            string ManifestXML = HTAlt.Tools.ReadFile(ProfileDirectory + "settings.kpf", Encoding.UTF8);
            // Write XML to Stream so we don't need to load the same file again.
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(ManifestXML); //Writes our XML file
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream); //Loads our XML Stream
            // Make sure that this is an extension manifest.
            if (document.FirstChild.Name.ToLower() != "profile") { return; }
            // This is the part where my brain stopped and tried shutting down (aka sleep).
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLower() == "homepage")
                {
                    Homepage = node.InnerText;
                }
                else if (node.Name.ToLower() == "menusize")
                {
                    string w = node.InnerText.Substring(0, node.InnerText.IndexOf(";"));
                    string h = node.InnerText.Substring(node.InnerText.IndexOf(";"), node.InnerText.Length - node.InnerText.IndexOf(";"));
                    MenuSize = new Size(Convert.ToInt32(w.Replace(";","")), Convert.ToInt32(h.Replace(";", "")));
                }
                else if (node.Name.ToLower() == "menupoint")
                {
                    string x = node.InnerText.Substring(0, node.InnerText.IndexOf(";"));
                    string y = node.InnerText.Substring(node.InnerText.IndexOf(";"), node.InnerText.Length - node.InnerText.IndexOf(";"));
                    MenuPoint = new Point(Convert.ToInt32(x.Replace(";", "")), Convert.ToInt32(y.Replace(";", "")));
                }
                else if (node.Name.ToLower() == "searchengine")
                {
                    SearchEngine = node.InnerText;
                }
                else if (node.Name.ToLower() == "startup")
                {
                    Startup = node.InnerText;
                }
                else if (node.Name.ToLower() == "lastproxy")
                {
                    LastProxy = node.InnerText;
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
                    string themeFile = node.Attributes["File"] != null ? node.Attributes["File"].Value : "";
                    if (!File.Exists(themeFile)) { themeFile = ""; }
                    Theme = new ThemeSettings(themeFile);
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "name")
                        {
                            Theme.Name = subnode.InnerText;
                        }
                        else if (subnode.Name.ToLower() == "author")
                        {
                            Theme.Author = subnode.InnerText;
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
                            Theme.BackgroundStyle = subnode.InnerText;
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
                else if (node.Name.ToLower() == "notifications")
                {
                    Notification = new NotificationSettings();
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "autosilent")
                        {
                            Notification.AutoSilent = subnode.InnerText == "true";
                        }
                        else if (subnode.Name.ToLower() == "silent")
                        {
                            Notification.Silent = subnode.InnerText == "true";
                        }
                        else if (subnode.Name.ToLower() == "donotplaysound")
                        {
                            Notification.DoNotPlaySound = subnode.InnerText == "true";
                        }
                        else if (subnode.Name.ToLower() == "quietmode")
                        {
                            Notification.QuietMode = subnode.InnerText == "true";
                        }
                        else if (subnode.Name.ToLower() == "autosilentmode")
                        {
                            Notification.AutoSilentMode = subnode.InnerText;
                        }
                        else if (subnode.Name.ToLower() == "sites")
                        {
                            Notification.Sites = new List<Site>();
                            foreach (XmlNode sitenode in subnode.ChildNodes)
                            {
                                Site site = new Site();
                                site.AllowCookies = sitenode.Attributes["Cookies"] != null ? (sitenode.Attributes["Cookies"].Value == "true") : false;
                                site.AllowNotifications = sitenode.Attributes["AllowNotifications"] != null ? (sitenode.Attributes["AllowNotifications"].Value == "true") : false;
                                site.Name = sitenode.Attributes["Name"] != null ? sitenode.Attributes["Name"].Value : "";
                                site.Url = sitenode.Attributes["Url"] != null ? sitenode.Attributes["Url"].Value : "";
                                Notification.Sites.Add(site);
                            }
                        }
                    }
                }
                else if (node.Name.ToLower() == "extensions")
                {
                    Extensions = new Extensions(node.ChildNodes.Count > 0 ? node.OuterXml : "") { Settings = this };
                }
                else if (node.Name.ToLower() == "collections")
                {
                    CollectionManager = new CollectionManager();
                    CollectionManager.readCollections(node.ChildNodes.Count > 0 ? node.OuterXml : "", true);
                }
                else if (node.Name.ToLower() == "history")
                {
                    History = new HistorySettings(node.ChildNodes.Count > 0 ? node.OuterXml : "");
                }
                else if (node.Name.ToLower() == "downloads")
                {
                    Downloads = new DownloadSettings();
                    Downloads.Downloads = new List<Site>();
                    Downloads.DownloadDirectory = node.Attributes["directory"] != null ? node.Attributes["directory"].Value : "";
                    Downloads.OpenDownload = node.Attributes["open"] != null ? (node.Attributes["open"].Value == "true") : false;
                    Downloads.UseDownloadFolder = node.Attributes["usedownloadfolder"] != null ? (node.Attributes["usedownloadfolder"].Value == "true") : false;
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "site")
                        {
                            Site newSite = new Site();
                            newSite.Name = subnode.Attributes["Name"] != null ? subnode.Attributes["Name"].Value : "";
                            newSite.Url = subnode.Attributes["Url"] != null ? subnode.Attributes["Url"].Value : "";
                            newSite.Date = subnode.Attributes["Date"] != null ? subnode.Attributes["Date"].Value : "";
                            newSite.LocalUrl = subnode.Attributes["LocalUrl"] != null ? subnode.Attributes["LocalUrl"].Value : "";
                            int status = Convert.ToInt32(subnode.Attributes["LocalUrl"] != null ? subnode.Attributes["LocalUrl"].Value : "0");
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
        }
        public void Save()
        {
            string x =
            "<Profile>" + Environment.NewLine +
            "<Homepage>" + Homepage +"</Homepage>" + Environment.NewLine +
            "<MenuSize>" + MenuSize.Width + ";" + MenuSize.Height + "</MenuSize>" + Environment.NewLine +
            "<MenuPoint>" + MenuPoint.X +";" + MenuPoint.Y + "</MenuPoint>" + Environment.NewLine +
            "<SearchEngine>" + SearchEngine +"</SearchEngine>" + Environment.NewLine +
            "<Startup>" + Startup + "</Startup>" + Environment.NewLine +
            "<LastProxy>" + LastProxy +"</LastProxy>" + Environment.NewLine +
            "<MenuWasMaximized>" + (MenuWasMaximized ? "true" : "false") + "</MenuWasMaximized>" + Environment.NewLine +
            "<DoNotTrack>" + (DoNotTrack ? "true" : "false") + "</DoNotTrack>" + Environment.NewLine +
            "<AutoRestore>" + (AutoRestore ? "true" : "false") + "</AutoRestore>" + Environment.NewLine +
            "<RemeberLastProxy>" + (RememberLastProxy ? "true" : "false") + "</RemeberLastProxy>" + Environment.NewLine +
            "<Theme File=\"" + Theme.ThemeFile + "\">" + Environment.NewLine +
            "<Name>" + Theme.Name + "</Name>" + Environment.NewLine +
            "<Author>" + Theme.Author + "</Author>" + Environment.NewLine +
            "<BackColor>" + HTAlt.Tools.ColorToHex(Theme.BackColor) + "</BackColor>" + Environment.NewLine +
            "<OverlayColor>" + HTAlt.Tools.ColorToHex(Theme.OverlayColor) + "</OverlayColor>" + Environment.NewLine +
            "<BackgroundStyle Layout=\"" + Theme.BackgroundStyleLayout + "\">" + Theme.BackgroundStyle + "</BackgroundStyle>" + Environment.NewLine +
            "<NewTabColor>" + (int)Theme.NewTabColor + "</NewTabColor>" + Environment.NewLine +
            "<CloseButtonColor>"+ (int)Theme.CloseButtonColor + "</CloseButtonColor>" + Environment.NewLine +
            "</Theme>" + Environment.NewLine +
            "<Notifications>" + Environment.NewLine +
            "<Silent>" + (Notification.Silent ? "true" : "false") + "</Silent>" + Environment.NewLine + 
            "<AutoSilent>" + (Notification.AutoSilent ? "true" : "false") +"</AutoSilent> " + Environment.NewLine +
            "<DoNotPlaySound>" + (Notification.DoNotPlaySound ? "true" : "false") + "</DoNotPlaySound>" + Environment.NewLine +
            "<QuietMode>" + (Notification.QuietMode ? "true" : "false") + "</QuietMode>" + Environment.NewLine +
            "<AutoSilentMode>" + Notification.AutoSilentMode + "</AutoSilentMode>" + Environment.NewLine +
            "<Sites>" + Environment.NewLine;
            foreach (Site site in Notification.Sites)
            {
                x += "<Site Name=\""
                     + site.Name
                     + "\" Url=\""
                     + site.Url
                     + "\" AllowNotifications=\""
                     + (site.AllowNotifications ? "true" : "false")
                     + "\" AllowCookies=\""
                     + (site.AllowCookies ? "true" : "false")
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "</Sites> " + Environment.NewLine + "</Notifications>" + Environment.NewLine + Extensions.ExtractList + CollectionManager.writeCollections + "<History>" + Environment.NewLine;
            foreach (Site site in Notification.Sites)
            {
                x += "<Site Name=\""
                     + site.Name
                     + "\" Url=\""
                     + site.Url
                     + "\" Date=\""
                     + site.Date
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "</History>" + Environment.NewLine +
                "<Downloads Directory=\"\" Open=\"false\" UseDownloadFolder=\"false\">" + Environment.NewLine;
            foreach (Site site in Notification.Sites)
            {
                x += "<Site Name=\""
                     + site.Name
                     + "\" Url=\""
                     + site.Url
                     + "\" Status=\""
                     + (int)site.Status
                     + "\" Date=\""
                     + site.Date
                     + "\" LocalUrl=\""
                     + site.LocalUrl
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "</Downloads>" + Environment.NewLine + Favorites.outXml + "</Profile>" + Environment.NewLine;
            HTAlt.Tools.WriteFile(ProfileDirectory + "settings.kpf", x, Encoding.UTF8);
        }
        public string ProfileName { get; set; }
        public string ProfileDirectory
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + ProfileName + "\\";
            }
        }
        public bool DismissUpdate { get; set; }
        public string Homepage { get; set; }
        public Size MenuSize { get; set; }
        public Point MenuPoint { get; set; }
        public string SearchEngine { get; set; }
        public string LanguageFile { get; set; }
        public bool RememberLastProxy { get; set; }
        public string LastProxy { get; set; }
        public bool DisableLanguageError { get; set; }
        public bool MenuWasMaximized { get; set; }
        public bool DoNotTrack { get; set; }
        public DownloadSettings Downloads { get; set; }
        public ThemeSettings Theme { get; set; }
        public CollectionManager CollectionManager { get; set; }
        public HistorySettings History { get; set; }
        public NotificationSettings Notification { get; set; }
        public FavoritesSettings Favorites { get; set; }
        public Extensions Extensions { get; set; }
        public string Startup { get; set; }
        public bool AutoRestore { get; set; }
    }
    public class ThemeSettings
    {
        public ThemeSettings(string themeFile)
        {
            if (string.IsNullOrWhiteSpace(themeFile))
            {
                Name = "Korot Default";
                Author = "Haltroy";
                BackColor = Color.FromArgb(255, 5, 0, 36);
                OverlayColor = Color.FromArgb(255, 85, 180, 212);
                BackgroundStyle = "BACKCOLOR";
                BackgroundStyleLayout = 0;
                NewTabColor = TabColors.OverlayColor;
                CloseButtonColor = TabColors.OverlayColor;
                return;
            }
            ThemeFile = themeFile;
            string ManifestXML = HTAlt.Tools.ReadFile(ThemeFile, Encoding.UTF8);
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(ManifestXML);
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            if (document.FirstChild.Name.ToLower() != "theme") { return; }
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLower() == "name")
                {
                    Name = node.InnerText;
                }
                else if (node.Name.ToLower() == "author")
                {
                    Author = node.InnerText;
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
                    BackgroundStyle = node.InnerText;
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
    public static class VersionInfo
    {
        public static string CodeName => "Laika";
        public static bool IsPreRelease => true;
        public static int PreReleaseNumber => 1;
    }
    public class HistorySettings
    {
        public HistorySettings(string historyxml)
        {
            History = new List<Site>();
            if (string.IsNullOrWhiteSpace(historyxml) || historyxml.ToLower().Replace(Environment.NewLine, "") == "<history></history>")
            {
                return;
            }
            XmlDocument document = new XmlDocument();
            document.Load(historyxml);
            if (document.FirstChild.Name.ToLower() != "history") { return; }
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLower() == "site")
                {
                    Site newSite = new Site();
                    newSite.Date = node.Attributes["Date"] != null ? node.Attributes["Date"].Value : "";
                    newSite.Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value : "";
                    newSite.Url = node.Attributes["Url"] != null ? node.Attributes["Url"].Value : "";
                    History.Add(newSite);
                }
            }
        }
        public List<Site> History { get; set; }
    }
    public class NotificationSettings
    {
        public bool Silent { get; set; }
        public List<Site> Sites { get; set; }
        public bool AutoSilent { get; set; }
        public bool DoNotPlaySound { get; set; }
        public bool QuietMode { get; set; }
        public string AutoSilentMode { get; set; }
        public Site GetSiteFromUrl(string Url)
        {
            if (Sites.Find(i => i.Url == Url) == null)
            {
                return new Site() { Url = Url, AllowCookies = false, AllowNotifications = false };
            }
            return Sites.Find(i => i.Url == Url);
        }
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
                document.Load(xmlString);

                foreach (XmlNode node in document.FirstChild.ChildNodes)
                {
                    if (node.Name == "Folder")
                    {
                        Folder folder = new Folder()
                        {
                            Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value : HTAlt.Tools.GenerateRandomText(),
                            Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value: HTAlt.Tools.GenerateRandomText(),
                        };
                        folder.ParentFolder = null;
                        GenerateMenusFromXML(node, folder);
                        Favorites.Add(folder);
                    }
                    else if (node.Name == "Favorite")
                    {
                        Favorite favorite = new Favorite()
                        {
                            Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value : HTAlt.Tools.GenerateRandomText(),
                            Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value : HTAlt.Tools.GenerateRandomText(),
                            Url = node.Attributes["Url"] == null ? "" : node.Attributes["Url"].Value,
                            IconPath = node.Attributes["Icon"] == null ? "" : node.Attributes["Icon"].Value
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
                        Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value : HTAlt.Tools.GenerateRandomText(),
                        Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value : HTAlt.Tools.GenerateRandomText(),
                    };
                    subfolder.ParentFolder = folder;
                    GenerateMenusFromXML(node, subfolder);
                    folder.Favorites.Add(subfolder);
                }
                else if (node.Name == "Favorite")
                {
                    Favorite favorite = new Favorite()
                    {
                        Name = node.Attributes["Name"] != null ? node.Attributes["Name"].Value : HTAlt.Tools.GenerateRandomText(),
                        Text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value : HTAlt.Tools.GenerateRandomText(),
                        Url = node.Attributes["Url"] == null ? "" : node.Attributes["Url"].Value,
                        IconPath = node.Attributes["Icon"] == null ? "" : node.Attributes["Icon"].Value
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
                string x = "<" + (isNotFolder ? "Favorite" : "Folder") + " Name=\"" + Name + "\" Text=\"" + Text + "\"";
                if (isNotFolder)
                {
                    var favorite = this as Favorite;
                    x += " Url=\"" + favorite.Url + "\" IconPath=\"" + favorite.IconPath + "\" />";
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
    
}
