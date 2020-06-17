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
        Settings Settings;
        public MyJumplist(IntPtr windowHandle,Settings settings)
        {
            Settings = settings;
            list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, windowHandle);
            list.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
            BuildList();

        }
        string NIW = "New Incognito Window";
        string NW = "New Window";
        private void BuildList()
        {
            NIW = Settings.LanguageSystem.GetItemText("NewWindow");
            NW = Settings.LanguageSystem.GetItemText("NewIncognitoWindow");
            JumpListCustomCategory userActionsCategory = new JumpListCustomCategory("Actions");
            userActionsCategory.AddJumpListItems();
            list.AddCustomCategories(userActionsCategory);
            list.ClearAllUserTasks();
            JumpListLink jlIncognito = new JumpListLink(Application.ExecutablePath + " -incognito", NIW);
            jlIncognito.IconReference = new IconReference(Application.ExecutablePath, 0);
            list.AddUserTasks(jlIncognito);
            JumpListLink jlN = new JumpListLink(Application.ExecutablePath, NW);
            jlN.IconReference = new IconReference(Application.ExecutablePath, 0);
            list.AddUserTasks(jlN);
            list.Refresh();
        }
    }
}
