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
        /// <summary>
        /// Creating a JumpList for the application
        /// </summary>
        /// <param name="windowHandle"></param>
        public MyJumplist(IntPtr windowHandle,frmMain MainForm)
        {
            
            anaform = MainForm;
            list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, windowHandle);
            list.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
            BuildList();

        }
        string NIW = "New Incognito Window";
        string NW = "New Window";

        /// <summary>
        /// Builds the Jumplist
        /// </summary>
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

            string incmodepath = Application.ExecutablePath +  " -incognito";
            JumpListLink jlNotepad = new JumpListLink(incmodepath, NIW);
            jlNotepad.IconReference = new IconReference(Application.ExecutablePath, 0);

            string newwindow = Application.ExecutablePath;
            JumpListLink jlCalculator = new JumpListLink(newwindow, NW);
            jlCalculator.IconReference = new IconReference(Application.ExecutablePath, 0);

            list.ClearAllUserTasks();
            list.AddUserTasks(jlNotepad);
            list.AddUserTasks(jlCalculator);
            list.Refresh();
        }
    }
}
