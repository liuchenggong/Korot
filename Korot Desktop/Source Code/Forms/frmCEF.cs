using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmCEF : Form
    {
        TabPage parentTabPage;
        bool isLoading = false;
        string loaduri = null;
        bool _Incognito = false;
        string userName;
        string userCache;
        frmMain anaform;
        public ChromiumWebBrowser chromiumWebBrowser1;
        string defaultproxyaddress;
        public frmCEF(TabPage pranetPage, frmMain rmmain, bool isIncognito, string loadurl, string profileName)
        {
            InitializeComponent();
            parentTabPage = pranetPage;
            loaduri = loadurl;
            anaform = rmmain;
            _Incognito = isIncognito;
            userName = profileName;
            userCache = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profileName + "\\cache\\";
            WebProxy proxy = (WebProxy)WebProxy.GetDefaultProxy();
            Uri resource = new Uri("http://localhost");
            Uri resourceProxy = proxy.GetProxy(resource);
            if (resourceProxy == resource)
            {
                defaultproxyaddress = null;
                Output.WriteLine("[INFO] No proxy detected.Disabling proxies.");
                button6.Enabled = false;
            }
            else
            {
                defaultproxyaddress = resourceProxy.AbsoluteUri.ToString();
                Output.WriteLine("[INFO] Listening on proxy : " + defaultproxyaddress);
                SetProxy(chromiumWebBrowser1, defaultproxyaddress);
            }

            InitializeChromium();
        }
        async private void SetProxy(ChromiumWebBrowser cwb, string Address)
        {
            if (Address == null) { }
            else
            {
                await Cef.UIThreadTaskFactory.StartNew(delegate
                {
                    var rc = cwb.GetBrowser().GetHost().RequestContext;
                    var v = new Dictionary<string, object>();
                    v["mode"] = "fixed_servers";
                    v["server"] = Address;
                    string error;
                    bool success = rc.SetPreference("proxy", v, out error);
                });
            }
        }
        private static ManagementObject GetMngObj(string className)
        {
            var wmi = new ManagementClass(className);

            foreach (var o in wmi.GetInstances())
            {
                var mo = (ManagementObject)o;
                if (mo != null) return mo;
            }

            return null;
        }

        public static string GetOsVer()
        {
            try
            {
                ManagementObject mo = GetMngObj("Win32_OperatingSystem");

                if (null == mo)
                    return string.Empty;

                return mo["Version"] as string;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static int Brightness(System.Drawing.Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.UserAgent = "Mozilla/5.0 ( Windows NT "
                + GetOsVer()
                + "; "
                + Environment.OSVersion.Platform
                + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/"
                + Cef.ChromiumVersion
                + " Safari/537.36 Korot/"
                + Application.ProductVersion.ToString();
            if (_Incognito) { settings.CachePath = null; settings.PersistSessionCookies = false; }
            else { settings.CachePath = userCache; }
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "korot",
                SchemeHandlerFactory = new SchemeHandlerFactory(anaform, this)
            });
            // Initialize cef with the provided settings
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            chromiumWebBrowser1 = new ChromiumWebBrowser(loaduri);
            panel1.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.RequestHandler = new RequestHandlerKorot(anaform, this);
            chromiumWebBrowser1.DisplayHandler = new DisplayHandler(this, anaform);
            chromiumWebBrowser1.LoadingStateChanged += loadingstatechanged;
            chromiumWebBrowser1.TitleChanged += cef_TitleChanged;
            chromiumWebBrowser1.AddressChanged += cef_AddressChanged;
            chromiumWebBrowser1.LoadError += cef_onLoadError;
            chromiumWebBrowser1.KeyDown += tabform_KeyDown;
            chromiumWebBrowser1.MenuHandler = new ContextMenuHandler(this, anaform);
            chromiumWebBrowser1.LifeSpanHandler = new BrowserLifeSpanHandler(this);
            chromiumWebBrowser1.DownloadHandler = new DownloadHandler(this, anaform);
            chromiumWebBrowser1.JsDialogHandler = new JsHandler(anaform);
            chromiumWebBrowser1.DialogHandler = new MyDialogHandler();
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
        }
        public void executeStartupExtensions()
        {
            foreach (String x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\", "*.*", SearchOption.AllDirectories))
            {
                if (x.EndsWith("\\startup.js", StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        Output.WriteLine("[Korot] Script Execute : " + x);
                        chromiumWebBrowser1.EvaluateScriptAsync(File.ReadAllText(x));
                        Output.WriteLine("[Korot] Script Execute Completed: " + x);
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine("[Korot] Script Execute Error : {Script:" + x + ",ErrorMessage:" + ex.Message + "}");
                        continue;
                    }
                }
            }
        }
        public bool certError = false;
        public bool cookieUsage = false;
        public void ChangeStatus(string status) => label2.Text = status;
        public void loadingstatechanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
            {
                certError = false;
                cookieUsage = false;
                pictureBox2.Invoke(new Action(() => pictureBox2.Image = Properties.Resources.lockg));
                this.Invoke(new Action(() => showCertificateErrorsToolStripMenuItem.Tag = null));
                this.Invoke(new Action(() => showCertificateErrorsToolStripMenuItem.Visible = false));
                this.Invoke(new Action(() => safeStatusToolStripMenuItem.Text = anaform.CertificateOKTitle));
                this.Invoke(new Action(() => ınfoToolStripMenuItem.Text = anaform.CertificateOK));
                this.Invoke(new Action(() => cookieInfoToolStripMenuItem.Text = anaform.notUsesCookies));
                if (Brightness(Properties.Settings.Default.BackColor) > 130)
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
                    this.InvokeOnUiThreadIfRequired(() => Korot.Properties.Settings.Default.History += DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + this.Text + ";" + (textBox1.Text) + ";");

                }
                executeStartupExtensions();
                if (Brightness(Properties.Settings.Default.BackColor) > 130)
                {
                    button2.Image = Korot.Properties.Resources.refresh;
                }
                else
                { button2.Image = Korot.Properties.Resources.refresh_w; }
            }
            try
            {
                button1.Invoke(new Action(() => button1.Enabled = e.CanGoBack));
                button3.Invoke(new Action(() => button3.Enabled = e.CanGoForward));
            }
            catch
            {
                try
                {
                    button1.Invoke(new Action(() => button1.Enabled = false));
                    button3.Invoke(new Action(() => button3.Enabled = false));
                }catch { }
            }
            isLoading = e.IsLoading;
        }

        public void NewTab(string url) => anaform.Invoke(new Action(() => anaform.NewTab(url)));
        public void RefreshFavorites()
        {
            mFavorites.Items.Clear();
            string Playlist = Properties.Settings.Default.Favorites;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                ToolStripMenuItem miFavorite = new ToolStripMenuItem();
                miFavorite.Tag = SplittedFase[i].Replace(Environment.NewLine, "");
                i += 1;
                miFavorite.Text = SplittedFase[i].Replace(Environment.NewLine, "");
                i += 1;
                miFavorite.Click += TestToolStripMenuItem_Click;
                mFavorites.Items.Add(miFavorite);
                i += 1;
            }
        }
        private void tabform_Load(object sender, EventArgs e)
        {
            if (_Incognito) { } else { pictureBox1.Visible = false; textBox1.Size = new Size(textBox1.Size.Width + pictureBox1.Size.Width, textBox1.Size.Height); }

            RefreshFavorites();
            LoadProxies();
            LoadExt();
            RefreshProfiles();
            profilenameToolStripMenuItem.Text = userName;
            label3.Text = anaform.SearchOnPage;
            label6.Text = anaform.CaseSensitive;
            showCertificateErrorsToolStripMenuItem.Text = anaform.showCertError;
            chromiumWebBrowser1.Select();
        }

        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about)|(?:about)|(?:file)|(?:korot)|(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s);
        }
        private void button4_Click(object sender, EventArgs e)
        {

            string urlLower = textBox1.Text.ToLower();
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



        private void button1_Click(object sender, EventArgs e) => chromiumWebBrowser1.Back();

        private void button3_Click(object sender, EventArgs e) => chromiumWebBrowser1.Forward();

        private void button2_Click(object sender, EventArgs e)
        {
            if (isLoading)
            {
                chromiumWebBrowser1.Stop();
            }
            else { chromiumWebBrowser1.Reload(); }
        }
        private void cef_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() => textBox1.Text = e.Address);
            if (Properties.Settings.Default.Favorites.Contains(e.Address))
            {
                isLoadedPageFavroited = true;
                button7.Image = Properties.Resources.star_on;
            }
            else
            {
                if (Brightness(Properties.Settings.Default.BackColor) > 130) { button7.Image = Properties.Resources.star; }
                else { button7.Image = Properties.Resources.star_w; }
                isLoadedPageFavroited = false;
            }
            Uri newUri = null;
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
        }


        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            if (e.Title.Length < 101)
            {
                this.InvokeOnUiThreadIfRequired(() => this.Text = e.Title);
            }
            else
            {
                this.InvokeOnUiThreadIfRequired(() => this.Text = e.Title.Substring(0, 100));
            }
            this.Parent.Invoke(new Action(() => this.Parent.Text = this.Text));
        }

        private void button5_Click(object sender, EventArgs e) => chromiumWebBrowser1.Load(Properties.Settings.Default.Homepage);

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button4_Click(null, null);
            }
        }

        private void tabform_KeyDown(object sender, KeyEventArgs e)
        {
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
            else if (e.KeyCode == Keys.F && e.Control)
            {
                panel3.Visible = !panel3.Visible;
                findTextBox.Text = "";
                chromiumWebBrowser1.StopFinding(true);
            }
        }
        private static int GerekiyorsaAzalt(int defaultint, int azaltma) => defaultint > azaltma ? defaultint - 20 : defaultint;
        private static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır) => defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        void ChnageTheme()
        {
            if (Brightness(Properties.Settings.Default.BackColor) > 130) //Light
            {
                findTextBox.BackColor = Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                findTextBox.ForeColor = Color.Black;
                panel3.BackColor = Properties.Settings.Default.BackColor;
                panel3.ForeColor = Color.Black;
                pbProgress.BackColor = Properties.Settings.Default.OverlayColor;
                button9.Image = Properties.Resources.profiles;
                cmsProfiles.BackColor = Properties.Settings.Default.BackColor;
                cmsProfiles.ForeColor = Color.Black;
                foreach (ToolStripMenuItem x in cmsProfiles.Items) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Color.Black; }
                button1.Image = Properties.Resources.leftarrow;
                button2.Image = Properties.Resources.refresh;
                button3.Image = Properties.Resources.rightarrow;
                button4.Image = Properties.Resources.go;
                button5.Image = Properties.Resources.home;
                textBox1.BackColor = Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox1.ForeColor = Color.Black;
                this.BackColor = Properties.Settings.Default.BackColor;
                this.ForeColor = Color.Black;
                label2.BackColor = Properties.Settings.Default.BackColor;
                label2.ForeColor = Color.Black;
                cmsPrivacy.BackColor = Properties.Settings.Default.BackColor;
                cmsPrivacy.ForeColor = Color.Black;
                textBox1.BackColor = Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox1.ForeColor = Color.Black;
                button6.Image = Properties.Resources.prxy;
                contextMenuStrip2.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip2.ForeColor = Color.Black;
                if (isLoadedPageFavroited) { button7.Image = Properties.Resources.star_on; } else { button7.Image = Properties.Resources.star; }
                mFavorites.BackColor = Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                mFavorites.ForeColor = Color.Black;

                button8.Image = Properties.Resources.ext;
                contextMenuStrip1.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip1.ForeColor = Color.Black;
                foreach (ToolStripMenuItem x in contextMenuStrip1.Items)
                {
                    x.BackColor = Properties.Settings.Default.BackColor;
                    x.ForeColor = Color.Black;
                }

            }
            else //Dark
            {
                pbProgress.BackColor = Properties.Settings.Default.OverlayColor;
                contextMenuStrip1.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip1.ForeColor = Color.White;
                foreach (ToolStripMenuItem x in contextMenuStrip1.Items)
                {
                    x.BackColor = Properties.Settings.Default.BackColor;
                    x.ForeColor = Color.White;
                }
                cmsPrivacy.BackColor = Properties.Settings.Default.BackColor;
                cmsPrivacy.ForeColor = Color.White;
                button8.Image = Properties.Resources.ext_w;
                button9.Image = Properties.Resources.profiles_w;
                cmsProfiles.BackColor = Properties.Settings.Default.BackColor;
                cmsProfiles.ForeColor = Color.White;
                foreach (ToolStripMenuItem x in cmsProfiles.Items) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Color.White; }
                findTextBox.BackColor = Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                findTextBox.ForeColor = Color.White;
                panel3.BackColor = Properties.Settings.Default.BackColor;
                panel3.ForeColor = Color.White;
                button1.Image = Properties.Resources.leftarrow_w;
                button2.Image = Properties.Resources.refresh_w;
                button3.Image = Properties.Resources.rightarrow_w;
                button4.Image = Properties.Resources.go_w;
                button5.Image = Properties.Resources.home_w;
                textBox1.BackColor = Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                textBox1.ForeColor = Color.White;
                this.BackColor = Properties.Settings.Default.BackColor;
                this.ForeColor = Color.White;
                label2.BackColor = Properties.Settings.Default.BackColor;
                label2.ForeColor = Color.White;
                button6.Image = Properties.Resources.prxy_w;
                contextMenuStrip2.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip2.ForeColor = Color.White;
                textBox1.BackColor = Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                textBox1.ForeColor = Color.White;

                if (isLoadedPageFavroited) { button7.Image = Properties.Resources.star_on; } else { button7.Image = Properties.Resources.star_w; }
                mFavorites.BackColor = Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                mFavorites.ForeColor = Color.White;

            }
        }
        int websiteprogress;
        public void ChangeProgress(int value)
        {
            websiteprogress = value;
            pbProgress.Width = (this.Width / 100) * websiteprogress;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ChnageTheme();
                this.Parent.Text = this.Text;
            }catch { } //ignored
            try
            {
                if (((TabControl)parentTabPage.Parent).TabPages.Contains(parentTabPage)) { }
                else
                {
                    anaform.Invoke(new Action(() => anaform.RemoveMefromList(this)));
                    this.Close();
                }
            }
            catch { anaform.Invoke(new Action(() => anaform.RemoveMefromList(this))); this.Close(); }
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(((ToolStripMenuItem)sender).Tag.ToString());
        }
        bool isLoadedPageFavroited = false;

        private void Button7_Click(object sender, EventArgs e)
        {
            if (isLoadedPageFavroited)
            {
                Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(chromiumWebBrowser1.Address + ";", "");
                Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(this.Text + ";", "");
                button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
                isLoadedPageFavroited = false;
            }
            else
            {
                Properties.Settings.Default.Favorites += (chromiumWebBrowser1.Address + ";");
                Properties.Settings.Default.Favorites += (this.Text + ";");
                button7.Image = Properties.Resources.star_on;
                isLoadedPageFavroited = true;
            }
            RefreshFavorites();
        }
        void RefreshProfiles()
        {
            switchToToolStripMenuItem.DropDownItems.Clear();
            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"))
            {
                ToolStripMenuItem profileItem = new ToolStripMenuItem();
                profileItem.Text = new DirectoryInfo(x).Name;
                profileItem.Click += ProfilesToolStripMenuItem_Click;
                switchToToolStripMenuItem.DropDownItems.Add(profileItem);
            }
        }
        public void RefreshTranslation()
        {
            emptyItem.Text = anaform.empty;
            defaultproxyItem.Text = anaform.defaultproxytext;
            switchToToolStripMenuItem.Text = anaform.switchTo;
            newProfileToolStripMenuItem.Text = anaform.newprofile;
            deleteThisProfileToolStripMenuItem.Text = anaform.deleteProfile;
            showCertificateErrorsToolStripMenuItem.Text = anaform.showCertError;
            if (certError)
            {
                safeStatusToolStripMenuItem.Text = anaform.CertificateErrorTitle;
                ınfoToolStripMenuItem.Text = anaform.CertificateError;
            }else
            {
                safeStatusToolStripMenuItem.Text = anaform.CertificateOKTitle;
                ınfoToolStripMenuItem.Text = anaform.CertificateOK;
            }
            if (cookieUsage) { cookieInfoToolStripMenuItem.Text = anaform.usesCookies; }else { cookieInfoToolStripMenuItem.Text = anaform.notUsesCookies; }
            label7.Text = anaform.CertErrorPageTitle;
            label8.Text = anaform.CertErrorPageMessage;
            button10.Text = anaform.CertErrorPageButton;
        }
        private void ProfilesToolStripMenuItem_Click(object sender, EventArgs e) => anaform.Invoke(new Action(() => anaform.SwitchProfile(((ToolStripMenuItem)sender).Text)));

        private void NewProfileToolStripMenuItem_Click(object sender, EventArgs e) => anaform.Invoke(new Action(() => anaform.NewProfile()));

        private void DeleteThisProfileToolStripMenuItem_Click(object sender, EventArgs e) => anaform.Invoke(new Action(() => anaform.DeleteProfile(userName)));

        private void Button9_Click(object sender, EventArgs e) => cmsProfiles.Show(MousePosition);

        private void ExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExt formext = new frmExt(this, anaform, userName, ((ToolStripMenuItem)sender).Tag.ToString());
            formext.Show();
        }
        public void LoadExt()
        {
            contextMenuStrip1.Items.Clear();
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }

            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"))
            {
                if (File.Exists(x + "\\extension.kem"))
                {
                    ToolStripMenuItem extItem = new ToolStripMenuItem();
                    extItem.Text = new DirectoryInfo(x).Name;
                    if (!File.Exists(x + "\\icon.png"))
                    {
                        if (Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                        else { extItem.Image = Properties.Resources.ext_w; }
                    }
                    else
                    {
                        extItem.Image = Image.FromFile(x + "\\icon.png");
                    }
                    ToolStripMenuItem extRunItem = new ToolStripMenuItem();
                    extItem.Click += ExtensionToolStripMenuItem_Click;
                    extItem.Tag = x + "\\extension.kem";
                    contextMenuStrip1.Items.Add(extItem);

                }
            }
            if (contextMenuStrip1.Items.Count == 0)
            {
                ToolStripMenuItem emptylol = new ToolStripMenuItem();
                emptyItem = emptylol;
                emptylol.Text = anaform.empty;
                emptylol.Enabled = false;
                contextMenuStrip1.Items.Add(emptylol);
            }
        }
        ToolStripMenuItem emptyItem;
        ToolStripMenuItem defaultproxyItem;
        public void LoadProxies()
        {
            contextMenuStrip2.Items.Clear();
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\"); }
            // add default proxy
            ToolStripMenuItem defaultProxyMenuItem = new ToolStripMenuItem();
            defaultproxyItem = defaultProxyMenuItem;
            defaultProxyMenuItem.Text = anaform.defaultproxytext;
            defaultProxyMenuItem.Click += ExampleProxyToolStripMenuItem_Click;
            defaultProxyMenuItem.Tag = defaultproxyaddress;
            contextMenuStrip2.Items.Add(defaultProxyMenuItem);
            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\"))
            {
                if (File.Exists(x + "\\proxy.kem"))
                {
                    ToolStripMenuItem extItem = new ToolStripMenuItem();
                    extItem.Text = new DirectoryInfo(x).Name;
                    extItem.Click += ExampleProxyToolStripMenuItem_Click;
                    extItem.Tag = File.ReadAllText(x + "\\proxy.kem");
                    contextMenuStrip2.Items.Add(extItem);

                }
            }
        }
        private void Button8_Click(object sender, EventArgs e) => contextMenuStrip1.Show(MousePosition);

        private void TmrSlower_Tick(object sender, EventArgs e) => RefreshFavorites();


        public void FrmCEF_SizeChanged(object sender, EventArgs e) => pbProgress.Width = (this.Width / 100) * websiteprogress;

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(findTextBox.Text)) & panel3.Visible)
            {
                chromiumWebBrowser1.Find(0, findTextBox.Text, false, haltroySwitch1.Checked, false);
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            findTextBox.Text = "";
            chromiumWebBrowser1.StopFinding(true);
        }

        private void Panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => tabform_KeyDown(panel1, new KeyEventArgs(e.KeyData));

        private void ExampleProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SetProxy(chromiumWebBrowser1, ((ToolStripMenuItem)sender).Tag.ToString());
            }
            catch { }
            chromiumWebBrowser1.Reload();
        }
        public string GetElementValueByID(ChromiumWebBrowser browser,string elementID)
        {
            bool isError = false;
            string result = null;
            try
            {
                string script = string.Format("document.getElementById(\"" + elementID + "\").value;");
                browser.EvaluateScriptAsync(script).ContinueWith(x =>
                {
                    var response = x.Result;

                    if (response.Success && response.Result != null)
                    {
                        result = response.Result.ToString();
                    }
                    else
                    {
                        result = null;
                    }
                });
                if (isError)
                {
                    isError = true;
                    throw new NullReferenceException("[ERROR] [KOROT:frmCEF] GetElementValueByID :\" browser: " + browser.Name.ToString() + " elementID: " + elementID + "\"");
                }
                else { return result; }
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button6_Click(object sender, EventArgs e) => contextMenuStrip2.Show(MousePosition);

        private void pictureBox2_Click(object sender, EventArgs e) => cmsPrivacy.Show(pictureBox2, 0, pictureBox2.Size.Height);

        private void xToolStripMenuItem_Click(object sender, EventArgs e) => cmsPrivacy.Close();

        private void showCertificateErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showCertificateErrorsToolStripMenuItem.Tag != null)
            {
                TextBox txtCertificate = new TextBox() { ScrollBars = ScrollBars.Both,Multiline = true, Dock = DockStyle.Fill, Text = showCertificateErrorsToolStripMenuItem.Tag.ToString() };
                Form frmCertificate = new Form() { Icon = anaform.Icon,Text = anaform.CertificateErrorMenuTitle,FormBorderStyle = FormBorderStyle.SizableToolWindow};
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
    }
}
