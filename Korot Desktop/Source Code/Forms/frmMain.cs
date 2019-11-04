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

namespace Korot
{

    public partial class frmMain : HaltroyFramework.HaltroyForms
    {
        frmSettings Settingsfrm;
        private MyJumplist list;
        bool isMouseDown = false;
        Point mouseposition;
        string[] _args = null;
        string maname = " - Korot";
        bool isIncognito = false;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public frmMain(string[] args,frmSettings formsettings)
        {
            InitializeComponent();
            Settingsfrm = formsettings;
            if (args.Contains("-incognito"))
            {
                isIncognito = true;
            }
            _args = args;
            list = new MyJumplist(this.Handle, Settingsfrm);
            
        }
        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks > 1) 
                {
                    frmMain_MouseDoubleClick(sender, e);
                }
                else
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    ReleaseCapture();
                }
            }
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
            this.BackColor = Properties.Settings.Default.BackColor;
            tabControl1.BackTabColor = Properties.Settings.Default.BackColor;
            tabControl1.BorderColor = Properties.Settings.Default.BackColor;
            tabControl1.HeaderColor = Properties.Settings.Default.BackColor;
            tabControl1.ActiveColor = Properties.Settings.Default.OverlayColor;
            tabControl1.HorizontalLineColor = Properties.Settings.Default.OverlayColor;            
           
            menuStrip1.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabControl1.SelectedTextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabControl1.TextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
           
            menuStrip1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;    
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
            HaltroyFramework.HaltroyInputBox newprof = new HaltroyFramework.HaltroyInputBox("Korot",Settingsfrm.newProfileInfo + Environment.NewLine + "/ \\ : ? * |", this.Icon, "", Properties.Settings.Default.BackColor, Properties.Settings.Default.OverlayColor, Settingsfrm.OK, Settingsfrm.Cancel, 400, 150);
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
            betaTS.Text = "Korot Beta " + Application.ProductVersion.ToString();
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
            NewTab(Korot.Properties.Settings.Default.Homepage);

            foreach (string x in _args)
            {
                if (x == Application.ExecutablePath) { } else if (x == "-incognito") { } else { NewTab(x); }
            }
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
                NewTab(SplittedFase[i].Replace(Environment.NewLine, ""));
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
            else
            {
                throw new InvalidOperationException("How tf did you get this error message?Tell me niBBa!");
            }
        }
        public void TabText(int TabID, string TabText)
        {
            tabControl1.Invoke(new Action(() => tabControl1.TabPages[TabID].Text = TabText.ToString()));
        }
        public void NewTab(string url)
        {
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
            TabPage tab = new TabPage();
            frmCEF form = new frmCEF(tab, this,Settingsfrm, isIncognito, url, Properties.Settings.Default.LastUser);
            form.TopMost = false;
            form.TopLevel = false;
            form.Visible = true;
            tab.Text = Settingsfrm.newtabtitle;
            form.Dock = DockStyle.Fill;
            form.ShowInTaskbar = true;
            CefFormList.Add(form);
            tabControl1.Invoke(new Action(() => tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, tab)));
            tab.Controls.Add(form);
            tabControl1.Invoke(new Action(() => tabControl1.SelectedTab = tab));
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                tabControl1.SelectedIndex -= 1;
                NewTab("korot://newtab");
            }

            else
            {
                if (tabControl1.SelectedTab == null) { } else { this.Text = tabControl1.SelectedTab.Text + maname; }
            }
            if (tabControl1.TabPages.Count == 1) { this.Close(); }
            PrintImages();
            tabPage2.Text = "+";
            if (this.WindowState == FormWindowState.Maximized) { button2.Text = "▫"; } else { button2.Text = "□"; }
        }
        public void Fullscreenmode(bool fullscreen)
        {
            this.MaximizedBounds = Screen.FromHandle(this.Handle).Bounds;
            this.FormBorderStyle = fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
            this.WindowState = fullscreen ? FormWindowState.Maximized : FormWindowState.Normal;
            menuStrip1.Visible = !fullscreen;
        }
        private void frmMain_MouseDoubleClick(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
                    this.WindowState = FormWindowState.Maximized;
                    button2.Text = "▫";
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                    button2.Text = "□";
                }
            }
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Korot.Properties.Settings.Default.WindowPosX = this.Location.X;
            Korot.Properties.Settings.Default.WindowPosY = this.Location.Y;
            Korot.Properties.Settings.Default.WindowSizeH = this.Size.Height;
            Korot.Properties.Settings.Default.WindowSizeW = this.Size.Width;
            Korot.Properties.Settings.Default.Save();
            if (Directory.Exists(Application.StartupPath + "\\Profiles\\user0\\")) { } else { Directory.CreateDirectory(Application.StartupPath + "\\Profiles\\user0\\"); }
            SaveSettings(Application.StartupPath + "\\Profiles\\user0\\settings.ksf", Application.StartupPath + "\\Profiles\\user0\\history.ksf", Application.StartupPath + "\\Profiles\\user0\\favorites.ksf", Application.StartupPath + "\\Profiles\\user0\\download.ksf");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastSessionURIs = "";
            Korot.Properties.Settings.Default.Save();
            this.Close();
        }



        private void SessionLogger_Tick(object sender, EventArgs e)
        {
            WriteCurrentSession();
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape & e.Shift == true)
            {
                isMouseDown = false;
                this.Top = 0;
                this.Left = 0;
            }
        }
        bool isShiftKeyDown = false;
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private static readonly int VK_ESCAPE = 0x1B;
        private static readonly int VK_SHIFT = 0x10;
        private void TmrSlower_Tick(object sender, EventArgs e)
        {
            try
            {
                short keyState = GetAsyncKeyState(VK_ESCAPE);
                short keyState2 = GetAsyncKeyState(VK_SHIFT);
                //Check if the MSB is set. If so, then the key is pressed.
                bool escIsPressed = ((keyState >> 15) & 0x0001) == 0x0001;

                //Check if the LSB is set. If so, then the key was pressed since
                //the last call to GetAsyncKeyState
                bool unprocessedPress = ((keyState >> 0) & 0x0001) == 0x0001;

                bool shiftIsPressed = ((keyState2 >> 15) & 0x0001) == 0x0001;

                //Check if the LSB is set. If so, then the key was pressed since
                //the last call to GetAsyncKeyState
                bool unprocessedPress2 = ((keyState2 >> 0) & 0x0001) == 0x0001;

                isShiftKeyDown = shiftIsPressed | unprocessedPress2;
                if ((escIsPressed & shiftIsPressed) | (escIsPressed & unprocessedPress2) | (unprocessedPress & shiftIsPressed) | (unprocessedPress & unprocessedPress2))
                {
                    isMouseDown = false;
                    this.Top = 0;
                    this.Left = 0;
                }
            }
            catch
            {
                //Ignored
            }
        }

        private static ManagementObject GetMngObj(string className)
        {
            var wmi = new ManagementClass(className);

            foreach (var o in wmi.GetInstances())
            {
                var mo = (ManagementObject)o;
                if (mo != null) return mo;
            }

            return null;
        }

        public static string GetOsVer()
        {
            try
            {
                ManagementObject mo = GetMngObj("Win32_OperatingSystem");

                if (null == mo)
                    return string.Empty;

                return mo["Version"] as string;
            }
            catch
            {
                return string.Empty;
            }
        }

        
        private void korotBeta453ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (betaTS.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
                {
                    betaTS.DisplayStyle = ToolStripItemDisplayStyle.Image;
                }else if (betaTS.DisplayStyle == ToolStripItemDisplayStyle.Image)
                {
                    betaTS.DisplayStyle = ToolStripItemDisplayStyle.Text;
                }
                else if (betaTS.DisplayStyle == ToolStripItemDisplayStyle.Text)
                {
                    betaTS.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                }
        }


        private void frmMain_Resize(object sender, EventArgs e)
        {
            foreach (frmCEF cefform in CefFormList)
            {
                cefform.Invoke(new Action(() => cefform.FrmCEF_SizeChanged(null, null)));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MouseEventArgs newe = new MouseEventArgs(MouseButtons.Left, 2, Cursor.Position.X, Cursor.Position.Y,0);
            frmMain_MouseDoubleClick(sender, newe);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settingsfrm.Show();
        }
    }


}
