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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public partial class frmUpdate : Form
    {
        private string CheckUrl = "https://raw.githubusercontent.com/Haltroy/Korot/master/Korot.htupdate";
        private string downloadUrl;
        private string fileName = "";
        private int UpdateType; //0 = zip 1 = installer
        private readonly string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\";
        private readonly WebClient WebC = new WebClient();
        private string StatusType;
        private string installingTxt;
        private string installStatus;
        public Settings Settings;
        public frmUpdate(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
            WebC.DownloadStringCompleted += WebC_DownloadStringCompleted;
            WebC.DownloadProgressChanged += WebC_DownloadProgressChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileAsyncCompleted;
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private void RefreshTranslate()
        {
            StatusType = Settings.LanguageSystem.GetItemText("DownloadProgress"); ;
            installStatus = Settings.LanguageSystem.GetItemText("UpdatingMessage");
            Text = Settings.LanguageSystem.GetItemText("KorotUpdate");
            installingTxt = Settings.LanguageSystem.GetItemText("Installing");
            label1.Text = installStatus;
            label2.Text = StatusType.Replace("[PERC]", "0").Replace("[CURRENT]", "0").Replace("[TOTAL]", "0");

        }
        private static int Brightness(System.Drawing.Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        private static int GerekiyorsaAzalt(int defaultint, int azaltma)
        {
            return defaultint > azaltma ? defaultint - 20 : defaultint;
        }

        private static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır)
        {
            return defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        }
        private void frmUpdate_Load(object sender, EventArgs e)
        {
            RefreshTranslate();
            WebC.DownloadStringAsync(new Uri(CheckUrl));
        }
        private void WebC_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            htProgressBar1.Value = e.ProgressPercentage;
            label2.Text = StatusType.Replace("[PERC]", e.ProgressPercentage.ToString()).Replace("[CURRENT]", (e.BytesReceived / 1024).ToString()).Replace("[TOTAL]", (e.TotalBytesToReceive / 1024).ToString());
            label1.Text = installStatus;
        }
        private void WebC_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
                WebC.DownloadStringAsync(new Uri(CheckUrl));
            }
            else
            {
                XmlDocument dokk /* r6 joke here lol */ = new XmlDocument();
                dokk.LoadXml(e.Result);
                KorotVersion Newest = new KorotVersion(dokk.FirstChild.NextSibling.OuterXml);
                KorotVersion Current = new KorotVersion("");
                bool isUpToDate = Current.WhicIsNew(Newest, Environment.Is64BitProcess ? "amd64" : "i86") == Current;
                KorotVersion.UpdateType type = Current.GetUpdateType(Newest);
                KorotVersion.Architecture arch = Newest.Archs.Find(i => i.Type == (Environment.Is64BitProcess ? "amd64" : "i86"));
                if (!isUpToDate)
                {
                    switch (type)
                    {
                        case KorotVersion.UpdateType.Installer:
                            UpdateType = 1;
                            fileName = "install.exe";
                            downloadUrl = Newest.InstallerUrl;
                            break;
                        case KorotVersion.UpdateType.Upgrade:
                            UpdateType = 0;
                            fileName = Newest.Version + (Environment.Is64BitProcess ? "-amd64" : "-i86") + "-U" + ".hup";
                            downloadUrl = arch.Update.Replace("[VERSION]", Newest.Version);
                            break;
                        case KorotVersion.UpdateType.FullUpgrade:
                            UpdateType = 0;
                            fileName = Newest.Version + (Environment.Is64BitProcess ? "-amd64" : "-i86") + ".hup";
                            downloadUrl = arch.FullUpdate.Replace("[VERSION]", Newest.Version);
                            break;
                    }
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\")) { Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\", true); }
                    if (File.Exists(downloadFolder + fileName)) { File.Delete(downloadFolder + fileName); }
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\");
                    WebC.DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
                }
                else if (arch == null)
                {
                    throw new Exception("Current architecture not found.");
                }
                else
                {
                    Restart();
                }
            }
        }


        private readonly string backupFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\UpdateBackup\\";

        private async void GetBackup()
        {
            await Task.Run(() =>
            {
                if (Directory.Exists(backupFolder))
                {
                    Directory.Delete(backupFolder, true);
                }
                Directory.CreateDirectory(backupFolder);
                KorotTools.Copy(Application.StartupPath, backupFolder);
                //Directory.Delete(Application.StartupPath, true);
                //Directory.CreateDirectory(Application.StartupPath);
                try
                {
                    string newVerLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\UpdateNewVer\\";
                    if (Directory.Exists(newVerLocation)) { Directory.Delete(newVerLocation, true); }
                    Directory.CreateDirectory(newVerLocation);
                    ZipFile.ExtractToDirectory(downloadFolder + fileName, newVerLocation, Encoding.UTF8);
                    KorotTools.Copy(newVerLocation, Application.StartupPath);
                    File.Delete(downloadFolder + fileName);
                    Directory.Delete(newVerLocation, true);
                    Directory.Delete(backupFolder, true);
                    Restart();
                }
                catch (Exception ex)
                {
                    Output.WriteLine(" [Korot.Updater] Error while extracting: " + ex.ToString());
                    ReturnBackup();
                }
            });
        }

        private async void ReturnBackup()
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(Application.StartupPath)) { Directory.CreateDirectory(Application.StartupPath); }
                KorotTools.Copy(backupFolder, Application.StartupPath);
                File.Delete(downloadFolder + fileName);
                string newVerLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\UpdateNewVer\\";
                if (Directory.Exists(newVerLocation)) { Directory.Delete(newVerLocation, true); }
                Directory.Delete(backupFolder, true);
                Restart();
            });
        }

        private void Restart(bool notUpdated = false)
        {
            Process.Start(Application.ExecutablePath, notUpdated ? "" : "https://github.com/Haltroy/Korot/releases");
            allowClose = true;
            Application.Exit();
        }

        private void WebC_DownloadFileAsyncCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Output.WriteLine("[Korot.Updater] Download File Error: " + e.Error.ToString());
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else if (e.Cancelled)
            {
                Output.WriteLine("[Korot.Updater] Download File Cancelled.");
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else
            {
                if (UpdateType == 1)
                {
                    allowClose = true;
                    Process.Start(downloadFolder + fileName);
                    Application.Exit();
                }
                else
                {
                    label2.Text = installingTxt;
                    GetBackup();
                }
            }
        }

        private bool allowClose = false;

        private void OtherInstances()
        {
            try
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);

                //Loop through the running processes in with the same name 
                foreach (Process process in processes)
                {
                    //Ignore the current process 
                    if (process.Id != current.Id)
                    {
                        //Make sure that the process is running from the exe file. 
                        if (Assembly.GetExecutingAssembly().Location.
                             Replace("/", "\\") == current.MainModule.FileName)
                        {
                            //Kill the other process instance.  
                            Process.Start("taskkill /f /pid" + process.Id);

                        }
                    }
                }
            }
            catch { } //Ignored, possibly wrong PID
        }

        private void frmUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose) { e.Cancel = true; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OtherInstances();
            BackColor = Settings.Theme.BackColor;
            ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            htProgressBar1.BackColor = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
            htProgressBar1.BarColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;

        }
    }

    
}
