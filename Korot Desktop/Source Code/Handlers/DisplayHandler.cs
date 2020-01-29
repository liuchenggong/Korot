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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

namespace Korot
{
    class DisplayHandler : IDisplayHandler
    {
        frmCEF CEFform;
        frmMain aNaFRM;
        public DisplayHandler(frmCEF tabform, frmMain anaform)
        {
            CEFform = tabform;
            aNaFRM = anaform;
        }
        public void OnAddressChanged(IWebBrowser chromiumWebBrowser, AddressChangedEventArgs addressChangedArgs)
        {

        }

        public bool OnAutoResize(IWebBrowser chromiumWebBrowser, IBrowser browser, CefSharp.Structs.Size newSize)
        {
            return false;
        }

        public bool OnConsoleMessage(IWebBrowser chromiumWebBrowser, ConsoleMessageEventArgs consoleMessageArgs)
        {
            Output.WriteLine("[Korot.DisplayHandler.OnConsoleMessage] (" + consoleMessageArgs.Source + ") " + consoleMessageArgs.Message);
            return false;
        }

        public void OnFaviconUrlChange(IWebBrowser chromiumWebBrowser, IBrowser browser, IList<string> urls)
        {
            WebClient webc = new WebClient();
            foreach (String x in urls)
            {
                try
                {
                    using (Stream stream = webc.OpenRead(new Uri(x)))
                    {
                        Bitmap bitmap = new Bitmap(stream);
                        bitmap.SetResolution(72, 72);
                        Icon icon = System.Drawing.Icon.FromHandle(bitmap.GetHicon());
                        CEFform.Invoke(new Action(() => CEFform.Icon = icon));
                        //CEFform.ParentTabs.Invoke(new Action(() => CEFform.ParentTabs.Icon = icon));

                    }
                }
                catch { continue; }
            }
        }

        public void OnFullscreenModeChange(IWebBrowser chromiumWebBrowser, IBrowser browser, bool fullscreen)
        {
            aNaFRM.Invoke(new Action(() => aNaFRM.Fullscreenmode(fullscreen)));

        }

        public void OnLoadingProgressChange(IWebBrowser chromiumWebBrowser, IBrowser browser, double progress)
        {
            try
            {
                CEFform.Invoke(new Action(() => CEFform.ChangeProgress(progress)));
            }
            catch { }
        }

        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
        {
            try { CEFform.Invoke(new Action(() => CEFform.ChangeStatus(statusMessageArgs.Value))); } catch { }
        }

        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
        {
            //if you are reading this then drink some water idk but dont die
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }
    }
}
