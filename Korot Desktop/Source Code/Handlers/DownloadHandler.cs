using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.Handler;

namespace Korot
{
    public class DownloadHandler : IDownloadHandler
    {
        frmSettings settingsform;
        frmCEF ActiveForm;
        frmMain aNaFRM;
        public DownloadHandler (frmCEF activeForm,frmMain anaform, frmSettings _settingsform)
        {
            settingsform = _settingsform;
            ActiveForm = activeForm;
            aNaFRM = anaform;
        }
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
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
                    frmdown.label1.Text = settingsform.fromtwodot + downloadItem.Url;
                    frmdown.label2.Text = settingsform.totwodot + saveFileDialog1.FileName;
                    frmdown.Text = settingsform.korotdownloading;
                    frmdown.checkBox1.Text = settingsform.openfileafterdownload;
                    frmdown.checkBox2.Text = settingsform.closethisafterdownload;
                    frmdown.button1.Text = settingsform.open;
                }
            
        }
        frmDownloader frmdown = new frmDownloader();
        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            frmdown.pictureBox1.Size = new System.Drawing.Size(downloadItem.PercentComplete * 5, 20);
            frmdown.label3.Text = downloadItem.PercentComplete + "%";
            if (downloadItem.IsCancelled ) { frmdown.Close(); }
            if (downloadItem.IsComplete)
            {      
                    frmdown.downloaddone();
                    aNaFRM.Invoke(new Action(() => settingsform.RefreshDownloadList()));
            }
        }
    }
}
