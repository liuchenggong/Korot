using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.Structs;
using Korot;
using Korot.Properties;

namespace Korot
{
    class DisplayHandler : IDisplayHandler
    {
        frmCEF CEFform;
        frmMain aNaFRM;
        public DisplayHandler (frmCEF tabform,frmMain anaform)
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
                        Image favicon = Image.FromStream(stream);
                        aNaFRM.Invoke(new Action(() => aNaFRM.ımageList2.Images.Add(x,favicon)));
                        CEFform.Parent.Invoke(new Action(() => ((System.Windows.Forms.TabPage)CEFform.Parent).ImageKey = x));
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
            //we done this but i still need that progress so
            try
            {
                CEFform.Invoke(new Action(() => CEFform.ChangeProgress(Convert.ToInt32(progress * 100))));
            }catch { }
        }

        public void OnStatusMessage(IWebBrowser chromiumWebBrowser, StatusMessageEventArgs statusMessageArgs)
        {
            CEFform.Invoke(new Action(() => CEFform.ChangeStatus(statusMessageArgs.Value)));
        }

        public void OnTitleChanged(IWebBrowser chromiumWebBrowser, TitleChangedEventArgs titleChangedArgs)
        {
            //no need
        }

        public bool OnTooltipChanged(IWebBrowser chromiumWebBrowser, ref string text)
        {
            return false;
        }
    }
}
