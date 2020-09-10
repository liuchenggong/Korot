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
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
        private string loaduri = null;
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
        private frmMain _anaform;
        private frmMain _parentform => ((frmMain)ParentTabs);
        public frmMain anaform { 
            get
            {
                if (_parentform is null)
                {
                    return _anaform;
                }else { return _parentform; }
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
            Updater();
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
            timer1.Start();
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
            pbForeColor.BackColor = Settings.Theme.ForeColor;
            pbOverlay.BackColor = Settings.Theme.OverlayColor;
            RefreshLangList();
            refreshThemeList();
            ChangeTheme();
            lbVersion.Text = Application.ProductVersion.ToString() + " " + "[" + VersionInfo.CodeName + "]" + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)");
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

        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot", listBox2.SelectedItem.ToString() + Environment.NewLine + anaform.ThemeMessage, new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true }) { StartPosition = FormStartPosition.CenterParent, Yes = anaform.Yes, No = anaform.No, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + listBox2.SelectedItem.ToString());
                    comboBox1.Text = listBox2.SelectedItem.ToString().Replace(".ktf", "");
                }
            }
        }
        #region "Translate"


        public void LoadLangFromFile(string fileLocation)
        {
            if (Settings.LanguageSystem.LangFile != fileLocation) { Settings.LanguageSystem.ReadFromFile(fileLocation, true); }
            anaform.Reload = Settings.LanguageSystem.GetItemText("Reload");
            anaform.soundFiles = Settings.LanguageSystem.GetItemText("SoundFiles");
            lbDefaultNotifSound.Text = Settings.LanguageSystem.GetItemText("UseDefaultSound");
            lbForeColor.Text = Settings.LanguageSystem.GetItemText("ForeColor");
            lbAutoSelect.Text = Settings.LanguageSystem.GetItemText("AutoForeColor");
            lbNinja.Text = Settings.LanguageSystem.GetItemText("NinjaMode");
            btThemeWizard.Text = Settings.LanguageSystem.GetItemText("ThemeWizardButton");
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
            btBlocked.Text = Settings.LanguageSystem.GetItemText("BlockMenuButton");
            tpBlock.Text = Settings.LanguageSystem.GetItemText("BlockMenuTitle");
            lbBlockedSites.Text = Settings.LanguageSystem.GetItemText("BlockMenuTitle");
            anaform.ChangePicInfo = Settings.LanguageSystem.GetItemText("ChangePicInfo");
            anaform.ResetImage = Settings.LanguageSystem.GetItemText("ResetImage");
            anaform.SelectNewImage = Settings.LanguageSystem.GetItemText("SelectNewImage");
            anaform.MuteThisTab = Settings.LanguageSystem.GetItemText("MuteThisTab");
            anaform.ChangePic = Settings.LanguageSystem.GetItemText("ChangePic");
            anaform.ProfileNameTemp = Settings.LanguageSystem.GetItemText("ProfileNameTemp");
            anaform.UnmuteThisTab = Settings.LanguageSystem.GetItemText("UnmuteThisTab");
            anaform.allowCookie = Settings.LanguageSystem.GetItemText("AllowCookie");
            anaform.NewTabEdit = Settings.LanguageSystem.GetItemText("NewTabEdit");
            tpNewTab.Text = Settings.LanguageSystem.GetItemText("NewTabEditorTitle");
            lbNewTabTitle.Text = Settings.LanguageSystem.GetItemText("NewTabEditorTitle");
            lbNTTitle.Text = Settings.LanguageSystem.GetItemText("NewTabEditTitle");
            lbNTUrl.Text = Settings.LanguageSystem.GetItemText("NewTabEditUrl");
            btClear.Text = Settings.LanguageSystem.GetItemText("NewTabEditClear");
            btNewTab.Text = Settings.LanguageSystem.GetItemText("NewTabEditButton");
            anaform.Clear = Settings.LanguageSystem.GetItemText("Clear");
            anaform.RemoveSelected = Settings.LanguageSystem.GetItemText("RemoveSelected");
            anaform.ImportProfile = Settings.LanguageSystem.GetItemText("ImportProfile");
            anaform.importProfileInfo = Settings.LanguageSystem.GetItemText("ImportProfileInfo");
            anaform.ExportProfile = Settings.LanguageSystem.GetItemText("ExportProfile");
            anaform.exportProfileInfo = Settings.LanguageSystem.GetItemText("ExportProfileInfo");
            anaform.ProfileFileInfo = Settings.LanguageSystem.GetItemText("ProfileFileInfo");
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
            lbCollections.Text = Settings.LanguageSystem.GetItemText("Collections");
            anaform.Collections = Settings.LanguageSystem.GetItemText("Collections");
            tpCert.Text = Settings.LanguageSystem.GetItemText("CertificateError");
            tpAbout.Text = Settings.LanguageSystem.GetItemText("About");
            anaform.AboutText = Settings.LanguageSystem.GetItemText("About");
            tpSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
            anaform.SettingsText = Settings.LanguageSystem.GetItemText("Settings");
            tpSite.Text = Settings.LanguageSystem.GetItemText("SiteSettings");
            tpCollection.Text = Settings.LanguageSystem.GetItemText("Collections");
            tpDownload.Text = Settings.LanguageSystem.GetItemText("Downloads");
            anaform.DownloadsText = Settings.LanguageSystem.GetItemText("Downloads");
            tpHistory.Text = Settings.LanguageSystem.GetItemText("History");
            anaform.HistoryText = Settings.LanguageSystem.GetItemText("History");
            tpTheme.Text = Settings.LanguageSystem.GetItemText("Themes");
            anaform.ThemesText = Settings.LanguageSystem.GetItemText("Themes");
            lbautoRestore.Text = Settings.LanguageSystem.GetItemText("RestoreOldSessions");
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
            showNewTabPageToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ShowNewTabPage");
            showHomepageToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ShowHomepage");
            showAWebsiteToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("GoToURL");
            lbDownloadFolder.Text = Settings.LanguageSystem.GetItemText("DownloadToFolder");
            lbAutoDownload.Text = Settings.LanguageSystem.GetItemText("Auto-downloadFolder");
            lbAtStartup.Text = Settings.LanguageSystem.GetItemText("AtStartup");
            btReset.Text = Settings.LanguageSystem.GetItemText("ResetKorotButton");
            anaform.resetConfirm = Settings.LanguageSystem.GetItemText("ResetKorotInfo");
            lbSiteSettings.Text = Settings.LanguageSystem.GetItemText("SiteSettings");
            btCookie.Text = Settings.LanguageSystem.GetItemText("SiteSettingsButton");
            anaform.IncognitoModeTitle = Settings.LanguageSystem.GetItemText("IncognitoMode");
            anaform.IncognitoModeInfo = Settings.LanguageSystem.GetItemText("IncognitoModeInfo");
            anaform.LearnMore = Settings.LanguageSystem.GetItemText("ClickToLearnMore");
            lbLastProxy.Text = Settings.LanguageSystem.GetItemText("RememberLastProxy");
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
            lbBackImageStyle.Text = Settings.LanguageSystem.GetItemText("BackgroundImageLayout");
            anaform.imageFiles = Settings.LanguageSystem.GetItemText("ImageFiles");
            anaform.allFiles = Settings.LanguageSystem.GetItemText("AllFiles");
            anaform.selectBackImage = Settings.LanguageSystem.GetItemText("SelectBackgroundImage");
            colorToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("UseBackgroundColor");
            anaform.usingBC = Settings.LanguageSystem.GetItemText("UsingBackgroundColor");
            ımageFromURLToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ImageFromBase64");
            ımageFromLocalFileToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ImageFromFile");
            lbDNT.Text = Settings.LanguageSystem.GetItemText("EnableDoNotTrack");
            lbFlash.Text = Settings.LanguageSystem.GetItemText("EnableFlash");
            lbFlashInfo.Text = Settings.LanguageSystem.GetItemText("FlashInfo");
            llLicenses.Text = Settings.LanguageSystem.GetItemText("LicensesSpecialThanks");
            lbSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
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
            btUpdater.Text = Settings.LanguageSystem.GetItemText("CheckForUpdates");
            btInstall.Text = Settings.LanguageSystem.GetItemText("InstallUpdate");
            anaform.checking = Settings.LanguageSystem.GetItemText("CheckingForUpdates");
            anaform.uptodate = Settings.LanguageSystem.GetItemText("UpToDate");
            anaform.installStatus = Settings.LanguageSystem.GetItemText("UpdatingMessage");
            anaform.StatusType = Settings.LanguageSystem.GetItemText("DownloadProgress");
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
            anaform.updateavailable = Settings.LanguageSystem.GetItemText("UpdateAvailable"); ;
            anaform.privatemode = Settings.LanguageSystem.GetItemText("Incognito");
            anaform.updateTitle = Settings.LanguageSystem.GetItemText("KorotUpdate");
            anaform.updateMessage = Settings.LanguageSystem.GetItemText("KorotUpdateAvailable");
            anaform.updateError = Settings.LanguageSystem.GetItemText("KorotUpdateError");
            anaform.NewTabtitle = Settings.LanguageSystem.GetItemText("NewTab");
            anaform.customSearchNote = Settings.LanguageSystem.GetItemText("SearchEngineInfo");
            anaform.customSearchMessage = Settings.LanguageSystem.GetItemText("SearchengineTitle");
            lbBackImage.Text = Settings.LanguageSystem.GetItemText("BackgroundStyle");
            anaform.newWindow = Settings.LanguageSystem.GetItemText("NewWindow");
            anaform.newincognitoWindow = Settings.LanguageSystem.GetItemText("NewIncognitoWindow");
            lbDownloads.Text = Settings.LanguageSystem.GetItemText("Downloads");
            lbHomepage.Text = Settings.LanguageSystem.GetItemText("HomePage");
            anaform.SearchOnPage = Settings.LanguageSystem.GetItemText("SearchOnThisPage");
            lbTheme.Text = Settings.LanguageSystem.GetItemText("Themes");
            customToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("Custom");
            anaform.settingstitle = Settings.LanguageSystem.GetItemText("Settings");
            lbHistory.Text = Settings.LanguageSystem.GetItemText("History");
            lbAbout.Text = Settings.LanguageSystem.GetItemText("About");
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
            lbBackColor.Text = Settings.LanguageSystem.GetItemText("BackgroundColor");
            anaform.enterAValidUrl = Settings.LanguageSystem.GetItemText("EnterAValidURL");
            lbOveralColor.Text = Settings.LanguageSystem.GetItemText("OverlayColor");
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
            lbOpen.Text = Settings.LanguageSystem.GetItemText("OpenFilesAfterDownload");
            anaform.open = Settings.LanguageSystem.GetItemText("Open");
            anaform.openLinkInNewTab = Settings.LanguageSystem.GetItemText("OpenLinkInNewTab");
            removeSelectedTSMI.Text = Settings.LanguageSystem.GetItemText("RemoveSelected");
            clearTSMI.Text = Settings.LanguageSystem.GetItemText("Clear");
            anaform.OpenInNewTab = Settings.LanguageSystem.GetItemText("OpenInNewTab");
            anaform.OpenFile = Settings.LanguageSystem.GetItemText("OpenFile");
            anaform.OpenFileInExplorert = Settings.LanguageSystem.GetItemText("OpenFolderContainingThisFile");
            anaform.ResetToDefaultProxy = Settings.LanguageSystem.GetItemText("ResetToFefaultProxySetting");
            lbThemeName.Text = Settings.LanguageSystem.GetItemText("ThemeName");
            anaform.Yes = Settings.LanguageSystem.GetItemText("Yes");
            anaform.No = Settings.LanguageSystem.GetItemText("No");
            anaform.OK = Settings.LanguageSystem.GetItemText("OK");
            anaform.Cancel = Settings.LanguageSystem.GetItemText("Cancel");
            btCertError.Text = Settings.LanguageSystem.GetItemText("Save");
            lbThemes.Text = Settings.LanguageSystem.GetItemText("ThemeList");
            anaform.SearchOnWeb = Settings.LanguageSystem.GetItemText("AddressBar2");
            anaform.goTotxt = Settings.LanguageSystem.GetItemText("AddressBar1");
            anaform.newProfileInfo = Settings.LanguageSystem.GetItemText("EnterAProfileName");
            lbSearchEngine.Text = Settings.LanguageSystem.GetItemText("SearchEngine");
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
            label21.Text = anaform.aboutInfo.Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + ((!(string.IsNullOrWhiteSpace(Settings.Theme.Author) && string.IsNullOrWhiteSpace(Settings.Theme.Name))) ? Settings.LanguageSystem.GetItemText("AboutInfoTheme").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Settings.Theme.Author) ? anaform.anon : Settings.Theme.Author).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Settings.Theme.Name) ? anaform.noname : Settings.Theme.Name) : "");
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
            HTAlt.WinForms.HTInputBox inputb = new HTAlt.WinForms.HTInputBox(anaform.customSearchNote, anaform.customSearchMessage, Settings.SearchEngine) { Icon = Icon, SetToDefault = anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor };
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (HTAlt.Tools.ValidUrl(inputb.TextValue, new string[] { "http", "https", "about", "ftp", "smtp", "pop", "korot" }) && !inputb.TextValue.StartsWith("korot://") && !inputb.TextValue.StartsWith("file://") && !inputb.TextValue.StartsWith("about"))
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
                Color = Settings.Theme.BackColor,
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbBack.BackColor = colorpicker.Color;
                Settings.Theme.BackColor = colorpicker.Color;
                Settings.JustChangedTheme(); ChangeTheme(true);
            }

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                Color = Settings.Theme.OverlayColor,
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbOverlay.BackColor = colorpicker.Color;
                Settings.Theme.OverlayColor = colorpicker.Color;
                Settings.JustChangedTheme(); ChangeTheme(true);
            }
        }

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

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Theme.BackgroundStyle = "BACKCOLOR";
            textBox4.Text = anaform.usingBC;
            colorToolStripMenuItem.Checked = true;
            Settings.JustChangedTheme(); ChangeTheme(true);
        }
        private void FromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox inputbox = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                            anaform.enterAValidCode,
                                                                                            "")
            { Icon = Icon, SetToDefault = anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor };
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                Settings.Theme.BackgroundStyle = inputbox.TextValue + ";";
                textBox4.Text = Settings.Theme.BackgroundStyle;
                colorToolStripMenuItem.Checked = false;
                Settings.JustChangedTheme(); ChangeTheme(true);
            }

        }

        private void FromLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog
            {
                Filter = anaform.imageFiles + "|*.jpg;*.png;*.bmp;*.jpeg;*.jfif;*.gif;*.apng;*.ico;*.svg;*.webp|" + anaform.allFiles + "|*.*",
                Title = anaform.selectBackImage,
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
                    Settings.JustChangedTheme(); ChangeTheme(true);
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
        private string CheckUrl = "https://raw.githubusercontent.com/Haltroy/Korot/master/Korot.htupdate";

        private void btUpdater_Click(object sender, EventArgs e)
        {
            if (UpdateWebC.IsBusy) { UpdateWebC.CancelAsync(); }
            UpdateWebC.DownloadStringAsync(new Uri(CheckUrl));
            updateProgress = 0;
        }
        private void Label2_Click(object sender, EventArgs e)
        {
            NewTab("https://haltroy.com/Korot.html");
        }

        public void LoadTheme(string ThemeFile)
        {
            Settings.Theme.LoadFromFile(ThemeFile);
            Settings.JustChangedTheme(); ChangeTheme(true);
        }
        public void refreshThemeList()
        {
            int savedValue = listBox2.SelectedIndex;
            int scroll = listBox2.TopIndex;
            listBox2.Items.Clear();
            string[] array = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\");
            for (int i = 0; i < array.Length; i++)
            {
                string x = array[i];
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
            UpdateWebC.DownloadStringAsync(new Uri(CheckUrl));
            updateProgress = 0;
        }

        private bool alreadyCheckedForUpdatesOnce = false;
        private void updater_checking(object sender, DownloadProgressChangedEventArgs e)
        {
            lbUpdateStatus.Text = anaform.checking;
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
                lbUpdateStatus.Text = anaform.updateError;
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
                lbUpdateStatus.Text = anaform.updateavailable;
                btUpdater.Visible = true;
                btInstall.Visible = true;
            }
            else
            {
                alreadyCheckedForUpdatesOnce = true;
                updateProgress = 2;
                lbUpdateStatus.Text = anaform.updateavailable;
                btInstall.Visible = true;
                btUpdater.Visible = true;
                HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox(
                    anaform.updateTitle,
                    anaform.updateMessage,
                    new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true })
                { StartPosition = FormStartPosition.CenterParent, Yes = anaform.Yes, No = anaform.No, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
                DialogResult diagres = mesaj.ShowDialog();
                if (diagres == DialogResult.Yes)
                {
                    if (Application.OpenForms.OfType<frmUpdate>().Count() < 1)
                    {
                        Process.Start(Application.ExecutablePath, "-update");
                    }
                    else
                    {
                        foreach (frmUpdate x in Application.OpenForms)
                        {
                            x.Focus();
                        }
                    }
                }
                Settings.DismissUpdate = true;
            }
        }
        private void UpdateResult(string info)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(info);
            KorotVersion Newest = new KorotVersion(doc.FirstChild.NextSibling.OuterXml);
            KorotVersion Current = new KorotVersion();
            bool isUpToDate = Current.WhicIsNew(Newest, Environment.Is64BitProcess ? "amd64" : "i86") == Current;
            if (!isUpToDate)
            {
                updateAvailable();
            }
            else
            {
                btUpdater.Visible = true;
                btInstall.Visible = false;
                updateProgress = 1;
                lbUpdateStatus.Text = anaform.uptodate;

            }
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
                }else { return null; }
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
                if(!(incognitomenu is null)){ incognitomenu.Hide(); }
                if(!(profmenu is null)){ profmenu.Hide(); }
                if(!(hammenu is null)){ hammenu.Hide(); }
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
            switch (new Random().Next(0, 10000))
            {
                case 6:
                    lbKorot.Text = "Another Chromium-based web browser";
                    break;
                case 17:
                    lbKorot.Text = "StoneBrowser";
                    break;
                case 45:
                    lbKorot.Text = "null";
                    break;
                case 71:
                    lbKorot.Text = "web browser made by retarded";
                    break;
                case 3:
                    lbKorot.Text = "web browser designed to lag and eat ram";
                    break;
                case 9:
                    lbKorot.Text = "korot";
                    break;
                case 35:
                    lbKorot.Text = "StoneHomepage";
                    break;
                case 48:
                    lbKorot.Text = "ZStone";
                    break;
                case 7:
                    lbKorot.Text = (new Random().Next(0, int.MaxValue) % 2 == 0 ? "Pell" : "Kolme") + " Browser";
                    break;
                case 33:
                    lbKorot.Text = new Random().Next(0, int.MaxValue) % 2 == 0 ? "Webtroy" : "Ninova";
                    break;
                default:
                    lbKorot.Text = "Korot";
                    break;
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
                //chromiumWebBrowser1.Back();
            }
            else if (tabControl1.SelectedTab == tpCert) //Certificate Error Menu
            {
                bypassThisDeletion = true;
                SessionSystem.GoBack(chromiumWebBrowser1);
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
        List<string> AlreadyValidUrl = new List<string>();
        List<string> AlreadyNotValidUrl = new List<string>();
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
                    if(e.Frame.IsMain)
                    {
                        chromiumWebBrowser1.Load(e.FailedUrl);
                    }else
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
                pbOverlay.BackColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                pbProgress.BackColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsDownload.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsDoNotTrack.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsFav.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsOpen.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
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
                pbBack.BackColor = Settings.Theme.BackColor;
                cmsFavorite.BackColor = Settings.Theme.BackColor;
                cmsFavorite.ForeColor = ForeColor;
                button12.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.collection_w : Properties.Resources.collection);
                pbStore.Image = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.store_w : Properties.Resources.store);
                btLangStore.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.store_w : Properties.Resources.store);
                btlangFolder.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.extfolder_w : Properties.Resources.extfolder);
                btClose6.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose2.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose4.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose7.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose5.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose8.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose9.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose3.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                btClose10.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                lbSettings.BackColor = Color.Transparent;
                lbSettings.ForeColor = ForeColor;

                pbPrivacy.BackColor = backcolor2;
                pBlock.BackColor = backcolor2;
                pBlock.ForeColor = ForeColor;
                tbAddress.BackColor = backcolor2;
                pbIncognito.BackColor = backcolor2;
                fromHour.BackColor = backcolor2;
                fromHour.ForeColor = ForeColor;
                fromMin.BackColor = backcolor2;
                fromMin.ForeColor = ForeColor;
                toHour.BackColor = backcolor2;
                toHour.ForeColor = ForeColor;
                toMin.BackColor = backcolor2;
                toMin.ForeColor = ForeColor;
                cmsSearchEngine.BackColor = Settings.Theme.BackColor;
                listBox2.ForeColor = ForeColor;
                comboBox1.ForeColor = ForeColor;
                tbHomepage.ForeColor = ForeColor;
                tbSearchEngine.ForeColor = ForeColor;
                btCertError.ForeColor = ForeColor;
                btNewTab.BackColor = backcolor2;
                btNewTab.ForeColor = ForeColor;
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
                tbSoundLoc.BackColor = backcolor2;
                tbSoundLoc.ForeColor = ForeColor;
                hsDefaultSound.BackColor = Settings.Theme.BackColor;
                hsDefaultSound.ButtonColor = rbc2;
                hsDefaultSound.ButtonHoverColor = rbc3;
                hsDefaultSound.ButtonPressedColor = rbc4;
                btOpenSound.BackColor = backcolor3;
                btOpenSound.ForeColor = ForeColor;
                hsAutoRestore.BackColor = Settings.Theme.BackColor;
                hsAutoRestore.ButtonColor = rbc2;
                hsAutoRestore.ButtonHoverColor = rbc3;
                hsAutoRestore.ButtonPressedColor = rbc4;
                hsAutoForeColor.BackColor = Settings.Theme.BackColor;
                hsAutoForeColor.ButtonColor = rbc2;
                hsAutoForeColor.ButtonHoverColor = rbc3;
                hsAutoForeColor.ButtonPressedColor = rbc4;
                hsNinja.BackColor = Settings.Theme.BackColor;
                hsNinja.ButtonColor = rbc2;
                hsNinja.ButtonHoverColor = rbc3;
                hsNinja.ButtonPressedColor = rbc4;
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
                cmsSearchEngine.ForeColor = ForeColor;
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
                cmsStartup.ForeColor = ForeColor;
                tbFolder.ForeColor = ForeColor;
                tbStartup.ForeColor = ForeColor;
                btReset.BackColor = backcolor2;
                btDownloadFolder.BackColor = backcolor2;
                button12.BackColor = backcolor2;
                tbSearchEngine.BackColor = backcolor2;
                btNotification.BackColor = backcolor2;
                btNotification.ForeColor = ForeColor;
                panel1.BackColor = backcolor2;
                panel1.ForeColor = ForeColor;
                btCertError.BackColor = backcolor2;
                tbHomepage.BackColor = backcolor2;
                tbSearchEngine.BackColor = backcolor2;
                flpLayout.BackColor = Settings.Theme.BackColor;
                flpLayout.ForeColor = ForeColor;
                flpNewTab.BackColor = Settings.Theme.BackColor;
                flpNewTab.ForeColor = ForeColor;
                flpClose.BackColor = Settings.Theme.BackColor;
                flpClose.ForeColor = ForeColor;
                lbStatus.BackColor = Settings.Theme.BackColor;
                BackColor = Settings.Theme.BackColor;
                cbLang.BackColor = Settings.Theme.BackColor;
                pbIncognito.Image = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.inctab_w : Properties.Resources.inctab);
                tbAddress.ForeColor = ForeColor;
                ForeColor = ForeColor;
                tbTitle.BackColor = backcolor2;
                tbTitle.ForeColor = ForeColor;
                tbUrl.BackColor = backcolor2;
                tbUrl.ForeColor = ForeColor;
                lbStatus.ForeColor = ForeColor;
                cbLang.ForeColor = ForeColor;
                textBox4.ForeColor = ForeColor;
                if (isPageFavorited(chromiumWebBrowser1.Address)) { btFav.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.star_on_w : Properties.Resources.star_on); } else { btFav.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.star : Properties.Resources.star_w); }
                mFavorites.ForeColor = ForeColor;
                if (!noProfilePic) { btProfile.ButtonImage = Settings.NinjaMode ? null : profilePic; } else { btProfile.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.profiles : Properties.Resources.profiles_w); }
                btBack.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                btRefresh.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.refresh : Properties.Resources.refresh_w);
                btNext.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w);
                btNotifBack.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                btBlockBack.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                btNewTabBack.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                btCookieBack.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                btHome.ButtonImage = Settings.NinjaMode ? null : (isbright ? Properties.Resources.home : Properties.Resources.home_w);
                btHamburger.ButtonImage = Settings.NinjaMode ? null : (isbright ? (anaform is null ? Properties.Resources.hamburger : (anaform.newDownload ? Properties.Resources.hamburger_i : Properties.Resources.hamburger)) : (anaform is null ? Properties.Resources.hamburger_w : (anaform.newDownload ? Properties.Resources.hamburger_i_w : Properties.Resources.hamburger_w)));
                L0.BackColor = backcolor2;
                L0.ForeColor = ForeColor;
                L1.BackColor = backcolor2;
                L1.ForeColor = ForeColor;
                L2.BackColor = backcolor2;
                L2.ForeColor = ForeColor;
                L3.BackColor = backcolor2;
                L3.ForeColor = ForeColor;
                L4.BackColor = backcolor2;
                L4.ForeColor = ForeColor;
                L5.BackColor = backcolor2;
                L5.ForeColor = ForeColor;
                L6.BackColor = backcolor2;
                L6.ForeColor = ForeColor;
                L7.BackColor = backcolor2;
                L7.ForeColor = ForeColor;
                L8.BackColor = backcolor2;
                L8.ForeColor = ForeColor;
                L9.BackColor = backcolor2;
                L9.ForeColor = ForeColor;
                btClear.BackColor = backcolor2;
                btClear.ForeColor = ForeColor;
                tbAddress.BackColor = backcolor2;
                textBox4.BackColor = backcolor2;
                mFavorites.BackColor = Settings.Theme.BackColor;
                cmsBStyle.BackColor = Settings.Theme.BackColor;
                cmsBStyle.ForeColor = ForeColor;
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
            btHamburger.ButtonImage = HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? (anaform is null ? Properties.Resources.hamburger : (anaform.newDownload ? Properties.Resources.hamburger_i : Properties.Resources.hamburger)) : (anaform is null ? Properties.Resources.hamburger_w : (anaform.newDownload ? Properties.Resources.hamburger_i_w : Properties.Resources.hamburger_w));
            comboBox1.Text = !onThemeName ? (Settings.Theme.LoadedDefaults ? "((default))" : Settings.Theme.Name) : comboBox1.Text;
            Task.Run(() => getZoomLevel());
        }
        
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
                execTimer50Events();
            }

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
            RefreshTranslation();
            Text = tabControl1.SelectedTab.Text;
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
            pbForeColor.Location = new Point(lbForeColor.Location.X + lbForeColor.Width, pbForeColor.Location.Y);
            lbAutoSelect.Location = new Point(pbForeColor.Location.X + pbForeColor.Width + 10 , lbAutoSelect.Location.Y);
            hsDefaultSound.Location = new Point(lbDefaultNotifSound.Location.X + lbDefaultNotifSound.Width, hsDefaultSound.Location.Y);
            hsAutoForeColor.Location = new Point(lbAutoSelect.Location.X + lbAutoSelect.Width, hsAutoForeColor.Location.Y);
            hsNinja.Location = new Point(lbNinja.Location.X + lbNinja.Width, hsNinja.Location.Y);
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
            label21.Text = anaform.aboutInfo.Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + ((!(string.IsNullOrWhiteSpace(Settings.Theme.Author) && string.IsNullOrWhiteSpace(Settings.Theme.Name))) ? Settings.LanguageSystem.GetItemText("AboutInfoTheme").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Settings.Theme.Author) ? anaform.anon : Settings.Theme.Author).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Settings.Theme.Name) ? anaform.noname : Settings.Theme.Name) : "");
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
            hsDefaultSound.Checked = Settings.UseDefaultSound;
            tbSoundLoc.Enabled = !hsDefaultSound.Checked;
            tbSoundLoc.Text = Settings.SoundLocation;
            btOpenSound.Enabled = !hsDefaultSound.Checked;
            colorToolStripMenuItem.Checked = Settings.Theme.BackgroundStyle == "BACKCOLOR" ? true : false;
            textBox4.Text = Settings.Theme.BackgroundStyle == "BACKCOLOR" ? anaform.usingBC : Settings.Theme.BackgroundStyle;
            lbCertErrorTitle.Text = anaform.CertErrorPageTitle;
            lbCertErrorInfo.Text = anaform.CertErrorPageMessage;
            btCertError.Text = anaform.CertErrorPageButton;
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
            hsAutoForeColor.Checked = Settings.Theme.AutoForeColor;
            hsNinja.Checked = Settings.NinjaMode;
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
            if(incognitomenu is null)
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
            if (anaform.historyTab != null)
            {
                anaform.SelectedTab = anaform.historyTab;
            }
            else
            {
                if (hisman is null)
                {
                    hisman = new frmHistory(this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true
                    };
                    pHisMan.Controls.Add(hisman);
                    Settings.AllForms.Add(hisman);
                }
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
                if (dowman is null)
                {
                    dowman = new frmDownload(this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true
                    };
                    pDowMan.Controls.Add(dowman);
                    Settings.AllForms.Add(dowman);
                }
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
                        if (siteman is null)
                        {
                            siteman = new frmSites(this)
                            {
                                TopLevel = false,
                                FormBorderStyle = FormBorderStyle.None,
                                Dock = DockStyle.Fill,
                                Visible = true
                            };
                            pSite.Controls.Add(siteman);
                            Settings.AllForms.Add(siteman);
                        }
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
                    if (blockmenu is null)
                    {
                        blockmenu = new frmBlock(this)
                        {
                            TopLevel = false,
                            FormBorderStyle = FormBorderStyle.None,
                            Dock = DockStyle.Fill,
                            Visible = true
                        };
                        pBlock.Controls.Add(blockmenu);
                        Settings.AllForms.Add(blockmenu);
                    }
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
            FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = anaform.selectAFolder };
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
            HTAlt.WinForms.HTInputBox inputb = new HTAlt.WinForms.HTInputBox("Korot", anaform.enterAValidUrl, Settings.SearchEngine) { Icon = Icon, SetToDefault = anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor };
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
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot", anaform.resetConfirm,
                                                                                      new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
            { StartPosition = FormStartPosition.CenterParent, Yes = anaform.Yes, No = anaform.No, OK = anaform.OK, Cancel = anaform.Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
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
                this.Controls.Remove(hammenu);
                hammenu.Dispose();
            }
            if (blockmenu != null)
            {
                Settings.ThemeChangeForm.Remove(blockmenu);
                Settings.AllForms.Remove(blockmenu);
                blockmenu.Close();
                pBlock.Controls.Remove(blockmenu);
                blockmenu.Dispose();
            }

            if (profmenu != null)
            {
                Settings.ThemeChangeForm.Remove(profmenu);
                Settings.AllForms.Remove(profmenu);
                profmenu.Close();
                this.Controls.Remove(profmenu);
                profmenu.Dispose();
            }

            if (incognitomenu != null)
            {
                Settings.ThemeChangeForm.Remove(incognitomenu);
                Settings.AllForms.Remove(incognitomenu);
                incognitomenu.Close();
                this.Controls.Remove(incognitomenu);
                incognitomenu.Dispose();
            }

            if (privmenu != null)
            {
                Settings.ThemeChangeForm.Remove(privmenu);
                Settings.AllForms.Remove(privmenu);
                privmenu.Close();
                this.Controls.Remove(privmenu);
                privmenu.Dispose();
            }

            if (siteman != null)
            {
                Settings.ThemeChangeForm.Remove(siteman);
                Settings.AllForms.Remove(siteman);
                siteman.Close();
                pSite.Controls.Remove(siteman);
                siteman.Dispose();
            }


            if (dowman != null)
            {
                Settings.ThemeChangeForm.Remove(dowman);
                Settings.AllForms.Remove(dowman);
                dowman.Close();
                pDowMan.Controls.Remove(dowman);
                dowman.Dispose();
            }

            if (hisman != null)
            {
                Settings.ThemeChangeForm.Remove(hisman);
                Settings.AllForms.Remove(hisman);
                hisman.Close();
                pHisMan.Controls.Remove(hisman);
                hisman.Dispose();
            }

            if (ColMan != null)
            {
                Settings.ThemeChangeForm.Remove(ColMan);
                Settings.AllForms.Remove(ColMan);
                ColMan.Close();
                pColMan.Controls.Remove(ColMan);
                ColMan.Dispose();
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
                for (int  i = 0; i < prev.Length; i++)
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
                if (ColMan is null)
                {
                    ColMan = new frmCollection(this)
                    {
                        TopLevel = false,
                        FormBorderStyle = FormBorderStyle.None,
                        Dock = DockStyle.Fill,
                        Visible = true
                    };
                    pColMan.Controls.Add(ColMan);
                    Settings.AllForms.Add(ColMan);
                }
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
                Settings.JustChangedTheme(); ChangeTheme(true);

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
                Settings.JustChangedTheme(); ChangeTheme(true);

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
                Settings.JustChangedTheme(); ChangeTheme(true);

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
                Settings.JustChangedTheme(); ChangeTheme(true);

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
                Settings.JustChangedTheme(); ChangeTheme(true);

            }
        }

        private void rbBackColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBackColor.Checked)
            {
                rbForeColor.Checked = false;
                rbOverlayColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.BackColor;
                Settings.JustChangedTheme(); ChangeTheme(true);

            }
        }

        private void rbForeColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForeColor.Checked)
            {
                rbBackColor.Checked = false;
                rbOverlayColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.ForeColor;
                Settings.JustChangedTheme(); ChangeTheme(true);

            }
        }

        private void rbOverlayColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOverlayColor.Checked)
            {
                rbForeColor.Checked = false;
                rbBackColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.OverlayColor;
                Settings.JustChangedTheme(); ChangeTheme(true);

            }
        }

        private void rbBackColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBackColor1.Checked)
            {
                rbForeColor1.Checked = false;
                rbOverlayColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.BackColor;
                Settings.JustChangedTheme(); ChangeTheme(true);

            }
        }

        private void rbForeColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForeColor1.Checked)
            {
                rbBackColor1.Checked = false;
                rbOverlayColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.ForeColor;
                Settings.JustChangedTheme(); ChangeTheme(true);

            }
        }

        private void rbOverlayColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOverlayColor1.Checked)
            {
                rbForeColor1.Checked = false;
                rbBackColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.OverlayColor;
                Settings.JustChangedTheme(); ChangeTheme(true);

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

        private void hsFlash_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Flash = hsFlash.Checked;
        }

        private void tpHistory_Enter(object sender, EventArgs e)
        {
            hisman.RefreshList();
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

        private void btNewTab_Click(object sender, EventArgs e)
        {
            EditNewTabItem();
        }

        private void LoadNewTabSites()
        {
            NTRefreshNotDone = true;
            //l0
            bool fs0 = Settings.NewTabSites.FavoritedSite0 != null;
            L0T.Text = fs0 ? Settings.NewTabSites.FavoritedSite0.Name : "...";
            L0U.Text = fs0 ? Settings.NewTabSites.FavoritedSite0.Url : "...";
            //l1
            bool fs1 = Settings.NewTabSites.FavoritedSite1 != null;
            L1T.Text = fs1 ? Settings.NewTabSites.FavoritedSite1.Name : "...";
            L1U.Text = fs1 ? Settings.NewTabSites.FavoritedSite1.Url : "...";
            //l2
            bool fs2 = Settings.NewTabSites.FavoritedSite2 != null;
            L2T.Text = fs2 ? Settings.NewTabSites.FavoritedSite2.Name : "...";
            L2U.Text = fs2 ? Settings.NewTabSites.FavoritedSite2.Url : "...";
            //l3
            bool fs3 = Settings.NewTabSites.FavoritedSite3 != null;
            L3T.Text = fs3 ? Settings.NewTabSites.FavoritedSite3.Name : "...";
            L3U.Text = fs3 ? Settings.NewTabSites.FavoritedSite3.Url : "...";
            //l4
            bool fs4 = Settings.NewTabSites.FavoritedSite4 != null;
            L4T.Text = fs4 ? Settings.NewTabSites.FavoritedSite4.Name : "...";
            L4U.Text = fs4 ? Settings.NewTabSites.FavoritedSite4.Url : "...";
            //l5
            bool fs5 = Settings.NewTabSites.FavoritedSite5 != null;
            L5T.Text = fs5 ? Settings.NewTabSites.FavoritedSite5.Name : "...";
            L5U.Text = fs5 ? Settings.NewTabSites.FavoritedSite5.Url : "...";
            //l6
            bool fs6 = Settings.NewTabSites.FavoritedSite6 != null;
            L6T.Text = fs6 ? Settings.NewTabSites.FavoritedSite6.Name : "...";
            L6U.Text = fs6 ? Settings.NewTabSites.FavoritedSite6.Url : "...";
            //l7
            bool fs7 = Settings.NewTabSites.FavoritedSite7 != null;
            L7T.Text = fs7 ? Settings.NewTabSites.FavoritedSite7.Name : "...";
            L7U.Text = fs7 ? Settings.NewTabSites.FavoritedSite7.Url : "...";
            //l8
            bool fs8 = Settings.NewTabSites.FavoritedSite8 != null;
            L8T.Text = fs8 ? Settings.NewTabSites.FavoritedSite8.Name : "...";
            L8U.Text = fs8 ? Settings.NewTabSites.FavoritedSite8.Url : "...";
            //l9
            bool fs9 = Settings.NewTabSites.FavoritedSite9 != null;
            L9T.Text = fs9 ? Settings.NewTabSites.FavoritedSite9.Name : "...";
            L9U.Text = fs9 ? Settings.NewTabSites.FavoritedSite9.Url : "...";
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
            switch (editL)
            {
                case 0:
                    if (Settings.NewTabSites.FavoritedSite0 == null) { Settings.NewTabSites.FavoritedSite0 = new Site(); }
                    Settings.NewTabSites.FavoritedSite0.Name = tbTitle.Text;
                    break;
                case 1:
                    if (Settings.NewTabSites.FavoritedSite1 == null) { Settings.NewTabSites.FavoritedSite1 = new Site(); }
                    Settings.NewTabSites.FavoritedSite1.Name = tbTitle.Text;
                    break;
                case 2:
                    if (Settings.NewTabSites.FavoritedSite2 == null) { Settings.NewTabSites.FavoritedSite2 = new Site(); }
                    Settings.NewTabSites.FavoritedSite2.Name = tbTitle.Text;
                    break;
                case 3:
                    if (Settings.NewTabSites.FavoritedSite3 == null) { Settings.NewTabSites.FavoritedSite3 = new Site(); }
                    Settings.NewTabSites.FavoritedSite3.Name = tbTitle.Text;
                    break;
                case 4:
                    if (Settings.NewTabSites.FavoritedSite4 == null) { Settings.NewTabSites.FavoritedSite4 = new Site(); }
                    Settings.NewTabSites.FavoritedSite4.Name = tbTitle.Text;
                    break;
                case 5:
                    if (Settings.NewTabSites.FavoritedSite5 == null) { Settings.NewTabSites.FavoritedSite5 = new Site(); }
                    Settings.NewTabSites.FavoritedSite5.Name = tbTitle.Text;
                    break;
                case 6:
                    if (Settings.NewTabSites.FavoritedSite6 == null) { Settings.NewTabSites.FavoritedSite6 = new Site(); }
                    Settings.NewTabSites.FavoritedSite6.Name = tbTitle.Text;
                    break;
                case 7:
                    if (Settings.NewTabSites.FavoritedSite7 == null) { Settings.NewTabSites.FavoritedSite7 = new Site(); }
                    Settings.NewTabSites.FavoritedSite7.Name = tbTitle.Text;
                    break;
                case 8:
                    if (Settings.NewTabSites.FavoritedSite8 == null) { Settings.NewTabSites.FavoritedSite8 = new Site(); }
                    Settings.NewTabSites.FavoritedSite8.Name = tbTitle.Text;
                    break;
                case 9:
                    if (Settings.NewTabSites.FavoritedSite9 == null) { Settings.NewTabSites.FavoritedSite9 = new Site(); }
                    Settings.NewTabSites.FavoritedSite9.Name = tbTitle.Text;
                    break;
            }
            LoadNewTabSites();
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            if (NTRefreshNotDone) { return; }
            switch (editL)
            {
                case 0:
                    if (Settings.NewTabSites.FavoritedSite0 == null) { Settings.NewTabSites.FavoritedSite0 = new Site(); }
                    Settings.NewTabSites.FavoritedSite0.Url = tbUrl.Text;
                    break;
                case 1:
                    if (Settings.NewTabSites.FavoritedSite1 == null) { Settings.NewTabSites.FavoritedSite1 = new Site(); }
                    Settings.NewTabSites.FavoritedSite1.Url = tbUrl.Text;
                    break;
                case 2:
                    if (Settings.NewTabSites.FavoritedSite2 == null) { Settings.NewTabSites.FavoritedSite2 = new Site(); }
                    Settings.NewTabSites.FavoritedSite2.Url = tbUrl.Text;
                    break;
                case 3:
                    if (Settings.NewTabSites.FavoritedSite3 == null) { Settings.NewTabSites.FavoritedSite3 = new Site(); }
                    Settings.NewTabSites.FavoritedSite3.Url = tbUrl.Text;
                    break;
                case 4:
                    if (Settings.NewTabSites.FavoritedSite4 == null) { Settings.NewTabSites.FavoritedSite4 = new Site(); }
                    Settings.NewTabSites.FavoritedSite4.Url = tbUrl.Text;
                    break;
                case 5:
                    if (Settings.NewTabSites.FavoritedSite5 == null) { Settings.NewTabSites.FavoritedSite5 = new Site(); }
                    Settings.NewTabSites.FavoritedSite5.Url = tbUrl.Text;
                    break;
                case 6:
                    if (Settings.NewTabSites.FavoritedSite6 == null) { Settings.NewTabSites.FavoritedSite6 = new Site(); }
                    Settings.NewTabSites.FavoritedSite6.Url = tbUrl.Text;
                    break;
                case 7:
                    if (Settings.NewTabSites.FavoritedSite7 == null) { Settings.NewTabSites.FavoritedSite7 = new Site(); }
                    Settings.NewTabSites.FavoritedSite7.Url = tbUrl.Text;
                    break;
                case 8:
                    if (Settings.NewTabSites.FavoritedSite8 == null) { Settings.NewTabSites.FavoritedSite8 = new Site(); }
                    Settings.NewTabSites.FavoritedSite8.Url = tbUrl.Text;
                    break;
                case 9:
                    if (Settings.NewTabSites.FavoritedSite9 == null) { Settings.NewTabSites.FavoritedSite9 = new Site(); }
                    Settings.NewTabSites.FavoritedSite9.Url = tbUrl.Text;
                    break;
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
            switch (itemid)
            {
                case 0:
                    if (Settings.NewTabSites.FavoritedSite0 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite0.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite0.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 1:
                    if (Settings.NewTabSites.FavoritedSite1 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite1.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite1.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 2:
                    if (Settings.NewTabSites.FavoritedSite2 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite2.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite2.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 3:
                    if (Settings.NewTabSites.FavoritedSite3 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite3.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite3.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 4:
                    if (Settings.NewTabSites.FavoritedSite4 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite4.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite4.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 5:
                    if (Settings.NewTabSites.FavoritedSite5 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite5.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite5.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 6:
                    if (Settings.NewTabSites.FavoritedSite6 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite6.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite6.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 7:
                    if (Settings.NewTabSites.FavoritedSite7 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite7.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite7.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 8:
                    if (Settings.NewTabSites.FavoritedSite8 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite8.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite8.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
                case 9:
                    if (Settings.NewTabSites.FavoritedSite9 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite9.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite9.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btClear.Enabled = true;
                    break;
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

        private void btThemeWizard_Click(object sender, EventArgs e)
        {
            if (!(anaform is null))
            {
                anaform.Invoke(new Action(() =>
                {
                    frmThemeWizard wizard = new frmThemeWizard(Settings);
                    wizard.ShowDialog();
                }));
            }
        }

        private void hsAutoForeColor_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Theme.AutoForeColor = hsAutoForeColor.Checked;
            Settings.Theme.ForeColor = hsAutoForeColor.Checked ? (HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor)) : Settings.Theme.ForeColor;
            Settings.JustChangedTheme(); ChangeTheme(true);
        }

        private void hsNinja_CheckedChanged(object sender, EventArgs e)
        {
            Settings.NinjaMode = hsNinja.Checked;
            Settings.JustChangedTheme(); ChangeTheme(true);
        }

        private void pbForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                Color = Settings.Theme.ForeColor,
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbForeColor.BackColor = colorpicker.Color;
                Settings.Theme.AutoForeColor = false;
                Settings.Theme.ForeColor = colorpicker.Color;
                Settings.JustChangedTheme(); ChangeTheme(true);
            }
        }

        private void btOpenSound_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                InitialDirectory = Settings.SoundLocation,
                FileName = Settings.SoundLocation,
                Multiselect = false,
                Filter = anaform.soundFiles + "|*.mp3;*.wav;*.aac;*.midi|" + anaform.allFiles + "|*.*"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings.SoundLocation = dialog.FileName;
                tbSoundLoc.Text = dialog.FileName;
            }
        }

        private void hsDefaultSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.UseDefaultSound = hsDefaultSound.Checked;
            tbSoundLoc.Enabled = !hsDefaultSound.Checked;
            btOpenSound.Enabled = !hsDefaultSound.Checked;
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
                if (workNode.Attributes["Index"] != null) {
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
        public SessionSystem() : this("") { }
        private List<Session> _Sessions = new List<Session>();
        public string XmlOut()
        {
            string x = "<Session Index=\"" + SelectedIndex + "\" >" + Environment.NewLine;
            for(int i = 0; i < Sessions.Count;i++)
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
        public void MoveTo(int i,ChromiumWebBrowser browser)
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
            }else
            {
                throw new ArgumentOutOfRangeException("\"i\" was bigger than Sessions.Count or smaller than 0. [i=\"" + i + "\" Count=\"" + Sessions.Count + "\"]");
            }
        }
        public void Add(string url,string title)
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
        public bool CanGoBack() => CanGoBack(SelectedSession);
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
        public bool CanGoForward() => CanGoForward(SelectedSession);
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
        public Session[] Before() => Before(SelectedSession);
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
            for(int i =0; i< current; i++)
            {
                fs.Add(Sessions[i]);
            }
            return fs.ToArray();
        }
        public Session[] After() => After(SelectedSession);
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
        public override bool Equals(object obj) => obj is Session session && Url == session.Url;

        public override int GetHashCode() => -1915121810 + EqualityComparer<string>.Default.GetHashCode(Url);

        public Session(string _Url, string _Title)
        {
            Url = _Url;
            Title = _Title;
        }
        public Session() : this("", "") { }
        public Session(string _Url) : this(_Url, _Url) { }

        public string Url { get; set; }
        public string Title { get; set; }
    }
}
