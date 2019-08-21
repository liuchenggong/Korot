using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Webtroy
{
    public partial class frmUpdate : Form
    {
        string installerloc = "https://onedrive.live.com/download?resid=3FD0899CA240B9B!1616&authkey=!AFiqkCi2_qm-4-0";
        string installoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Webtroy-Installer.exe";
        WebClient webc = new WebClient();
        public frmUpdate()
        {
            InitializeComponent();
            webc.DownloadProgressChanged += webc_DownloadStateChanged;
            webc.DownloadFileCompleted += webc_downloaddone;
        }
        private void webc_DownloadStateChanged(object sender,DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Label1.Text = "Downloading...%" + percentage;
        }
        private void webc_downloaddone(Object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Process.Start(installoc);
            System.Threading.Thread.Sleep(3000);
            Webtroy.Properties.Settings.Default.Save();
            Application.Exit();
        }
        private void frmUpdate_Load(object sender, EventArgs e)
        {
            webc.DownloadFileAsync(new Uri(installerloc),    installoc );
            if (Properties.Settings.Default.UseDarkImages)
            { this.BackColor = Color.Black; this.ForeColor = Color.White; }
            else
            { this.BackColor = Color.White; this.ForeColor = Color.Black; }
        }
    }
}
