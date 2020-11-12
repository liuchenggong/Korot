/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

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
            HTAlt.WinForms.HTInputBox newprof = new HTAlt.WinForms.HTInputBox("Korot", cefform.anaform.newProfileInfo + Environment.NewLine + "/ \\ : ? * |", "") { Icon = cefform.anaform.Icon, SetToDefault = cefform.anaform.SetToDefault, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = cefform.Settings.Theme.BackColor, AutoForeColor = false, ForeColor = cefform.Settings.Theme.ForeColor };
            DialogResult diagres = newprof.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (newprof.TextValue.Contains("/") || newprof.TextValue.Contains("\\") || newprof.TextValue.Contains(":") || newprof.TextValue.Contains("?") || newprof.TextValue.Contains("*") || newprof.TextValue.Contains("|"))
                { NewProfile(cefform); }
                else
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + newprof.TextValue);
                    Settings newSettings = new Settings(newprof.TextValue);
                    newSettings.Save();
                    SwitchProfile(newprof.TextValue, cefform);
                }
            }
            return true;
        }
    }
}