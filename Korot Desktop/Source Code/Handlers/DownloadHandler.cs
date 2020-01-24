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
                ActiveForm.Invoke(new Action(() => ActiveForm.button11.FlatAppearance.BorderSize = 1));
            }
            else
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog() { Filter = ActiveForm.allFiles + "|*.*", FilterIndex = 2, RestoreDirectory = true, FileName = downloadItem.SuggestedFileName };
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    callback.Continue(saveFileDialog1.FileName, false);
                    ActiveForm.Invoke(new Action(() => ActiveForm.button11.FlatAppearance.BorderSize = 1));
                }
            }
            browser.GoBack();
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            aNaFRM.Invoke(new Action(() => aNaFRM.removeThisDownloadItem(downloadItem)));
            aNaFRM.CurrentDownloads.Add(downloadItem);
            if (aNaFRM.CancelledDownloads.Contains(downloadItem.Url)) { aNaFRM.removeThisDownloadItem(downloadItem); aNaFRM.CurrentDownloads.Remove(downloadItem); downloadItem.IsCancelled = true; callback.Cancel(); }
            if (downloadItem.IsCancelled)
            {
                aNaFRM.CurrentDownloads.Remove(downloadItem);
                Properties.Settings.Default.DowloadHistory += "X;" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + downloadItem.FullPath + ";" + downloadItem.Url + ";"; 
            }
            if (downloadItem.IsComplete)
            {
                aNaFRM.CurrentDownloads.Remove(downloadItem);
                Properties.Settings.Default.DowloadHistory += "✓;" + DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + downloadItem.Url + ";" + downloadItem.FullPath + ";";

                if (downloadItem.SuggestedFileName.ToLower().EndsWith(".kef"))
                    {
                        frmInstallExt installExt = new frmInstallExt(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName);
                        installExt.Show();
                }else
                {
                        ActiveForm.Invoke(new Action(() => ActiveForm.RefreshDownloadList()));
                }
            }
            
        }
    }
}