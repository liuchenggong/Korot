/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Korot
{
    internal class MyDialogHandler : IDialogHandler
    {
        public bool OnFileDialog(IWebBrowser chromiumWebBrowser, IBrowser browser, CefFileDialogMode mode, CefFileDialogFlags flags, string title, string defaultFilePath, List<string> acceptFilters, int selectedAcceptFilter, IFileDialogCallback callback)
        {
            if (mode == CefFileDialogMode.Open || mode == CefFileDialogMode.OpenMultiple)
            {
                OpenFileDialog openfld = new OpenFileDialog
                {
                    Filter = acceptFilters.ToString()
                };
                if (mode == CefFileDialogMode.OpenMultiple)
                {
                    openfld.Multiselect = true;
                }
                else
                {
                    openfld.Multiselect = false;
                }
                openfld.Title = title;
                openfld.DefaultExt = acceptFilters[selectedAcceptFilter];
                openfld.FileName = defaultFilePath;
                if (openfld.ShowDialog() == DialogResult.OK)
                {
                    List<string> returnval = new List<string>();
                    foreach (string x in openfld.FileNames)
                    {
                        returnval.Add(x);
                    }
                    callback.Continue(selectedAcceptFilter, returnval);
                    return true;
                }
                else { callback.Cancel(); return false; }
            }
            else if (mode == CefFileDialogMode.OpenFolder)
            {
                FolderBrowserDialog openfld = new FolderBrowserDialog
                {
                    Description = title
                };

                if (openfld.ShowDialog() == DialogResult.OK)
                {
                    List<string> returnvalue = new List<string>
                    {
                        openfld.SelectedPath.ToString()
                    };
                    callback.Continue(selectedAcceptFilter, returnvalue);
                    return true;
                }
                else { callback.Cancel(); return false; }
            }
            else
            {
                SaveFileDialog savefld = new SaveFileDialog
                {
                    Filter = acceptFilters.ToString(),
                    DefaultExt = acceptFilters[selectedAcceptFilter],
                    FileName = defaultFilePath
                };
                if (savefld.ShowDialog() == DialogResult.OK)
                {
                    List<string> returnval = new List<string>();
                    foreach (string x in savefld.FileNames)
                    {
                        returnval.Add(x);
                    }
                    callback.Continue(selectedAcceptFilter, returnval);
                    return true;
                }
                else { callback.Cancel(); return false; }
            }
        }
    }
}