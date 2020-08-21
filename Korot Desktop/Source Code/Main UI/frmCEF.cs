//MIT License
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
using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
        public frmBlock blockmenu;
        public frmHamburger hammenu;
        public frmProfile profmenu;
        public frmIncognito incognitomenu;
        public frmPrivacy privmenu;
        public string DateFormat = "dd/MM/yy HH:mm:ss";
        public frmSites siteman;
        public frmDownload dowman;
        public frmHistory hisman;
        public Settings Settings;
        private frmCollection ColMan;
        public bool closing;
        public ContextMenuStrip cmsCEF = null;
        private int updateProgress = 0;
        private bool isLoading = false;
        private readonly string loaduri = null;
        public bool _Incognito = false;
        public string userName;
        public string profilePath;
        private readonly string userCache;

        public string defaultProxy = null;
        public ChromiumWebBrowser chromiumWebBrowser1;
        private readonly List<ToolStripMenuItem> favoritesFolders = new List<ToolStripMenuItem>();
        private readonly List<ToolStripMenuItem> favoritesNoIcon = new List<ToolStripMenuItem>();
        public bool NotificationListenerMode = false;
        public frmMain anaform => ((frmMain)ParentTabs);
        public bool noProfilePic = true;
        public Image profilePic;
        public void RefreshProfilePic()
        {
            if (!File.Exists(profilePath + "img.png")) { noProfilePic = true; }
            else
            {
                noProfilePic = false;
                profilePic = Image.FromStream(HTAlt.Tools.ReadFile(profilePath + "img.png"));
            }
        }
        public frmCEF(Settings settings, bool isIncognito = false, string loadurl = "korot://newtab", string profileName = "user0", bool notifListenMode = false)
        {
            Settings = settings;
            NotificationListenerMode = notifListenMode;
            loaduri = loadurl;
            _Incognito = isIncognito;
            userName = profileName;
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + profileName + "\\";
            userCache = profilePath + "cache\\";
            InitializeComponent();
            LoadMenus();
            InitializeChromium();
            RefreshProfilePic();
            foreach (Control x in Controls)
            {
                try { x.KeyDown += tabform_KeyDown; x.MouseWheel += MouseScroll; x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
            Updater();
        }
        public void LoadMenus()
        {
            hammenu = new frmHamburger(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom),
                Dock = DockStyle.Right,
                Visible = true,
                Height = Height,
            };
            profmenu = new frmProfile(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right),
                Visible = true
            };
            incognitomenu = new frmIncognito(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Anchor = (AnchorStyles.Top | AnchorStyles.Right),
                Visible = true
            };
            privmenu = new frmPrivacy(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Visible = true
            };
            blockmenu = new frmBlock(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                Visible = true
            };
            siteman = new frmSites(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                Visible = true
            };
            hisman = new frmHistory(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                Visible = true
            };
            dowman = new frmDownload(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                Visible = true
            };
            Controls.Add(hammenu);
            Controls.Add(profmenu);
            Controls.Add(incognitomenu);
            Controls.Add(privmenu);
            pBlock.Controls.Add(blockmenu);
            pDowMan.Controls.Add(dowman);
            pHisMan.Controls.Add(hisman);
            pSite.Controls.Add(siteman);
        }
        public void LoadDynamicMenu()
        {
            mFavorites.Items.Clear();
            favoritesNoIcon.Clear();
            favoritesFolders.Clear();
            foreach (Folder folder in Settings.Favorites.Favorites)
            {
                if (folder is Favorite)
                {
                    Favorite favorite = folder as Favorite;
                    ToolStripMenuItem menuItem = new ToolStripMenuItem
                    {
                        Name = favorite.Name,
                        Text = favorite.Text
                    };
                    menuItem.DropDown.RenderMode = ToolStripRenderMode.Professional;
                    menuItem.Tag = favorite;
                    if (!string.IsNullOrWhiteSpace(favorite.IconPath))
                    {
                        if (File.Exists(favorite.IconPath.Replace("{ICONSTORAGE}", iconStorage)))
                        {
                            Stream fs = HTAlt.Tools.ReadFile(favorite.IconPath.Replace("{ICONSTORAGE}", iconStorage));
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
                else
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem
                    {
                        Name = folder.Name,
                        Text = folder.Text,
                        Tag = folder
                    };
                    menuItem.MouseUp += menuStrip1_MouseUp;
                    menuItem.DropDown.RenderMode = ToolStripRenderMode.Professional;
                    favoritesFolders.Add(menuItem);

                    mFavorites.Items.Add(menuItem);
                    GenerateMenusFromXML(folder, (ToolStripMenuItem)mFavorites.Items[mFavorites.Items.Count - 1]);

                }
            }
            UpdateFavoriteColor();
            updateFavoritesImages();
        }
        private void GenerateMenusFromXML(Folder folder, ToolStripMenuItem menuItem)
        {
            ToolStripMenuItem item = null;

            foreach (Folder subfolder in folder.Favorites)
            {
                if (subfolder is Favorite)
                {
                    item = new ToolStripMenuItem
                    {
                        Name = subfolder.Name,
                        Text = subfolder.Text,
                        Tag = subfolder
                    };
                    item.DropDown.RenderMode = ToolStripRenderMode.Professional;
                    if (!string.IsNullOrWhiteSpace((subfolder as Favorite).IconPath))
                    {
                        if (File.Exists((subfolder as Favorite).IconPath.Replace("{ICONSTORAGE}", iconStorage)))
                        {
                            Stream fs = HTAlt.Tools.ReadFile((subfolder as Favorite).IconPath.Replace("{ICONSTORAGE}", iconStorage));
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
                else if (subfolder is Folder)
                {
                    item = new ToolStripMenuItem
                    {
                        Name = subfolder.Name,
                        Text = subfolder.Text,
                        Tag = subfolder
                    };
                    item.DropDown.RenderMode = ToolStripRenderMode.Professional;
                    item.MouseUp += menuStrip1_MouseUp;
                    favoritesFolders.Add(item);
                    menuItem.DropDownItems.Add(item);
                    GenerateMenusFromXML(subfolder, (ToolStripMenuItem)menuItem.DropDownItems[menuItem.DropDownItems.Count - 1]);

                }
            }
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings
            {
                UserAgent = "Mozilla/5.0 ( Windows "
                + Program.getOSInfo()
                + "; "
                + (Environment.Is64BitProcess ? "Win64" : "Win32NT")
                + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/"
                + Cef.ChromiumVersion
                + " Safari/537.36 Korot/"
                + Application.ProductVersion.ToString()
                + (VersionInfo.IsPreRelease ? ("-pre" + VersionInfo.PreReleaseNumber) : "")
                + "(" + VersionInfo.CodeName + ")"
            };
            if (_Incognito) { settings.CachePath = null; settings.PersistSessionCookies = false; settings.RootCachePath = null; }
            else { settings.CachePath = userCache; settings.RootCachePath = userCache; }
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "korot",
                SchemeHandlerFactory = new SchemeHandlerFactory(this)
                {
                    ext = null,
                    isExt = false,
                    extForm = null
                }
            });
            // Initialize cef with the provided settings
            settings.DisableGpuAcceleration();
            if (Settings.Flash) { settings.CefCommandLineArgs.Add("enable-system-flash"); }
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            chromiumWebBrowser1 = new ChromiumWebBrowser(string.IsNullOrWhiteSpace(loaduri) ? "korot://newtab" : loaduri);
            pCEF.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            chromiumWebBrowser1.ConsoleMessage += cef_consoleMessage;
            chromiumWebBrowser1.FindHandler = new FindHandler(hammenu);
            chromiumWebBrowser1.KeyboardHandler = new KeyboardHandler(this);
            chromiumWebBrowser1.RequestHandler = new RequestHandlerKorot(this);
            chromiumWebBrowser1.DisplayHandler = new DisplayHandler(this);
            chromiumWebBrowser1.LoadingStateChanged += loadingstatechanged;
            chromiumWebBrowser1.TitleChanged += cef_TitleChanged;
            chromiumWebBrowser1.AddressChanged += cef_AddressChanged;
            chromiumWebBrowser1.LoadError += cef_onLoadError;
            chromiumWebBrowser1.KeyDown += tabform_KeyDown;
            chromiumWebBrowser1.LostFocus += cef_LostFocus;
            chromiumWebBrowser1.Enter += cef_GotFocus;
            chromiumWebBrowser1.GotFocus += cef_GotFocus;
            chromiumWebBrowser1.MenuHandler = new ContextMenuHandler(this);
            chromiumWebBrowser1.LifeSpanHandler = new BrowserLifeSpanHandler(this);
            chromiumWebBrowser1.DownloadHandler = new DownloadHandler(this);
            chromiumWebBrowser1.JsDialogHandler = new JsHandler(this);
            chromiumWebBrowser1.DialogHandler = new MyDialogHandler();
            chromiumWebBrowser1.JavascriptMessageReceived += OnBrowserJavascriptMessageReceived;
            chromiumWebBrowser1.FrameLoadEnd += OnFrameLoadEnd;
            chromiumWebBrowser1.MouseWheel += MouseScroll;
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
            if (defaultProxy != null && Settings.RememberLastProxy && !string.IsNullOrWhiteSpace(Settings.LastProxy))
            {
                SetProxyAddress(Settings.LastProxy);
            }
        }
        private bool startupScriptsExecuted = false;
        public void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain && chromiumWebBrowser1.CanExecuteJavascriptInMainFrame)
            {
                if (!startupScriptsExecuted)
                {
                    foreach (Extension y in Settings.Extensions.ExtensionList)
                    {
                        if (y.Settings.autoLoad)
                        {
                            chromiumWebBrowser1.ExecuteScriptAsync(@"  " + HTAlt.Tools.ReadFile(y.Startup.Replace("[EXTFOLDER]", new FileInfo(y.ManifestFile).Directory + "\\"), Encoding.UTF8));
                        }
                    }
                    startupScriptsExecuted = true;
                }
                chromiumWebBrowser1.ExecuteScriptAsync(@"  " + Properties.Resources.notificationDefault.Replace("[$]", getNotificationPermission(e.Frame.Url)));
                foreach (Extension y in Settings.Extensions.ExtensionList)
                {
                    if (y.Settings.autoLoad)
                    {
                        chromiumWebBrowser1.ExecuteScriptAsync(@"  " + HTAlt.Tools.ReadFile(y.Background.Replace("[EXTFOLDER]", new FileInfo(y.ManifestFile).Directory + "\\"), Encoding.UTF8));
                    }
                }
            }
        }
        public string getNotificationPermission(string url)
        {
            string x = HTAlt.Tools.GetBaseURL(url);
            Site site = Settings.GetSiteFromUrl(x);
            if (site == null)
            {
                return "denied";
            }
            else
            {
                if (Settings.GetSiteFromUrl(x).AllowNotifications)
                {
                    return "granted";
                }
                return "denied";
            }
        }
        public void PushNewNotification(Notification notification)
        {
            frmNotification notifyForm = new frmNotification(this, notification);
            anaform.notifications.Add(notifyForm);
            notifyForm.Show();
        }
        public frmNotification getNotificationByID(string ID)
        {
            List<frmNotification> foundList = new List<frmNotification>();
            foreach (frmNotification x in anaform.notifications)
            {
                if (x.notification.id == ID)
                {
                    foundList.Add(x);
                }
            }
            if (foundList.Count > 0) { return foundList[0]; } else { return null; }
        }
        public void requestNotificationPermission(string url)
        {
            Invoke(new Action(() =>
            {
                string baseUrl = HTAlt.Tools.GetBaseURL(url);
                if (anaform.notificationAsked.Contains(baseUrl)) { return; }
                else
                {
                    frmNotificationPermission newPerm = new frmNotificationPermission(this, baseUrl)
                    {
                        Location = new Point(pbPrivacy.Location.X, pNavigate.Height),
                        TopLevel = false,
                        Visible = true
                    };
                    Controls.Add(newPerm);
                    newPerm.Show();
                    newPerm.Focus();
                    newPerm.BringToFront();
                }
            }));
        }
        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            //Get the underlying browser host wrapper
            IBrowserHost browserHost = chromiumWebBrowser1.GetBrowser().GetHost();
            IRequestContext requestContext = browserHost.RequestContext;
            // Browser must be initialized before getting/setting preferences
            if (!Settings.DoNotTrack) { return; }
            bool success = requestContext.SetPreference("enable_do_not_track", true, out string errorMessage);
            if (!success)
            {
                Output.WriteLine("Unable to set preference enable_do_not_track [errorMessage: " + errorMessage + "]");
            }
        }

        private void EditNewTabItem()
        {
            if (anaform.newtabeditTab != null)
            {
                anaform.SelectedTab = anaform.newtabeditTab;
            }
            else
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        resetPage(true);
                        anaform.newtabeditTab = ParentTab;
                        btNext.Enabled = true;
                        allowSwitching = true;
                        tabControl1.SelectedTab = tpNewTab;
                    }));
                }
                else
                {
                    resetPage(true);
                    anaform.newtabeditTab = ParentTab;
                    btNext.Enabled = true;
                    allowSwitching = true;
                    tabControl1.SelectedTab = tpNewTab;
                }
            }
        }

        public void refreshPage()
        {
            chromiumWebBrowser1.Reload();
        }

        private void OnBrowserJavascriptMessageReceived(object sender, JavascriptMessageReceivedEventArgs e)
        {
            string message = (string)e.Message;
            ChromiumWebBrowser browser = (sender as ChromiumWebBrowser);
            if (string.Equals(message, "[Korot.EditNewTabItem]"))
            {
                if (chromiumWebBrowser1.Address.ToLower().StartsWith("korot"))
                {
                    EditNewTabItem();
                }
            }
            else if (string.Equals(message, "[Korot.Notification.RequestPermission]"))
            {
                requestNotificationPermission(browser.Address);
            }
            else
            {
                if (message.ToLower().StartsWith("[korot.notification "))
                {
                    if (Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(browser.Address)).AllowNotifications)
                    {
                        MemoryStream stream = new MemoryStream();
                        StreamWriter writer = new StreamWriter(stream);
                        writer.Write(message.Replace("[", "<").Replace("]", ">"));
                        writer.Flush();
                        stream.Position = 0;
                        XmlDocument document = new XmlDocument();
                        document.Load(stream);
                        XmlNode node = document.DocumentElement;
                        string id = node.Attributes["ID"] != null ? node.Attributes["ID"].Value : "";
                        string image = node.Attributes["Icon"] != null ? node.Attributes["Icon"].Value : "";
                        string body = node.Attributes["Body"] != null ? node.Attributes["Body"].Value : "";
                        string title = node.Attributes["Message"] != null ? node.Attributes["Message"].Value : "";
                        string onclick = node.Attributes["onClick"] != null ? node.Attributes["onClick"].Value : "";
                        Notification newnot = new Notification() { id = id, url = browser.Address, message = body, title = title, imageUrl = image, action = onclick };
                        frmNotification existingNot = getNotificationByID(id);
                        if (existingNot != null) { existingNot.notification = newnot; }
                        else
                        {
                            PushNewNotification(newnot);
                        }
                    }
                }
            }
        }
        private void tabform_Load(object sender, EventArgs e)
        {
            if (NotificationListenerMode) { timer1.Start(); }
            Uri testUri = new Uri("https://haltroy.com");
            Uri aUri = WebRequest.GetSystemWebProxy().GetProxy(testUri);
            if (aUri != testUri)
            {
                defaultProxy = aUri.AbsoluteUri;
            }


            else
            {
                if (Settings.RememberLastProxy && !string.IsNullOrWhiteSpace(Settings.LastProxy)) { SetProxyAddress(Settings.LastProxy); }
            }
            frmCollection collectionManager = new frmCollection(this)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                Visible = true
            };
            ColMan = collectionManager;
            collectionManager.Show();
            panel3.Controls.Add(collectionManager);
            if (File.Exists(Settings.Theme.ThemeFile))
            {
                comboBox1.Text = new FileInfo(Settings.Theme.ThemeFile).Name.Replace(".ktf", "");
            }
            else
            {
                comboBox1.Text = "";
                Settings.Theme.Author = "";
                Settings.Theme.Name = "";
            }
            tbHomepage.Text = Settings.Homepage;
            tbSearchEngine.Text = Settings.SearchEngine;
            if (Settings.Homepage == "korot://newtab") { rbNewTab.Enabled = true; }
            pbBack.BackColor = Settings.Theme.BackColor;
            pbOverlay.BackColor = Settings.Theme.OverlayColor;
            RefreshLangList();
            refreshThemeList();
            ChangeTheme();
            lbVersion.Text = Application.ProductVersion.ToString() + (VersionInfo.IsPreRelease ? "-pre" + VersionInfo.PreReleaseNumber : "") + " " + "[" + VersionInfo.CodeName + "]" + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)");
            RefreshFavorites();
            chromiumWebBrowser1.Select();
            EasterEggs();
            RefreshTranslation();
            RefreshSizes();
            if (_Incognito)
            {
                foreach (Control x in tpSettings.Controls)
                {
                    if (x != btClose || x != btClose2 || x != btClose6 || x != btClose7 || x != btClose9) { x.Enabled = false; }
                }
                btInstall.Enabled = false;
                btUpdater.Enabled = false;
                btProfile.Enabled = false;
                cmsBStyle.Enabled = false;
                cmsSearchEngine.Enabled = false;
                btFav.Enabled = false;
                removeSelectedTSMI.Enabled = false;
                clearTSMI.Enabled = false;
                Settings.Theme.BackColor = Color.FromArgb(255, 64, 64, 64);
                Settings.Theme.OverlayColor = Color.DodgerBlue;
            }
            else
            {
                pbIncognito.Visible = false;
                tbAddress.Size = new Size(tbAddress.Size.Width + pbIncognito.Size.Width, tbAddress.Size.Height);
            }
            Settings.Extensions.UpdateExtensions();
        }

        private readonly string iconStorage = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\IconStorage\\";

        private void updateFavoritesImages()
        {
            foreach (ToolStripMenuItem x in favoritesFolders)
            {
                x.Image = HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.folder : Properties.Resources.folder_w;
            }
            foreach (ToolStripMenuItem x in favoritesNoIcon)
            {
                x.Image = HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.web : Properties.Resources.web_w;
            }
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is null) { return; }
            if (!(sender is ToolStripMenuItem)) { return; }
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            if (tsmi.Tag is null) { return; }
            selectedFavorite = tsmi;
            if (selectedFavorite.Tag is Favorite)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (tsmi.Tag != null)
                    {
                        if (tsmi.Tag is Favorite)
                        {
                            chromiumWebBrowser1.Load((tsmi.Tag as Favorite).Url);
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (tsmi.Tag != null)
                    {
                        tsopenInNewTab.Text =openInNewTab;
                        openİnNewWindowToolStripMenuItem.Text =openInNewWindow;
                        openİnNewIncognitoWindowToolStripMenuItem.Text =openInNewIncWindow;
                        cmsFavorite.Show(MousePosition);
                    }
                }
            }else if (selectedFavorite.Tag is Folder)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (tsmi.Tag != null)
                    {
                        tsopenInNewTab.Text = openAllInNewTab;
                        openİnNewWindowToolStripMenuItem.Text = openAllInNewWindow;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = openAllInNewIncWindow;
                        cmsFavorite.Show(MousePosition);
                    }
                }
            }
            
        }

        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot", listBox2.SelectedItem.ToString() + Environment.NewLine + ThemeMessage, new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true }) { StartPosition = FormStartPosition.CenterParent, Yes = Yes, No = No, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + listBox2.SelectedItem.ToString());
                    comboBox1.Text = listBox2.SelectedItem.ToString().Replace(".ktf", "");
                }
            }
        }
        #region "Translate"
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


        public void LoadLangFromFile(string fileLocation)
        {
            Settings.LanguageSystem.ReadFromFile(fileLocation, true);
            Extensions = Settings.LanguageSystem.GetItemText("Extensions");
            editblockitem = Settings.LanguageSystem.GetItemText("EditBlockItem");
            addblockitem = Settings.LanguageSystem.GetItemText("AddBlockItem");
            lv0 = Settings.LanguageSystem.GetItemText("Lv0");
            lv1 = Settings.LanguageSystem.GetItemText("Lv1");
            lv2 = Settings.LanguageSystem.GetItemText("Lv2");
            lv3 = Settings.LanguageSystem.GetItemText("Lv3");
            blocklevel = Settings.LanguageSystem.GetItemText("BlockLevel");
            Done = Settings.LanguageSystem.GetItemText("Done");
            BlockThisSite = Settings.LanguageSystem.GetItemText("BlockThisSite");
            lv0info = Settings.LanguageSystem.GetItemText("LV0Info");
            lv1info = Settings.LanguageSystem.GetItemText("LV1Info");
            lv2info = Settings.LanguageSystem.GetItemText("LV2Info");
            lv3info = Settings.LanguageSystem.GetItemText("LV3Info");
            btBlocked.Text = Settings.LanguageSystem.GetItemText("BlockMenuButton");
            tpBlock.Text = Settings.LanguageSystem.GetItemText("BlockMenuTitle");
            lbBlockedSites.Text = Settings.LanguageSystem.GetItemText("BlockMenuTitle");
            ChangePicInfo = Settings.LanguageSystem.GetItemText("ChangePicInfo");
            ResetImage = Settings.LanguageSystem.GetItemText("ResetImage");
            SelectNewImage = Settings.LanguageSystem.GetItemText("SelectNewImage");
            MuteThisTab = Settings.LanguageSystem.GetItemText("MuteThisTab");
            ChangePic = Settings.LanguageSystem.GetItemText("ChangePic");
            ProfileNameTemp = Settings.LanguageSystem.GetItemText("ProfileNameTemp");
            UnmuteThisTab = Settings.LanguageSystem.GetItemText("UnmuteThisTab");
            allowCookie = Settings.LanguageSystem.GetItemText("AllowCookie");
            aboutInfo = Settings.LanguageSystem.GetItemText("KorotAbout").Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + ((!(string.IsNullOrWhiteSpace(Settings.Theme.Author) && string.IsNullOrWhiteSpace(Settings.Theme.Name))) ? Settings.LanguageSystem.GetItemText("AboutInfoTheme").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Settings.Theme.Author) ? anon : Settings.Theme.Author).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Settings.Theme.Name) ? noname : Settings.Theme.Name) : "");
            NewTabEdit = Settings.LanguageSystem.GetItemText("NewTabEdit");
            tpNewTab.Text = Settings.LanguageSystem.GetItemText("NewTabEditorTitle");
            lbNewTabTitle.Text = Settings.LanguageSystem.GetItemText("NewTabEditorTitle");
            lbNTTitle.Text = Settings.LanguageSystem.GetItemText("NewTabEditTitle");
            lbNTUrl.Text = Settings.LanguageSystem.GetItemText("NewTabEditUrl");
            btClear.Text = Settings.LanguageSystem.GetItemText("NewTabEditClear");
            btNewTab.Text = Settings.LanguageSystem.GetItemText("NewTabEditButton");
            Clear = Settings.LanguageSystem.GetItemText("Clear");
            RemoveSelected = Settings.LanguageSystem.GetItemText("RemoveSelected");
            ImportProfile = Settings.LanguageSystem.GetItemText("ImportProfile");
            importProfileInfo = Settings.LanguageSystem.GetItemText("ImportProfileInfo");
            ExportProfile = Settings.LanguageSystem.GetItemText("ExportProfile");
            exportProfileInfo = Settings.LanguageSystem.GetItemText("ExportProfileInfo");
            ProfileFileInfo = Settings.LanguageSystem.GetItemText("ProfileFileInfo");
            string[] errormenu = new string[] { Settings.LanguageSystem.GetItemText("ErrorRestart"), Settings.LanguageSystem.GetItemText("ErrorDesc1"), Settings.LanguageSystem.GetItemText("ErrorDesc2"), Settings.LanguageSystem.GetItemText("ErrorTI") };
            SafeFileSettingOrganizedClass.ErrorMenu = errormenu;
            btNotification.Text = Settings.LanguageSystem.GetItemText("NotificationSettingsButton");
            lbNotifSetting.Text = Settings.LanguageSystem.GetItemText("NotificationSettings");
            tpNotification.Text = Settings.LanguageSystem.GetItemText("NotificationSettings");
            lbPlayNotifSound.Text = Settings.LanguageSystem.GetItemText("PlayNotificationSound");
            lbSilentMode.Text = Settings.LanguageSystem.GetItemText("SilentMode");
            lbSchedule.Text = Settings.LanguageSystem.GetItemText("ScheduleSilentMode");
            scheduleFrom.Text = Settings.LanguageSystem.GetItemText("StartFrom");
            scheduleTo.Text = Settings.LanguageSystem.GetItemText("EndAt");
            lb24HType.Text = Settings.LanguageSystem.GetItemText("24HourInfo");
            scheduleEvery.Text = Settings.LanguageSystem.GetItemText("Every");
            lbSunday.Text = Settings.LanguageSystem.GetItemText("Su");
            lbMonday.Text = Settings.LanguageSystem.GetItemText("M");
            lbTuesday.Text = Settings.LanguageSystem.GetItemText("T");
            lbWednesday.Text = Settings.LanguageSystem.GetItemText("W");
            lbThursday.Text = Settings.LanguageSystem.GetItemText("Th");
            lbFriday.Text = Settings.LanguageSystem.GetItemText("F");
            lbSaturday.Text = Settings.LanguageSystem.GetItemText("S");
            notificationPermission = Settings.LanguageSystem.GetItemText("NotificationInfo");
            deny = Settings.LanguageSystem.GetItemText("Deny");
            allow = Settings.LanguageSystem.GetItemText("Allow");
            changeColID = Settings.LanguageSystem.GetItemText("ChangeCollectionID");
            siteCookies = Settings.LanguageSystem.GetItemText("Cookies");
            siteNotifications = Settings.LanguageSystem.GetItemText("Notifications");
            changeColIDInfo = Settings.LanguageSystem.GetItemText("ChangeCollectionIDInfo");
            changeColText = Settings.LanguageSystem.GetItemText("ChangeCollectionText");
            changeColTextInfo = Settings.LanguageSystem.GetItemText("ChangeCollectionTextInfo");
            importColItem = Settings.LanguageSystem.GetItemText("ImportItemInfo");
            importColItemInfo = Settings.LanguageSystem.GetItemText("ImportItem");
            SetToDefault = Settings.LanguageSystem.GetItemText("SetToDefault");
            titleBackInfo = Settings.LanguageSystem.GetItemText("ChangeTitleColorInfo");
            addToCollection = Settings.LanguageSystem.GetItemText("AddToCollection");
            newColInfo = Settings.LanguageSystem.GetItemText("NewCollectionInfo");
            newColName = Settings.LanguageSystem.GetItemText("NewCollectionName");
            importColInfo = Settings.LanguageSystem.GetItemText("ImportCollectionInfo");
            delColInfo = Settings.LanguageSystem.GetItemText("CollectionDeleteInfo");
            clearColInfo = Settings.LanguageSystem.GetItemText("CollectionClearInfo");
            okToClipboard = Settings.LanguageSystem.GetItemText("OKTOClipboard");

            deleteCollection = Settings.LanguageSystem.GetItemText("DeleteCollection");

            importCollection = Settings.LanguageSystem.GetItemText("Import");
            exportCollection = Settings.LanguageSystem.GetItemText("Export");
            deleteItem = Settings.LanguageSystem.GetItemText("DeleteItem");
            exportItem = Settings.LanguageSystem.GetItemText("ExportItem");
            editItem = Settings.LanguageSystem.GetItemText("EditItem");
            catCommon = Settings.LanguageSystem.GetItemText("CollectionCommon");
            catText = Settings.LanguageSystem.GetItemText("CollectionTextBased");
            catOnline = Settings.LanguageSystem.GetItemText("CollectionOnline");
            catPicture = Settings.LanguageSystem.GetItemText("CollectionPicture");
            TitleID = Settings.LanguageSystem.GetItemText("CollectionID");
            TitleBackColor = Settings.LanguageSystem.GetItemText("CollectionBackColor");
            TitleText = Settings.LanguageSystem.GetItemText("CollectionText");
            TitleFont = Settings.LanguageSystem.GetItemText("CollectionFont");
            TitleSize = Settings.LanguageSystem.GetItemText("CollectionFontSize");
            TitleProp = Settings.LanguageSystem.GetItemText("CollectionFontProperties");
            TitleRegular = Settings.LanguageSystem.GetItemText("Regular");
            TitleBold = Settings.LanguageSystem.GetItemText("Bold");
            TitleItalic = Settings.LanguageSystem.GetItemText("Italic");
            TitleUnderline = Settings.LanguageSystem.GetItemText("Underline");
            TitleStrikeout = Settings.LanguageSystem.GetItemText("Strikeout");
            TitleForeColor = Settings.LanguageSystem.GetItemText("CollectionForeColor");
            TitleSource = Settings.LanguageSystem.GetItemText("CollectionSource");
            TitleWidth = Settings.LanguageSystem.GetItemText("CollectionWidth");
            TitleHeight = Settings.LanguageSystem.GetItemText("CollectionHeight");
            TitleDone = Settings.LanguageSystem.GetItemText("CollectionDone");
            TitleEditItem = Settings.LanguageSystem.GetItemText("EditCollectionItem");
            image = Settings.LanguageSystem.GetItemText("CollectionItemImage");
            text = Settings.LanguageSystem.GetItemText("CollectionItemText");
            link = Settings.LanguageSystem.GetItemText("CollectionItemLink");
            lbCollections.Text = Settings.LanguageSystem.GetItemText("Collections");
            Collections = Settings.LanguageSystem.GetItemText("Collections");
            tpCert.Text = Settings.LanguageSystem.GetItemText("CertificateError");
            tpAbout.Text = Settings.LanguageSystem.GetItemText("About");
            AboutText = Settings.LanguageSystem.GetItemText("About");
            tpSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
            SettingsText = Settings.LanguageSystem.GetItemText("Settings");
            tpSite.Text = Settings.LanguageSystem.GetItemText("SiteSettings");
            tpCollection.Text = Settings.LanguageSystem.GetItemText("Collections");
            tpDownload.Text = Settings.LanguageSystem.GetItemText("Downloads");
            DownloadsText = Settings.LanguageSystem.GetItemText("Downloads");
            tpHistory.Text = Settings.LanguageSystem.GetItemText("History");
            HistoryText = Settings.LanguageSystem.GetItemText("History");
            tpTheme.Text = Settings.LanguageSystem.GetItemText("Themes");
            ThemesText = Settings.LanguageSystem.GetItemText("Themes");
            lbautoRestore.Text = Settings.LanguageSystem.GetItemText("RestoreOldSessions");

            ubuntuLicense = Settings.LanguageSystem.GetItemText("UbuntuFontLicense");
            updateTitleTheme = Settings.LanguageSystem.GetItemText("KorotThemeUpdater");
            updateTitleExt = Settings.LanguageSystem.GetItemText("KorotExtensionUpdater");
            updateExtInfo = Settings.LanguageSystem.GetItemText("ExtensionUpdatingInfo");
            openInNewWindow = Settings.LanguageSystem.GetItemText("OpeninNewWindow");
            openAllInNewWindow = Settings.LanguageSystem.GetItemText("OpenAllinNewWindow");
            openInNewIncWindow = Settings.LanguageSystem.GetItemText("OpeninNewIncognitoWindow");
            openAllInNewIncWindow = Settings.LanguageSystem.GetItemText("OpenAllinNewIncognitoWindow");
            openAllInNewTab = Settings.LanguageSystem.GetItemText("OpenAllInNewTab");
            newFavorite = Settings.LanguageSystem.GetItemText("NewFavorite");
            nametd = Settings.LanguageSystem.GetItemText("Name");
            urltd = Settings.LanguageSystem.GetItemText("Url");
            add = Settings.LanguageSystem.GetItemText("Add");
            newFavoriteToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("NewFavorite");
            newFolderToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("NewFolderButton");
            newFolder = Settings.LanguageSystem.GetItemText("NewFolderButton");
            defaultFolderName = Settings.LanguageSystem.GetItemText("NewFolder");
            folderInfo = Settings.LanguageSystem.GetItemText("PleaseenteranamefornewFolder");
            copyImage = Settings.LanguageSystem.GetItemText("CopyImage");
            openLinkInNewWindow = Settings.LanguageSystem.GetItemText("OpenLinkinaNewWindow");
            openLinkInNewIncWindow = Settings.LanguageSystem.GetItemText("OpenLinkinaNewIncognitoWindow");
            copyImageAddress = Settings.LanguageSystem.GetItemText("CopyImageAddress");
            saveLinkAs = Settings.LanguageSystem.GetItemText("SaveLinkAs");
            empty = Settings.LanguageSystem.GetItemText("Empty");
            btCleanLog.Text = Settings.LanguageSystem.GetItemText("CleanLogData");
            lbShowFavorites.Text = Settings.LanguageSystem.GetItemText("ShowFavoritesMenu");
            lbNewTabColor.Text = Settings.LanguageSystem.GetItemText("NewTabButtonColor");
            lbCloseColor.Text = Settings.LanguageSystem.GetItemText("CloseButtonColor");
            rbNone.Text = Settings.LanguageSystem.GetItemText("None");
            rbTile.Text = Settings.LanguageSystem.GetItemText("Tile");
            rbCenter.Text = Settings.LanguageSystem.GetItemText("Center");
            rbStretch.Text = Settings.LanguageSystem.GetItemText("Stretch");
            rbZoom.Text = Settings.LanguageSystem.GetItemText("Zoom");
            rbBackColor.Text = Settings.LanguageSystem.GetItemText("BackColor");
            rbForeColor.Text = Settings.LanguageSystem.GetItemText("ForeColor");
            rbOverlayColor.Text = Settings.LanguageSystem.GetItemText("OverlayColor2");
            rbBackColor1.Text = Settings.LanguageSystem.GetItemText("BackColor");
            rbForeColor1.Text = Settings.LanguageSystem.GetItemText("ForeColor");
            rbOverlayColor1.Text = Settings.LanguageSystem.GetItemText("OverlayColor2");
            licenseTitle = Settings.LanguageSystem.GetItemText("TitleLicensesSpecialThanks");
            kLicense = Settings.LanguageSystem.GetItemText("KorotLicense");
            vsLicense = Settings.LanguageSystem.GetItemText("MSVS2019CLicense");
            chLicense = Settings.LanguageSystem.GetItemText("ChromiumLicense");
            cefLicense = Settings.LanguageSystem.GetItemText("CefSharpLicense");
            etLicense = Settings.LanguageSystem.GetItemText("EasyTabsLicense");
            specialThanks = Settings.LanguageSystem.GetItemText("SpecialThanks");
            JSAlert = Settings.LanguageSystem.GetItemText("MessageDialog");
            JSConfirm = Settings.LanguageSystem.GetItemText("ConfirmDialog");
            selectAFolder = Settings.LanguageSystem.GetItemText("DownloadFolderInfo");
            showNewTabPageToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ShowNewTabPage");
            showHomepageToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ShowHomepage");
            showAWebsiteToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("GoToURL");
            lbDownloadFolder.Text = Settings.LanguageSystem.GetItemText("DownloadToFolder");
            lbAutoDownload.Text = Settings.LanguageSystem.GetItemText("Auto-downloadFolder");
            lbAtStartup.Text = Settings.LanguageSystem.GetItemText("AtStartup");
            btReset.Text = Settings.LanguageSystem.GetItemText("ResetKorotButton");
            resetConfirm = Settings.LanguageSystem.GetItemText("ResetKorotInfo");
            lbSiteSettings.Text = Settings.LanguageSystem.GetItemText("SiteSettings");
            btCookie.Text = Settings.LanguageSystem.GetItemText("SiteSettingsButton");
            IncognitoModeTitle = Settings.LanguageSystem.GetItemText("IncognitoMode");
            IncognitoModeInfo = Settings.LanguageSystem.GetItemText("IncognitoModeInfo");
            LearnMore = Settings.LanguageSystem.GetItemText("ClickToLearnMore");
            lbLastProxy.Text = Settings.LanguageSystem.GetItemText("RememberLastProxy");
            findC = Settings.LanguageSystem.GetItemText("Current");
            findT = Settings.LanguageSystem.GetItemText("Total");
            findL = Settings.LanguageSystem.GetItemText("Last");
            noSearch = Settings.LanguageSystem.GetItemText("NotSearchingNoResults");
            anon = Settings.LanguageSystem.GetItemText("ThemeUnknownPerson");
            noname = Settings.LanguageSystem.GetItemText("ThemeNameUnknown");
            themeInfo = Settings.LanguageSystem.GetItemText("AboutInfoTheme");
            renderProcessDies = Settings.LanguageSystem.GetItemText("RenderProcessTerminated");
            enterAValidCode = Settings.LanguageSystem.GetItemText("EnterValidBase64");
            ResetZoom = Settings.LanguageSystem.GetItemText("ResetZoom");
            htmlFiles = Settings.LanguageSystem.GetItemText("HTMLFile");
            print = Settings.LanguageSystem.GetItemText("Print");

            IncognitoTitle1 = Settings.LanguageSystem.GetItemText("IncognitoInfoTitle");
            IncognitoT1M1 = Settings.LanguageSystem.GetItemText("IncognitoInfoT1M1");
            IncognitoT1M2 = Settings.LanguageSystem.GetItemText("IncognitoInfoT1M2");
            IncognitoT1M3 = Settings.LanguageSystem.GetItemText("IncognitoInfoT1M3");
            IncognitoTitle2 = Settings.LanguageSystem.GetItemText("IncognitoInfoTitle2");
            IncognitoT2M1 = Settings.LanguageSystem.GetItemText("IncognitoInfoT2M1");
            IncognitoT2M2 = Settings.LanguageSystem.GetItemText("IncognitoInfoT2M2");
            IncognitoT2M3 = Settings.LanguageSystem.GetItemText("IncognitoInfoT2M3");
            disallowCookie = Settings.LanguageSystem.GetItemText("DisallowCookie");
            lbBackImageStyle.Text = Settings.LanguageSystem.GetItemText("BackgroundImageLayout");
            imageFiles = Settings.LanguageSystem.GetItemText("ImageFiles");
            allFiles = Settings.LanguageSystem.GetItemText("AllFiles");
            selectBackImage = Settings.LanguageSystem.GetItemText("SelectBackgroundImage");
            colorToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("UseBackgroundColor");
            usingBC = Settings.LanguageSystem.GetItemText("UsingBackgroundColor");
            ımageFromURLToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ImageFromBase64");
            ımageFromLocalFileToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ImageFromFile");
            lbDNT.Text = Settings.LanguageSystem.GetItemText("EnableDoNotTrack");
            lbFlash.Text = Settings.LanguageSystem.GetItemText("EnableFlash");
            lbFlashInfo.Text = Settings.LanguageSystem.GetItemText("FlashInfo");
            label21.Text = aboutInfo;
            llLicenses.Text = Settings.LanguageSystem.GetItemText("LicensesSpecialThanks");
            lbSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
            CertErrorPageButton = Settings.LanguageSystem.GetItemText("UserUnderstandsRisks");
            CertErrorPageMessage = Settings.LanguageSystem.GetItemText("WebsiteNotSafeInfo");
            CertErrorPageTitle = Settings.LanguageSystem.GetItemText("WebsiteNotSafe");
            usesCookies = Settings.LanguageSystem.GetItemText("WebsiteUsesCookies");
            notUsesCookies = Settings.LanguageSystem.GetItemText("WebsiteNoCookies");
            showCertError = Settings.LanguageSystem.GetItemText("ShowCertificateError");
            CertificateErrorMenuTitle = Settings.LanguageSystem.GetItemText("CertificateErrorDetails");
            CertificateErrorTitle = Settings.LanguageSystem.GetItemText("NotSafe");
            CertificateError = Settings.LanguageSystem.GetItemText("WebsiteWithErrors");
            CertificateOKTitle = Settings.LanguageSystem.GetItemText("Safe");
            CertificateOK = Settings.LanguageSystem.GetItemText("WebsiteNoErrors");
            ErrorTheme = Settings.LanguageSystem.GetItemText("ThemeFileCorrupted");
            ThemeMessage = Settings.LanguageSystem.GetItemText("ApplyThemeInfo");
            btUpdater.Text = Settings.LanguageSystem.GetItemText("CheckForUpdates");
            btInstall.Text = Settings.LanguageSystem.GetItemText("InstallUpdate");
            checking = Settings.LanguageSystem.GetItemText("CheckingForUpdates");
            uptodate = Settings.LanguageSystem.GetItemText("UpToDate");
            installStatus = Settings.LanguageSystem.GetItemText("UpdatingMessage");
            StatusType = Settings.LanguageSystem.GetItemText("DownloadProgress");
            rbNewTab.Text = Settings.LanguageSystem.GetItemText("NewTab");
            switch (updateProgress)
            {
                case 0:
                    lbUpdateStatus.Text = Settings.LanguageSystem.GetItemText("CheckingForUpdates");
                    break;
                case 1:
                    lbUpdateStatus.Text = Settings.LanguageSystem.GetItemText("UpToDate");
                    break;
                case 2:
                    lbUpdateStatus.Text = Settings.LanguageSystem.GetItemText("UpdateAvailable");
                    break;
                case 3:
                    lbUpdateStatus.Text = Settings.LanguageSystem.GetItemText("KorotUpdateError");
                    break;
            }
            updateavailable = Settings.LanguageSystem.GetItemText("UpdateAvailable"); ;
            privatemode = Settings.LanguageSystem.GetItemText("Incognito");
            updateTitle = Settings.LanguageSystem.GetItemText("KorotUpdate");
            updateMessage = Settings.LanguageSystem.GetItemText("KorotUpdateAvailable");
            updateError = Settings.LanguageSystem.GetItemText("KorotUpdateError");
            NewTabtitle = Settings.LanguageSystem.GetItemText("NewTab");
            customSearchNote = Settings.LanguageSystem.GetItemText("SearchEngineInfo");
            customSearchMessage = Settings.LanguageSystem.GetItemText("SearchengineTitle");
            lbBackImage.Text = Settings.LanguageSystem.GetItemText("BackgroundStyle");
            newWindow = Settings.LanguageSystem.GetItemText("NewWindow");
            newincognitoWindow = Settings.LanguageSystem.GetItemText("NewIncognitoWindow");
            lbDownloads.Text = Settings.LanguageSystem.GetItemText("Downloads");
            lbHomepage.Text = Settings.LanguageSystem.GetItemText("HomePage");
            SearchOnPage = Settings.LanguageSystem.GetItemText("SearchOnThisPage");
            lbTheme.Text = Settings.LanguageSystem.GetItemText("Themes");
            customToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("Custom");
            settingstitle = Settings.LanguageSystem.GetItemText("Settings");
            lbHistory.Text = Settings.LanguageSystem.GetItemText("History");
            lbAbout.Text = Settings.LanguageSystem.GetItemText("About");
            IncognitoT = Settings.LanguageSystem.GetItemText("Incognito");
            IncognitoTitle = Settings.LanguageSystem.GetItemText("IncognitoInfoTitle");
            newCollection = Settings.LanguageSystem.GetItemText("NewCollectionName");
            clearCollection = Settings.LanguageSystem.GetItemText("DeleteCollection");
            goBack = Settings.LanguageSystem.GetItemText("GoBack");
            goForward = Settings.LanguageSystem.GetItemText("GoForward");
            refresh = Settings.LanguageSystem.GetItemText("Refresh");
            refreshNoCache = Settings.LanguageSystem.GetItemText("RefreshNoCache");
            stop = Settings.LanguageSystem.GetItemText("Stop");
            selectAll = Settings.LanguageSystem.GetItemText("SelectAll");
            openLinkInNewTab = Settings.LanguageSystem.GetItemText("OpenLinkInNewTab");
            copyLink = Settings.LanguageSystem.GetItemText("CopyLink");
            saveImageAs = Settings.LanguageSystem.GetItemText("SaveImageAs");
            openImageInNewTab = Settings.LanguageSystem.GetItemText("OpenImageInNewTab");
            paste = Settings.LanguageSystem.GetItemText("Paste");
            copy = Settings.LanguageSystem.GetItemText("Copy");
            cut = Settings.LanguageSystem.GetItemText("Cut");
            undo = Settings.LanguageSystem.GetItemText("Undo");
            redo = Settings.LanguageSystem.GetItemText("Redo");
            delete = Settings.LanguageSystem.GetItemText("Delete");
            SearchOrOpenSelectedInNewTab = Settings.LanguageSystem.GetItemText("SearchOpenTheSelected");
            developerTools = Settings.LanguageSystem.GetItemText("DeveloperTools");
            viewSource = Settings.LanguageSystem.GetItemText("ViewSource");
            restoreOldSessions = Settings.LanguageSystem.GetItemText("RestoreLastSession");
            lbBackColor.Text = Settings.LanguageSystem.GetItemText("BackgroundColor");
            enterAValidUrl = Settings.LanguageSystem.GetItemText("EnterAValidURL");
            lbOveralColor.Text = Settings.LanguageSystem.GetItemText("OverlayColor");
            Month1 = Settings.LanguageSystem.GetItemText("Month1");
            Month2 = Settings.LanguageSystem.GetItemText("Month2");
            Month3 = Settings.LanguageSystem.GetItemText("Month3");
            Month4 = Settings.LanguageSystem.GetItemText("Month4");
            Month5 = Settings.LanguageSystem.GetItemText("Month5");
            Month6 = Settings.LanguageSystem.GetItemText("Month6");
            Month7 = Settings.LanguageSystem.GetItemText("Month7");
            Month8 = Settings.LanguageSystem.GetItemText("Month8");
            Month9 = Settings.LanguageSystem.GetItemText("Month9");
            Month10 = Settings.LanguageSystem.GetItemText("Month10");
            Month11 = Settings.LanguageSystem.GetItemText("Month11");
            Month12 = Settings.LanguageSystem.GetItemText("Month12");
            Month0 = Settings.LanguageSystem.GetItemText("Month0");
            fromtwodot = Settings.LanguageSystem.GetItemText("From1");
            totwodot = Settings.LanguageSystem.GetItemText("To1");
            korotdownloading = Settings.LanguageSystem.GetItemText("KorotDownloading");
            lbOpen.Text = Settings.LanguageSystem.GetItemText("OpenFilesAfterDownload");
            open = Settings.LanguageSystem.GetItemText("Open");
            openLinkInNewTab = Settings.LanguageSystem.GetItemText("OpenLinkInNewTab");
            removeSelectedTSMI.Text = Settings.LanguageSystem.GetItemText("RemoveSelected");
            clearTSMI.Text = Settings.LanguageSystem.GetItemText("Clear");
            OpenInNewTab = Settings.LanguageSystem.GetItemText("OpenInNewTab");
            OpenFile = Settings.LanguageSystem.GetItemText("OpenFile");
            OpenFileInExplorert = Settings.LanguageSystem.GetItemText("OpenFolderContainingThisFile");
            ResetToDefaultProxy = Settings.LanguageSystem.GetItemText("ResetToFefaultProxySetting");
            lbThemeName.Text = Settings.LanguageSystem.GetItemText("ThemeName");
            Yes = Settings.LanguageSystem.GetItemText("Yes");
            No = Settings.LanguageSystem.GetItemText("No");
            OK = Settings.LanguageSystem.GetItemText("OK");
            Cancel = Settings.LanguageSystem.GetItemText("Cancel");
            btCertError.Text = Settings.LanguageSystem.GetItemText("Save");
            lbThemes.Text = Settings.LanguageSystem.GetItemText("ThemeList");
            SearchOnWeb = Settings.LanguageSystem.GetItemText("AddressBar2");
            goTotxt = Settings.LanguageSystem.GetItemText("AddressBar1");
            newProfileInfo = Settings.LanguageSystem.GetItemText("EnterAProfileName");
            lbSearchEngine.Text = Settings.LanguageSystem.GetItemText("SearchEngine");
            MonthNames = Settings.LanguageSystem.GetItemText("NewTabMonths");
            DayNames = Settings.LanguageSystem.GetItemText("NewTabDays");
            SearchHelpText = Settings.LanguageSystem.GetItemText("NewTabSearch");
            ErrorPageTitle = Settings.LanguageSystem.GetItemText("KorotError");
            KT = Settings.LanguageSystem.GetItemText("ErrorTitle");
            ET = Settings.LanguageSystem.GetItemText("ErrorTitle1");
            E1 = Settings.LanguageSystem.GetItemText("ErrorT1M1");
            E2 = Settings.LanguageSystem.GetItemText("ErrorT1M2");
            E3 = Settings.LanguageSystem.GetItemText("ErrorT1M3");
            E4 = Settings.LanguageSystem.GetItemText("ErrorT1M4");
            RT = Settings.LanguageSystem.GetItemText("ErrorTitle2");
            R1 = Settings.LanguageSystem.GetItemText("ErrorT2M1");
            R2 = Settings.LanguageSystem.GetItemText("ErrorT2M2");
            R3 = Settings.LanguageSystem.GetItemText("ErrorT2M3");
            R4 = Settings.LanguageSystem.GetItemText("ErrorT2M4");
            Search = Settings.LanguageSystem.GetItemText("Search");
            newprofile = Settings.LanguageSystem.GetItemText("NewProfile");
            switchTo = Settings.LanguageSystem.GetItemText("SwitchTo");
            deleteProfile = Settings.LanguageSystem.GetItemText("DeleteThisProfile");
            RefreshTranslation();
            RefreshSizes();
        }
        public void RefreshLangList()
        {
            cbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.klf", SearchOption.TopDirectoryOnly))
            {
                cbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }
            string selected = Path.GetFileNameWithoutExtension(Settings.LanguageSystem.LangFile);
            cbLang.SelectedItem = selected;
            cbLang.Text = selected;
        }
        #endregion
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox inputb = new HTAlt.WinForms.HTInputBox(customSearchNote, customSearchMessage, Settings.SearchEngine) { Icon = Icon, SetToDefault = SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (ValidHttpURL(inputb.TextValue) && !inputb.TextValue.StartsWith("korot://") && !inputb.TextValue.StartsWith("file://") && !inputb.TextValue.StartsWith("about"))
                {
                    Settings.SearchEngine = inputb.TextValue;
                    tbSearchEngine.Text = Settings.SearchEngine;
                }
                else
                {
                    customToolStripMenuItem_Click(null, null);
                }
            }
        }
        private void SearchEngineSelection_Click(object sender, EventArgs e)
        {
            Settings.SearchEngine = ((ToolStripMenuItem)sender).Tag.ToString();
            tbSearchEngine.Text = Settings.SearchEngine;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (tbHomepage.Text.ToLower().StartsWith("korot://newtab"))
            {
                rbNewTab.Checked = true;
                Settings.Homepage = tbHomepage.Text;
            }
            else
            {
                rbNewTab.Checked = false;
                Settings.Homepage = tbHomepage.Text;
            }
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            cmsSearchEngine.Show(MousePosition);
        }
        private void openmytaginnewtab(object sender, LinkLabelLinkClickedEventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab(((LinkLabel)sender).Tag.ToString())));
        }
        private void ClearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Settings.Favorites.Favorites.Clear();
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
                pbBack.BackColor = colorpicker.Color;
                Settings.Theme.BackColor = colorpicker.Color;
                pbBack.BackColor = colorpicker.Color;
                ChangeTheme();
            }

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
                pbOverlay.BackColor = colorpicker.Color;
                Settings.Theme.OverlayColor = colorpicker.Color;
                pbOverlay.BackColor = colorpicker.Color;
                ChangeTheme();
            }
        }

        public string GetMonthNameOfDate(int month)
        {
            switch (month)
            {
                case 1:
                    return Month1;
                case 2:
                    return Month2;
                case 3:
                    return Month3;
                case 4:
                    return Month4;
                case 5:
                    return Month5;
                case 6:
                    return Month6;
                case 7:
                    return Month7;
                case 8:
                    return Month8;
                case 9:
                    return Month9;
                case 10:
                    return Month10;
                case 11:
                    return Month11;
                case 12:
                    return Month12;
                default:
                    return Month0; // You cannot see this month in an ordinary usage of Korot. This means that this month is not real so I put my name which I should not exist too.
            }
        }

        public string GetDateInfo(DateTime date)
        {
            return date.Day + " " + GetMonthNameOfDate(date.Month) + " " + date.Year + " " + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00");
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Theme.BackgroundStyle = "BACKCOLOR";
            textBox4.Text = usingBC;
            colorToolStripMenuItem.Checked = true;

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
            HTAlt.WinForms.HTInputBox inputbox = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                            enterAValidCode,
                                                                                            "")
            { Icon = Icon, SetToDefault = SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                Settings.Theme.BackgroundStyle = inputbox.TextValue + ";";
                textBox4.Text = Settings.Theme.BackgroundStyle;
                colorToolStripMenuItem.Checked = false;
            }

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
                if (HTAlt.Tools.ImageToBase64(Image.FromFile(filedlg.FileName)).Length <= 131072)
                {
                    string imageType = Path.GetExtension(filedlg.FileName).Replace(".", "");
                    Settings.Theme.BackgroundStyle = "background-image: url('data:image/" + imageType + ";base64," + HTAlt.Tools.ImageToBase64(Image.FromFile(filedlg.FileName)) + "');";
                    textBox4.Text = Settings.Theme.BackgroundStyle;
                    colorToolStripMenuItem.Checked = false;
                }
                else
                {
                    FromLocalFileToolStripMenuItem_Click(sender, e);
                }
            }

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewTab.Checked)
            {
                Settings.Homepage = "korot://newtab";
                tbHomepage.Text = Settings.Homepage;
            }
        }
        public int fromH = -1;
        public int fromM = -1;
        public int toH = -1;
        public int toM = -1;
        public bool Nsunday = false;
        public bool Nmonday = false;
        public bool Ntuesday = false;
        public bool Nwednesday = false;
        public bool Nthursday = false;
        public bool Nfriday = false;
        public bool Nsaturday = false;
        public void RefreshScheduledSiletMode()
        {
            if (Settings.AutoSilent)
            {
                string Playlist = Settings.AutoSilentMode;
                string[] SplittedFase = Playlist.Split(':');
                if (SplittedFase.Length - 1 > 9)
                {

                    fromHour.Value = Convert.ToInt32(SplittedFase[0]);
                    fromMin.Value = Convert.ToInt32(SplittedFase[1]);
                    toHour.Value = Convert.ToInt32(SplittedFase[2]);
                    toMin.Value = Convert.ToInt32(SplittedFase[3]);
                    bool sunday = SplittedFase[4] == "1";
                    bool monday = SplittedFase[5] == "1";
                    bool tuesday = SplittedFase[6] == "1";
                    bool wednesday = SplittedFase[7] == "1";
                    bool thursday = SplittedFase[8] == "1";
                    bool friday = SplittedFase[9] == "1";
                    bool saturday = SplittedFase[10] == "1";
                    fromH = Convert.ToInt32(SplittedFase[0]);
                    fromM = Convert.ToInt32(SplittedFase[1]);
                    toH = Convert.ToInt32(SplittedFase[2]);
                    toM = Convert.ToInt32(SplittedFase[3]);
                    Nsunday = sunday;
                    Nmonday = monday;
                    Ntuesday = tuesday;
                    Nwednesday = wednesday;
                    Nthursday = thursday;
                    Nfriday = friday;
                    Nsaturday = saturday;
                    lbSunday.BackColor = sunday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbMonday.BackColor = monday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbTuesday.BackColor = tuesday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbWednesday.BackColor = wednesday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbThursday.BackColor = thursday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbFriday.BackColor = friday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbSaturday.BackColor = saturday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbSunday.Tag = sunday ? "1" : "0";
                    lbMonday.Tag = monday ? "1" : "0";
                    lbTuesday.Tag = tuesday ? "1" : "0";
                    lbWednesday.Tag = wednesday ? "1" : "0";
                    lbThursday.Tag = thursday ? "1" : "0";
                    lbFriday.Tag = friday ? "1" : "0";
                    lbSaturday.Tag = saturday ? "1" : "0";
                }
            }
        }
        public void writeSchedules()
        {
            string ScheduleBuild = fromHour.Value + ":";
            ScheduleBuild += fromMin.Value + ":";
            ScheduleBuild += toHour.Value + ":";
            ScheduleBuild += toMin.Value + ":";
            ScheduleBuild += (lbSunday.Tag != null ? lbSunday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbMonday.Tag != null ? lbMonday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbTuesday.Tag != null ? lbTuesday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbWednesday.Tag != null ? lbWednesday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbThursday.Tag != null ? lbThursday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbFriday.Tag != null ? lbFriday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbSaturday.Tag != null ? lbSaturday.Tag.ToString() : "0") + ":";
            Settings.AutoSilentMode = ScheduleBuild;
        }
        private void tmrRefresher_Tick(object sender, EventArgs e)
        {
        }

        private void label15_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                refreshThemeList();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\Themes\\" + "\"");
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
        private void Label2_Click(object sender, EventArgs e)
        {
            NewTab("https://haltroy.com/Korot.html");
        }

        public void LoadTheme(string ThemeFile)
        {
            Settings.Theme.LoadFromFile(ThemeFile);
        }
        public void refreshThemeList()
        {
            int savedValue = listBox2.SelectedIndex;
            int scroll = listBox2.TopIndex;
            listBox2.Items.Clear();
            foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\"))
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
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\"); }
            string themeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + comboBox1.Text + ".ktf";
            Theme saveTheme = new Theme("")
            {
                ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + comboBox1.Text + ".ktf",
                BackColor = Settings.Theme.BackColor,
                OverlayColor = Settings.Theme.OverlayColor,
                MininmumKorotVersion = new Version(Application.ProductVersion),
                Version = new Version(Application.ProductVersion),
                Name = comboBox1.Text,
                Author = userName,
                BackgroundStyle = Settings.Theme.BackgroundStyle,
                BackgroundStyleLayout = Settings.Theme.BackgroundStyleLayout,
                CloseButtonColor = Settings.Theme.CloseButtonColor,
                NewTabColor = Settings.Theme.NewTabColor
            };
            saveTheme.SaveTheme();
            Settings.Theme.ThemeFile = themeFile;
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

        private void updateAvailable()
        {
            if (alreadyCheckedForUpdatesOnce || Settings.DismissUpdate || _Incognito || NotificationListenerMode)
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
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(
                    updateTitle,
                    updateMessage,
                    new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true })
                { StartPosition = FormStartPosition.CenterParent, Yes = Yes, No = No, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
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
                Settings.DismissUpdate = true;
            }
        }

        private string NewestPreVer = "";
        private void UpdateResult(string info)
        {
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase3 = info.Split(token);
            string preNo = SplittedFase3[5].Substring(1).Replace(Environment.NewLine, "");
            string preNewest = SplittedFase3[4].Substring(1).Replace(Environment.NewLine, "") + "-pre" + preNo;
            Version newest = new Version(SplittedFase3[0].Replace(Environment.NewLine, ""));
            Version current = new Version(Application.ProductVersion);
            if (VersionInfo.IsPreRelease)
            {
                if (preNo == "0")
                {
                    updateAvailable();
                }
                else
                {
                    NewestPreVer = preNewest;
                    if (Convert.ToInt32(preNo) > VersionInfo.PreReleaseNumber)
                    {
                        updateAvailable();
                    }
                    else
                    {
                        btUpdater.Visible = true;
                        btInstall.Visible = false;
                        updateProgress = 1;
                        lbUpdateStatus.Text = uptodate;
                    }
                }
            }
            else
            {
                if (newest > current)
                {
                    updateAvailable();
                }
                else
                {
                    btUpdater.Visible = true;
                    btInstall.Visible = false;
                    updateProgress = 1;
                    lbUpdateStatus.Text = uptodate;
                }
            }
        }
        public HTTitleTabs ParentTabs => (ParentForm as HTTitleTabs);
        public HTTitleTab ParentTab
        {
            get
            {
                List<int> tabIndexes = new List<int>();
                foreach (HTTitleTab x in ParentTabs.Tabs)
                {
                    if (x.Content == this) { tabIndexes.Add(ParentTabs.Tabs.IndexOf(x)); }
                }
                return (tabIndexes.Count > 0 ? ParentTabs.Tabs[tabIndexes[0]] : null);
            }
        }
        public void SetProxy(string ProxyFile)
        {
            List<Proxy> ProxyList = new List<Proxy>();
            List<Proxy> ErrorProxies = new List<Proxy>();
            XmlDocument document = new XmlDocument();
            document.LoadXml(HTAlt.Tools.ReadFile(ProxyFile, Encoding.UTF8));
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                if (node.Name.ToLower() == "proxy")
                {
                    if (node.Attributes["IP"] == null) { return; }
                    Proxy prx = new Proxy
                    {
                        ID = node.Attributes["ID"] != null ? node.Attributes["ID"].Value : HTAlt.Tools.GenerateRandomText(12),
                        Address = node.Attributes["IP"].Value
                    };
                    ProxyList.Add(prx);
                }
            }
            foreach (Proxy prx in ProxyList)
            {
                try
                {
                    SetProxyAddress(prx.Address);
                    Settings.LastProxy = prx.Address;
                    Output.WriteLine(" [Korot.Proxy] Set proxy to \"" + prx.Address + "\" (ID=\"" + prx.ID + "\" File=\"" + ProxyFile + "\")");
                    break;
                }
                catch (Exception ex)
                {
                    prx.Exception = ex;
                    ProxyList.Remove(prx);
                    ErrorProxies.Add(prx);
                }
            }
            foreach (Proxy prx in ErrorProxies)
            {
                Output.WriteLine(" [Korot.Proxy] Cannot set proxy to \"" + prx.Address + "\" (ID=\"" + prx.ID + "\" File=\"" + ProxyFile + "\" Exception=\"" + prx.Exception.ToString() + "\")");
            }
        }
        public async void SetProxyAddress(string Address)
        {
            if (Address == null) { }
            else
            {
                await Cef.UIThreadTaskFactory.StartNew(delegate
                {
                    IRequestContext rc = chromiumWebBrowser1.GetBrowser().GetHost().RequestContext;
                    Dictionary<string, object> v = new Dictionary<string, object>
                    {
                        ["mode"] = "fixed_servers",
                        ["server"] = Address
                    };
                    bool success = rc.SetPreference("proxy", v, out string error);
                });
            }
        }

        private void cef_GotFocus(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                privmenu.Hide();
                incognitomenu.Hide();
                profmenu.Hide();
                hammenu.Hide();
            }));
        }
        private void cef_LostFocus(object sender, EventArgs e)
        {
            if (cmsCEF != null)
            {
                Invoke(new Action(() =>
                {
                    cmsCEF.Hide();
                    cmsCEF.Close();
                    cmsCEF = null;
                }));
            }
        }
        private void cef_consoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            Output.WriteLine(" [Korot.ConsoleMessage] Message received: [Line: " + e.Line + " Level: " + e.Level + " Source: " + e.Source + " Message:" + e.Message + "]");
        }
        public void updateThemes()
        {
            foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\", "*.*", SearchOption.AllDirectories))
            {
                if (x.EndsWith(".ktf", StringComparison.CurrentCultureIgnoreCase))
                {
                    string Playlist = HTAlt.Tools.ReadFile(x, Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    if (SplittedFase[9].Substring(1).Replace(Environment.NewLine, "") == "1")
                    {
                        frmUpdateExt extUpdate = new frmUpdateExt(x, true, Settings)
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
        }

        public bool certError = false;
        public bool cookieUsage = false;
        public void ChangeStatus(string status)
        {
            lbStatus.Text = status;
        }
        public void CreateNewCollection()
        {
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot", newColInfo, newColName)
            { Icon = Icon, OK = OK, SetToDefault = SetToDefault, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
            DialogResult diagres = mesaj.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (!string.IsNullOrWhiteSpace(mesaj.TextValue))
                {
                    Collection newCol = new Collection
                    {
                        ID = HTAlt.Tools.GenerateRandomText(12),
                        Text = mesaj.TextValue
                    };
                    Settings.CollectionManager.Collections.Add(newCol);
                }
                else { CreateNewCollection(); }
            }
        }
        public void loadingstatechanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!IsDisposed)
            {
                if (e.IsLoading)
                {
                    certError = false;
                    cookieUsage = false;
                    Invoke(new Action(() => pbPrivacy.Image = Properties.Resources.lockg));
                    Invoke(new Action(() => certificatedetails = ""));
                    Invoke(new Action(() => certError = false));
                    if (HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130)
                    {
                        btRefresh.Image = Korot.Properties.Resources.cancel;
                    }
                    else { btRefresh.Image = Korot.Properties.Resources.cancel_w; }
                }
                else
                {
                    if (_Incognito) { }
                    else
                    {
                        Invoke(new Action(() => Settings.History.Add(new Korot.Site() { Date = DateTime.Now.ToString(DateFormat), Name = Text, Url = tbAddress.Text })));

                    }
                    if (HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130)
                    {
                        btRefresh.Image = Korot.Properties.Resources.refresh;
                    }
                    else
                    { btRefresh.Image = Korot.Properties.Resources.refresh_w; }
                }
                if (onCEFTab)
                {
                    if (!e.Browser.IsDisposed)
                    {
                        btBack.Invoke(new Action(() => btBack.Enabled = canGoBack()));
                        btNext.Invoke(new Action(() => btNext.Enabled = canGoForward()));
                    }
                    else
                    {
                        btBack.Invoke(new Action(() => btBack.Enabled = false));
                        btNext.Invoke(new Action(() => btNext.Enabled = false));
                    }
                }
                isLoading = e.IsLoading;
            }
        }

        public bool OnJSAlert(string url, string message)
        {
            if (!NotificationListenerMode)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(JSAlert.Replace("[TITLE]", Text).Replace("[URL]", url), message, new HTAlt.WinForms.HTDialogBoxContext() { OK = true, Cancel = true })
                { StartPosition = FormStartPosition.CenterParent, Yes = Yes, No = No, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
                mesaj.ShowDialog();
                return true;
            }
            else { return false; }
        }


        public bool OnJSConfirm(string url, string message, out bool returnval)
        {
            if (!NotificationListenerMode)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(JSConfirm.Replace("[TITLE]", Text).Replace("[URL]", url), message, new HTAlt.WinForms.HTDialogBoxContext() { OK = true, Cancel = true })
                { StartPosition = FormStartPosition.CenterParent, Yes = Yes, No = No, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
                if (mesaj.ShowDialog() == DialogResult.OK) { returnval = true; } else { returnval = false; }
                return true;
            }
            else { returnval = false; return false; }
        }


        public bool OnJSPrompt(string url, string message, string defaultValue, out bool returnval, out string textresult)
        {
            if (!NotificationListenerMode)
            {
                HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox(url, message, defaultValue)
                { SetToDefault = SetToDefault, StartPosition = FormStartPosition.CenterParent, Icon = Icon, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
                if (mesaj.ShowDialog() == DialogResult.OK) { returnval = true; } else { returnval = false; }
                textresult = mesaj.TextValue;
                return true;
            }
            else { textresult = null; returnval = false; return false; }
        }
        public void NewTab(string url)
        {
            anaform.Invoke(new Action(() => { anaform.CreateTab(ParentTab, url); }));
        }

        private bool isFavMenuHidden = false;

        private void showFavMenu()
        {
            pNavigate.Height += mFavorites.Height;
            tabControl1.Height -= mFavorites.Height;
            tabControl1.Top += mFavorites.Height;
            mFavorites.Visible = true;
            isFavMenuHidden = false;
        }

        private void hideFavMenu()
        {
            pNavigate.Height -= mFavorites.Height;
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
                if (Settings.Favorites.ShowFavorites)
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

        private void EasterEggs()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            if (randomNumber == 6)
            {
                lbKorot.Text = "Another Chromium-based web browser";
            }
            else if (randomNumber == 17)
            {
                lbKorot.Text = "StoneBrowser";
            }
            else if (randomNumber == 45)
            {
                lbKorot.Text = "null";
            }
            else if (randomNumber == 71)
            {
                lbKorot.Text = "web browser made by retarded";
            }
            else if (randomNumber == 3)
            {
                lbKorot.Text = "web browser designed to lag and eat ram";
            }
            else if (randomNumber == 9)
            {
                lbKorot.Text = "korot";
            }
            else if (randomNumber == 35)
            {
                lbKorot.Text = "StoneHomepage";
            }
            else if (randomNumber == 48)
            {
                lbKorot.Text = "ZStone";
            }
            else if (randomNumber == 7)
            {
                Random random2 = new Random();
                int randomNumber2 = random2.Next(1, 2);
                lbKorot.Text = (randomNumber2 == 1 ? "Pell" : "Kolme") + " Browser";
            }
            else if (randomNumber == 33)
            {
                Random random2 = new Random();
                int randomNumber2 = random2.Next(1, 2);
                lbKorot.Text = (randomNumber2 == 1 ? "Webtroy" : "Ninova");
            }
            else
            {
                lbKorot.Text = "Korot";
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

        private void button4_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            string urlLower = tbAddress.Text.ToLower();
            if (ValidHttpURL(urlLower))
            {
                loadPage(urlLower, Text);
            }

            else
            {
                loadPage(Settings.SearchEngine + urlLower, Text);

            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            loadPage(Settings.Homepage);
        }

        private void loadPage(string url, string title = "")
        {
            redirectTo(url, title);
            lbURL_SelectedIndexChanged(null, null);
        }
        public void GoBack()
        {
            lbURL.SelectedIndex = lbURL.SelectedIndex == 0 ? 0 : lbURL.SelectedIndex - 1;
            lbTitle.SelectedIndex = lbURL.SelectedIndex;
        }


        public void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tpCef) //CEF
            {
                GoBack();
                //chromiumWebBrowser1.Back();
            }
            else if (tabControl1.SelectedTab == tpCert) //Certificate Error Menu
            {
                GoBack();
                resetPage();
            }
            else if (tabControl1.SelectedTab == tpSettings
                     || tabControl1.SelectedTab == tpHistory
                     || tabControl1.SelectedTab == tpDownload
                     || tabControl1.SelectedTab == tpAbout
                     || tabControl1.SelectedTab == tpTheme
                     || tabControl1.SelectedTab == tpCollection
                     || tabControl1.SelectedTab == tpSite
                     || tabControl1.SelectedTab == tpNotification
                     || tabControl1.SelectedTab == tpNewTab
                     || tabControl1.SelectedTab == tpBlock) //Menu
            {
                resetPage();
            }
        }
        public void resetPage(bool doNotGoToCEFTab = false)
        {
            anaform.settingTab = anaform.settingTab == ParentTab ? null : anaform.settingTab;
            anaform.themeTab = anaform.themeTab == ParentTab ? null : anaform.themeTab;
            anaform.historyTab = anaform.historyTab == ParentTab ? null : anaform.historyTab;
            anaform.downloadTab = anaform.downloadTab == ParentTab ? null : anaform.downloadTab;
            anaform.siteTab = anaform.siteTab == ParentTab ? null : anaform.siteTab;
            anaform.aboutTab = anaform.aboutTab == ParentTab ? null : anaform.aboutTab;
            anaform.collectionTab = anaform.collectionTab == ParentTab ? null : anaform.collectionTab;
            anaform.notificationTab = anaform.notificationTab == ParentTab ? null : anaform.notificationTab;
            anaform.newtabeditTab = anaform.newtabeditTab == ParentTab ? null : anaform.newtabeditTab;
            anaform.licenseTab = anaform.licenseTab == ParentTab ? null : anaform.licenseTab;
            anaform.blockTab = anaform.blockTab == ParentTab ? null : anaform.blockTab;
            if (!doNotGoToCEFTab)
            {
                allowSwitching = true;
                tabControl1.SelectedTab = tpCef;
            }
        }
        public void button3_Click(object sender, EventArgs e)
        {
            resetPage();
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            lbURL.SelectedIndex = lbURL.SelectedIndex == lbURL.Items.Count - 1 ? lbURL.SelectedIndex : lbURL.SelectedIndex + 1;
            lbTitle.SelectedIndex = lbURL.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetPage();
            if (isLoading)
            {
                chromiumWebBrowser1.Stop();
            }
            else { chromiumWebBrowser1.Reload(); }
        }
        private void cef_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            Invoke(new Action(() => tbAddress.Text = e.Address));
            Invoke(new Action(() =>
            {
                if (isPageFavorited(chromiumWebBrowser1.Address))
                {
                    btFav.ButtonImage = !HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.star_on_w : Properties.Resources.star_on;
                }
                else
                {
                    btFav.ButtonImage = HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
                }
            }));
            if (!ValidHttpURL(e.Address))
            {
                chromiumWebBrowser1.Load(Settings.SearchEngine + e.Address);
            }
            if (lbURL.Items.Count != 0)
            {
                if (e.Address != lbURL.Items[lbURL.Items.Count - 1].ToString())
                {
                    Invoke(new Action(() => redirectTo(e.Address, Text)));
                }
            }
        }
        private void cef_onLoadError(object sender, LoadErrorEventArgs e)
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
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
            }
        }


        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                tpCef.Text = e.Title;
                int si = lbTitle.SelectedIndex;
                if (si != -1)
                {
                    if (lbURL.Items[si].ToString() != chromiumWebBrowser1.Address)
                    {
                        if (chromiumWebBrowser1.Address.ToLower().StartsWith("korot"))
                        {
                            if (chromiumWebBrowser1.Address.ToLower().StartsWith("korot://newtab") ||
chromiumWebBrowser1.Address.ToLower().StartsWith("korot://links") ||
chromiumWebBrowser1.Address.ToLower().StartsWith("korot://license") ||
chromiumWebBrowser1.Address.ToLower().StartsWith("korot://incognito"))
                            {
                                lbTitle.Items.RemoveAt(si);
                                lbTitle.Items.Insert(si, e.Title);
                                lbTitle.SelectedIndex = si;
                            }
                        }
                        else
                        {
                            lbTitle.Items.RemoveAt(si);
                            lbTitle.Items.Insert(si, e.Title);
                            lbTitle.SelectedIndex = si;
                        }
                    }
                }
            }));
        }

        public void showHideSearchMenu()
        {
            if (hammenu.Visible)
            {
                hammenu.Visible = false;
                hammenu.Hide();
            }
            else
            {
                hammenu.Visible = true;
                hammenu.Show();
                hammenu.BringToFront();
            }
        }
        public void retrieveKey(int code)
        {
            KeyEventArgs newE;
            if (code == 0) //BrowserBack
            {
                newE = new KeyEventArgs(Keys.BrowserBack);
                tabform_KeyDown(this, newE);
            }
            else if (code == 1) //BrowserForward
            {
                newE = new KeyEventArgs(Keys.BrowserForward);
                tabform_KeyDown(this, newE);
            }
            else if (code == 2) //BrowserRefresh
            {
                newE = new KeyEventArgs(Keys.BrowserRefresh);
                tabform_KeyDown(this, newE);
            }
            else if (code == 3) //BrowserStop
            {
                newE = new KeyEventArgs(Keys.BrowserStop);
                tabform_KeyDown(this, newE);
            }
            else if (code == 4) //BrowserHome
            {
                newE = new KeyEventArgs(Keys.BrowserHome);
                tabform_KeyDown(this, newE);
            }
            else if (code == 5) //Fullscreen
            {
                newE = new KeyEventArgs((Keys.Control | Keys.F));
                tabform_KeyDown(this, newE);
            }
            else if (code == 6) //Mute
            {
                newE = new KeyEventArgs((Keys.Control | Keys.M));
                tabform_KeyDown(this, newE);
            }
        }

        public bool canGoForward()
        {
            return tabControl1.SelectedTab == tpCef ? (lbURL.SelectedIndex != lbURL.Items.Count - 1) : true;
        }
        public bool canGoBack()
        {
            return tabControl1.SelectedTab == tpCef ? (lbURL.SelectedIndex != 0) : true;
        }

        public bool isControlKeyPressed = false;
        public void tabform_KeyDown(object sender, KeyEventArgs e)
        {

            isControlKeyPressed = e.Control;
            if (e.KeyData == Keys.BrowserBack)
            {
                button1_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.BrowserForward)
            {
                button3_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.BrowserRefresh)
            {
                button2_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.BrowserStop)
            {
                button2_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.BrowserHome)
            {
                button5_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed)
            {
                NewTab("korot://newtab");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed && e.Shift)
            {
                Process.Start(Application.ExecutablePath, "-incognito");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed && e.Alt)
            {
                Process.Start(Application.ExecutablePath);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.F && isControlKeyPressed)
            {
                showHideSearchMenu();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.Enter)
            {
                button4_Click(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.KeyData == Keys.Up || e.KeyData == Keys.PageUp) && isControlKeyPressed)
            {
                Console.WriteLine("[TODO] Ctrl+Up ZoomIn Key Press");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.KeyData == Keys.Down || e.KeyData == Keys.PageDown) && isControlKeyPressed)
            {
                Console.WriteLine("[TODO] Ctrl+Down ZoomOut Key Press");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.PrintScreen && isControlKeyPressed)
            {
                Console.WriteLine("[TODO] Ctrl+PrntScrn ScreenShot Key Press");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.S && isControlKeyPressed)
            {
                Console.WriteLine("[TODO] Ctrl+S SavePage Key Press");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (isControlKeyPressed && e.Shift && e.KeyData == Keys.N)
            {
                Process.Start(Application.ExecutablePath, "-incognito");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (isControlKeyPressed && e.Alt && e.KeyData == Keys.N)
            {
                Process.Start(Application.ExecutablePath);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (isControlKeyPressed && e.KeyData == Keys.N)
            {
                NewTab("korot://newtab");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.F11)
            {
                Console.WriteLine("[TODO] F11 FullScreen Key Press");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.M && isControlKeyPressed)
            {
                Console.WriteLine("[TODO] Ctrl+M Mute Key Press");
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        private Image GetImageFromURL(string URL)
        {
            if (URL == "BACKCOLOR") { return null; }
            else
            {
                int virgulIndex = URL.IndexOf(',') + 1;
                string justB64Code = URL.Substring(virgulIndex);
                Output.WriteLine(string.Concat(justB64Code.Reverse().Skip(3).Reverse()));
                return HTAlt.Tools.Base64ToImage(string.Concat(justB64Code.Reverse().Skip(3).Reverse()));
            }
        }

        private void ChangeThemeForMenuItems(ToolStripMenuItem item)
        {
            item.BackColor = Settings.Theme.BackColor;
            item.ForeColor = !HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Color.White : Color.Black;
            item.DropDown.BackColor = Settings.Theme.BackColor;
            item.DropDown.ForeColor = !HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Color.White : Color.Black;
            foreach (ToolStripMenuItem x in item.DropDownItems)
            {
                ChangeThemeForMenuDDItems(x.DropDown);
            }
        }

        private void ChangeThemeForMenuDDItems(ToolStripDropDown itemDD)
        {
            itemDD.BackColor = Settings.Theme.BackColor;
            itemDD.ForeColor = !HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Color.White : Color.Black;
            foreach (ToolStripMenuItem x in itemDD.Items)
            {
                x.BackColor = Settings.Theme.BackColor;
                x.ForeColor = !HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Color.White : Color.Black;
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
            // It won't throw any exceptions when there's try.
            // This absoluletly does not makes sense.
            // It throws ArgumentException in System.Drawing and shutdown application
            // if there's no try {}
            // What the fuck?
            try
            {
                if (anaform != null)
                {
                    if (anaform.tabRenderer != null)
                    {
                        anaform.tabRenderer.ApplyColors(Settings.Theme.BackColor, HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor), Settings.Theme.OverlayColor, Settings.Theme.BackColor);
                        anaform.Update();
                    }
                }
                if (Settings.Theme.OverlayColor != oldOverlayColor)
                {
                    oldOverlayColor = Settings.Theme.OverlayColor;
                    pbOverlay.BackColor = Settings.Theme.OverlayColor;
                    pbProgress.BackColor = Settings.Theme.OverlayColor;
                    hsDownload.OverlayColor = Settings.Theme.OverlayColor;
                    hsDoNotTrack.OverlayColor = Settings.Theme.OverlayColor;
                    hsFav.OverlayColor = Settings.Theme.OverlayColor;
                    hsOpen.OverlayColor = Settings.Theme.OverlayColor;
                    if (Cef.IsInitialized)
                    {
                        if (chromiumWebBrowser1.IsBrowserInitialized)
                        {
                            if (chromiumWebBrowser1.Address.StartsWith("korot:")) { chromiumWebBrowser1.Reload(); }
                        }
                    }
                }

                if (Settings.Theme.BackColor != oldBackColor)
                {
                    UpdateFavoriteColor();
                    updateFavoritesImages();
                    bool isbright = HTAlt.Tools.IsBright(Settings.Theme.BackColor);
                    Color foreColor = HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor);
                    Color backcolor2 = HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
                    Color backcolor3 = HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 40, false);
                    Color backcolor4 = HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 60, false);
                    Color rbc2 = HTAlt.Tools.ReverseColor(backcolor2, false);
                    Color rbc3 = HTAlt.Tools.ReverseColor(backcolor3, false);
                    Color rbc4 = HTAlt.Tools.ReverseColor(backcolor4, false);
                    lbStatus.ForeColor = foreColor;
                    lbStatus.BackColor = Settings.Theme.BackColor;
                    if (Cef.IsInitialized)
                    {
                        if (chromiumWebBrowser1.IsBrowserInitialized)
                        {
                            if (chromiumWebBrowser1.Address.StartsWith("korot:")) { chromiumWebBrowser1.Reload(); }
                        }
                    }
                    pbBack.BackColor = Settings.Theme.BackColor;
                    cmsFavorite.BackColor = Settings.Theme.BackColor;
                    oldBackColor = Settings.Theme.BackColor;
                    cmsFavorite.ForeColor = foreColor;
                    button12.ButtonImage = !isbright ? Properties.Resources.collection_w : Properties.Resources.collection;
                    pbStore.Image = !isbright ? Properties.Resources.store_w : Properties.Resources.store;
                    btLangStore.ButtonImage = !isbright ? Properties.Resources.store_w : Properties.Resources.store;
                    btlangFolder.ButtonImage = !isbright ? Properties.Resources.extfolder_w : Properties.Resources.extfolder;
                    btClose6.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose2.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose4.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose7.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose5.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose8.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose9.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose3.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    btClose10.ButtonImage = !isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                    lbSettings.BackColor = Color.Transparent;
                    lbSettings.ForeColor = foreColor;

                    pbPrivacy.BackColor = backcolor2;
                    pBlock.BackColor = backcolor2;
                    pBlock.ForeColor = ForeColor;
                    tbAddress.BackColor = backcolor2;
                    pbIncognito.BackColor = backcolor2;
                    fromHour.BackColor = backcolor2;
                    fromHour.ForeColor = foreColor;
                    fromMin.BackColor = backcolor2;
                    fromMin.ForeColor = foreColor;
                    toHour.BackColor = backcolor2;
                    toHour.ForeColor = foreColor;
                    toMin.BackColor = backcolor2;
                    toMin.ForeColor = foreColor;
                    cmsSearchEngine.BackColor = Settings.Theme.BackColor;
                    listBox2.ForeColor = foreColor;
                    comboBox1.ForeColor = foreColor;
                    tbHomepage.ForeColor = foreColor;
                    tbSearchEngine.ForeColor = foreColor;
                    btCertError.ForeColor = foreColor;
                    btNewTab.BackColor = backcolor2;
                    btNewTab.ForeColor = foreColor;
                    hsNotificationSound.BackColor = Settings.Theme.BackColor;
                    hsNotificationSound.ButtonColor = rbc2;
                    hsNotificationSound.ButtonHoverColor = rbc3;
                    hsNotificationSound.ButtonPressedColor = rbc4;
                    hsSilent.BackColor = Settings.Theme.BackColor;
                    hsSilent.ButtonColor = rbc2;
                    hsSilent.ButtonHoverColor = rbc3;
                    hsSilent.ButtonPressedColor = rbc4;
                    hsSchedule.BackColor = Settings.Theme.BackColor;
                    hsSchedule.ButtonColor = rbc2;
                    hsSchedule.ButtonHoverColor = rbc3;
                    hsSchedule.ButtonPressedColor = rbc4;
                    hsAutoRestore.BackColor = Settings.Theme.BackColor;
                    hsAutoRestore.ButtonColor = rbc2;
                    hsAutoRestore.ButtonHoverColor = rbc3;
                    hsAutoRestore.ButtonPressedColor = rbc4;
                    hsDownload.BackColor = Settings.Theme.BackColor;
                    hsDownload.ButtonColor = rbc2;
                    hsDownload.ButtonHoverColor = rbc3;
                    hsDownload.ButtonPressedColor = rbc4;
                    hsDoNotTrack.BackColor = Settings.Theme.BackColor;
                    hsDoNotTrack.ButtonColor = rbc2;
                    hsDoNotTrack.ButtonHoverColor = rbc3;
                    hsDoNotTrack.ButtonPressedColor = rbc4;
                    hsProxy.BackColor = Settings.Theme.BackColor;
                    hsProxy.ButtonColor = rbc2;
                    hsProxy.ButtonHoverColor = rbc3;
                    hsProxy.ButtonPressedColor = rbc4;
                    hsFav.BackColor = Settings.Theme.BackColor;
                    hsFav.ButtonColor = rbc2;
                    hsFav.ButtonHoverColor = rbc3;
                    hsFav.ButtonPressedColor = rbc4;
                    hsOpen.BackColor = Settings.Theme.BackColor;
                    hsOpen.ButtonColor = rbc2;
                    hsOpen.ButtonHoverColor = rbc3;
                    hsOpen.ButtonPressedColor = rbc4;
                    hsFlash.BackColor = Settings.Theme.BackColor;
                    hsFlash.ButtonColor = rbc2;
                    hsFlash.ButtonHoverColor = rbc3;
                    hsFlash.ButtonPressedColor = rbc4;
                    cmsSearchEngine.ForeColor = foreColor;
                    listBox2.BackColor = backcolor2;
                    comboBox1.BackColor = backcolor2;
                    btCookie.BackColor = backcolor2;
                    btInstall.BackColor = backcolor2;
                    btUpdater.BackColor = backcolor2;
                    tbHomepage.BackColor = backcolor2;
                    btCleanLog.BackColor = backcolor2;
                    tbFolder.BackColor = backcolor2;
                    tbStartup.BackColor = backcolor2;
                    cmsStartup.BackColor = Settings.Theme.BackColor;
                    cmsStartup.ForeColor = foreColor;
                    tbFolder.ForeColor = foreColor;
                    tbStartup.ForeColor = foreColor;
                    btReset.BackColor = backcolor2;
                    btDownloadFolder.BackColor = backcolor2;
                    button12.BackColor = backcolor2;
                    tbSearchEngine.BackColor = backcolor2;
                    btNotification.BackColor = backcolor2;
                    btNotification.ForeColor = foreColor;
                    panel1.BackColor = backcolor2;
                    panel1.ForeColor = foreColor;
                    btCertError.BackColor = backcolor2;
                    tbHomepage.BackColor = backcolor2;
                    tbSearchEngine.BackColor = backcolor2;
                    flpLayout.BackColor = Settings.Theme.BackColor;
                    flpLayout.ForeColor = foreColor;
                    flpNewTab.BackColor = Settings.Theme.BackColor;
                    flpNewTab.ForeColor = foreColor;
                    flpClose.BackColor = Settings.Theme.BackColor;
                    flpClose.ForeColor = foreColor;
                    lbStatus.BackColor = Settings.Theme.BackColor;
                    BackColor = Settings.Theme.BackColor;
                    cbLang.BackColor = Settings.Theme.BackColor;
                    pbIncognito.Image = !isbright ? Properties.Resources.inctab_w : Properties.Resources.inctab;
                    tbAddress.ForeColor = foreColor;
                    ForeColor = foreColor;
                    tbTitle.BackColor = backcolor2;
                    tbTitle.ForeColor = foreColor;
                    tbUrl.BackColor = backcolor2;
                    tbUrl.ForeColor = foreColor;
                    lbStatus.ForeColor = foreColor;
                    cbLang.ForeColor = foreColor;
                    textBox4.ForeColor = foreColor;
                    if (isPageFavorited(chromiumWebBrowser1.Address)) { btFav.ButtonImage = !isbright ? Properties.Resources.star_on_w : Properties.Resources.star_on; } else { btFav.ButtonImage = isbright ? Properties.Resources.star : Properties.Resources.star_w; }
                    mFavorites.ForeColor = foreColor;
                    if (!noProfilePic) { btProfile.ButtonImage = profilePic; } else { btProfile.ButtonImage = isbright ? Properties.Resources.profiles : Properties.Resources.profiles_w; }
                    btBack.ButtonImage = isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                    btRefresh.ButtonImage = isbright ? Properties.Resources.refresh : Properties.Resources.refresh_w;
                    btNext.ButtonImage = isbright ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w;
                    btNotifBack.ButtonImage = isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                    btBlockBack.ButtonImage = isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                    btNewTabBack.ButtonImage = isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                    btCookieBack.ButtonImage = isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                    btHome.ButtonImage = isbright ? Properties.Resources.home : Properties.Resources.home_w;
                    btHamburger.ButtonImage = isbright ? (anaform.newDownload ? Properties.Resources.hamburger_i : Properties.Resources.hamburger) : (anaform is null ? Properties.Resources.hamburger_w : (anaform.newDownload ? Properties.Resources.hamburger_i_w : Properties.Resources.hamburger_w));
                    L0.BackColor = backcolor2;
                    L0.ForeColor = foreColor;
                    L1.BackColor = backcolor2;
                    L1.ForeColor = foreColor;
                    L2.BackColor = backcolor2;
                    L2.ForeColor = foreColor;
                    L3.BackColor = backcolor2;
                    L3.ForeColor = foreColor;
                    L4.BackColor = backcolor2;
                    L4.ForeColor = foreColor;
                    L5.BackColor = backcolor2;
                    L5.ForeColor = foreColor;
                    L6.BackColor = backcolor2;
                    L6.ForeColor = foreColor;
                    L7.BackColor = backcolor2;
                    L7.ForeColor = foreColor;
                    L8.BackColor = backcolor2;
                    L8.ForeColor = foreColor;
                    L9.BackColor = backcolor2;
                    L9.ForeColor = foreColor;
                    btClear.BackColor = backcolor2;
                    btClear.ForeColor = foreColor;
                    tbAddress.BackColor = backcolor2;
                    textBox4.BackColor = backcolor2;
                    mFavorites.BackColor = Settings.Theme.BackColor;
                    cmsBStyle.BackColor = Settings.Theme.BackColor;
                    cmsBStyle.ForeColor = foreColor;
                    cmsBack.BackColor = Settings.Theme.BackColor;
                    cmsBack.ForeColor = foreColor;
                    cmsForward.BackColor = Settings.Theme.BackColor;
                    cmsForward.ForeColor = foreColor;
                    foreach (ToolStripItem x in cmsForward.Items) { x.BackColor = Settings.Theme.BackColor; x.ForeColor = foreColor; }
                    foreach (ToolStripItem x in cmsBack.Items) { x.BackColor = Settings.Theme.BackColor; x.ForeColor = foreColor; }
                    foreach (TabPage x in tabControl1.TabPages) { x.BackColor = Settings.Theme.BackColor; x.ForeColor = foreColor; }
                    foreach (Control c in Controls)
                    {
                        c.Refresh();
                    }
                }
                if (Settings.Theme.BackgroundStyle != "BACKCOLOR")
                {
                    if (Settings.Theme.BackgroundStyle != oldStyle)
                    {
                        oldStyle = Settings.Theme.BackgroundStyle;
                        Image backStyle = GetImageFromURL(Settings.Theme.BackgroundStyle);
                        pNavigate.BackgroundImage = backStyle;
                        mFavorites.BackgroundImage = backStyle;
                        foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImage = backStyle; }
                        tpSettings.BackgroundImage = backStyle;
                    }
                }
                else
                {
                    if (Settings.Theme.BackgroundStyle != oldStyle)
                    {
                        oldStyle = Settings.Theme.BackgroundStyle;
                        pNavigate.BackgroundImage = null;
                        mFavorites.BackgroundImage = null;
                        foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImage = null; }
                        tpSettings.BackgroundImage = null;
                    }
                }
                if (Settings.Theme.BackgroundStyleLayout == 0) //NONE
                {
                    pNavigate.BackgroundImageLayout = ImageLayout.None;
                    mFavorites.BackgroundImageLayout = ImageLayout.None;
                    tpSettings.BackgroundImageLayout = ImageLayout.None;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.None; }
                }
                else if (Settings.Theme.BackgroundStyleLayout == 1) //TILE
                {
                    pNavigate.BackgroundImageLayout = ImageLayout.Tile;
                    mFavorites.BackgroundImageLayout = ImageLayout.Tile;
                    tpSettings.BackgroundImageLayout = ImageLayout.Tile;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Tile; }
                }
                else if (Settings.Theme.BackgroundStyleLayout == 2) //CENTER
                {
                    pNavigate.BackgroundImageLayout = ImageLayout.Center;
                    mFavorites.BackgroundImageLayout = ImageLayout.Center;
                    tpSettings.BackgroundImageLayout = ImageLayout.Center;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Center; }
                }
                else if (Settings.Theme.BackgroundStyleLayout == 3) //STRETCH
                {
                    pNavigate.BackgroundImageLayout = ImageLayout.Stretch;
                    mFavorites.BackgroundImageLayout = ImageLayout.Stretch;
                    tpSettings.BackgroundImageLayout = ImageLayout.Stretch;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Stretch; }
                }
                else if (Settings.Theme.BackgroundStyleLayout == 4) //ZOOM
                {
                    pNavigate.BackgroundImageLayout = ImageLayout.Zoom;
                    mFavorites.BackgroundImageLayout = ImageLayout.Zoom;
                    tpSettings.BackgroundImageLayout = ImageLayout.Zoom;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Zoom; }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
        private void execTimer50Events()
        {
            hsDoNotTrack.Checked = Settings.DoNotTrack;
            hsFlash.Checked = Settings.Flash;
            tbHomepage.Text = Settings.Homepage;
            rbNewTab.Checked = Settings.Homepage == "korot://newtab";
            tbSearchEngine.Text = Settings.SearchEngine;
            hsProxy.Checked = Settings.RememberLastProxy;
            hsNotificationSound.Checked = !Settings.QuietMode;
            hsSilent.Checked = Settings.Silent;
            hsSchedule.Checked = Settings.AutoSilent;
            panel1.Enabled = Settings.AutoSilent;
            RefreshScheduledSiletMode();
            refreshThemeList();
            RefreshFavorites();
            LoadNewTabSites();
            btHamburger.ButtonImage = HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130 ? (anaform is null ? Properties.Resources.hamburger : (anaform.newDownload ? Properties.Resources.hamburger_i : Properties.Resources.hamburger)) : (anaform is null ? Properties.Resources.hamburger_w : (anaform.newDownload ? Properties.Resources.hamburger_i_w : Properties.Resources.hamburger_w));
            comboBox1.Text = !onThemeName ? (Settings.Theme.LoadedDefaults ? "((default))" : Settings.Theme.Name) : comboBox1.Text;

        }
        private int timer1int = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chromiumWebBrowser1.IsDisposed)
            {
                Close();
            }

            if (timer1int != 50)
            {
                timer1int++;
            }
            else
            {
                timer1int = 0;
                execTimer50Events();
            }

            onCEFTab = (tabControl1.SelectedTab == tpCef);
            ChangeTheme();
            if (Parent != null)
            {
                Parent.Text = Text;
            }
            RefreshTranslation();

            Text = tabControl1.SelectedTab.Text;
            if (NotificationListenerMode)
            {
                isMuted = true;
                chromiumWebBrowser1.GetBrowserHost().SetAudioMuted(true);
            }
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(((ToolStripMenuItem)sender).Tag.ToString());
        }
        private bool isPageFavorited(string url)
        {
            return Settings.Favorites.FavoritesWithNoFolders.Find(i => i.Url == url) != null;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (isPageFavorited(chromiumWebBrowser1.Address))
            {
                Settings.Favorites.DeleteFolder(Settings.Favorites.FavoritesWithNoFolders.Find(i => i.Url == chromiumWebBrowser1.Address));
                btFav.ButtonImage = HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
            }
            else
            {
                frmNewFav newFav = new frmNewFav(Text, chromiumWebBrowser1.Address, this);
                newFav.ShowDialog();
            }
            RefreshFavorites();
        }


        public void RefreshSizes()
        {
            flpFrom.Location = new Point(scheduleFrom.Location.X + scheduleFrom.Width + 10, flpFrom.Location.Y);
            scheduleTo.Location = new Point(flpFrom.Location.X + flpFrom.Width + 10, scheduleTo.Location.Y);
            flpTo.Location = new Point(scheduleTo.Location.X + scheduleTo.Width + 10, flpTo.Location.Y);
            flpEvery.Location = new Point(scheduleEvery.Location.X + scheduleEvery.Width + 10, flpEvery.Location.Y);
            lbVersion.Location = new Point(lbKorot.Location.X + lbKorot.Width, lbVersion.Location.Y);
            flpClose.Location = new Point(lbCloseColor.Location.X + lbCloseColor.Width, flpClose.Location.Y);
            flpClose.Width = tpTheme.Width - (lbCloseColor.Width + lbCloseColor.Location.X + 25);
            flpNewTab.Location = new Point(lbNewTabColor.Location.X + lbNewTabColor.Width, flpNewTab.Location.Y);
            flpNewTab.Width = tpTheme.Width - (lbNewTabColor.Width + lbNewTabColor.Location.X + 25);
            hsAutoRestore.Location = new Point(lbautoRestore.Location.X + lbautoRestore.Width + 5, hsAutoRestore.Location.Y);
            hsFav.Location = new Point(lbShowFavorites.Location.X + lbShowFavorites.Width + 5, hsFav.Location.Y);
            hsDoNotTrack.Location = new Point(lbDNT.Location.X + lbDNT.Width + 5, hsDoNotTrack.Location.Y);
            hsFlash.Location = new Point(lbFlash.Location.X + lbFlash.Width + 5, hsFlash.Location.Y);
            hsOpen.Location = new Point(lbOpen.Location.X + lbOpen.Width + 5, hsOpen.Location.Y);
            hsDownload.Location = new Point(lbAutoDownload.Location.X + lbAutoDownload.Width + 5, hsDownload.Location.Y);
            hsProxy.Location = new Point(lbLastProxy.Location.X + lbLastProxy.Width + 5, hsProxy.Location.Y);
            llLicenses.LinkArea = new LinkArea(0, llLicenses.Text.Length);
            llLicenses.Location = new Point(label21.Location.X, label21.Location.Y + label21.Size.Height);
            textBox4.Location = new Point(lbBackImage.Location.X + lbBackImage.Width, textBox4.Location.Y);
            textBox4.Width = tpTheme.Width - (lbBackImage.Width + lbBackImage.Location.X + 25);
            tbStartup.Location = new Point(lbAtStartup.Location.X + lbAtStartup.Width, tbStartup.Location.Y);
            tbStartup.Width = tpSettings.Width - (lbAtStartup.Width + lbAtStartup.Location.X + 15);
            tbTitle.Location = new Point(lbNTTitle.Location.X + lbNTTitle.Width, tbTitle.Location.Y);
            tbTitle.Width = tpNewTab.Width - (lbNTTitle.Width + lbNTTitle.Location.X + 15);
            tbUrl.Location = new Point(lbNTUrl.Location.X + lbNTUrl.Width, tbUrl.Location.Y);
            tbUrl.Width = tpNewTab.Width - (lbNTUrl.Width + lbNTUrl.Location.X + 15);
            flpLayout.Location = new Point(lbBackImageStyle.Location.X + lbBackImageStyle.Width, flpLayout.Location.Y);
            flpLayout.Width = tpTheme.Width - (lbBackImageStyle.Width + lbBackImageStyle.Location.X + 25);
            pbBack.Location = new Point(lbBackColor.Location.X + lbBackColor.Width, pbBack.Location.Y);
            pbOverlay.Location = new Point(lbOveralColor.Location.X + lbOveralColor.Width, pbOverlay.Location.Y);
            tbFolder.Location = new Point(lbDownloadFolder.Location.X + lbDownloadFolder.Width, tbFolder.Location.Y);
            tbFolder.Width = tpDownload.Width - (lbDownloadFolder.Location.X + lbDownloadFolder.Width + btDownloadFolder.Width + 25);
            btDownloadFolder.Location = new Point(tbFolder.Location.X + tbFolder.Width, btDownloadFolder.Location.Y);
            comboBox1.Location = new Point(lbThemeName.Location.X + lbThemeName.Width, comboBox1.Location.Y);
            comboBox1.Width = tpTheme.Width - (lbThemeName.Location.X + lbThemeName.Width + button12.Width + 25);
            button12.Location = new Point(comboBox1.Location.X + comboBox1.Width, button12.Location.Y);
            tbHomepage.Location = new Point(lbHomepage.Location.X + lbHomepage.Width + 5, tbHomepage.Location.Y);
            tbHomepage.Width = tpSettings.Width - (lbHomepage.Location.X + lbHomepage.Width + rbNewTab.Width + 30);
            rbNewTab.Location = new Point(tbHomepage.Location.X + tbHomepage.Width + 5, rbNewTab.Location.Y);
            tbSearchEngine.Location = new Point(lbSearchEngine.Location.X + lbSearchEngine.Width + 5, tbSearchEngine.Location.Y);
            tbSearchEngine.Width = tpSettings.Width - (lbSearchEngine.Location.X + lbSearchEngine.Width + 25);
        }
        public void RefreshTranslation()
        {
            label21.Text = Settings.LanguageSystem.GetItemText("KorotAbout").Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + ((!(string.IsNullOrWhiteSpace(Settings.Theme.Author) && string.IsNullOrWhiteSpace(Settings.Theme.Name))) ? Settings.LanguageSystem.GetItemText("AboutInfoTheme").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Settings.Theme.Author) ? anon : Settings.Theme.Author).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Settings.Theme.Name) ? noname : Settings.Theme.Name) : "");
            hsOpen.Checked = Settings.Downloads.OpenDownload;
            hsAutoRestore.Checked = Settings.AutoRestore;
            hsFav.Checked = Settings.Favorites.ShowFavorites;
            switch ((int)Settings.Theme.CloseButtonColor)
            {
                case 0:
                    rbBackColor1.Checked = true;
                    break;
                case 1:
                    rbForeColor1.Checked = true;
                    break;
                case 2:
                    rbOverlayColor1.Checked = true;
                    break;
            }
            switch ((int)Settings.Theme.NewTabColor)
            {
                case 0:
                    rbBackColor.Checked = true;
                    break;
                case 1:
                    rbForeColor.Checked = true;
                    break;
                case 2:
                    rbOverlayColor.Checked = true;
                    break;
            }
            switch (Settings.Theme.BackgroundStyleLayout)
            {
                case 0:
                    rbNone.Checked = true;
                    break;
                case 1:
                    rbTile.Checked = true;
                    break;
                case 2:
                    rbCenter.Checked = true;
                    break;
                case 3:
                    rbStretch.Checked = true;
                    break;
                case 4:
                    rbZoom.Checked = true;
                    break;
            }
            colorToolStripMenuItem.Checked = Settings.Theme.BackgroundStyle == "BACKCOLOR" ? true : false;
            textBox4.Text = Settings.Theme.BackgroundStyle == "BACKCOLOR" ? usingBC : Settings.Theme.BackgroundStyle;
            lbCertErrorTitle.Text = CertErrorPageTitle;
            lbCertErrorInfo.Text = CertErrorPageMessage;
            btCertError.Text = CertErrorPageButton;
            if (Settings.Startup.ToLower() == "korot://newtab")
            {
                tbStartup.Text = showNewTabPageToolStripMenuItem.Text;
            }
            else if (Settings.Startup.ToLower() == "korot://homepage" || Settings.Startup.ToLower() == Settings.Homepage.ToLower())
            {
                tbStartup.Text = showHomepageToolStripMenuItem.Text;
            }
            else
            {
                tbStartup.Text = Settings.Startup;
            }
            hsDownload.Checked = Settings.Downloads.UseDownloadFolder;
            lbDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Enabled = hsDownload.Checked;
            btDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Text = Settings.Downloads.DownloadDirectory;
        }
        public void Fullscreenmode(bool fullscreen)
        {
            if (fullscreen != anaform.isFullScreen)
            {
                if (fullscreen)
                {
                    tabControl1.Location = new Point(tabControl1.Location.X, tabControl1.Location.Y - pNavigate.Height);
                    tabControl1.Height += pNavigate.Height;
                }
                else
                {
                    tabControl1.Location = new Point(tabControl1.Location.X, tabControl1.Location.Y + pNavigate.Height);
                    tabControl1.Height -= pNavigate.Height;
                }
                pNavigate.Visible = !fullscreen;
                anaform.Invoke(new Action(() => anaform.Fullscreenmode(fullscreen)));
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (profmenu.Visible)
            {
                profmenu.Hide();
            }
            else
            {
                profmenu.Location = new Point(btProfile.Location.X - profmenu.Width, btProfile.Location.Y + btProfile.Height);
                profmenu.Show();
                profmenu.BringToFront();
            }
        }
        public void applyExtension(Extension ext)
        {
            if (ext.Settings.activateScript)
            {
                chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(HTAlt.Tools.ReadFile(ext.Startup, Encoding.UTF8), "korot://extension/" + ext.CodeName);
            }
            if (defaultProxy != null && ext.Settings.hasProxy)
            {
                SetProxy(ext.Proxy);
            }
            if (ext.Settings.showPopupMenu)
            {
                frmExt formext = new frmExt(this, userName, ext)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Size = ext.Size
                };
                Controls.Add(formext);
                formext.Visible = true;
                formext.BringToFront();
                formext.Show();
            }
        }
        public void showDevTools()
        {
            if (chromiumWebBrowser1.IsHandleCreated)
            {
                chromiumWebBrowser1.GetBrowser().ShowDevTools();
            }
        }


        public string certificatedetails = "";
        public bool isCertError = false;
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
            if (privmenu.Visible)
            {
                privmenu.Hide();
            }
            else
            {
                privmenu.Location = new Point(pbPrivacy.Location.X, pbPrivacy.Location.Y + pbPrivacy.Height);
                privmenu.Show();
                privmenu.BringToFront();
            }
        }
        public List<string> CertAllowedUrls = new List<string>();
        private void button10_Click(object sender, EventArgs e)
        {
            CertAllowedUrls.Add(btCertError.Tag.ToString());
            chromiumWebBrowser1.Refresh();
            pnlCert.Visible = false;
        }

        public void SwitchToSettings()
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpSettings;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (hammenu.Visible)
            {
                hammenu.Hide();
            }
            else
            {
                hammenu.Location = new Point(btHamburger.Location.X - hammenu.Width, btHamburger.Location.Y + btHamburger.Height);
                hammenu.Show();
                hammenu.BringToFront();
            }
        }

        private void restoreLastSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private bool allowSwitching = false;
        private bool onCEFTab = true;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitching == false) { e.Cancel = true; } else { chromiumWebBrowser1.StopFinding(true); e.Cancel = false; allowSwitching = false; }
            onCEFTab = (tabControl1.SelectedTab == tpCef);
            if (tabControl1.SelectedTab == tpCef)
            {
                cef_GotFocus(sender, e);
                resetPage();
            }
            else if (tabControl1.SelectedTab == tpCert)
            {
                cef_GotFocus(sender, e);
                cef_LostFocus(sender, e);
                resetPage();
            }
            else
            {
                cef_LostFocus(sender, e);
            }
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
            Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\Extensions\\\"");
            e.Cancel = true;
        }

        private void hsDoNotTrack_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DoNotTrack = hsDoNotTrack.Checked;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (incognitomenu.Visible)
            {
                incognitomenu.Hide();
            }
            else
            {
                incognitomenu.Location = new Point(pbIncognito.Location.X - incognitomenu.Width, pbIncognito.Location.Y + pbIncognito.Height);
                incognitomenu.Show();
                incognitomenu.BringToFront();
            }
        }
        private void RecursiveNewTab(Folder folder,ToolStripMenuItem item)
        {
            if (folder != null)
            {
                if (folder.Favorites != null)
                {
                    foreach (ToolStripMenuItem subitem in item.DropDown.Items)
                    {
                        if (item.Tag is Favorite) { NewTab((subitem.Tag as Favorite).Url); }
                        else if (item.Tag is Folder) { RecursiveNewTab(subitem.Tag as Folder,subitem); }
                    }
                }
            }
        }
        private void openInNewTab_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag != null)
                {
                    if (selectedFavorite.Tag is Favorite)
                    {
                        NewTab((selectedFavorite.Tag as Favorite).Url);
                    }
                    else
                    {
                        foreach (ToolStripMenuItem item in selectedFavorite.DropDown.Items)
                        {
                            if (item.Tag is Favorite) { NewTab((item.Tag as Favorite).Url); }
                            else if (item.Tag is Folder) { RecursiveNewTab(item.Tag as Folder,item); }
                        }
                    }
                }
            }
        }
        private void removeSelectedTSMI_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag is Favorite)
                {
                    if ((selectedFavorite.Tag as Favorite).Url == chromiumWebBrowser1.Address)
                    {
                        btFav.ButtonImage = HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
                    }
                }

                Settings.Favorites.DeleteFolder(selectedFavorite.Tag as Folder);
                RefreshFavorites();
            }
        }

        private void clearTSMI_Click(object sender, EventArgs e)
        {
            Settings.Favorites.Favorites.Clear();
            btFav.ButtonImage = HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
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
                HTAlt.Tools.WriteFile(save.FileName, TakeScrenshot.ImageToByte2(TakeScrenshot.Snapshot(chromiumWebBrowser1)));
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
                HTAlt.Tools.WriteFile(save.FileName, htmlText.Result, Encoding.UTF8);
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
                    KeyEventArgs newE = new KeyEventArgs((Keys.Control | Keys.Up));
                    tabform_KeyDown(this, newE);
                }
                else if (e.Delta < 0)
                {
                    KeyEventArgs newE = new KeyEventArgs((Keys.Control | Keys.Down));
                    tabform_KeyDown(this, newE);
                }
            }
        }

        public void SwitchToHistory()
        {
            if (anaform.historyTab != null)
            {
                anaform.SelectedTab = anaform.historyTab;
            }
            else
            {
                anaform.historyTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpHistory;
            }
        }

        public void SwitchToDownloads()
        {
            if (anaform.downloadTab != null)
            {
                anaform.SelectedTab = anaform.downloadTab;
                anaform.newDownload = false;
            }
            else
            {
                anaform.downloadTab = ParentTab;
                anaform.newDownload = false;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpDownload;
            }
        }
        public void SwitchToAbout()
        {
            if (anaform.aboutTab != null)
            {
                anaform.SelectedTab = anaform.aboutTab;
            }
            else
            {
                anaform.aboutTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpAbout;
            }
        }

        private void hsProxy_CheckedChanged(object sender, EventArgs e)
        {
            Settings.RememberLastProxy = hsProxy.Checked;
        }

        public void SwitchToThemes()
        {
            if (anaform.themeTab != null)
            {
                anaform.SelectedTab = anaform.themeTab;
            }
            else
            {
                anaform.themeTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpTheme;
            }
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

        private void RefreshBlock()
        {
            blockmenu.GenerateUI();
        }
        public void OpenSiteSettings()
        {
            Invoke(new Action(() =>
                {
                    if (anaform.siteTab != null)
                    {
                        anaform.SelectedTab = anaform.siteTab;
                    }
                    else
                    {
                        resetPage(true);
                        anaform.siteTab = ParentTab;
                        btNext.Enabled = true;
                        allowSwitching = true;
                        tabControl1.SelectedTab = tpSite;
                        siteman.GenerateUI();
                    }
                }));
        }
        public void OpenBlockSettings()
        {
            Invoke(new Action(() =>
            {
                if (anaform.blockTab != null)
                {
                    anaform.SelectedTab = anaform.blockTab;
                }
                else
                {
                    resetPage(true);
                    anaform.blockTab = ParentTab;
                    btNext.Enabled = true;
                    allowSwitching = true;
                    tabControl1.SelectedTab = tpBlock;
                    RefreshBlock();
                }
            }));
        }

        private void button15_Click(object sender, EventArgs e)
        {
            OpenSiteSettings();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = selectAFolder };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = dialog.SelectedPath;
                Settings.Downloads.DownloadDirectory = tbFolder.Text;
            }
        }

        private void tbFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Downloads.DownloadDirectory = tbFolder.Text;
        }

        private void hsDownload_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Downloads.UseDownloadFolder = hsDownload.Checked;
            lbDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Enabled = hsDownload.Checked;
            btDownloadFolder.Enabled = hsDownload.Checked;
        }

        private void showNewTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbStartup.Text = showNewTabPageToolStripMenuItem.Text;
            Settings.Startup = "korot://newtab";
        }

        private void showHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbStartup.Text = showHomepageToolStripMenuItem.Text;
            Settings.Startup = Settings.Homepage;
        }

        private void showAWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox inputb = new HTAlt.WinForms.HTInputBox("Korot", enterAValidUrl, Settings.SearchEngine) { Icon = Icon, SetToDefault = SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor };
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(inputb.TextValue) || (inputb.TextValue.ToLower() == "korot://newtab") || inputb.TextValue.ToLower() == Settings.Homepage.ToLower() || inputb.TextValue.ToLower() == "korot://homepage")
                {
                    showAWebsiteToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    Settings.Startup = inputb.TextValue;
                    tbStartup.Text = inputb.TextValue;
                }
            }
        }

        private void tbStartup_Click(object sender, EventArgs e)
        {
            cmsStartup.Show(MousePosition);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot", resetConfirm,
                                                                                      new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
            { StartPosition = FormStartPosition.CenterParent, Yes = Yes, No = No, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
            if (mesaj.ShowDialog() == DialogResult.Yes)
            {
                Process.Start(Application.ExecutablePath, "-oobe");
                Application.Exit();

            }
        }

        private void hsFav_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Favorites.ShowFavorites = hsFav.Checked;
            RefreshFavorites();
        }

        private void btCleanLog_Click(object sender, EventArgs e)
        {
            string x = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Logs\\";
            Program.RemoveDirectory(x);
        }

        private void hsOpen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Downloads.OpenDownload = hsOpen.Checked;
        }

        private void tsWebStore_Click(object sender, EventArgs e)
        {

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
        private void RecursiveNewWindow(Folder folder,ToolStripMenuItem item,bool isIncognito)
        {
            if (folder != null)
            {
                if (folder.Favorites != null)
                {
                    foreach (ToolStripMenuItem subitem in item.DropDown.Items)
                    {
                        if (subitem.Tag is Favorite) { Process.Start(Application.ExecutablePath, (isIncognito ? "-incognito " : "") + (subitem.Tag as Favorite).Url); }
                        else if (subitem.Tag is Folder) { RecursiveNewWindow((item.Tag as Folder), item, isIncognito); }
                    }
                }
            }
        }
        private void FavoriteInNewWindow(bool Incognito)
        {
            if (selectedFavorite != null)
            {
                if (selectedFavorite.Tag != null)
                {
                    if (selectedFavorite.Tag is Favorite)
                    {
                        Process.Start(Application.ExecutablePath, (selectedFavorite.Tag as Favorite).Url);
                    }
                    else
                    {
                        foreach (ToolStripMenuItem item in selectedFavorite.DropDown.Items)
                        {
                            if (item.Tag is Favorite) { Process.Start(Application.ExecutablePath, (Incognito ? "-incognito " : "") + (item.Tag as Favorite).Url); }
                            else if (item.Tag is Folder) { RecursiveNewWindow((item.Tag as Folder), item, Incognito); }
                        }
                    }
                }
            }
        }

        private void openİnNewWindowToolStripMenuItem_Click(object sender, EventArgs e) => FavoriteInNewWindow(false);

        private void openİnNewIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e) => FavoriteInNewWindow(true);

        private bool isLeftPressed, isRightPressed = false;

        private void frmCEF_FormClosing(object sender, FormClosingEventArgs e)
        {
            closing = true;
        }
        public void redirectTo(string url, string title)
        {
            if (bypassThisDeletion)
            {
                bypassThisDeletion = false;
            }
            else
            {
                if (lbURL.SelectedIndex != -1)
                {
                    if (lbURL.Items[lbURL.SelectedIndex].ToString() != url)
                    {
                        bypassThisIndexChange = true;
                        int selectedItem = lbURL.SelectedIndex;
                        if (lbURL.Items.Count - 1 > 0)
                        {
                            while (lbURL.Items.Count - 1 != selectedItem)
                            {
                                lbURL.Items.RemoveAt(selectedItem + 1);
                            }
                            while (lbTitle.Items.Count - 1 != selectedItem)
                            {
                                lbTitle.Items.RemoveAt(selectedItem + 1);
                            }
                        }
                        lbURL.Items.Add(url);
                        lbTitle.Items.Add(title);
                        lbURL.SelectedIndex = lbURL.Items.Count - 1;
                        lbTitle.SelectedIndex = lbURL.SelectedIndex;
                        return;
                    }
                }
                else
                {
                    lbURL.Items.Add(url);
                    lbTitle.Items.Add(title);
                    lbURL.SelectedIndex = lbURL.Items.Count - 1;
                    lbTitle.SelectedIndex = lbURL.SelectedIndex;
                }
            }
        }

        private bool bypassThisIndexChange = false;
        private bool bypassThisDeletion = false;
        public bool indexChanged = false;
        private void lbURL_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTitle.SelectedIndex = lbURL.SelectedIndex;
            if (bypassThisIndexChange) { bypassThisIndexChange = false; }
            else
            {
                bypassThisDeletion = true;
                indexChanged = true;
                chromiumWebBrowser1.Load(lbURL.SelectedItem.ToString());
            }
            btBack.Visible = lbURL.SelectedIndex != 0;
            btNext.Visible = lbURL.SelectedIndex != lbURL.Items.Count - 1;
        }

        private void cmsBack_Opening(object sender, CancelEventArgs e)
        {
            cmsBack.Items.Clear();
            if ((lbURL.SelectedIndex != -1) && lbURL.Items.Count > 0)
            {
                int i = 0;
                while (i != lbURL.SelectedIndex)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem
                    {
                        Text = lbTitle.Items[i].ToString(),
                        ShortcutKeyDisplayString = i.ToString(),
                        Tag = lbURL.Items[i].ToString(),
                        ShowShortcutKeys = false
                    };
                    item.Click += backfrowardItemClick;
                    cmsBack.Items.Add(item);
                    i += 1;
                }
            }
        }

        private void backfrowardItemClick(object sender, EventArgs e)
        {
            int switchTo = Convert.ToInt32(((ToolStripMenuItem)sender).ShortcutKeyDisplayString);
            lbURL.SelectedIndex = switchTo;
        }

        private void cmsForward_Opening(object sender, CancelEventArgs e)
        {
            cmsForward.Items.Clear();
            if ((lbURL.SelectedIndex != -1) && lbURL.Items.Count > 0)
            {
                int i = lbURL.SelectedIndex + 1;
                while (i != lbURL.Items.Count - 1)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem
                    {
                        Text = lbTitle.Items[i].ToString(),
                        ShortcutKeyDisplayString = i.ToString(),
                        Tag = lbURL.Items[i].ToString(),
                        ShowShortcutKeys = false
                    };
                    item.Click += backfrowardItemClick;
                    cmsForward.Items.Add(item);
                    i += 1;
                }
            }
        }

        private void hsAutoRestore_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoRestore = hsAutoRestore.Checked;
        }

        private void tpCollection_Enter(object sender, EventArgs e)
        {
            ColMan.genColList();
        }


        public void SwitchToCollections()
        {
            if (anaform.collectionTab != null)
            {
                anaform.SelectedTab = anaform.collectionTab;
            }
            else
            {
                anaform.collectionTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpCollection;
            }
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNone.Checked)
            {
                rbTile.Checked = false;
                rbCenter.Checked = false;
                rbStretch.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 0;
                ChangeTheme();

            }
        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTile.Checked)
            {
                rbNone.Checked = false;
                rbCenter.Checked = false;
                rbStretch.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 1;
                ChangeTheme();

            }
        }

        private void rbCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCenter.Checked)
            {
                rbTile.Checked = false;
                rbNone.Checked = false;
                rbStretch.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 2;
                ChangeTheme();

            }
        }

        private void rbStretch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStretch.Checked)
            {
                rbTile.Checked = false;
                rbCenter.Checked = false;
                rbNone.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 3;
                ChangeTheme();

            }
        }

        private void rbZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZoom.Checked)
            {
                rbTile.Checked = false;
                rbCenter.Checked = false;
                rbStretch.Checked = false;
                rbNone.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 4;
                ChangeTheme();

            }
        }

        private void rbBackColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBackColor.Checked)
            {
                rbForeColor.Checked = false;
                rbOverlayColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.BackColor;
                ChangeTheme();

            }
        }

        private void rbForeColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForeColor.Checked)
            {
                rbBackColor.Checked = false;
                rbOverlayColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.ForeColor;
                ChangeTheme();

            }
        }

        private void rbOverlayColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOverlayColor.Checked)
            {
                rbForeColor.Checked = false;
                rbBackColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.OverlayColor;
                ChangeTheme();

            }
        }

        private void rbBackColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBackColor1.Checked)
            {
                rbForeColor1.Checked = false;
                rbOverlayColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.BackColor;
                ChangeTheme();

            }
        }

        private void rbForeColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForeColor1.Checked)
            {
                rbBackColor1.Checked = false;
                rbOverlayColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.ForeColor;
                ChangeTheme();

            }
        }

        private void rbOverlayColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOverlayColor1.Checked)
            {
                rbForeColor1.Checked = false;
                rbBackColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.OverlayColor;
                ChangeTheme();

            }
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            lbStatus.Visible = !string.IsNullOrWhiteSpace(lbStatus.Text);
        }

        private void hsNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.QuietMode = !hsNotificationSound.Checked;
        }

        private void lbHaftaGunu_Click(object sender, EventArgs e)
        {
            Label myLabel = sender as Label;
            if (myLabel.Tag.ToString() == "1")
            {
                myLabel.Tag = "0";
                myLabel.BackColor = panel1.BackColor;
            }
            else if (myLabel.Tag.ToString() == "0")
            {
                myLabel.Tag = "1";
                myLabel.BackColor = Settings.Theme.OverlayColor;
            }
            writeSchedules();
            RefreshScheduledSiletMode();
        }

        private void fromHour_ValueChanged(object sender, EventArgs e)
        {
            writeSchedules();
        }

        private void hsSchedule_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoSilent = hsSchedule.Checked;
            panel1.Enabled = hsSchedule.Checked;
        }

        private void hsSilent_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DoNotPlaySound = hsSilent.Checked;
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            if (anaform.notificationTab != null)
            {
                anaform.SelectedTab = anaform.notificationTab;
            }
            else
            {
                resetPage(true);
                anaform.notificationTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpNotification;
            }
        }

        private void btNotifBack_Click(object sender, EventArgs e)
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                resetPage(true);
                anaform.settingTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpSettings;
            }
        }

        private void btBlockBack_Click(object sender, EventArgs e)
        {
            OpenSiteSettings();
        }

        private void tbAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public bool isMuted = false;

        private void tmrNotifListener_Tick(object sender, EventArgs e)
        {
            if (NotificationListenerMode)
            {
                chromiumWebBrowser1.Refresh();
                isMuted = true;
                chromiumWebBrowser1.GetBrowserHost().SetAudioMuted(true);
            }
            bool c = Settings.IsQuietTime;
            if (Settings.QuietMode)
            {
                isMuted = true;
                chromiumWebBrowser1.GetBrowserHost().SetAudioMuted(true);
            }
        }

        private bool onThemeName = false;
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            onThemeName = true;
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            onThemeName = false;
        }

        private void ımportProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog() { Title = importProfileInfo, Filter = ProfileFileInfo + "|*.kpa", };
            DialogResult dialog = fileDialog.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                ZipFile.ExtractToDirectory(fileDialog.FileName, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\", Encoding.UTF8);
            }
        }

        private void exportThisProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog() { Title = exportProfileInfo, Filter = ProfileFileInfo + "|*.kpa", };
            DialogResult dialog = fileDialog.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                ZipFile.CreateFromDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\", fileDialog.FileName, CompressionLevel.Optimal, true, Encoding.UTF8);
            }
        }

        private void hsFlash_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Flash = hsFlash.Checked;
        }

        private void tpHistory_Enter(object sender, EventArgs e)
        {
            hisman.RefreshList();
        }

        private void tsLanguages_DropDownOpening(object sender, EventArgs e)
        {
            RefreshLangList();
        }

        private void htButton1_Click(object sender, EventArgs e)
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                resetPage(true);
                anaform.settingTab = ParentTab;
                btNext.Enabled = true;
                allowSwitching = true;
                tabControl1.SelectedTab = tpSettings;
            }
        }

        private void tsExtFolder_Click(object sender, EventArgs e)
        {
        }

        private void tsLangFolder_Click(object sender, EventArgs e)
        {

        }

        private void tsLangStore_Click(object sender, EventArgs e)
        {

        }

        private void btNewTab_Click(object sender, EventArgs e)
        {
            EditNewTabItem();
        }

        private void LoadNewTabSites()
        {
            NTRefreshNotDone = true;
            //l0
            if (Settings.NewTabSites.FavoritedSite0 != null)
            {
                L0T.Text = Settings.NewTabSites.FavoritedSite0.Name;
                L0U.Text = Settings.NewTabSites.FavoritedSite0.Url;
            }
            else
            {
                L0T.Text = "...";
                L0U.Text = "...";
            }
            //l1
            if (Settings.NewTabSites.FavoritedSite1 != null)
            {
                L1T.Text = Settings.NewTabSites.FavoritedSite1.Name;
                L1U.Text = Settings.NewTabSites.FavoritedSite1.Url;
            }
            else
            {
                L1T.Text = "...";
                L1U.Text = "...";
            }
            //l2
            if (Settings.NewTabSites.FavoritedSite2 != null)
            {
                L2T.Text = Settings.NewTabSites.FavoritedSite2.Name;
                L2U.Text = Settings.NewTabSites.FavoritedSite2.Url;
            }
            else
            {
                L2T.Text = "...";
                L2U.Text = "...";
            }
            //l3
            if (Settings.NewTabSites.FavoritedSite3 != null)
            {
                L3T.Text = Settings.NewTabSites.FavoritedSite3.Name;
                L3U.Text = Settings.NewTabSites.FavoritedSite3.Url;
            }
            else
            {
                L3T.Text = "...";
                L3U.Text = "...";
            }
            //l4
            if (Settings.NewTabSites.FavoritedSite4 != null)
            {
                L4T.Text = Settings.NewTabSites.FavoritedSite4.Name;
                L4U.Text = Settings.NewTabSites.FavoritedSite4.Url;
            }
            else
            {
                L4T.Text = "...";
                L4U.Text = "...";
            }
            //l5
            if (Settings.NewTabSites.FavoritedSite5 != null)
            {
                L5T.Text = Settings.NewTabSites.FavoritedSite5.Name;
                L5U.Text = Settings.NewTabSites.FavoritedSite5.Url;
            }
            else
            {
                L5T.Text = "...";
                L5U.Text = "...";
            }
            //l6
            if (Settings.NewTabSites.FavoritedSite6 != null)
            {
                L6T.Text = Settings.NewTabSites.FavoritedSite6.Name;
                L6U.Text = Settings.NewTabSites.FavoritedSite6.Url;
            }
            else
            {
                L6T.Text = "...";
                L6U.Text = "...";
            }
            //l7
            if (Settings.NewTabSites.FavoritedSite7 != null)
            {
                L7T.Text = Settings.NewTabSites.FavoritedSite7.Name;
                L7U.Text = Settings.NewTabSites.FavoritedSite7.Url;
            }
            else
            {
                L7T.Text = "...";
                L7U.Text = "...";
            }
            //l8
            if (Settings.NewTabSites.FavoritedSite8 != null)
            {
                L8T.Text = Settings.NewTabSites.FavoritedSite8.Name;
                L8U.Text = Settings.NewTabSites.FavoritedSite8.Url;
            }
            else
            {
                L8T.Text = "...";
                L8U.Text = "...";
            }
            //l9
            if (Settings.NewTabSites.FavoritedSite9 != null)
            {
                L9T.Text = Settings.NewTabSites.FavoritedSite9.Name;
                L9U.Text = Settings.NewTabSites.FavoritedSite9.Url;
            }
            else
            {
                L9T.Text = "...";
                L9U.Text = "...";
            }
            L0.BorderStyle = editL == 0 ? BorderStyle.FixedSingle : BorderStyle.None;
            L1.BorderStyle = editL == 1 ? BorderStyle.FixedSingle : BorderStyle.None;
            L2.BorderStyle = editL == 2 ? BorderStyle.FixedSingle : BorderStyle.None;
            L3.BorderStyle = editL == 3 ? BorderStyle.FixedSingle : BorderStyle.None;
            L4.BorderStyle = editL == 4 ? BorderStyle.FixedSingle : BorderStyle.None;
            L5.BorderStyle = editL == 5 ? BorderStyle.FixedSingle : BorderStyle.None;
            L6.BorderStyle = editL == 6 ? BorderStyle.FixedSingle : BorderStyle.None;
            L7.BorderStyle = editL == 7 ? BorderStyle.FixedSingle : BorderStyle.None;
            L8.BorderStyle = editL == 8 ? BorderStyle.FixedSingle : BorderStyle.None;
            L9.BorderStyle = editL == 9 ? BorderStyle.FixedSingle : BorderStyle.None;
            NTRefreshNotDone = false;
        }

        private int editL = 0;

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            if (NTRefreshNotDone) { return; }
            if (editL == 0)
            {
                if (Settings.NewTabSites.FavoritedSite0 == null) { Settings.NewTabSites.FavoritedSite0 = new Site(); }
                Settings.NewTabSites.FavoritedSite0.Name = tbTitle.Text;
            }
            else if (editL == 1)
            {
                if (Settings.NewTabSites.FavoritedSite1 == null) { Settings.NewTabSites.FavoritedSite1 = new Site(); }
                Settings.NewTabSites.FavoritedSite1.Name = tbTitle.Text;
            }
            else if (editL == 2)
            {
                if (Settings.NewTabSites.FavoritedSite2 == null) { Settings.NewTabSites.FavoritedSite2 = new Site(); }
                Settings.NewTabSites.FavoritedSite2.Name = tbTitle.Text;
            }
            else if (editL == 3)
            {
                if (Settings.NewTabSites.FavoritedSite3 == null) { Settings.NewTabSites.FavoritedSite3 = new Site(); }
                Settings.NewTabSites.FavoritedSite3.Name = tbTitle.Text;
            }
            else if (editL == 4)
            {
                if (Settings.NewTabSites.FavoritedSite4 == null) { Settings.NewTabSites.FavoritedSite4 = new Site(); }
                Settings.NewTabSites.FavoritedSite4.Name = tbTitle.Text;
            }
            else if (editL == 5)
            {
                if (Settings.NewTabSites.FavoritedSite5 == null) { Settings.NewTabSites.FavoritedSite5 = new Site(); }
                Settings.NewTabSites.FavoritedSite5.Name = tbTitle.Text;
            }
            else if (editL == 6)
            {
                if (Settings.NewTabSites.FavoritedSite6 == null) { Settings.NewTabSites.FavoritedSite6 = new Site(); }
                Settings.NewTabSites.FavoritedSite6.Name = tbTitle.Text;
            }
            else if (editL == 7)
            {
                if (Settings.NewTabSites.FavoritedSite7 == null) { Settings.NewTabSites.FavoritedSite7 = new Site(); }
                Settings.NewTabSites.FavoritedSite7.Name = tbTitle.Text;
            }
            else if (editL == 8)
            {
                if (Settings.NewTabSites.FavoritedSite8 == null) { Settings.NewTabSites.FavoritedSite8 = new Site(); }
                Settings.NewTabSites.FavoritedSite8.Name = tbTitle.Text;
            }
            else if (editL == 9)
            {
                if (Settings.NewTabSites.FavoritedSite9 == null) { Settings.NewTabSites.FavoritedSite9 = new Site(); }
                Settings.NewTabSites.FavoritedSite9.Name = tbTitle.Text;
            }
            LoadNewTabSites();
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            if (NTRefreshNotDone) { return; }
            if (editL == 0)
            {
                if (Settings.NewTabSites.FavoritedSite0 == null) { Settings.NewTabSites.FavoritedSite0 = new Site(); }
                Settings.NewTabSites.FavoritedSite0.Url = tbUrl.Text;
            }
            else if (editL == 1)
            {
                if (Settings.NewTabSites.FavoritedSite1 == null) { Settings.NewTabSites.FavoritedSite1 = new Site(); }
                Settings.NewTabSites.FavoritedSite1.Url = tbUrl.Text;
            }
            else if (editL == 2)
            {
                if (Settings.NewTabSites.FavoritedSite2 == null) { Settings.NewTabSites.FavoritedSite2 = new Site(); }
                Settings.NewTabSites.FavoritedSite2.Url = tbUrl.Text;
            }
            else if (editL == 3)
            {
                if (Settings.NewTabSites.FavoritedSite3 == null) { Settings.NewTabSites.FavoritedSite3 = new Site(); }
                Settings.NewTabSites.FavoritedSite3.Url = tbUrl.Text;
            }
            else if (editL == 4)
            {
                if (Settings.NewTabSites.FavoritedSite4 == null) { Settings.NewTabSites.FavoritedSite4 = new Site(); }
                Settings.NewTabSites.FavoritedSite4.Url = tbUrl.Text;
            }
            else if (editL == 5)
            {
                if (Settings.NewTabSites.FavoritedSite5 == null) { Settings.NewTabSites.FavoritedSite5 = new Site(); }
                Settings.NewTabSites.FavoritedSite5.Url = tbUrl.Text;
            }
            else if (editL == 6)
            {
                if (Settings.NewTabSites.FavoritedSite6 == null) { Settings.NewTabSites.FavoritedSite6 = new Site(); }
                Settings.NewTabSites.FavoritedSite6.Url = tbUrl.Text;
            }
            else if (editL == 7)
            {
                if (Settings.NewTabSites.FavoritedSite7 == null) { Settings.NewTabSites.FavoritedSite7 = new Site(); }
                Settings.NewTabSites.FavoritedSite7.Url = tbUrl.Text;
            }
            else if (editL == 8)
            {
                if (Settings.NewTabSites.FavoritedSite8 == null) { Settings.NewTabSites.FavoritedSite8 = new Site(); }
                Settings.NewTabSites.FavoritedSite8.Url = tbUrl.Text;
            }
            else if (editL == 9)
            {
                if (Settings.NewTabSites.FavoritedSite9 == null) { Settings.NewTabSites.FavoritedSite9 = new Site(); }
                Settings.NewTabSites.FavoritedSite9.Url = tbUrl.Text;
            }
            LoadNewTabSites();
        }

        private bool NTRefreshNotDone = false;
        private void siteItem_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            Panel pnl = cntrl is Panel ? cntrl as Panel : cntrl.Parent as Panel;
            if (pnl == null) { return; }
            if (pnl.Tag == null) { return; }
            int itemid = Convert.ToInt32(pnl.Tag);
            editL = itemid;
            NTRefreshNotDone = true;
            LoadNewTabSites();
            if (itemid == 0)
            {
                if (Settings.NewTabSites.FavoritedSite0 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite0.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite0.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 1)
            {
                if (Settings.NewTabSites.FavoritedSite1 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite1.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite1.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 2)
            {
                if (Settings.NewTabSites.FavoritedSite2 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite2.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite2.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 3)
            {
                if (Settings.NewTabSites.FavoritedSite3 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite3.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite3.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 4)
            {
                if (Settings.NewTabSites.FavoritedSite4 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite4.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite4.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 5)
            {
                if (Settings.NewTabSites.FavoritedSite5 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite5.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite5.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 6)
            {
                if (Settings.NewTabSites.FavoritedSite6 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite6.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite6.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 7)
            {
                if (Settings.NewTabSites.FavoritedSite7 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite7.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite7.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 8)
            {
                if (Settings.NewTabSites.FavoritedSite8 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite8.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite8.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            else if (itemid == 9)
            {
                if (Settings.NewTabSites.FavoritedSite9 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite9.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite9.Url; }
                tbTitle.Enabled = true;
                tbUrl.Enabled = true;
                btClear.Enabled = true;
            }
            NTRefreshNotDone = false;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            if (!NTRefreshNotDone)
            {
                if (editL == 0) { Settings.NewTabSites.FavoritedSite0 = null; }
                else if (editL == 1) { Settings.NewTabSites.FavoritedSite1 = null; }
                else if (editL == 2) { Settings.NewTabSites.FavoritedSite2 = null; }
                else if (editL == 3) { Settings.NewTabSites.FavoritedSite3 = null; }
                else if (editL == 4) { Settings.NewTabSites.FavoritedSite4 = null; }
                else if (editL == 5) { Settings.NewTabSites.FavoritedSite5 = null; }
                else if (editL == 6) { Settings.NewTabSites.FavoritedSite6 = null; }
                else if (editL == 7) { Settings.NewTabSites.FavoritedSite7 = null; }
                else if (editL == 8) { Settings.NewTabSites.FavoritedSite8 = null; }
                else if (editL == 9) { Settings.NewTabSites.FavoritedSite9 = null; }
                tbTitle.Text = "";
                tbUrl.Text = "";
                LoadNewTabSites();
            }
        }

        private void btlangFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Application.StartupPath + "\\Lang\\\"");
        }

        private void btLangStore_Click(object sender, EventArgs e)
        {
            NewTab("https://haltroy.com/store/Korot/Languages/index.html");
        }

        private void cbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLangFromFile(Application.StartupPath + "//Lang//" + cbLang.Text + ".klf");
        }

        private void btBlocked_Click(object sender, EventArgs e)
        {
            OpenBlockSettings();
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
