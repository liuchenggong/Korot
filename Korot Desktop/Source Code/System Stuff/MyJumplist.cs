/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using System;
using System.Windows.Forms;

namespace Korot
{
    public class MyJumplist
    {
        private readonly JumpList list;
        private readonly Settings Settings;

        public MyJumplist(IntPtr windowHandle, Settings settings)
        {
            Settings = settings;
            list = JumpList.CreateJumpListForIndividualWindow(TaskbarManager.Instance.ApplicationId, windowHandle);
            list.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;
            BuildList();
        }

        private string NIW = "New Incognito Window";
        private string NW = "New Window";

        private void BuildList()
        {
            NIW = Settings.LanguageSystem.GetItemText("NewWindow");
            NW = Settings.LanguageSystem.GetItemText("NewIncognitoWindow");
            JumpListCustomCategory userActionsCategory = new JumpListCustomCategory("Actions");
            userActionsCategory.AddJumpListItems();
            list.AddCustomCategories(userActionsCategory);
            list.ClearAllUserTasks();
            JumpListLink jlIncognito = new JumpListLink(Application.ExecutablePath + " -incognito", NIW)
            {
                IconReference = new IconReference(Application.ExecutablePath, 0)
            };
            list.AddUserTasks(jlIncognito);
            JumpListLink jlN = new JumpListLink(Application.ExecutablePath, NW)
            {
                IconReference = new IconReference(Application.ExecutablePath, 0)
            };
            list.AddUserTasks(jlN);
            try
            {
                list.Refresh();
            }
            catch (Exception) { } //ignored
        }
    }
}