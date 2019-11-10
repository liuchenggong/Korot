using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;

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

        public bool OnJSAlert(IWebBrowser browser, string url, string message)
        {
            HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot - Alert","Alert on url : " + url + "\n Message:" + message, anaform.Icon,System.Windows.Forms.MessageBoxButtons.OKCancel,Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
           DialogResult diag = mesaj.ShowDialog();
            return true;
        }

        public bool OnJSBeforeUnload(IWebBrowser browserControl, IBrowser browser, string message, bool isReload, IJsDialogCallback callback)
        {
            return false;
        }

        public bool OnJSConfirm(IWebBrowser browser, string url, string message, out bool retval)
        {
            HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot - Confirm", "Confirm on url : " + url + "\n Message:" + message,anaform.Icon, System.Windows.Forms.MessageBoxButtons.OKCancel,Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
            DialogResult diag = mesaj.ShowDialog();
            if (diag == DialogResult.OK) { retval = true; } else { retval = false; }
            return true;
        }

        public bool OnJSDialog(IWebBrowser browserControl, IBrowser browser, string originUrl, CefJsDialogType dialogType, string messageText, string defaultPromptText, IJsDialogCallback callback, ref bool suppressMessage)
        {
            return false;
        }

        public bool OnJSPrompt(IWebBrowser browser, string url, string message, string defaultValue, out bool retval, out string result)
        {
            HaltroyFramework.HaltroyInputBox mesaj = new HaltroyFramework.HaltroyInputBox(url, message,anaform.Icon, defaultValue,Properties.Settings.Default.BackColor,Properties.Settings.Default.OverlayColor,anaform.OK,anaform.Cancel,400,150);
            DialogResult diag = mesaj.ShowDialog();
            if (diag == DialogResult.OK) { retval = true; } else { retval = false; }
            result = mesaj.textBox1.Text;
            return true;
        }

        public void OnResetDialogState(IWebBrowser browserControl, IBrowser browser)
        {
            
        }
    }
}
