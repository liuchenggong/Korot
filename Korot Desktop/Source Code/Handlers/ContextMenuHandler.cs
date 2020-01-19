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

        public ContextMenuHandler(frmCEF activeform, frmMain aNaform) { ActiveForm = activeform; anafrm = aNaform; }

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear();
            lastSelText = parameters.SelectionText;
            model.AddItem(CefMenuCommand.Back, ActiveForm.goBack);
            model.AddItem(CefMenuCommand.Forward, ActiveForm.goForward);
            model.AddItem((CefMenuCommand)RefreshTab, ActiveForm.refresh);
            model.AddItem(CefMenuCommand.ReloadNoCache, ActiveForm.refreshNoCache);
            model.AddItem(CefMenuCommand.StopLoad, ActiveForm.stop);
            model.AddItem(CefMenuCommand.SelectAll, ActiveForm.selectAll);
            model.AddSeparator();
            if (parameters.LinkUrl != "")
            {
                model.AddItem((CefMenuCommand)OpenLinkInNewTab, ActiveForm.openLinkInNewTab);
                model.AddItem((CefMenuCommand)CopyLinkAddress, ActiveForm.copyLink);
            }
            if (parameters.HasImageContents && parameters.SourceUrl.Length > 0)
            {
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
            model.AddItem(CefMenuCommand.Print, ActiveForm.print);
            model.AddItem((CefMenuCommand)ShowDevTools, ActiveForm.developerTools);
            model.AddItem(CefMenuCommand.ViewSource, ActiveForm.viewSource);
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
            }
            if (id == CopyLinkAddress)
            {
                Clipboard.SetText(parameters.LinkUrl);
            }
            if (id == OpenImageInNewTab)
            {
                browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.SourceUrl + "')");
            }
            if (id == SeacrhOrOpenSelectedInNewTab)
            {
                browserControl.GetMainFrame().ExecuteJavaScriptAsync("window.open('" + parameters.SelectionText + "')");
            }
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) { }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback) { return false; }
    }
}
