/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
//using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
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
        private readonly string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\";
        private readonly WebClient WebC = new WebClient();
        public int Progress = 0;
        public bool isUpToDate = false;
        public bool isDownloading = false;
        public bool isReady = false;
        public bool isError = false;
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
                isError = true;
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(e.Result);
                KorotVersion Newest = new KorotVersion(doc.FirstChild.NextSibling.OuterXml);
                KorotVersion Current = new KorotVersion("");
                bool _isUpToDate = Current.WhicIsNew(Newest, Environment.Is64BitProcess ? "amd64" : "i86") == Current;
                KorotVersion.UpdateType type = Current.GetUpdateType(Newest);
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
                    if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\")) { Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\", true); }
                    if (File.Exists(downloadFolder + fileName)) { File.Delete(downloadFolder + fileName); }
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Haltroy\\Korot\\");
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
        private void WebC_DownloadFileAsyncCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                isError = true;
                Output.WriteLine("[frmUpdate] Download File Error: " + e.Error.ToString());
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else if (e.Cancelled)
            {
                isError = true;
                Output.WriteLine("[frmUpdate] Download File Cancelled.");
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else
            {
                isDownloading = false;
                isReady = true;
            }
        }
        private bool alreadyOpenInstaller = false;
        public void ApplyUpdate()
        {
            if (alreadyOpenInstaller) { return; } else { alreadyOpenInstaller = true; }
            isInstalling = true;
            if (UpdateType == 1)
            {
                allowClose = true;
                Process.Start(downloadFolder + fileName);
                Application.Exit();
            }
            else
            {
                Process.Start(Application.StartupPath + "\\KorotUpdate.exe", "\"" + downloadFolder + fileName + "\"");
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