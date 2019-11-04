using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class Form1 : Form
    {
        string downloadUrl = "http://bit.ly/KorotSetup";
        string downloadloc = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\Installer.exe";
        frmSettings anaform;
        WebClient WebC = new WebClient();
        public Form1(frmSettings formSettings)
        {
            InitializeComponent();
            anaform = formSettings;
            label1.Text = formSettings.installStatus;
            label2.Text = formSettings.StatusType;
            WebC.DownloadProgressChanged += WebC_DownloadProgressChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileAsyncCompleted;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\")) { Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\", true); }
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\");
            pictureBox1.Width = 0;
            WebC.DownloadFileAsync(new Uri(downloadUrl), downloadloc);
        }
        private void WebC_DownloadProgressChanged(object sender,DownloadProgressChangedEventArgs e)
        {
            pictureBox1.Width = e.ProgressPercentage * 4;
            label2.Text = anaform.StatusType.Replace("[PERC]", e.ProgressPercentage.ToString()).Replace("[CURRENT]", (e.BytesReceived / 1024).ToString()).Replace("[TOTAL]", (e.TotalBytesToReceive / 1024).ToString());
            label1.Text = anaform.installStatus;
            this.Text = anaform.updateTitle;
        }

        private void WebC_DownloadFileAsyncCompleted(object sender,AsyncCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled) { } else
            {
                Process.Start(downloadloc);
                Application.Exit();
            }
        }
    }
}
