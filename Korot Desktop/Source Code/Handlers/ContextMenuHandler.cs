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
using System.Windows.Forms;

namespace Korot
{
    internal class ContextMenuHandler : IContextMenuHandler
    {
        frmCEF ActiveForm;
        frmMain anafrm;

        private string lastSelText = "";

        public ContextMenuHandler(frmCEF activeform, frmMain aNaform) { ActiveForm = activeform; anafrm = aNaform; }

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear();
            lastSelText = parameters.SelectionText;
            ActiveForm.Invoke(new Action(() =>
            {
                ActiveForm.cmsCef.Show(Cursor.Position);
                ActiveForm.tsSep1.Visible = false;
                ActiveForm.tsSep2.Visible = false;
                ActiveForm.openLinkInNewTabToolStripMenuItem.Visible = false;
                ActiveForm.copyLinkAddressToolStripMenuItem.Visible = false;
                ActiveForm.openImageInNewTabToolStripMenuItem.Visible = false;
                ActiveForm.saveImageAsToolStripMenuItem.Visible = false;
                ActiveForm.pasteToolStripMenuItem.Visible = false;
                ActiveForm.copyToolStripMenuItem.Visible = false;
                ActiveForm.searchOrOpenSelectedInNewTabToolStripMenuItem.Visible = false;
                ActiveForm.cutToolStripMenuItem.Visible = false;
                ActiveForm.undoToolStripMenuItem.Visible = false;
                ActiveForm.redoToolStripMenuItem.Visible = false;
                ActiveForm.deleteToolStripMenuItem.Visible = false;
            }));
            if (parameters.LinkUrl != "")
            {
                ActiveForm.Invoke(new Action(() =>
                {
                    ActiveForm.tsSep1.Visible = true;
                    ActiveForm.openLinkInNewTabToolStripMenuItem.Tag = parameters.LinkUrl;
                    ActiveForm.copyLinkAddressToolStripMenuItem.Tag = parameters.LinkUrl;
                    ActiveForm.openLinkInNewTabToolStripMenuItem.Visible = true;
                    ActiveForm.copyLinkAddressToolStripMenuItem.Visible = true;
                }));
            }
            if (parameters.HasImageContents && parameters.SourceUrl.Length > 0)
            {
                ActiveForm.Invoke(new Action(() =>
                {
                    ActiveForm.tsSep1.Visible = true;
                    ActiveForm.openImageInNewTabToolStripMenuItem.Tag = parameters.SourceUrl;
                    ActiveForm.saveImageAsToolStripMenuItem.Tag = parameters.SourceUrl;
                    ActiveForm.openImageInNewTabToolStripMenuItem.Visible = true;
                    ActiveForm.saveImageAsToolStripMenuItem.Visible = true;
                }));
            }
            if (parameters.IsEditable)
            {
                ActiveForm.Invoke(new Action(() =>
                {
                    ActiveForm.tsSep2.Visible = true;
                    ActiveForm.pasteToolStripMenuItem.Visible = true;
                }));
            }
            if (parameters.SelectionText != "")
            {
                ActiveForm.Invoke(new Action(() =>
                {
                    ActiveForm.tsSep2.Visible = true;
                    ActiveForm.copyToolStripMenuItem.Visible = true;
                    ActiveForm.searchOrOpenSelectedInNewTabToolStripMenuItem.Tag = parameters.SelectionText;
                    ActiveForm.searchOrOpenSelectedInNewTabToolStripMenuItem.Visible = true;
                }));
                if (parameters.IsEditable)
                {
                    ActiveForm.Invoke(new Action(() =>
                    {
                        ActiveForm.tsSep2.Visible = true;
                        ActiveForm.cutToolStripMenuItem.Visible = true;
                        ActiveForm.undoToolStripMenuItem.Visible = true;
                        ActiveForm.redoToolStripMenuItem.Visible = true;
                        ActiveForm.deleteToolStripMenuItem.Visible = true;
                    }));
                }
            }
        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags) { return false; }
        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) { }
        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback) { return false; }
    }
}
