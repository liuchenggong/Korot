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
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public partial class frmCEF : Form
    {
        public bool closing;
        private int updateProgress = 0;

        //0=Checking 1=UpToDate 2=UpdateAvailabe 3=Error
        private bool isLoading = false;
        private readonly string loaduri = null;
        public bool _Incognito = false;
        private readonly string userName;
        private readonly string profilePath;
        private readonly string userCache;
#pragma warning disable IDE0052 //(we don't need this for now but we might need this later)
        private int findIdentifier;
#pragma warning restore IDE0052
        private int findTotal;
        private int findCurrent;
        private bool findLast;
        private readonly string defaultProxy = null;
        public ChromiumWebBrowser chromiumWebBrowser1;
        private readonly List<ToolStripMenuItem> favoritesFolders = new List<ToolStripMenuItem>();
        private readonly List<ToolStripMenuItem> favoritesNoIcon = new List<ToolStripMenuItem>();
        // [NEWTAB]
        public frmMain anaform()
        {
            return ((frmMain)ParentTabs);
        }
        public frmCEF(bool isIncognito = false, string loadurl = "korot://newtab", string profileName = "user0")
        {
            loaduri = loadurl;
            _Incognito = isIncognito;
            userName = profileName;
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profileName + "\\";
            userCache = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profileName + "\\cache\\";
            InitializeComponent();
            InitializeChromium();
            updateExtensions();
            foreach (Control x in Controls)
            {
                try { x.KeyDown += tabform_KeyDown; x.MouseWheel += MouseScroll; x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
            Uri testUri = new Uri("https://haltroy.com");
            Uri aUri = WebRequest.GetSystemWebProxy().GetProxy(testUri);
            if (aUri != testUri)
            {
                defaultProxy = aUri.AbsoluteUri;
            }

            if (defaultProxy == null) { DefaultProxyts.Visible = false; DefaultProxyts.Enabled = false; }
            else
            {
                if (Properties.Settings.Default.rememberLastProxy && !string.IsNullOrWhiteSpace(Properties.Settings.Default.LastProxy)) { SetProxy(chromiumWebBrowser1, Properties.Settings.Default.LastProxy); }
            }
        }

        private void RefreshHistory()
        {
            int selectedValue = hlvHistory.SelectedIndices.Count > 0 ? hlvHistory.SelectedIndices[0] : 0;
            ListViewItem scroll = hlvHistory.TopItem;
            hlvHistory.Items.Clear();
            string Playlist = Properties.Settings.Default.History;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while ((i != Count) && (Count > 2))
            {
                ListViewItem listV = new ListViewItem(SplittedFase[i].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 1].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 2].Replace(Environment.NewLine, ""));
                hlvHistory.Items.Add(listV);
                i += 3;
            } while ((i != Count) && (Count > 2))
            {
                ListViewItem listV = new ListViewItem(SplittedFase[i].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 1].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 2].Replace(Environment.NewLine, ""));
                hlvHistory.Items.Add(listV);
                i += 3;
            }
            if (selectedValue <= (hlvHistory.Items.Count - 1))
            {
                hlvHistory.SelectedIndices.Clear();
                if (selectedValue < (hlvHistory.Items.Count - 1))
                {
                    hlvHistory.Items[selectedValue].Selected = true;
                    hlvHistory.TopItem = scroll;
                }
            }
            hlvHistory.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private readonly string iconStorage = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\IconStorage\\";
        public void LoadDynamicMenu()
        {
            mFavorites.Items.Clear();
            favoritesNoIcon.Clear();
            favoritesFolders.Clear();
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Favorites))
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(Properties.Settings.Default.Favorites.Replace("[", "<").Replace("]", ">"));
                writer.Flush();
                stream.Position = 0;
                XmlDocument document = new XmlDocument();
                document.Load(stream);

                XmlElement element = document.DocumentElement;

                foreach (XmlNode node in document.FirstChild.ChildNodes)
                {
                    if (node.Name == "Folder")
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem
                        {
                            Name = node.Attributes["Name"].Value,
                            Text = node.Attributes["Text"].Value,
                            Tag = "korot://folder"
                        };
                        menuItem.MouseUp += menuStrip1_MouseUp;
                        menuItem.DropDown.RenderMode = ToolStripRenderMode.Professional;
                        favoritesFolders.Add(menuItem);

                        mFavorites.Items.Add(menuItem);
                        GenerateMenusFromXML(node, (ToolStripMenuItem)mFavorites.Items[mFavorites.Items.Count - 1]);

                    }
                    else if (node.Name == "Favorite")
                    {
                        ToolStripMenuItem menuItem = new ToolStripMenuItem
                        {
                            Name = node.Attributes["Name"].Value,
                            Text = node.Attributes["Text"].Value
                        };
                        menuItem.DropDown.RenderMode = ToolStripRenderMode.Professional;
                        menuItem.Tag = node.Attributes["Url"] == null ? null : node.Attributes["Url"].Value;
                        if (node.Attributes["Icon"] != null)
                        {
                            if (File.Exists(node.Attributes["Icon"].Value.Replace("{ICONSTORAGE}", iconStorage)) && !string.IsNullOrWhiteSpace(node.Attributes["Icon"].Value))
                            {
                                Stream fs = FileSystem2.ReadFile(node.Attributes["Icon"].Value.Replace("{ICONSTORAGE}", iconStorage));
                                menuItem.Image = Image.FromStream(fs);
                                fs.Close();
                            }
                            else
                            {
                                favoritesNoIcon.Add(menuItem);
                            }
                        }
                        else
                        {
                            favoritesNoIcon.Add(menuItem);
                        }
                        menuItem.MouseUp += menuStrip1_MouseUp;

                        mFavorites.Items.Add(menuItem);
                    }
                }
            }
            else
            {
                Properties.Settings.Default.Favorites = "[root][/root]";
                LoadDynamicMenu();
            }
            UpdateFavoriteColor();
            updateFavoritesImages();
        }

        private void updateFavoritesImages()
        {
            foreach (ToolStripMenuItem x in favoritesFolders)
            {
                x.Image = Tools.isBright(Properties.Settings.Default.BackColor) ? Properties.Resources.folder : Properties.Resources.folder_w;
            }
            foreach (ToolStripMenuItem x in favoritesNoIcon)
            {
                x.Image = Tools.isBright(Properties.Settings.Default.BackColor) ? Properties.Resources.web : Properties.Resources.web_w;
            }
        }
        private void GenerateMenusFromXML(XmlNode rootNode, ToolStripMenuItem menuItem)
        {
            ToolStripMenuItem item = null;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                if (node.Name == "Folder")
                {
                    item = new ToolStripMenuItem
                    {
                        Name = node.Attributes["Name"].Value,
                        Text = node.Attributes["Text"].Value,
                        Tag = "korot://folder"
                    };
                    item.DropDown.RenderMode = ToolStripRenderMode.Professional;
                    item.MouseUp += menuStrip1_MouseUp;
                    favoritesFolders.Add(item);
                    menuItem.DropDownItems.Add(item);


                    GenerateMenusFromXML(node, (ToolStripMenuItem)menuItem.DropDownItems[menuItem.DropDownItems.Count - 1]);
                }
                else if (node.Name == "Favorite")
                {
                    item = new ToolStripMenuItem
                    {
                        Name = node.Attributes["Name"].Value,
                        Text = node.Attributes["Text"].Value
                    };
                    item.DropDown.RenderMode = ToolStripRenderMode.Professional;
                    item.Tag = node.Attributes["Url"] == null ? null : node.Attributes["Url"].Value;
                    if (node.Attributes["Icon"] != null)
                    {
                        if (File.Exists(node.Attributes["Icon"].Value.Replace("{ICONSTORAGE}", iconStorage)) && !string.IsNullOrWhiteSpace(node.Attributes["Icon"].Value))
                        {
                            Stream fs = FileSystem2.ReadFile(node.Attributes["Icon"].Value.Replace("{ICONSTORAGE}", iconStorage));
                            item.Image = Image.FromStream(fs);
                            fs.Close();
                        }
                        else
                        {
                            favoritesNoIcon.Add(item);
                        }
                    }
                    else
                    {
                        favoritesNoIcon.Add(item);
                    }
                    menuItem.DropDownItems.Add(item);


                    // add an event handler to the menu item added
                    item.MouseUp += menuStrip1_MouseUp;
                }
            }
        }
        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (((ToolStripMenuItem)sender).Tag != null)
                {
                    if (((ToolStripMenuItem)sender).Tag.ToString() != "korot://folder")
                    {
                        chromiumWebBrowser1.Load(((ToolStripMenuItem)sender).Tag.ToString());
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (((ToolStripMenuItem)sender).Tag != null)
                {
                    selectedFavorite = ((ToolStripMenuItem)sender);
                    tsopenInNewTab.Text = (((ToolStripMenuItem)sender).Tag.ToString() != "korot://folder") ? openInNewTab : openAllInNewTab;
                    openİnNewWindowToolStripMenuItem.Text = (((ToolStripMenuItem)sender).Tag.ToString() != "korot://folder") ? openInNewWindow : openAllInNewWindow;
                    openİnNewIncognitoWindowToolStripMenuItem.Text = (((ToolStripMenuItem)sender).Tag.ToString() != "korot://folder") ? openInNewIncWindow : openAllInNewIncWindow;
                    cmsFavorite.Show(MousePosition);
                }
            }
        }
        public void FindUpdate(int identifier, int count, int activeMatchOrdinal, bool finalUpdate)
        {
            findIdentifier = identifier;
            findTotal = count;
            findCurrent = activeMatchOrdinal;
            findLast = finalUpdate;
        }
        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot", listBox2.SelectedItem.ToString() + Environment.NewLine + ThemeMessage, Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140);
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + listBox2.SelectedItem.ToString());
                    comboBox1.Text = listBox2.SelectedItem.ToString().Replace(".ktf", "");
                }
            }
        }
        #region "Translate"
        public string ubuntuLicense = "Ubuntu Font License";
        public string newProfileInfo = "Please enter a name for the new profile.It should not contain: ";
        public string Yes = "Yes";
        public string No = "No";
        public string OK = "OK";
        public string Cancel = "Cancel";
        public string updateTitleTheme = "Korot Theme Updater";
        public string updateTitleExt = "Korot Extension Updater";
        public string updateExtInfo = "Updating [NAME]...[NEWLINE]Please wait...";
        public string openInNewWindow = "Open in New Window";
        public string openAllInNewWindow = "Open All in New Window(s)";
        public string openInNewIncWindow = "Open in New Incognito Window";
        public string openAllInNewIncWindow = "Open All in New Incognito Window(s)";
        public string openInNewTab = "Open in New Tab";
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
        public string SearchOnPage = "Search: ";
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
        public string switchTo = "Switch to:";
        public string deleteProfile = "Delete this profile";
        public string newprofile = "New Profile";
        public string CertErrorPageTitle = "This website is not secure";
        public string CertErrorPageMessage = "This website is using a certificate that has errors. Which means your information (credit cards,passwords,messages...) can be stolen by unknown people in this website.";
        public string CertErrorPageButton = "I understand these risks.";
        public string renderProcessDies = "Render Process Terminated. Closing application...";
        public string themeInfo = "[THEMENAME] theme made by [THEMEAUTHOR].";
        public string anon = "an unknown person";
        public string noname = "This unknown";


        private void dummyCMS_Opening(object sender, CancelEventArgs e)
        {
            Process.Start(Application.StartupPath + "//Lang//");
        }
        public void LoadLangFromFile(string fileLocation)
        {
            string Playlist = FileSystem2.ReadFile(fileLocation, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SF = Playlist.Split(token);

            while (SF.Length != 300)
            {
                Array.Resize<string>(ref SF, SF.Length + 1);
                SF[SF.Length - 1] = "mmisingno";
            }
            Version langVersion = new Version(SF[0].ToString().Replace(Environment.NewLine, "") != "mmissingno" ? SF[0].ToString().Replace(Environment.NewLine, "") : "0.0.0.0");
            Version current = new Version(Application.ProductVersion);
            if (langVersion < current)
            {
                if (Properties.Settings.Default.disableLangErrors)
                {
                    ReadLangFileFromTemp(SF);
                    Properties.Settings.Default.LangFile = fileLocation;
                    if (!_Incognito) { Properties.Settings.Default.Save(); }
                }
                else
                {
                    HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot", "This language file is made for an older version of Korot. This can cause some problems. Do you wish to proceed?", Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, "Yes", "No", "OK", "Cancel", 390, 140);
                    if (mesaj.ShowDialog() == DialogResult.Yes)
                    {
                        Properties.Settings.Default.disableLangErrors = true;
                        ReadLangFileFromTemp(SF);
                        Properties.Settings.Default.LangFile = fileLocation;
                        if (!_Incognito) { Properties.Settings.Default.Save(); }

                    }
                }
            }
            else
            {
                ReadLangFileFromTemp(SF);
                Properties.Settings.Default.LangFile = fileLocation;
                if (!_Incognito) { Properties.Settings.Default.Save(); }
            }
        }
        public void ReadLangFileFromTemp(string[] SplittedFase)
        {
            if (SplittedFase.Length >= 269)
            {
                Properties.Settings.Default.KorotErrorTitle = SplittedFase[262].Substring(1).Replace(Environment.NewLine, "");
                Properties.Settings.Default.KorotErrorDesc = SplittedFase[263].Substring(1).Replace(Environment.NewLine, "");
                Properties.Settings.Default.KorotErrorTI = SplittedFase[264].Substring(1).Replace(Environment.NewLine, ""); ;
                ubuntuLicense = SplittedFase[261].Substring(1).Replace(Environment.NewLine, "");
                tsFullscreen.Text = SplittedFase[257].Substring(1).Replace(Environment.NewLine, "");
                updateTitleTheme = SplittedFase[258].Substring(1).Replace(Environment.NewLine, "");
                updateTitleExt = SplittedFase[259].Substring(1).Replace(Environment.NewLine, "");
                updateExtInfo = SplittedFase[260].Substring(1).Replace(Environment.NewLine, "");
                openInNewWindow = SplittedFase[251 + 1].Substring(1).Replace(Environment.NewLine, "");
                openAllInNewWindow = SplittedFase[252 + 1].Substring(1).Replace(Environment.NewLine, "");
                openInNewIncWindow = SplittedFase[253 + 1].Substring(1).Replace(Environment.NewLine, "");
                openAllInNewIncWindow = SplittedFase[254 + 1].Substring(1).Replace(Environment.NewLine, "");
                openAllInNewTab = SplittedFase[250 + 1].Substring(1).Replace(Environment.NewLine, "");
                newFavorite = SplittedFase[243 + 1].Substring(1).Replace(Environment.NewLine, "");
                nametd = SplittedFase[244 + 1].Substring(1).Replace(Environment.NewLine, "");
                urltd = SplittedFase[245 + 1].Substring(1).Replace(Environment.NewLine, "");
                add = SplittedFase[246 + 1].Substring(1).Replace(Environment.NewLine, "");
                newFavoriteToolStripMenuItem.Text = SplittedFase[255 + 1].Substring(1).Replace(Environment.NewLine, "");
                newFolderToolStripMenuItem.Text = SplittedFase[247 + 1].Substring(1).Replace(Environment.NewLine, "");
                newFolder = SplittedFase[247 + 1].Substring(1).Replace(Environment.NewLine, "");
                defaultFolderName = SplittedFase[248 + 1].Substring(1).Replace(Environment.NewLine, "");
                folderInfo = SplittedFase[249 + 1].Substring(1).Replace(Environment.NewLine, "");
                copyImage = SplittedFase[238 + 1].Substring(1).Replace(Environment.NewLine, "");
                openLinkInNewWindow = SplittedFase[239 + 1].Substring(1).Replace(Environment.NewLine, "");
                openLinkInNewIncWindow = SplittedFase[240 + 1].Substring(1).Replace(Environment.NewLine, "");
                copyImageAddress = SplittedFase[241 + 1].Substring(1).Replace(Environment.NewLine, "");
                saveLinkAs = SplittedFase[242 + 1].Substring(1).Replace(Environment.NewLine, "");
                tsWebStore.Text = SplittedFase[237 + 1].Substring(1).Replace(Environment.NewLine, "");
                tsEmptyExt.Text = SplittedFase[233 + 1].Substring(1).Replace(Environment.NewLine, "");
                tsEmptyProfile.Text = SplittedFase[233 + 1].Substring(1).Replace(Environment.NewLine, "");
                btCleanLog.Text = SplittedFase[234 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbUResources.Text = SplittedFase[230 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbURinfo.Text = SplittedFase[229 + 1].Substring(1).Replace(Environment.NewLine, "");
                label33.Text = SplittedFase[228 + 1].Substring(1).Replace(Environment.NewLine, "");
                label31.Text = SplittedFase[226 + 1].Substring(1).Replace(Environment.NewLine, "");
                label32.Text = SplittedFase[227 + 1].Substring(1).Replace(Environment.NewLine, "");
                int cb3Index = comboBox3.SelectedIndex;
                comboBox3.Items.Clear();
                comboBox3.Items.Add(SplittedFase[218 + 1].Substring(1).Replace(Environment.NewLine, ""));
                comboBox3.Items.Add(SplittedFase[219 + 1].Substring(1).Replace(Environment.NewLine, ""));
                comboBox3.Items.Add(SplittedFase[220 + 1].Substring(1).Replace(Environment.NewLine, ""));
                comboBox3.Items.Add(SplittedFase[221 + 1].Substring(1).Replace(Environment.NewLine, ""));
                comboBox3.Items.Add(SplittedFase[222 + 1].Substring(1).Replace(Environment.NewLine, ""));
                comboBox3.SelectedIndex = cb3Index;
                int dNTIndex = dudNewTab.SelectedIndex;
                dudNewTab.Items.Clear();
                dudNewTab.Items.Add(SplittedFase[223 + 1].Substring(1).Replace(Environment.NewLine, ""));
                dudNewTab.Items.Add(SplittedFase[224 + 1].Substring(1).Replace(Environment.NewLine, ""));
                dudNewTab.Items.Add(SplittedFase[225 + 1].Substring(1).Replace(Environment.NewLine, ""));
                dudNewTab.SelectedIndex = dNTIndex;
                int dCIndex = dudClose.SelectedIndex;
                dudClose.Items.Clear();
                dudClose.Items.Add(SplittedFase[223 + 1].Substring(1).Replace(Environment.NewLine, ""));
                dudClose.Items.Add(SplittedFase[224 + 1].Substring(1).Replace(Environment.NewLine, ""));
                dudClose.Items.Add(SplittedFase[225 + 1].Substring(1).Replace(Environment.NewLine, ""));
                dudClose.SelectedIndex = dCIndex;
                licenseTitle = SplittedFase[211 + 1].Substring(1).Replace(Environment.NewLine, "");
                kLicense = SplittedFase[212 + 1].Substring(1).Replace(Environment.NewLine, "");
                vsLicense = SplittedFase[213 + 1].Substring(1).Replace(Environment.NewLine, "");
                chLicense = SplittedFase[214 + 1].Substring(1).Replace(Environment.NewLine, "");
                cefLicense = SplittedFase[215 + 1].Substring(1).Replace(Environment.NewLine, "");
                etLicense = SplittedFase[216 + 1].Substring(1).Replace(Environment.NewLine, "");
                specialThanks = SplittedFase[217 + 1].Substring(1).Replace(Environment.NewLine, "");
                JSAlert = SplittedFase[210 + 1].Substring(1).Replace(Environment.NewLine, "");
                JSConfirm = SplittedFase[209 + 1].Substring(1).Replace(Environment.NewLine, "");
                selectAFolder = SplittedFase[208 + 1].Substring(1).Replace(Environment.NewLine, "");
                button12.Text = SplittedFase[207 + 1].Substring(1).Replace(Environment.NewLine, "");
                showNewTabPageToolStripMenuItem.Text = SplittedFase[204 + 1].Substring(1).Replace(Environment.NewLine, "");
                showHomepageToolStripMenuItem.Text = SplittedFase[205 + 1].Substring(1).Replace(Environment.NewLine, "");
                showAWebsiteToolStripMenuItem.Text = SplittedFase[206 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbDownloadFolder.Text = SplittedFase[203 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbAutoDownload.Text = SplittedFase[202 + 1].Substring(1).Replace(Environment.NewLine, "");
                label28.Text = SplittedFase[201 + 1].Substring(1).Replace(Environment.NewLine, "");
                button18.Text = SplittedFase[200 + 1].Substring(1).Replace(Environment.NewLine, "");
                resetConfirm = SplittedFase[199 + 1].Substring(1).Replace(Environment.NewLine, "");
                label27.Text = SplittedFase[196 + 1].Substring(1).Replace(Environment.NewLine, "");
                allowSelectedToolStripMenuItem.Text = SplittedFase[197 + 1].Substring(1).Replace(Environment.NewLine, "");
                clearToolStripMenuItem1.Text = SplittedFase[17 + 1].Substring(1).Replace(Environment.NewLine, "");
                btCookie.Text = SplittedFase[198 + 1].Substring(1).Replace(Environment.NewLine, "");
                ıncognitoModeToolStripMenuItem.Text = SplittedFase[193 + 1].Substring(1).Replace(Environment.NewLine, "");
                thisSessionİsNotGoingToBeSavedToolStripMenuItem.Text = SplittedFase[194 + 1].Substring(1).Replace(Environment.NewLine, "");
                clickHereToLearnMoreToolStripMenuItem.Text = SplittedFase[195 + 1].Substring(1).Replace(Environment.NewLine, "");
                chStatus.Text = SplittedFase[192 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbLastProxy.Text = SplittedFase[186 + 1].Substring(1).Replace(Environment.NewLine, "");
                findC = SplittedFase[187 + 1].Substring(1).Replace(Environment.NewLine, "");
                findT = SplittedFase[188 + 1].Substring(1).Replace(Environment.NewLine, "");
                findL = SplittedFase[189 + 1].Substring(1).Replace(Environment.NewLine, "");
                noSearch = SplittedFase[190 + 1].Substring(1).Replace(Environment.NewLine, "");
                tsSearchNext.Text = SplittedFase[185 + 1].Substring(1).Replace(Environment.NewLine, "");
                anon = SplittedFase[183 + 1].Substring(1).Replace(Environment.NewLine, "");
                noname = SplittedFase[184 + 1].Substring(1).Replace(Environment.NewLine, "");
                themeInfo = SplittedFase[182 + 1].Substring(1).Replace(Environment.NewLine, "");
                extensionToolStripMenuItem1.Text = SplittedFase[181 + 1].Substring(1).Replace(Environment.NewLine, "");
                renderProcessDies = SplittedFase[178 + 1].Substring(1).Replace(Environment.NewLine, "");
                enterAValidCode = SplittedFase[177 + 1].Substring(1).Replace(Environment.NewLine, "");
                zoomInToolStripMenuItem.Text = SplittedFase[174 + 1].Substring(1).Replace(Environment.NewLine, "");
                resetZoomToolStripMenuItem.Text = SplittedFase[175 + 1].Substring(1).Replace(Environment.NewLine, "");
                zoomOutToolStripMenuItem.Text = SplittedFase[176 + 1].Substring(1).Replace(Environment.NewLine, "");
                htmlFiles = SplittedFase[171 + 1].Substring(1).Replace(Environment.NewLine, "");
                takeAScreenshotToolStripMenuItem.Text = SplittedFase[172 + 1].Substring(1).Replace(Environment.NewLine, "");
                saveThisPageToolStripMenuItem.Text = SplittedFase[173 + 1].Substring(1).Replace(Environment.NewLine, "");
                print = SplittedFase[170 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT = SplittedFase[160 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoTitle = SplittedFase[160 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoTitle1 = SplittedFase[161 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT1M1 = SplittedFase[163 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT1M2 = SplittedFase[164 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT1M3 = SplittedFase[165 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoTitle2 = SplittedFase[166 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT2M1 = SplittedFase[167 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT2M2 = SplittedFase[168 + 1].Substring(1).Replace(Environment.NewLine, "");
                IncognitoT2M3 = SplittedFase[169 + 1].Substring(1).Replace(Environment.NewLine, "");
                disallowCookie = SplittedFase[158 + 1].Substring(1).Replace(Environment.NewLine, "");
                allowCookie = SplittedFase[159 + 1].Substring(1).Replace(Environment.NewLine, "");
                if (Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser1.Address))
                {
                    disallowThisPageForCookieAccessToolStripMenuItem.Text = SplittedFase[158 + 1].Substring(1).Replace(Environment.NewLine, "");
                }
                else
                {
                    disallowThisPageForCookieAccessToolStripMenuItem.Text = SplittedFase[159 + 1].Substring(1).Replace(Environment.NewLine, "");
                }
                label25.Text = SplittedFase[139 + 1].Substring(1).Replace(Environment.NewLine, "");
                imageFiles = SplittedFase[136 + 1].Substring(1).Replace(Environment.NewLine, "");
                allFiles = SplittedFase[137 + 1].Substring(1).Replace(Environment.NewLine, "");
                selectBackImage = SplittedFase[138 + 1].Substring(1).Replace(Environment.NewLine, "");
                colorToolStripMenuItem.Text = SplittedFase[132 + 1].Substring(1).Replace(Environment.NewLine, "");
                usingBC = SplittedFase[133 + 1].Substring(1).Replace(Environment.NewLine, "");
                ımageFromURLToolStripMenuItem.Text = SplittedFase[134 + 1].Substring(1).Replace(Environment.NewLine, "");
                ımageFromLocalFileToolStripMenuItem.Text = SplittedFase[135 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbDNT.Text = SplittedFase[129 + 1].Substring(1).Replace(Environment.NewLine, "");
                aboutInfo = SplittedFase[108 + 1].Substring(1).Replace(Environment.NewLine, "");
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeAuthor) && string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeName))
                {
                    label21.Text = SplittedFase[108 + 1].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                }
                else
                {
                    label21.Text = SplittedFase[108 + 1].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + SplittedFase[182 + 1].Substring(1).Replace(Environment.NewLine, "").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeAuthor) ? anon : Properties.Settings.Default.ThemeAuthor).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeName) ? noname : Properties.Settings.Default.ThemeName);
                }
                linkLabel1.Text = SplittedFase[109 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbSettings.Text = SplittedFase[9 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertErrorPageButton = SplittedFase[107 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertErrorPageMessage = SplittedFase[106 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertErrorPageTitle = SplittedFase[105 + 1].Substring(1).Replace(Environment.NewLine, "");
                usesCookies = SplittedFase[103 + 1].Substring(1).Replace(Environment.NewLine, "");
                notUsesCookies = SplittedFase[104 + 1].Substring(1).Replace(Environment.NewLine, "");
                showCertError = SplittedFase[102 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertificateErrorMenuTitle = SplittedFase[97 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertificateErrorTitle = SplittedFase[98 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertificateError = SplittedFase[99 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertificateOKTitle = SplittedFase[100 + 1].Substring(1).Replace(Environment.NewLine, "");
                CertificateOK = SplittedFase[101 + 1].Substring(1).Replace(Environment.NewLine, "");
                ErrorTheme = SplittedFase[96 + 1].Substring(1).Replace(Environment.NewLine, "");
                ThemeMessage = SplittedFase[95 + 1].Substring(1).Replace(Environment.NewLine, "");
                btUpdater.Text = SplittedFase[90 + 1].Substring(1).Replace(Environment.NewLine, "");
                btInstall.Text = SplittedFase[91 + 1].Substring(1).Replace(Environment.NewLine, "");
                checking = SplittedFase[88 + 1].Substring(1).Replace(Environment.NewLine, "");
                uptodate = SplittedFase[89 + 1].Substring(1).Replace(Environment.NewLine, "");
                installStatus = SplittedFase[92 + 1].Substring(1).Replace(Environment.NewLine, "");
                StatusType = SplittedFase[93 + 1].Substring(1).Replace(Environment.NewLine, "");
                radioButton1.Text = SplittedFase[4 + 1].Substring(1).Replace(Environment.NewLine, "");
                if (updateProgress == 0)
                {
                    lbUpdateStatus.Text = SplittedFase[88 + 1].Substring(1).Replace(Environment.NewLine, "");
                }
                else if (updateProgress == 1)
                {
                    lbUpdateStatus.Text = SplittedFase[89 + 1].Substring(1).Replace(Environment.NewLine, "");
                }
                else if (updateProgress == 2)
                {
                    lbUpdateStatus.Text = SplittedFase[87 + 1].Substring(1).Replace(Environment.NewLine, "");
                }
                else if (updateProgress == 3)
                {
                    lbUpdateStatus.Text = SplittedFase[3 + 1].Substring(1).Replace(Environment.NewLine, "");
                }
                updateavailable = SplittedFase[87 + 1].Substring(1).Replace(Environment.NewLine, ""); ;
                privatemode = SplittedFase[0 + 1].Substring(1).Replace(Environment.NewLine, "");
                updateTitle = SplittedFase[1 + 1].Substring(1).Replace(Environment.NewLine, "");
                updateMessage = SplittedFase[2 + 1].Substring(1).Replace(Environment.NewLine, "");
                updateError = SplittedFase[3 + 1].Substring(1).Replace(Environment.NewLine, "");
                NewTabtitle = SplittedFase[4 + 1].Substring(1).Replace(Environment.NewLine, "");
                customSearchNote = SplittedFase[5 + 1].Substring(1).Replace(Environment.NewLine, "");
                customSearchMessage = SplittedFase[6 + 1].Substring(1).Replace(Environment.NewLine, "");
                label12.Text = SplittedFase[16 + 1].Substring(1).Replace(Environment.NewLine, "");
                newWindow = SplittedFase[7 + 1].Substring(1).Replace(Environment.NewLine, "");
                newincognitoWindow = SplittedFase[8 + 1].Substring(1).Replace(Environment.NewLine, "");
                label6.Text = SplittedFase[38 + 1].Substring(1).Replace(Environment.NewLine, "");
                downloadsToolStripMenuItem.Text = SplittedFase[38 + 1].Substring(1).Replace(Environment.NewLine, "");
                aboutToolStripMenuItem.Text = SplittedFase[18 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbHomepage.Text = SplittedFase[11 + 1].Substring(1).Replace(Environment.NewLine, "");
                SearchOnPage = SplittedFase[58 + 1].Substring(1).Replace(Environment.NewLine, "");
                label26.Text = SplittedFase[13 + 1].Substring(1).Replace(Environment.NewLine, "");
                tsThemes.Text = SplittedFase[94 + 1].Substring(1).Replace(Environment.NewLine, "");
                caseSensitiveToolStripMenuItem.Text = SplittedFase[59 + 1].Substring(1).Replace(Environment.NewLine, "");
                customToolStripMenuItem.Text = SplittedFase[14 + 1].Substring(1).Replace(Environment.NewLine, "");
                removeSelectedToolStripMenuItem.Text = SplittedFase[15 + 1].Substring(1).Replace(Environment.NewLine, "");
                clearToolStripMenuItem.Text = SplittedFase[17 + 1].Substring(1).Replace(Environment.NewLine, "");
                settingstitle = SplittedFase[9 + 1].Substring(1).Replace(Environment.NewLine, "");
                historyToolStripMenuItem.Text = SplittedFase[10 + 1].Substring(1).Replace(Environment.NewLine, "");
                label4.Text = SplittedFase[10 + 1].Substring(1).Replace(Environment.NewLine, "");
                label22.Text = SplittedFase[50 + 1].Substring(1).Replace(Environment.NewLine, "");
                goBack = SplittedFase[19 + 1].Substring(1).Replace(Environment.NewLine, "");
                goForward = SplittedFase[20 + 1].Substring(1).Replace(Environment.NewLine, "");
                refresh = SplittedFase[21 + 1].Substring(1).Replace(Environment.NewLine, "");
                refreshNoCache = SplittedFase[22 + 1].Substring(1).Replace(Environment.NewLine, "");
                stop = SplittedFase[23 + 1].Substring(1).Replace(Environment.NewLine, "");
                selectAll = SplittedFase[24 + 1].Substring(1).Replace(Environment.NewLine, "");
                openLinkInNewTab = SplittedFase[25 + 1].Substring(1).Replace(Environment.NewLine, "");
                copyLink = SplittedFase[26 + 1].Substring(1).Replace(Environment.NewLine, "");
                saveImageAs = SplittedFase[27 + 1].Substring(1).Replace(Environment.NewLine, "");
                openImageInNewTab = SplittedFase[28 + 1].Substring(1).Replace(Environment.NewLine, "");
                paste = SplittedFase[29 + 1].Substring(1).Replace(Environment.NewLine, "");
                copy = SplittedFase[30 + 1].Substring(1).Replace(Environment.NewLine, "");
                cut = SplittedFase[31 + 1].Substring(1).Replace(Environment.NewLine, "");
                undo = SplittedFase[32 + 1].Substring(1).Replace(Environment.NewLine, "");
                redo = SplittedFase[33 + 1].Substring(1).Replace(Environment.NewLine, "");
                delete = SplittedFase[34 + 1].Substring(1).Replace(Environment.NewLine, "");
                SearchOrOpenSelectedInNewTab = SplittedFase[35 + 1].Substring(1).Replace(Environment.NewLine, "");
                developerTools = SplittedFase[36 + 1].Substring(1).Replace(Environment.NewLine, "");
                viewSource = SplittedFase[37 + 1].Substring(1).Replace(Environment.NewLine, "");
                restoreOldSessions = SplittedFase[82 + 1].Substring(1).Replace(Environment.NewLine, "");
                label14.Text = SplittedFase[39 + 1].Substring(1).Replace(Environment.NewLine, "");
                enterAValidUrl = SplittedFase[80 + 1].Substring(1).Replace(Environment.NewLine, "");
                label16.Text = SplittedFase[40 + 1].Substring(1).Replace(Environment.NewLine, "");
                chDate.Text = SplittedFase[45 + 1].Substring(1).Replace(Environment.NewLine, "");
                chDateHistory.Text = SplittedFase[45 + 1].Substring(1).Replace(Environment.NewLine, "");
                chTitle.Text = SplittedFase[235 + 1].Substring(1).Replace(Environment.NewLine, "");
                chURL.Text = SplittedFase[236 + 1].Substring(1).Replace(Environment.NewLine, "");
                fromtwodot = SplittedFase[47 + 1].Substring(1).Replace(Environment.NewLine, "");
                chFrom.Text = SplittedFase[46 + 1].Substring(1).Replace(Environment.NewLine, "");
                totwodot = SplittedFase[48 + 1].Substring(1).Replace(Environment.NewLine, "");
                korotdownloading = SplittedFase[41 + 1].Substring(1).Replace(Environment.NewLine, "");
                chTo.Text = SplittedFase[44 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbOpen.Text = SplittedFase[42 + 1].Substring(1).Replace(Environment.NewLine, "");
                open = SplittedFase[43 + 1].Substring(1).Replace(Environment.NewLine, "");
                openLinkİnNewTabToolStripMenuItem.Text = SplittedFase[25 + 1].Substring(1).Replace(Environment.NewLine, "");
                openInNewTab = SplittedFase[55 + 1].Substring(1).Replace(Environment.NewLine, "");
                removeSelectedTSMI.Text = SplittedFase[15 + 1].Substring(1).Replace(Environment.NewLine, "");
                clearTSMI.Text = SplittedFase[17 + 1].Substring(1).Replace(Environment.NewLine, "");
                openFileToolStripMenuItem.Text = SplittedFase[56 + 1].Substring(1).Replace(Environment.NewLine, "");
                openFileİnExplorerToolStripMenuItem.Text = SplittedFase[57 + 1].Substring(1).Replace(Environment.NewLine, "");
                removeSelectedToolStripMenuItem1.Text = SplittedFase[15 + 1].Substring(1).Replace(Environment.NewLine, "");
                clearToolStripMenuItem2.Text = SplittedFase[17 + 1].Substring(1).Replace(Environment.NewLine, "");
                DefaultProxyts.Text = SplittedFase[53 + 1].Substring(1).Replace(Environment.NewLine, "");
                label13.Text = SplittedFase[51 + 1].Substring(1).Replace(Environment.NewLine, "");
                Yes = SplittedFase[83 + 1].Substring(1).Replace(Environment.NewLine, "");
                No = SplittedFase[84 + 1].Substring(1).Replace(Environment.NewLine, "");
                OK = SplittedFase[85 + 1].Substring(1).Replace(Environment.NewLine, "");
                Cancel = SplittedFase[86 + 1].Substring(1).Replace(Environment.NewLine, "");
                button10.Text = SplittedFase[52 + 1].Substring(1).Replace(Environment.NewLine, "");
                label15.Text = SplittedFase[54 + 1].Substring(1).Replace(Environment.NewLine, "");
                SearchOnWeb = SplittedFase[79 + 1].Substring(1).Replace(Environment.NewLine, "");
                goTotxt = SplittedFase[78 + 1].Substring(1).Replace(Environment.NewLine, "");
                newProfileInfo = SplittedFase[81 + 1].Substring(1).Replace(Environment.NewLine, "");
                lbSearchEngine.Text = SplittedFase[12 + 1].Substring(1).Replace(Environment.NewLine, "");
                MonthNames = SplittedFase[61 + 1].Substring(1).Replace(Environment.NewLine, "");
                DayNames = SplittedFase[60 + 1].Substring(1).Replace(Environment.NewLine, "");
                SearchHelpText = SplittedFase[62 + 1].Substring(1).Replace(Environment.NewLine, "");
                ErrorPageTitle = SplittedFase[49 + 1].Substring(1).Replace(Environment.NewLine, "");
                KT = SplittedFase[63 + 1].Substring(1).Replace(Environment.NewLine, "");
                ET = SplittedFase[64 + 1].Substring(1).Replace(Environment.NewLine, "");
                E1 = SplittedFase[65 + 1].Substring(1).Replace(Environment.NewLine, "");
                E2 = SplittedFase[66 + 1].Substring(1).Replace(Environment.NewLine, "");
                E3 = SplittedFase[67 + 1].Substring(1).Replace(Environment.NewLine, "");
                E4 = SplittedFase[68 + 1].Substring(1).Replace(Environment.NewLine, "");
                RT = SplittedFase[69 + 1].Substring(1).Replace(Environment.NewLine, "");
                R1 = SplittedFase[70 + 1].Substring(1).Replace(Environment.NewLine, "");
                R2 = SplittedFase[71 + 1].Substring(1).Replace(Environment.NewLine, "");
                R3 = SplittedFase[72 + 1].Substring(1).Replace(Environment.NewLine, "");
                R4 = SplittedFase[73 + 1].Substring(1).Replace(Environment.NewLine, "");
                Search = SplittedFase[74 + 1].Substring(1).Replace(Environment.NewLine, "");
                newprofile = SplittedFase[76 + 1].Substring(1).Replace(Environment.NewLine, "");
                switchTo = SplittedFase[75 + 1].Substring(1).Replace(Environment.NewLine, "");
                deleteProfile = SplittedFase[77 + 1].Substring(1).Replace(Environment.NewLine, "");
                RefreshTranslation();
                RefreshSizes();
            }
            else
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle, "This file does not suitable for this version of Korot.Please ask the creator of this language to update." + Environment.NewLine + " Error : Missing Line" + "[ Line Count: " + SplittedFase.Length + "]", Icon, MessageBoxButtons.OK, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140);
                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(" [KOROT] Error at applying a language file : [ Line Count: " + SplittedFase.Length + "]");
            }

        }
        public void RefreshLangList()
        {
            cbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.lang", SearchOption.TopDirectoryOnly))
            {
                cbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }
            cbLang.Text = Path.GetFileNameWithoutExtension(Properties.Settings.Default.LangFile);
        }
        #endregion
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputb = new HaltroyFramework.HaltroyInputBox(customSearchNote, customSearchMessage, Icon, Properties.Settings.Default.SearchURL, Properties.Settings.Default.BackColor, OK, Cancel, 400, 150);
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (ValidHttpURL(inputb.TextValue()) && !inputb.TextValue().StartsWith("korot://") && !inputb.TextValue().StartsWith("file://") && !inputb.TextValue().StartsWith("about"))
                {
                    Properties.Settings.Default.SearchURL = inputb.TextValue();
                    tbSearchEngine.Text = Properties.Settings.Default.SearchURL;
                }
                else
                {
                    customToolStripMenuItem_Click(null, null);
                }
            }
        }
        private void SearchEngineSelection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SearchURL = ((ToolStripMenuItem)sender).Tag.ToString();
            tbSearchEngine.Text = Properties.Settings.Default.SearchURL;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (tbHomepage.Text.ToLower().StartsWith("korot://newtab"))
            {
                radioButton1.Checked = true;
                Properties.Settings.Default.Homepage = tbHomepage.Text;
                if (!_Incognito) { Properties.Settings.Default.Save(); }
            }
            else
            {
                radioButton1.Checked = false;
                Properties.Settings.Default.Homepage = tbHomepage.Text;
                if (!_Incognito) { Properties.Settings.Default.Save(); }
            }
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            cmsSearchEngine.Show(MousePosition);
        }
        private void openmytaginnewtab(object sender, LinkLabelLinkClickedEventArgs e)
        {
            anaform().Invoke(new Action(() => anaform().CreateTab(((LinkLabel)sender).Tag.ToString())));
        }
        private void ClearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Favorites = "";
            if (!_Incognito) { Properties.Settings.Default.Save(); }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackColor = colorpicker.Color;
                Properties.Settings.Default.BackColor = colorpicker.Color;
                pictureBox3.BackColor = colorpicker.Color;
                ChangeTheme();
            }
            checkIfDefaultTheme();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.BackColor = colorpicker.Color;
                Properties.Settings.Default.OverlayColor = colorpicker.Color;
                pictureBox4.BackColor = colorpicker.Color;
                ChangeTheme();
            }
            checkIfDefaultTheme();
        }

        private void Label14_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Process.Start(Application.StartupPath + "//Themes//");
            }

        }

        private void Label7_Click_1(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.White;
            Properties.Settings.Default.BackColor = pictureBox3.BackColor;
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            ChangeTheme();
        }


        private void Label8_Click(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.DodgerBlue;
            Properties.Settings.Default.OverlayColor = pictureBox4.BackColor;
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            ChangeTheme();
        }

        public void RefreshDownloadList()
        {
            int selectedValue = hlvDownload.SelectedIndices.Count > 0 ? hlvDownload.SelectedIndices[0] : 0;
            ListViewItem scroll = hlvDownload.TopItem;
            hlvDownload.Items.Clear();
            if (anaform() != null)
            {
                foreach (DownloadItem item in anaform().CurrentDownloads)
                {
                    ListViewItem listV = new ListViewItem
                    {
                        Text = item.PercentComplete + "%"
                    };
                    listV.SubItems.Add(DateTime.Now.ToString("dd/MM/yy hh:mm:ss"));
                    listV.SubItems.Add(item.FullPath);
                    listV.SubItems.Add(item.Url);
                    hlvDownload.Items.Add(listV);
                }
            }
            string Playlist = Properties.Settings.Default.DowloadHistory;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while ((i != Count) && (Count > 3))
            {
                ListViewItem listV = new ListViewItem
                {
                    Text = SplittedFase[i].Replace(Environment.NewLine, "")
                };
                listV.SubItems.Add(SplittedFase[i + 1].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 2].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 3].Replace(Environment.NewLine, ""));
                hlvDownload.Items.Add(listV);
                i += 4;
            }
            hlvDownload.SelectedIndices.Clear();
            if (selectedValue < (hlvDownload.Items.Count - 1))
            {
                hlvDownload.Items[selectedValue].Selected = true;
                hlvDownload.TopItem = scroll;
            }
            hlvDownload.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        private void ListView2_DoubleClick(object sender, EventArgs e)
        {
            cmsDownload.Show(MousePosition);
        }
        private void OpenLinkİnNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anaform().Invoke(new Action(() => anaform().CreateTab(hlvDownload.SelectedItems[0].SubItems[2].Text)));
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(hlvDownload.SelectedItems[0].SubItems[3].Text);
        }

        private void OpenFileİnExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + hlvDownload.SelectedItems[0].SubItems[3].Text);
        }

        private void RemoveSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (hlvDownload.SelectedItems.Count > 0)
            {
                if (hlvDownload.SelectedItems[0].Text == "X" || hlvDownload.SelectedItems[0].Text == "✓")
                {
                    string x = hlvDownload.SelectedItems[0].Text + ";" + hlvDownload.SelectedItems[0].SubItems[1].Text + ";" + hlvDownload.SelectedItems[0].SubItems[2].Text + ";" + hlvDownload.SelectedItems[0].SubItems[3].Text + ";";
                    Properties.Settings.Default.DowloadHistory = Properties.Settings.Default.DowloadHistory.Replace(x, "");
                }
                else { anaform().CancelledDownloads.Add(hlvDownload.SelectedItems[0].SubItems[3].Text); }
                if (!_Incognito) { Properties.Settings.Default.Save(); }
                RefreshDownloadList();
            }
        }

        private void ClearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DowloadHistory = "";
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            RefreshDownloadList();
        }

        private void HlvHistory_DoubleClick(object sender, EventArgs e)
        {
            if (hlvHistory.SelectedItems.Count > 0)
            {
                anaform().Invoke(new Action(() => anaform().CreateTab(hlvHistory.SelectedItems[0].SubItems[2].Text)));
            }
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void NewIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "-incognito");
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackStyle = "BACKCOLOR";
            textBox4.Text = usingBC;
            colorToolStripMenuItem.Checked = true;
            checkIfDefaultTheme();
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about\:\/\/)|(?:about\:\/\/)|(?:file\:\/\/)|(?:https\:\/\/)|(?:korot\:\/\/)|(?:http:\/\/)|(?:\:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex Rgx2 = new Regex(@"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx2.IsMatch(s) || Rgx.IsMatch(s);
        }
        private void FromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputbox = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                            enterAValidCode,
                                                                                            Icon,
                                                                                            "",
                                                                                            Properties.Settings.Default.BackColor,
                                                                                            OK, Cancel, 400, 150);
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackStyle = inputbox.TextValue() + ";";
                textBox4.Text = Properties.Settings.Default.BackStyle;
                colorToolStripMenuItem.Checked = false;
            }
            checkIfDefaultTheme();
        }

        private void FromLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog
            {
                Filter = imageFiles + "|*.jpg;*.png;*.bmp;*.jpeg;*.jfif;*.gif;*.apng;*.ico;*.svg;*.webp|" + allFiles + "|*.*",
                Title = selectBackImage,
                Multiselect = false
            };
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                if (FileSystem2.ImageToBase64(Image.FromFile(filedlg.FileName)).Length <= 131072)
                {
                    string imageType = Path.GetExtension(filedlg.FileName).Replace(".", "");
                    Properties.Settings.Default.BackStyle = "background-image: url('data:image/" + imageType + ";base64," + FileSystem2.ImageToBase64(Image.FromFile(filedlg.FileName)) + "');";
                    textBox4.Text = Properties.Settings.Default.BackStyle;
                    colorToolStripMenuItem.Checked = false;
                }
                else
                {
                    FromLocalFileToolStripMenuItem_Click(sender, e);
                }
            }
            checkIfDefaultTheme();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Properties.Settings.Default.Homepage = "korot://newtab";
                tbHomepage.Text = Properties.Settings.Default.Homepage;
            }
        }

        private void tmrRefresher_Tick(object sender, EventArgs e)
        {
            hsDoNotTrack.Checked = Properties.Settings.Default.DoNotTrack;
            tbHomepage.Text = Properties.Settings.Default.Homepage;
            radioButton1.Checked = Properties.Settings.Default.Homepage == "korot://newtab";
            tbSearchEngine.Text = Properties.Settings.Default.SearchURL;
            hsProxy.Checked = Properties.Settings.Default.rememberLastProxy;
            RefreshLangList();
            refreshThemeList();
            RefreshDownloadList();
            RefreshHistory();
            RefreshCookies();
        }

        private void label15_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                refreshThemeList();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + "\"");
            }
        }


        private void btInstall_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "-update");
            Application.Exit();
        }

        private void btUpdater_Click(object sender, EventArgs e)
        {
            if (UpdateWebC.IsBusy) { UpdateWebC.CancelAsync(); }
            UpdateWebC.DownloadStringAsync(new Uri("https://haltroy.com/Update/Korot.htupdate"));
            updateProgress = 0;
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshHistory();
            RefreshDownloadList();
        }
        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string x = hlvHistory.SelectedItems[0].SubItems[0].Text + ";";
            string y = hlvHistory.SelectedItems[0].SubItems[1].Text + ";";
            string t = hlvHistory.SelectedItems[0].SubItems[2].Text + ";";
            Properties.Settings.Default.History = Properties.Settings.Default.History.Replace(x + y + t, "");
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            RefreshHistory();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.History = "";
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            RefreshHistory();
        }



        private void Label2_Click(object sender, EventArgs e)
        {
            NewTab("https://haltroy.com/Korot.html");
        }

        private void checkIfDefaultTheme()
        {
            if (pictureBox3.BackColor == themeDefaultBackColor &&
                pictureBox4.BackColor == themeDefaultOverlayColor &&
                comboBox3.SelectedIndex == themeDefaultLayout &&
                dudNewTab.SelectedIndex == themeDefaultNTC &&
                dudClose.SelectedIndex == themeDefaultCBC &&
                ((themeDefaultBackstyle == "BACKCOLOR" && textBox4.Text == usingBC) || textBox4.Text == themeDefaultBackstyle))
            {
                Properties.Settings.Default.ThemeFile = appliedTheme;
                comboBox1.Text = themeName + ".ktf";
                Properties.Settings.Default.ThemeAuthor = themeOwner;
                Properties.Settings.Default.ThemeName = themeName;
            }
            else
            {
                comboBox1.Text = "";
                Properties.Settings.Default.ThemeAuthor = "";
                Properties.Settings.Default.ThemeName = "";
            }
        }

        private string themeOwner;
        private string themeName;
        private string appliedTheme;
        private Color themeDefaultBackColor;
        private Color themeDefaultOverlayColor;
        private string themeDefaultBackstyle;
        private int themeDefaultLayout;
        private int themeDefaultNTC;
        private int themeDefaultCBC;
        public void LoadTheme(string themeFile)
        {
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string Playlist3 = FileSystem2.ReadFile(themeFile, Encoding.UTF8);
            string[] SplittedFase3 = Playlist3.Split(token);
            if (!(SplittedFase3.Length < 9))
            {
                if (SplittedFase3[6].Substring(2).Replace(Environment.NewLine, "").Length > 131072) { return; }
                Properties.Settings.Default.BackColor = Tools.HexToColor(SplittedFase3[0].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.OverlayColor = Tools.HexToColor(SplittedFase3[1].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.BackStyle = SplittedFase3[2].Substring(1).Replace(Environment.NewLine, "");
                Properties.Settings.Default.BStyleLayout = Convert.ToInt32(SplittedFase3[3].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.newTabColor = Convert.ToInt32(SplittedFase3[4].Substring(1).Replace(Environment.NewLine, ""));
                Properties.Settings.Default.closeColor = Convert.ToInt32(SplittedFase3[5].Substring(1).Replace(Environment.NewLine, ""));
                Properties.Settings.Default.ThemeName = SplittedFase3[6].Substring(1).Replace(Environment.NewLine, "");
                Properties.Settings.Default.ThemeAuthor = SplittedFase3[7].Substring(1).Replace(Environment.NewLine, "");
                appliedTheme = themeFile;
                themeOwner = Properties.Settings.Default.ThemeAuthor;
                themeName = Properties.Settings.Default.ThemeName;
                themeDefaultBackColor = Properties.Settings.Default.BackColor;
                themeDefaultOverlayColor = Properties.Settings.Default.OverlayColor;
                themeDefaultBackstyle = Properties.Settings.Default.BackStyle;
                themeDefaultLayout = Properties.Settings.Default.BStyleLayout;
                themeDefaultNTC = Properties.Settings.Default.newTabColor;
                themeDefaultCBC = Properties.Settings.Default.closeColor;
                pictureBox3.BackColor = Properties.Settings.Default.BackColor;
                pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
                Properties.Settings.Default.ThemeFile = themeFile;
                textBox4.Text = Properties.Settings.Default.BackStyle == "BACKCOLOR" ? usingBC : Properties.Settings.Default.BackStyle;
                ChangeTheme();
                RefreshTranslation();
                RefreshSizes();
            }
            else
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot"))
                {
                    Process.Start(Application.ExecutablePath, "-oobe");
                    Application.Exit();
                }
                else
                {
                    Tools.createThemes();
                }
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle,
                                                                                          ErrorTheme,
                                                                                          Icon,
                                                                                          MessageBoxButtons.OK,
                                                                                          Properties.Settings.Default.BackColor,
                                                                                          Yes, No, OK, Cancel, 390, 140);

                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(" [KOROT] Error at applying a theme : [Line Count:" + SplittedFase3.Length + "]");
                LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf");

            }
        }
        public void refreshThemeList()
        {
            int savedValue = listBox2.SelectedIndex;
            int scroll = listBox2.TopIndex;
            listBox2.Items.Clear();
            foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"))
            {
                if (x.EndsWith(".ktf", StringComparison.OrdinalIgnoreCase))
                {
                    listBox2.Items.Add(new FileInfo(x).Name);
                }
            }
            if (savedValue <= (listBox2.Items.Count - 1))
            {
                listBox2.SelectedIndex = savedValue;
                listBox2.TopIndex = scroll;
            }
        }


        private void Button12_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter objWriter3;
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            objWriter3 = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf");
            string x = Properties.Settings.Default.BackStyle;
            string lol = Properties.Settings.Default.BackColor.R
                + Environment.NewLine
                + Properties.Settings.Default.BackColor.G
                + Environment.NewLine
                + Properties.Settings.Default.BackColor.B
                + Environment.NewLine
                + Properties.Settings.Default.OverlayColor.R
                + Environment.NewLine
                + Properties.Settings.Default.OverlayColor.G
                + Environment.NewLine
                + Properties.Settings.Default.OverlayColor.B
                + Environment.NewLine
                + x
                + Environment.NewLine
                + Properties.Settings.Default.BStyleLayout
                + Environment.NewLine
                + comboBox1.Text
                + Environment.NewLine
                + userName
                + Environment.NewLine;
            objWriter3.WriteLine(lol);
            objWriter3.Close();
            Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf";
            refreshThemeList();
        }

        private readonly WebClient UpdateWebC = new WebClient();
        public void Updater()
        {
            UpdateWebC.DownloadStringCompleted += Updater_DownloadStringCompleted;
            UpdateWebC.DownloadProgressChanged += updater_checking;
            UpdateWebC.DownloadStringAsync(new Uri("https://haltroy.com/Update/Korot.htupdate"));
            updateProgress = 0;
        }

        private bool alreadyCheckedForUpdatesOnce = false;
        private void updater_checking(object sender, DownloadProgressChangedEventArgs e)
        {
            lbUpdateStatus.Text = checking;
            updateProgress = 0;
            btUpdater.Visible = false;
        }
        private void Updater_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                updateProgress = 3;
                btInstall.Visible = false;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = updateError;
            }
            else
            {
                UpdateResult(e.Result);
            }
        }

        private void UpdateResult(string info)
        {
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase3 = info.Split(token);
            Version newest = new Version(SplittedFase3[0].Replace(Environment.NewLine, ""));
            Version current = new Version(Application.ProductVersion);
            if (newest > current)
            {
                if (alreadyCheckedForUpdatesOnce || Properties.Settings.Default.dismissUpdate || _Incognito)
                {
                    updateProgress = 2;
                    lbUpdateStatus.Text = updateavailable;
                    btUpdater.Visible = true;
                    btInstall.Visible = true;
                }
                else
                {
                    alreadyCheckedForUpdatesOnce = true;
                    updateProgress = 2;
                    lbUpdateStatus.Text = updateavailable;
                    btInstall.Visible = true;
                    btUpdater.Visible = true;
                    HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(
                        updateTitle,
                        updateMessage,
                        Icon,
                        MessageBoxButtons.YesNo,
                        Properties.Settings.Default.BackColor,
                        Yes,
                        No,
                        OK,
                        Cancel,
                        390,
                        140);
                    DialogResult diagres = mesaj.ShowDialog();
                    if (diagres == DialogResult.Yes)
                    {
                        if (Application.OpenForms.OfType<Form1>().Count() < 1)
                        {
                            Process.Start(Application.ExecutablePath, "-update");
                        }
                        else
                        {
                            foreach (Form1 x in Application.OpenForms)
                            {
                                x.Focus();
                            }
                        }
                    }
                    Properties.Settings.Default.dismissUpdate = true;
                }
            }
            else
            {
                btUpdater.Visible = true;
                btInstall.Visible = false;
                updateProgress = 1;
                lbUpdateStatus.Text = uptodate;
            }
        }

        private Font CreateFont(string fontFile, float size, FontStyle style)
        {
            using (PrivateFontCollection pfc = new PrivateFontCollection())
            {
                pfc.AddFontFile(fontFile);
                using (FontFamily fontFamily = new FontFamily(pfc.Families[0].Name, pfc))
                {
                    return new Font(fontFamily, size, style);
                }
            }
        }
        public TitleBarTabs ParentTabs => (ParentForm as TitleBarTabs);
        private async void SetProxy(ChromiumWebBrowser cwb, string Address)
        {
            if (Address == null) { }
            else
            {
                await Cef.UIThreadTaskFactory.StartNew(delegate
                {
                    IRequestContext rc = cwb.GetBrowser().GetHost().RequestContext;
                    Dictionary<string, object> v = new Dictionary<string, object>
                    {
                        ["mode"] = "fixed_servers",
                        ["server"] = Address
                    };
                    bool success = rc.SetPreference("proxy", v, out string error);
                });
            }
        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings
            {
                UserAgent = "Mozilla/5.0 ( Windows "
                + Tools.getOSInfo()
                + "; "
                + (Environment.Is64BitProcess ? "WOW64" : "Win32NT")
                + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/"
                + Cef.ChromiumVersion
                + " Safari/537.36 Korot/"
                + Application.ProductVersion.ToString()
            };
            if (_Incognito) { settings.CachePath = null; settings.PersistSessionCookies = false; settings.RootCachePath = null; }
            else { settings.CachePath = userCache; settings.RootCachePath = userCache; }
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "korot",
                SchemeHandlerFactory = new SchemeHandlerFactory(this)
                {
                    extKEM = "",
                    isExt = false,
                    extForm = null
                }
            });
            // Initialize cef with the provided settings
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            chromiumWebBrowser1 = new ChromiumWebBrowser(loaduri);
            pCEF.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.ConsoleMessage += cef_consoleMessage;
            chromiumWebBrowser1.FindHandler = new FindHandler(this);
            chromiumWebBrowser1.KeyboardHandler = new KeyboardHandler(this);
            chromiumWebBrowser1.RequestHandler = new RequestHandlerKorot(this);
            chromiumWebBrowser1.DisplayHandler = new DisplayHandler(this);
            chromiumWebBrowser1.LoadingStateChanged += loadingstatechanged;
            chromiumWebBrowser1.TitleChanged += cef_TitleChanged;
            chromiumWebBrowser1.AddressChanged += cef_AddressChanged;
            chromiumWebBrowser1.LoadError += cef_onLoadError;
            chromiumWebBrowser1.KeyDown += tabform_KeyDown;
            chromiumWebBrowser1.MenuHandler = new ContextMenuHandler(this);
            chromiumWebBrowser1.LifeSpanHandler = new BrowserLifeSpanHandler(this);
            chromiumWebBrowser1.DownloadHandler = new DownloadHandler(this);
            chromiumWebBrowser1.JsDialogHandler = new JsHandler(this);
            chromiumWebBrowser1.DialogHandler = new MyDialogHandler();
            chromiumWebBrowser1.MouseWheel += MouseScroll;
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
            if (defaultProxy != null && Properties.Settings.Default.rememberLastProxy && !string.IsNullOrWhiteSpace(Properties.Settings.Default.LastProxy))
            {
                SetProxy(chromiumWebBrowser1, Properties.Settings.Default.LastProxy);
            }
            executeStartupExtensions();

        }
        private void cef_consoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            Output.WriteLine(" [Korot.ConsoleMessage] Message received: [Line: " + e.Line + "Level: " + e.Level + " Source: " + e.Source + " Message:" + e.Message + "]");
        }
        public void updateExtensions()
        {
            if (Properties.Settings.Default.alreadyUpdatedExt) { return; }
            if (Properties.Settings.Default.allowUnknownResources)
            {
                foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\", "*.*", SearchOption.AllDirectories))
                {
                    if (x.EndsWith("\\ext.kem", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(5, 1) == "1")
                        {
                            frmUpdateExt extUpdate = new frmUpdateExt(x, false)
                            {
                                Text = updateTitleExt
                            };
                            extUpdate.label1.Text = updateExtInfo
    .Replace("[NAME]", SplittedFase[0].Substring(0).Replace(Environment.NewLine, ""))
    .Replace("[NEWLINE]", Environment.NewLine);
                            extUpdate.infoTemp = StatusType;
                            extUpdate.Show();
                        }
                    }
                }
            }
            else
            {
                foreach (string y in Properties.Settings.Default.registeredExtensions)
                {
                    string x = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        + "\\Korot\\Extensions\\"
                        + y
                        + "\\ext.kem";
                    string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(5, 1) == "1")
                    {
                        frmUpdateExt extUpdate = new frmUpdateExt(x, false)
                        {
                            Text = updateTitleExt
                        };
                        extUpdate.label1.Text = updateExtInfo
                            .Replace("[NAME]", SplittedFase[0].Substring(0).Replace(Environment.NewLine, ""))
                            .Replace("[NEWLINE]", Environment.NewLine);
                        extUpdate.infoTemp = StatusType;
                        extUpdate.Show();
                    }
                }
            }
            Properties.Settings.Default.alreadyUpdatedExt = true;
        }
        public void updateThemes()
        {
            if (Properties.Settings.Default.alreadyUpdatedThemes) { return; }
            foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\", "*.*", SearchOption.AllDirectories))
            {
                if (x.EndsWith(".ktf", StringComparison.CurrentCultureIgnoreCase))
                {
                    string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    if (SplittedFase[9].Substring(1).Replace(Environment.NewLine, "") == "1")
                    {
                        frmUpdateExt extUpdate = new frmUpdateExt(x, true)
                        {
                            Text = updateTitleTheme
                        };
                        extUpdate.label1.Text = updateExtInfo
.Replace("[NAME]", SplittedFase[0].Substring(0).Replace(Environment.NewLine, ""))
.Replace("[NEWLINE]", Environment.NewLine);
                        extUpdate.infoTemp = StatusType;
                        extUpdate.Show();
                    }
                }
            }
            Properties.Settings.Default.alreadyUpdatedThemes = true;
        }


        public void executeStartupExtensions()
        {
            if (Properties.Settings.Default.allowUnknownResources)
            {
                foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\", "*.*", SearchOption.AllDirectories))
                {
                    if (x.EndsWith("\\ext.kem", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1" && (new FileInfo(x).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\")).Length < 5242880))
                        {
                            chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\"), Encoding.UTF8));
                        }
                    }
                }
            }
            else
            {
                foreach (string y in Properties.Settings.Default.registeredExtensions)
                {
                    string x = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        + "\\Korot\\Extensions\\"
                        + y
                        + "\\ext.kem";
                    string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1" && (new FileInfo(x).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\")).Length < 5242880))
                    {
                        chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\"), Encoding.UTF8));
                    }
                }
            }
        }
        public void executeBackgroundExtensions()
        {
            if (Properties.Settings.Default.allowUnknownResources)
            {
                foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\", "*.*", SearchOption.AllDirectories))
                {
                    if (x.EndsWith("\\ext.kem", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        if (SplittedFase.Length >= 11)
                        {
                            if (SplittedFase[8].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1" && (new FileInfo(x).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\")).Length < 5242880))
                            {
                                chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\"), Encoding.UTF8));
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (string y in Properties.Settings.Default.registeredExtensions)
                {
                    string x = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
                        + "\\Korot\\Extensions\\"
                        + y
                        + "\\ext.kem";
                    string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    if (SplittedFase.Length >= 11)
                    {
                        if (SplittedFase[8].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1" && (new FileInfo(x).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\")).Length < 5242880))
                        {
                            chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\"), Encoding.UTF8));
                        }
                    }
                }
            }
        }

        public bool certError = false;
        public bool cookieUsage = false;
        public void ChangeStatus(string status)
        {
            label2.Text = status;
        }

        public void loadingstatechanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
            {
                certError = false;
                cookieUsage = false;
                pictureBox2.Invoke(new Action(() => pictureBox2.Image = Properties.Resources.lockg));
                Invoke(new Action(() => showCertificateErrorsToolStripMenuItem.Tag = null));
                Invoke(new Action(() => showCertificateErrorsToolStripMenuItem.Visible = false));
                Invoke(new Action(() => safeStatusToolStripMenuItem.Text = CertificateOKTitle));
                Invoke(new Action(() => ınfoToolStripMenuItem.Text = CertificateOK));
                Invoke(new Action(() => cookieInfoToolStripMenuItem.Text = notUsesCookies));
                if (Tools.Brightness(Properties.Settings.Default.BackColor) > 130)
                {
                    button2.Image = Korot.Properties.Resources.cancel;
                }
                else { button2.Image = Korot.Properties.Resources.cancel_w; }
            }
            else
            {
                if (_Incognito) { }
                else
                {
                    Invoke(new Action(() => Korot.Properties.Settings.Default.History += DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + Text + ";" + (tbAddress.Text) + ";"));

                }
                executeBackgroundExtensions();
                if (Tools.Brightness(Properties.Settings.Default.BackColor) > 130)
                {
                    button2.Image = Korot.Properties.Resources.refresh;
                }
                else
                { button2.Image = Korot.Properties.Resources.refresh_w; }
            }
            if (onCEFTab)
            {
                if (!e.Browser.IsDisposed)
                {
                    button1.Invoke(new Action(() => button1.Enabled = e.CanGoBack));
                    button3.Invoke(new Action(() => button3.Enabled = e.CanGoForward));
                }
                else
                {
                    button1.Invoke(new Action(() => button1.Enabled = false));
                    button3.Invoke(new Action(() => button3.Enabled = false));
                }
            }
            isLoading = e.IsLoading;
            if (Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser1.Address))
            {
                disallowThisPageForCookieAccessToolStripMenuItem.Text = allowCookie;
            }
            else
            {
                disallowThisPageForCookieAccessToolStripMenuItem.Text = disallowCookie;
            }
        }

        public bool OnJSAlert(string url, string message)
        {
            HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(JSAlert.Replace("[TITLE]", Text).Replace("[URL]", url), message, anaform().Icon, System.Windows.Forms.MessageBoxButtons.OKCancel, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            mesaj.ShowDialog();
            return true;
        }


        public bool OnJSConfirm(string url, string message, out bool returnval)
        {
            HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(JSConfirm.Replace("[TITLE]", Text).Replace("[URL]", url), message, anaform().Icon, System.Windows.Forms.MessageBoxButtons.OKCancel, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (mesaj.ShowDialog() == DialogResult.OK) { returnval = true; } else { returnval = false; }
            return true;
        }


        public bool OnJSPrompt(string url, string message, string defaultValue, out bool returnval, out string textresult)
        {
            HaltroyFramework.HaltroyInputBox mesaj = new HaltroyFramework.HaltroyInputBox(url, message, anaform().Icon, defaultValue, Properties.Settings.Default.BackColor, OK, Cancel, 400, 150)
            {
                StartPosition = FormStartPosition.CenterParent
            };
            if (mesaj.ShowDialog() == DialogResult.OK) { returnval = true; } else { returnval = false; }
            textresult = mesaj.TextValue();
            return true;
        }
        public void NewTab(string url)
        {
            anaform().Invoke(new Action(() => { anaform().CreateTab(url); anaform().SelectedTabIndex = anaform().Tabs.Count - 1; }));
        }

        private bool isFavMenuHidden = false;

        private void showFavMenu()
        {
            panel2.Height += mFavorites.Height;
            tabControl1.Height -= mFavorites.Height;
            tabControl1.Top += mFavorites.Height;
            mFavorites.Visible = true;
            isFavMenuHidden = false;
        }

        private void hideFavMenu()
        {
            panel2.Height -= mFavorites.Height;
            tabControl1.Height += mFavorites.Height;
            tabControl1.Top -= mFavorites.Height;
            mFavorites.Visible = false;
            isFavMenuHidden = true;
        }

        private ToolStripMenuItem selectedFavorite = null;
        public void RefreshFavorites()
        {
            mFavorites.Items.Clear();
            LoadDynamicMenu();
            if (mFavorites.Items.Count < 1)
            {
                if (!isFavMenuHidden)
                {
                    hideFavMenu();
                }
            }
            else
            {
                if (Properties.Settings.Default.showFav)
                {
                    if (isFavMenuHidden)
                    {
                        showFavMenu();
                    }
                }
                else
                {
                    if (!isFavMenuHidden)
                    {
                        hideFavMenu();
                    }
                }
            }
        }
        private void miFavorite_MouseDown(object sender, MouseEventArgs e)
        {
            itemClicked = true;
            if (e.Button == MouseButtons.Left) //Open it
            {
                chromiumWebBrowser1.Load(((ToolStripMenuItem)sender).Tag.ToString());
            }
            else if (e.Button == MouseButtons.Right) //Show menu
            {
                selectedFavorite = (ToolStripMenuItem)sender;
                cmsFavorite.Show(MousePosition);
            }
        }
        private void tabform_Load(object sender, EventArgs e)
        {
            Updater();
            if (File.Exists(Properties.Settings.Default.ThemeFile))
            {
                comboBox1.Text = new FileInfo(Properties.Settings.Default.ThemeFile).Name.Replace(".ktf", "");
            }
            else
            {
                comboBox1.Text = "";
                Properties.Settings.Default.ThemeAuthor = "";
                Properties.Settings.Default.ThemeName = "";
            }
            tbHomepage.Text = Properties.Settings.Default.Homepage;
            tbSearchEngine.Text = Properties.Settings.Default.SearchURL;
            if (Properties.Settings.Default.Homepage == "korot://newtab") { radioButton1.Enabled = true; }
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshLangList();
            refreshThemeList();
            ChangeTheme();
            RefreshDownloadList();
            label18.Text = Application.ProductVersion.ToString();
            label17.Visible = Environment.Is64BitProcess;
            RefreshFavorites();
            LoadExt();
            RefreshProfiles();
            profilenameToolStripMenuItem.Text = userName;
            //caseSensitiveToolStripMenuItem.Text = CaseSensitive;
            showCertificateErrorsToolStripMenuItem.Text = showCertError;
            chromiumWebBrowser1.Select();
            hsDoNotTrack.Checked = Properties.Settings.Default.DoNotTrack;
            RefreshTranslation();
            RefreshSizes();
            if (_Incognito)
            {
                foreach (Control x in tpSettings.Controls)
                {
                    if (x != button13) { x.Enabled = false; }
                }
                btInstall.Enabled = false;
                btUpdater.Enabled = false;
                button9.Enabled = false;
                cmsBStyle.Enabled = false;
                cmsSearchEngine.Enabled = false;
                button7.Enabled = false;
                cmsHistory.Enabled = false;
                cmsProfiles.Enabled = false;
                removeSelectedToolStripMenuItem1.Enabled = false;
                clearToolStripMenuItem2.Enabled = false;
                removeSelectedToolStripMenuItem.Enabled = false;
                clearToolStripMenuItem.Enabled = false;
                disallowThisPageForCookieAccessToolStripMenuItem.Enabled = false;
                removeSelectedTSMI.Enabled = false;
                clearTSMI.Enabled = false;
                Properties.Settings.Default.BackColor = Color.FromArgb(255, 64, 64, 64);
                Properties.Settings.Default.OverlayColor = Color.DodgerBlue;
            }
            else
            {
                pictureBox1.Visible = false;
                tbAddress.Size = new Size(tbAddress.Size.Width + pictureBox1.Size.Width, tbAddress.Size.Height);
            }
            LoadLangFromFile(Properties.Settings.Default.LangFile);
            cbLang.Text = Path.GetFileNameWithoutExtension(Properties.Settings.Default.LangFile);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            string urlLower = tbAddress.Text.ToLower();
            if (ValidHttpURL(urlLower))
            {
                chromiumWebBrowser1.Load(urlLower);
            }

            else
            {
                chromiumWebBrowser1.Load(Properties.Settings.Default.SearchURL + urlLower);
                button1.Enabled = true;

            }
        }



        public void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tpCef) //CEF
            {
                chromiumWebBrowser1.Back();
            }
            else if (tabControl1.SelectedTab == tpCert) //Certificate Error Menu
            {
                chromiumWebBrowser1.Back();
                allowSwitching = true;
                tabControl1.SelectedTab = tpCef;
            }
            else if (tabControl1.SelectedTab == tpSettings
                     || tabControl1.SelectedTab == tpHistory
                     || tabControl1.SelectedTab == tpDownload
                     || tabControl1.SelectedTab == tpAbout
                     || tabControl1.SelectedTab == tpTheme) //Menu
            {
                allowSwitching = true;
                tabControl1.SelectedTab = tpCef;
            }

        }

        public void button3_Click(object sender, EventArgs e) { allowSwitching = true; tabControl1.SelectedTab = tpCef; chromiumWebBrowser1.Forward(); }

        private void button2_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            if (isLoading)
            {
                chromiumWebBrowser1.Stop();
            }
            else { chromiumWebBrowser1.Reload(); }
        }
        private void cef_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            Invoke(new Action(() => tbAddress.Text = e.Address));
            if (isPageFavorited(chromiumWebBrowser1.Address))
            {
                button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.star_on_w : Properties.Resources.star_on;
            }
            else
            {
                button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
            }
            if (!ValidHttpURL(e.Address))
            {
                chromiumWebBrowser1.Load(Properties.Settings.Default.SearchURL + e.Address);
            }
        }
        private void cef_onLoadError(object sender, LoadErrorEventArgs e)
        {
            if (e == null) //User Asked
            {
                chromiumWebBrowser1.Load("korot://error/?e=TEST");
            }
            else
            {
                if (e.Frame.IsMain)
                {
                    chromiumWebBrowser1.Load("korot://error/?e=" + e.ErrorText);
                }
                else
                {
                    e.Frame.LoadUrl("korot://error/?e=" + e.ErrorText);
                }
            }
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                chromiumWebBrowser1.Back();
            }
        }


        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            if (e.Title.Length < 101)
            {
                Invoke(new Action(() => Text = e.Title));
            }
            else
            {
                Invoke(new Action(() => Text = e.Title.Substring(0, 100)));
            }
            Parent.Invoke(new Action(() => Parent.Text = Text));
        }

        public void showHideSearchMenu()
        {
            if (cmsHamburger.Visible)
            {
                cmsHamburger.Close();
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
            }
            else
            {
                cmsHamburger.Show(button11, 0, 0);
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
            }
        }
        public void retrieveKey(int code)
        {
            if (code == 0) //BrowserBack
            {
                button1_Click(null, null);
            }
            else if (code == 1) //BrowserForward
            {
                button3_Click(null, null);
            }
            else if (code == 2) //BrowserRefresh
            {
                button2_Click(null, null);
            }
            else if (code == 3) //BrowserStop
            {
                button2_Click(null, null);
            }
            else if (code == 4) //BrowserHome
            {
                button5_Click(null, null);
            }
            else if (code == 5) //Fullscreen
            {
                tsFullscreen_Click(null, null);
            }
        }
        private void button5_Click(object sender, EventArgs e) { allowSwitching = true; tabControl1.SelectedTab = tpCef; chromiumWebBrowser1.Load(Properties.Settings.Default.Homepage); }

        public bool isControlKeyPressed = false;
        public void tabform_KeyDown(object sender, KeyEventArgs e)
        {

            isControlKeyPressed = e.Control;
            if (e.KeyData == Keys.BrowserBack)
            {
                button1_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserForward)
            {
                button3_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserRefresh)
            {
                button2_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserStop)
            {
                button2_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserHome)
            {
                button5_Click(null, null);
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed)
            {
                NewTab("korot://newtab");
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed && e.Shift)
            {
                Process.Start(Application.ExecutablePath, "-incognito");
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed && e.Alt)
            {
                Process.Start(Application.ExecutablePath);
            }
            else if (e.KeyCode == Keys.F && isControlKeyPressed)
            {
                showHideSearchMenu();
            }
            else if (e.KeyData == Keys.Enter)
            {
                button4_Click(null, null);
            }
            else if ((e.KeyData == Keys.Up || e.KeyData == Keys.PageUp) && isControlKeyPressed)
            {
                zoomInToolStripMenuItem_Click(sender, null);
            }
            else if ((e.KeyData == Keys.Down || e.KeyData == Keys.PageDown) && isControlKeyPressed)
            {
                zoomOutToolStripMenuItem_Click(sender, null);
            }
            else if (e.KeyData == Keys.PrintScreen && isControlKeyPressed)
            {
                takeScreenShot();
            }
            else if (e.KeyData == Keys.S && isControlKeyPressed)
            {
                savePage();
            }
            else if (isControlKeyPressed && e.Shift && e.KeyData == Keys.N)
            {
                NewIncognitoWindowToolStripMenuItem_Click(null, null);
            }
            else if (isControlKeyPressed && e.Alt && e.KeyData == Keys.N)
            {
                NewWindowToolStripMenuItem_Click(null, null);
            }
            else if (isControlKeyPressed && e.KeyData == Keys.N)
            {
                NewTab("korot://newtab");
            }
            else if (e.KeyData == Keys.F11)
            {
                tsFullscreen_Click(null, null);
            }
        }
        public void tsFullscreen_Click(object sender, EventArgs e)
        {
            anaform().Invoke(new Action(() => anaform().Fullscreenmode(!anaform().isFullScreen)));
        }
        private Image GetImageFromURL(string URL)
        {
            if (URL == "BACKCOLOR") { return null; }
            else
            {
                int virgulIndex = URL.IndexOf(',') + 1;
                string justB64Code = URL.Substring(virgulIndex);
                Output.WriteLine(string.Concat(justB64Code.Reverse().Skip(3).Reverse()));
                return FileSystem2.Base64ToImage(string.Concat(justB64Code.Reverse().Skip(3).Reverse()));
            }
        }

        private void ChangeThemeForMenuItems(ToolStripMenuItem item)
        {
            item.BackColor = Properties.Settings.Default.BackColor;
            item.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            item.DropDown.BackColor = Properties.Settings.Default.BackColor;
            item.DropDown.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            foreach (ToolStripMenuItem x in item.DropDownItems)
            {
                ChangeThemeForMenuDDItems(x.DropDown);
            }
        }

        private void ChangeThemeForMenuDDItems(ToolStripDropDown itemDD)
        {
            itemDD.BackColor = Properties.Settings.Default.BackColor;
            itemDD.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            foreach (ToolStripMenuItem x in itemDD.Items)
            {
                x.BackColor = Properties.Settings.Default.BackColor;
                x.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                ChangeThemeForMenuDDItems(x.DropDown);
            }
        }

        private void UpdateFavoriteColor()
        {
            foreach (ToolStripMenuItem y in mFavorites.Items)
            {
                ChangeThemeForMenuItems(y);
            }
        }
        private Color oldBackColor;
        private Color oldOverlayColor;
        private string oldStyle;

        private void ChangeTheme()
        {
            if (anaform() != null)
            {
                anaform().tabRenderer.ChangeColors(Properties.Settings.Default.BackColor, Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White, Properties.Settings.Default.OverlayColor);
                anaform().Update();
            }
            if (Properties.Settings.Default.OverlayColor != oldOverlayColor)
            {
                oldOverlayColor = Properties.Settings.Default.OverlayColor;
                pbProgress.BackColor = Properties.Settings.Default.OverlayColor;
                hsDownload.OverlayColor = Properties.Settings.Default.OverlayColor;
                hsDoNotTrack.OverlayColor = Properties.Settings.Default.OverlayColor;
                hsFav.OverlayColor = Properties.Settings.Default.OverlayColor;
                hsOpen.OverlayColor = Properties.Settings.Default.OverlayColor;
                hsUnknown.OverlayColor = Properties.Settings.Default.OverlayColor;
                hlvDownload.OverlayColor = Properties.Settings.Default.OverlayColor;
                hlvHistory.OverlayColor = Properties.Settings.Default.OverlayColor;
                Console.WriteLine(chromiumWebBrowser1.Address);
                if (chromiumWebBrowser1.Address.StartsWith("korot:")) { chromiumWebBrowser1.Reload(); }
            }
            if (Properties.Settings.Default.BackColor != oldBackColor)
            {
                UpdateFavoriteColor();
                updateFavoritesImages();
                if (chromiumWebBrowser1.Address.StartsWith("korot:")) { chromiumWebBrowser1.Reload(); }
                cmsFavorite.BackColor = Properties.Settings.Default.BackColor;
                cmsIncognito.BackColor = Properties.Settings.Default.BackColor;
                Console.WriteLine(chromiumWebBrowser1.Address);
                oldBackColor = Properties.Settings.Default.BackColor;
                cmsIncognito.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsFavorite.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                pbStore.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.store_w : Properties.Resources.store;
                tsWebStore.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.store_w : Properties.Resources.store;
                button13.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                tsThemes.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.theme_w : Properties.Resources.theme;
                button6.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button4.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button8.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button14.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button16.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                lbSettings.BackColor = Color.Transparent;
                lbSettings.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hlvDownload.BackColor = Properties.Settings.Default.BackColor;
                hlvDownload.HeaderBackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                hlvDownload.HeaderForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hlvHistory.BackColor = Properties.Settings.Default.BackColor;
                hlvHistory.HeaderBackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                hlvHistory.HeaderForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsDownload.BackColor = Properties.Settings.Default.BackColor;
                cmsHistory.BackColor = Properties.Settings.Default.BackColor;
                cmsSearchEngine.BackColor = Properties.Settings.Default.BackColor;
                profilenameToolStripMenuItem.DropDown.BackColor = Properties.Settings.Default.BackColor;
                profilenameToolStripMenuItem.DropDown.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsDownload.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                listBox2.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                comboBox1.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                tbHomepage.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                tbSearchEngine.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                button10.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hlvDownload.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cbLang.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hsDownload.BackColor = Properties.Settings.Default.BackColor;
                hsDownload.ButtonColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsDownload.BorderColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsDownload.ButtonHoverColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false), false);
                hsDownload.ButtonPressedColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 60, false), false);
                hsDoNotTrack.BackColor = Properties.Settings.Default.BackColor;
                hsDoNotTrack.ButtonColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsDoNotTrack.BorderColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsDoNotTrack.ButtonHoverColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false), false);
                hsDoNotTrack.ButtonPressedColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 60, false), false);
                hsProxy.BackColor = Properties.Settings.Default.BackColor;
                hsProxy.ButtonColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsProxy.BorderColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsProxy.ButtonHoverColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false), false);
                hsProxy.ButtonPressedColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 60, false), false);
                hsUnknown.BackColor = Properties.Settings.Default.BackColor;
                hsUnknown.ButtonColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsUnknown.BorderColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsUnknown.ButtonHoverColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false), false);
                hsUnknown.ButtonPressedColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 60, false), false);
                hsFav.BackColor = Properties.Settings.Default.BackColor;
                hsFav.ButtonColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsFav.BorderColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsFav.ButtonHoverColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false), false);
                hsFav.ButtonPressedColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 60, false), false);
                hsOpen.BackColor = Properties.Settings.Default.BackColor;
                hsOpen.ButtonColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsOpen.BorderColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false), false);
                hsOpen.ButtonHoverColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 40, false), false);
                hsOpen.ButtonPressedColor = Tools.TersRenk(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 60, false), false);
                hlvHistory.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cbLang.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsHistory.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsSearchEngine.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                listBox2.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                comboBox1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                lbCookie.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                btCookie.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                btInstall.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                btUpdater.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                tbHomepage.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                btCleanLog.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                tbFolder.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                tbStartup.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                cmsStartup.BackColor = Properties.Settings.Default.BackColor;
                cmsStartup.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                tbFolder.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                tbStartup.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                lbURinfo.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                button18.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                btDownloadFolder.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                button12.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                tbSearchEngine.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                button10.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                tbHomepage.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                tbSearchEngine.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                cbLang.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                cbLang.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                toolStripTextBox1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                comboBox3.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                comboBox3.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                dudClose.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                dudClose.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                dudNewTab.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
                dudNewTab.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsProfiles.BackColor = Properties.Settings.Default.BackColor;
                cmsHamburger.BackColor = Properties.Settings.Default.BackColor;
                cmsPrivacy.BackColor = Properties.Settings.Default.BackColor;
                label2.BackColor = Properties.Settings.Default.BackColor;
                BackColor = Properties.Settings.Default.BackColor;
                extensionToolStripMenuItem1.DropDown.BackColor = Properties.Settings.Default.BackColor;
                aboutToolStripMenuItem.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.about_w : Properties.Resources.about;
                downloadsToolStripMenuItem.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.download_w : Properties.Resources.download;
                historyToolStripMenuItem.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.history_w : Properties.Resources.history;
                pictureBox1.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.inctab_w : Properties.Resources.inctab;
                tbAddress.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsHamburger.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsProfiles.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                label2.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                toolStripTextBox1.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsPrivacy.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                extensionToolStripMenuItem1.DropDown.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                textBox4.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                if (isPageFavorited(chromiumWebBrowser1.Address)) { button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.star_on_w : Properties.Resources.star_on; } else { button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; }
                mFavorites.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                settingsToolStripMenuItem.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.Settings : Properties.Resources.Settings_w;
                newWindowToolStripMenuItem.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.newwindow : Properties.Resources.newwindow_w;
                newIncognitoWindowToolStripMenuItem.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.inctab : Properties.Resources.inctab_w;
                button9.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.profiles : Properties.Resources.profiles_w;
                button1.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                button2.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.refresh : Properties.Resources.refresh_w;
                button3.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w;
                //button4.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.go : Properties.Resources.go_w;
                button5.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.home : Properties.Resources.home_w;
                button11.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.hamburger : Properties.Resources.hamburger_w;
                tbAddress.BackColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(Tools.GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 20), Tools.GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 20), Tools.GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 20)) : Color.FromArgb(Tools.GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 20, 255), Tools.GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 20, 255), Tools.GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 20, 255));
                textBox4.BackColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(Tools.GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 20), Tools.GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 20), Tools.GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 20)) : Color.FromArgb(Tools.GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 20, 255), Tools.GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 20, 255), Tools.GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 20, 255));
                mFavorites.BackColor = Properties.Settings.Default.BackColor;
                extensionToolStripMenuItem1.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.ext : Properties.Resources.ext_w;
                cmsBStyle.BackColor = Properties.Settings.Default.BackColor;
                cmsBStyle.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsCookie.BackColor = Properties.Settings.Default.BackColor;
                cmsCookie.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                switchToToolStripMenuItem.DropDown.BackColor = Properties.Settings.Default.BackColor; switchToToolStripMenuItem.DropDown.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                foreach (ToolStripItem x in cmsProfiles.Items) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White; }
                foreach (ToolStripItem x in extensionToolStripMenuItem1.DropDownItems) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White; }
                foreach (TabPage x in tabControl1.TabPages) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black; }
            }
            if (Properties.Settings.Default.BackStyle != "BACKCOLOR")
            {
                if (Properties.Settings.Default.BackStyle != oldStyle)
                {
                    oldStyle = Properties.Settings.Default.BackStyle;
                    Image backStyle = GetImageFromURL(Properties.Settings.Default.BackStyle);
                    panel2.BackgroundImage = backStyle;
                    mFavorites.BackgroundImage = backStyle;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImage = backStyle; }
                    tpSettings.BackgroundImage = backStyle;
                }
            }
            else
            {
                if (Properties.Settings.Default.BackStyle != oldStyle)
                {
                    oldStyle = Properties.Settings.Default.BackStyle;
                    panel2.BackgroundImage = null;
                    mFavorites.BackgroundImage = null;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImage = null; }
                    tpSettings.BackgroundImage = null;
                }
            }
            if (Properties.Settings.Default.BStyleLayout == 0) //NONE
            {
                panel2.BackgroundImageLayout = ImageLayout.None;
                mFavorites.BackgroundImageLayout = ImageLayout.None;
                tpSettings.BackgroundImageLayout = ImageLayout.None;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.None; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 1) //TILE
            {
                panel2.BackgroundImageLayout = ImageLayout.Tile;
                mFavorites.BackgroundImageLayout = ImageLayout.Tile;
                tpSettings.BackgroundImageLayout = ImageLayout.Tile;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Tile; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 2) //CENTER
            {
                panel2.BackgroundImageLayout = ImageLayout.Center;
                mFavorites.BackgroundImageLayout = ImageLayout.Center;
                tpSettings.BackgroundImageLayout = ImageLayout.Center;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Center; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 3) //STRETCH
            {
                panel2.BackgroundImageLayout = ImageLayout.Stretch;
                mFavorites.BackgroundImageLayout = ImageLayout.Stretch;
                tpSettings.BackgroundImageLayout = ImageLayout.Stretch;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Stretch; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 4) //ZOOM
            {
                panel2.BackgroundImageLayout = ImageLayout.Zoom;
                mFavorites.BackgroundImageLayout = ImageLayout.Zoom;
                tpSettings.BackgroundImageLayout = ImageLayout.Zoom;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Zoom; }
            }
        }

        private double websiteprogress;
        public void ChangeProgress(double value)
        {
            if (value == 1) { websiteprogress = value; pbProgress.Width = Width; }
            else
            {
                websiteprogress = value;
                pbProgress.Width = Convert.ToInt32(Convert.ToDouble(Width / 100) * Convert.ToDouble(value * 100));
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (chromiumWebBrowser1.IsDisposed)
            {
                Close();
            }

            if (findLast)
            {
                tsSearchStatus.Text = findC + " " + findCurrent + " " + findL + " " + findT + " " + findTotal;
            }
            else if (findCurrent == 0 && findTotal == 0)
            {
                tsSearchStatus.Text = noSearch;
            }
            else
            {
                tsSearchStatus.Text = findC + " " + findCurrent + " " + findL + " " + findT + " " + findTotal;
            }

            onCEFTab = (tabControl1.SelectedTab == tpCef);
            ChangeTheme();
            if (Parent != null)
            {
                Parent.Text = Text;
            }
            RefreshTranslation();
            if (anaform() != null)
            {
                if (anaform().restoremedaddy == "") { spRestorer.Visible = false; restoreLastSessionToolStripMenuItem.Visible = false; } else { spRestorer.Visible = true; restoreLastSessionToolStripMenuItem.Visible = true; }
            }
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(((ToolStripMenuItem)sender).Tag.ToString());
        }
        private bool isPageFavorited(string url)
        {
            return Properties.Settings.Default.Favorites.Contains("Url=\"" + url + "\"");
        }
        public void removeFavorite(string url)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Properties.Settings.Default.Favorites.Replace("[", "<").Replace("]", ">"));
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            XmlNodeList nodes = document.SelectNodes("//element[@Url='" + url + "']");
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].ParentNode.RemoveChild(nodes[i]);
            }
            Properties.Settings.Default.Favorites = document.OuterXml.Replace("<", "[").Replace(">", "]");
            LoadDynamicMenu();
        }
        public void removeFavoriteWithName(string name)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Properties.Settings.Default.Favorites.Replace("[", "<").Replace("]", ">"));
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            XmlNodeList nodes = document.SelectNodes("//element[@Name='" + name + "']");
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes[i].ParentNode.RemoveChild(nodes[i]);
            }
            Properties.Settings.Default.Favorites = document.OuterXml.Replace("<", "[").Replace(">", "]");
            LoadDynamicMenu();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (isPageFavorited(chromiumWebBrowser1.Address))
            {
                removeFavorite(chromiumWebBrowser1.Address);
                button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
            }
            else
            {
                frmNewFav newFav = new frmNewFav(Text, chromiumWebBrowser1.Address, this);
                newFav.ShowDialog();
            }
            RefreshFavorites();
        }

        private void RefreshProfiles()
        {
            switchToToolStripMenuItem.DropDownItems.Clear();
            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"))
            {
                if (x != profilePath)
                {
                    DirectoryInfo info = new DirectoryInfo(x);
                    if (info.Name == userName) { }
                    else
                    {
                        ToolStripMenuItem profileItem = new ToolStripMenuItem
                        {
                            Text = info.Name
                        };
                        profileItem.Click += ProfilesToolStripMenuItem_Click;
                        switchToToolStripMenuItem.DropDownItems.Add(profileItem);
                    }
                }
            }
            if (switchToToolStripMenuItem.DropDownItems.Count == 0)
            {
                switchToToolStripMenuItem.DropDownItems.Add(tsEmptyProfile);
            }
        }
        public void RefreshSizes()
        {
            dudClose.Location = new Point(label32.Location.X + label32.Width, dudClose.Location.Y);
            dudClose.Width = tpTheme.Width - (label32.Width + label32.Location.X + 25);
            dudNewTab.Location = new Point(label31.Location.X + label31.Width, dudNewTab.Location.Y);
            dudNewTab.Width = tpTheme.Width - (label31.Width + label31.Location.X + 25);
            hsFav.Location = new Point(label33.Location.X + label33.Width + 5, hsFav.Location.Y);
            hsDoNotTrack.Location = new Point(lbDNT.Location.X + lbDNT.Width + 5, hsDoNotTrack.Location.Y);
            hsOpen.Location = new Point(lbOpen.Location.X + lbOpen.Width + 5, hsOpen.Location.Y);
            hsDownload.Location = new Point(lbAutoDownload.Location.X + lbAutoDownload.Width + 5, hsDownload.Location.Y);
            hsProxy.Location = new Point(lbLastProxy.Location.X + lbLastProxy.Width + 5, hsProxy.Location.Y);
            hsUnknown.Location = new Point(lbUResources.Location.X + lbUResources.Width + 5, hsUnknown.Location.Y);
            linkLabel1.LinkArea = new LinkArea(0, linkLabel1.Text.Length);
            linkLabel1.Location = new Point(label21.Location.X, label21.Location.Y + label21.Size.Height);
            textBox4.Location = new Point(label12.Location.X + label12.Width, textBox4.Location.Y);
            textBox4.Width = tpTheme.Width - (label12.Width + label12.Location.X + 25);
            tbStartup.Location = new Point(label28.Location.X + label28.Width, tbStartup.Location.Y);
            tbStartup.Width = tpSettings.Width - (label28.Width + label28.Location.X + 15);
            comboBox3.Location = new Point(label25.Location.X + label25.Width, comboBox3.Location.Y);
            comboBox3.Width = tpTheme.Width - (label25.Width + label25.Location.X + 25);
            pictureBox3.Location = new Point(label14.Location.X + label14.Width, pictureBox3.Location.Y);
            pictureBox4.Location = new Point(label16.Location.X + label16.Width, pictureBox4.Location.Y);
            tbFolder.Location = new Point(lbDownloadFolder.Location.X + lbDownloadFolder.Width, tbFolder.Location.Y);
            tbFolder.Width = tpDownload.Width - (lbDownloadFolder.Location.X + lbDownloadFolder.Width + btDownloadFolder.Width + 25);
            btDownloadFolder.Location = new Point(tbFolder.Location.X + tbFolder.Width, btDownloadFolder.Location.Y);
            comboBox1.Location = new Point(label13.Location.X + label13.Width, comboBox1.Location.Y);
            comboBox1.Width = tpTheme.Width - (label13.Location.X + label13.Width + button12.Width + 25);
            button12.Location = new Point(comboBox1.Location.X + comboBox1.Width, button12.Location.Y);
            tbHomepage.Location = new Point(lbHomepage.Location.X + lbHomepage.Width + 5, tbHomepage.Location.Y);
            tbHomepage.Width = tpSettings.Width - (lbHomepage.Location.X + lbHomepage.Width + 25);
            radioButton1.Location = new Point(tbHomepage.Location.X, tbHomepage.Location.Y + tbHomepage.Height + 5);
            tbSearchEngine.Location = new Point(lbSearchEngine.Location.X + lbSearchEngine.Width + 5, tbSearchEngine.Location.Y);
            tbSearchEngine.Width = tpSettings.Width - (lbSearchEngine.Location.X + lbSearchEngine.Width + 25);
        }
        public void RefreshTranslation()
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeAuthor) && string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeName))
            {
                label21.Text = aboutInfo.Replace("[NEWLINE]", Environment.NewLine);
            }
            else
            {
                label21.Text = aboutInfo.Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + themeInfo.Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeAuthor) ? anon : Properties.Settings.Default.ThemeAuthor).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeName) ? noname : Properties.Settings.Default.ThemeName);
            }

            hsOpen.Checked = Properties.Settings.Default.downloadOpen;
            hsUnknown.Checked = Properties.Settings.Default.allowUnknownResources;
            hsFav.Checked = Properties.Settings.Default.showFav;
            dudClose.SelectedIndex = Properties.Settings.Default.closeColor;
            dudNewTab.SelectedIndex = Properties.Settings.Default.newTabColor;
            comboBox3.SelectedIndex = Properties.Settings.Default.BStyleLayout;
            colorToolStripMenuItem.Checked = Properties.Settings.Default.BackStyle == "BACKCOLOR" ? true : false;
            switchToToolStripMenuItem.Text = switchTo;
            newProfileToolStripMenuItem.Text = newprofile;
            deleteThisProfileToolStripMenuItem.Text = deleteProfile;
            showCertificateErrorsToolStripMenuItem.Text = showCertError;
            textBox4.Text = Properties.Settings.Default.BackStyle == "BACKCOLOR" ? usingBC : Properties.Settings.Default.BackStyle;
            if (certError)
            {
                safeStatusToolStripMenuItem.Text = CertificateErrorTitle;
                ınfoToolStripMenuItem.Text = CertificateError;
            }
            else
            {
                safeStatusToolStripMenuItem.Text = CertificateOKTitle;
                ınfoToolStripMenuItem.Text = CertificateOK;
            }
            if (cookieUsage) { cookieInfoToolStripMenuItem.Text = usesCookies; } else { cookieInfoToolStripMenuItem.Text = notUsesCookies; }
            label7.Text = CertErrorPageTitle;
            label8.Text = CertErrorPageMessage;
            button10.Text = CertErrorPageButton;
            newWindowToolStripMenuItem.Text = newWindow;
            newIncognitoWindowToolStripMenuItem.Text = newincognitoWindow;
            settingsToolStripMenuItem.Text = settingstitle;
            restoreLastSessionToolStripMenuItem.Text = restoreOldSessions;
            if (Properties.Settings.Default.StartupURL.ToLower() == "korot://newtab")
            {
                tbStartup.Text = showNewTabPageToolStripMenuItem.Text;
            }
            else if (Properties.Settings.Default.StartupURL.ToLower() == "korot://homepage" || Properties.Settings.Default.StartupURL.ToLower() == Properties.Settings.Default.Homepage.ToLower())
            {
                tbStartup.Text = showHomepageToolStripMenuItem.Text;
            }
            else
            {
                tbStartup.Text = Properties.Settings.Default.StartupURL;
            }
            hsDownload.Checked = Properties.Settings.Default.useDownloadFolder;
            lbDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Enabled = hsDownload.Checked;
            btDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Text = Properties.Settings.Default.DownloadFolder;
        }
        private void ProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileManagement.SwitchProfile(((ToolStripMenuItem)sender).Text, this);
        }

        private void NewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileManagement.NewProfile(this);
        }

        private void DeleteThisProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileManagement.DeleteProfile(userName, this);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            cmsProfiles.Show(MousePosition);
        }
        private void DefaultProxyts_Click(object sender, EventArgs e)
        {
            SetProxy(chromiumWebBrowser1, defaultProxy);
            DefaultProxyts.Enabled = false;
        }
        public void applyExtension(string fileLocation)
        {
            string Playlist = FileSystem2.ReadFile(fileLocation, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase.Length >= 11)
            {
                if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "1" && (new FileInfo(fileLocation).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\")).Length < 5242880))
                {
                    chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[8].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\"), Encoding.UTF8));
                }
                if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "1" && !string.IsNullOrWhiteSpace(SplittedFase[7].Substring(1).Replace(Environment.NewLine, "")) && defaultProxy != null)
                {
                    SetProxy(chromiumWebBrowser1, SplittedFase[7].Substring(1).Replace(Environment.NewLine, ""));
                    DefaultProxyts.Enabled = true;
                    if (Properties.Settings.Default.rememberLastProxy) { Properties.Settings.Default.LastProxy = SplittedFase[7].Substring(1).Replace(Environment.NewLine, ""); }
                }
                bool allowWebContent = false;
                if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "1") { allowWebContent = true; }
                if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "1")
                {
                    frmExt formext = new frmExt(this, userName, fileLocation, SplittedFase[6].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\"), allowWebContent)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        StartPosition = FormStartPosition.Manual,
                        Size = new Size(Convert.ToInt32(SplittedFase[10].Substring(1).Replace(Environment.NewLine, "")), Convert.ToInt32(SplittedFase[9].Substring(1).Replace(Environment.NewLine, "")))
                    };
                    formext.Location = new Point(anaform().Left + (anaform().Width - (formext.Width + 200)), anaform().Top);
                    Controls.Add(formext);
                    formext.Visible = true;
                    formext.BringToFront();
                    formext.Show();
                }
            }
        }
        private void ExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applyExtension(((ToolStripMenuItem)sender).Tag.ToString());

        }
        public void showDevTools()
        {
            if (chromiumWebBrowser1.IsHandleCreated)
            {
                chromiumWebBrowser1.GetBrowser().ShowDevTools();
            }
        }
        public void LoadExt()
        {
            extensionToolStripMenuItem1.DropDownItems.Clear();
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }
            if (Properties.Settings.Default.allowUnknownResources)
            {
                foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"))
                {
                    if (File.Exists(x + "\\ext.kem") && new FileInfo(x + "\\ext.kem").Length < 1048576)
                    {
                        string Playlist = FileSystem2.ReadFile(x + "\\ext.kem", Encoding.UTF8);
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        ToolStripMenuItem extItem = new ToolStripMenuItem
                        {
                            Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "")
                        };
                        if (!File.Exists(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\")))
                        {
                            if (Tools.Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                            else { extItem.Image = Properties.Resources.ext_w; }
                        }
                        else
                        {
                            if (new FileInfo(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\")).Length < 5242880)
                            {
                                extItem.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\"));
                            }
                            else
                            {
                                if (Tools.Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                                else { extItem.Image = Properties.Resources.ext_w; }
                            }
                        }
                        ToolStripMenuItem extRunItem = new ToolStripMenuItem();
                        extItem.Click += ExtensionToolStripMenuItem_Click;
                        extItem.Tag = x + "\\ext.kem";
                        extensionToolStripMenuItem1.DropDown.Items.Add(extItem);
                    }
                }
            }
            else
            {
                foreach (string y in Properties.Settings.Default.registeredExtensions)
                {
                    string x = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\" + y;
                    if (File.Exists(x + "\\ext.kem") && new FileInfo(x + "\\ext.kem").Length < 1048576)
                    {
                        string Playlist = FileSystem2.ReadFile(x + "\\ext.kem", Encoding.UTF8);
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        ToolStripMenuItem extItem = new ToolStripMenuItem
                        {
                            Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "")
                        };
                        if (!File.Exists(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\")))
                        {
                            if (Tools.Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                            else { extItem.Image = Properties.Resources.ext_w; }
                        }
                        else
                        {
                            if (new FileInfo(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\")).Length < 5242880)
                            {
                                extItem.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\"));
                            }
                            else
                            {
                                if (Tools.Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                                else { extItem.Image = Properties.Resources.ext_w; }
                            }
                        }
                        ToolStripMenuItem extRunItem = new ToolStripMenuItem();
                        extItem.Click += ExtensionToolStripMenuItem_Click;
                        extItem.Tag = x + "\\ext.kem";
                        extensionToolStripMenuItem1.DropDown.Items.Add(extItem);
                    }
                }
            }
            if (extensionToolStripMenuItem1.DropDownItems.Count == 0)
            {
                extensionToolStripMenuItem1.DropDown.Items.Add(tsEmptyExt);
            }
            extensionToolStripMenuItem1.DropDown.Items.Add(tsExt);
            extensionToolStripMenuItem1.DropDown.Items.Add(tsWebStore);
        }

        private void TmrSlower_Tick(object sender, EventArgs e)
        {
            RefreshFavorites();
        }

        public void FrmCEF_SizeChanged(object sender, EventArgs e)
        {
            ChangeProgress(websiteprogress);
        }


        private void Panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            tabform_KeyDown(pCEF, new KeyEventArgs(e.KeyData));
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cmsPrivacy.Show(pictureBox2, 0, pictureBox2.Size.Height);
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmsPrivacy.Close();
        }

        private void showCertificateErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showCertificateErrorsToolStripMenuItem.Tag != null)
            {
                TextBox txtCertificate = new TextBox() { ScrollBars = ScrollBars.Both, Multiline = true, Dock = DockStyle.Fill, Text = showCertificateErrorsToolStripMenuItem.Tag.ToString() };
                Form frmCertificate = new Form() { Icon = Icon, Text = CertificateErrorMenuTitle, FormBorderStyle = FormBorderStyle.SizableToolWindow };
                frmCertificate.Controls.Add(txtCertificate);
                frmCertificate.ShowDialog();
            }
        }
        public List<string> CertAllowedUrls = new List<string>();
        private void button10_Click(object sender, EventArgs e)
        {
            CertAllowedUrls.Add(button10.Tag.ToString());
            chromiumWebBrowser1.Refresh();
            pnlCert.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpSettings;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (cmsHamburger.Visible) { cmsHamburger.Close(); } else { cmsHamburger.Show(button11, 0, 0); }
            button11.FlatAppearance.BorderSize = 0;
        }

        private void restoreLastSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anaform().Invoke(new Action(() => anaform().ReadSession(anaform().restoremedaddy)));
            restoreLastSessionToolStripMenuItem.Visible = false;
        }

        private bool allowSwitching = false;
        private bool onCEFTab = true;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitching == false) { e.Cancel = true; } else { toolStripTextBox1.Text = SearchOnPage; chromiumWebBrowser1.StopFinding(true); e.Cancel = false; allowSwitching = false; }
            onCEFTab = (tabControl1.SelectedTab == tpCef);
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            cmsBStyle.Show(textBox4, 0, 0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewTab("korot://licenses");
        }
        private void contextMenuStrip4_Opening(object sender, CancelEventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\\"");
            e.Cancel = true;
        }

        private void hsDoNotTrack_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DoNotTrack = hsDoNotTrack.Checked;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BStyleLayout = comboBox3.SelectedIndex;
            checkIfDefaultTheme();
        }

        private void Button10_Click(object sender, EventArgs e)
        {

        }

        private void disallowThisPageForCookieAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser1.Address))
            {
                Properties.Settings.Default.CookieDisallowList.Remove(chromiumWebBrowser1.Address);
                chromiumWebBrowser1.Reload();
            }
            else
            {
                Properties.Settings.Default.CookieDisallowList.Add(chromiumWebBrowser1.Address);
                chromiumWebBrowser1.Reload();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            cmsIncognito.Show(pictureBox1, 0, 0);
        }

        private void openInNewTab_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag != null)
                {
                    if (selectedFavorite.Tag.ToString() != "korot://folder")
                    {
                        NewTab(selectedFavorite.Tag.ToString());
                    }
                    else
                    {
                        foreach (ToolStripItem item in selectedFavorite.DropDown.Items)
                        {
                            if (item.Tag.ToString() != "korot://folder") { NewTab(item.Tag.ToString()); }
                        }
                    }
                }
            }
        }
        private void removeSelectedTSMI_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag.ToString() == chromiumWebBrowser1.Address)
                {
                    button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
                }
                if (selectedFavorite.Tag.ToString() != "korot://folder")
                {
                    removeFavorite(selectedFavorite.Tag.ToString());
                }
                else
                {
                    removeFavoriteWithName(selectedFavorite.Name);
                }
                RefreshFavorites();
            }
        }

        private void clearTSMI_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Favorites = "[root][/root]";
            button7.Image = Tools.Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
            RefreshFavorites();
        }

        private void cmsFavorite_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (e.CloseReason != ToolStripDropDownCloseReason.ItemClicked)
            {
                itemClicked = false;
                selectedFavorite = null;
            }
        }

        private void cmsFavorite_Opening(object sender, CancelEventArgs e)
        {
            if (selectedFavorite == null)
            {
                removeSelectedTSMI.Enabled = false;
                tsopenInNewTab.Enabled = false;
                openİnNewWindowToolStripMenuItem.Enabled = false;
                openİnNewIncognitoWindowToolStripMenuItem.Enabled = false;
                tsSepFav.Visible = false;
                removeSelectedTSMI.Visible = false;
                tsopenInNewTab.Visible = false;
                openİnNewWindowToolStripMenuItem.Visible = false;
                openİnNewIncognitoWindowToolStripMenuItem.Visible = false;
            }
            else
            {
                if (selectedFavorite.Tag != null)
                {
                    if (selectedFavorite.Tag.ToString() == "korot://folder")
                    {
                        tsopenInNewTab.Text = openAllInNewTab;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = openAllInNewIncWindow;
                        openİnNewWindowToolStripMenuItem.Text = openAllInNewWindow;
                    }
                    else
                    {
                        tsopenInNewTab.Text = openInNewTab;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = openInNewIncWindow;
                        openİnNewWindowToolStripMenuItem.Text = openInNewWindow;
                    }
                }
                removeSelectedTSMI.Enabled = !_Incognito;
                tsopenInNewTab.Enabled = true;
                openİnNewWindowToolStripMenuItem.Enabled = true;
                openİnNewIncognitoWindowToolStripMenuItem.Enabled = true;
                tsSepFav.Visible = true;
                removeSelectedTSMI.Visible = !_Incognito;
                tsopenInNewTab.Visible = true;
                openİnNewWindowToolStripMenuItem.Visible = true;
                openİnNewIncognitoWindowToolStripMenuItem.Visible = true;
            }
        }
        public void takeScreenShot()
        {
            takeAScreenshotToolStripMenuItem_Click(null, null);
        }

        public void savePage()
        {
            saveThisPageToolStripMenuItem_Click(null, null);
        }

        private void takeAScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                FileName = "Korot Screenshot.png",
                Filter = imageFiles + "|*.png|" + allFiles + "|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileSystem2.WriteFile(save.FileName, TakeScrenshot.ImageToByte2(TakeScrenshot.Snapshot(chromiumWebBrowser1)));
            }
        }

        private void saveThisPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName = Text + ".html",
                Filter = htmlFiles + "|*.html;*.htm|" + allFiles + "|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                Task<string> htmlText = chromiumWebBrowser1.GetSourceAsync();
                FileSystem2.WriteFile(save.FileName, htmlText.Result, Encoding.UTF8);
            }
        }

        private void frmCEF_KeyUp(object sender, KeyEventArgs e)
        {
            isControlKeyPressed = !e.Control;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {

            if (isControlKeyPressed)
            {
                allowSwitching = true;
                tabControl1.SelectedTab = tpCef;
                if (e.Delta > 0)
                {
                    zoomInToolStripMenuItem_Click(sender, null);
                }
                else if (e.Delta < 0)
                {
                    zoomOutToolStripMenuItem_Click(sender, null);
                }
            }
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            chromiumWebBrowser1.SetZoomLevel(0.0);
            cmsHamburger.Show(button11, 0, 0);
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        public void zoomIn()
        {
            Task<double> zoomLevel = chromiumWebBrowser1.GetZoomLevelAsync();
            if (zoomLevel.Result <= 8)
            {
                chromiumWebBrowser1.SetZoomLevel(zoomLevel.Result + 0.25);
            }
        }
        public void zoomOut()
        {
            Task<double> zoomLevel = chromiumWebBrowser1.GetZoomLevelAsync();
            if (zoomLevel.Result >= -0.75)
            {
                chromiumWebBrowser1.SetZoomLevel(zoomLevel.Result - 0.25);
            }
        }
        public void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            zoomIn();
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);

        }

        public void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            zoomOut();
            cmsHamburger.Show(button11, 0, 0);
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }

        private bool doNotDestroyFind = false;
        private void cmsHamburger_Opening(object sender, CancelEventArgs e)
        {
            if (!doNotDestroyFind)
            {
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
                doNotDestroyFind = false;
            }
            LoadExt();
            Task.Run(new Action(() => getZoomLevel()));
        }

        private async void getZoomLevel()
        {
            await Task.Run(() =>
            {
                Task<double> zoomLevel = chromiumWebBrowser1.GetZoomLevelAsync();
                zOOMLEVELToolStripMenuItem.Text = ((zoomLevel.Result * 100) + 100) + "%";
            });

        }

        private string searchPrev;
        private void toolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(toolStripTextBox1.Text)) & toolStripTextBox1.Text != SearchOnPage)
            {
                searchPrev = toolStripTextBox1.Text;
                chromiumWebBrowser1.Find(0, toolStripTextBox1.Text, true, caseSensitiveToolStripMenuItem.Checked, false);
            }
            else
            {
                doNotDestroyFind = false;
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
            }
        }

        private void cmsHamburger_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (!doNotDestroyFind)
            {
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
                doNotDestroyFind = false;
            }
        }

        private void caseSensitiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            if (!toolStripTextBox1.Selected)
            {
                toolStripTextBox1.SelectAll();
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpHistory;
        }

        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpDownload;
        }
        private void tsSearchNext_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Find(0, toolStripTextBox1.Text, true, caseSensitiveToolStripMenuItem.Checked, true);
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpAbout;
        }

        private void hsProxy_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.rememberLastProxy = hsProxy.Checked;
        }

        private void tsThemes_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpTheme;
        }

        private void clickHereToLearnMoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab("korot://incognito");
            anaform().Invoke(new Action(() => anaform().SelectedTabIndex = anaform().Tabs.Count - 1));
        }

        private void ıncognitoModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmsIncognito.Close();
        }

        private bool itemClicked = false;
        private void mFavorites_MouseClick(object sender, MouseEventArgs e)
        {
            if (!itemClicked) { if (e.Button == MouseButtons.Right) { selectedFavorite = null; cmsFavorite.Show(MousePosition); } }
        }

        private void cmsFavorite_Opened(object sender, EventArgs e)
        {
            cmsFavorite_Opening(null, null);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpCookie;
        }

        private void RefreshCookies()
        {
            int selectedValue = lbCookie.SelectedIndex;
            int scroll = lbCookie.TopIndex;
            lbCookie.Items.Clear();
            foreach (string x in Properties.Settings.Default.CookieDisallowList)
            {
                lbCookie.Items.Add(x);
            }
            if (selectedValue < lbCookie.Items.Count - 1)
            {
                lbCookie.SelectedIndex = selectedValue;
                lbCookie.TopIndex = scroll;
            }
        }
        private void clearToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.CookieDisallowList.Clear();
            RefreshCookies();
        }

        private void allowSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbCookie.SelectedItem != null)
            {
                Properties.Settings.Default.CookieDisallowList.Remove(lbCookie.SelectedItem.ToString());
            }

            RefreshCookies();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tpSettings;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = selectAFolder };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = dialog.SelectedPath;
                Properties.Settings.Default.DownloadFolder = tbFolder.Text;
            }
        }

        private void tbFolder_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DownloadFolder = tbFolder.Text;
        }

        private void hsDownload_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.useDownloadFolder = hsDownload.Checked;
            lbDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Enabled = hsDownload.Checked;
            btDownloadFolder.Enabled = hsDownload.Checked;
        }

        private void showNewTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbStartup.Text = showNewTabPageToolStripMenuItem.Text;
            Properties.Settings.Default.StartupURL = "korot://newtab";
        }

        private void showHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbStartup.Text = showHomepageToolStripMenuItem.Text;
            Properties.Settings.Default.StartupURL = "korot://homepage";
        }

        private void showAWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputb = new HaltroyFramework.HaltroyInputBox("Korot", enterAValidUrl, Icon, Properties.Settings.Default.SearchURL, Properties.Settings.Default.BackColor, OK, Cancel, 400, 150);
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(inputb.TextValue()) || (inputb.TextValue().ToLower() == "korot://newtab") || inputb.TextValue().ToLower() == Properties.Settings.Default.Homepage.ToLower() || inputb.TextValue().ToLower() == "korot://homepage")
                {
                    showAWebsiteToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    Properties.Settings.Default.StartupURL = inputb.TextValue();
                    tbStartup.Text = inputb.TextValue();
                }
            }
        }

        private void tbStartup_Click(object sender, EventArgs e)
        {
            cmsStartup.Show(MousePosition);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot", resetConfirm,
                                                                                      anaform().Icon,
                                                                                      MessageBoxButtons.YesNoCancel,
                                                                                      Properties.Settings.Default.BackColor,
                                                                                      Yes, No,
                                                                                      OK, Cancel, 390,
                                                                                      140);
            if (mesaj.ShowDialog() == DialogResult.Yes)
            {
                Process.Start(Application.ExecutablePath, "-oobe");
                Application.Exit();

            }
        }
        private void dudNewTab_SelectedItemChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.newTabColor = dudNewTab.SelectedIndex;
            ChangeTheme();
            checkIfDefaultTheme();
        }

        private void dudClose_SelectedItemChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.closeColor = dudClose.SelectedIndex;
            ChangeTheme();
            checkIfDefaultTheme();
        }

        private void hsFav_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.showFav = hsFav.Checked;
            RefreshFavorites();
        }

        private void hsUnknown_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.allowUnknownResources = hsUnknown.Checked;
            LoadExt();
            refreshThemeList();
        }

        private void btCleanLog_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\"))
            {
                File.Delete(x);
                count += 1;
            }
            Output.WriteLine(" [Korot.CleanLogs] " + count + " files deleted.");
        }

        private void hsOpen_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.downloadOpen = hsOpen.Checked;
        }

        private void tsWebStore_Click(object sender, EventArgs e)
        {
            NewTab("https://haltroy.com/store/Korot/Extensions/index.html");
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            NewTab("https://haltroy.com/store/Korot/Themes/index.html");
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewFav newFav = new frmNewFav(defaultFolderName, "korot://folder", this);
            newFav.ShowDialog();
        }

        private void newFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewFav newFav = new frmNewFav("", "", this);
            newFav.ShowDialog();
        }

        private void openİnNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag != null)
                {
                    if (selectedFavorite.Tag.ToString() != "korot://folder")
                    {
                        Process.Start(Application.ExecutablePath, selectedFavorite.Tag.ToString());
                    }
                    else
                    {
                        foreach (ToolStripItem item in selectedFavorite.DropDown.Items)
                        {
                            if (item.Tag.ToString() != "korot://folder") { Process.Start(Application.ExecutablePath, item.Tag.ToString()); }
                        }
                    }
                }
            }
        }

        private void openİnNewIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag != null)
                {
                    if (selectedFavorite.Tag.ToString() != "korot://folder")
                    {
                        Process.Start(Application.ExecutablePath, "-incognito" + selectedFavorite.Tag.ToString());
                    }
                    else
                    {
                        foreach (ToolStripItem item in selectedFavorite.DropDown.Items)
                        {
                            if (item.Tag.ToString() != "korot://folder") { Process.Start(Application.ExecutablePath, "-incognito" + item.Tag.ToString()); }
                        }
                    }
                }
            }
        }

        private bool isLeftPressed, isRightPressed = false;

        private void cbLang_TextUpdate(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\Lang\\" + cbLang.Text + ".lang"))
            {
                LoadLangFromFile(Application.StartupPath + "\\Lang\\" + cbLang.Text + ".lang");
            }
        }

        private void frmCEF_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }

        private void label20_MouseClick(object sender, MouseEventArgs e)
        {
            isLeftPressed = e.Button == MouseButtons.Left ? true : isLeftPressed;
            isRightPressed = e.Button == MouseButtons.Right ? true : isRightPressed;
            if (isLeftPressed && isRightPressed)
            {
                NewTab("https://www.youtube.com/watch?v=KMU0tzLwhbE");
                isLeftPressed = false;
                isRightPressed = false;
            }
        }
    }
}
