/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using CefSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Korot
{
    internal class DisplayHandler : IDisplayHandler
    {
        private readonly frmCEF CEFform;

        public frmMain anaform()
        {
            return ((frmMain)CEFform.ParentTabs);
        }

        public DisplayHandler(frmCEF tabform)
        {
            CEFform = tabform;
        }

        #region "Not in use"

        public void OnAddressChanged(IWebBrowser chromiumWebBrowser, AddressChangedEventArgs addressChangedArgs)
        {
            return;
        }

        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
        {
            return;
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }

        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, CefSharp.Structs.Size newSize)
        {
            return false;
        }

        #endregion "Not in use"

        public bool OnConsoleMessage(IWebBrowser chromiumWebBrowser, ConsoleMessageEventArgs consoleMessageArgs)
        {
            return true;
        }

        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            OnFaviconChange(chromiumWebBrowser, browser, urls);
        }

        public async void OnFaviconChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            await Task.Run(() =>
            {
                WebClient webc = new WebClient();
                foreach (string x in urls)
                {
                    try
                    {
                        using (Stream stream = webc.OpenRead(new Uri(x)))
                        {
                            Bitmap bitmap = new Bitmap(stream);
                            bitmap.SetResolution(72, 72);
                            Icon icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
                            CEFform.Invoke(new Action(() => CEFform.Icon = icon));
                        }
                    }
                    catch { continue; }
                }
            });
        }

        public void OnFullscreenModeChange(IWebBrowser chromiumWebBrowser, IBrowser browser, bool fullscreen)
        {
            CEFform.Invoke(new Action(() => CEFform.Fullscreenmode(fullscreen)));
        }

        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
            if (CEFform == null) { return; }
            if (CEFform.anaform == null) { return; }
            if (!CEFform.IsDisposed && !CEFform.closing && !CEFform.anaform.closing)
            {
                CEFform.Invoke(new Action(() => CEFform.ChangeProgress(progress)));
            }
        }

        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
        {
            try { CEFform.Invoke(new Action(() => CEFform.ChangeStatus(statusMessageArgs.Value))); } catch { }
        }
    }
}