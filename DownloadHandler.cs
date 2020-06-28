

using CefSharp;
using System;

namespace Korot
{
  public class DownloadHandler : IDownloadHandler
  {
    public event EventHandler<DownloadItem> OnBeforeDownloadFired;

    public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

    public void OnBeforeDownload(
      IBrowser browser,
      DownloadItem downloadItem,
      IBeforeDownloadCallback callback)
    {
      EventHandler<DownloadItem> beforeDownloadFired = this.OnBeforeDownloadFired;
      if (beforeDownloadFired != null)
        beforeDownloadFired((object) this, downloadItem);
      if (callback.IsDisposed)
        return;
      using (callback)
        callback.Continue(downloadItem.SuggestedFileName, true);
    }

    public void OnDownloadUpdated(
      IBrowser browser,
      DownloadItem downloadItem,
      IDownloadItemCallback callback)
    {
      EventHandler<DownloadItem> downloadUpdatedFired = this.OnDownloadUpdatedFired;
      if (downloadUpdatedFired == null)
        return;
      downloadUpdatedFired((object) this, downloadItem);
    }
  }
}
