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
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class Form1 : Form
    {
        string UpdateURL = "https://github.com/Haltroy/Korot/releases/download/[LATEST]/Korot-Full-[ARCH].zip";
        string InstallerURL = "http://bit.ly/KorotSetup";
        string downloadUrl;
        string fileName = "";
        int UpdateType; //0 = zip 1 = installer
        string downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\update";
        WebClient WebC = new WebClient();
        string StatusType;
        string installingTxt;
        string installStatus;
        public Form1()
        {
            InitializeComponent();
            WebC.DownloadStringCompleted += WebC_DownloadStringCompleted;
            WebC.DownloadProgressChanged += WebC_DownloadProgressChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileAsyncCompleted;
            foreach (Control x in this.Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }
        void RefreshTranslate()
        {
            if (!File.Exists(Properties.Settings.Default.LangFile)) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }
            string Playlist = FileSystem2.ReadFile(Properties.Settings.Default.LangFile, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            StatusType = SplittedFase[93].Substring(1).Replace(Environment.NewLine, "");
            installStatus = SplittedFase[92].Substring(1).Replace(Environment.NewLine, "");
            this.Text = SplittedFase[1].Substring(1).Replace(Environment.NewLine, "");
            installingTxt = SplittedFase[265].Substring(1).Replace(Environment.NewLine, "");
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
        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshTranslate();
            pictureBox1.Width = 0;
            WebC.DownloadStringAsync(new Uri("https://haltroy.com/Update/Korot.htupdate"));
        }
        private void WebC_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pictureBox1.Width = e.ProgressPercentage * 3;
            label2.Text = StatusType.Replace("[PERC]", e.ProgressPercentage.ToString()).Replace("[CURRENT]", (e.BytesReceived / 1024).ToString()).Replace("[TOTAL]", (e.TotalBytesToReceive / 1024).ToString());
            label1.Text = installStatus;
        }
        private void WebC_DownloadStringCompleted (object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
                WebC.DownloadStringAsync(new Uri("https://haltroy.com/Update/Korot.htupdate"));
            }
            else
            {
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase3 = e.Result.Split(token);
                bool requireCompleteUpgrade = SplittedFase3[1].Trim().Replace(Environment.NewLine, "") == "1";
                string nv = SplittedFase3[0].Replace(Environment.NewLine,"");
                string minmv = SplittedFase3[2].Replace(Environment.NewLine, "");
                UpdateURL = SplittedFase3[3].Replace(Environment.NewLine, "");
                string arch = Environment.Is64BitProcess ? "x64" : "x86";
                Version current = new Version(Application.ProductVersion);
                Version MinVersion = new Version(minmv);
                if (requireCompleteUpgrade)
                {
                    UpdateType = 2;
                    fileName = ".exe";
                    downloadUrl = InstallerURL;
                }else
                {
                    if (current > MinVersion)
                    {
                        UpdateType = 0;
                        fileName = ".hup";
                        downloadUrl = UpdateURL.Replace("[ARCH]", arch).Replace("[LATEST]", nv);
                    }else
                    {
                            UpdateType = 1;
                            fileName = ".exe";
                            downloadUrl = InstallerURL;
                      
                    }
                }
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\")) { Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\", true); }
                if (File.Exists(downloadFolder + fileName)) { File.Delete(downloadFolder + fileName); }
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\");
                WebC.DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
        }
        void RecursiveBackup(string folderName,string backupFolderName)
        {
            foreach (String x in Directory.GetFiles(folderName))
            {
                FileInfo info = new FileInfo(x);
                File.Move(x, backupFolderName + (backupFolderName.EndsWith("\\") ? "" : "\\") + info.Name);
            }
            foreach (String x in Directory.GetDirectories(folderName))
            {
                DirectoryInfo info = new DirectoryInfo(x);
                if (Directory.Exists(backupFolderName + (backupFolderName.EndsWith("\\") ? "" : "\\") + info.Name))
                {
                    Directory.Delete(backupFolderName + (backupFolderName.EndsWith("\\") ? "" : "\\") + info.Name, true);
                }
                Directory.Move(x, backupFolderName + (backupFolderName.EndsWith("\\") ? "" : "\\") + info.Name);
                //RecursiveBackup(x, backupFolderName + (backupFolderName.EndsWith("\\") ? "" : "\\") + info.Name);
            }
        }
        string backupFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\UpdateBackup\\";
        async void GetBackup()
        {
            await Task.Run(() => {
                if (Directory.Exists(backupFolder))
                {
                    Directory.Delete(backupFolder,true);
                }
                Directory.CreateDirectory(backupFolder);
                RecursiveBackup(Application.StartupPath, backupFolder);
                    //Directory.Delete(Application.StartupPath, true);
                    //Directory.CreateDirectory(Application.StartupPath);
                try
                {
                    ZipFile.ExtractToDirectory(downloadFolder + fileName, Application.StartupPath,Encoding.UTF8);
                    Restart();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" [Korot.Updater] Error while Extracting: " + ex.ToString());
                    ReturnBackup();
                }
                
            });
        }
        async void ReturnBackup()
        {
            await Task.Run(() => {
                if (Directory.Exists(Application.StartupPath)){Directory.Delete(Application.StartupPath, true);}
                Directory.CreateDirectory(Application.StartupPath);
                //Directory.Move(backupFolder, Application.StartupPath);
                foreach (String x in Directory.GetDirectories(backupFolder)){ DirectoryInfo current = new DirectoryInfo(x);Directory.Move(x, Application.StartupPath + current.Name + "\\");}
                foreach (String x in Directory.GetFiles(backupFolder)){FileInfo current = new FileInfo(x);File.Move(x, Application.StartupPath + current.Name);}
                Restart();
            });
        }
        void Restart()
        {
            Process.Start(Application.ExecutablePath, "https://github.com/Haltroy/Korot/releases");
            allowClose = true;
            Application.Exit();
        }

        private void WebC_DownloadFileAsyncCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("[Korot.Updater] Download File Error: " + e.Error.ToString());
                if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadFolder + fileName);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("[Korot.Updater] Download File Cancelled.");
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
                }else
                {
                    label2.Text = installingTxt;
                    GetBackup();
                }
            }
        }
        bool allowClose = false;
        void OtherInstances()
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
            catch { } //Ignored, possibly wrong PID cuz its closed or administrated launch
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose) { e.Cancel = true; }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            OtherInstances();
            this.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            panel1.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 20, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 20, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 20, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 20), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 20), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 20));
            pictureBox1.BackColor = Properties.Settings.Default.OverlayColor;

        }
    }
}
