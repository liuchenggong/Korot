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
    public class JsHandler : IJsDialogHandler
    {
        frmCEF Cefform;
        frmMain anaform;
        public JsHandler(frmCEF _frmCEF, frmMain _frmMain)
        {
            anaform = _frmMain;
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
            }else if (dialogType == CefJsDialogType.Confirm)
            {
                bool value = false;
                Cefform.Invoke(new Action(() => Cefform.OnJSConfirm(originUrl,messageText,out value)));
                callback.Continue(value);
                return true;
            }
            else if(dialogType == CefJsDialogType.Prompt){
                bool value = false;
                string result = null;
                Cefform.Invoke(new Action(() => Cefform.OnJSPrompt(originUrl, messageText,defaultPromptText, out value,out result)));
                callback.Continue(value, result);
                return true;
            }else
            {
                return false;
            }
                
        }


        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {

        }
    }
}
