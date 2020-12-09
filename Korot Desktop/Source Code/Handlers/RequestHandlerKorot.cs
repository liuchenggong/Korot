/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Korot
{
    internal class RequestHandlerKorot : IRequestHandler
    {
        public frmMain anaform()
        {
            return ((frmMain)cefform.ParentTabs);
        }

        private readonly frmCEF cefform;

        public RequestHandlerKorot(frmCEF _frmCEF)
        {
            cefform = _frmCEF;
        }

        public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            return new ResReqHandler(cefform);
        }

        public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            if (!string.IsNullOrWhiteSpace(request.Url))
            {
                if (!(request.TransitionType == TransitionType.AutoSubFrame
                    || request.TransitionType == TransitionType.SourceMask
                    || request.TransitionType == TransitionType.ForwardBack
                    || request.TransitionType == TransitionType.Reload
                    || request.TransitionType == TransitionType.ChainStart
                    || request.TransitionType == TransitionType.IsRedirect
                    || request.TransitionType == TransitionType.ClientRedirect
                    || request.TransitionType == TransitionType.ServerRedirect))
                {
                    if (request.Url.ToLowerInvariant().StartsWith("korot") && (
                        request.Url.ToLowerInvariant().StartsWith("korot://newtab")
                              || request.Url.ToLowerInvariant().StartsWith("korot://links")
                              || request.Url.ToLowerInvariant().StartsWith("korot://license")
                              || request.Url.ToLowerInvariant().StartsWith("korot://incognito")))
                    {
                        cefform.Invoke(new Action(() => cefform.redirectTo(request.Url, request.Url)));
                    }
                    else
                    {
                        if (request.Url.ToLowerInvariant().StartsWith("devtools")) { return false; }
                        cefform.Invoke(new Action(() => cefform.redirectTo(request.Url, request.Url)));
                    }
                }
            }
            else
            {
                cefform.Invoke(new Action(() => { cefform.SessionSystem.GoBack(cefform.chromiumWebBrowser1); }));
            }
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
                cefform.certificatedetails = certError;
                cefform.certErrorUrl = requestUrl;
                cefform.certError = true;

                cefform.pbPrivacy.Image = Properties.Resources.lockr;
            }));
            if (cefform.CertAllowedUrls.Contains(requestUrl))
            {
                callback.Continue(true);
                return true;
            }
            else
            {
                cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.Load("korot://certerror")));
                callback.Cancel();
                return false;
            }
        }

        public bool OnOpenUrlFromTab(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            if (userGesture)
            {
                anaform().Invoke(new Action(() => anaform().CreateTab(targetUrl)));
                return true;
            }
            else { return false; }
        }

        public void OnPluginCrashed(IWebBrowser chromiumWebBrowser, IBrowser browser, string pluginPath)
        {
        }

        public bool OnQuotaRequest(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            callback.Dispose();
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser chromiumWebBrowser, IBrowser browser, CefTerminationStatus status)
        {
            anaform().Invoke(new Action(() => anaform().Hide()));
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot", cefform.anaform.renderProcessDies, new HTAlt.WinForms.HTDialogBoxContext(MessageBoxButtons.OK)) { Yes = cefform.anaform.Yes, No = cefform.anaform.No, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor, BackColor = cefform.Settings.Theme.BackColor, Icon = cefform.anaform.Icon };
            if (mesaj.ShowDialog() == DialogResult.OK || mesaj.ShowDialog() == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        public void OnRenderViewReady(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
        }

        public bool OnSelectClientCertificate(IWebBrowser chromiumWebBrowser, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            Control control = (Control)chromiumWebBrowser;

            control.Invoke(new Action(delegate ()
            {
                X509Certificate2Collection selectedCertificateCollection = X509Certificate2UI.SelectFromCollection(certificates, "Certificates Dialog", "Select Certificate for authentication", X509SelectionFlag.SingleSelection);
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

        public void OnDocumentAvailableInMainFrame(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            // absolutely do nothing
        }
    }
}