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