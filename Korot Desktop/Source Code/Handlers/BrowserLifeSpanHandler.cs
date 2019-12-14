using CefSharp;

namespace Korot
{
    public class BrowserLifeSpanHandler : ILifeSpanHandler
    {
        frmCEF _tabform;
        public BrowserLifeSpanHandler(frmCEF tabf)
        {
            _tabform = tabf;
        }
        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName,
            WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo,
            IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            newBrowser = null;
            _tabform.NewTab(targetUrl);
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
            //
        }

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
            //nothing
        }
    }
}
