using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAPICodePack.Taskbar;
using System.IO;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Shell;
using System.Windows.Forms;

namespace Korot
{
    public class MyJumplist
    {
        private JumpList list;
        frmMain anaform;
        public MyJumplist(IntPtr windowHandle,frmMain MainForm)
        {
            
            anaform = MainForm;
            list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, windowHandle);
            list.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
            BuildList();

        }
        string NIW = "New Incognito Window";
        string NW = "New Window";
        private void BuildList()
        {
            string Playlist = HTAlt.Tools.ReadFile(Properties.Settings.Default.LangFile, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SF = Playlist.Split(token);
            NIW = SF[9].Substring(1).Replace(Environment.NewLine, "");
            NW = SF[8].Substring(1).Replace(Environment.NewLine, "");
            JumpListCustomCategory userActionsCategory = new JumpListCustomCategory("Actions");
            userActionsCategory.AddJumpListItems();
            list.AddCustomCategories(userActionsCategory);
            list.ClearAllUserTasks();
            string incmodepath = Application.ExecutablePath + " -incognito";
            JumpListLink jlIncognito = new JumpListLink(incmodepath, NIW);
            jlIncognito.IconReference = new IconReference(Application.ExecutablePath, 0);
            list.AddUserTasks(jlIncognito);
            string newwindow = Application.ExecutablePath;
            JumpListLink jlN = new JumpListLink(newwindow, NW);
            jlN.IconReference = new IconReference(Application.ExecutablePath, 0);
            list.AddUserTasks(jlN);
            list.Refresh();
        }
    }
}
