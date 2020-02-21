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
using CefSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Korot
{

    public partial class frmMain : TitleBarTabs
    {
        public List<DownloadItem> CurrentDownloads = new List<DownloadItem>();
        public List<string> CancelledDownloads = new List<string>();
        public bool isIncognito = false;
        public KorotTabRenderer tabRenderer;
        public string newincwindow = "New Incognito Window";
        public string newwindow = "New  Window";
        public string Yes = "Yes";
        public string No = "No";
        public string OK = "OK";
        public string Cancel = "Cancel";
        public string newProfileInfo = "Please enter a name for the new profile.It should not contain: ";
        public frmMain()
        {

            AeroPeekEnabled = true;
            tabRenderer = new KorotTabRenderer(this, Color.Black, Color.White, Color.DodgerBlue, null, false);
            TabRenderer = tabRenderer;
            Icon = Properties.Resources.KorotIcon;
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(660, 340);
            this.Size = new Size(Properties.Settings.Default.WindowSizeW, Properties.Settings.Default.WindowSizeH);
            this.Location = new Point(Properties.Settings.Default.WindowPosX, Properties.Settings.Default.WindowPosY);
        }
        public void removeThisDownloadItem(DownloadItem removeItem)
        {
            List<DownloadItem> removeDownloads = new List<DownloadItem>();
            foreach (DownloadItem x in CurrentDownloads)
            {
                if (x == removeItem) { removeDownloads.Add(x); }
                if (x.FullPath == removeItem.FullPath && x.Url == removeItem.Url && x.OriginalUrl == removeItem.OriginalUrl)
                {
                    removeDownloads.Add(x);
                }
            }
            foreach (DownloadItem x in removeDownloads)
            {
                CurrentDownloads.Remove(x);
            }
            removeDownloads.Clear();
        }
        private static int Brightness(Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        private static int GerekiyorsaAzalt(int defaultint, int azaltma)
        {
            return defaultint > azaltma ? defaultint - 20 : defaultint;
        }

        private static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır)
        {
            return defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        }

        void PrintImages()
        {
            this.MinimumSize = new System.Drawing.Size(660, 340);
            this.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
        }
        void createThemes()
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
        }
        void LoadSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory, string disCookieFile)
        {
            // Settings
            string Playlist = FileSystem2.ReadFile(settingFile, Encoding.UTF8);
            char[] token = new char[] { ';' };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase.Length >= 15)
            {
                Properties.Settings.Default.Homepage = SplittedFase[0].Replace(Environment.NewLine, "");
                Properties.Settings.Default.SearchURL = SplittedFase[1].Replace(Environment.NewLine, "");
                Properties.Settings.Default.downloadOpen = SplittedFase[2].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.downloadClose = SplittedFase[3].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.ThemeFile = SplittedFase[4].Replace(Environment.NewLine, "");
                Properties.Settings.Default.DoNotTrack = SplittedFase[5].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.LangFile = SplittedFase[6].Replace(Environment.NewLine, "");
                Properties.Settings.Default.rememberLastProxy = SplittedFase[7].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.LastProxy = SplittedFase[8].Replace(Environment.NewLine, "");
                Properties.Settings.Default.DownloadFolder = SplittedFase[9].Replace(Environment.NewLine, "");
                Properties.Settings.Default.useDownloadFolder = SplittedFase[10].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.StartupURL = SplittedFase[11].Replace(Environment.NewLine, "");
                Properties.Settings.Default.showFav = SplittedFase[12].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.allowUnknownResources = SplittedFase[13].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.dontshowUResource = SplittedFase[14].Replace(Environment.NewLine, "") == "1";
            }
            else
            {
                Console.WriteLine("Error at reading settings(" + settingFile + ") : [Lines: " + SplittedFase.Length + "]");
                SaveSettings(settingFile, historyFile, favoritesFile, downloadHistory, disCookieFile);
                return;
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
            createThemes();
            // History
            Properties.Settings.Default.History = FileSystem2.ReadFile(historyFile, Encoding.UTF8);
            // Favorites
            Properties.Settings.Default.Favorites = FileSystem2.ReadFile(favoritesFile, Encoding.UTF8);
            // Downloads
            Properties.Settings.Default.DowloadHistory = FileSystem2.ReadFile(downloadHistory, Encoding.UTF8);

        }
        void SaveSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory, string disCookieFile)
        {
            // Settings

            string settingsText = Properties.Settings.Default.Homepage + ";";

            settingsText += Properties.Settings.Default.SearchURL + ";";

            settingsText += (Properties.Settings.Default.downloadOpen ? "1" : "0") + ";";

            settingsText += (Properties.Settings.Default.downloadClose ? "1" : "0") + ";";

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

            settingsText += (Properties.Settings.Default.dontshowUResource ? "1" : "0") + ";";

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
            FileSystem2.WriteFile(favoritesFile, Properties.Settings.Default.Favorites, Encoding.UTF8);

            // Download
            FileSystem2.WriteFile(downloadHistory, Properties.Settings.Default.DowloadHistory, Encoding.UTF8);
        }

        public bool IsDirectoryEmpty(string path)
        {
            try
            {
                if (Directory.GetDirectories(path).Length > 0) { return false; } else { return true; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" [KOROT] IsDirectoryEmpty Error : " + ex.ToString());
                return true;
            }
        }
        public void SwitchProfile(string profilename)
        {
            Properties.Settings.Default.LastUser = profilename;
            if (!isIncognito) { Properties.Settings.Default.Save(); }
            Process.Start(Application.ExecutablePath);
            this.Close();
        }
        public void DeleteProfile(string profilename)
        {
            Properties.Settings.Default.LastUser = new DirectoryInfo(Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")[0]).Name;
            if (!isIncognito) { Properties.Settings.Default.Save(); }
            frmCEF obj = (frmCEF)Application.OpenForms["frmCEF"]; obj.Close(); CefSharp.Cef.Shutdown();
            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profilename + "\\", true);
            if (!isIncognito) { Properties.Settings.Default.Save(); }
            Process.Start(Application.ExecutablePath);
            this.Close();
        }
        public void NewProfile()
        {
            HaltroyFramework.HaltroyInputBox newprof = new HaltroyFramework.HaltroyInputBox("Korot", newProfileInfo + Environment.NewLine + "/ \\ : ? * |", this.Icon, "", Properties.Settings.Default.BackColor, Properties.Settings.Default.OverlayColor, OK, Cancel, 400, 150);
            DialogResult diagres = newprof.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (newprof.textBox1.Text.Contains("/") || newprof.textBox1.Text.Contains("\\") || newprof.textBox1.Text.Contains(":") || newprof.textBox1.Text.Contains("?") || newprof.textBox1.Text.Contains("*") || newprof.textBox1.Text.Contains("|"))
                { NewProfile(); }
                else
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + newprof.textBox1.Text);
                    SwitchProfile(newprof.TextValue());
                }
            }

        }
        public string restoremedaddy = "";
        string profilePath;
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            if (DateTime.Now.ToString("MM") == "03" & DateTime.Now.ToString("dd") == "11")
            {
                Output.WriteLine("Happy " + (DateTime.Now.Year - 2001) + "th Birthday Dad!");
                List<string> HaltroyNameHistory = new List<string>() { "efojaeren", "Eren Kanat", "ErenKanat02", "ErenKanat03", "Lapisman", "LapisGamingTR", "NirvanaWolfTR", "TheLordEren", "SnowWolfTR", "Pell Game", "Pell Artz", "TheEfoja", "Mr Pell", "Pellguy", "LordPell", "Pellaraptor", "Pellraptor", "Pellerma", "PLLTR", "SpringTR", "KurtSys32", "KANAT", "Spiklyman (vs Mendebur Lemur)", "Wingaxy", "Haltroy" };
                Output.WriteLine("Here's a quick history about you:");
                foreach (string x in HaltroyNameHistory)
                {
                    Output.WriteLine(x);
                }
            }
            else if (DateTime.Now.ToString("MM") == "06" & DateTime.Now.ToString("dd") == "09")
            {
                List<string> KorotNameHistory = new List<string>() { "StoneHomepage (not browser) (First code written by Haltroy)", "StoneBrowser (Trident) (First ever program written by Haltroy)", "ZStone (Trident)", "Pell Browser (Trident)", "Kolme Browser (Trident)", "Ninova (Gecko)", "Webtroy (Gecko,CEF)", "Korot (CEF)", "Korot (Boron)" };
                Output.WriteLine("I'm " + (DateTime.Now.Year - 2017) + " years old now!");
                Output.WriteLine("Here's a quick history about me:");
                foreach (string x in KorotNameHistory)
                {
                    Output.WriteLine(x);
                }
            }
            else if (DateTime.Now.ToString("MM") == "01" & DateTime.Now.ToString("dd") == "21")
            {
                List<string> PTNameHistory = new List<string>() { "Pell Media Player (WMP)", "ZStone (WMP)", "Kolme Player (WMP)", "MyPlay (WMP)", "Playtroy" };
                Output.WriteLine("My sister's " + (DateTime.Now.Year - 2017) + " years old now!");
                Output.WriteLine("Here's a quick history about my sister:");
                foreach (string x in PTNameHistory)
                {
                    Output.WriteLine(x);
                }
            }
            if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\"); }
                if (IsDirectoryEmpty(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                if (!(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"))) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
                if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
                createThemes();
                if (File.Exists(profilePath + "settings.ksf") &&
                        File.Exists(profilePath + "history.ksf") &&
                        File.Exists(profilePath + "favorites.ksf") &&
                        File.Exists(profilePath + "download.ksf") &&
                        File.Exists(profilePath + "cookieDisallow.ksf"))
                {
                    LoadSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf",
                        profilePath + "cookieDisallow.ksf");
                }
                else
                {
                    SaveSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf",
                        profilePath + "cookieDisallow.ksf");
                }
            }
            else
            { Process.Start(Application.ExecutablePath, "-oobe"); this.Close(); }

            this.Location = new Point(Korot.Properties.Settings.Default.WindowPosX, Korot.Properties.Settings.Default.WindowPosY);
            this.Size = new Size(Korot.Properties.Settings.Default.WindowSizeW, Korot.Properties.Settings.Default.WindowSizeH);
            PrintImages();
            if (Properties.Settings.Default.LangFile == null) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }

            if (Properties.Settings.Default.LastSessionURIs == "")
            {
                restoremedaddy = "";
            }
            else
            {
                restoremedaddy = Properties.Settings.Default.LastSessionURIs;
            }

            SessionLogger.Start();
        }

        public void ReadSession(string Session)
        {
            string[] SplittedFase = Session.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                CreateTab(SplittedFase[i].Replace(Environment.NewLine, ""));
                i += 1;
            }
        }
        public void WriteSessions(string Session)
        {
            try
            {
                Properties.Settings.Default.LastSessionURIs = Session;
                if (!isIncognito) { Properties.Settings.Default.Save(); }
            }
            catch
            {
            }
        }
        public void ReadLatestCurrentSession()
        {
            ReadSession(Properties.Settings.Default.LastSessionURIs);
        }
        public void WriteCurrentSession()
        {
            string CurrentSessionURIs = null;
            foreach (TitleBarTab x in this.Tabs)
            {
                CurrentSessionURIs += ((frmCEF)x.Content).chromiumWebBrowser1.Address + ";";
            }
            WriteSessions(CurrentSessionURIs);
        }
        public void CreateTab(string url = "korot://newtab")
        {
            if (!Directory.Exists(profilePath) && profilePath != null) { Directory.CreateDirectory(profilePath); }
            TitleBarTab newTab = new TitleBarTab(this)
            {
                Content = new frmCEF(this, isIncognito, url, Properties.Settings.Default.LastUser)
            };
            this.Tabs.Add(newTab);
        }
        public override TitleBarTab CreateTab()
        {
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
            return new TitleBarTab(this)
            {
                Content = new frmCEF(this, isIncognito, "korot://newtab", Properties.Settings.Default.LastUser)
            };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PrintImages();
        }
        public bool isFullScreen = false;
        public void Fullscreenmode(bool fullscreen)
        {
            isFullScreen = fullscreen;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).Bounds;
            this.FormBorderStyle = fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
            this.WindowState = fullscreen ? FormWindowState.Maximized : FormWindowState.Normal;
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isIncognito)
            {
                Korot.Properties.Settings.Default.WindowPosX = this.Location.X;
                Korot.Properties.Settings.Default.WindowPosY = this.Location.Y;
                Korot.Properties.Settings.Default.WindowSizeH = this.Size.Height;
                Korot.Properties.Settings.Default.WindowSizeW = this.Size.Width;
                if (e.CloseReason != CloseReason.None || e.CloseReason != CloseReason.WindowsShutDown || e.CloseReason != CloseReason.TaskManagerClosing)
                {
                    Korot.Properties.Settings.Default.LastSessionURIs = "";
                }
                else
                {
                    Korot.Properties.Settings.Default.LastSessionURIs = "";
                    foreach (TitleBarTab x in this.Tabs)
                    {
                        Korot.Properties.Settings.Default.LastSessionURIs += ((frmCEF)x.Content).chromiumWebBrowser1.Address + ";";
                    }
                }
                if (!isIncognito) { Properties.Settings.Default.Save(); }
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\")) { } else { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                SaveSettings(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\settings.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\history.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\favorites.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\download.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\cookieDisallow.ksf");
            }
        }

        private void SessionLogger_Tick(object sender, EventArgs e)
        {
            WriteCurrentSession();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            foreach (TitleBarTab x in this.Tabs)
            {
                ((frmCEF)x.Content).Invoke(new Action(() => ((frmCEF)x.Content).FrmCEF_SizeChanged(null, null)));
            }

        }
    }
}
