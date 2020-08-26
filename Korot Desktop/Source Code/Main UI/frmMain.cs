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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{

    public partial class frmMain : HTAlt.WinForms.HTTitleTabs
    {
        public Settings Settings;
        private readonly MyJumplist list;
        public List<DownloadItem> CurrentDownloads = new List<DownloadItem>();
        public List<string> CancelledDownloads = new List<string>();
        public List<string> notificationAsked = new List<string>();
        public List<frmNotification> notifications { get; set; }
        public Collection<frmCEF> NotifListeners { get => notifListeners; set => notifListeners = value; }
        public bool newDownload = false;
        public bool isIncognito = false;
        public HTTabRenderer tabRenderer;
        public HTTitleTab blockTab = null;
        public HTTitleTab licenseTab = null;
        public HTTitleTab newtabeditTab = null;
        public HTTitleTab settingTab = null;
        public HTTitleTab themeTab = null;
        public HTTitleTab historyTab = null;
        public HTTitleTab downloadTab = null;
        public HTTitleTab aboutTab = null;
        public HTTitleTab siteTab = null;
        public HTTitleTab collectionTab = null;
        public HTTitleTab notificationTab = null;

        #region Notification Listener
        private string closeKorotMessage = "Do you want to close Korot?";
#pragma warning disable 414
        private string closeAllMessage = "Do you really want to close them all?";
#pragma warning restore 414
        private string closeAll = "Clsoe all";
        private string closeKorot = "Close Korot";
        public string Yes = "Yes";
        public string No = "No";
        public string Cancel = "Cancel";
        private ToolStripMenuItem tsCloseK;
        private ToolStripMenuItem tsCloseAll;
        private ToolStripSeparator tsSepNL;
        public Collection<frmCEF> notifListeners = new Collection<frmCEF>();
        private readonly ContextMenuStrip cmsNL = new ContextMenuStrip() { RenderMode = ToolStripRenderMode.System, ShowImageMargin = false, };
        private readonly NotifyIcon NLEditor = new NotifyIcon() { Text = "Korot", Icon = Properties.Resources.KorotIcon, Visible = true };
        private void InitNL()
        {
            closeAll = Settings.LanguageSystem.GetItemText("CloseAll");
            closeAllMessage = Settings.LanguageSystem.GetItemText("CloseAllInfo");
            closeKorot = Settings.LanguageSystem.GetItemText("CloseKorot");
            closeKorotMessage = Settings.LanguageSystem.GetItemText("CloseKorotInfo");
            Yes = Settings.LanguageSystem.GetItemText("Yes");
            No = Settings.LanguageSystem.GetItemText("No");
            Cancel = Settings.LanguageSystem.GetItemText("Cancel");
            cmsNL.Items.Clear();
            foreach (frmCEF x in notifListeners)
            {
                ToolStripMenuItem tsItem = new ToolStripMenuItem
                {
                    Text = x.Text,
                    Tag = x,
                    Name = HTAlt.Tools.GenerateRandomText(12)
                };
                tsItem.Click += closeNL_Click;
                cmsNL.Items.Add(tsItem);
            }
            tsSepNL = new ToolStripSeparator()
            {
                Name = "tsSepNL"
            };
            cmsNL.Items.Add(tsSepNL);
            tsCloseAll = new ToolStripMenuItem()
            {
                Text = closeAll,
                Name = "CloseAllTS"
            };
            tsCloseAll.Click += closeall_Click;
            cmsNL.Items.Add(tsCloseAll);
            tsCloseK = new ToolStripMenuItem()
            {
                Text = closeKorot,
                Name = "CloseKorotTS"
            };
            tsCloseK.Click += closekorot_Click;
            cmsNL.Items.Add(tsCloseK);
            NLEditor.Visible = (notifListeners.Count > 0);
            cmsNL.BackColor = Settings.Theme.BackColor;
            cmsNL.ForeColor = HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor);
            foreach (ToolStripItem x in cmsNL.Items)
            {
                x.BackColor = Settings.Theme.BackColor;
                x.ForeColor = HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor);
            }
        }
        private void tmrNL_Tick(object sender, EventArgs e)
        {
            InitNL();
        }
        private void closeNL_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripItem tsSender = sender as ToolStripItem;
                Form tsForm = tsSender.Tag as Form;
                cmsNL.Items.Remove(tsSender);
                tsForm.Close();
                NotifListeners.Remove(tsForm as frmCEF);
            }
        }
        private void closeall_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot",
                                                      closeAllMessage,
                                                      new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
            { Icon = Properties.Resources.KorotIcon, Yes = Yes, No = No, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
            DialogResult diares = mesaj.ShowDialog();
            if (diares == DialogResult.Yes)
            {
                foreach (ToolStripItem x in cmsNL.Items)
                {
                    if (x.Name != "CloseAllTS" && x.Name != "CloseKorotTS" && x.Name != "tsSepNL")
                    {
                        closeNL_Click(x, e);
                    }
                }
            }
        }
        private void closekorot_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot",
                                                      closeKorotMessage,
                                                      new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
            { Icon = Properties.Resources.KorotIcon, Yes = Yes, No = No, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
            DialogResult diares = mesaj.ShowDialog();
            if (diares == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #endregion

        #region "Translate"
        public string OK = "OK";
        public string Extensions = "Extensions";
        public string OpenInNewTab = "Open in new tab";
        public string OpenFile = "Open file";
        public string OpenFileInExplorert = "Open folder containing this file";
        public string Month1 = "January";
        public string Month2 = "February";
        public string Month3 = "March";
        public string Month4 = "April";
        public string Month5 = "May";
        public string Month6 = "June";
        public string Month7 = "July";
        public string Month8 = "August";
        public string Month9 = "September";
        public string Month10 = "October";
        public string Month11 = "November";
        public string Month12 = "December";
        public string Month0 = "Haltroy";
        public string notificationPermission = "Allow [URL] for sending notifications?";
        public string allow = "Allow";
        public string deny = "Deny";
        public string ubuntuLicense = "Ubuntu Font License";
        public string newProfileInfo = "Please enter a name for the new profile.It should not contain: ";
        public string updateTitleTheme = "Korot Theme Updater";
        public string updateTitleExt = "Korot Extension Updater";
        public string updateExtInfo = "Updating [NAME]...[NEWLINE]Please wait...";
        public string openInNewWindow = "Open in New Window";
        public string openAllInNewWindow = "Open All in New Window(s)";
        public string openInNewIncWindow = "Open in New Incognito Window";
        public string openAllInNewIncWindow = "Open All in New Incognito Window(s)";
        public string openAllInNewTab = "Open All in New Tab(s)";
        public string newFavorite = "New Favorite";
        public string nametd = "Name :";
        public string urltd = "Url : ";
        public string add = "Add";
        public string newFolder = "New Folder";
        public string defaultFolderName = "New Folder";
        public string folderInfo = "Please enter a name for new Folder.";
        public string copyImage = "Copy Image";
        public string openLinkInNewWindow = "Open Link in a New Window";
        public string openLinkInNewIncWindow = "Open Link in a New Incognito Window";
        public string copyImageAddress = "Copy Image Address";
        public string saveLinkAs = "Save Link as...";
        public string goBack = "Go Back";
        public string goForward = "Go Forward";
        public string refresh = "Refresh";
        public string refreshNoCache = "Refresh (No Cache)";
        public string stop = "Stop";
        public string selectAll = "Select All";
        public string openLinkInNewTab = "Open Link in New Tab";
        public string copyLink = "Copy Link";
        public string saveImageAs = "Save Image as...";
        public string openImageInNewTab = "Open Image in New Tab";
        public string paste = "Paste";
        public string copy = "Copy";
        public string cut = "Cut";
        public string undo = "Undo";
        public string redo = "Redo";
        public string delete = "Delete";
        public string SearchOrOpenSelectedInNewTab = "Search/Open Selected in New Tab";
        public string developerTools = "Developer Tools";
        public string viewSource = "View Source";
        public string licenseTitle = "Licenses & Special Thanks Page";
        public string kLicense = "Korot License";
        public string vsLicense = "Microsoft Visual Studio 2019 Community License";
        public string chLicense = "Chromium License";
        public string cefLicense = "CefSharp License";
        public string etLicense = "EasyTabs License";
        public string specialThanks = "Special Thanks...";
        public string JSConfirm = "Confirm on page [TITLE]:";
        public string JSAlert = "A message from page [TITLE]:";
        public string selectAFolder = "Select a folder for downloads.";
        public string resetConfirm = "Do you want to reset Korot?" + Environment.NewLine + "(All of your personal datas are going to gone forever. This includes profiles, settings, downloads etc.)";
        public string findC = "Current: ";
        public string findT = "Total: ";
        public string findL = "(Last)";
        public string noSearch = "Not Searching or No Results";
        public string aboutInfo = "Korot uses Chromium by Google using CefSharp. [NEWLINE]Korot is written in C# using Visual Studio Community by Microsoft. [NEWLINE]Korot uses modified version of EasyTabs. [NEWLINE]Translation made by Haltroy. [NEWLINE][THEMENAME] theme made by [THEMEAUTHOR].";
        public string htmlFiles = "HTML File";
        public string print = "Print";
        public string IncognitoT = "Incognito";
        public string IncognitoTitle = "You are now in Incognito Mode!";
        public string IncognitoTitle1 = "Korot will not going to:";
        public string IncognitoT1M1 = "Record your history and downloads";
        public string IncognitoT1M2 = "Record cookies, sessions and form details";
        public string IncognitoT1M3 = "Record settings";
        public string IncognitoTitle2 = "But your activity can recorded by:";
        public string IncognitoT2M1 = "Websites";
        public string IncognitoT2M2 = "Your Internet service provider or your local network owner";
        public string IncognitoT2M3 = "Other viewers";
        public string disallowCookie = "Disallow this page using cookies";
        public string allowCookie = "Allow this page using cookie";
        public string imageFiles = "Image Files";
        public string allFiles = "All Files";
        public string selectBackImage = "Select a background image...";
        public string usingBC = "Using background color";
        public string settingstitle = "Settings";
        public string restoreOldSessions = "Restore last session";
        public string newWindow = "New Window";
        public string newincognitoWindow = "New Incognito Window";
        public string usesCookies = "This website uses cookies.";
        public string notUsesCookies = "This website does not use cookies.";
        public string showCertError = "Show Certificate Error";
        public string CertificateErrorMenuTitle = "Certificate Error Details";
        public string CertificateErrorTitle = "Not Safe";
        public string CertificateError = "This website is using a certificate that has errors.";
        public string CertificateOKTitle = "Safe";
        public string CertificateOK = "This website is using a certificate that has no errors.";
        public string ErrorTheme = "This theme file is corrupted or not suitable for this version.";
        public string ThemeMessage = "Do you want to change to this theme ?";
        public string UserAgentMessage = "Please enter an user agent.";
        public string installStatus = "Downloading Update...";
        public string StatusType = "[PERC]% | [CURRENT] KiB downloaded out of [TOTAL] KiB.";
        public string enterAValidCode = "Please enter a Valid Base64 Code.";
        public string enterAValidUrl = "Enter a Valid URL";
        public string goTotxt = "Go to \"[TEXT]\"";
        public string SearchOnWeb = "Search \"[TEXT]\"";
        public string defaultproxytext = "Default Proxy";
        public string SearchOnPage = "Search on this page";
        //public string CaseSensitive = "Case Sensitive";
        public string privatemode = "Incognito";
        public string updateTitle = "Korot - Update";
        public string updateMessage = "Update available.Do you want to update?";
        public string updateError = "Error while checking for the updates.";
        public string checking = "Checking for updates...";
        public string uptodate = "Your Korot is up-to-date.";
        public string updateavailable = "Update available.";
        public string NewTabtitle = "New Tab";
        public string customSearchNote = "(Note: Searched text will be put after the url)";
        public string customSearchMessage = "Write Custom Search Url";
        public string korotdownloading = "Korot - Downloading";
        public string fromtwodot = "From : ";
        public string totwodot = "To : ";
        public string open = "Open";
        public string Search = "Search";
        public string run = "Run";
        public string startatstarup = "Run at startup";
        public string ErrorPageTitle = "Korot - Error";
        public string MonthNames = "\"January\",\"February\",\"March\",\"April\",\"May\",\"June\",\"July\",\"August\",\"September\",\"October\",\"November\",\"December\"";
        public string DayNames = "\"Sunday\",\"Monday\",\"Tuesday\",\"Wednesday\",\"Thursday\",\"Friday\",\"Saturday\"";
        public string SearchHelpText = "Search on web or enter an URL.";
        public string KT = "Korot can't display this page.";
        public string ET = "One possibility is because of one of these errors:";
        public string E1 = "1.  The URL is incorrect.";
        public string E2 = "2.  The website is not responding,too busy or too slow.";
        public string E3 = "3.  Machine disconnected from Internet or connection is too slow.";
        public string E4 = "4.  Antivirus program thinks this browser is a virus or the Website includes a virus.";
        public string RT = "We recommend:";
        public string R1 = "1.  Checking the URL for errors(like grammar errors). ";
        public string R2 = "2.  Connect the machine to Internet. ";
        public string R3 = "3.  Wait a few minutes and try again. ";
        public string R4 = "4.  Disable Antivirus or add this browser to whitelist of Antivirus.";
        public string switchTo = "Switch to another profile...";
        public string deleteProfile = "Delete this profile";
        public string newprofile = "New Profile";
        public string CertErrorPageTitle = "This website is not secure";
        public string CertErrorPageMessage = "This website is using a certificate that has errors. Which means your information (credit cards,passwords,messages...) can be stolen by unknown people in this website.";
        public string CertErrorPageButton = "I understand these risks.";
        public string renderProcessDies = "Render Process Terminated. Closing application...";
        public string themeInfo = "[THEMENAME] theme made by [THEMEAUTHOR].";
        public string anon = "an unknown person";
        public string noname = "This unknown";
        public string text = "Text";
        public string image = "Image";
        public string link = "Link";
        //Editor
        public string catCommon = "Common";
        public string catText = "Text-based";
        public string catOnline = "Online";
        public string catPicture = "Picture";
        public string TitleID = "ID: ";
        public string TitleBackColor = "BackColor: ";
        public string TitleText = "Text: ";
        public string TitleFont = "Font: ";
        public string TitleSize = "FontSize: ";
        public string TitleProp = "FontProperties: ";
        public string TitleRegular = "Regular";
        public string TitleBold = "Bold";
        public string TitleItalic = "Italic";
        public string TitleUnderline = "Underline";
        public string TitleStrikeout = "Strikeout";
        public string TitleForeColor = "ForeColor: ";
        public string TitleSource = "Source: ";
        public string TitleWidth = "Width: ";
        public string TitleHeight = "Height: ";
        public string TitleDone = "Done";
        public string TitleEditItem = "Edit Item";
        public string importColItem = "Import item";
        public string importColItemInfo = "Enter a valid item code to import.";
        public string changeColID = "Change Collection ID";
        public string changeColIDInfo = "Enter a valid ID for this collection.";
        public string changeColText = "Change Collection Text";
        public string changeColTextInfo = "Enter a valid Text for this collection.";
        public string empty = "((empty))";
        public string SetToDefault = "Set to default";
        //Collection Manager
        public string Clear = "Clear";
        public string RemoveSelected = "Remove Selected";
        public string newColInfo = "Enter a name for new collection";
        public string newColName = "New Collection";
        public string importColInfo = "Enter code for new collection";
        public string delColInfo = "Do you really want to delete $?";
        public string clearColInfo = "Do you really want to delete all?";
        public string okToClipboard = "Press OK to copy to clipboard.";
        public string newCollection = "New Collection";
        public string deleteCollection = "Delete this collection";
        public string clearCollection = "Clear";
        public string importCollection = "Import";
        public string exportCollection = "Export";
        public string deleteItem = "Delete this item";
        public string exportItem = "Export this item";
        public string editItem = "Edit this item";
        public string addToCollection = "Add to Collection";
        public string titleBackInfo = "Click the rectangle on top to change color.";
        public string MuteThisTab = "Mute this tab";
        public string UnmuteThisTab = "Unmute this tab";
        public string siteCookies = "Cookies:";
        public string siteNotifications = "Notifications:";
        public string importProfileInfo = "Import Profile from...";
        public string exportProfileInfo = "Export Profile to...";
        public string ProfileFileInfo = "Korot Profile Archive";
        public string NewTabEdit = "Edit";
        public string IncognitoModeTitle = "Incognito Mode";
        public string IncognitoModeInfo = "This session is not going to be saved.";
        public string LearnMore = "Learn more...";
        public string ProfileNameTemp = "Hello, [NAME] !";
        public string ChangePic = "Change picture";
        public string ExportProfile = "Export this profile";
        public string ImportProfile = "Import profile";
        public string ChangePicInfo = "Do you want to reset the image or select new one?";
        public string ResetImage = "Reset image";
        public string SelectNewImage = "Select a new image";
        public string ResetToDefaultProxy = "Reset to default proxy";
        public string ResetZoom = "Reset Zoom";
        public string Collections = "Collections";
        public string SettingsText = "Settings";
        public string ThemesText = "Themes";
        public string AboutText = "About";
        public string DownloadsText = "Downloads";
        public string HistoryText = "History";
        public string BlockThisSite = "Block this site...";
        public string lv0info = "Blocks the Url in its host website.";
        public string lv1info = "Blocks the Url's website.";
        public string lv2info = "Block the Url's website with its subnomains.";
        public string lv3info = "Blocks when address includes this Url.";
        public string Done = "Done";
        public string editblockitem = "Edit block...";
        public string addblockitem = "Create new block...";
        public string lv0 = "Level 0";
        public string lv1 = "Level 1";
        public string lv2 = "Level 2";
        public string lv3 = "Level 3";
        public string blocklevel = "Block Level:";
        #endregion
        public frmMain(Settings settings)
        {
            Settings = settings;
            AeroPeekEnabled = true;
            tabRenderer = new HTTabRenderer(this);
            TabRenderer = tabRenderer;
            Icon = Properties.Resources.KorotIcon;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
            bool exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
            if (!exists)
            {
                foreach (Site x in Settings.Sites)
                {
                    if (!x.AllowNotifications) { return; }
                    frmCEF notfiListener = new frmCEF(this, Settings, isIncognito, x.Url, SafeFileSettingOrganizedClass.LastUser, true)
                    {
                        Visible = true,
                        Enabled = true
                    };
                    notfiListener.Show();
                    NotifListeners.Add(notfiListener);
                    notfiListener.Hide();
                }
                InitNL();
                Timer tmrNL = new Timer() { Interval = 5000, };
                tmrNL.Tick += tmrNL_Tick;
                tmrNL.Start();
            }
            list = new MyJumplist(Handle, settings);
        }
        public void removeThisDownloadItem(DownloadItem removeItem)
        {
            List<DownloadItem> removeDownloads = new List<DownloadItem>();
            foreach (DownloadItem x in CurrentDownloads)
            {
                if (x == removeItem) { removeDownloads.Add(x); }
                if (x.FullPath == removeItem.FullPath && x.Url == removeItem.Url && x.OriginalUrl == removeItem.OriginalUrl)
                {
                    removeDownloads.Add(x);
                }
            }
            foreach (DownloadItem x in removeDownloads)
            {
                CurrentDownloads.Remove(x);
            }
            removeDownloads.Clear();
        }

        private void PrintImages()
        {
            tabRenderer.ApplyColors(Settings.Theme.BackColor, HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor), Settings.Theme.OverlayColor, Settings.Theme.BackColor);
            this.Update();
            MinimumSize = new System.Drawing.Size(650, 350);
            BackColor = Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor);
        }
        public string OldSessions;
        private void frmMain_Load(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            if (DateTime.Now.ToString("MM") == "03" & DateTime.Now.ToString("dd") == "11")
            {
                Output.WriteLine("Happy " + (DateTime.Now.Year - 2001) + "th Birthday Dad!");
                List<string> HaltroyNameHistory = new List<string>() { "efojaeren", "Eren Kanat", "ErenKanat02", "ErenKanat03", "Lapisman", "LapisGamingTR", "NirvanaWolfTR", "TheLordEren", "SnowWolfTR", "Pell Game", "Pell Artz", "TheEfoja", "Mr Pell", "Pellguy", "LordPell", "Pellaraptor", "Pellraptor", "Pellerma", "PLLTR", "SpringTR", "KurtSys32", "KANAT", "Spiklyman (vs Mendebur Lemur)", "Wingaxy", "Haltroy" };
                Output.WriteLine("Here's a quick history about you:");
                foreach (string x in HaltroyNameHistory)
                {
                    Output.WriteLine(x);
                }
            }
            else if (DateTime.Now.ToString("MM") == "06" & DateTime.Now.ToString("dd") == "09")
            {
                List<string> KorotNameHistory = new List<string>() { "StoneHomepage (not browser) (First code written by Haltroy)", "StoneBrowser (Trident) (First ever program written by Haltroy)", "ZStone (Trident)", "Pell Browser (Trident)", "Kolme Browser (Trident)", "Ninova (Gecko)", "Webtroy (Gecko,CEF)", "Korot (CEF)" };
                Output.WriteLine("I'm " + (DateTime.Now.Year - 2017) + " years old now!");
                Output.WriteLine("Here's a quick history about me:");
                foreach (string x in KorotNameHistory)
                {
                    Output.WriteLine(x);
                }
            }
            else if (DateTime.Now.ToString("MM") == "01" & DateTime.Now.ToString("dd") == "21")
            {
                List<string> PTNameHistory = new List<string>() { "Pell Media Player (WMP)", "ZStone (WMP)", "Kolme Player (WMP)", "MyPlay (WMP)", "Playtroy" };
                Output.WriteLine("My sister's " + (DateTime.Now.Year - 2017) + " years old now!");
                Output.WriteLine("Here's a quick history about my sister:");
                foreach (string x in PTNameHistory)
                {
                    Output.WriteLine(x);
                }
            }
            PrintImages();
            if (Settings.AutoRestore)
            {
                ReadSession(SafeFileSettingOrganizedClass.LastSession);
            }
            else
            {
                OldSessions = SafeFileSettingOrganizedClass.LastSession;
            }
            SessionLogger.Start();
            MinimumSize = new System.Drawing.Size(660, 340);
            MaximizedBounds = Screen.GetWorkingArea(this);
            if (!Settings.MenuWasMaximized) { WindowState = FormWindowState.Normal; }
            else { WindowState = FormWindowState.Maximized; }
            Size = Settings.MenuSize;
            Location = Settings.MenuPoint;

        }

        public void ReadSession(string Session)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Session);
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                frmCEF cefform = new frmCEF(this, Settings, isIncognito, "korot://newtab", SafeFileSettingOrganizedClass.LastUser);
                cefform.lbURL.Items.Clear();
                cefform.lbTitle.Items.Clear();
                foreach (XmlNode subnode in node.ChildNodes)
                {
                    if (subnode.Name.ToLower() == "site")
                    {
                        string url = subnode.Attributes["Url"].Value;
                        string title = subnode.Attributes["Title"].Value;
                        if (!(url is null || title is null))
                        {
                            cefform.lbURL.Items.Add(url);
                            cefform.lbTitle.Items.Add(title);
                        }
                    }
                }
                cefform.lbURL.SelectedIndex = Convert.ToInt32(node.Attributes["Index"].Value);
                cefform.lbTitle.SelectedIndex = Convert.ToInt32(node.Attributes["Index"].Value);
                HTTitleTab tab = new HTTitleTab(this)
                {
                    Content = cefform
                };
                Tabs.Add(tab);
            }

        }
        private string writtenSession = "";
        public void WriteSessions(string Session)
        {
            if (writtenSession == Session) { return; }
            writtenSession = Session;
            SafeFileSettingOrganizedClass.LastSession = Session;
        }
        public void WriteCurrentSession()
        {
            string CurrentSessionURIs = "<root>" + Environment.NewLine;
            foreach (HTTitleTab x in Tabs)
            {
                frmCEF cefform = (frmCEF)x.Content;
                List<Site> currentSites = new List<Site>();
                int i = 0; int Count = cefform.lbURL.Items.Count - 1;
                if (cefform.lbURL.Items.Count > 0)
                {
                    while (i != Count)
                    {
                        currentSites.Add(new Korot.Site() { Name = cefform.lbTitle.Items[i].ToString(), Url = cefform.lbURL.Items[i].ToString() });
                        i += 1;
                    }
                }
                CurrentSessionURIs += " <Session Index=\"" + cefform.lbURL.SelectedIndex + "\" >" + Environment.NewLine;
                foreach (Site site in currentSites)
                {
                    CurrentSessionURIs += "  <Site Title=\"" + site.Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Text=\"" + Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Url=\"" + site.Url.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" Text=\"" + Text.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "\" />" + Environment.NewLine;
                }
                CurrentSessionURIs += " </Session>" + Environment.NewLine;
            }
            CurrentSessionURIs += "</root>" + Environment.NewLine;
            WriteSessions(CurrentSessionURIs);
        }
        public bool closing = false;
        public void CreateTab(HTTitleTab referenceTab, string url = "korot://newtab")
        {
            frmCEF cefform = new frmCEF(this, Settings, isIncognito, url, SafeFileSettingOrganizedClass.LastUser);
            Settings.AllForms.Add(cefform);
            HTTitleTab newTab = new HTTitleTab(this)
            {
                BackColor = referenceTab.BackColor,
                UseDefaultBackColor = referenceTab.UseDefaultBackColor,
                Content = cefform
            };
            Tabs.Insert(Tabs.IndexOf(referenceTab) + 1, newTab);
            SelectedTabIndex = Tabs.IndexOf(referenceTab) + 1;
            //Tabs.Add(newTab);
        }
        public void CreateTab(string url = "korot://newtab")
        {
            frmCEF cefform = new frmCEF(this, Settings, isIncognito, url, SafeFileSettingOrganizedClass.LastUser);
            Settings.AllForms.Add(cefform);
            HTTitleTab newTab = new HTTitleTab(this)
            {
                BackColor = Settings.Theme.BackColor,
                UseDefaultBackColor = true,
                Content = cefform
            };
            Tabs.Add(newTab);
            SelectedTabIndex = Tabs.Count - 1;
        }
        public override HTTitleTab CreateTab()
        {
            frmCEF cefform = new frmCEF(this, Settings, isIncognito, "korot://newtab", SafeFileSettingOrganizedClass.LastUser);
            Settings.AllForms.Add(cefform);
            return new HTTitleTab(this)
            {
                BackColor = Settings.Theme.BackColor,
                UseDefaultBackColor = true,
                Content = cefform
            };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Settings.ThemeChangeForm.Contains(this))
            {
                PrintImages();
                Settings.ThemeChangeForm.Remove(this);
            }
        }
        public bool isFullScreen = false;
        public bool wasMaximized = false;
        public void Fullscreenmode(bool fullscreen)
        {
            if (fullscreen)
            {
                wasMaximized = WindowState == FormWindowState.Maximized;
                WindowState = FormWindowState.Normal;
                Taskbar.Hide();
                MaximizedBounds = Screen.FromHandle(Handle).Bounds;
                WindowState = FormWindowState.Maximized;
                isFullScreen = true;
            }
            else
            {
                Taskbar.Show();
                MaximizedBounds = Screen.GetWorkingArea(this);
                if (!wasMaximized)
                {
                    WindowState = FormWindowState.Normal;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    WindowState = FormWindowState.Maximized;
                }
                isFullScreen = false;

            }
            FormBorderStyle = fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Taskbar.Show();
            closing = true;
            if (!isIncognito)
            {
                Settings.MenuPoint = Location;
                Settings.MenuSize = Size;
                Settings.MenuWasMaximized = WindowState == FormWindowState.Maximized;

                if (e.CloseReason != CloseReason.None || e.CloseReason != CloseReason.WindowsShutDown || e.CloseReason != CloseReason.TaskManagerClosing)
                {
                    Korot.SafeFileSettingOrganizedClass.LastSession = "";
                }
                else
                {
                    WriteCurrentSession();

                }
                Settings.Save();
            }
        }

        private void SessionLogger_Tick(object sender, EventArgs e)
        {
            WriteCurrentSession();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            foreach (HTTitleTab x in Tabs)
            {
                ((frmCEF)x.Content).Invoke(new Action(() => ((frmCEF)x.Content).FrmCEF_SizeChanged(null, null)));
            }

        }
    }
}
