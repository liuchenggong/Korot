using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Korot
{
    class ProfileManagement
    {
        public static bool SwitchProfile(string profilename, frmCEF cefform)
        {
            Properties.Settings.Default.LastUser = profilename;
            if (!cefform._Incognito) { Properties.Settings.Default.Save(); }
            Process.Start(Application.ExecutablePath);
            Application.Exit();
            return true;
        }
        public static bool DeleteProfile(string profilename, frmCEF cefform)
        {
            Properties.Settings.Default.LastUser = new DirectoryInfo(Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")[0]).Name;
            if (!cefform._Incognito) { Properties.Settings.Default.Save(); }
            frmCEF obj = (frmCEF)Application.OpenForms["frmCEF"]; obj.Close(); CefSharp.Cef.Shutdown();
            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profilename + "\\", true);
            if (!cefform._Incognito) { Properties.Settings.Default.Save(); }
            Process.Start(Application.ExecutablePath);
            Application.Exit();
            return true;
        }
        public static bool NewProfile(frmCEF cefform)
        {
            HaltroyFramework.HaltroyInputBox newprof = new HaltroyFramework.HaltroyInputBox("Korot", cefform.newProfileInfo + Environment.NewLine + "/ \\ : ? * |", cefform.anaform().Icon, "", Properties.Settings.Default.BackColor, cefform.OK, cefform.Cancel, 400, 150);
            DialogResult diagres = newprof.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (newprof.TextValue().Contains("/") || newprof.TextValue().Contains("\\") || newprof.TextValue().Contains(":") || newprof.TextValue().Contains("?") || newprof.TextValue().Contains("*") || newprof.TextValue().Contains("|"))
                { NewProfile(cefform); }
                else
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + newprof.TextValue());
                    SwitchProfile(newprof.TextValue(), cefform);
                }
            }
            return true;
        }
    }
}
