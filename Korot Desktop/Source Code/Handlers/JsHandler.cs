/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using CefSharp;
using System;

namespace Korot
{
    public class JsHandler : IJsDialogHandler
    {
        private readonly frmCEF Cefform;

        public JsHandler(frmCEF _frmCEF)
        {
            Cefform = _frmCEF;
        }

        public bool OnBeforeUnloadDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, string messageText, bool isReload, IJsDialogCallback callback)
        {
            return false;
        }

        public void OnDialogClosed(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
        {
            return false;
        }

        public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            if (dialogType == CefJsDialogType.Alert)
            {
                Cefform.Invoke(new Action(() => Cefform.OnJSAlert(originUrl, messageText)));
                return true;
            }
            else if (dialogType == CefJsDialogType.Confirm)
            {
                bool value = false;
                Cefform.Invoke(new Action(() => Cefform.OnJSConfirm(originUrl, messageText, out value)));
                callback.Continue(value);
                return true;
            }
            else if (dialogType == CefJsDialogType.Prompt)
            {
                bool value = false;
                string result = null;
                Cefform.Invoke(new Action(() => Cefform.OnJSPrompt(originUrl, messageText, defaultPromptText, out value, out result)));
                callback.Continue(value, result);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {
        }
    }
}