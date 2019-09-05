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
    public partial class frame0 : Form
    {
        frmFrame FrameForm;
        public frame0(frmFrame frameform)
        {
            InitializeComponent();
            FrameForm = frameform;
            WebC.DownloadStringCompleted += WebC_DownloadStringCompleted;
            WebC.DownloadProgressChanged += WebC_StatusChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileCompleted;
        }
        bool initMode = true;
        WebClient WebC = new WebClient();
        private void WebC_StatusChanged(object sender,DownloadProgressChangedEventArgs e)
        {
            if (initMode) { label1.Text = "Initializing..."; } else { label1.Text = "Downloading..."; }
            button1.Enabled = false;
            panel1.Visible = true;
            pictureBox1.Width = e.ProgressPercentage * 3;
            label2.Visible = true;
            label2.Text = e.ProgressPercentage + "% | " + ((e.TotalBytesToReceive - e.BytesReceived) / 1048576) + " MB left.";
        }
        private void WebC_DownloadStringCompleted(object sender,DownloadStringCompletedEventArgs e)
        {
            panel1.Visible = false;
            initMode = false;
            button1.Enabled = true;
            label2.Visible = false;
            if ((!e.Cancelled) && (e.Error == null))
            {
                    Version KorotCurrent = new Version(Application.ProductVersion);
                    Version KorotLatest = new Version(e.Result);
                    if (KorotCurrent < KorotLatest)
                    {
                        button1.Text = "Update";
                        label1.Text = "Update available for Korot Installer.";
                    }
                    else
                    {
                    FrameForm.Invoke(new Action(() => FrameForm.ShowFrame(new frame1(FrameForm))));
                    }
                
            }
        }
        private void Frame0_Load(object sender, EventArgs e)
        {
                label1.Text = "Checking for updates...";
            WebC.DownloadStringAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2127&authkey=!AOHzgD-_gJ_ftFU&ithint=file%2ctxt&e=LURFBi"));
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (FrameForm.isDarkMode)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                button1.BackColor = Color.FromArgb(255, 20, 20, 20);
                panel1.BackColor = Color.FromArgb(255, 20, 20, 20);
            }
            else
            {
                this.BackColor =Color.White;
                this.ForeColor =  Color.Black;
                button1.BackColor = Color.FromArgb(255, 245,245,245);
                panel1.BackColor = Color.FromArgb(255, 245, 245, 245);
            }
        }
    
        string DownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot-Setup.exe";
        private void WebC_DownloadFileCompleted(object sender,AsyncCompletedEventArgs e)
        {
            if ((!e.Cancelled) && (e.Error == null))
            {
                Process.Start(DownloadPath);

            }else
            {
                label1.Text = "Error while downloading files.";
                if (e.Cancelled == true) { label2.Text = "Cancelled."; } else { label2.Text = e.Error.Message; }
                button1.Enabled = true;
                label3.Visible = false;
                FrameForm.Invoke(new Action(() => FrameForm.doNotClose = false));
            }
            }
        private void Button1_Click(object sender, EventArgs e)
        {
            label3.Visible = true;
            FrameForm.Invoke(new Action(() => FrameForm.doNotClose = true)) ;
            button1.Enabled = false;
            panel1.Visible = true;
            label2.Visible = true;
            pictureBox1.Width = 0;
            if (File.Exists(DownloadPath)) { File.Delete(DownloadPath); }
            WebC.DownloadFileAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2122&authkey=!AOMgX7p_xUKC9H4&e=BeOlwX"), DownloadPath);
        }

        private void Label3_Click(object sender, EventArgs e)
        {
            WebC.CancelAsync();
        }
    }
}
