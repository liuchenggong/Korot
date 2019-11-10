using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Korot
{
    class CookieAccessFilter : ICookieAccessFilter
    {
        frmMain anaform;
        frmCEF Cefform;
        public CookieAccessFilter(frmMain _anaform, frmCEF _Cefform)
        {
            anaform = _anaform;
            Cefform = _Cefform;
        }
        public bool CanSaveCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, Cookie cookie)
        {
            Cefform.Invoke(new Action(() => Cefform.cookieInfoToolStripMenuItem.Text = Cefform.usesCookies));
            Cefform.Invoke(new Action(() => Cefform.cookieUsage = true));
            if (!Cefform.certError) { Cefform.Invoke(new Action(() => Cefform.pictureBox2.Image = Properties.Resources.locko)); }
            return true;
        }

        public bool CanSendCookie(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
        {
            Cefform.Invoke(new Action(() => Cefform.cookieInfoToolStripMenuItem.Text = Cefform.usesCookies));
            Cefform.Invoke(new Action(() => Cefform.cookieUsage = true));
            if (!Cefform.certError) { Cefform.Invoke(new Action(() => Cefform.pictureBox2.Image = Properties.Resources.locko)); }
            return true;
        }
    }
}
