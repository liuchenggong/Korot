using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace Korot {
	internal class ContextMenuHandler : IContextMenuHandler {

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
        frmMain aNaFRM;

		private string lastSelText = "";

		public ContextMenuHandler(frmCEF activeform,frmMain anaform) {

            ActiveForm = activeform;
            aNaFRM = anaform;
		}

		public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model) {
			
			// clear the menu
			model.Clear();
			// save text
			lastSelText = parameters.SelectionText;

            model.AddItem(CefMenuCommand.Back, aNaFRM.goBack);
            model.AddItem(CefMenuCommand.Forward, aNaFRM.goForward);
            model.AddItem((CefMenuCommand)RefreshTab, aNaFRM.refresh);
            model.AddItem(CefMenuCommand.ReloadNoCache, aNaFRM.refreshNoCache);
            model.AddItem(CefMenuCommand.StopLoad, aNaFRM.stop);
            model.AddItem(CefMenuCommand.SelectAll, aNaFRM.selectAll);
            model.AddSeparator();

			//Removing existing menu item
			//bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option
			if (parameters.LinkUrl != "") {
				model.AddItem((CefMenuCommand)OpenLinkInNewTab, aNaFRM.openLinkInNewTab);
				model.AddItem((CefMenuCommand)CopyLinkAddress,aNaFRM.copyLink);
			}
			if (parameters.HasImageContents && parameters.SourceUrl.Length > 0) {

                // RIGHT CLICKED ON IMAGE
                model.AddItem((CefMenuCommand)OpenImageInNewTab,aNaFRM.openImageInNewTab );
                model.AddItem((CefMenuCommand)SaveImageAs, aNaFRM.saveImageAs);
                model.AddSeparator();
			}
            if (parameters.IsEditable)
            {
                model.AddItem(CefMenuCommand.Paste, aNaFRM.paste);
            }
            if (parameters.SelectionText != "") {

                // TEXT IS SELECTED
                model.AddItem(CefMenuCommand.Copy, aNaFRM.copy);
                if (parameters.IsEditable)
                {
                    model.AddItem(CefMenuCommand.Cut, aNaFRM.cut);

                    model.AddItem(CefMenuCommand.Undo, aNaFRM.undo);
                    model.AddItem(CefMenuCommand.Redo, aNaFRM.redo);
                    model.AddItem(CefMenuCommand.Delete, aNaFRM.delete);
                }
                model.AddItem((CefMenuCommand)SeacrhOrOpenSelectedInNewTab, aNaFRM.SearchOrOpenSelectedInNewTab);
                model.AddSeparator();
            }


			//Add new custom menu items
			//#if DEBUG
			model.AddItem((CefMenuCommand)ShowDevTools,  aNaFRM.developerTools);
			model.AddItem(CefMenuCommand.ViewSource, aNaFRM.viewSource);
			//#endif



        }

		public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags) {

			int id = (int)commandId;

			if (id == ShowDevTools) {
				browser.ShowDevTools();
			}
			if (id == CloseDevTools) {
				browser.CloseDevTools();
			}
			if (id == SaveImageAs) {
				browser.GetHost().StartDownload(parameters.SourceUrl);
			}
			if (id == SaveLinkAs) {
				browser.GetHost().StartDownload(parameters.LinkUrl);
			}
			if (id == OpenLinkInNewTab) {
                browserControl.EvaluateScriptAsync("window.open('" + parameters.LinkUrl + "')");
               // ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.LinkUrl)));
                // aNaFRM.Invoke(new Action(() => aNaFRM.NewTab(parameters.LinkUrl))); blank
               // browserControl.ExecuteScriptAsync("window.open('" + parameters.LinkUrl + "')"); => CEFSHARP ONLY SUPPORT V8CONTEXT FOR NOW
			}
			if (id == CopyLinkAddress) {
				Clipboard.SetText(parameters.LinkUrl);

			}
			if (id == OpenImageInNewTab)
            {
                browserControl.EvaluateScriptAsync("window.open('" + parameters.SourceUrl + "')");
                //ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.SourceUrl)));
                //browserControl.ExecuteScriptAsync("window.open('" + parameters.SourceUrl + "')"); => CEFSHARP ONLY SUPPORT V8CONTEXT FOR NOW
            }
            if (id == SeacrhOrOpenSelectedInNewTab)
            {
               // ActiveForm.Invoke(new Action(() => ActiveForm.NewTab(parameters.SelectionText)));
                // browserControl.ExecuteScriptAsync("window.open('" + parameters.SelectionText + "')"); => CEFSHARP ONLY SUPPORT V8CONTEXT FOR NOW
                browserControl.EvaluateScriptAsync("window.open('" + parameters.SelectionText + "')");
            }

			return false;
		}

		public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) {

		}

		public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback) {

			// show default menu
			return false;
		}
	}
}
