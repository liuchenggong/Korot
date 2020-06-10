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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Korot
{
    public class Tools
    {
        
        
        public static bool createThemes()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Light.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Light</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#ffffff</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#55b4d4</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Light.ktf", newTheme, Encoding.UTF8);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Dark.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Dark</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#000000</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#55b4d4</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Dark.ktf", newTheme, Encoding.UTF8);
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Midnight.ktf"))
            {
                string newTheme = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                                  "<Name>Korot Midnight</Name>" + Environment.NewLine +
                                   "<Author>Haltroy</Author>" + Environment.NewLine +
                                   "<Version>1.0.0.0</Version>" + Environment.NewLine +
                                   "<UseHaltroyUpdate>false</UseHaltroyUpdate>" + Environment.NewLine +
                                   "<BackColor>#050024</BackColor>" + Environment.NewLine +
                                   "<OverlayColor>#55b4d4</OverlayColor>" + Environment.NewLine +
                                   "<NewTabButtonColor>2</NewTabButtonColor>" + Environment.NewLine +
                                   "<CloseButtonColor>2</CloseButtonColor>" + Environment.NewLine +
                                   "<BackgroundStyle Layout=\"0\">BACKCOLOR</BackgroundStyle>" + Environment.NewLine +
                                   "</Theme>" + Environment.NewLine;
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\Korot Midnight.ktf", newTheme, Encoding.UTF8);
            }
            return true;
        }
        public static bool createFolders()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Themes\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Extensions\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\Logs\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\IconStorage\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\" + Properties.Settings.Default.LastUser + "\\IconStorage\\"); }
            return true;
        }
        public static bool FixDefaultLanguage()
        {
            if (!Directory.Exists(Application.StartupPath + "\\Lang\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
            }
            HTAlt.Tools.WriteFile(Application.StartupPath + "\\Lang\\English.lang", Properties.Resources.English);
            return true;
        }
    }
}
