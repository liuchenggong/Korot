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
using CefSharp;
using System;
using System.Windows.Forms;

namespace Korot
{
    public class DownloadHandler : IDownloadHandler
    {
        frmCEF ActiveForm;
        frmMain aNaFRM;
        public DownloadHandler(frmCEF activeForm, frmMain anaform)
        {
            ActiveForm = activeForm;
            aNaFRM = anaform;
        }
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (downloadItem.SuggestedFileName.ToLower().EndsWith(".kef"))
            {
                callback.Continue(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName, false);
                frmdown.Show();
                frmdown.label1.Text = ActiveForm.fromtwodot + downloadItem.Url;
                frmdown.label2.Text = ActiveForm.totwodot + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName;
                frmdown.Text = ActiveForm.korotdownloading;
                frmdown.checkBox1.Enabled = false;
                frmdown.checkBox2.Enabled = false;
                frmdown.checkBox1.Text = ActiveForm.openfileafterdownload;
                frmdown.checkBox2.Text = ActiveForm.closethisafterdownload;
                frmdown.button1.Enabled = false;
                frmdown.button1.Text = ActiveForm.open;
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.FileName = downloadItem.SuggestedFileName;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    callback.Continue(saveFileDialog1.FileName, false);
                    frmdown.Show();
                    frmdown.label1.Text = ActiveForm.fromtwodot + downloadItem.Url;
                    frmdown.label2.Text = ActiveForm.totwodot + saveFileDialog1.FileName;
                    frmdown.Text = ActiveForm.korotdownloading;
                    frmdown.checkBox1.Text = ActiveForm.openfileafterdownload;
                    frmdown.checkBox2.Text = ActiveForm.closethisafterdownload;
                    frmdown.button1.Text = ActiveForm.open;
                }
            }
        }
        frmDownloader frmdown = new frmDownloader();
        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            frmdown.pictureBox1.Size = new System.Drawing.Size(downloadItem.PercentComplete * 3, 20);
            frmdown.label3.Text = downloadItem.PercentComplete + "%";
            if (downloadItem.CurrentSpeed < 1024) //byte 
            {
                frmdown.lbSpeed.Text = downloadItem.CurrentSpeed + " b/s";
            }
            else if (downloadItem.CurrentSpeed > 1024 && downloadItem.CurrentSpeed < 1048576) //kb 
            {
                frmdown.lbSpeed.Text = downloadItem.CurrentSpeed / 1024 + " kb/s";
            }
            else if (downloadItem.CurrentSpeed < 1073741824 && downloadItem.CurrentSpeed > 1048576) //mb 
            {
                frmdown.lbSpeed.Text = downloadItem.CurrentSpeed / 1048576 + " mb/s";
            }
            else if (downloadItem.CurrentSpeed > 1073741824) //gb 
            {
                frmdown.lbSpeed.Text = downloadItem.CurrentSpeed / 1048576 + " gb/s";
            }

            if (downloadItem.IsCancelled) { frmdown.Close(); }
            if (downloadItem.IsComplete)
            {
                if (downloadItem.SuggestedFileName.ToLower().EndsWith(".kef"))
                {
                    frmdown.Close();
                    frmInstallExt installExt = new frmInstallExt(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName);
                    installExt.Show();
                }
                else
                {
                    frmdown.downloaddone();
                    aNaFRM.Invoke(new Action(() => ActiveForm.RefreshDownloadList()));
                }
            }
        }
    }
}