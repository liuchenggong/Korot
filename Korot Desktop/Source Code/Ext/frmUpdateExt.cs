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
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmUpdateExt : Form
    {
        public Settings Settings;
        Extension Extension;
        Theme Theme;
        public bool isTheme = false;
        private readonly WebClient webC = new WebClient();
        private Version currentVersion;
        private string fileLocation;
        private string fileURL;
        public string info = "Updating [NAME]..." + Environment.NewLine + "Please wait...";
        public string infoTemp = "[PERC]% | [CURRENT] KiB downloaded out of [TOTAL] KiB.";

        public frmUpdateExt(Extension ext, Settings settings)
        {
            Settings = settings;
            isTheme = false;
            Extension = ext;
            InitializeComponent();
            Lang();
            webC.DownloadStringCompleted += webC_DownloadStringComplete;
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        public frmUpdateExt(Theme theme, Settings settings)
        {
            Settings = settings;
            isTheme = true;
            Theme = theme;
            InitializeComponent();
            Lang();
            webC.DownloadStringCompleted += webC_DownloadStringComplete;
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private void Lang()
        {
            Text = Settings.LanguageSystem.GetItemText("KorotExtensionUpdater");
            info = Settings.LanguageSystem.GetItemText("ExtensionUpdatingInfo").Replace("[NEWLINE]", Environment.NewLine);
            infoTemp = Settings.LanguageSystem.GetItemText("DownloadProgress");
        }

        private readonly string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\";

        private void Read()
        {
            if (Extension != null)
            {
                label1.Text = info.Replace("[NAME]", Extension.CodeName);
                fileURL = "https://haltroy.com/store/item/" + Extension.CodeName + "/" + Extension.CodeName + ".kef";
                fileLocation = tempPath + HTAlt.Tools.GenerateRandomText(12) + "\\" + Extension.CodeName + ".kef";
                verLocation = "https://haltroy.com/store/item/" + Extension.CodeName + "/.htupdate";
            }
            if (Theme != null)
            {
                label1.Text = info.Replace("[NAME]", Theme.CodeName);
                fileURL = "https://haltroy.com/store/item/" + Theme.CodeName + "/" + Theme.CodeName + ".ktf";
                fileLocation = tempPath + HTAlt.Tools.GenerateRandomText(12) + "\\" + Theme.CodeName + ".ktf";
                verLocation = "https://haltroy.com/store/item/" + Theme.CodeName + "/.htupdate";
            }
            downloadString();
        }

        private string verLocation;

        private void frmUpdateExt_Load(object sender, EventArgs e)
        {
            Hide();
            Read();
        }

        private async void downloadString()
        {
            await Task.Run(() =>
            {
                webC.DownloadStringAsync(new Uri(verLocation));
            });
        }

        private async void downloadFile()
        {
            await Task.Run(() =>
            {
                if (File.Exists(fileLocation)) { File.Delete(fileLocation); }
                webC.DownloadFileAsync(new Uri(fileURL), fileLocation);
            });
        }

        public void webC_DownloadStringComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                downloadString();
            }
            else
            {
                Version latest = new Version(e.Result);
                if (latest > currentVersion)
                {
                    startDownload();
                }
            }
        }

        public void webC_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            htProgressBar1.Value = e.ProgressPercentage;
            label2.Text = infoTemp.Replace("[PERC]", e.ProgressPercentage.ToString())
                .Replace("[CURRENT]", (e.BytesReceived / 1024).ToString())
                .Replace("[TOTAL]", (e.TotalBytesToReceive / 1024).ToString());
        }

        public void webC_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            { downloadFile(); }
            else
            {
                webC.Dispose();
                frmInstallExt installExt = new frmInstallExt(Settings, fileLocation, true);
                installExt.ShowDialog();
                Directory.Delete(new FileInfo(fileLocation).DirectoryName, true);
                Close();
            }
        }

        private void startDownload()
        {
            Show();
            downloadFile();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Settings.Theme.BackColor;
            ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            htProgressBar1.BarColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
            htProgressBar1.BackColor = BackColor;
        }
    }
}