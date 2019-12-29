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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class Form1 : Form
    {
        string downloadUrl = "";
        string downloadloc = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\update.zip";
        frmCEF anaform;
        WebClient WebC = new WebClient();
        public Form1(frmCEF formSettings)
        {
            InitializeComponent();
            this.TopMost = true;
            anaform = formSettings;
            label1.Text = formSettings.installStatus;
            label2.Text = formSettings.StatusType;
            WebC.DownloadProgressChanged += WebC_DownloadProgressChanged;
            WebC.DownloadFileCompleted += WebC_DownloadFileAsyncCompleted;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Environment.Is64BitProcess) { downloadUrl = anaform.ZipPath64; }else { downloadUrl = anaform.ZipPath32; }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\")) { Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\", true); }
            if(File.Exists(downloadloc)) { File.Delete(downloadloc); }
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\");
            pictureBox1.Width = 0;
                WebC.DownloadFileAsync(new Uri(downloadUrl), downloadloc);
        }
        private void WebC_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            pictureBox1.Width = e.ProgressPercentage * 4;
            label2.Text = anaform.StatusType.Replace("[PERC]", e.ProgressPercentage.ToString()).Replace("[CURRENT]", (e.BytesReceived / 1024).ToString()).Replace("[TOTAL]", (e.TotalBytesToReceive / 1024).ToString());
            label1.Text = anaform.installStatus;
            this.Text = anaform.updateTitle;
        }

        private void WebC_DownloadFileAsyncCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled) 
            {
            if (((WebClient)sender).IsBusy) { ((WebClient)sender).CancelAsync(); }
            ((WebClient)sender).DownloadFileAsync(new Uri(downloadUrl), downloadloc);
            }
            else
            {
                Install();
            }
        }
        async void Install()
        {
            await Task.Run(() => 
            {
                string installPath = Application.StartupPath;
                string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\old\\";
                try
                {
                    
                    Directory.Move(installPath, tempPath);
                    ZipFile.ExtractToDirectory(downloadloc, installPath);
                    Process.Start(Application.ExecutablePath, "https://github.com/Haltroy/Korot/releases");
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    Output.WriteLine(ex.ToString());
                }
            });
        }
    }
}
