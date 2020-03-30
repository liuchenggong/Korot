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
            newBrowser = null;
            tabform.NewTab(targetUrl);
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            IntPtr windowHandle = browser.GetHost().GetWindowHandle();
            ChromiumWebBrowser webBrowser = (ChromiumWebBrowser)browserControl;

            if (browser.MainFrame.Url.Equals("devtools://devtools/devtools_app.html"))
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
