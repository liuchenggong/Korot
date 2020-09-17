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
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Korot
{
    public partial class frmUpdate : Form
    {
        private readonly string CheckUrl = "https://raw.githubusercontent.com/Haltroy/Korot/master/Korot.htupdate";
        private string downloadUrl;
        private string fileName = "";
        private int UpdateType; //0 = zip 1 = installer
        private readonly string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\";
        private readonly WebClient WebC = new WebClient();
        public int Progress = 0;
        public bool isUpToDate = false;
        public bool isDownloading = false;
        public bool isReady = false;
        public Settings Settings;

        public frmUpdate(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
            WebC.DownloadStringCompleted += WebC_DownloadStringCompleted;
            WebC.DownloadProgressChanged += WebC_DownloadProgressChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileAsyncCompleted;
        }

        public void CheckForUpdates()
        {
            WebC.DownloadStringAsync(new Uri(CheckUrl));
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void WebC_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            isDownloading = true;
            htProgressBar1.Value = e.ProgressPercentage;
            Progress = e.ProgressPercentage;
        }

        private void WebC_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            isDownloading = false;
            if (e.Error != null || e.Cancelled)
            {
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
                WebC.DownloadStringAsync(new Uri(CheckUrl));
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(e.Result);
                KorotVersion Newest = new KorotVersion(doc.FirstChild.NextSibling.OuterXml);
                KorotVersion Current = new KorotVersion("");
                bool _isUpToDate = Current.WhicIsNew(Newest, Environment.Is64BitProcess ? "amd64" : "i86") == Current;
                KorotVersion.UpdateType type = Current.GetUpdateType(Newest);
                KorotVersion.Architecture arch = Newest.Archs.Find(i => i.Type == (Environment.Is64BitProcess ? "amd64" : "i86"));
                if (!_isUpToDate)
                {
                    isUpToDate = false;
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
                    isDownloading = true;
                    WebC.DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
                }
                else if (arch == null)
                {
                    Output.WriteLine(" [frmUpdate Error] Current Architecture not found.");
                }
                else
                {
                    isUpToDate = true;
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
                    if (doRestart)
                    {
                        Restart();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                catch (Exception ex)
                {
                    Output.WriteLine(" [frmUpdate] Error while extracting: " + ex.ToString());
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
                if (doRestart)
                {
                    Restart();
                }
                else
                {
                    Application.Exit();
                }
            });
        }

        public bool doRestart = false;

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
                Output.WriteLine("[frmUpdate] Download File Error: " + e.Error.ToString());
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else if (e.Cancelled)
            {
                Output.WriteLine("[frmUpdate] Download File Cancelled.");
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else
            {
                isReady = true;
            }
        }

        public void ApplyUpdate()
        {
            isInstalling = true;
            if (UpdateType == 1)
            {
                allowClose = true;
                Process.Start(downloadFolder + fileName);
                Application.Exit();
            }
            else
            {
                GetBackup();
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

        public bool isInstalling = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isInstalling)
            {
                OtherInstances();
            }
        }
    }
}