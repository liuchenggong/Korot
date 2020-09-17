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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public partial class frmCEF : Form
    {
        public frmSettings setmenu;
        public frmHamburger hammenu;
        public frmProfile profmenu;
        public frmIncognito incognitomenu;
        public frmPrivacy privmenu;
        public string DateFormat = "dd/MM/yy HH:mm:ss";
        public Settings Settings;
        public bool closing;
        public ContextMenuStrip cmsCEF = null;
        private bool isLoading = false;
        private readonly string loaduri = null;
        public bool _Incognito = false;
        public string userName;
        public string profilePath;
        private readonly string userCache;
        public SessionSystem SessionSystem;
        public string defaultProxy = null;
        public ChromiumWebBrowser chromiumWebBrowser1;
        private readonly List<ToolStripMenuItem> favoritesFolders = new List<ToolStripMenuItem>();
        private readonly List<ToolStripMenuItem> favoritesNoIcon = new List<ToolStripMenuItem>();
        public bool NotificationListenerMode = false;
        private readonly frmMain _anaform;
        private frmMain _parentform => ((frmMain)ParentTabs);

        public frmMain anaform
        {
            get
            {
                if (_parentform is null)
                {
                    return _anaform;
                }
                else { return _parentform; }
            }
        }

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

        public frmCEF(frmMain _aform, Settings settings, bool isIncognito = false, string loadurl = "korot://newtab", string profileName = "user0", bool notifListenMode = false, string Session = "")
        {
            _anaform = _aform;
            Settings = settings;
            SessionSystem = new SessionSystem(Session);
            NotificationListenerMode = notifListenMode;
            loaduri = loadurl;
            _Incognito = isIncognito;
            userName = profileName;
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + profileName + "\\";
            userCache = profilePath + "cache\\";
            InitializeComponent();
            InitializeChromium();
            RefreshProfilePic();
            foreach (Control x in Controls)
            {
                try { x.KeyDown += tabform_KeyDown; x.MouseWheel += MouseScroll; x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
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
                + KorotTools.getOSInfo()
                + "; "
                + (Environment.Is64BitProcess ? "Win64" : "Win32NT")
                + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/"
                + Cef.ChromiumVersion
                + " Safari/537.36 Korot/"
                + Application.ProductVersion.ToString()
                + " (" + VersionInfo.CodeName + ")"
            };
            if (_Incognito) { settings.CachePath = null; settings.PersistSessionCookies = false; settings.RootCachePath = null; }
            else { settings.CachePath = userCache; settings.RootCachePath = userCache; }
            CefCustomScheme scheme = new CefCustomScheme
            {
                SchemeName = "korot",
                SchemeHandlerFactory = new SchemeHandlerFactory(this)
                {
                    ext = null,
                    isExt = false,
                    extForm = null
                },
            };
            settings.RegisterScheme(scheme);
            // Initialize cef with the provided settings
            settings.DisableGpuAcceleration();
            if (Settings.Flash) { settings.CefCommandLineArgs.Add("enable-system-flash"); }
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            chromiumWebBrowser1 = new ChromiumWebBrowser("");
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
            chromiumWebBrowser1.Load(string.IsNullOrWhiteSpace(loaduri) ? "korot://newtab" : loaduri);
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
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                allowSwitching = true;
                tabControl1.SelectedTab = tpSettings;
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
            timer1.Start();
            LoadLangFromFile(Settings.LanguageSystem.LangFile);
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

            ChangeTheme();
            Settings.UpdateFavList();
            RefreshFavorites();
            chromiumWebBrowser1.Select();
            if (_Incognito)
            {
                btProfile.Enabled = false;
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
                        tsopenInNewTab.Text = anaform.OpenInNewTab;
                        openİnNewWindowToolStripMenuItem.Text = anaform.openInNewWindow;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = anaform.openInNewIncWindow;
                        cmsFavorite.Show(MousePosition);
                    }
                }
            }
            else if (selectedFavorite.Tag is Folder)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (tsmi.Tag != null)
                    {
                        tsopenInNewTab.Text = anaform.openAllInNewTab;
                        openİnNewWindowToolStripMenuItem.Text = anaform.openAllInNewWindow;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = anaform.openAllInNewIncWindow;
                        cmsFavorite.Show(MousePosition);
                    }
                }
            }
        }

        #region "Translate"

        public void LoadLangFromFile(string fileLocation)
        {
            if (Settings.LanguageSystem.LangFile != fileLocation) { Settings.LanguageSystem.ReadFromFile(fileLocation, true); }
            anaform.Reload = Settings.LanguageSystem.GetItemText("Reload");
            anaform.soundFiles = Settings.LanguageSystem.GetItemText("SoundFiles");
            anaform.KorotUpToDate = Settings.LanguageSystem.GetItemText("KorotUpToDate");
            anaform.KorotUpdating = Settings.LanguageSystem.GetItemText("KorotUpdating");
            anaform.KorotUpdated = Settings.LanguageSystem.GetItemText("KorotUpdated");
            anaform.CleanCacheMessage = Settings.LanguageSystem.GetItemText("AutoCleanMessage");
            anaform.Extensions = Settings.LanguageSystem.GetItemText("Extensions");
            anaform.editblockitem = Settings.LanguageSystem.GetItemText("EditBlockItem");
            anaform.addblockitem = Settings.LanguageSystem.GetItemText("AddBlockItem");
            anaform.lv0 = Settings.LanguageSystem.GetItemText("Lv0");
            anaform.lv1 = Settings.LanguageSystem.GetItemText("Lv1");
            anaform.lv2 = Settings.LanguageSystem.GetItemText("Lv2");
            anaform.lv3 = Settings.LanguageSystem.GetItemText("Lv3");
            anaform.blocklevel = Settings.LanguageSystem.GetItemText("BlockLevel");
            anaform.Done = Settings.LanguageSystem.GetItemText("Done");
            anaform.BlockThisSite = Settings.LanguageSystem.GetItemText("BlockThisSite");
            anaform.lv0info = Settings.LanguageSystem.GetItemText("LV0Info");
            anaform.lv1info = Settings.LanguageSystem.GetItemText("LV1Info");
            anaform.lv2info = Settings.LanguageSystem.GetItemText("LV2Info");
            anaform.lv3info = Settings.LanguageSystem.GetItemText("LV3Info");

            anaform.ChangePicInfo = Settings.LanguageSystem.GetItemText("ChangePicInfo");
            anaform.ResetImage = Settings.LanguageSystem.GetItemText("ResetImage");
            anaform.SelectNewImage = Settings.LanguageSystem.GetItemText("SelectNewImage");
            anaform.MuteThisTab = Settings.LanguageSystem.GetItemText("MuteThisTab");
            anaform.ChangePic = Settings.LanguageSystem.GetItemText("ChangePic");
            anaform.ProfileNameTemp = Settings.LanguageSystem.GetItemText("ProfileNameTemp");
            anaform.UnmuteThisTab = Settings.LanguageSystem.GetItemText("UnmuteThisTab");
            anaform.allowCookie = Settings.LanguageSystem.GetItemText("AllowCookie");
            anaform.NewTabEdit = Settings.LanguageSystem.GetItemText("NewTabEdit");

            anaform.Clear = Settings.LanguageSystem.GetItemText("Clear");
            anaform.RemoveSelected = Settings.LanguageSystem.GetItemText("RemoveSelected");
            anaform.ImportProfile = Settings.LanguageSystem.GetItemText("ImportProfile");
            anaform.importProfileInfo = Settings.LanguageSystem.GetItemText("ImportProfileInfo");
            anaform.ExportProfile = Settings.LanguageSystem.GetItemText("ExportProfile");
            anaform.exportProfileInfo = Settings.LanguageSystem.GetItemText("ExportProfileInfo");
            anaform.ProfileFileInfo = Settings.LanguageSystem.GetItemText("ProfileFileInfo");
            string[] errormenu = new string[] { Settings.LanguageSystem.GetItemText("ErrorRestart"), Settings.LanguageSystem.GetItemText("ErrorDesc1"), Settings.LanguageSystem.GetItemText("ErrorDesc2"), Settings.LanguageSystem.GetItemText("ErrorTI") };
            SafeFileSettingOrganizedClass.ErrorMenu = errormenu;
            anaform.notificationPermission = Settings.LanguageSystem.GetItemText("NotificationInfo");
            anaform.deny = Settings.LanguageSystem.GetItemText("Deny");
            anaform.allow = Settings.LanguageSystem.GetItemText("Allow");
            anaform.changeColID = Settings.LanguageSystem.GetItemText("ChangeCollectionID");
            anaform.siteCookies = Settings.LanguageSystem.GetItemText("Cookies");
            anaform.siteNotifications = Settings.LanguageSystem.GetItemText("Notifications");
            anaform.changeColIDInfo = Settings.LanguageSystem.GetItemText("ChangeCollectionIDInfo");
            anaform.changeColText = Settings.LanguageSystem.GetItemText("ChangeCollectionText");
            anaform.changeColTextInfo = Settings.LanguageSystem.GetItemText("ChangeCollectionTextInfo");
            anaform.importColItem = Settings.LanguageSystem.GetItemText("ImportItemInfo");
            anaform.importColItemInfo = Settings.LanguageSystem.GetItemText("ImportItem");
            anaform.SetToDefault = Settings.LanguageSystem.GetItemText("SetToDefault");
            anaform.titleBackInfo = Settings.LanguageSystem.GetItemText("ChangeTitleColorInfo");
            anaform.addToCollection = Settings.LanguageSystem.GetItemText("AddToCollection");
            anaform.newColInfo = Settings.LanguageSystem.GetItemText("NewCollectionInfo");
            anaform.newColName = Settings.LanguageSystem.GetItemText("NewCollectionName");
            anaform.importColInfo = Settings.LanguageSystem.GetItemText("ImportCollectionInfo");
            anaform.delColInfo = Settings.LanguageSystem.GetItemText("CollectionDeleteInfo");
            anaform.clearColInfo = Settings.LanguageSystem.GetItemText("CollectionClearInfo");
            anaform.okToClipboard = Settings.LanguageSystem.GetItemText("OKTOClipboard");
            anaform.deleteCollection = Settings.LanguageSystem.GetItemText("DeleteCollection");
            anaform.importCollection = Settings.LanguageSystem.GetItemText("Import");
            anaform.exportCollection = Settings.LanguageSystem.GetItemText("Export");
            anaform.deleteItem = Settings.LanguageSystem.GetItemText("DeleteItem");
            anaform.exportItem = Settings.LanguageSystem.GetItemText("ExportItem");
            anaform.editItem = Settings.LanguageSystem.GetItemText("EditItem");
            anaform.catCommon = Settings.LanguageSystem.GetItemText("CollectionCommon");
            anaform.catText = Settings.LanguageSystem.GetItemText("CollectionTextBased");
            anaform.catOnline = Settings.LanguageSystem.GetItemText("CollectionOnline");
            anaform.catPicture = Settings.LanguageSystem.GetItemText("CollectionPicture");
            anaform.TitleID = Settings.LanguageSystem.GetItemText("CollectionID");
            anaform.TitleBackColor = Settings.LanguageSystem.GetItemText("CollectionBackColor");
            anaform.TitleText = Settings.LanguageSystem.GetItemText("CollectionText");
            anaform.TitleFont = Settings.LanguageSystem.GetItemText("CollectionFont");
            anaform.TitleSize = Settings.LanguageSystem.GetItemText("CollectionFontSize");
            anaform.TitleProp = Settings.LanguageSystem.GetItemText("CollectionFontProperties");
            anaform.TitleRegular = Settings.LanguageSystem.GetItemText("Regular");
            anaform.TitleBold = Settings.LanguageSystem.GetItemText("Bold");
            anaform.TitleItalic = Settings.LanguageSystem.GetItemText("Italic");
            anaform.TitleUnderline = Settings.LanguageSystem.GetItemText("Underline");
            anaform.TitleStrikeout = Settings.LanguageSystem.GetItemText("Strikeout");
            anaform.TitleForeColor = Settings.LanguageSystem.GetItemText("CollectionForeColor");
            anaform.TitleSource = Settings.LanguageSystem.GetItemText("CollectionSource");
            anaform.TitleWidth = Settings.LanguageSystem.GetItemText("CollectionWidth");
            anaform.TitleHeight = Settings.LanguageSystem.GetItemText("CollectionHeight");
            anaform.TitleDone = Settings.LanguageSystem.GetItemText("CollectionDone");
            anaform.TitleEditItem = Settings.LanguageSystem.GetItemText("EditCollectionItem");
            anaform.image = Settings.LanguageSystem.GetItemText("CollectionItemImage");
            anaform.text = Settings.LanguageSystem.GetItemText("CollectionItemText");
            anaform.link = Settings.LanguageSystem.GetItemText("CollectionItemLink");

            anaform.Collections = Settings.LanguageSystem.GetItemText("Collections");
            anaform.CertificateError = Settings.LanguageSystem.GetItemText("CertificateError");

            anaform.AboutText = Settings.LanguageSystem.GetItemText("About");
            tpSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
            anaform.SettingsText = Settings.LanguageSystem.GetItemText("Settings");
            anaform.DownloadsText = Settings.LanguageSystem.GetItemText("Downloads");

            anaform.HistoryText = Settings.LanguageSystem.GetItemText("History");

            anaform.ThemesText = Settings.LanguageSystem.GetItemText("Themes");

            anaform.ubuntuLicense = Settings.LanguageSystem.GetItemText("UbuntuFontLicense");
            anaform.updateTitleTheme = Settings.LanguageSystem.GetItemText("KorotThemeUpdater");
            anaform.updateTitleExt = Settings.LanguageSystem.GetItemText("KorotExtensionUpdater");
            anaform.updateExtInfo = Settings.LanguageSystem.GetItemText("ExtensionUpdatingInfo");
            anaform.openInNewWindow = Settings.LanguageSystem.GetItemText("OpeninNewWindow");
            anaform.openAllInNewWindow = Settings.LanguageSystem.GetItemText("OpenAllinNewWindow");
            anaform.openInNewIncWindow = Settings.LanguageSystem.GetItemText("OpeninNewIncognitoWindow");
            anaform.openAllInNewIncWindow = Settings.LanguageSystem.GetItemText("OpenAllinNewIncognitoWindow");
            anaform.openAllInNewTab = Settings.LanguageSystem.GetItemText("OpenAllInNewTab");
            anaform.newFavorite = Settings.LanguageSystem.GetItemText("NewFavorite");
            anaform.nametd = Settings.LanguageSystem.GetItemText("Name");
            anaform.urltd = Settings.LanguageSystem.GetItemText("Url");
            anaform.add = Settings.LanguageSystem.GetItemText("Add");
            newFavoriteToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("NewFavorite");
            newFolderToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("NewFolderButton");
            anaform.newFolder = Settings.LanguageSystem.GetItemText("NewFolderButton");
            anaform.defaultFolderName = Settings.LanguageSystem.GetItemText("NewFolder");
            anaform.folderInfo = Settings.LanguageSystem.GetItemText("PleaseenteranamefornewFolder");
            anaform.copyImage = Settings.LanguageSystem.GetItemText("CopyImage");
            anaform.openLinkInNewWindow = Settings.LanguageSystem.GetItemText("OpenLinkinaNewWindow");
            anaform.openLinkInNewIncWindow = Settings.LanguageSystem.GetItemText("OpenLinkinaNewIncognitoWindow");
            anaform.copyImageAddress = Settings.LanguageSystem.GetItemText("CopyImageAddress");
            anaform.saveLinkAs = Settings.LanguageSystem.GetItemText("SaveLinkAs");
            anaform.empty = Settings.LanguageSystem.GetItemText("Empty");
            anaform.licenseTitle = Settings.LanguageSystem.GetItemText("TitleLicensesSpecialThanks");
            anaform.kLicense = Settings.LanguageSystem.GetItemText("KorotLicense");
            anaform.vsLicense = Settings.LanguageSystem.GetItemText("MSVS2019CLicense");
            anaform.chLicense = Settings.LanguageSystem.GetItemText("ChromiumLicense");
            anaform.cefLicense = Settings.LanguageSystem.GetItemText("CefSharpLicense");
            anaform.etLicense = Settings.LanguageSystem.GetItemText("EasyTabsLicense");
            anaform.specialThanks = Settings.LanguageSystem.GetItemText("SpecialThanks");
            anaform.JSAlert = Settings.LanguageSystem.GetItemText("MessageDialog");
            anaform.JSConfirm = Settings.LanguageSystem.GetItemText("ConfirmDialog");
            anaform.selectAFolder = Settings.LanguageSystem.GetItemText("DownloadFolderInfo");

            anaform.resetConfirm = Settings.LanguageSystem.GetItemText("ResetKorotInfo");

            anaform.IncognitoModeTitle = Settings.LanguageSystem.GetItemText("IncognitoMode");
            anaform.IncognitoModeInfo = Settings.LanguageSystem.GetItemText("IncognitoModeInfo");
            anaform.LearnMore = Settings.LanguageSystem.GetItemText("ClickToLearnMore");

            anaform.findC = Settings.LanguageSystem.GetItemText("Current");
            anaform.findT = Settings.LanguageSystem.GetItemText("Total");
            anaform.findL = Settings.LanguageSystem.GetItemText("Last");
            anaform.noSearch = Settings.LanguageSystem.GetItemText("NotSearchingNoResults");
            anaform.anon = Settings.LanguageSystem.GetItemText("ThemeUnknownPerson");
            anaform.noname = Settings.LanguageSystem.GetItemText("ThemeNameUnknown");
            anaform.themeInfo = Settings.LanguageSystem.GetItemText("AboutInfoTheme");
            anaform.renderProcessDies = Settings.LanguageSystem.GetItemText("RenderProcessTerminated");
            anaform.enterAValidCode = Settings.LanguageSystem.GetItemText("EnterValidBase64");
            anaform.ResetZoom = Settings.LanguageSystem.GetItemText("ResetZoom");
            anaform.htmlFiles = Settings.LanguageSystem.GetItemText("HTMLFile");
            anaform.print = Settings.LanguageSystem.GetItemText("Print");

            anaform.IncognitoTitle1 = Settings.LanguageSystem.GetItemText("IncognitoInfoTitle");
            anaform.IncognitoT1M1 = Settings.LanguageSystem.GetItemText("IncognitoInfoT1M1");
            anaform.IncognitoT1M2 = Settings.LanguageSystem.GetItemText("IncognitoInfoT1M2");
            anaform.IncognitoT1M3 = Settings.LanguageSystem.GetItemText("IncognitoInfoT1M3");
            anaform.IncognitoTitle2 = Settings.LanguageSystem.GetItemText("IncognitoInfoTitle2");
            anaform.IncognitoT2M1 = Settings.LanguageSystem.GetItemText("IncognitoInfoT2M1");
            anaform.IncognitoT2M2 = Settings.LanguageSystem.GetItemText("IncognitoInfoT2M2");
            anaform.IncognitoT2M3 = Settings.LanguageSystem.GetItemText("IncognitoInfoT2M3");
            anaform.disallowCookie = Settings.LanguageSystem.GetItemText("DisallowCookie");

            anaform.imageFiles = Settings.LanguageSystem.GetItemText("ImageFiles");
            anaform.allFiles = Settings.LanguageSystem.GetItemText("AllFiles");
            anaform.selectBackImage = Settings.LanguageSystem.GetItemText("SelectBackgroundImage");

            anaform.usingBC = Settings.LanguageSystem.GetItemText("UsingBackgroundColor");

            anaform.CertErrorPageButton = Settings.LanguageSystem.GetItemText("UserUnderstandsRisks");
            anaform.CertErrorPageMessage = Settings.LanguageSystem.GetItemText("WebsiteNotSafeInfo");
            anaform.CertErrorPageTitle = Settings.LanguageSystem.GetItemText("WebsiteNotSafe");
            anaform.usesCookies = Settings.LanguageSystem.GetItemText("WebsiteUsesCookies");
            anaform.notUsesCookies = Settings.LanguageSystem.GetItemText("WebsiteNoCookies");
            anaform.showCertError = Settings.LanguageSystem.GetItemText("ShowCertificateError");
            anaform.CertificateErrorMenuTitle = Settings.LanguageSystem.GetItemText("CertificateErrorDetails");
            anaform.CertificateErrorTitle = Settings.LanguageSystem.GetItemText("NotSafe");
            anaform.CertificateError = Settings.LanguageSystem.GetItemText("WebsiteWithErrors");
            anaform.CertificateOKTitle = Settings.LanguageSystem.GetItemText("Safe");
            anaform.CertificateOK = Settings.LanguageSystem.GetItemText("WebsiteNoErrors");
            anaform.ErrorTheme = Settings.LanguageSystem.GetItemText("ThemeFileCorrupted");
            anaform.ThemeMessage = Settings.LanguageSystem.GetItemText("ApplyThemeInfo");

            anaform.checking = Settings.LanguageSystem.GetItemText("CheckingForUpdates");
            anaform.uptodate = Settings.LanguageSystem.GetItemText("UpToDate");
            anaform.installStatus = Settings.LanguageSystem.GetItemText("UpdatingMessage");
            anaform.StatusType = Settings.LanguageSystem.GetItemText("DownloadProgress");

            anaform.updateavailable = Settings.LanguageSystem.GetItemText("UpdateAvailable"); ;
            anaform.privatemode = Settings.LanguageSystem.GetItemText("Incognito");
            anaform.updateTitle = Settings.LanguageSystem.GetItemText("KorotUpdate");
            anaform.updateMessage = Settings.LanguageSystem.GetItemText("KorotUpdateAvailable");
            anaform.updateError = Settings.LanguageSystem.GetItemText("KorotUpdateError");
            anaform.NewTabtitle = Settings.LanguageSystem.GetItemText("NewTab");
            anaform.customSearchNote = Settings.LanguageSystem.GetItemText("SearchEngineInfo");
            anaform.customSearchMessage = Settings.LanguageSystem.GetItemText("SearchengineTitle");

            anaform.newWindow = Settings.LanguageSystem.GetItemText("NewWindow");
            anaform.newincognitoWindow = Settings.LanguageSystem.GetItemText("NewIncognitoWindow");

            anaform.SearchOnPage = Settings.LanguageSystem.GetItemText("SearchOnThisPage");

            anaform.settingstitle = Settings.LanguageSystem.GetItemText("Settings");

            anaform.IncognitoT = Settings.LanguageSystem.GetItemText("Incognito");
            anaform.IncognitoTitle = Settings.LanguageSystem.GetItemText("IncognitoInfoTitle");
            anaform.newCollection = Settings.LanguageSystem.GetItemText("NewCollectionName");
            anaform.clearCollection = Settings.LanguageSystem.GetItemText("DeleteCollection");
            anaform.goBack = Settings.LanguageSystem.GetItemText("GoBack");
            anaform.goForward = Settings.LanguageSystem.GetItemText("GoForward");
            anaform.refresh = Settings.LanguageSystem.GetItemText("Refresh");
            anaform.refreshNoCache = Settings.LanguageSystem.GetItemText("RefreshNoCache");
            anaform.stop = Settings.LanguageSystem.GetItemText("Stop");
            anaform.selectAll = Settings.LanguageSystem.GetItemText("SelectAll");
            anaform.openLinkInNewTab = Settings.LanguageSystem.GetItemText("OpenLinkInNewTab");
            anaform.copyLink = Settings.LanguageSystem.GetItemText("CopyLink");
            anaform.saveImageAs = Settings.LanguageSystem.GetItemText("SaveImageAs");
            anaform.openImageInNewTab = Settings.LanguageSystem.GetItemText("OpenImageInNewTab");
            anaform.paste = Settings.LanguageSystem.GetItemText("Paste");
            anaform.copy = Settings.LanguageSystem.GetItemText("Copy");
            anaform.cut = Settings.LanguageSystem.GetItemText("Cut");
            anaform.undo = Settings.LanguageSystem.GetItemText("Undo");
            anaform.redo = Settings.LanguageSystem.GetItemText("Redo");
            anaform.delete = Settings.LanguageSystem.GetItemText("Delete");
            anaform.SearchOrOpenSelectedInNewTab = Settings.LanguageSystem.GetItemText("SearchOpenTheSelected");
            anaform.developerTools = Settings.LanguageSystem.GetItemText("DeveloperTools");
            anaform.viewSource = Settings.LanguageSystem.GetItemText("ViewSource");
            anaform.restoreOldSessions = Settings.LanguageSystem.GetItemText("RestoreLastSession");

            anaform.enterAValidUrl = Settings.LanguageSystem.GetItemText("EnterAValidURL");

            anaform.Month1 = Settings.LanguageSystem.GetItemText("Month1");
            anaform.Month2 = Settings.LanguageSystem.GetItemText("Month2");
            anaform.Month3 = Settings.LanguageSystem.GetItemText("Month3");
            anaform.Month4 = Settings.LanguageSystem.GetItemText("Month4");
            anaform.Month5 = Settings.LanguageSystem.GetItemText("Month5");
            anaform.Month6 = Settings.LanguageSystem.GetItemText("Month6");
            anaform.Month7 = Settings.LanguageSystem.GetItemText("Month7");
            anaform.Month8 = Settings.LanguageSystem.GetItemText("Month8");
            anaform.Month9 = Settings.LanguageSystem.GetItemText("Month9");
            anaform.Month10 = Settings.LanguageSystem.GetItemText("Month10");
            anaform.Month11 = Settings.LanguageSystem.GetItemText("Month11");
            anaform.Month12 = Settings.LanguageSystem.GetItemText("Month12");
            anaform.Month0 = Settings.LanguageSystem.GetItemText("Month0");
            anaform.fromtwodot = Settings.LanguageSystem.GetItemText("From1");
            anaform.totwodot = Settings.LanguageSystem.GetItemText("To1");
            anaform.korotdownloading = Settings.LanguageSystem.GetItemText("KorotDownloading");

            anaform.open = Settings.LanguageSystem.GetItemText("Open");
            anaform.openLinkInNewTab = Settings.LanguageSystem.GetItemText("OpenLinkInNewTab");
            removeSelectedTSMI.Text = Settings.LanguageSystem.GetItemText("RemoveSelected");
            clearTSMI.Text = Settings.LanguageSystem.GetItemText("Clear");
            anaform.OpenInNewTab = Settings.LanguageSystem.GetItemText("OpenInNewTab");
            anaform.OpenFile = Settings.LanguageSystem.GetItemText("OpenFile");
            anaform.OpenFileInExplorert = Settings.LanguageSystem.GetItemText("OpenFolderContainingThisFile");
            anaform.ResetToDefaultProxy = Settings.LanguageSystem.GetItemText("ResetToFefaultProxySetting");

            anaform.Yes = Settings.LanguageSystem.GetItemText("Yes");
            anaform.No = Settings.LanguageSystem.GetItemText("No");
            anaform.OK = Settings.LanguageSystem.GetItemText("OK");
            anaform.Cancel = Settings.LanguageSystem.GetItemText("Cancel");

            anaform.SearchOnWeb = Settings.LanguageSystem.GetItemText("AddressBar2");
            anaform.goTotxt = Settings.LanguageSystem.GetItemText("AddressBar1");
            anaform.newProfileInfo = Settings.LanguageSystem.GetItemText("EnterAProfileName");

            anaform.MonthNames = Settings.LanguageSystem.GetItemText("NewTabMonths");
            anaform.DayNames = Settings.LanguageSystem.GetItemText("NewTabDays");
            anaform.SearchHelpText = Settings.LanguageSystem.GetItemText("NewTabSearch");
            anaform.ErrorPageTitle = Settings.LanguageSystem.GetItemText("KorotError");
            anaform.KT = Settings.LanguageSystem.GetItemText("ErrorTitle");
            anaform.ET = Settings.LanguageSystem.GetItemText("ErrorTitle1");
            anaform.E1 = Settings.LanguageSystem.GetItemText("ErrorT1M1");
            anaform.E2 = Settings.LanguageSystem.GetItemText("ErrorT1M2");
            anaform.E3 = Settings.LanguageSystem.GetItemText("ErrorT1M3");
            anaform.E4 = Settings.LanguageSystem.GetItemText("ErrorT1M4");
            anaform.RT = Settings.LanguageSystem.GetItemText("ErrorTitle2");
            anaform.R1 = Settings.LanguageSystem.GetItemText("ErrorT2M1");
            anaform.R2 = Settings.LanguageSystem.GetItemText("ErrorT2M2");
            anaform.R3 = Settings.LanguageSystem.GetItemText("ErrorT2M3");
            anaform.R4 = Settings.LanguageSystem.GetItemText("ErrorT2M4");
            anaform.Search = Settings.LanguageSystem.GetItemText("Search");
            anaform.newprofile = Settings.LanguageSystem.GetItemText("NewProfile");
            anaform.switchTo = Settings.LanguageSystem.GetItemText("SwitchTo");
            anaform.deleteProfile = Settings.LanguageSystem.GetItemText("DeleteThisProfile");
            anaform.aboutInfo = Settings.LanguageSystem.GetItemText("KorotAbout");
        }

        #endregion "Translate"

        public string GetMonthNameOfDate(int month)
        {
            switch (month)
            {
                case 1:
                    return anaform.Month1;

                case 2:
                    return anaform.Month2;

                case 3:
                    return anaform.Month3;

                case 4:
                    return anaform.Month4;

                case 5:
                    return anaform.Month5;

                case 6:
                    return anaform.Month6;

                case 7:
                    return anaform.Month7;

                case 8:
                    return anaform.Month8;

                case 9:
                    return anaform.Month9;

                case 10:
                    return anaform.Month10;

                case 11:
                    return anaform.Month11;

                case 12:
                    return anaform.Month12;

                default:
                    return anaform.Month0; // You cannot see this month in an ordinary usage of Korot. This means that this month is not real so I put my name which I should not exist too.
            }
        }

        public string GetDateInfo(DateTime date)
        {
            return date.Day + " " + GetMonthNameOfDate(date.Month) + " " + date.Year + " " + date.Hour.ToString("00") + ":" + date.Minute.ToString("00") + ":" + date.Second.ToString("00");
        }

        public HTTitleTabs ParentTabs => (ParentForm as HTTitleTabs);

        public HTTitleTab ParentTab
        {
            get
            {
                if (!NotificationListenerMode)
                {
                    List<int> tabIndexes = new List<int>();
                    foreach (HTTitleTab x in ParentTabs.Tabs)
                    {
                        if (x.Content == this) { tabIndexes.Add(ParentTabs.Tabs.IndexOf(x)); }
                    }
                    return (tabIndexes.Count > 0 ? ParentTabs.Tabs[tabIndexes[0]] : null);
                }
                else { return null; }
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
                if (!(privmenu is null)) { privmenu.Hide(); }
                if (!(incognitomenu is null)) { incognitomenu.Hide(); }
                if (!(profmenu is null)) { profmenu.Hide(); }
                if (!(hammenu is null)) { hammenu.Hide(); }
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
                            Text = anaform.updateTitleTheme
                        };
                        extUpdate.label1.Text = anaform.updateExtInfo
.Replace("[NAME]", SplittedFase[0].Substring(0).Replace(Environment.NewLine, ""))
.Replace("[NEWLINE]", Environment.NewLine);
                        extUpdate.infoTemp = anaform.StatusType;
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
            HTAlt.WinForms.HTInputBox mesaj = new HTAlt.WinForms.HTInputBox("Korot", anaform.newColInfo, anaform.newColName)
            { Icon = Icon, OK = anaform.OK, SetToDefault = anaform.SetToDefault, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor };
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
                        btBack.Invoke(new Action(() => btBack.Enabled = SessionSystem.CanGoBack()));
                        btNext.Invoke(new Action(() => btNext.Enabled = SessionSystem.CanGoForward()));
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
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(anaform.JSAlert.Replace("[TITLE]", Text).Replace("[URL]", url), message, new HTAlt.WinForms.HTDialogBoxContext() { OK = true, Cancel = true })
                { StartPosition = FormStartPosition.CenterParent, Yes = anaform.Yes, No = anaform.No, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
                mesaj.ShowDialog();
                return true;
            }
            else { return false; }
        }

        public bool OnJSConfirm(string url, string message, out bool returnval)
        {
            if (!NotificationListenerMode)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(anaform.JSConfirm.Replace("[TITLE]", Text).Replace("[URL]", url), message, new HTAlt.WinForms.HTDialogBoxContext() { OK = true, Cancel = true })
                { StartPosition = FormStartPosition.CenterParent, Yes = anaform.Yes, No = anaform.No, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
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
                { SetToDefault = anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, Icon = Icon, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor };
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
            if (Settings.UpdateFavorites.Contains(this))
            {
                Settings.UpdateFavorites.Remove(this);
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
            if (HTAlt.Tools.ValidUrl(urlLower, new string[] { "http", "https", "about", "ftp", "smtp", "pop", "korot" }))
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
            chromiumWebBrowser1.Load(SessionSystem.SelectedSession.Url);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tpCef) //CEF
            {
                bypassThisDeletion = true;
                SessionSystem.GoBack(chromiumWebBrowser1);
                resetPage();
                //chromiumWebBrowser1.Back();
            }
            else if (tabControl1.SelectedTab == tpSettings) //Menu
            {
                resetPage();
            }
        }

        public void resetPage(bool doNotGoToCEFTab = false)
        {
            anaform.settingTab = anaform.settingTab == ParentTab ? null : anaform.settingTab;
            anaform.licenseTab = anaform.licenseTab == ParentTab ? null : anaform.licenseTab;
            if (!doNotGoToCEFTab)
            {
                allowSwitching = true;
                tabControl1.SelectedTab = tpCef;
            }
        }

        public void button3_Click(object sender, EventArgs e)
        {
            bypassThisDeletion = true;
            resetPage();
            allowSwitching = true;
            tabControl1.SelectedTab = tpCef;
            SessionSystem.GoForward(chromiumWebBrowser1);
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

        private readonly List<string> AlreadyValidUrl = new List<string>();
        private readonly List<string> AlreadyNotValidUrl = new List<string>();
        public string[] customProts = new string[] { "http", "https", "about", "ftp", "smtp", "pop", "korot" };

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
            if (!KorotTools.ValidHttpURL(e.Address))
            {
                chromiumWebBrowser1.Load(Settings.SearchEngine + e.Address);
            }
            if (SessionSystem.Sessions.Count != 0)
            {
                if (e.Address != SessionSystem.Sessions[SessionSystem.Sessions.Count - 1].ToString())
                {
                    Invoke(new Action(() => redirectTo(e.Address, Text)));
                }
            }
        }

        private void cef_onLoadError(object sender, LoadErrorEventArgs e)
        {
            if (e == null) //User Asked
            {
                chromiumWebBrowser1.Load("korot://error/?e=TEST?t=TEST?u=korot://empty");
            }
            else
            {
                if (e.FailedUrl.ToLower().StartsWith("korot") || e.FailedUrl.ToLower().StartsWith("chrome") || e.FailedUrl.ToLower().StartsWith("about"))
                {
                    if (e.Frame.IsMain)
                    {
                        chromiumWebBrowser1.Load(e.FailedUrl);
                    }
                    else
                    {
                        e.Frame.LoadUrl("korot://error/?e=FORBIDDEN?u=" + e.FailedUrl);
                    }
                }
                else
                {
                    if (e.Frame.IsMain)
                    {
                        chromiumWebBrowser1.Load("korot://error/?e=" + e.ErrorCode + "?u=" + e.FailedUrl);
                    }
                    else
                    {
                        e.Frame.LoadUrl("korot://error/?e=" + e.ErrorCode + "?u=" + e.FailedUrl);
                    }
                }
            }
        }

        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                tpCef.Text = e.Title;
                int si = SessionSystem.SelectedIndex;
                if (si != -1)
                {
                    if (SessionSystem.Sessions[si].Url != chromiumWebBrowser1.Address)
                    {
                        if (chromiumWebBrowser1.Address.ToLower().StartsWith("korot"))
                        {
                            if (chromiumWebBrowser1.Address.ToLower().StartsWith("korot://newtab") ||
chromiumWebBrowser1.Address.ToLower().StartsWith("korot://links") ||
chromiumWebBrowser1.Address.ToLower().StartsWith("korot://license") ||
chromiumWebBrowser1.Address.ToLower().StartsWith("korot://incognito"))
                            {
                                SessionSystem.Sessions[si].Title = e.Title;
                            }
                        }
                        else
                        {
                            SessionSystem.Sessions[si].Title = e.Title;
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
                Invoke(new Action(() => zoomIn()));
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if ((e.KeyData == Keys.Down || e.KeyData == Keys.PageDown) && isControlKeyPressed)
            {
                Invoke(new Action(() => zoomOut()));
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.PrintScreen && isControlKeyPressed)
            {
                Invoke(new Action(() => GetScreenShot()));
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.S && isControlKeyPressed)
            {
                Invoke(new Action(() => SavePageAs()));
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
                Invoke(new Action(() => Fullscreenmode(!anaform.isFullScreen)));
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (e.KeyData == Keys.M && isControlKeyPressed)
            {
                Invoke(new Action(() => ChangeMuteStatus()));
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        public void SavePageAs()
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Settings.SaveFolder,
                FileName = Text + ".html",
                Filter = anaform.htmlFiles + "|*.html;*.htm|" + anaform.allFiles + "|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                Task<string> htmlText = chromiumWebBrowser1.GetSourceAsync();
                HTAlt.Tools.WriteFile(save.FileName, htmlText.Result, Encoding.UTF8);
            }
        }

        public void ChangeMuteStatus()
        {
            isMuted = !isMuted;
            chromiumWebBrowser1.GetBrowserHost().SetAudioMuted(isMuted);
        }

        public void GetScreenShot()
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Settings.ScreenShotFolder,
                FileName = "Korot Screenshot.png",
                Filter = anaform.imageFiles + "|*.png|" + anaform.allFiles + "|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                HTAlt.Tools.WriteFile(save.FileName, TakeScrenshot.ImageToByte2(TakeScrenshot.Snapshot(chromiumWebBrowser1)));
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

        private void ChangeTheme(bool force = false)
        {
            if (Settings.Theme.OverlayColor != oldOverlayColor || force)
            {
                oldOverlayColor = Settings.Theme.OverlayColor;

                pbProgress.BackColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;

                if (Cef.IsInitialized)
                {
                    if (chromiumWebBrowser1.IsBrowserInitialized)
                    {
                        if (chromiumWebBrowser1.Address.StartsWith("korot:")) { chromiumWebBrowser1.Reload(); }
                    }
                }
            }

            if (Settings.Theme.BackColor != oldBackColor || force)
            {
                oldBackColor = Settings.Theme.BackColor;
                UpdateFavoriteColor();
                updateFavoritesImages();
                BackColor = Settings.Theme.BackColor;
                ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
                bool isbright = HTAlt.Tools.IsBright(Settings.Theme.BackColor);
                Color backcolor2 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
                Color backcolor3 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 40, false);
                Color backcolor4 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 60, false);
                Color rbc2 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(backcolor2);
                Color rbc3 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(backcolor3);
                Color rbc4 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(backcolor4);
                lbStatus.ForeColor = ForeColor;
                lbStatus.BackColor = Settings.Theme.BackColor;
                if (hammenu != null)
                {
                    hammenu.ForceReDraw();
                }
                if (Cef.IsInitialized)
                {
                    if (chromiumWebBrowser1.IsBrowserInitialized)
                    {
                        if (chromiumWebBrowser1.Address.StartsWith("korot:")) { chromiumWebBrowser1.Reload(); }
                    }
                }

                cmsFavorite.BackColor = Settings.Theme.BackColor;
                cmsFavorite.ForeColor = ForeColor;

                pbPrivacy.BackColor = backcolor2;

                tbAddress.BackColor = backcolor2;
                pbIncognito.BackColor = backcolor2;

                lbStatus.BackColor = Settings.Theme.BackColor;

                pbIncognito.Image = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.inctab_w : Properties.Resources.inctab);
                tbAddress.ForeColor = ForeColor;
                ForeColor = ForeColor;

                lbStatus.ForeColor = ForeColor;

                if (isPageFavorited(chromiumWebBrowser1.Address)) { btFav.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.star_on_w : Properties.Resources.star_on); } else { btFav.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.star : Properties.Resources.star_w); }
                mFavorites.ForeColor = ForeColor;
                if (!noProfilePic) { btProfile.ButtonImage = Settings.NinjaMode ? null : profilePic; } else { btProfile.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.profiles : Properties.Resources.profiles_w); }
                btBack.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                btRefresh.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.refresh : Properties.Resources.refresh_w);
                btNext.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w);
                btHome.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.home : Properties.Resources.home_w);
                btHamburger.ButtonImage = Settings.NinjaMode ? null : (isbright ? (anaform is null ? Properties.Resources.hamburger : (anaform.newDownload ? Properties.Resources.hamburger_i : Properties.Resources.hamburger)) : (anaform is null ? Properties.Resources.hamburger_w : (anaform.newDownload ? Properties.Resources.hamburger_i_w : Properties.Resources.hamburger_w)));
                tbAddress.BackColor = backcolor2;
                mFavorites.BackColor = Settings.Theme.BackColor;
                cmsBack.BackColor = Settings.Theme.BackColor;
                cmsBack.ForeColor = ForeColor;
                cmsForward.BackColor = Settings.Theme.BackColor;
                cmsForward.ForeColor = ForeColor;
                foreach (ToolStripItem x in cmsForward.Items) { x.BackColor = Settings.Theme.BackColor; x.ForeColor = ForeColor; }
                foreach (ToolStripItem x in cmsBack.Items) { x.BackColor = Settings.Theme.BackColor; x.ForeColor = ForeColor; }
                foreach (TabPage x in tabControl1.TabPages) { x.BackColor = Settings.Theme.BackColor; x.ForeColor = ForeColor; }
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

        public async void getZoomLevel()
        {
            await Task.Run(() =>
            {
                Task<double> zLevel = chromiumWebBrowser1.GetZoomLevelAsync();
                zoomLevel = zLevel.Result;
            });
        }

        public double zoomLevel = 0;

        private int timer1int = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chromiumWebBrowser1.IsDisposed)
            {
                Close();
            }
            if (IsDisposed) { return; }

            if (timer1int != 50)
            {
                timer1int++;
            }
            else
            {
                timer1int = 0;
                btHamburger.ButtonImage = HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? (anaform is null ? Properties.Resources.hamburger : (anaform.newDownload ? Properties.Resources.hamburger_i : Properties.Resources.hamburger)) : (anaform is null ? Properties.Resources.hamburger_w : (anaform.newDownload ? Properties.Resources.hamburger_i_w : Properties.Resources.hamburger_w));
                Task.Run(() => getZoomLevel());
            }
            RefreshFavorites();
            onCEFTab = (tabControl1.SelectedTab == tpCef);
            if (Settings.ThemeChangeForm.Contains(this))
            {
                ChangeTheme();
                Settings.ThemeChangeForm.Remove(this);
            }
            if (Parent != null)
            {
                Parent.Text = Text;
            }
            if (tabControl1.SelectedTab == tpCef)
            {
                Text = tpCef.Text;
            }
            else
            {
                Text = setmenu.Text;
            }
            if (NotificationListenerMode)
            {
                isMuted = true;
                if (chromiumWebBrowser1.IsBrowserInitialized)
                {
                    chromiumWebBrowser1.GetBrowserHost().SetAudioMuted(true);
                }
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
            Settings.UpdateFavList();
            RefreshFavorites();
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
            if (profmenu is null)
            {
                profmenu = new frmProfile(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Anchor = (AnchorStyles.Top | AnchorStyles.Right),
                };
                Controls.Add(profmenu);
                Settings.AllForms.Add(profmenu);
            }
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
            if (privmenu is null)
            {
                privmenu = new frmPrivacy(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                };
                Controls.Add(privmenu);
                Settings.AllForms.Add(privmenu);
            }
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

        public void SwitchToSettings()
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                setmenu.SwitchSettings();
                allowSwitching = true;
                tabControl1.SelectedTab = tpSettings;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (hammenu is null)
            {
                hammenu = new frmHamburger(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Anchor = (AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom),
                    Dock = DockStyle.Right,
                    Height = Height,
                };
                Controls.Add(hammenu);
                Settings.AllForms.Add(hammenu);
            }
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
            else
            {
                cef_LostFocus(sender, e);
            }
        }

        public string certErrorUrl = "";

        private void textBox4_Click(object sender, EventArgs e)
        {
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (incognitomenu is null)
            {
                incognitomenu = new frmIncognito(this)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Anchor = (AnchorStyles.Top | AnchorStyles.Right),
                };
                Controls.Add(incognitomenu);
                Settings.AllForms.Add(incognitomenu);
            }
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

        private void RecursiveNewTab(Folder folder, ToolStripMenuItem item)
        {
            if (folder != null)
            {
                if (folder.Favorites != null)
                {
                    foreach (ToolStripMenuItem subitem in item.DropDown.Items)
                    {
                        if (item.Tag is Favorite) { NewTab((subitem.Tag as Favorite).Url); }
                        else if (item.Tag is Folder) { RecursiveNewTab(subitem.Tag as Folder, subitem); }
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
                            else if (item.Tag is Folder) { RecursiveNewTab(item.Tag as Folder, item); }
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
                Settings.UpdateFavList();
                RefreshFavorites();
            }
        }

        private void clearTSMI_Click(object sender, EventArgs e)
        {
            Settings.Favorites.Favorites.Clear();
            btFav.ButtonImage = HTAlt.Tools.Brightness(Settings.Theme.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
            Settings.UpdateFavList();
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
                    if (selectedFavorite.Tag is Favorite)
                    {
                        tsopenInNewTab.Text = anaform.OpenInNewTab;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = anaform.openInNewIncWindow;
                        openİnNewWindowToolStripMenuItem.Text = anaform.openInNewWindow;
                    }
                    else
                    {
                        tsopenInNewTab.Text = anaform.openAllInNewTab;
                        openİnNewIncognitoWindowToolStripMenuItem.Text = anaform.openAllInNewIncWindow;
                        openİnNewWindowToolStripMenuItem.Text = anaform.openAllInNewWindow;
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
                Filter = anaform.imageFiles + "|*.png|" + anaform.allFiles + "|*.*"
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
                Filter = anaform.htmlFiles + "|*.html;*.htm|" + anaform.allFiles + "|*.*"
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
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                        Size = tpSettings.Size,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                allowSwitching = true;
                setmenu.SwitchHistory();
                tabControl1.SelectedTab = tpSettings;
            }
        }

        public void SwitchToDownloads()
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                        Size = tpSettings.Size,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                allowSwitching = true;
                setmenu.SwitchDownloads();
                tabControl1.SelectedTab = tpSettings;
            }
        }

        public void SwitchToAbout()
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                        Size = tpSettings.Size,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                allowSwitching = true;
                setmenu.SwitchAbout();
                tabControl1.SelectedTab = tpSettings;
            }
        }

        public void SwitchToThemes()
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                        Size = tpSettings.Size,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                allowSwitching = true;
                setmenu.SwitchThemes();
                tabControl1.SelectedTab = tpSettings;
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

        public void OpenSiteSettings()
        {
            Invoke(new Action(() =>
                {
                    if (anaform.settingTab != null)
                    {
                        anaform.SelectedTab = anaform.settingTab;
                    }
                    else
                    {
                        anaform.settingTab = ParentTab;
                        if (setmenu is null)
                        {
                            setmenu = new frmSettings(Settings, this)
                            {
                                TopLevel = false,
                                FormBorderStyle = FormBorderStyle.None,
                                Dock = DockStyle.Fill,
                                Visible = true,
                                ShowInTaskbar = false,
                                Size = tpSettings.Size,
                            };
                            tpSettings.Controls.Add(setmenu);
                            setmenu.Show(); Settings.AllForms.Add(setmenu);
                        }
                        allowSwitching = true;
                        setmenu.SwitchSite();
                        tabControl1.SelectedTab = tpSettings;
                    }
                }));
        }

        public void OpenBlockSettings()
        {
            Invoke(new Action(() =>
            {
                if (anaform.settingTab != null)
                {
                    anaform.SelectedTab = anaform.settingTab;
                }
                else
                {
                    anaform.settingTab = ParentTab;
                    if (setmenu is null)
                    {
                        setmenu = new frmSettings(Settings, this)
                        {
                            TopLevel = false,
                            FormBorderStyle = FormBorderStyle.None,
                            Dock = DockStyle.Fill,
                            Visible = true,
                            ShowInTaskbar = false,
                            Size = tpSettings.Size,
                        };
                        tpSettings.Controls.Add(setmenu);
                        setmenu.Show(); Settings.AllForms.Add(setmenu);
                    }
                    allowSwitching = true;
                    setmenu.SwitchBlock();
                    tabControl1.SelectedTab = tpSettings;
                }
            }));
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewFav newFav = new frmNewFav(anaform.defaultFolderName, "korot://folder", this);
            newFav.ShowDialog();
        }

        private void newFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewFav newFav = new frmNewFav("", "", this);
            newFav.ShowDialog();
        }

        private void RecursiveNewWindow(Folder folder, ToolStripMenuItem item, bool isIncognito)
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

        private void openİnNewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoriteInNewWindow(false);
        }

        private void openİnNewIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoriteInNewWindow(true);
        }

        private bool isLeftPressed, isRightPressed = false;

        private void frmCEF_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.ThemeChangeForm.Remove(this);
            Settings.AllForms.Remove(this);
            if (hammenu != null)
            {
                Settings.ThemeChangeForm.Remove(hammenu);
                Settings.AllForms.Remove(hammenu);
                hammenu.Close();
                Controls.Remove(hammenu);
                hammenu.Dispose();
            }

            if (profmenu != null)
            {
                Settings.ThemeChangeForm.Remove(profmenu);
                Settings.AllForms.Remove(profmenu);
                profmenu.Close();
                Controls.Remove(profmenu);
                profmenu.Dispose();
            }

            if (incognitomenu != null)
            {
                Settings.ThemeChangeForm.Remove(incognitomenu);
                Settings.AllForms.Remove(incognitomenu);
                incognitomenu.Close();
                Controls.Remove(incognitomenu);
                incognitomenu.Dispose();
            }

            if (privmenu != null)
            {
                Settings.ThemeChangeForm.Remove(privmenu);
                Settings.AllForms.Remove(privmenu);
                privmenu.Close();
                Controls.Remove(privmenu);
                privmenu.Dispose();
            }

            if (setmenu != null)
            {
                Settings.ThemeChangeForm.Remove(setmenu);
                Settings.AllForms.Remove(setmenu);
                setmenu.Close();
                tpSettings.Controls.Remove(setmenu);
                setmenu.Dispose();
            }
            resetPage();
            closing = true;
        }

        public void redirectTo(string url, string title)
        {
            SessionSystem.SkipAdd = bypassThisDeletion;
            bypassThisDeletion = false;
            SessionSystem.Add(url, title);
        }

        public bool bypassThisDeletion = false;
        public bool indexChanged = false;

        private void cmsBack_Opening(object sender, CancelEventArgs e)
        {
            cmsBack.Items.Clear();
            Session[] prev = SessionSystem.Before();
            if (prev.Length > 0)
            {
                for (int i = 0; i < prev.Length; i++)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem
                    {
                        Text = prev[i].Title,
                        ShortcutKeyDisplayString = i.ToString(),
                        Tag = prev[i].Url,
                        ShowShortcutKeys = false
                    };
                    item.Click += backforwardItemClick;
                    cmsBack.Items.Add(item);
                }
            }
            if (cmsBack.Items.Count == 0)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    Text = anaform.empty,
                    Enabled = false
                };
                cmsBack.Items.Add(item);
            }
            cmsBack.Items.Add(tsSepBack);
            cmsBack.Items.Add(tsBackHistory);
        }

        private void backforwardItemClick(object sender, EventArgs e)
        {
            int switchTo = Convert.ToInt32(((ToolStripMenuItem)sender).ShortcutKeyDisplayString);
            SessionSystem.SelectedIndex = switchTo;
            SessionSystem.SelectedSession = SessionSystem.Sessions[switchTo];
            chromiumWebBrowser1.Load(SessionSystem.SelectedSession.Url);
        }

        private void cmsForward_Opening(object sender, CancelEventArgs e)
        {
            cmsForward.Items.Clear();
            Session[] prev = SessionSystem.After();
            if (prev.Length > 0)
            {
                for (int i = 0; i < prev.Length; i++)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem
                    {
                        Text = prev[i].Title,
                        ShortcutKeyDisplayString = i.ToString(),
                        Tag = prev[i].Url,
                        ShowShortcutKeys = false
                    };
                    item.Click += backforwardItemClick;
                    cmsForward.Items.Add(item);
                }
            }
            if (cmsForward.Items.Count == 0)
            {
                ToolStripMenuItem item = new ToolStripMenuItem
                {
                    Text = anaform.empty,
                    Enabled = false
                };
                cmsForward.Items.Add(item);
            }
            cmsForward.Items.Add(tsSepForward);
            cmsForward.Items.Add(tsForwardHistory);
        }

        public void SwitchToCollections()
        {
            if (anaform.settingTab != null)
            {
                anaform.SelectedTab = anaform.settingTab;
            }
            else
            {
                anaform.settingTab = ParentTab;
                if (setmenu is null)
                {
                    setmenu = new frmSettings(Settings, this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true,
                        ShowInTaskbar = false,
                        Size = tpSettings.Size,
                    };
                    tpSettings.Controls.Add(setmenu);
                    setmenu.Show(); Settings.AllForms.Add(setmenu);
                }
                allowSwitching = true;
                setmenu.SwitchCollections();
                tabControl1.SelectedTab = tpSettings;
            }
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            lbStatus.Visible = !string.IsNullOrWhiteSpace(lbStatus.Text);
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