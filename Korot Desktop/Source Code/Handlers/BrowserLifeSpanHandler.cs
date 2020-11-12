/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using CefSharp.WinForms;
using System;
using System.Windows.Forms;

namespace Korot
{
    public class BrowserLifeSpanHandler : ILifeSpanHandler
    {
        private readonly frmCEF tabform;

        public BrowserLifeSpanHandler(frmCEF tabf)
        {
            tabform = tabf;
        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName,
            WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo,
            IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            if (targetDisposition == WindowOpenDisposition.NewPopup)
            {
                frmPopup popup = new frmPopup(tabform, tabform.userName, targetUrl)
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = new System.Drawing.Point(popupFeatures.X, popupFeatures.Y),
                    Width = popupFeatures.Width,
                    Height = popupFeatures.Height,
                };
                popup.Show();
            }
            else
            {
                tabform.NewTab(targetUrl);
            }
            newBrowser = null;
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            IntPtr windowHandle = browser.GetHost().GetWindowHandle();
            ChromiumWebBrowser webBrowser = (ChromiumWebBrowser)browserControl;

            if (browser.MainFrame.Url.ToLower().StartsWith("devtools://"))
            {
                Control parentControl = Control.FromChildHandle(windowHandle);

                //If the windowHandle doesn't have a matching WinForms control
                //then we assume it's hosted by a native popup window (the default)
                //and allow the default behaviour which sends a WM_CLOSE message
                if (parentControl == null)
                {
                    return false;
                }

                //Dispose of the parent control we used to host DevTools, this will release the DevTools window handle
                //and the ILifeSpanHandler.OnBeforeClose() will be call after.
                webBrowser.Invoke(new Action(() =>
                {
                    parentControl.Dispose();
                }));

                return true;
            }

            //If browser is disposed or the handle has been released then we don't
            //need to remove the tab (likely removed from menu)
            if (!webBrowser.IsDisposed && webBrowser.IsHandleCreated)
            {
                webBrowser.Invoke(new Action(() =>
                {
                    if (webBrowser.FindForm() is frmCEF owner)
                    {
                        owner.Close();
                        //owner.RemoveTab(windowHandle);
                    }
                }));
            }

            //return true here to handle closing yourself (no WM_CLOSE will be sent).
            return true;
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
        }
    }
}