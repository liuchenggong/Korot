using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmExt : Form
    {
        frmSettings Settingsform;
        string ExtensionPopupPath;
        frmCEF tabform;
        string userCache;
        frmMain anaform;
        string startupJS;
        ChromiumWebBrowser chromiumWebBrowser1;
        string extensionFolder;
        public frmExt(frmCEF CefForm,frmMain rmmain, string profileName,string manifestFile, frmSettings _frmSettings)
        {
            Settingsform = _frmSettings;
            InitializeComponent();
            extensionFolder = new FileInfo(manifestFile).DirectoryName + "//";
            tabform = CefForm;
            anaform = rmmain;
            userCache = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Users\\" + profileName + "\\cache\\";
            string manifest = System.IO.File.ReadAllText(manifestFile);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = manifest.Split(token);
            int i = 0;
            this.Text = SplittedFase[i].ToString().Replace(Environment.NewLine, "").Replace(Environment.NewLine, "") + " v" + SplittedFase[i + 2].ToString().Substring(1).Replace(Environment.NewLine, "").Replace(Environment.NewLine, "") + " by " + SplittedFase[i + 1].ToString().Substring(1).Replace(Environment.NewLine, "").Replace(Environment.NewLine, "");
            ExtensionPopupPath = extensionFolder + SplittedFase[i + 3].ToString().Substring(1).Replace(Environment.NewLine, "").Replace(Environment.NewLine, "");
            startupJS = extensionFolder + SplittedFase[i + 4].ToString().Substring(1).Replace(Environment.NewLine, "").Replace(Environment.NewLine, "");
                InitializeChromium();
        }
        private static bool IsLocalPath(string p)
        {
            if (p.ToLower().StartsWith("http:\\") | p.ToLower().StartsWith("https:\\") | p.ToLower().StartsWith("ftp:\\"))
            {
                return false;
            }else if (p.ToLower().StartsWith("file:\\"))
            {
                return true;
            }

            return new Uri(p).IsFile;
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
        private void cefaddresschanged(object sender,AddressChangedEventArgs e)
        {
                if (IsLocalPath(e.Address))
                {

                } else
                {
                    tabform.Invoke(new Action(() => tabform.NewTab(e.Address)));
                    e.Browser.GoBack();
                }
            
        }
        public static string GetOsVer() { try { ManagementObject mo = GetMngObj("Win32_OperatingSystem"); if (null == mo) return string.Empty; return mo["Version"] as string; } catch { return string.Empty; } }

        private void FrmExt_Load(object sender, EventArgs e)        {        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.UserAgent = "Mozilla/5.0 ( Windows NT " + GetOsVer() + "; " + Environment.OSVersion.Platform + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/" + Cef.ChromiumVersion + " Safari/537.36 Korot/" + Application.ProductVersion.ToString();
            settings.CachePath = userCache;
            // Initialize cef with the provided settings
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            else { chromiumWebBrowser1 = new ChromiumWebBrowser(ExtensionPopupPath); }
            this.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.Load(ExtensionPopupPath);
            chromiumWebBrowser1.AddressChanged += cefaddresschanged;
            chromiumWebBrowser1.DisplayHandler = new DisplayHandler(tabform, anaform);
            chromiumWebBrowser1.TitleChanged += cef_TitleChanged;
            chromiumWebBrowser1.LoadError += cef_onLoadError;
            chromiumWebBrowser1.MenuHandler = new ContextMenuHandler(tabform, anaform, Settingsform);
            chromiumWebBrowser1.LifeSpanHandler = new BrowserLifeSpanHandler(tabform);
            chromiumWebBrowser1.DialogHandler = new MyDialogHandler();
            chromiumWebBrowser1.DownloadHandler = new DownloadHandler(tabform, anaform, Settingsform);
            chromiumWebBrowser1.JsDialogHandler = new JsHandler(Settingsform);
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
        }
        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.InvokeOnUiThreadIfRequired(() => this.Text = e.Title);
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
    }
}
