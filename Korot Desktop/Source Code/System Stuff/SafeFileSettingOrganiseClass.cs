using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HTAlt;

namespace Korot
{
    class SafeFileSettingOrganizedClass
    {
        public static string GetUserFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\";
        public static string LastUser
        {
            get
            {
                if (File.Exists(GetUserFolder + "LASTUSER.SFSOC")) 
                {
                    return HTAlt.Tools.ReadFile(GetUserFolder + "LASTUSER.SFSOC", Encoding.UTF8);
                }else
                {
                    HTAlt.Tools.WriteFile(GetUserFolder + "LASTUSER.SFSOC", "",Encoding.UTF8);
                    return LastUser;
                }
            }
            set
            {
                HTAlt.Tools.WriteFile(GetUserFolder + "LASTUSER.SFSOC", value, Encoding.UTF8);
            }
        }
        public static string LastSession
        {
            get
            {
                if (File.Exists(GetUserFolder + "LASTSESSION.SFSOC"))
                {
                    return HTAlt.Tools.ReadFile(GetUserFolder + "LASTSESSION.SFSOC", Encoding.UTF8);
                }
                else
                {
                    HTAlt.Tools.WriteFile(GetUserFolder + "LASTSESSION.SFSOC", "", Encoding.UTF8);
                    return LastSession;
                }
            }
            set
            {
                HTAlt.Tools.WriteFile(GetUserFolder + "LASTSESSION.SFSOC", value, Encoding.UTF8);
            }
        }
        public static string[] ErrorMenu
        {
            get
            {
                if (File.Exists(GetUserFolder + "ERRORMENU.SFSOC"))
                {
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    return HTAlt.Tools.ReadFile(GetUserFolder + "ERRORMENU.SFSOC", Encoding.UTF8).Split(token);
                }
                else
                {
                    HTAlt.Tools.WriteFile(GetUserFolder + "ERRORMENU.SFSOC", "", Encoding.UTF8);
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
                HTAlt.Tools.WriteFile(GetUserFolder + "ERRORMENU.SFSOC", newval, Encoding.UTF8);
            }
        }
    }
}
