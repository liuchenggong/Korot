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
using System.Windows.Forms;

namespace Korot
{
    internal class ContextMenuHandler : IContextMenuHandler
    {

        private const int ShowDevTools = 26501;
        private const int CloseDevTools = 26502;
        private const int SaveImageAs = 26503;
        private const int SaveLinkAs = 26505;
        private const int CopyLinkAddress = 26506;
        private const int OpenLinkInNewTab = 26507;
        private const int SeacrhOrOpenSelectedInNewTab = 40007;
        private const int RefreshTab = 40008;
        private const int OpenImageInNewTab = 26508;
        frmCEF ActiveForm;
        frmMain anafrm;

        private string lastSelText = "";

        public ContextMenuHandler(frmCEF activeform, frmMain aNaform)
        {
            ActiveForm = activeform;
            anafrm = aNaform;
        }

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {

            // clear the menu
            model.Clear();
            // save text
            lastSelText = parameters.SelectionText;

            model.AddItem(CefMenuCommand.Back, ActiveForm.goBack);
            model.AddItem(CefMenuCommand.Forward, ActiveForm.goForward);
            model.AddItem((CefMenuCommand)RefreshTab, ActiveForm.refresh);
            model.AddItem(CefMenuCommand.ReloadNoCache, ActiveForm.refreshNoCache);
            model.AddItem(CefMenuCommand.StopLoad, ActiveForm.stop);
            model.AddItem(CefMenuCommand.SelectAll, ActiveForm.selectAll);
            model.AddSeparator();

            //Removing existing menu item
            //bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option
            if (parameters.LinkUrl != "")
            {
                model.AddItem((CefMenuCommand)OpenLinkInNewTab, ActiveForm.openLinkInNewTab);
                model.AddItem((CefMenuCommand)CopyLinkAddress, ActiveForm.copyLink);
            }
            if (parameters.HasImageContents && parameters.SourceUrl.Length > 0)
            {

                // RIGHT CLICKED ON IMAGE
                model.AddItem((CefMenuCommand)OpenImageInNewTab, ActiveForm.openImageInNewTab);
                model.AddItem((CefMenuCommand)SaveImageAs, ActiveForm.saveImageAs);
                model.AddSeparator();
            }
            if (parameters.IsEditable)
            {
                model.AddItem(CefMenuCommand.Paste, ActiveForm.paste);
            }
            if (parameters.SelectionText != "")
            {

                // TEXT IS SELECTED
                model.AddItem(CefMenuCommand.Copy, ActiveForm.copy);
                if (parameters.IsEditable)
                {
                    model.AddItem(CefMenuCommand.Cut, ActiveForm.cut);

                    model.AddItem(CefMenuCommand.Undo, ActiveForm.undo);
                    model.AddItem(CefMenuCommand.Redo, ActiveForm.redo);
                    model.AddItem(CefMenuCommand.Delete, ActiveForm.delete);
                }
                model.AddItem((CefMenuCommand)SeacrhOrOpenSelectedInNewTab, ActiveForm.SearchOrOpenSelectedInNewTab);
                model.AddSeparator();
            }


            //Add new custom menu items
            //#if DEBUG
            model.AddItem((CefMenuCommand)ShowDevTools, ActiveForm.developerTools);
            model.AddItem(CefMenuCommand.ViewSource, ActiveForm.viewSource);
            //#endif



        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {

            int id = (int)commandId;

            if (id == ShowDevTools)
            {
                browser.ShowDevTools();
            }
            if (id == CloseDevTools)
            {
                browser.CloseDevTools();
            }
            if (id == SaveImageAs)
            {
                browser.GetHost().StartDownload(parameters.SourceUrl);
            }
            if (id == SaveLinkAs)
            {
                browser.GetHost().StartDownload(parameters.LinkUrl);
            }
            if (id == OpenLinkInNewTab)
            {
                browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.LinkUrl + "')");
                // ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.LinkUrl)));
                // ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.LinkUrl))); blank
                // browserControl.ExecuteScriptAsync("window.open('" + parameters.LinkUrl + "')"); => CEFSHARP ONLY SUPPORT V8CONTEXT FOR NOW
            }
            if (id == CopyLinkAddress)
            {
                Clipboard.SetText(parameters.LinkUrl);

            }
            if (id == OpenImageInNewTab)
            {
                browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.SourceUrl + "')");
                //ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.SourceUrl)));
                //browserControl.ExecuteScriptAsync("window.open('" + parameters.SourceUrl + "')"); => CEFSHARP ONLY SUPPORT V8CONTEXT FOR NOW
            }
            if (id == SeacrhOrOpenSelectedInNewTab)
            {
                // ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.SelectionText)));
                // browserControl.ExecuteScriptAsync("window.open('" + parameters.SelectionText + "')"); => CEFSHARP ONLY SUPPORT V8CONTEXT FOR NOW
                browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.SelectionText + "')");
            }

            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {

            // show default menu
            return false;
        }
    }
}
