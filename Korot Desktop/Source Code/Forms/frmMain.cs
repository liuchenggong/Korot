using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HaltroyTabs;

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
            tabRenderer = new KorotTabRenderer(this, Color.Black, Color.White, Color.DodgerBlue);
            TabRenderer = tabRenderer;
            Icon = Properties.Resources.KorotIcon;
            list = new MyJumplist(this.Handle,this);
            InitializeComponent();
            this.MinimumSize = new System.Drawing.Size(660, 340);
            this.Size = new Size(Properties.Settings.Default.WindowSizeW, Properties.Settings.Default.WindowSizeH);
            this.Location = new Point(Properties.Settings.Default.WindowPosX, Properties.Settings.Default.WindowPosY);
            this.Font = new Font("Ubuntu",this.Font.Size);
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
                StreamReader ReadFile = new StreamReader(settingFile, Encoding.UTF8, false);
                string Playlist = ReadFile.ReadToEnd();
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                Properties.Settings.Default.Homepage = SplittedFase[0].Replace(Environment.NewLine, "");
                Properties.Settings.Default.SearchURL = SplittedFase[1].Replace(Environment.NewLine, "");
                Properties.Settings.Default.downloadOpen = SplittedFase[2].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.downloadClose = SplittedFase[3].Replace(Environment.NewLine, "") == "1";
                Properties.Settings.Default.ThemeFile = SplittedFase[4].Replace(Environment.NewLine, "");
                ReadFile.Close();
                if (Properties.Settings.Default.ThemeFile == null || !File.Exists(Properties.Settings.Default.ThemeFile))
                {
                    Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf";
                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf"))
                {
                    StreamWriter newtheme = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf");
                    newtheme.WriteLine("255");
                    newtheme.WriteLine("255");
                    newtheme.WriteLine("255");
                    newtheme.WriteLine("30");
                    newtheme.WriteLine("144");
                    newtheme.WriteLine("255");
                    newtheme.WriteLine("background-color: rgb(255,255,255)");
                    newtheme.Close();
                }
                if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf"))
                {
                    StreamWriter newtheme = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf");
                    newtheme.WriteLine("0");
                    newtheme.WriteLine("0");
                    newtheme.WriteLine("0");
                    newtheme.WriteLine("30");
                    newtheme.WriteLine("144");
                    newtheme.WriteLine("255");
                    newtheme.WriteLine("background-color: rgb(0,0,0)");
                    newtheme.Close();
                }
                // History
                StreamReader ReadFile1 = new StreamReader(historyFile, Encoding.UTF8, false);
                Properties.Settings.Default.History = ReadFile1.ReadToEnd();
                ReadFile1.Close();
                // Favorites
                StreamReader ReadFile2 = new StreamReader(favoritesFile, Encoding.UTF8, false);
                Properties.Settings.Default.Favorites = ReadFile2.ReadToEnd();
                ReadFile2.Close();
                // Theme

                // Downloads
                StreamReader ReadFile4 = new StreamReader(downloadHistory, Encoding.UTF8, false);
                Properties.Settings.Default.DowloadHistory = ReadFile4.ReadToEnd();
                ReadFile4.Close();
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
                System.IO.StreamWriter objWriter;
                objWriter = new System.IO.StreamWriter(settingFile);
                objWriter.WriteLine(Properties.Settings.Default.Homepage);
                objWriter.WriteLine(Properties.Settings.Default.SearchURL);
                if (Properties.Settings.Default.downloadOpen) { objWriter.WriteLine("1"); } else { objWriter.WriteLine("0"); }
                if (Properties.Settings.Default.downloadClose) { objWriter.WriteLine("1"); } else { objWriter.WriteLine("0"); }
                objWriter.WriteLine(Properties.Settings.Default.ThemeFile);
                objWriter.Close();
                // History
                System.IO.StreamWriter objWriter1;
                objWriter1 = new System.IO.StreamWriter(historyFile);
                objWriter1.WriteLine(Properties.Settings.Default.History);
                objWriter1.Close();
                // Favorites
                System.IO.StreamWriter objWriter2;
                objWriter2 = new System.IO.StreamWriter(favoritesFile);
                objWriter2.WriteLine(Properties.Settings.Default.Favorites);
                objWriter2.Close();
                // Download
                System.IO.StreamWriter objWriter4;
                objWriter4 = new System.IO.StreamWriter(downloadHistory);
                objWriter4.WriteLine(Properties.Settings.Default.DowloadHistory);
                objWriter4.Close();
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
            HaltroyFramework.HaltroyInputBox newprof = new HaltroyFramework.HaltroyInputBox("Korot",newProfileInfo + Environment.NewLine + "/ \\ : ? * |", this.Icon, "", Properties.Settings.Default.BackColor, Properties.Settings.Default.OverlayColor, OK,Cancel, 400, 150);
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
        public bool IsProcessOpen(string name)
        {
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                //now we're going to see if any of the running processes
                //match the currently running processes. Be sure to not
                //add the .exe to the name you provide, i.e: NOTEPAD,
                //not NOTEPAD.EXE or false is always returned even if
                //notepad is running.
                //Remember, if you have the process running more than once, 
                //say IE open 4 times the loop thr way it is now will close all 4,
                //if you want it to just close the first one it finds
                //then add a return; after the Kill
                if (clsProcess.ProcessName.Contains(name))
                {
                    //if the process is found to be running then we
                    //return a true
                    return true;
                }
            }
            //otherwise we return a false
            return false;
        }
        public string restoremedaddy = "";
        string profilePath;
       public List<frmCEF> CefFormList = new List<frmCEF>();
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            if (DateTime.Now.ToString("MM") == "03" & DateTime.Now.ToString("dd") == "11")
            {
                Output.WriteLine("Happy " + (DateTime.Now.Year - 2001) + "th Birthday Dad!");
                List<string> HaltroyNameHistory = new List<string>() {"efojaeren","Eren Kanat","ErenKanat02","ErenKanat03","Lapisman","LapisGamingTR","NirvanaWolfTR","TheLordEren","SnowWolfTR","Pell Game","Pell Artz","TheEfoja","Mr Pell","Pellguy","LordPell","Pellaraptor","Pellraptor","Pellerma","PLLTR","SpringTR","KurtSys32","KANAT","Spiklyman (vs Mendebur Lemur)","Wingaxy","Haltroy"};
                Output.WriteLine("Here's a quick history about you:");
                foreach (string x in HaltroyNameHistory)
                {
                    Output.WriteLine(x);
                }
                Output.WriteLine("That's a lot of usernames but each of them is still terrible since day 0.");
            } else if (DateTime.Now.ToString("MM") == "06" & DateTime.Now.ToString("dd") == "09")
            {
                List<string> KorotNameHistory = new List<string>(){"StoneHomepage (not browser) (First code written by Haltroy)","StoneBrowser (Trident) (First ever program written by Haltroy)", "ZStone (Trident)", "Pell Browser (Trident)", "Kolme Browser (Trident)", "Ninova (Gecko)", "Webtroy (Gecko,CEF)", "Korot (CEF)","Korot (Boron)"};
                Output.WriteLine("I'm " + (DateTime.Now.Year - 2017) + " years old now!");
                Output.WriteLine("Here's a quick history about me:");
                foreach (string x in KorotNameHistory)
                {
                    Output.WriteLine(x);
                }
                Output.WriteLine("I'm too far now isn't it?");
            } else if (DateTime.Now.ToString("MM") == "01" & DateTime.Now.ToString("dd") == "21")
            {
                List<string> PTNameHistory = new List<string>(){"Pell Media Player (WMP)","ZStone (WMP)","Kolme Player (WMP)","MyPlay (WMP)","Playtroy"};
                Output.WriteLine("My sister's " + (DateTime.Now.Year - 2017) + " years old now!");
                Output.WriteLine("Here's a quick history about my sister:");
                foreach (string x in PTNameHistory)
                {
                    Output.WriteLine(x);
                }
                Output.WriteLine("She's too far now isn't it?");
            }
            else if (DateTime.Now.ToString("MM") == "10" & DateTime.Now.ToString("dd") == "18")
            {
                Output.WriteLine("Your account on Instagram is now " + (DateTime.Now.Year - 2017) + " years old!");
                Output.WriteLine("\"Herkes içinde bir yıldız taşır.Önemli olan o yıldızı kullanabilmektir.İyi günler...\" -Haltroy");
            }
            if (Properties.Settings.Default.LastUser == "") { Properties.Settings.Default.LastUser = "user0"; }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\"); }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\"); }
            if (IsDirectoryEmpty(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
            if (!(Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"))) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\Korot Light.ktf"))
            {
                StreamWriter newtheme = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf");
                newtheme.WriteLine("255");
                newtheme.WriteLine("255");
                newtheme.WriteLine("255");
                newtheme.WriteLine("30");
                newtheme.WriteLine("144");
                newtheme.WriteLine("255");
                newtheme.WriteLine("background-color: rgb(255,255,255)");
                newtheme.Close();
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\Korot Dark.ktf"))
            {
                StreamWriter newtheme = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf");
                newtheme.WriteLine("0");
                newtheme.WriteLine("0");
                newtheme.WriteLine("0");
                newtheme.WriteLine("30");
                newtheme.WriteLine("144");
                newtheme.WriteLine("255");
                newtheme.WriteLine("background-color: rgb(0,0,0)");
                newtheme.Close();
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
            CreateTab(Korot.Properties.Settings.Default.Homepage);

            
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
            Properties.Settings.Default.LastSessionURIs = Session;
            Properties.Settings.Default.Save();
        }
        public void ReadLatestCurrentSession()
        {
            ReadSession(Properties.Settings.Default.LastSessionURIs);
        }
        public void WriteCurrentSession()
        {
            string CurrentSessionURIs = null;
            foreach (frmCEF tabform in CefFormList)
            {
                CurrentSessionURIs += ((frmCEF)tabform).chromiumWebBrowser1.Address + ";";
            }
            WriteSessions(CurrentSessionURIs);
        }
        public void RemoveMefromList(frmCEF myself)
        {
            if (CefFormList.Contains(myself))
            {
                CefFormList.Remove(myself);
            }
        }
        public TitleBarTab CreateTab(string url = "korot://newtab")
        {
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
            return new TitleBarTab(this)
            {
                Content = new frmCEF(this, isIncognito, url, Properties.Settings.Default.LastUser)
            };
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
            }else
            {
                Korot.Properties.Settings.Default.LastSessionURIs = "";
                foreach (frmCEF x in CefFormList)
                {
                    Korot.Properties.Settings.Default.LastSessionURIs += x.chromiumWebBrowser1.Address;
                }
            }
            Korot.Properties.Settings.Default.Save();
            if (Directory.Exists(Application.StartupPath + "\\Profiles\\user0\\")) { } else { Directory.CreateDirectory(Application.StartupPath + "\\Profiles\\user0\\"); }
            SaveSettings(Application.StartupPath + "\\Profiles\\user0\\settings.ksf", Application.StartupPath + "\\Profiles\\user0\\history.ksf", Application.StartupPath + "\\Profiles\\user0\\favorites.ksf", Application.StartupPath + "\\Profiles\\user0\\download.ksf");
            
        }

        private void SessionLogger_Tick(object sender, EventArgs e)
        {
            WriteCurrentSession();
        }
     
        private void frmMain_Resize(object sender, EventArgs e)
        {
            foreach (frmCEF cefform in CefFormList)
            {
                cefform.Invoke(new Action(() => cefform.FrmCEF_SizeChanged(null, null)));
            }
        }
    }


}
