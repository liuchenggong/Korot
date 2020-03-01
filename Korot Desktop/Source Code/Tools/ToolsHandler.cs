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
using System.Text;
using System.Windows.Forms;

namespace Korot
{
    public class Tools
    {
        public static Image getImageFromUrl(string url)
        {
                using (System.Net.WebClient webClient = new System.Net.WebClient())
                {
                    using (Stream stream = webClient.OpenRead(url))
                    {
                        return Image.FromStream(stream);
                    }
                }
        }
        public static bool createThemes()
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf"))
            {
                string newTheme = "255" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "30" + Environment.NewLine +
                                "144" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "BACKCOLOR" + Environment.NewLine +
                                "0" + Environment.NewLine +
"Korot Light" + Environment.NewLine +
"Haltroy" + Environment.NewLine +
"2" + Environment.NewLine +
"1";
                FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf", newTheme, Encoding.UTF8);

            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf"))
            {
                string newTheme = "0" + Environment.NewLine +
                                "0" + Environment.NewLine +
                                "0" + Environment.NewLine +
                                "30" + Environment.NewLine +
                                "144" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "BACKCOLOR" + Environment.NewLine +
                                "0" + Environment.NewLine +
"Korot Dark" + Environment.NewLine +
"Haltroy" + Environment.NewLine +
"2" + Environment.NewLine +
"1";
                FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf", newTheme, Encoding.UTF8);

            }
            return true;
        }
        public static bool SaveSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory, string disCookieFile)
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

            FileSystem2.WriteFile(settingFile, settingsText, Encoding.UTF8);
            string cookieList = "";
            foreach (String x in Properties.Settings.Default.CookieDisallowList)
            {
                cookieList += x + Environment.NewLine;
            }
            FileSystem2.WriteFile(disCookieFile, cookieList, Encoding.UTF8);
            // History
            FileSystem2.WriteFile(historyFile, Properties.Settings.Default.History, Encoding.UTF8);
            // Favorites
            FileSystem2.WriteFile(favoritesFile, Properties.Settings.Default.Favorites.Replace("]","]" + Environment.NewLine), Encoding.UTF8);

            // Download
            FileSystem2.WriteFile(downloadHistory, Properties.Settings.Default.DowloadHistory, Encoding.UTF8);
            return true;
        }
        public static bool LoadSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory, string disCookieFile)
        {
            // Settings
            string Playlist = FileSystem2.ReadFile(settingFile, Encoding.UTF8);
            char[] token = new char[] { ';' };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase.Length >= 13)
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
            }
            else
            {
                Console.WriteLine("Error at reading settings(" + settingFile + ") : [Lines: " + SplittedFase.Length + "]");
                Tools.SaveSettings(settingFile, historyFile, favoritesFile, downloadHistory, disCookieFile);
                return false;
            }
            Properties.Settings.Default.CookieDisallowList.Clear();
            string Playlist2 = FileSystem2.ReadFile(disCookieFile, Encoding.UTF8);
            char[] token2 = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase2 = Playlist2.Split(token2);
            int Count = SplittedFase2.Length - 1; ; int i = 0;
            while ((i != Count) && (Count >= 1))
            {
                Properties.Settings.Default.CookieDisallowList.Add(SplittedFase2[i].Replace(Environment.NewLine, ""));
                i += 1;
            }
            if (Properties.Settings.Default.ThemeFile == null || !File.Exists(Properties.Settings.Default.ThemeFile))
            {
                Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf";
            }
            Tools.createThemes();
            // History
            Properties.Settings.Default.History = FileSystem2.ReadFile(historyFile, Encoding.UTF8);
            // Favorites
            Properties.Settings.Default.Favorites = FileSystem2.ReadFile(favoritesFile, Encoding.UTF8).Replace("]" + Environment.NewLine, "]");
            // Downloads
            Properties.Settings.Default.DowloadHistory = FileSystem2.ReadFile(downloadHistory, Encoding.UTF8);
            return true;
        }
        public static bool createFolders()
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\IconStorage\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\IconStorage\\"); }
            if (IsDirectoryEmpty(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
            if (!(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"))) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
            return true;
        }
        public static bool IsDirectoryEmpty(string path)
        {
            if (Directory.Exists(path))
            {
                if (Directory.GetDirectories(path).Length > 0) { return false; } else { return true; }
            }
            else { return true; }
        }
        public static int Brightness(Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        public static bool isBright(Color c)
        {
            return Brightness(c) > 130;
        }
        public static int GerekiyorsaAzalt(int defaultint, int azaltma)
        {
            return defaultint > azaltma ? defaultint - azaltma : defaultint;
        }
        public static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır)
        {
            return defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        }
        public static Color TersRenk (Color c,bool reverseAlpha)
        {
            return Color.FromArgb(reverseAlpha ? (255 - c.A) : c.A,
                                  255 - c.R,
                                  255 - c.G,
                                  255 - c.B);
        }
        public static Color ShiftBrightnessIfNeeded(Color baseColor, int value, bool shiftAlpha)
        {
            if (isBright(baseColor))
            {
                return Color.FromArgb(shiftAlpha ? GerekiyorsaAzalt(baseColor.A, value) : baseColor.A,
                                      GerekiyorsaAzalt(baseColor.R, value),
                                      GerekiyorsaAzalt(baseColor.G, value),
                                      GerekiyorsaAzalt(baseColor.B, value));
            }
            else
            {
                return Color.FromArgb(shiftAlpha ? GerekiyorsaArttır(baseColor.A, value, 255) : baseColor.A,
                      GerekiyorsaArttır(baseColor.R, value, 255),
                      GerekiyorsaArttır(baseColor.G, value, 255),
                      GerekiyorsaArttır(baseColor.B, value, 255));
            }
        }
        public static bool FixDefaultLanguage()
        {
            if (!Directory.Exists(Application.StartupPath + "\\Lang\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Lang\\");
            }
            FileSystem2.WriteFile(Application.StartupPath + "\\Lang\\English.lang", Properties.Resources.English);
            return true;
        }
    }
}
