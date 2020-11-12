/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using System;

namespace Korot
{
    internal class CookieAccessFilter : ICookieAccessFilter
    {
        private readonly frmCEF Cefform;

        public CookieAccessFilter(frmCEF _Cefform)
        {
            Cefform = _Cefform;
        }

        public bool CanSaveCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, Cookie cookie)
        {
            setCookie(chromiumWebBrowser);
            if (Cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(chromiumWebBrowser.Address)) != null)
            {
                return Cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(chromiumWebBrowser.Address)).AllowCookies;
            }
            else
            {
                return true;
            }
        }

        public bool CanSendCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            setCookie(chromiumWebBrowser);
            if (Cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(chromiumWebBrowser.Address)) != null)
            {
                return Cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(chromiumWebBrowser.Address)).AllowCookies;
            }
            else
            {
                return true;
            }
        }

        public void setCookie(IWebBrowser chromiumWebBrowser)
        {
            if (Cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(chromiumWebBrowser.Address)) != null)
            {
                if (Cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(chromiumWebBrowser.Address)).AllowCookies)
                {
                    if (Cefform == null) { return; }
                    if (Cefform.anaform == null) { return; }
                    if (Cefform.closing) { return; }
                    if (Cefform.anaform.closing) { return; }
                    if (Cefform.IsDisposed) { return; }
                    if (Cefform.anaform.IsDisposed) { return; }
                    Cefform.Invoke(new Action(() => Cefform.cookieUsage = true));
                    if (!Cefform.certError) { Cefform.Invoke(new Action(() => Cefform.pbPrivacy.Image = Properties.Resources.locko)); }
                }
            }
        }
    }
}