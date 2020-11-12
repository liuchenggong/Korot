/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmPopup : Form
    {
        private readonly string loadurl;
        private readonly frmCEF tabform;
        private readonly string userCache;
        private ChromiumWebBrowser chromiumWebBrowser1;

        public frmPopup(frmCEF CefForm, string profileName, string url)
        {
            InitializeComponent();
            tabform = CefForm;
            loadurl = url;
            userCache = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\Users\\" + profileName + "\\cache\\";
            Text = "Korot";
            InitializeChromium();
        }

        private void FrmExt_Load(object sender, EventArgs e)
        {
            Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
            Location = new System.Drawing.Point((bounds.Width / 2) - (Width/2), (bounds.Height / 2) - (Height /2));
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings
            {
                UserAgent = KorotTools.GetUserAgent()
            };
            if (tabform._Incognito) { settings.CachePath = null; settings.PersistSessionCookies = false; settings.RootCachePath = null; }
            else { settings.CachePath = userCache; settings.RootCachePath = userCache; }
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "korot",
                SchemeHandlerFactory = new SchemeHandlerFactory(tabform)
                {
                    ext = null,
                    isExt = false,
                    extForm = null
                }
            });
            // Initialize cef with the provided settings
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            chromiumWebBrowser1 = new ChromiumWebBrowser(loadurl);
            panel1.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.RequestHandler = new RequestHandlerKorot(tabform);
            chromiumWebBrowser1.DisplayHandler = new DisplayHandler(tabform);
            chromiumWebBrowser1.AddressChanged += cef_AddressChanged;
            chromiumWebBrowser1.TitleChanged += cef_TitleChanged;
            chromiumWebBrowser1.LoadError += cef_onLoadError;
            chromiumWebBrowser1.MenuHandler = new ContextMenuHandler(tabform);
            chromiumWebBrowser1.LifeSpanHandler = new BrowserLifeSpanHandler(tabform);
            chromiumWebBrowser1.DownloadHandler = new DownloadHandler(tabform);
            chromiumWebBrowser1.JsDialogHandler = new JsHandler(tabform);
            chromiumWebBrowser1.DialogHandler = new MyDialogHandler();
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
        }

        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Invoke(new Action(() => Text = e.Title));
        }

        private void cef_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            Invoke(new Action(() => tbAddress.Text = e.Address));
        }

        private void cef_onLoadError(object sender, LoadErrorEventArgs e)
        {
            if (e == null) //User Asked
            {
                chromiumWebBrowser1.Load("http://korot://error?e=TEST");
            }
            else
            {
                if (e.Frame.IsMain)
                {
                    chromiumWebBrowser1.LoadHtml("http://korot://error?e=" + e.ErrorText);
                }
                else
                {
                    e.Frame.LoadUrl("http://korot://error?e=" + e.ErrorText);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tbAddress.BackColor = tabform.Settings.NinjaMode ? tabform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(tabform.Settings.Theme.BackColor, 20, false);
            tbAddress.ForeColor = tabform.Settings.NinjaMode ? tabform.Settings.Theme.BackColor : tabform.Settings.Theme.ForeColor;
        }
    }
}