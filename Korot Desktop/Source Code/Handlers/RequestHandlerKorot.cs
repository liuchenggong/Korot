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
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    class RequestHandlerKorot : IRequestHandler
    {
        frmMain anaform;
        frmCEF cefform;
        public RequestHandlerKorot(frmMain _frmMain, frmCEF _frmCEF)
        {
            anaform = _frmMain;
            cefform = _frmCEF;
        }

        public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            Task.Run(() =>
            {
                using (callback)
                {
                    if (originUrl.Contains("https://httpbin.org/basic-auth/"))
                    {
                        var parts = originUrl.Split('/');
                        var username = parts[parts.Length - 2];
                        var password = parts[parts.Length - 1];
                        callback.Continue(username, password);
                    }
                }
            });

            return true;
        }

        public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return new ResReqHandler(anaform, cefform);
        }

        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            return false;
        }

        public bool OnCertificateError(IWebBrowser chromiumWebBrowser, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            string certError = "CefErrorCode: "
                + errorCode
                + Environment.NewLine
                + "Url: "
                + requestUrl
                + Environment.NewLine
                + "SSLInfo: "
                + Environment.NewLine
                + "CertStatus: "
                + sslInfo.CertStatus
                + Environment.NewLine
                + "X509Certificate: "
                + sslInfo.X509Certificate.ToString();
            cefform.Invoke(new Action(() =>
            {
                cefform.safeStatusToolStripMenuItem.Text = cefform.CertificateErrorTitle;
                cefform.ınfoToolStripMenuItem.Text = cefform.CertificateError;
                cefform.showCertificateErrorsToolStripMenuItem.Tag = certError;
                cefform.certError = true;
                cefform.showCertificateErrorsToolStripMenuItem.Visible = true;
                cefform.pictureBox2.Image = Properties.Resources.lockr;
            }));
            if (cefform.CertAllowedUrls.Contains(requestUrl))
            {
                callback.Continue(true);
                return true;
            }
            else
            {
                cefform.Invoke(new Action(() =>
                {
                    cefform.pnlCert.Visible = true;
                    cefform.button10.Tag = requestUrl;
                    cefform.tabControl1.SelectedTab = cefform.tabPage2;
                }));
                callback.Cancel();
                return false;
            }
        }

        public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            if (userGesture)
            {
                anaform.Invoke(new Action(() => anaform.CreateTab(targetUrl)));
                return true;
            }
            else { return false; }
        }

        public void OnPluginCrashed(IWebBrowser chromiumWebBrowser, IBrowser browser, string pluginPath) { }
        public bool OnQuotaRequest(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {
            anaform.Invoke(new Action(() => anaform.Hide()));
            HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot", cefform.renderProcessDies, anaform.Icon, MessageBoxButtons.OK, Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
            if (mesaj.ShowDialog() == DialogResult.OK || mesaj.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser) { }

        public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            var control = (Control)chromiumWebBrowser;

            control.Invoke(new Action(delegate ()
            {
                var selectedCertificateCollection = X509Certificate2UI.SelectFromCollection(certificates, "Certificates Dialog", "Select Certificate for authentication", X509SelectionFlag.SingleSelection);
                if (selectedCertificateCollection.Count > 0)
                {
                    //X509Certificate2UI.SelectFromCollection returns a collection, we've used SingleSelection, so just take the first
                    //The underlying CEF implementation only accepts a single certificate
                    callback.Select(selectedCertificateCollection[0]);
                }
                else
                {
                    //User canceled no certificate should be selected.
                    callback.Select(null);
                }
            }));

            return true;
        }
    }
}
