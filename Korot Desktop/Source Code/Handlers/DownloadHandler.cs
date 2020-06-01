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
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Korot
{
    public class DownloadHandler : IDownloadHandler
    {
        private readonly frmCEF ActiveForm;
        public frmMain anaform()
        {
            return ((frmMain)ActiveForm.ParentTabs);
        }
        public DownloadHandler(frmCEF activeForm)
        {
            ActiveForm = activeForm;
        }
        public static bool ValidHaltroyWebsite(string s)
        {
            string Pattern = @"(?:http\:\/\/haltroy\.com)|(?:https\:\/\/haltroy\.com)";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s.Substring(0, 19));
        }
        public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            if (downloadItem.SuggestedFileName.ToLower().EndsWith(".kef"))
            {
                if (ValidHaltroyWebsite(downloadItem.OriginalUrl))
                {
                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName))
                    {
                        File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName);
                    }
                    downloadItem.FullPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName;
                    callback.Continue(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Korot\\DownloadTemp\\" + downloadItem.SuggestedFileName, false);
                    ActiveForm.Invoke(new Action(() => ActiveForm.btHamburger.FlatAppearance.BorderSize = 1));
                }
                else
                {
                    if (ActiveForm.Settings.Downloads.UseDownloadFolder)
                    {
                        downloadItem.FullPath = ActiveForm.Settings.Downloads.DownloadDirectory + "\\" + downloadItem.SuggestedFileName;
                        callback.Continue(ActiveForm.Settings.Downloads.DownloadDirectory + "\\" + downloadItem.SuggestedFileName, false);
                        ActiveForm.Invoke(new Action(() => ActiveForm.btHamburger.FlatAppearance.BorderSize = 1));
                    }
                    else
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog() { Filter = ActiveForm.allFiles + "|*.*", FilterIndex = 2, RestoreDirectory = true, FileName = downloadItem.SuggestedFileName };
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            downloadItem.FullPath = saveFileDialog1.FileName;
                            callback.Continue(saveFileDialog1.FileName, false);
                            ActiveForm.Invoke(new Action(() => ActiveForm.btHamburger.FlatAppearance.BorderSize = 1));
                        }
                    }
                }
            }
            else
            {
                if (ActiveForm.Settings.Downloads.UseDownloadFolder)
                {
                    downloadItem.FullPath = ActiveForm.Settings.Downloads.DownloadDirectory + "\\" + downloadItem.SuggestedFileName;
                    callback.Continue(ActiveForm.Settings.Downloads.DownloadDirectory + "\\" + downloadItem.SuggestedFileName, false);
                    ActiveForm.Invoke(new Action(() => ActiveForm.btHamburger.FlatAppearance.BorderSize = 1));
                }
                else
                {
                    SaveFileDialog saveFileDialog1 = new SaveFileDialog() { Filter = ActiveForm.allFiles + "|*.*", FilterIndex = 2, RestoreDirectory = true, FileName = downloadItem.SuggestedFileName };
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        downloadItem.FullPath = saveFileDialog1.FileName;
                        callback.Continue(saveFileDialog1.FileName, false);
                        ActiveForm.Invoke(new Action(() => ActiveForm.btHamburger.FlatAppearance.BorderSize = 1));
                    }
                }
            }
            if (chromiumWebBrowser.CanGoBack == false)
            {
                ActiveForm.Invoke(new Action(() => ActiveForm.Close()));
            }
            else
            {
                ActiveForm.Invoke(new Action(() => ActiveForm.GoBack()));
            }
        }

        public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            anaform().Invoke(new Action(() => anaform().removeThisDownloadItem(downloadItem)));
            anaform().CurrentDownloads.Add(downloadItem);
            if (anaform().CancelledDownloads.Contains(downloadItem.Url)) { anaform().removeThisDownloadItem(downloadItem); anaform().CurrentDownloads.Remove(downloadItem); downloadItem.IsCancelled = true; callback.Cancel(); }
            if (downloadItem.IsCancelled)
            {
                anaform().CurrentDownloads.Remove(downloadItem);
                Site site = new Site();
                site.Date = DateTime.Now.ToString("dd/MM/yy hh:mm:ss");
                site.Url = downloadItem.Url;
                site.LocalUrl = downloadItem.FullPath;
                site.Status = DownloadStatus.Cancelled;
                ActiveForm.Settings.Downloads.Downloads.Add(site);
            }
            if (downloadItem.IsComplete)
            {
                if (downloadItem.FullPath.ToLower().EndsWith(".kef") || downloadItem.FullPath.ToLower().EndsWith(".ktf"))
                {
                    frmInstallExt ınstallExt = new frmInstallExt(ActiveForm.Settings,downloadItem.FullPath, Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser +"\\Extensions" + Path.GetFileNameWithoutExtension(downloadItem.FullPath)));
                    ınstallExt.Show();
                }
                Site site = new Site();
                site.Date = DateTime.Now.ToString("dd/MM/yy hh:mm:ss");
                site.Url = downloadItem.Url;
                site.LocalUrl = downloadItem.FullPath;
                site.Status = DownloadStatus.Downloaded;
                ActiveForm.Settings.Downloads.Downloads.Add(site);
                anaform().CurrentDownloads.Remove(downloadItem);
                ActiveForm.Invoke(new Action(() => ActiveForm.RefreshDownloadList()));
            }

        }
    }
}