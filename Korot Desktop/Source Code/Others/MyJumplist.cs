using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
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
        public MyJumplist(IntPtr windowHandle, frmMain _frmSettings)
        {
            anaform = _frmSettings;
            list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, windowHandle);
            list.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
            BuildList();

        }

        /// <summary>
        /// Builds the Jumplist
        /// </summary>
        private void BuildList()
        {
            try
            {
                JumpListCustomCategory userActionsCategory = new JumpListCustomCategory("Actions");

                list.ClearAllUserTasks();
                userActionsCategory.AddJumpListItems();
                list.AddCustomCategories(userActionsCategory);

                string incmodepath = "\"" + Application.ExecutablePath + "\"" + " -incognito";
                JumpListLink jlNotepad = new JumpListLink(incmodepath, anaform.newincwindow);
                jlNotepad.IconReference = new IconReference(Application.ExecutablePath, 0);

                string incmodepath2 = "\"" + Application.ExecutablePath + "\"";
                JumpListLink jlNotepad2 = new JumpListLink(incmodepath2, anaform.newincwindow);
                jlNotepad2.IconReference = new IconReference(Application.ExecutablePath, 0);

                list.AddUserTasks(jlNotepad);
                list.AddUserTasks(jlNotepad2);
                list.Refresh();
            }
            catch { } //Ignore
        }
    }
}
