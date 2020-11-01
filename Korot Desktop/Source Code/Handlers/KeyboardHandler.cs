/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public class KeyboardHandler : IKeyboardHandler
    {
        private readonly frmCEF _frmCEF;
        private const int VK_CONTROL = 0x11;
        private const int VK_UP = 0x26;
        private const int VK_DOWN = 0x28;
        private const int VK_BROWSER_BACK = 0xA6;
        private const int VK_BROWSER_FORWARD = 0xA7;
        private const int VK_BROWSER_REFRESH = 0xA8;
        private const int VK_BROWSER_STOP = 0xA9;
        private const int VK_BROWSER_SEARCH = 0xAA;
        private const int VK_BROWSER_HOME = 0xAC;
        private const int VK_PRIOR = 0x21;
        private const int VK_NEXT = 0x22;
        private const int keyF = 0x46;
        private const int keyN = 0x4E;
        private const int keyS = 0x53;
        private const int keyM = 0x4D;
        private const int VK_F11 = 0x7A;
        private const int VK_SNAPSHOT = 0x2C;

        public KeyboardHandler(frmCEF FrmCEF)
        {
            _frmCEF = FrmCEF;
        }

        public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            _frmCEF.Invoke(new Action(() => _frmCEF.isControlKeyPressed = ((modifiers == CefEventFlags.ControlDown) || windowsKeyCode == VK_CONTROL)));
            if (windowsKeyCode == VK_BROWSER_BACK)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(0)));
                //_frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserBack)); }));
                return true;
            }
            else if (windowsKeyCode == VK_BROWSER_FORWARD)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(1)));
                //_frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserForward)); }));
                return true;
            }
            else if (windowsKeyCode == VK_BROWSER_REFRESH)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(2)));
                //  _frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserRefresh)); }));
                return true;
            }
            else if (windowsKeyCode == VK_BROWSER_STOP)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(3)));
                //_frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserStop)); }));

                return true;
            }
            else if (windowsKeyCode == keyM && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(6)));
                //_frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserForward)); }));
                return true;
            }
            else if (windowsKeyCode == VK_F11)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(5)));
                //_frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserStop)); }));

                return true;
            }
            else if (windowsKeyCode == VK_BROWSER_SEARCH)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => { _frmCEF.showHideSearchMenu(); }));
                return true;
            }
            else if (windowsKeyCode == VK_BROWSER_HOME)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.retrieveKey(4)));
                // _frmCEF.Invoke(new Action(() => { _frmCEF.tabform_KeyDown(chromiumWebBrowser, new KeyEventArgs(Keys.BrowserHome)); }));
                return true;
            }
            else if ((windowsKeyCode == VK_PRIOR || windowsKeyCode == VK_UP) && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                //_frmCEF.Invoke(new Action(() => { _frmCEF.zoomIn(); }));
                Task<double> zoomLevel = chromiumWebBrowser.GetZoomLevelAsync();
                if (zoomLevel.Result <= 8)
                {
                    chromiumWebBrowser.SetZoomLevel(zoomLevel.Result + 0.25);
                }
                return true;
            }
            else if ((windowsKeyCode == VK_NEXT || windowsKeyCode == VK_DOWN) && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                Task<double> zoomLevel = chromiumWebBrowser.GetZoomLevelAsync();
                if (zoomLevel.Result >= -0.75)
                {
                    chromiumWebBrowser.SetZoomLevel(zoomLevel.Result - 0.25);
                }
                //_frmCEF.Invoke(new Action(() => { _frmCEF.zoomOut(); }));
                return true;
            }
            else if (windowsKeyCode == keyF && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => { _frmCEF.showHideSearchMenu(); }));
                return true;
            }
            else if (windowsKeyCode == keyS && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => { _frmCEF.savePage(); }));
                return true;
            }
            else if (windowsKeyCode == VK_SNAPSHOT && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => { _frmCEF.takeScreenShot(); }));
                return true;
            }
            else if (windowsKeyCode == keyN && modifiers == CefEventFlags.ControlDown)
            {
                isKeyboardShortcut = true;
                _frmCEF.Invoke(new Action(() => _frmCEF.NewTab("korot://newtab")));
                return true;
            }
            else if (windowsKeyCode == keyN && modifiers.HasFlag(CefEventFlags.ControlDown) && modifiers.HasFlag(CefEventFlags.ShiftDown))
            {
                isKeyboardShortcut = true;
                Process.Start(Application.ExecutablePath, "-incognito");
                return true;
            }
            else if (windowsKeyCode == keyN && modifiers.HasFlag(CefEventFlags.ControlDown) && modifiers.HasFlag(CefEventFlags.AltDown))
            {
                isKeyboardShortcut = true;
                Process.Start(Application.ExecutablePath);
                return true;
            }
            else { isKeyboardShortcut = false; return false; }
        }

        public bool OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            if ((windowsKeyCode == keyF && modifiers == CefEventFlags.ControlDown)
                || ((windowsKeyCode == VK_PRIOR || windowsKeyCode == VK_NEXT || windowsKeyCode == VK_UP || windowsKeyCode == VK_DOWN) && modifiers == CefEventFlags.ControlDown)
                || windowsKeyCode == VK_BROWSER_BACK
                || windowsKeyCode == VK_BROWSER_FORWARD
                || windowsKeyCode == VK_BROWSER_REFRESH
                || windowsKeyCode == VK_BROWSER_STOP
                || windowsKeyCode == VK_BROWSER_SEARCH
                || windowsKeyCode == VK_BROWSER_HOME
                || (windowsKeyCode == keyN && modifiers == CefEventFlags.ControlDown)
                || (windowsKeyCode == keyS && modifiers == CefEventFlags.ControlDown)
                || (windowsKeyCode == VK_SNAPSHOT && modifiers == CefEventFlags.ControlDown)
                || (windowsKeyCode == keyN && modifiers.HasFlag(CefEventFlags.ControlDown) && modifiers.HasFlag(CefEventFlags.ShiftDown))
                || (windowsKeyCode == keyN && modifiers.HasFlag(CefEventFlags.ControlDown) && modifiers.HasFlag(CefEventFlags.AltDown))
                || (windowsKeyCode == keyM && modifiers.HasFlag(CefEventFlags.ControlDown)))
            {
                return false;
            }
            else { return true; }
        }
    }
}