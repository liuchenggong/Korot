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

namespace Korot
{
    internal class CookieAccessFilter : ICookieAccessFilter
    {
        private readonly frmCEF Cefform;
        public frmMain anaform()
        {
            return ((frmMain)Cefform.ParentTabs);
        }
        public CookieAccessFilter(frmCEF _Cefform)
        {
            Cefform = _Cefform;
        }
        public bool CanSaveCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, Cookie cookie)
        {
            if (!Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser.Address))
            {
                if (Cefform != null)
                {
                    if (anaform() != null)
                    {
                        if ((!Cefform.IsDisposed) || !Cefform.closing)
                        {
                            if (!Cefform.IsDisposed)
                            {
                                Cefform.Invoke(new Action(() => Cefform.cookieInfoToolStripMenuItem.Text = Cefform.usesCookies));
                                Cefform.Invoke(new Action(() => Cefform.cookieUsage = true));
                                if (!Cefform.certError) { Cefform.Invoke(new Action(() => Cefform.pictureBox2.Image = Properties.Resources.locko)); }
                            }
                        }
                    }
                }
            }
            return !Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser.Address);
        }

        public bool CanSendCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            if (!Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser.Address))
            {
                if (Cefform != null)
                {
                    if (anaform() != null)
                    {
                        if (!(Cefform.IsDisposed) || !Cefform.closing || !Cefform.anaform().closing)
                        {
                            Cefform.Invoke(new Action(() => Cefform.cookieInfoToolStripMenuItem.Text = Cefform.usesCookies));
                            Cefform.Invoke(new Action(() => Cefform.cookieUsage = true));
                            if (!Cefform.certError) { Cefform.Invoke(new Action(() => Cefform.pictureBox2.Image = Properties.Resources.locko)); }
                        }
                    }
                }
            }
            return !Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser.Address);
        }
    }
}
