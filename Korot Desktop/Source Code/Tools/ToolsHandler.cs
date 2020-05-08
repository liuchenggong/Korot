﻿//MIT License
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
        public static string getOSInfo()
        {
            string fullName = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
            //We only need the version number or name like 7,Vista,10
            //Remove any other unnecesary thing.
            fullName = fullName.Replace("Microsoft Windows", "")
                .Replace(" (PRODUCT) RED", "")
                .Replace(" Business", "")
                .Replace(" Education", "")
                .Replace(" Embedded", "")
                .Replace(" Enterprise LTSC", "")
                .Replace(" Enterprise", "")
                .Replace(" Home Basic", "")
                .Replace(" Home Premium", "")
                .Replace(" Home", "")
                .Replace(" Insider", "")
                .Replace(" IoT Core", "")
                .Replace(" IoT", "")
                .Replace(" KN", "")
                .Replace(" Media Center 2002", "")
                .Replace(" Media Center 2004", "")
                .Replace(" Media Center 2005", "")
                .Replace(" Mobile Enterprise", "")
                .Replace(" Mobile", "")
                .Replace(" N", "")
                .Replace(" Pro Education", "")
                .Replace(" Pro for Workstations", "")
                .Replace(" Professional x64", "")
                .Replace(" Professional", "")
                .Replace(" Pro", "")
                .Replace(" Signature Edition", "")
                .Replace(" Single Language", "")
                .Replace(" Starter", "")
                .Replace(" S", "")
                .Replace(" Tablet PC", "")
                .Replace(" Team", "")
                .Replace(" Ultimate", "")
                .Replace(" VL", "")
                .Replace(" X", "")
                .Replace(" with Bing", "")
                .Replace(" ", "");
            string versionNumber;
            switch (fullName)
            {
                case "XP":
                    versionNumber = "5.1";
                    break;
                case "Vista":
                    versionNumber = "6.0";
                    break;
                case "7":
                    versionNumber = "6.1";
                    break;
                default:
                    versionNumber = fullName;
                    break;
            }
            return "NT " + fullName;
        }
        
        public static bool createThemes()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf"))
            {
                string newTheme = "#ffffff" + Environment.NewLine +
                                "#1e90ff" + Environment.NewLine +
                                "BACKCOLOR" + Environment.NewLine +
                                "0" + Environment.NewLine +
                                "2" + Environment.NewLine +
                                "1" + Environment.NewLine +
                                "Korot Light" + Environment.NewLine +
                                "Haltroy" + Environment.NewLine +
                                "1.0.0.0" + Environment.NewLine + "1";
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf", newTheme, Encoding.UTF8);

            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf"))
            {
                string newTheme = "#000000" + Environment.NewLine +
                                "#1e90ff" + Environment.NewLine +
                                "BACKCOLOR" + Environment.NewLine +
                                "0" + Environment.NewLine +
                                "2" + Environment.NewLine +
                                "1" + Environment.NewLine +
                                "Korot Dark" + Environment.NewLine +
                                "Haltroy" + Environment.NewLine +
                                "1.0.0.0" + Environment.NewLine + "1";
                HTAlt.Tools.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf", newTheme, Encoding.UTF8);

            }
            return true;
        }
        public static bool SaveSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory, string disCookieFile, string notAllow,string notBlock)
        {
            // Settings

            string settingsText = Properties.Settings.Default.Homepage + ";";

            settingsText += Properties.Settings.Default.SearchURL + ";";

            settingsText += (Properties.Settings.Default.downloadOpen ? "1" : "0") + ";";


            settingsText += Properties.Settings.Default.ThemeFile + ";";

            settingsText += (Properties.Settings.Default.DoNotTrack ? "1" : "0") + ";";

            settingsText += Properties.Settings.Default.LangFile + ";";

            settingsText += (Properties.Settings.Default.rememberLastProxy ? "1" : "0") + ";";

            settingsText += Properties.Settings.Default.LastProxy + ";";

            settingsText += Properties.Settings.Default.DownloadFolder + ";";

            settingsText += (Properties.Settings.Default.useDownloadFolder ? "1" : "0") + ";";

            settingsText += Properties.Settings.Default.StartupURL + ";";

            settingsText += (Properties.Settings.Default.showFav ? "1" : "0") + ";";

            settingsText += (Properties.Settings.Default.allowUnknownResources ? "1" : "0") + ";";

            settingsText += (Properties.Settings.Default.quietMode ? "1" : "0") + ";";

            settingsText += (Properties.Settings.Default.silentAllNotifications ? "1" : "0") + ";";

            settingsText += (Properties.Settings.Default.autoSilent ? "1" : "0") + ";";

            settingsText += Properties.Settings.Default.autoSilentMode + ";";

            HTAlt.Tools.WriteFile(settingFile, settingsText, Encoding.UTF8);
            //Cookie
            string cookieList = "";
            foreach (string x in Properties.Settings.Default.CookieDisallowList)
            {
                cookieList += x + Environment.NewLine;
            }
            HTAlt.Tools.WriteFile(disCookieFile, cookieList, Encoding.UTF8);
            //NotAllow
            string allowList = "";
            foreach (string x in Properties.Settings.Default.notificationAllow)
            {
                allowList += x + Environment.NewLine;
            }
            HTAlt.Tools.WriteFile(notAllow, allowList, Encoding.UTF8);
            //NotBlock
            string blockList = "";
            foreach (string x in Properties.Settings.Default.notificationBlock)
            {
                blockList += x + Environment.NewLine;
            }
            HTAlt.Tools.WriteFile(notBlock, blockList, Encoding.UTF8);
            // History
            HTAlt.Tools.WriteFile(historyFile, Properties.Settings.Default.History, Encoding.UTF8);
            // Favorites
            HTAlt.Tools.WriteFile(favoritesFile, Properties.Settings.Default.Favorites.Replace("]", "]" + Environment.NewLine), Encoding.UTF8);

            // Download
            HTAlt.Tools.WriteFile(downloadHistory, Properties.Settings.Default.DowloadHistory, Encoding.UTF8);
            return true;
        }
        public static bool LoadSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory, string disCookieFile,string notAllow,string notBlock)
        {
            // Settings
            string Playlist = HTAlt.Tools.ReadFile(settingFile, Encoding.UTF8);
            char[] token = new char[] { ';' };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase.Length >= 16)
            {
                Properties.Settings.Default.Homepage = SplittedFase[0].Replace(Environment.NewLine, "");
                Properties.Settings.Default.SearchURL = SplittedFase[1].Replace(Environment.NewLine, "");
                Properties.Settings.Default.downloadOpen = SplittedFase[2].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.ThemeFile = SplittedFase[3].Replace(Environment.NewLine, "");
                Properties.Settings.Default.DoNotTrack = SplittedFase[4].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.LangFile = SplittedFase[5].Replace(Environment.NewLine, "");
                Properties.Settings.Default.rememberLastProxy = SplittedFase[6].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.LastProxy = SplittedFase[7].Replace(Environment.NewLine, "");
                Properties.Settings.Default.DownloadFolder = SplittedFase[8].Replace(Environment.NewLine, "");
                Properties.Settings.Default.useDownloadFolder = SplittedFase[9].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.StartupURL = SplittedFase[10].Replace(Environment.NewLine, "");
                Properties.Settings.Default.showFav = SplittedFase[11].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.allowUnknownResources = SplittedFase[12].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.quietMode = SplittedFase[13].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.silentAllNotifications = SplittedFase[14].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.autoSilent = SplittedFase[15].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.autoSilentMode = SplittedFase[16].Replace(Environment.NewLine, "");
            }
            else
            {
                Console.WriteLine("Error at reading settings(" + settingFile + ") : [Lines: " + SplittedFase.Length + "]");
                Tools.SaveSettings(settingFile, historyFile, favoritesFile, downloadHistory, disCookieFile,notAllow,notBlock);
                return false;
            }
            //Cookie
            Properties.Settings.Default.CookieDisallowList.Clear();
            string Playlist2 = HTAlt.Tools.ReadFile(disCookieFile, Encoding.UTF8);
            char[] token2 = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase2 = Playlist2.Split(token2);
            int Count = SplittedFase2.Length - 1; ; int i = 0;
            while ((i != Count) && (Count >= 1))
            {
                Properties.Settings.Default.CookieDisallowList.Add(SplittedFase2[i].Replace(Environment.NewLine, ""));
                i += 1;
            }
            //notAlow
            Properties.Settings.Default.notificationAllow.Clear();
            string Playlist3 = HTAlt.Tools.ReadFile(notAllow, Encoding.UTF8);
            char[] token3 = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase3 = Playlist3.Split(token3);
            int Count2 = SplittedFase3.Length - 1; ; int i2 = 0;
            while ((i2 != Count2) && (Count2 >= 1))
            {
                Properties.Settings.Default.notificationAllow.Add(SplittedFase3[i2].Replace(Environment.NewLine, ""));
                i2 += 1;
            }
            //notBlock
            Properties.Settings.Default.notificationBlock.Clear();
            string Playlist4 = HTAlt.Tools.ReadFile(notAllow, Encoding.UTF8);
            char[] token4 = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase4 = Playlist4.Split(token4);
            int Count3 = SplittedFase4.Length - 1; ; int i3 = 0;
            while ((i3 != Count3) && (Count3 >= 1))
            {
                Properties.Settings.Default.notificationBlock.Add(SplittedFase4[i3].Replace(Environment.NewLine, ""));
                i3 += 1;
            }
            //Theme
            if (Properties.Settings.Default.ThemeFile == null || !File.Exists(Properties.Settings.Default.ThemeFile))
            {
                Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf";
            }
            Tools.createThemes();
            // History
            Properties.Settings.Default.History = HTAlt.Tools.ReadFile(historyFile, Encoding.UTF8);
            // Favorites
            Properties.Settings.Default.Favorites = HTAlt.Tools.ReadFile(favoritesFile, Encoding.UTF8).Replace("]" + Environment.NewLine, "]");
            // Downloads
            Properties.Settings.Default.DowloadHistory = HTAlt.Tools.ReadFile(downloadHistory, Encoding.UTF8);
            return true;
        }
        public static bool createFolders()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\IconStorage\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\IconStorage\\"); }
            if (HTAlt.Tools.IsDirectoryEmpty(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
            if (!(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"))) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
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
