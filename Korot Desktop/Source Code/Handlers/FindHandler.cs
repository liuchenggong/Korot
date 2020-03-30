using CefSharp;
using CefSharp.Structs;
using System;

namespace Korot
{
    internal class FindHandler : IFindHandler
    {
        private readonly frmCEF _frmCEF;
        public FindHandler(frmCEF _frmCef)
        {
            _frmCEF = _frmCef;
        }
        public void OnFindResult(IWebBrowser chromiumWebBrowser, IBrowser browser, int identifier, int count, Rect selectionRect, int activeMatchOrdinal, bool finalUpdate)
        {
            _frmCEF.Invoke(new Action(() => _frmCEF.FindUpdate(identifier, count, activeMatchOrdinal, finalUpdate)));
        }
    }
}
