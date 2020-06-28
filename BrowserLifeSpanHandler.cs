

using CefSharp;

namespace Korot
{
  public class BrowserLifeSpanHandler : ILifeSpanHandler
  {
    private tabform _tabform;

    public BrowserLifeSpanHandler(tabform tabf)
    {
      this._tabform = tabf;
    }

    public bool OnBeforePopup(
      IWebBrowser browserControl,
      IBrowser browser,
      IFrame frame,
      string targetUrl,
      string targetFrameName,
      WindowOpenDisposition targetDisposition,
      bool userGesture,
      IPopupFeatures popupFeatures,
      IWindowInfo windowInfo,
      IBrowserSettings browserSettings,
      ref bool noJavascriptAccess,
      out IWebBrowser newBrowser)
    {
      newBrowser = (IWebBrowser) null;
      this._tabform.NewTab(targetUrl);
      return true;
    }

    public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
    {
    }

    public bool DoClose(IWebBrowser browserControl, IBrowser browser)
    {
      return false;
    }

    public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
    {
    }
  }
}
