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
using CefSharp.WinForms;
using EasyTabs;
using HTAlt;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public static class VersionInfo
    {
        public static string CodeName => "Qarabağ";

        public static int VersionNumber = 52;
    }

    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Cef.EnableHighDPISupport();
            KorotTools.createFolders();
            KorotTools.createThemes();
            if (!File.Exists(Application.StartupPath + "\\Lang\\English.klf"))
            {
                KorotTools.FixDefaultLanguage();
            }
            Settings settings = new Settings(SafeFileSettingOrganizedClass.LastUser);
            if (string.IsNullOrWhiteSpace(settings.Birthday) && !settings.CelebrateBirthday)
            {
                settings.BirthdayCount++;
            }
            if (settings.BirthdayCount > 10)
            {
                frmAskBirthday askBDay = new frmAskBirthday(settings);
                askBDay.ShowDialog();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool appStarted = false;
            List<frmNotification> notifications = new List<frmNotification>();
            try
            {
                if (args.Contains("--make-ext"))
                {
                    Application.Run(new frmMakeExt());
                    appStarted = true;
                    return;
                }
                else if (args.Contains("-oobe") || settings.LoadedDefaults || !Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\"))
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
                    settings.AllForms.Add(testApp);
                    bool isIncognito = args.Contains("-incognito");
                    if (SafeFileSettingOrganizedClass.LastUser == "") { SafeFileSettingOrganizedClass.LastUser = "user0"; }
                    foreach (string x in args)
                    {
                        if (x == Application.ExecutablePath || x == "-oobe" || x == "-update") { }
                        else if (x == "-incognito")
                        {
                            frmCEF cefform = new frmCEF(testApp, settings, true, "korot://incognito", SafeFileSettingOrganizedClass.LastUser) { };
                            settings.AllForms.Add(cefform);
                            testApp.Tabs.Add(new TitleBarTab(testApp) { Content = cefform });
                        }
                        else if (x.ToLower().EndsWith(".kef") || x.ToLower().EndsWith(".ktf"))
                        {
                            Application.Run(new frmInstallExt(settings, x));
                            appStarted = true;
                        }
                        else
                        {
                            testApp.CreateTab(x);
                        }
                    }
                    if (testApp.Tabs.Count < 1)
                    {
                        frmCEF cefform = new frmCEF(testApp, settings, isIncognito, settings.Startup, SafeFileSettingOrganizedClass.LastUser);
                        settings.AllForms.Add(cefform);
                        testApp.Tabs.Add(
new TitleBarTab(testApp)
{
    Content = cefform
});
                    }
                    testApp.SelectedTabIndex = 0;
                    TitleBarTabsApplicationContext applicationContext = new TitleBarTabsApplicationContext();
                    applicationContext.Start(testApp);
                    Application.Run(applicationContext);
                    appStarted = true;
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(" [Korot] FATAL_ERROR: " + ex.ToString());
                frmError form = new frmError(ex, settings);
                if (!appStarted) { Application.Run(form); } else { form.Show(); }
            }
        }

        public static void RemoveDirectory(string directory, bool displayresult = true)
        {
            List<FileFolderError> errors = new List<FileFolderError>();
            foreach (string x in Directory.GetFiles(directory)) { try { File.Delete(x); } catch (Exception ex) { errors.Add(new FileFolderError(x, ex, false)); } }
            foreach (string x in Directory.GetDirectories(directory)) { try { Directory.Delete(x, true); } catch (Exception ex) { errors.Add(new FileFolderError(x, ex, true)); } }
            if (displayresult) { if (errors.Count == 0) { Output.WriteLine(" [RemoveDirectory] Removed \"" + directory + "\" with no errors."); } else { Output.WriteLine(" [RemoveDirectory] Removed \"" + directory + "\" with " + errors.Count + " error(s)."); foreach (FileFolderError x in errors) { Output.WriteLine(" [RemoveDirectory] " + (x.isDirectory ? "Directory" : "File") + " Error: " + x.Location + " [" + x.Error.ToString() + "]"); } } }
        }
    }

    public class Settings
    {
        public Settings(string Profile)
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
            string ManifestXML = HTAlt.Tools.ReadFile(ProfileDirectory + "settings.kpf", Encoding.Unicode);
            XmlDocument document = new XmlDocument();
            document.LoadXml(ManifestXML);
            List<string> loadedSettings = new List<string>();
            foreach (XmlNode node in document.FirstChild.NextSibling.ChildNodes)
            {
                if (loadedSettings.Contains(node.Name.ToLower())) { return; } else { loadedSettings.Add(node.Name.ToLower()); }
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
                else if (node.Name.ToLower() == "ninjamode")
                {
                    NinjaMode = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "usedefaultsound")
                {
                    UseDefaultSound = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "soundlocation")
                {
                    if (!File.Exists(node.InnerText))
                    {
                        UseDefaultSound = true;
                    }
                    else
                    {
                        SoundLocation = node.InnerText;
                    }
                }
                else if (node.Name.ToLower() == "donottrack")
                {
                    DoNotTrack = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "flash")
                {
                    Flash = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "birthday")
                {
                    if (node.Attributes["Celebrate"] != null && node.Attributes["Date"] != null && node.Attributes["Count"] != null)
                    {
                        CelebrateBirthday = node.Attributes["Celebrate"].Value == "true";
                        Birthday = node.Attributes["Date"].Value;
                        BirthdayCount = Convert.ToInt32(node.Attributes["Count"].Value);
                    }
                }
                else if (node.Name.ToLower() == "autorestore")
                {
                    AutoRestore = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "rememberlastproxy")
                {
                    RememberLastProxy = node.InnerText == "true";
                }
                else if (node.Name.ToLower() == "screenshotfolder")
                {
                    ScreenShotFolder = node.InnerText;
                }
                else if (node.Name.ToLower() == "savefolder")
                {
                    SaveFolder = node.InnerText;
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
                else if (node.Name.ToLower() == "newtabmenu")
                {
                    NewTabSites = new NewTabSites(node.OuterXml);
                }
                else if (node.Name.ToLower() == "autosilent")
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
                        Site site = new Site
                        {
                            AllowCookies = sitenode.Attributes["AllowCookies"] != null ? (sitenode.Attributes["AllowCookies"].Value == "true") : false,
                            AllowNotifications = sitenode.Attributes["AllowNotifications"] != null ? (sitenode.Attributes["AllowNotifications"].Value == "true") : false,
                            Name = sitenode.Attributes["Name"] != null ? sitenode.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "",
                            Url = sitenode.Attributes["Url"] != null ? sitenode.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : ""
                        };
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
                else if (node.Name.ToLower() == "autocleaner")
                {
                    AutoCleaner.LoadFromXML(node.OuterXml);
                }
                else if (node.Name.ToLower() == "history")
                {
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "site")
                        {
                            Site newSite = new Site
                            {
                                Date = subnode.Attributes["Date"] != null ? subnode.Attributes["Date"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "",
                                Name = subnode.Attributes["Name"] != null ? subnode.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "",
                                Url = subnode.Attributes["Url"] != null ? subnode.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : ""
                            };
                            History.Add(newSite);
                        }
                    }
                }
                else if (node.Name.ToLower() == "siteblocks")
                {
                    foreach (XmlNode subnode in node.ChildNodes)
                    {
                        if (subnode.Name.ToLower() == "block")
                        {
                            if (subnode.Attributes["Level"] == null || subnode.Attributes["Filter"] == null || subnode.Attributes["Url"] == null) { }
                            else
                            {
                                BlockSite bs = new BlockSite() { Address = subnode.Attributes["Url"].Value, BlockLevel = Convert.ToInt32(subnode.Attributes["Level"].Value), Filter = subnode.Attributes["Filter"].Value };
                                Filters.Add(bs);
                            }
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
                            Site newSite = new Site
                            {
                                Name = subnode.Attributes["Name"] != null ? subnode.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "",
                                Url = subnode.Attributes["Url"] != null ? subnode.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "",
                                Date = subnode.Attributes["Date"] != null ? subnode.Attributes["Date"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : "",
                                LocalUrl = subnode.Attributes["LocalUrl"] != null ? subnode.Attributes["LocalUrl"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'") : ""
                            };
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
            _AutoCleaner.Settings = this;
        }

        #region Defaults

        private string _birthday = "";
        private bool _celebrateBDay = true;
        private int _bDayCount = 0;
        private AutoCleaner _AutoCleaner = new AutoCleaner("");
        private bool _UseDefaultSound = true;
        private string _SoundLoc = "";
        private bool _NinjaMode = false;
        private List<BlockSite> _Filters = new List<BlockSite>();
        private NewTabSites _NewTabSites = new NewTabSites("");
        private bool _Flash = false;
        public bool LoadedDefaults = false;
        private bool _Silent = false;
        private List<Site> _Sites = new List<Site>();
        private bool _AutoSilent = false;
        private bool _DoNotPlaySound = false;
        private bool _QuietMode = false;
        private string _AutoSilentMode = "";
        private string _ProfileName = "";
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
        private string _saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _screenshotFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private Extensions _Extensions = new Extensions("");
        private List<frmCEF> _UpdateFavorites = new List<frmCEF>();

        #endregion Defaults

        #region Properties

        public string Birthday { get => _birthday; set => _birthday = value; }
        public bool CelebrateBirthday { get => _celebrateBDay; set => _celebrateBDay = value; }
        public int BirthdayCount { get => _bDayCount; set => _bDayCount = value; }

        public AutoCleaner AutoCleaner { get => _AutoCleaner; set => _AutoCleaner = value; }
        public List<frmCEF> UpdateFavorites { get => _UpdateFavorites; set => _UpdateFavorites = value; }

        public void UpdateFavList()
        {
            for (int i = 0; i < AllForms.Count; i++)
            {
                if (AllForms[i] is frmCEF)
                {
                    UpdateFavorites.Add(AllForms[i] as frmCEF);
                }
            }
        }

        public bool UseDefaultSound { get => _UseDefaultSound; set => _UseDefaultSound = value; }
        public string SoundLocation { get => _SoundLoc; set => _SoundLoc = value; }
        public bool NinjaMode { get => _NinjaMode; set => _NinjaMode = value; }
        public string ScreenShotFolder { get => _screenshotFolder; set => _screenshotFolder = value; }
        public string SaveFolder { get => _saveFolder; set => _saveFolder = value; }

        public class BlockLevels
        {
            public static string Level0 = @"((http)|(https))\:\/\/§SITE§";
            public static string Level1 = @"((http)|(https))\:\/\/§SITE§";
            public static string Level2 = @"((http)|(https))\:\/\/[^\/]*?§SITE§";
            public static string Level3 = @"§SITE§";

            public static string Convert(string Url, string Level)
            {
                return Level.Replace("§SITE§", Url.Replace(".", @"\."));
            }

            public static string ConvertToLevel0(string Url)
            {
                return Convert(Url.Replace("https://", "").Replace("http://", ""), Level0);
            }

            public static string ConvertToLevel1(string Url)
            {
                return Convert(HTAlt.Tools.GetBaseURL(Url).Replace("https://", "").Replace("http://", ""), Level1);
            }

            public static string ConvertToLevel2(string Url)
            {
                return Convert(HTAlt.Tools.GetBaseURL(Url).Replace("https://", "").Replace("http://", ""), Level2);
            }

            public static string ConvertToLevel3(string Url)
            {
                return Convert(HTAlt.Tools.GetBaseURL(Url).Replace("https://", "").Replace("http://", ""), Level3);
            }
        }

        public List<BlockSite> Filters
        {
            get => _Filters;
            set => _Filters = value;
        }

        public NewTabSites NewTabSites
        {
            get => _NewTabSites;
            set => _NewTabSites = value;
        }

        public bool Flash
        {
            get => _Flash;
            set => _Flash = value;
        }

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

        #endregion Properties

        public List<Form> AllForms = new List<Form>();
        public List<Form> ThemeChangeForm = new List<Form>();

        public void JustChangedTheme()
        {
            for (int i = 0; i < AllForms.Count; i++) { ThemeChangeForm.Add(AllForms[i]); }
        }

        public bool IsQuietTime
        {
            get
            {
                string Playlist = AutoSilentMode;
                string[] SplittedFase = Playlist.Split(':');
                if (SplittedFase.Length - 1 > 9)
                {
                    bool sunday = SplittedFase[4] == "1";
                    bool monday = SplittedFase[5] == "1";
                    bool tuesday = SplittedFase[6] == "1";
                    bool wednesday = SplittedFase[7] == "1";
                    bool thursday = SplittedFase[8] == "1";
                    bool friday = SplittedFase[9] == "1";
                    bool saturday = SplittedFase[10] == "1";
                    int fromH = Convert.ToInt32(SplittedFase[0]);
                    int fromM = Convert.ToInt32(SplittedFase[1]);
                    int toH = Convert.ToInt32(SplittedFase[2]);
                    int toM = Convert.ToInt32(SplittedFase[3]);
                    bool Nsunday = sunday;
                    bool Nmonday = monday;
                    bool Ntuesday = tuesday;
                    bool Nwednesday = wednesday;
                    bool Nthursday = thursday;
                    bool Nfriday = friday;
                    bool Nsaturday = saturday;
                    if (AutoSilent)
                    {
                        DayOfWeek wk = DateTime.Today.DayOfWeek;
                        if ((Nsunday && wk == DayOfWeek.Sunday)
                            || (Nmonday && wk == DayOfWeek.Monday)
                            || (Ntuesday && wk == DayOfWeek.Tuesday)
                            || (Nwednesday && wk == DayOfWeek.Wednesday)
                            || (Nthursday && wk == DayOfWeek.Thursday)
                            || (Nfriday && wk == DayOfWeek.Friday)
                            || (Nsaturday && wk == DayOfWeek.Saturday))
                        {
                            //it passed the first test to be silent.
                            DateTime date = DateTime.Now;
                            int h = date.Hour;
                            int m = date.Minute;
                            if (fromH < h)
                            {
                                if (toH > h)
                                {
                                    QuietMode = true;
                                }
                                else if (toH == h)
                                {
                                    if (m >= toM)
                                    {
                                        QuietMode = true;
                                    }
                                    else
                                    {
                                        QuietMode = false;
                                    }
                                }
                                else
                                {
                                    QuietMode = false;
                                }
                            }
                            else if (fromH == h)
                            {
                                if (m >= fromM)
                                {
                                    QuietMode = true;
                                }
                                else
                                {
                                    QuietMode = false;
                                }
                            }
                            else
                            {
                                QuietMode = false;
                            }
                        }
                        else
                        {
                            QuietMode = false;
                        }
                    }
                    if (Silent) { QuietMode = true; }
                }
                return QuietMode;
            }
        }

        public bool IsUrlAllowed(string url)
        {
            bool allowed = true;
            foreach (BlockSite x in Filters)
            {
                Regex Rgx = new Regex(x.Filter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                allowed = !Rgx.IsMatch(url);
            }
            return allowed;
        }


        public void Save()
        {
            string x =
                "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine +
            "<Profile>" + Environment.NewLine +
            " <Homepage>" + Homepage.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Homepage>" + Environment.NewLine +
            "   <MenuSize>" + MenuSize.Width + ";" + MenuSize.Height + "</MenuSize>" + Environment.NewLine +
            "   <MenuPoint>" + MenuPoint.X + ";" + MenuPoint.Y + "</MenuPoint>" + Environment.NewLine +
            "   <ScreenShotFolder>" + ScreenShotFolder + "</ScreenShotFolder>" + Environment.NewLine +
            "   <SaveFolder>" + SaveFolder + "</SaveFolder>" + Environment.NewLine +
            "   <SearchEngine>" + SearchEngine.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</SearchEngine>" + Environment.NewLine +
            "   <LanguageFile>" + LanguageSystem.LangFile.Replace(Application.StartupPath, "[KOROTPATH]").Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</LanguageFile>" + Environment.NewLine +
            "   <Startup>" + Startup.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Startup>" + Environment.NewLine +
            "   <LastProxy>" + LastProxy.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</LastProxy>" + Environment.NewLine +
            "   <MenuWasMaximized>" + (MenuWasMaximized ? "true" : "false") + "</MenuWasMaximized>" + Environment.NewLine +
            "   <Flash>" + (Flash ? "true" : "false") + "</Flash>" + Environment.NewLine +
            "   <Birthday Celebrate=\"" + (CelebrateBirthday ? "true" : "false") + "\" Date=\"" + Birthday + "\" Count=\"" + BirthdayCount + "\" />" + Environment.NewLine +
            "   <DoNotTrack>" + (DoNotTrack ? "true" : "false") + "</DoNotTrack>" + Environment.NewLine +
            "   <AutoRestore>" + (AutoRestore ? "true" : "false") + "</AutoRestore>" + Environment.NewLine +
            "   <RememberLastProxy>" + (RememberLastProxy ? "true" : "false") + "</RememberLastProxy>" + Environment.NewLine +
            "   <Silent>" + (Silent ? "true" : "false") + "</Silent>" + Environment.NewLine +
            "   <AutoSilent>" + (AutoSilent ? "true" : "false") + "</AutoSilent> " + Environment.NewLine +
            "   <DoNotPlaySound>" + (DoNotPlaySound ? "true" : "false") + "</DoNotPlaySound>" + Environment.NewLine +
            "   <QuietMode>" + (QuietMode ? "true" : "false") + "</QuietMode>" + Environment.NewLine +
            "   <AutoSilentMode>" + AutoSilentMode.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</AutoSilentMode>" + Environment.NewLine + AutoCleaner.XMLOut() + Environment.NewLine +
            "   <Sites>" + Environment.NewLine;
            foreach (Site site in Sites)
            {
                x += "     <Site Name=\""
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
            x += "   </Sites>" + Environment.NewLine + "   <SiteBlocks>" + Environment.NewLine;
            foreach (BlockSite block in Filters)
            {
                // .Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                x += "     <Block Level=\"" + block.BlockLevel + "\" Url=\"" + block.Address + "\" Filter=\"" + block.Filter + "\" />" + Environment.NewLine;
            }
            x += "   </SiteBlocks>" + Environment.NewLine + "   <Theme File=\"" + (!string.IsNullOrWhiteSpace(Theme.ThemeFile) ? Theme.ThemeFile.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") : "") + "\">" + Environment.NewLine +
            "     <Name>" + Theme.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Name>" + Environment.NewLine +
            "     <Author>" + Theme.Author.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Author>" + Environment.NewLine +
            "     <BackColor>" + HTAlt.Tools.ColorToHex(Theme.BackColor) + "</BackColor>" + Environment.NewLine +
            "     <OverlayColor>" + HTAlt.Tools.ColorToHex(Theme.OverlayColor) + "</OverlayColor>" + Environment.NewLine +
            "     <BackgroundStyle Layout=\"" + Theme.BackgroundStyleLayout + "\">" + Theme.BackgroundStyle.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</BackgroundStyle>" + Environment.NewLine +
            "     <NewTabColor>" + (int)Theme.NewTabColor + "</NewTabColor>" + Environment.NewLine +
            "     <CloseButtonColor>" + (int)Theme.CloseButtonColor + "</CloseButtonColor>" + Environment.NewLine +
            "     </Theme>" + Environment.NewLine + NewTabSites.XMLOut + Environment.NewLine + Extensions.ExtractList + CollectionManager.writeCollections + "   <History>" + Environment.NewLine;
            foreach (Site site in History)
            {
                x += "     <Site Name=\""
                     + site.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Url=\""
                     + site.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" Date=\""
                     + site.Date.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;")
                     + "\" />"
                     + Environment.NewLine;
            }
            x += "   </History>" + Environment.NewLine +
                "   <Downloads Directory=\"" + Downloads.DownloadDirectory.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Open=\"" + (Downloads.OpenDownload ? "true" : "false") + "\" UseDownloadFolder=\"" + (Downloads.UseDownloadFolder ? "true" : "false") + "\">" + Environment.NewLine;
            foreach (Site site in Downloads.Downloads)
            {
                x += "     <Site Name=\""
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
            x += "   </Downloads>" + Environment.NewLine + Favorites.outXml + "   </Profile>" + Environment.NewLine;
            HTAlt.Tools.WriteFile(ProfileDirectory + "settings.kpf", x, Encoding.Unicode);
        }

        public string ProfileDirectory => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + ProfileName + "\\";

        public Site GetSiteFromUrl(string Url)
        {
            return Sites.Find(i => i.Url == Url);
        }
    }

    public class Theme
    {
        public void SaveTheme()
        {
            string x = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                "<Version>" + Version.ToString() + "</Version>" + Environment.NewLine +
                "<MinimumKorotVersion>" + MininmumKorotVersion.ToString() + "</MinimumKorotVersion>" + Environment.NewLine +
                "<UseHaltroyUpdate>" + (UseHaltroyUpdate ? "true" : "false") + "</UseHaltroyUpdate>" + Environment.NewLine +
            "<Name>" + Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Name>" + Environment.NewLine +
            "<Author>" + Author.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Author>" + Environment.NewLine +
            "<BackColor>" + HTAlt.Tools.ColorToHex(BackColor) + "</BackColor>" + Environment.NewLine +
            (AutoForeColor ? "" : ("<ForeColor>" + HTAlt.Tools.ColorToHex(ForeColor) + "</ForeColor>" + Environment.NewLine)) +
            "<OverlayColor>" + HTAlt.Tools.ColorToHex(OverlayColor) + "</OverlayColor>" + Environment.NewLine +
            "<BackgroundStyle Layout=\"" + BackgroundStyleLayout + "\">" + BackgroundStyle.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</BackgroundStyle>" + Environment.NewLine +
            "<NewTabColor>" + (int)NewTabColor + "</NewTabColor>" + Environment.NewLine +
            "<CloseButtonColor>" + (int)CloseButtonColor + "</CloseButtonColor>" + Environment.NewLine +
            "</Theme>";
            HTAlt.Tools.WriteFile(ThemeFile, x, Encoding.Unicode);
        }

        public void LoadFromFile(string themeFile)
        {
            if (string.IsNullOrWhiteSpace(themeFile))
            {
                Name = "Korot Light";
                ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Light.ktf";
                Author = "Haltroy";
                UseHaltroyUpdate = false;
                Version = new Version(Application.ProductVersion);
                MininmumKorotVersion = Version;
                BackColor = Color.FromArgb(255, 255, 255, 255);
                OverlayColor = Color.FromArgb(255, 85, 180, 212);
                BackgroundStyle = "BACKCOLOR";
                BackgroundStyleLayout = 0;
                NewTabColor = TabColors.OverlayColor;
                CloseButtonColor = TabColors.OverlayColor;
                LoadedDefaults = true;
                return;
            }
            ThemeFile = themeFile;
            string ManifestXML = HTAlt.Tools.ReadFile(ThemeFile, Encoding.Unicode);
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
                else if (node.Name.ToLower() == "forecolor")
                {
                    ForeColor = HTAlt.Tools.HexToColor(node.InnerText);
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
            if (ForeColor == Color.Empty || ForeColor == null)
            {
                AutoForeColor = true;
                ForeColor = HTAlt.Tools.AutoWhiteBlack(BackColor);
            }
            else
            {
                AutoForeColor = false;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Theme theme &&
                   EqualityComparer<Color>.Default.Equals(BackColor, theme.BackColor) &&
                   EqualityComparer<Color>.Default.Equals(ForeColor, theme.ForeColor) &&
                   EqualityComparer<Color>.Default.Equals(OverlayColor, theme.OverlayColor) &&
                   BackgroundStyle == theme.BackgroundStyle &&
                   BackgroundStyleLayout == theme.BackgroundStyleLayout &&
                   NewTabColor == theme.NewTabColor &&
                   CloseButtonColor == theme.CloseButtonColor;
        }

        public override int GetHashCode()
        {
            int hashCode = -648445378;
            hashCode = hashCode * -1521134295 + AutoForeColor.GetHashCode();
            hashCode = hashCode * -1521134295 + LoadedDefaults.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(Version);
            hashCode = hashCode * -1521134295 + UseHaltroyUpdate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Version>.Default.GetHashCode(MininmumKorotVersion);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + BackColor.GetHashCode();
            hashCode = hashCode * -1521134295 + ForeColor.GetHashCode();
            hashCode = hashCode * -1521134295 + OverlayColor.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThemeFile);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BackgroundStyle);
            hashCode = hashCode * -1521134295 + BackgroundStyleLayout.GetHashCode();
            hashCode = hashCode * -1521134295 + NewTabColor.GetHashCode();
            hashCode = hashCode * -1521134295 + CloseButtonColor.GetHashCode();
            return hashCode;
        }

        public bool AutoForeColor { get; set; }

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
        public Color ForeColor { get; set; }
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
        Error,
        Downloading
    }

    public class FavoritesSettings
    {
        public void DeleteFolder(Folder folder)
        {
            if (folder == null) { return; }
            if (folder.IsTopFavorite)
            {
                Favorites.Remove(folder);
            }
            else
            {
                if (folder is Favorite)
                {
                    folder.ParentFolder.Favorites.Remove(folder);
                }
                else
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
                string x = "   <Favorites Show=\"" + (ShowFavorites ? "true" : "false") + "\">" + Environment.NewLine;
                foreach (Folder y in Favorites)
                {
                    x += y.outXml + Environment.NewLine;
                }
                x += "   </Favorites>" + Environment.NewLine;
                return x;
            }
        }

        private void RecursiveFWNF(Folder folder, List<Favorite> list)
        {
            foreach (Folder x in folder.Favorites)
            {
                if (x is Favorite)
                {
                    list.Add(x as Favorite);
                }
                else
                {
                    RecursiveFWNF(x, list);
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
                    }
                    else
                    {
                        RecursiveFWNF(x, fav);
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
        private List<Folder> _Fav = new List<Folder>();
        public Folder ParentFolder { get; set; }
        public bool IsTopFavorite => ParentFolder == null;
        public string Name { get; set; }
        public string Text { get; set; }
        public List<Folder> Favorites { get => _Fav; set => _Fav = value; }

        public string outXml
        {
            get
            {
                bool isNotFolder = (this is Favorite);
                string x = "<" + (isNotFolder ? "Favorite" : "Folder") + " Name=\"" + Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Text=\"" + Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\"";
                if (isNotFolder)
                {
                    Favorite favorite = this as Favorite;
                    x += " Url=\"" + favorite.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" IconPath=\"" + favorite.IconPath.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />";
                }
                else
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
        public Image Icon => HTAlt.Tools.ReadFile(IconPath, "ignored");
    }

    public class FileFolderError
    {
        public FileFolderError(string _Location, Exception _Error, bool IsDirectory)
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
            }
            else
            {
                return item.Text.Replace("[NEWLINE]", Environment.NewLine).Replace("[VERSION]", Application.ProductVersion.ToString()).Replace("[CODENAME]", VersionInfo.CodeName);
            }
        }

        private string _LangFile = Application.StartupPath + "\\Lang\\English.klf";
        public int ItemCount => LanguageItems.Count;
        public string LangFile => _LangFile;

        public LanguageSystem(string fileLoc)
        {
            ReadFromFile(!string.IsNullOrWhiteSpace(fileLoc) ? fileLoc : (Application.StartupPath + "\\Lang\\English.klf"), true);
        }

        public void ForceReadFromFile(string fileLoc, bool clear = true)
        {
            _LangFile = fileLoc;
            string code = HTAlt.Tools.ReadFile(fileLoc, Encoding.Unicode);
            ReadCode(code, clear);
        }

        public void ReadFromFile(string fileLoc, bool clear = true)
        {
            if (_LangFile != fileLoc || LanguageItems.Count == 0)
            {
                ForceReadFromFile(fileLoc, clear);
            }
        }

        public void ReadCode(string xmlCode, bool clear = true)
        {
            if (clear) { LanguageItems.Clear(); }
            XmlDocument document = new XmlDocument();
            document.LoadXml(xmlCode);
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name == "Translate")
                {
                    string id = node.Attributes["ID"] != null ? node.Attributes["ID"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"") : HTAlt.Tools.GenerateRandomText(12);
                    string text = node.Attributes["Text"] != null ? node.Attributes["Text"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"") : id;
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

    public class NewTabSites
    {
        public string XMLOut => "<NewTabMenu>" + Environment.NewLine +
                   (FavoritedSite0 != null ? "<Attached0 Name=\"" + FavoritedSite0.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite0.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite1 != null ? "<Attached1 Name=\"" + FavoritedSite1.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite1.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite2 != null ? "<Attached2 Name=\"" + FavoritedSite2.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite2.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite3 != null ? "<Attached3 Name=\"" + FavoritedSite3.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite3.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite4 != null ? "<Attached4 Name=\"" + FavoritedSite4.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite4.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite5 != null ? "<Attached5 Name=\"" + FavoritedSite5.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite5.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite6 != null ? "<Attached6 Name=\"" + FavoritedSite6.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite6.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite7 != null ? "<Attached7 Name=\"" + FavoritedSite7.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite7.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite8 != null ? "<Attached8 Name=\"" + FavoritedSite8.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite8.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                   (FavoritedSite9 != null ? "<Attached9 Name=\"" + FavoritedSite9.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + FavoritedSite9.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine : "") +
                    "</NewTabMenu>";

        public NewTabSites(string xmlCode)
        {
            if (string.IsNullOrWhiteSpace(xmlCode))
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(xmlCode))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xmlCode);
                foreach (XmlNode node in document.FirstChild.ChildNodes)
                {
                    if (node.Name.ToLower() == "attached0")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite0 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached1")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite1 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached2")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite2 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached3")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite3 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached4")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite4 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached5")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite5 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached6")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite6 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached7")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite7 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached8")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite8 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                    else if (node.Name.ToLower() == "attached9")
                    {
                        if (node.Attributes["Name"] == null || node.Attributes["Url"] == null) { return; }
                        FavoritedSite9 = new Site
                        {
                            Name = node.Attributes["Name"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\""),
                            Url = node.Attributes["Url"].Value.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'").Replace("&quot;", "\"")
                        };
                    }
                }
            }
        }

        public string SiteToHTMLData(Site site)
        {
            string x = "<a href=\"" + site.Url + "\" style=\"§BACKSTYLE3§\">" + site.Name + "</a>" +
    "</br>" +
   "<a href=\"" + site.Url + "\" style=\"§BACKSTYLE3§font-size: small;\">" + site.Url.Substring(0, 10) + "</a>";
            return x;
        }

        public Site FavoritedSite0 { get; set; }
        public Site FavoritedSite1 { get; set; }
        public Site FavoritedSite2 { get; set; }
        public Site FavoritedSite3 { get; set; }
        public Site FavoritedSite4 { get; set; }
        public Site FavoritedSite5 { get; set; }
        public Site FavoritedSite6 { get; set; }
        public Site FavoritedSite7 { get; set; }
        public Site FavoritedSite8 { get; set; }
        public Site FavoritedSite9 { get; set; }
    }

    public class Proxy
    {
        public string ID { get; set; }
        public string Address { get; set; }
        public Exception Exception { get; set; }
    }

    public class BlockSite
    {
        public string Address { get; set; }
        public string Filter { get; set; }
        public int BlockLevel { get; set; }
    }

    public class KorotTools
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

            switch (fullName)
            {
                case "XP":
                    return "NT 5.1";

                case "Vista":
                    return "NT 6.0";

                case "7":
                    return "NT 6.1";

                default:
                    return "NT " + fullName;
            }
        }

        public static bool createThemes()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Light.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Light</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#ffffff</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Light.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Dark.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Dark</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#000000</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Dark.ktf", newTheme, Encoding.Unicode);
            }
            //0700
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Midnight.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Midnight</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#050024</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Midnight.ktf", newTheme, Encoding.Unicode);
            }
            //0800
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Emerald.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Emerald</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#78ff95</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Emerald.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot DarkLeaf.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot DarkLeaf</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#005212</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot DarkLeaf.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Strawberry.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Strawberry</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#ffabab</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Strawberry.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Crimson.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Crimson</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#4a0000</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Crimson.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Sunrise.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Sunrise</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#bfffff</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Sunrise.ktf", newTheme, Encoding.Unicode);
            }
            //0810
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Red.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Red</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#ff0000</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Red.ktf", newTheme, Encoding.Unicode);
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Blue.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Blue</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#0000ff</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Blue.ktf", newTheme, Encoding.Unicode);
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Green.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Green</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#00ff00</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Green.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Gray.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Gray</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#808080</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Gray.ktf", newTheme, Encoding.Unicode);
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Shadow.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot SHadow</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#202020</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Shadow.ktf", newTheme, Encoding.Unicode);
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Cement.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Cement</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#C0C0C0</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Cement.ktf", newTheme, Encoding.Unicode);
            }

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot DodgerBlue.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot DodgerBlue</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#47BFFF</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot DodgerBlue.ktf", newTheme, Encoding.Unicode);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Avocado.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Avocado</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#9ed99c</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Avocado.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Teal.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Teal</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#008080</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Teal.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Yellow.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Yellow</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#ffff00</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Yellow.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Orange.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Orange</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#ff8000</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Orange.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Brown.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Brown</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#A0522D</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Brown.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Leather.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Leather</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#D2691E</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Leather.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Gold.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Gold</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#DAA520</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Gold.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Creme.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Creme</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#FFF8DC</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Creme.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Purple.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Purple</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#4d004d</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Purple.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Raspberry.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Raspberry</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#ff99ff</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Raspberry.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Lavender.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Lavender</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#800080</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Lavender.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Fuchsia.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Fuchsia</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#ff00ff</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Fuchsia.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Pink.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Pink</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#ff4dff</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Pink.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Brick.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Brick</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#A52A2A</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Brick.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot DarkBlue.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot DarkBlue</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#000066</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot DarkBlue.ktf", newTheme, Encoding.Unicode); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Sea.ktf")) { string newTheme = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine + "<Name>Korot Sea</Name>" + Environment.NewLine + "<Author>Haltroy</Author>" + Environment.NewLine + "<Version>1.0.0.0</Version>" + Environment.NewLine + "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine + "<BackColor>#4da6ff</BackColor>" + Environment.NewLine + "<OverlayColor>#47BFFF</OverlayColor>" + Environment.NewLine + "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine + "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine + "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine + "</Theme>" + Environment.NewLine; HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Sea.ktf", newTheme, Encoding.Unicode); }
            return true;
        }

        public static long GetDirectorySize(string p)
        {
            // 1.
            // Get array of all file names.
            string[] a = Directory.GetFiles(p, "*.*");

            // 2.
            // Calculate total bytes of all files in a loop.
            long b = 0;
            foreach (string name in a)
            {
                // 3.
                // Use FileInfo to get length of each file.
                FileInfo info = new FileInfo(name);
                b += info.Length;
            }
            // 4.
            // Return total size
            return b;
        }

        public static bool isNonRedirectKorotPage(string Url)
        {
            return (Url.ToLower().StartsWith("korot://newtab")
                || Url.ToLower().StartsWith("korot://links")
                || Url.ToLower().StartsWith("korot://license")
                || Url.ToLower().StartsWith("korot://incognito")
                || Url.ToLower().StartsWith("korot://technical"));
        }

        public static bool createFolders()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Extensions\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Logs\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\IconStorage\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\IconStorage\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Scripts\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Scripts\\"); }
            return true;
        }

        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^((http(s)?|korot|file|pop|smtp|ftp|chrome|about):(\/\/)?)|(^([\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$))|(.{1,4}\:.{1,4}\:.{1,4}\:.{1,4}\:.{1,4}\:.{1,4}\:.{1,4}\:.{1,4})";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s);
        }

        public static string GetUserAgent()
        {
            return "Mozilla/5.0 ( Windows "
                + KorotTools.getOSInfo()
                + "; "
                + (Environment.Is64BitProcess ? "Win64" : "Win32NT") + "; " + (Environment.Is64BitProcess ? "x64" : "x86")
                + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/"
                + Cef.ChromiumVersion
                + " Safari/537.36 Korot/"
                + Application.ProductVersion.ToString()
                + " [" + VersionInfo.CodeName + "]";
        }

        public static bool FixDefaultLanguage()
        {
            if (!Directory.Exists(Application.StartupPath + "\\Lang\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
            }
            HTAlt.Tools.WriteFile(Application.StartupPath + "\\Lang\\English.klf", Properties.Resources.English);
            Tools.WriteFile(Application.StartupPath + "\\Lang\\Türkçe.klf", Properties.Resources.Türkçe);
            return true;
        }

       
    }

    public class KorotVersion
    {
        public KorotVersion() : this("<HaltroyUpdate><AppName>Korot</AppName><AppVersion>" + Application.ProductVersion.ToString() + "</AppVersion><AppVersionNo>" + VersionInfo.VersionNumber + "</AppVersionNo><MinimumNo>48</MinimumNo><InstallNo>47</InstallNo><AppInstallerUrl>http://bit.ly/KorotSetup</AppInstallerUrl><Architectures><Architecture Type=\"amd64\" VersionNo=\"" + VersionInfo.VersionNumber + "\" Upgrade=\"korot://empty\" FullUpgrade=\"korot://empty\" /><Architecture Type=\"i86\" VersionNo=\"" + VersionInfo.VersionNumber + "\" Upgrade=\"korot://empty\" FullUpgrade=\"korot://empty\" /></Architectures></HaltroyUpdate>")
        {
        }

        public KorotVersion(string XMLCode)
        {
            XmlDocument doc = new XmlDocument();
            Archs = new List<Architecture>();
            AppName = Application.ProductName;
            Version = Application.ProductVersion.ToString();
            VersionNumber = VersionInfo.VersionNumber;
            UpdateMinVer = VersionNumber - 1;
            InstallVer = VersionNumber - 2;
            InstallerUrl = "http://bit.ly/KorotSetup";
            if (!string.IsNullOrWhiteSpace(XMLCode))
            {
                doc.LoadXml(XMLCode);
                foreach (XmlNode node in doc.FirstChild.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "AppName":
                            AppName = node.InnerText;
                            break;

                        case "AppVersion":
                            Version = node.InnerText;
                            break;

                        case "AppVersionNo":
                            VersionNumber = Convert.ToInt32(node.InnerText);
                            break;

                        case "MinimumNo":
                            UpdateMinVer = Convert.ToInt32(node.InnerText);
                            break;

                        case "InstallNo":
                            InstallVer = Convert.ToInt32(node.InnerText);
                            break;

                        case "AppInstallerUrl":
                            InstallerUrl = node.InnerText;
                            break;

                        case "Architectures":
                            {
                                foreach (XmlNode subnode in node.ChildNodes)
                                {
                                    if (subnode.Name == "Architecture")
                                    {
                                        if (subnode.Attributes["Type"] != null && subnode.Attributes["VersionNo"] != null && subnode.Attributes["Upgrade"] != null && subnode.Attributes["FullUpgrade"] != null)
                                        {
                                            Architecture arch = new Architecture()
                                            {
                                                Type = subnode.Attributes["Type"].Value,
                                                VersionNo = Convert.ToInt32(subnode.Attributes["VersionNo"].Value),
                                                FullUpdate = subnode.Attributes["FullUpgrade"].Value,
                                                Update = subnode.Attributes["Upgrade"].Value
                                            };
                                            Archs.Add(arch);
                                        }
                                    }
                                }

                                break;
                            }
                    }
                }
            }
            if (Archs.Count == 0)
            {
                Archs.Add(new Architecture() { Type = Environment.Is64BitProcess ? "amd64" : "i86", VersionNo = VersionNumber, FullUpdate = "korot://empty", Update = "korot://empty" });
            }
        }

        public List<Architecture> Archs { get; set; }
        public string AppName { get; set; }
        public string Version { get; set; }
        public int VersionNumber { get; set; }
        public int UpdateMinVer { get; set; }
        public int InstallVer { get; set; }
        public string InstallerUrl { get; set; }

        public enum UpdateType
        {
            UpToDate,
            Upgrade,
            FullUpgrade,
            Installer
        }

        public UpdateType GetUpdateType(KorotVersion version)
        {
            if (version != null)
            {
                if (WhicIsNew(version) != this)
                {
                    if (VersionNumber < version.UpdateMinVer)
                    {
                        if (VersionNumber < version.InstallVer)
                        {
                            return UpdateType.Installer;
                        }
                        else
                        {
                            return UpdateType.FullUpgrade;
                        }
                    }
                    else
                    {
                        return UpdateType.Upgrade;
                    }
                }
                else
                {
                    return UpdateType.UpToDate;
                }
            }
            else
            {
                throw new NullReferenceException("version was null.");
            }
        }

        public KorotVersion WhicIsNew(KorotVersion version, string Arch)
        {
            if (version != null && !string.IsNullOrWhiteSpace(Arch))
            {
                Architecture arch = Archs.Find(i => i.Type == Arch);
                Architecture Narch = version.Archs.Find(i => i.Type == Arch);
                if (arch != null && Narch != null)
                {
                    if (VersionNumber >= version.VersionNumber)
                    {
                        if (arch.VersionNo >= Narch.VersionNo)
                        {
                            return this;
                        }
                        else
                        {
                            return version;
                        }
                    }
                    return version;
                }
                else
                {
                    throw new NullReferenceException("Cannot find specific architectures for both versions.");
                }
            }
            else
            {
                throw new ArgumentException("version or Arch is empty or null.");
            }
        }

        public KorotVersion WhicIsNew(KorotVersion version)
        {
            if (version != null)
            {
                if (version.VersionNumber <= VersionNumber)
                {
                    return this;
                }
                return version;
            }
            else
            {
                throw new ArgumentException("version was null.");
            }
        }

        public class Architecture
        {
            public string Type { get; set; }
            public string FullUpdate { get; set; }
            public string Update { get; set; }
            public int VersionNo { get; set; }
        }
    }

    public class SessionSystem
    {
        public SessionSystem(string XMLCode)
        {
            if (!string.IsNullOrWhiteSpace(XMLCode))
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(XMLCode);
                XmlNode workNode = document.FirstChild;
                if (document.FirstChild.Name.ToLower() == "xml") { workNode = document.FirstChild.NextSibling; }
                if (workNode.Attributes["Index"] != null)
                {
                    int si = Convert.ToInt32(workNode.Attributes["Index"].Value);
                    foreach (XmlNode node in workNode.ChildNodes)
                    {
                        if (node.Name.ToLower() == "sessionsite")
                        {
                            if (node.Attributes["Url"] != null && node.Attributes["Title"] != null)
                            {
                                Sessions.Add(new Session(node.Attributes["Url"].Value, node.Attributes["Tİtle"].Value));
                            }
                        }
                    }
                    SelectedIndex = si;
                    SelectedSession = Sessions[si];
                }
            }
        }

        public SessionSystem() : this("")
        {
        }

        private List<Session> _Sessions = new List<Session>();

        public string XmlOut()
        {
            string x = "<Session Index=\"" + SelectedIndex + "\" >" + Environment.NewLine;
            for (int i = 0; i < Sessions.Count; i++)
            {
                x += "<SessionSite Url=\"" + Sessions[i].Url + "\" Title=\"" + Sessions[i].Title + "\" >" + Environment.NewLine;
            }
            return x + "</Session>";
        }

        public List<Session> Sessions
        {
            get => _Sessions;
            set => _Sessions = value;
        }

        public bool SkipAdd = false;

        public void GoBack(ChromiumWebBrowser browser)
        {
            if (CanGoBack())
            {
                SkipAdd = true;
                SelectedIndex -= 1;
                SelectedSession = Sessions[SelectedIndex];
                browser.Invoke(new Action(() => browser.Load(SelectedSession.Url)));
            }
        }

        public void GoForward(ChromiumWebBrowser browser)
        {
            if (CanGoForward())
            {
                SkipAdd = true;
                SelectedIndex += 1;
                SelectedSession = Sessions[SelectedIndex];
                browser.Invoke(new Action(() => browser.Load(SelectedSession.Url)));
            }
        }

        public Session SessionInIndex(int Index)
        {
            return Sessions[Index];
        }

        public Session SelectedSession { get; set; }
        public int SelectedIndex { get; set; }

        public void MoveTo(int i, ChromiumWebBrowser browser)
        {
            if (browser is null)
            {
                throw new ArgumentNullException("\"browser\" was null");
            }
            if (i >= 0 && i < Sessions.Count)
            {
                SkipAdd = true;
                SelectedIndex = i;
                SelectedSession = Sessions[i];
                browser.Load(SelectedSession.Url);
            }
            else
            {
                throw new ArgumentOutOfRangeException("\"i\" was bigger than Sessions.Count or smaller than 0. [i=\"" + i + "\" Count=\"" + Sessions.Count + "\"]");
            }
        }

        public void Add(string url, string title)
        {
            Add(new Session(url, title));
        }

        public void Add(Session Session)
        {
            if (Session is null)
            {
                throw new ArgumentNullException("\"Session\" was null.");
            }
            if (Session.Url.ToLower().StartsWith("korot") && (!KorotTools.isNonRedirectKorotPage(Session.Url)))
            {
                return;
            }
            if (SkipAdd) { SkipAdd = false; return; }
            if (CanGoForward() && SelectedIndex + 1 < Sessions.Count)
            {
                if (!Session.Equals(Sessions[SelectedIndex]))
                {
                    Console.WriteLine("Session Not Equal: 1:" + Session.Url + " 2:" + Sessions[SelectedIndex].Url);
                    Session[] RemoveThese = After();
                    for (int i = 0; i < RemoveThese.Length; i++)
                    {
                        Sessions.Remove(RemoveThese[i]);
                    }
                    if (Sessions.Count > 0)
                    {
                        if (Sessions[Sessions.Count - 1].Url != Session.Url)
                        {
                            Sessions.Add(Session);
                        }
                    }
                    else
                    {
                        Sessions.Add(Session);
                    }
                }
            }
            else
            {
                if (Sessions.Count > 0)
                {
                    if (Sessions[Sessions.Count - 1].Url != Session.Url)
                    {
                        Sessions.Add(Session);
                    }
                }
                else
                {
                    Sessions.Add(Session);
                }
            }
            if (Sessions.Count > 0)
            {
                if (Sessions[Sessions.Count - 1].Url != Session.Url)
                {
                    SelectedSession = Session;
                    SelectedIndex = Sessions.IndexOf(Session);
                }
                else
                {
                    SelectedSession = Sessions[Sessions.Count - 1];
                    SelectedIndex = Sessions.Count - 1;
                }
            }
            else
            {
                Sessions.Add(Session);
            }
        }

        public bool CanGoBack()
        {
            return CanGoBack(SelectedSession);
        }

        public bool CanGoBack(Session Session)
        {
            if (Session is null)
            {
                return false;
            }
            if (!Sessions.Contains(Session))
            {
                throw new ArgumentOutOfRangeException("Cannot find Session[Url=\"" + (Session.Url == null ? "null" : Session.Url) + "\" Title=\"" + (Session.Title == null ? "null" : Session.Title) + "\"].");
            }
            int current = Sessions.IndexOf(Session);
            return current > 0;
        }

        public bool CanGoForward()
        {
            return CanGoForward(SelectedSession);
        }

        public bool CanGoForward(Session Session)
        {
            if (Session is null)
            {
                return false;
            }
            if (!Sessions.Contains(Session))
            {
                throw new ArgumentOutOfRangeException("Cannot find Session[Url=\"" + (Session.Url == null ? "null" : Session.Url) + "\" Title=\"" + (Session.Title == null ? "null" : Session.Title) + "\"].");
            }

            int current = Sessions.IndexOf(Session) + 1;
            return current < Sessions.Count;
        }

        public Session[] Before()
        {
            return Before(SelectedSession);
        }

        public Session[] Before(Session Session)
        {
            if (Session is null)
            {
                return new Session[] { };
            }
            if (!Sessions.Contains(Session))
            {
                throw new ArgumentOutOfRangeException("Cannot find Session[Url=\"" + (Session.Url == null ? "null" : Session.Url) + "\" Title=\"" + (Session.Title == null ? "null" : Session.Title) + "\"].");
            }
            int current = Sessions.IndexOf(Session);
            List<Session> fs = new List<Session>();
            for (int i = 0; i < current; i++)
            {
                fs.Add(Sessions[i]);
            }
            return fs.ToArray();
        }

        public Session[] After()
        {
            return After(SelectedSession);
        }

        public Session[] After(Session Session)
        {
            if (Session is null)
            {
                return new Session[] { };
            }
            if (!Sessions.Contains(Session))
            {
                throw new ArgumentOutOfRangeException("Cannot find Session[Url=\"" + (Session.Url == null ? "null" : Session.Url) + "\" Title=\"" + (Session.Title == null ? "null" : Session.Title) + "\"].");
            }
            int current = Sessions.IndexOf(Session) + 1;
            List<Session> fs = new List<Session>();
            for (int i = current; i < Sessions.Count; i++)
            {
                fs.Add(Sessions[i]);
            }
            return fs.ToArray();
        }
    }

    public class Session
    {
        public override bool Equals(object obj)
        {
            return obj is Session session && Url == session.Url;
        }

        public override int GetHashCode()
        {
            return -1915121810 + EqualityComparer<string>.Default.GetHashCode(Url);
        }

        public Session(string _Url, string _Title)
        {
            Url = _Url;
            Title = _Title;
        }

        public Session() : this("", "")
        {
        }

        public Session(string _Url) : this(_Url, _Url)
        {
        }

        public string Url { get; set; }
        public string Title { get; set; }
    }

    public class AutoCleaner
    {
        public AutoCleaner(string XMLCode)
        {
            //Defaults
            CleanCache = false;
            CleanDownloads = false;
            CleanHistory = false;
            CleanLogs = false;
            CleanCacheFile = false;
            CacheFileSize = 5;
            CleanCacheDaily = false;
            CleanCacheDay = 30;
            LatestCacheCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            CleanHistoryDaily = false;
            CleanHistoryDay = 30;
            CleanHistoryFile = false;
            HistoryFileSize = 5;
            CleanOldHistory = false;
            OldHistoryDay = 30;
            LatestHistoryCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            CleanLogsDaily = false;
            CleanLogsDay = 30;
            CleanLogsFile = false;
            LogsFileSize = 5;
            CleanOldLogs = false;
            OldLogsDay = 30;
            LatestLogsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            CleanDownloadsDaily = false;
            CleanDownloadsDay = 30;
            CleanDownloadsFile = false;
            DownloadsFileSize = 5;
            CleanOldDownloads = false;
            OldDownloadsDay = 30;
            LatestDownloadsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            //Load XML
            if (!string.IsNullOrWhiteSpace(XMLCode))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLCode);
                foreach (XmlNode node in doc.FirstChild.ChildNodes)
                {
                    if (node.Name == "Cache")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null)
                        {
                            CleanCache = node.Attributes["Clean"].Value == "true";
                            CleanCacheFile = node.Attributes["FileSize"].Value == "true";
                            CacheFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanCacheDaily = node.Attributes["Daily"].Value == "true";
                            CleanCacheDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            LatestCacheCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                    else if (node.Name == "Logs")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null && node.Attributes["Old"] != null && node.Attributes["OldDay"] != null)
                        {
                            CleanLogs = node.Attributes["Clean"].Value == "true";
                            CleanLogsFile = node.Attributes["FileSize"].Value == "true";
                            LogsFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanLogsDaily = node.Attributes["Daily"].Value == "true";
                            CleanLogsDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            CleanOldLogs = node.Attributes["Old"].Value == "true";
                            OldLogsDay = Convert.ToInt32(node.Attributes["OldDays"].Value);
                            LatestLogsCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                    else if (node.Name == "History")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null && node.Attributes["Old"] != null && node.Attributes["OldDay"] != null)
                        {
                            CleanHistory = node.Attributes["Clean"].Value == "true";
                            CleanHistoryFile = node.Attributes["FileSize"].Value == "true";
                            HistoryFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanHistoryDaily = node.Attributes["Daily"].Value == "true";
                            CleanHistoryDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            CleanOldHistory = node.Attributes["Old"].Value == "true";
                            OldHistoryDay = Convert.ToInt32(node.Attributes["OldDays"].Value);
                            LatestHistoryCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                    else if (node.Name == "Downloads")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null && node.Attributes["Old"] != null && node.Attributes["OldDay"] != null)
                        {
                            CleanDownloads = node.Attributes["Clean"].Value == "true";
                            CleanDownloadsFile = node.Attributes["FileSize"].Value == "true";
                            DownloadsFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanDownloadsDaily = node.Attributes["Daily"].Value == "true";
                            CleanDownloadsDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            CleanOldDownloads = node.Attributes["Old"].Value == "true";
                            OldDownloadsDay = Convert.ToInt32(node.Attributes["OldDays"].Value);
                            LatestDownloadsCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                }
            }
        }

        public void LoadFromXML(string XMLCode)
        {
            //Defaults
            CleanCache = false;
            CleanDownloads = false;
            CleanHistory = false;
            CleanLogs = false;
            CleanCacheFile = false;
            CacheFileSize = 5;
            CleanCacheDaily = false;
            CleanCacheDay = 30;
            LatestCacheCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            CleanHistoryDaily = false;
            CleanHistoryDay = 30;
            CleanHistoryFile = false;
            HistoryFileSize = 5;
            CleanOldHistory = false;
            OldHistoryDay = 30;
            LatestHistoryCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            CleanLogsDaily = false;
            CleanLogsDay = 30;
            CleanLogsFile = false;
            LogsFileSize = 5;
            CleanOldLogs = false;
            OldLogsDay = 30;
            LatestLogsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            CleanDownloadsDaily = false;
            CleanDownloadsDay = 30;
            CleanDownloadsFile = false;
            DownloadsFileSize = 5;
            CleanOldDownloads = false;
            OldDownloadsDay = 30;
            LatestDownloadsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            //Load XML
            if (!string.IsNullOrWhiteSpace(XMLCode))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLCode);
                foreach (XmlNode node in doc.FirstChild.ChildNodes)
                {
                    if (node.Name == "Cache")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null)
                        {
                            CleanCache = node.Attributes["Clean"].Value == "true";
                            CleanCacheFile = node.Attributes["FileSize"].Value == "true";
                            CacheFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanCacheDaily = node.Attributes["Daily"].Value == "true";
                            CleanCacheDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            LatestCacheCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                    else if (node.Name == "Logs")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null && node.Attributes["Old"] != null && node.Attributes["OldDay"] != null)
                        {
                            CleanLogs = node.Attributes["Clean"].Value == "true";
                            CleanLogsFile = node.Attributes["FileSize"].Value == "true";
                            LogsFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanLogsDaily = node.Attributes["Daily"].Value == "true";
                            CleanLogsDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            CleanOldLogs = node.Attributes["Old"].Value == "true";
                            OldLogsDay = Convert.ToInt32(node.Attributes["OldDays"].Value);
                            LatestLogsCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                    else if (node.Name == "History")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null && node.Attributes["Old"] != null && node.Attributes["OldDay"] != null)
                        {
                            CleanHistory = node.Attributes["Clean"].Value == "true";
                            CleanHistoryFile = node.Attributes["FileSize"].Value == "true";
                            HistoryFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanHistoryDaily = node.Attributes["Daily"].Value == "true";
                            CleanHistoryDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            CleanOldHistory = node.Attributes["Old"].Value == "true";
                            OldHistoryDay = Convert.ToInt32(node.Attributes["OldDays"].Value);
                            LatestHistoryCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                    else if (node.Name == "Downloads")
                    {
                        if (node.Attributes["Clean"] != null && node.Attributes["FileSize"] != null && node.Attributes["Size"] != null && node.Attributes["Daily"] != null && node.Attributes["Day"] != null && node.Attributes["Latest"] != null && node.Attributes["Old"] != null && node.Attributes["OldDay"] != null)
                        {
                            CleanDownloads = node.Attributes["Clean"].Value == "true";
                            CleanDownloadsFile = node.Attributes["FileSize"].Value == "true";
                            DownloadsFileSize = Convert.ToInt32(node.Attributes["Size"].Value);
                            CleanDownloadsDaily = node.Attributes["Daily"].Value == "true";
                            CleanDownloadsDay = Convert.ToInt32(node.Attributes["Day"].Value);
                            CleanOldDownloads = node.Attributes["Old"].Value == "true";
                            OldDownloadsDay = Convert.ToInt32(node.Attributes["OldDays"].Value);
                            LatestDownloadsCleanup = node.Attributes["Latest"].Value;
                        }
                    }
                }
            }
        }

        public string XMLOut()
        {
            return "<AutoUpdate>" + Environment.NewLine +
                "<Cache Clean=\"" + (CleanCache ? "true" : "false") + "\" FileSize=\"" + (CleanCacheFile ? "true" : "false") + "\" Size=\"" + CacheFileSize + "\" Daily=\"" + (CleanCacheDaily ? "true" : "false") + "\" Day=\"" + CleanCacheDay + "\" Latest=\"" + LatestCacheCleanup + "\" />" + Environment.NewLine +
                "<History Clean=\"" + (CleanHistory ? "true" : "false") + "\" FileSize=\"" + (CleanHistoryFile ? "true" : "false") + "\" Size=\"" + HistoryFileSize + "\" Daily=\"" + (CleanHistoryDaily ? "true" : "false") + "\" Day=\"" + CleanHistoryDay + "\" Latest=\"" + LatestHistoryCleanup + "\" Old=\"" + (CleanOldHistory ? "true" : "false") + "\" OldDay=\"" + OldHistoryDay + "\" />" + Environment.NewLine +
                "<Downloads Clean=\"" + (CleanDownloads ? "true" : "false") + "\" FileSize=\"" + (CleanDownloadsFile ? "true" : "false") + "\" Size=\"" + DownloadsFileSize + "\" Daily=\"" + (CleanDownloadsDaily ? "true" : "false") + "\" Day=\"" + CleanDownloadsDay + "\" Latest=\"" + LatestDownloadsCleanup + "\" Old=\"" + (CleanOldDownloads ? "true" : "false") + "\" OldDay=\"" + OldDownloadsDay + "\" />" + Environment.NewLine +
                "<Logs Clean=\"" + (CleanLogs ? "true" : "false") + "\" FileSize=\"" + (CleanLogsFile ? "true" : "false") + "\" Size=\"" + LogsFileSize + "\" Daily=\"" + (CleanLogsDaily ? "true" : "false") + "\" Day=\"" + CleanLogsDay + "\" Latest=\"" + LatestLogsCleanup + "\" Old=\"" + (CleanOldLogs ? "true" : "false") + "\" OldDay=\"" + OldLogsDay + "\" />" + Environment.NewLine +
                "</AutoUpdate>";
        }

        public Settings Settings;
        private readonly string DirPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\";
        public bool CleanCache { get; set; }
        public bool CleanDownloads { get; set; }
        public bool CleanHistory { get; set; }
        public bool CleanLogs { get; set; }

        public void DoForceCleanup()
        {
            Directory.Delete(DirPath + "cache\\", true);
            LatestCacheCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            string[] files = Directory.GetFiles(DirPath + "Logs\\", "*.txt", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                if (LogsSiteIsOld(files[i])) { File.Delete(files[i]); }
            }
            LatestLogsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            for (int i = 0; i < Settings.Downloads.Downloads.Count; i++)
            {
                if (DownloadsSiteIsOld(Settings.Downloads.Downloads[i])) { Settings.Downloads.Downloads.Remove((Settings.Downloads.Downloads[i])); }
            }
            LatestDownloadsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
            for (int i = 0; i < Settings.History.Count; i++)
            {
                if (HistorySiteIsOld(Settings.History[i])) { Settings.History.Remove((Settings.History[i])); }
            }
            LatestHistoryCleanup = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public void DoCleanup()
        {
            if (CleanCache)
            {
                if (CleanCacheFile)
                {
                    if (KorotTools.GetDirectorySize(DirPath + "cache\\") > (CacheFileSize * 1048576))
                    {
                        Directory.Delete(DirPath + "cache\\", true);
                        LatestCacheCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
                if (CacheCleanToday)
                {
                    Directory.Delete(DirPath + "cache\\", true);
                    LatestCacheCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            if (CleanLogs)
            {
                if (CleanLogsFile)
                {
                    if (KorotTools.GetDirectorySize(DirPath + "Logs\\") > (LogsFileSize * 1048576))
                    {
                        string[] files = Directory.GetFiles(DirPath + "Logs\\", "*.txt", SearchOption.TopDirectoryOnly);
                        for (int i = 0; i < files.Length; i++)
                        {
                            if (LogsSiteIsOld(files[i])) { File.Delete(files[i]); }
                        }
                        LatestLogsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
                if (LogsCleanToday)
                {
                    string[] files = Directory.GetFiles(DirPath + "Logs\\", "*.txt", SearchOption.TopDirectoryOnly);
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (LogsSiteIsOld(files[i])) { File.Delete(files[i]); }
                    }
                    LatestLogsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            if (CleanDownloads)
            {
                if (CleanDownloadsFile)
                {
                    if (KorotTools.GetDirectorySize(DirPath + "Downloads\\") > (DownloadsFileSize * 1048576))
                    {
                        for (int i = 0; i < Settings.Downloads.Downloads.Count; i++)
                        {
                            if (DownloadsSiteIsOld(Settings.Downloads.Downloads[i])) { Settings.Downloads.Downloads.Remove((Settings.Downloads.Downloads[i])); }
                        }
                        LatestDownloadsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
                if (DownloadsCleanToday)
                {
                    for (int i = 0; i < Settings.Downloads.Downloads.Count; i++)
                    {
                        if (DownloadsSiteIsOld(Settings.Downloads.Downloads[i])) { Settings.Downloads.Downloads.Remove((Settings.Downloads.Downloads[i])); }
                    }
                    LatestDownloadsCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
            if (CleanHistory)
            {
                if (CleanHistoryFile)
                {
                    if (KorotTools.GetDirectorySize(DirPath + "History\\") > (HistoryFileSize * 1048576))
                    {
                        for (int i = 0; i < Settings.History.Count; i++)
                        {
                            if (HistorySiteIsOld(Settings.History[i])) { Settings.History.Remove((Settings.History[i])); }
                        }
                        LatestHistoryCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
                if (HistoryCleanToday)
                {
                    for (int i = 0; i < Settings.History.Count; i++)
                    {
                        if (HistorySiteIsOld(Settings.History[i])) { Settings.History.Remove((Settings.History[i])); }
                    }
                    LatestHistoryCleanup = DateTime.Now.ToString("dd/MM/yyyy");
                }
            }
        }

        #region Cache

        public bool CleanCacheFile { get; set; }
        public int CacheFileSize { get; set; }
        public bool CleanCacheDaily { get; set; }
        public int CleanCacheDay { get; set; }
        public string LatestCacheCleanup { get; set; }

        public string NextCacheCleanup
        {
            get
            {
                DateTime.TryParseExact(LatestCacheCleanup, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime latest);
                latest = latest.AddDays(CleanCacheDay);
                return latest.ToString("dd/MM/yy");
            }
        }

        public bool CacheCleanToday => (DateTime.Now.ToString("dd/MM/yy") == NextCacheCleanup) && CleanCache && CleanCacheDaily;

        #endregion Cache

        #region History

        public bool CleanHistoryFile { get; set; }
        public int HistoryFileSize { get; set; }
        public bool CleanHistoryDaily { get; set; }
        public int CleanHistoryDay { get; set; }
        public string LatestHistoryCleanup { get; set; }
        public bool CleanOldHistory { get; set; }
        public int OldHistoryDay { get; set; }

        public bool HistorySiteIsOld(Site Site)
        {
            DateTime.TryParseExact(Site.Date, "dd/MM/yy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime sitedate);
            return DateTime.Now < sitedate.AddDays(CleanOldHistory ? OldHistoryDay : 0);
        }

        public string NextHistoryCleanup
        {
            get
            {
                DateTime.TryParseExact(LatestHistoryCleanup, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime latest);
                latest = latest.AddDays(CleanHistoryDay);
                return latest.ToString("dd/MM/yy");
            }
        }

        public bool HistoryCleanToday => (DateTime.Now.ToString("dd/MM/yy") == NextHistoryCleanup) && CleanHistory;

        #endregion History

        #region Downloads

        public bool CleanDownloadsFile { get; set; }
        public int DownloadsFileSize { get; set; }
        public bool CleanDownloadsDaily { get; set; }
        public int CleanDownloadsDay { get; set; }
        public string LatestDownloadsCleanup { get; set; }
        public bool CleanOldDownloads { get; set; }
        public int OldDownloadsDay { get; set; }

        public bool DownloadsSiteIsOld(Site Site)
        {
            DateTime.TryParseExact(Site.Date, "dd/MM/yy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime sitedate);
            return DateTime.Now < sitedate.AddDays(CleanOldDownloads ? OldDownloadsDay : 0);
        }

        public string NextDownloadsCleanup
        {
            get
            {
                DateTime.TryParseExact(LatestDownloadsCleanup, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime latest);
                latest = latest.AddDays(CleanDownloadsDay);
                return latest.ToString("dd/MM/yy");
            }
        }

        public bool DownloadsCleanToday => (DateTime.Now.ToString("dd/MM/yy") == NextDownloadsCleanup) && CleanDownloads;

        #endregion Downloads

        #region Logs

        public bool CleanLogsFile { get; set; }
        public int LogsFileSize { get; set; }
        public bool CleanLogsDaily { get; set; }
        public int CleanLogsDay { get; set; }
        public string LatestLogsCleanup { get; set; }
        public bool CleanOldLogs { get; set; }
        public int OldLogsDay { get; set; }

        public bool LogsSiteIsOld(string FileName)
        {
            DateTime.TryParseExact(Path.GetFileNameWithoutExtension(FileName), "yyyy-MM-dd-HH-mm-ss", null, System.Globalization.DateTimeStyles.None, out DateTime sitedate);
            return DateTime.Now < sitedate.AddDays(OldLogsDay * -1);
        }

        public string NextLogsCleanup
        {
            get
            {
                DateTime.TryParseExact(LatestLogsCleanup, "dd/MM/yy", null, System.Globalization.DateTimeStyles.None, out DateTime latest);
                latest = latest.AddDays(CleanOldLogs ? OldLogsDay : 0);
                return latest.ToString("dd/MM/yy");
            }
        }

        public bool LogsCleanToday => (DateTime.Now.ToString("dd/MM/yy") == NextLogsCleanup) && CleanLogs;

        #endregion Logs
    }
}