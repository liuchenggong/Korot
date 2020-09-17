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
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Korot
{
    internal class ProfileManagement
    {
        public static bool SwitchProfile(string profilename, frmCEF cefform)
        {
            SafeFileSettingOrganizedClass.LastUser = profilename;
            Process.Start(Application.ExecutablePath);
            Application.Exit();
            return true;
        }

        public static bool DeleteProfile(string profilename, frmCEF cefform)
        {
            SafeFileSettingOrganizedClass.LastUser = new DirectoryInfo(Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\")[0]).Name;
            frmCEF obj = (frmCEF)Application.OpenForms["frmCEF"]; obj.Close(); CefSharp.Cef.Shutdown();
            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + profilename + "\\", true);
            Process.Start(Application.ExecutablePath);
            Application.Exit();
            return true;
        }

        public static bool NewProfile(frmCEF cefform)
        {
            HTAlt.WinForms.HTInputBox newprof = new HTAlt.WinForms.HTInputBox("Korot", cefform.anaform.newProfileInfo + Environment.NewLine + "/ \\ : ? * |", "") { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackgroundColor = cefform.Settings.Theme.BackColor };
            DialogResult diagres = newprof.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (newprof.TextValue.Contains("/") || newprof.TextValue.Contains("\\") || newprof.TextValue.Contains(":") || newprof.TextValue.Contains("?") || newprof.TextValue.Contains("*") || newprof.TextValue.Contains("|"))
                { NewProfile(cefform); }
                else
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + newprof.TextValue);
                    SwitchProfile(newprof.TextValue, cefform);
                }
            }
            return true;
        }
    }
}