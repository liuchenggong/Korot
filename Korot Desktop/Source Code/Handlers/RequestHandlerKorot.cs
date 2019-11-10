using CefSharp;
using CefSharp.WinForms.Internals;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    class RequestHandlerKorot : IRequestHandler
    {
        frmMain anaform;
        frmCEF cefform;
        public RequestHandlerKorot(frmMain _frmMain,frmCEF _frmCEF) 
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
            
            cefform.Invoke(new Action(() => cefform.safeStatusToolStripMenuItem.Text = cefform.CertificateErrorTitle));
            cefform.Invoke(new Action(() => cefform.ınfoToolStripMenuItem.Text = cefform.CertificateError));
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
            cefform.Invoke(new Action(() => cefform.showCertificateErrorsToolStripMenuItem.Tag = certError));
            cefform.Invoke(new Action(() => cefform.certError = true));
            cefform.Invoke(new Action(() => cefform.showCertificateErrorsToolStripMenuItem.Visible = true));
            cefform.Invoke(new Action(() => cefform.pictureBox2.Image = Properties.Resources.lockr));
            if (cefform.CertAllowedUrls.Contains(requestUrl)) 
            {
                callback.Continue(true);
                return true;
            }
            else
            {
                cefform.Invoke(new Action(() => cefform.pnlCert.Visible = true));
                cefform.Invoke(new Action(() => cefform.button10.Tag = requestUrl));
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
            } else { return false; }
        }

        public void OnPluginCrashed(IWebBrowser chromiumWebBrowser, IBrowser browser, string pluginPath){}
        public bool OnQuotaRequest(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {
            chromiumWebBrowser.Load("korot://error/?=RENDER_PROCESS_TERMINATED");
        }

        public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser){}

        public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            var control = (Control)chromiumWebBrowser;

            control.InvokeOnUiThreadIfRequired(delegate ()
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
            });

            return true;
        }
    }
}
