/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using System;
using System.IO;
using System.Text;

namespace Korot
{
    internal class SafeFileSettingOrganizedClass
    {
        public static string GetUserFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\";

        public static string LastUser
        {
            get
            {
                if (File.Exists(GetUserFolder + "LASTUSER.SFSOC"))
                {
                    return HTAlt.Tools.ReadFile(GetUserFolder + "LASTUSER.SFSOC", Encoding.Unicode);
                }
                else
                {
                    HTAlt.Tools.WriteFile(GetUserFolder + "LASTUSER.SFSOC", "", Encoding.Unicode);
                    return LastUser;
                }
            }
            set => HTAlt.Tools.WriteFile(GetUserFolder + "LASTUSER.SFSOC", value, Encoding.Unicode);
        }

        public static string LastSession
        {
            get
            {
                if (File.Exists(GetUserFolder + "LASTSESSION.SFSOC"))
                {
                    return HTAlt.Tools.ReadFile(GetUserFolder + "LASTSESSION.SFSOC", Encoding.Unicode);
                }
                else
                {
                    HTAlt.Tools.WriteFile(GetUserFolder + "LASTSESSION.SFSOC", "", Encoding.Unicode);
                    return LastSession;
                }
            }
            set => HTAlt.Tools.WriteFile(GetUserFolder + "LASTSESSION.SFSOC", value, Encoding.Unicode);
        }

        public static string[] ErrorMenu
        {
            get
            {
                if (File.Exists(GetUserFolder + "ERRORMENU.SFSOC"))
                {
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    return HTAlt.Tools.ReadFile(GetUserFolder + "ERRORMENU.SFSOC", Encoding.Unicode).Split(token);
                }
                else
                {
                    HTAlt.Tools.WriteFile(GetUserFolder + "ERRORMENU.SFSOC", "", Encoding.Unicode);
                    return ErrorMenu;
                }
            }
            set
            {
                string newval = "";
                foreach (string x in value)
                {
                    newval += x + Environment.NewLine;
                }
                HTAlt.Tools.WriteFile(GetUserFolder + "ERRORMENU.SFSOC", newval, Encoding.Unicode);
            }
        }
    }
}