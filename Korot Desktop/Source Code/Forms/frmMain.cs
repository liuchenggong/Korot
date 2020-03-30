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
using System.Windows.Forms;

namespace Korot
{

    public partial class frmMain : TitleBarTabs
    {
        public List<DownloadItem> CurrentDownloads = new List<DownloadItem>();
        public List<string> CancelledDownloads = new List<string>();
        public bool isIncognito = false;
        public KorotTabRenderer tabRenderer;
        public frmMain()
        {

            AeroPeekEnabled = true;
            tabRenderer = new KorotTabRenderer(this, Color.Black, Color.White, Color.DodgerBlue, null, false);
            TabRenderer = tabRenderer;
            Icon = Properties.Resources.KorotIcon;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
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

        private void PrintImages()
        {
            MinimumSize = new System.Drawing.Size(660, 340);
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
        }


        public string restoremedaddy = "";
        private string profilePath;
        private void frmMain_Load(object sender, EventArgs e)
        {
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
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
                profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
                if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
                Tools.createThemes();
                if (File.Exists(profilePath + "settings.ksf") &&
                        File.Exists(profilePath + "history.ksf") &&
                        File.Exists(profilePath + "favorites.ksf") &&
                        File.Exists(profilePath + "download.ksf") &&
                        File.Exists(profilePath + "cookieDisallow.ksf"))
                {
                    Tools.LoadSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf",
                        profilePath + "cookieDisallow.ksf");
                }
                else
                {
                    Tools.SaveSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf",
                        profilePath + "cookieDisallow.ksf");
                }
            }
            else
            { Process.Start(Application.ExecutablePath, "-oobe"); Close(); }

            Location = new Point(Korot.Properties.Settings.Default.WindowPosX, Korot.Properties.Settings.Default.WindowPosY);
            Size = new Size(Korot.Properties.Settings.Default.WindowSizeW, Korot.Properties.Settings.Default.WindowSizeH);
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
            MinimumSize = new System.Drawing.Size(660, 340);
            MaximizedBounds = Screen.GetWorkingArea(this);
            if (Properties.Settings.Default.windowState == 0) { WindowState = FormWindowState.Normal; }
            else if (Properties.Settings.Default.windowState == 1) { WindowState = FormWindowState.Maximized; }
            else if (Properties.Settings.Default.windowState == 2) { WindowState = FormWindowState.Minimized; }
            else
            { Properties.Settings.Default.windowState = 0; WindowState = FormWindowState.Normal; }
            Size = new Size(Properties.Settings.Default.WindowSizeW, Properties.Settings.Default.WindowSizeH);
            Location = new Point(Properties.Settings.Default.WindowPosX, Properties.Settings.Default.WindowPosY);

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
            foreach (TitleBarTab x in Tabs)
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
                Content = new frmCEF(isIncognito, url, Properties.Settings.Default.LastUser)
            };
            Tabs.Add(newTab);
        }
        public override TitleBarTab CreateTab()
        {
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
            return new TitleBarTab(this)
            {
                Content = new frmCEF(isIncognito, "korot://newtab", Properties.Settings.Default.LastUser)
            };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            PrintImages();
        }
        public bool isFullScreen = false;
        public bool wasMaximized = false;
        public void Fullscreenmode(bool fullscreen)
        {
            if (fullscreen)
            {
                wasMaximized = WindowState == FormWindowState.Maximized;
                WindowState = FormWindowState.Maximized;
                MaximizedBounds = Screen.FromHandle(Handle).Bounds;
                isFullScreen = true;
            }
            else
            {
                if (!wasMaximized)
                {
                    WindowState = FormWindowState.Normal;
                }
                MaximizedBounds = Screen.GetWorkingArea(this);
                isFullScreen = false;
            }
            FormBorderStyle = fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isIncognito)
            {
                Korot.Properties.Settings.Default.WindowPosX = Location.X;
                Korot.Properties.Settings.Default.WindowPosY = Location.Y;
                Korot.Properties.Settings.Default.WindowSizeH = Size.Height;
                Korot.Properties.Settings.Default.WindowSizeW = Size.Width;
                if (WindowState == FormWindowState.Normal) { Properties.Settings.Default.windowState = 0; }
                else if (WindowState == FormWindowState.Maximized) { Properties.Settings.Default.windowState = 1; }
                else if (WindowState == FormWindowState.Minimized) { Properties.Settings.Default.windowState = 2; }
                Korot.Properties.Settings.Default.dismissUpdate = false;
                Korot.Properties.Settings.Default.alreadyUpdatedThemes = false;
                Korot.Properties.Settings.Default.alreadyUpdatedExt = false;
                Properties.Settings.Default.disableLangErrors = false;
                if (WindowState == FormWindowState.Normal) { Properties.Settings.Default.windowState = 0; }
                else if (WindowState == FormWindowState.Maximized) { Properties.Settings.Default.windowState = 1; }
                else if (WindowState == FormWindowState.Minimized) { Properties.Settings.Default.windowState = 2; }
                if (e.CloseReason != CloseReason.None || e.CloseReason != CloseReason.WindowsShutDown || e.CloseReason != CloseReason.TaskManagerClosing)
                {
                    Korot.Properties.Settings.Default.LastSessionURIs = "";
                }
                else
                {
                    Korot.Properties.Settings.Default.LastSessionURIs = "";
                    foreach (TitleBarTab x in Tabs)
                    {
                        Korot.Properties.Settings.Default.LastSessionURIs += ((frmCEF)x.Content).chromiumWebBrowser1.Address + ";";
                    }
                }
                if (!isIncognito) { Properties.Settings.Default.Save(); }
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\")) { } else { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                Tools.SaveSettings(
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
            foreach (TitleBarTab x in Tabs)
            {
                ((frmCEF)x.Content).Invoke(new Action(() => ((frmCEF)x.Content).FrmCEF_SizeChanged(null, null)));
            }

        }
    }
}
