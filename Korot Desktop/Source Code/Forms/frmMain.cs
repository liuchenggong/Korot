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
using HaltroyTabs;
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
        private MyJumplist list;
        // string[] _args = null;
        public bool isIncognito = false;
        public KorotTabRenderer tabRenderer;
        //TRANSLATE
        public string newincwindow = "New Incognito Window";
        public string newwindow = "New  Window";
        public string Yes = "Yes";
        public string No = "No";
        public string OK = "OK";
        public string Cancel = "Cancel";
        public string newProfileInfo = "Please enter a name for the new profile.It should not contain: ";
        //END
        public frmMain()
        {

            AeroPeekEnabled = true;
            tabRenderer = new KorotTabRenderer(this, Color.Black, Color.White, Color.DodgerBlue, null, false);
            TabRenderer = tabRenderer;
            Icon = Properties.Resources.KorotIcon;
            list = new MyJumplist(this.Handle, this);
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(660, 340);
            this.Size = new Size(Properties.Settings.Default.WindowSizeW, Properties.Settings.Default.WindowSizeH);
            this.Location = new Point(Properties.Settings.Default.WindowPosX, Properties.Settings.Default.WindowPosY);
        }

        private static int Brightness(Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        private static int GerekiyorsaAzalt(int defaultint, int azaltma) => defaultint > azaltma ? defaultint - 20 : defaultint;
        private static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır) => defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        void PrintImages()
        {
            this.MinimumSize = new System.Drawing.Size(660, 340);
            tabRenderer.ChangeColors(Properties.Settings.Default.BackColor, Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White, Properties.Settings.Default.OverlayColor);
            this.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
        }
        void LoadSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory)
        {
            // Settings
            try
            {
                string Playlist = FileSystem2.ReadFile(settingFile, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                Properties.Settings.Default.Homepage = SplittedFase[0].Replace(Environment.NewLine, "");
                Properties.Settings.Default.SearchURL = SplittedFase[1].Replace(Environment.NewLine, "");
                Properties.Settings.Default.downloadOpen = SplittedFase[2].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.downloadClose = SplittedFase[3].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.ThemeFile = SplittedFase[4].Replace(Environment.NewLine, "");
                Properties.Settings.Default.DoNotTrack = SplittedFase[5].Replace(Environment.NewLine, "") == "1";
                if (Properties.Settings.Default.ThemeFile == null || !File.Exists(Properties.Settings.Default.ThemeFile))
                {
                    Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf";
                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf"))
                {
                    string newTheme = "255" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "30" + Environment.NewLine +
                                    "144" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "BACKCOLOR" + Environment.NewLine +
                                    "0";
                    FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf", newTheme, Encoding.UTF8);

                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf"))
                {
                    string newTheme = "255" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "30" + Environment.NewLine +
                                    "144" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "BACKCOLOR" + Environment.NewLine +
                                    "0";
                    FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf", newTheme, Encoding.UTF8);

                }
                // History
                Properties.Settings.Default.History = FileSystem2.ReadFile(historyFile, Encoding.UTF8);
                // Favorites
                Properties.Settings.Default.Favorites = FileSystem2.ReadFile(favoritesFile, Encoding.UTF8);
                // Downloads
                Properties.Settings.Default.DowloadHistory = FileSystem2.ReadFile(downloadHistory, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at reading settings(" + settingFile + ") : " + ex.ToString());
                SaveSettings(settingFile, historyFile, favoritesFile, downloadHistory);
            }
        }
        void SaveSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory)
        {
            try
            {
                // Settings
                string settingsString = Properties.Settings.Default.Homepage + Environment.NewLine +
                Properties.Settings.Default.SearchURL + Environment.NewLine +
                (Properties.Settings.Default.downloadOpen ? "1" : "0") + Environment.NewLine +
                (Properties.Settings.Default.downloadClose ? "1" : "0") + Environment.NewLine +
                Properties.Settings.Default.ThemeFile + Environment.NewLine +
                (Properties.Settings.Default.DoNotTrack ? "1" : "0") + Environment.NewLine;
                FileSystem2.WriteFile(settingFile, settingsString, Encoding.UTF8);
                // History
                FileSystem2.WriteFile(historyFile, Properties.Settings.Default.History, Encoding.UTF8);
                // Favorites
                FileSystem2.WriteFile(favoritesFile, Properties.Settings.Default.Favorites, Encoding.UTF8);

                // Download
                FileSystem2.WriteFile(downloadHistory, Properties.Settings.Default.DowloadHistory, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at saving settings(" + settingFile + ") : " + ex.ToString());
            }
        }

        public bool IsDirectoryEmpty(string path)
        {
            try
            {
                if (Directory.GetDirectories(path).Length > 0) { return false; } else { return true; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IsDirectoryEmpty : " + ex.Message);
                return true;
            }
        }
        public void SwitchProfile(string profilename)
        {
            Properties.Settings.Default.LastUser = profilename;
            Properties.Settings.Default.Save();
            Application.Restart();
        }
        public void DeleteProfile(string profilename)
        {
            Properties.Settings.Default.LastUser = new DirectoryInfo(Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")[0]).Name;
            Properties.Settings.Default.Save();
            frmCEF obj = (frmCEF)Application.OpenForms["frmCEF"]; obj.Close(); CefSharp.Cef.Shutdown();
            Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profilename + "\\", true);
            Properties.Settings.Default.Save();
            Application.Restart();
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
            else if (DateTime.Now.ToString("MM") == "10" & DateTime.Now.ToString("dd") == "18")
            {
                Output.WriteLine("Your account on Instagram is now " + (DateTime.Now.Year - 2017) + " years old!");
                Output.WriteLine("\"Herkes içinde bir yıldız taşır.Önemli olan o yıldızı kullanabilmektir.İyi günler...\" -Haltroy");
            }
            if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\"); }
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\"); }
                if (IsDirectoryEmpty(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                if (!(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"))) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
                if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf"))
                {
                    string newTheme = "255" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "30" + Environment.NewLine +
                                    "144" + Environment.NewLine +
                                    "255" + Environment.NewLine +
                                    "BACKCOLOR" + Environment.NewLine +
                                    "0";
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
                    "0";
                    FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf", newTheme, Encoding.UTF8);
                }
                if (File.Exists(profilePath + "settings.ksf") &&
                        File.Exists(profilePath + "history.ksf") &&
                        File.Exists(profilePath + "favorites.ksf") &&
                        File.Exists(profilePath + "download.ksf"))
                {
                    LoadSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf");
                }
                else
                {
                    SaveSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf");
                }

                try
                {
                    PrintImages();
                }
                catch { }

            }
            else
            { Process.Start(Application.ExecutablePath, "-oobe"); this.Close(); }
            this.Location = new Point(Korot.Properties.Settings.Default.WindowPosX, Korot.Properties.Settings.Default.WindowPosY);
            this.Size = new Size(Korot.Properties.Settings.Default.WindowSizeW, Korot.Properties.Settings.Default.WindowSizeH);

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
                Properties.Settings.Default.Save();
            }
            catch { }
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
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
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
        public void Fullscreenmode(bool fullscreen)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).Bounds;
            this.FormBorderStyle = fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
            this.WindowState = fullscreen ? FormWindowState.Maximized : FormWindowState.Normal;
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
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
            Korot.Properties.Settings.Default.Save();
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\")) { } else { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
            SaveSettings(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\settings.ksf", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\history.ksf", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\favorites.ksf", Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\download.ksf");

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
