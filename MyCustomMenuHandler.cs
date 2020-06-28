

using CefSharp;

public class MyCustomMenuHandler : IContextMenuHandler
{
  public void OnBeforeContextMenu(
    IWebBrowser browserControl,
    IBrowser browser,
    IFrame frame,
    IContextMenuParams parameters,
    IMenuModel model)
  {
    if (model.Count > 0)
      model.AddSeparator();
    model.AddItem((CefMenuCommand) 26501, "Show DevTools");
    model.AddSeparator();
    model.AddItem((CefMenuCommand) 26502, "Open in new Window");
    model.AddItem((CefMenuCommand) 26503, "Open in New Tab");
  }

  public bool OnContextMenuCommand(
    IWebBrowser browserControl,
    IBrowser browser,
    IFrame frame,
    IContextMenuParams parameters,
    CefMenuCommand commandId,
    CefEventFlags eventFlags)
  {
    switch (commandId)
    {
      case (CefMenuCommand) 26501:
        browser.GetHost().ShowDevTools((IWindowInfo) null, 0, 0);
        return true;
      case (CefMenuCommand) 26502:
        return true;
      case (CefMenuCommand) 26503:
        return true;
      default:
        return false;
    }
  }

  public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
  {
  }

  public bool RunContextMenu(
    IWebBrowser browserControl,
    IBrowser browser,
    IFrame frame,
    IContextMenuParams parameters,
    IMenuModel model,
    IRunContextMenuCallback callback)
  {
    return false;
  }
}
