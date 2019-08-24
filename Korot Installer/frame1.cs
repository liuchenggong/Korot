using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Compression;
using System.ComponentModel;

namespace Korot_Installer
{
    public partial class frame1 : Form
    {
        frmFrame FrameForm;
        public frame1(frmFrame frameform)
        {
            InitializeComponent();
            FrameForm = frameform;
            WebC.DownloadStringCompleted += WebC_DownloadStringCompleted;
            WebC.DownloadProgressChanged += WebC_StatusChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileCompleted;
        }
        bool UpdateKorot = false;
        bool RepairMode = false;
        WebClient WebC = new WebClient();
        private void WebC_StatusChanged(object sender,DownloadProgressChangedEventArgs e)
        {
            panel1.Visible = true;
            pictureBox1.Width = e.ProgressPercentage * 3;
            label2.Visible = true;
            label2.Text = e.ProgressPercentage + "% | " + ((e.TotalBytesToReceive - e.BytesReceived) / 1048576) + " MB left.";
        }
        private void WebC_DownloadStringCompleted(object sender,DownloadStringCompletedEventArgs e)
        {
            label2.Visible = false;
            if ((!e.Cancelled) && (e.Error == null))
            {
                if (UpdateKorot)
                {
                    FileVersionInfo KorotCurrentVersion = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\Korot\\Korot Beta.exe");
                    Version KorotCurrent = new Version(KorotCurrentVersion.FileVersion);
                    Version KorotLatest = new Version(e.Result);
                    if (KorotCurrent < KorotLatest)
                    {
                        button1.Text = "Update";
                        RepairMode = false;
                        label1.Text = "Update available for Korot.";
                    }
                    else
                    {
                        button1.Text = "Repair";
                        RepairMode = true;
                        label1.Text = "Your Korot is up to date.";
                    }
                    VersionDownloaded = e.Result;
                }
                else
                {
                    VersionDownloaded = e.Result;
                }
            }
        }
        private void Frame1_Load(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "\\Korot\\Korot Beta.exe")) { isKorotInstalled = true; }
            if (isKorotInstalled)
            {
                label1.Text = "Checking for updates...";
                button2.Visible = true;
                bool UpdateKorot = true;
                button1.Text = "Repair"; // updatin > repairin
            }
            else
            {
                label1.Text = "Initializing Installer...";
                button2.Visible = false;
                bool UpdateKorot = false;
                button1.Text = "Install";
                label1.Text = "Your Korot is ready to be installed.";
            }
            WebC.DownloadStringAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2123&authkey=!ADjFaqhHH3MjOAQ&ithint=file%2ctxt&e=5QH8I8"));
        }

        bool isKorotInstalled;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (FrameForm.isDarkMode)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                button1.BackColor = Color.FromArgb(255, 20, 20, 20);
                button2.BackColor = Color.FromArgb(255, 20, 20, 20);
                panel1.BackColor = Color.FromArgb(255, 20, 20, 20);
            }
            else
            {
                this.BackColor =Color.White;
                this.ForeColor =  Color.Black;
                button1.BackColor = Color.FromArgb(255, 245,245,245);
                button2.BackColor = Color.FromArgb(255, 245, 245, 245);
                panel1.BackColor = Color.FromArgb(255, 245, 245, 245);
            }
        }
        void RegisterProduct(string AppName,string AppExe,string appVersion,string Publisher,string appFolder)
        {
            //Create a Temp CMD and write it in Temp folder
            string TemplateCMD = Properties.Resources.Register.Replace("[APPNAME]", AppName).Replace("[APPPATH]", AppExe.Replace("\\", "\\\\")).Replace("[APPVERSION]", appVersion).Replace("[PUBLISHER]", Publisher).Replace("[APPFOLDER]", appFolder.Replace("\\", "\\\\")).Replace("[INSTALLER]", Application.ExecutablePath.Replace("\\","\\\\"));
            if (Directory.Exists(Application.StartupPath + "\\Temp\\")) { Directory.Delete(Application.StartupPath + "\\Temp\\", true); }
            Directory.CreateDirectory(Application.StartupPath + "\\Temp\\");
            File.WriteAllText(Application.StartupPath + "\\Temp\\temp.cmd", TemplateCMD);
            //now run it
            ProcessStartInfo procInfo = new ProcessStartInfo();
            procInfo.UseShellExecute = true;
            // sets the path of the file to be opened to name.cmd
            procInfo.FileName = Application.StartupPath + "\\Temp\\temp.cmd";
            procInfo.WorkingDirectory = "";
            procInfo.Verb = "runas";
            procInfo.CreateNoWindow = true;
            procInfo.WindowStyle = ProcessWindowStyle.Hidden;
            // running the file in cmd
            Process.Start(procInfo);
        }
        string VersionDownloaded;
        async void UpdateFile(string appName, string zipPath, string extractPath)
        {
            Directory.Delete(extractPath, true);
            //Unzip Files
            label1.Text = "Installing...";
            label2.Text = "Unzipping Files...";
            ZipFile.ExtractToDirectory(zipPath, extractPath);
            label1.Text = "Done!";
            label2.Visible = false;
            FrameForm.Invoke(new Action(() => FrameForm.doNotClose = false));
            Process.Start(extractPath + appName + ".exe");
            Application.Exit();
        }
        async void InstallFile(string appName,string zipPath,string extractPath)
        {
            //Unzip Files
            label1.Text = "Installing...";
            label2.Text = "Unzipping Files...";
            ZipFile.ExtractToDirectory(zipPath,extractPath);
            //Register the product
            label2.Text = "Registering...";
            RegisterProduct(appName, extractPath + appName + ".exe", VersionDownloaded, "Haltroy", extractPath);
            //Register File Extensions
            label2.Text = "Registering file extensions...";
            RegisterFileExt(".html", extractPath + appName + ".exe", appName);
            RegisterFileExt(".htm", extractPath + appName + ".exe", appName);
            RegisterFileExt("http", extractPath + appName + ".exe", appName);
            RegisterFileExt(".css", extractPath + appName + ".exe", appName);
            //Create Shortcut
            label2.Text = "Creating shortcut...";
            CreateShortcut(appName, extractPath + appName + ".exe");
            label1.Text = "Done!";
            label2.Visible = false;
            FrameForm.Invoke(new Action(() => FrameForm.doNotClose = false));
            Process.Start(extractPath + appName + ".exe");
            Application.Exit();
        }
        void RegisterFileExt(string fext,string appUrl,string AppName)
        {
            //Get all text from resource then replace the text [FILEEXT] , [APPPATH] and [APPNAME]
            string TemplateReg = Properties.Resources.FileExtTemplate.Replace("[FILEEXT]", fext).Replace("[APPPATH]", appUrl.Replace("\\","\\\\")).Replace("[APPNAME]",AppName);
            //Write Registry to out file (temp.reg)
            if(Directory.Exists(Application.StartupPath + "\\Temp\\")) { Directory.Delete(Application.StartupPath + "\\Temp\\",true); }
            Directory.CreateDirectory(Application.StartupPath + "\\Temp\\");
            File.WriteAllText(Application.StartupPath + "\\Temp\\temp.reg",TemplateReg);
            //Execute silently
            string directory = Application.StartupPath + "\\Temp\\temp.reg";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + directory + "\"");
            regeditProcess.WaitForExit();
        }
        private void WebC_DownloadFileCompleted(object sender,AsyncCompletedEventArgs e)
        {
            if ((!e.Cancelled) && (e.Error == null))
            {
                if(UpdateKorot)
                {
                    UpdateFile("Korot Beta", Application.StartupPath + "\\update.hta", Application.StartupPath + "\\Korot\\");
                }else
                {
                    InstallFile("Korot Beta", Application.StartupPath + "\\install.hta", Application.StartupPath + "\\Korot\\");
                }
            }else
            {
                label1.Text = "Error while downloading files.";
                if (e.Cancelled == true) { label2.Text = "Cancelled."; } else { label2.Text = e.Error.Message; }
                button1.Enabled = true;
                button2.Enabled = true;
                label3.Visible = false;
                if (UpdateKorot || RepairMode)
                {
                    button2.Visible = true;
                }
                FrameForm.Invoke(new Action(() => FrameForm.doNotClose = false));

            }
            }
        private void Button1_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            FrameForm.Invoke(new Action(() => FrameForm.doNotClose = true)) ;
            button1.Enabled = false;
            button2.Visible = false;
            panel1.Visible = true;
            label2.Visible = true;
            pictureBox1.Width = 0;
            if (UpdateKorot) //Update
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2125&authkey=!AK-UGeBouicVZMY&e=Nz7056"), Application.StartupPath + "\\update.hta");
                }
                else
                {
                    WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2124&authkey=!AEFPIwGWYKZQxBk&e=SbD55e"), Application.StartupPath + "\\update.hta");
                }
            }
            else if (RepairMode) //Repair
            {
                if (File.Exists(Application.StartupPath + "\\update.hta"))
                {
                    InstallFile("Korot Beta", Application.StartupPath + "\\update.hta", Application.StartupPath + "\\Korot\\");
                }else if (File.Exists(Application.StartupPath + "\\install.hta"))
                {
                    InstallFile("Korot Beta", Application.StartupPath + "\\install.hta", Application.StartupPath + "\\Korot\\");
                }
                else
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2125&authkey=!AK-UGeBouicVZMY&e=Nz7056"), Application.StartupPath + "\\install.hta");
                    }
                    else
                    {
                        WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2124&authkey=!AEFPIwGWYKZQxBk&e=SbD55e"), Application.StartupPath + "\\install.hta");
                    }
                }
            }
            else //Install
            {
                //Download the file
                if (Environment.Is64BitOperatingSystem)
                {
                    WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2125&authkey=!AK-UGeBouicVZMY&e=Nz7056"), Application.StartupPath + "\\install.hta");
                }
                else
                {
                    WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2124&authkey=!AEFPIwGWYKZQxBk&e=SbD55e"), Application.StartupPath + "\\install.hta");
                }
                }
        }

        void CreateShortcut(string appName,string AppExe)
        {
            string appStartMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + "\\" + appName;
            using (StreamWriter writer = new StreamWriter(appStartMenuPath + ".url"))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + AppExe);
                writer.WriteLine("IconIndex=0");
                writer.WriteLine("IconFile=" + AppExe.Replace('\\', '/'));
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            WebC.CancelAsync();
        }
    }
}
