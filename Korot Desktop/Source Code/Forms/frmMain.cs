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

    public partial class frmMain : Form
    {

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                // If this form is inherited, the IDE needs this style
                // set so that it's coordinate system is correct.
                const Int32 WS_CHILDWINDOW = 0x40000000;
                // The following two styles are used to clip child
                // and sibling windows in Paint events.
                const Int32 WS_CLIPCHILDREN = 0x2000000;
                const Int32 WS_CLIPSIBLINGS = 0x4000000;
                // Add a Minimize button (or Minimize option in Window Menu).
                const Int32 WS_MINIMIZEBOX = 0x20000;
                // Add a Maximum/Restore Button (or Options in Window Menu).
                const Int32 WS_MAXIMIZEBOX = 0x10000;
                // Window can be resized.
                const Int32 WS_THICKFRAME = 0x40000;
                // Add A Window Menu
                const Int32 WS_SYSMENU = 0x80000;

                // Detect Double Clicks
                const int CS_DBLCLKS = 0x8;
                // Add a DropShadow (WinXP or greater).
                const int CS_DROPSHADOW = 0x20000;

                CreateParams cp = base.CreateParams;

                cp.Style = WS_CLIPCHILDREN | WS_CLIPSIBLINGS
            | WS_MAXIMIZEBOX | WS_MINIMIZEBOX
            | WS_SYSMENU | WS_THICKFRAME;

                if (this.DesignMode)
                    cp.Style = cp.Style | WS_CHILDWINDOW;

                int ClassFlags = CS_DBLCLKS;
                int OSVER = Environment.OSVersion.Version.Major * 10;
                OSVER += Environment.OSVersion.Version.Minor;

                if (OSVER >= 51)
                    ClassFlags = ClassFlags | CS_DROPSHADOW;
                cp.ClassStyle = ClassFlags;

                return cp;
            }
        }
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
        public frmMain(string[] args)
        {
            InitializeComponent();

            if (args.Contains("-incognito"))
            {
                isIncognito = true;
            }
            _args = args;
            list = new MyJumplist(this.Handle, this);
            
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
            tabControl2.ActiveColor = Properties.Settings.Default.OverlayColor;
            tabControl2.BackTabColor = Properties.Settings.Default.BackColor;
            tabControl2.BorderColor = Properties.Settings.Default.BackColor;
            tabControl2.HeaderColor = Properties.Settings.Default.BackColor;
            tabControl2.HorizontalLineColor = Properties.Settings.Default.OverlayColor;
            cmsDownload.BackColor = Properties.Settings.Default.BackColor;
            hlvDownload.BackColor = Properties.Settings.Default.BackColor;
            hlvHistory.BackColor = Properties.Settings.Default.BackColor;
            cmsHistory.BackColor = Properties.Settings.Default.BackColor;
            cmsSearchEngine.BackColor = Properties.Settings.Default.BackColor;
            menuStrip1.BackColor = Properties.Settings.Default.BackColor;
            this.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabControl1.SelectedTextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabControl1.TextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabControl2.SelectedTextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            tabControl2.TextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            cmsDownload.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            listBox2.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            comboBox1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            textBox2.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            textBox3.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            button10.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            hlvDownload.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            lbLang.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            hlvHistory.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            lbLang.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            cmsHistory.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            cmsSearchEngine.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            menuStrip1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            button4.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.Settings_w : Properties.Resources.Settings;
            listBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            comboBox1.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            textBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            textBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            button10.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            textBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            textBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            lbLang.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            lbLang.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
            foreach (TabPage x in tabControl2.TabPages)
            {
                x.BackColor = Properties.Settings.Default.BackColor;
                x.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
            }       
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
        WebClient UpdateWebC = new WebClient();
        public void Updater()
        {
            UpdateWebC.DownloadStringCompleted += Updater_DownloadStringCompleted;
            UpdateWebC.DownloadProgressChanged += updater_checking;
            UpdateWebC.DownloadStringAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2123&authkey=!ADjFaqhHH3MjOAQ&ithint=file%2ctxt&e=5QH8I8"));
            updateProgress = 0;
        }
        bool alreadyCheckedForUpdatesOnce = false;
        private void updater_checking(object sender, DownloadProgressChangedEventArgs e)
        {
            lbUpdateStatus.Text = checking;
            updateProgress = 0;
            btUpdater.Visible = false;
        }
        private void Updater_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                updateProgress = 3;
                btUpdater.Location = btInstall.Location;
                btInstall.Visible = false;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = updateError;
            }
            else
            {
                Version newest = new Version(e.Result);
                Version current = new Version(Application.ProductVersion);
                if (newest > current)
                {
                    if (alreadyCheckedForUpdatesOnce)
                    {
                        btUpdater.Location = new Point(btInstall.Location.X, btInstall.Location.Y + 5 + btInstall.Height);
                        updateProgress = 2;
                        lbUpdateStatus.Text = updateavailable;
                        btUpdater.Visible = true;
                        btInstall.Visible = true;
                    }
                    else
                    {
                        btUpdater.Location = new Point(btInstall.Location.X, btInstall.Location.Y + 5 + btInstall.Height);
                        alreadyCheckedForUpdatesOnce = true;
                        updateProgress = 2;
                        lbUpdateStatus.Text = updateavailable;
                        btInstall.Visible = true;
                        btUpdater.Visible = true;
                        HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(updateTitle, updateMessage, this.Icon, MessageBoxButtons.YesNo, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140);
                        DialogResult diagres = mesaj.ShowDialog();
                        if (diagres == DialogResult.Yes)
                        {
                            Form1 frmUpdate = new Form1(this);
                            frmUpdate.Show();
                        }
                    }
                }
                else
                {
                    btUpdater.Visible = true;
                    btInstall.Visible = false;
                    btUpdater.Location = btInstall.Location;
                    updateProgress = 1;
                    lbUpdateStatus.Text = uptodate;
                }
            }
        }
        string profilePath;
        frmSettings frmS = new frmSettings();
        List<frmCEF> CefFormList = new List<frmCEF>();
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
            tbKorot.Parent = null;
            frmS.tabControl1.TabPages.Add(tbKorot);
            Updater();
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
            //directly copied from frmCEF
            try
            {
                comboBox1.Text = new FileInfo(Properties.Settings.Default.ThemeFile).Name.Replace(".ktf", "");
            }
            catch
            {
                comboBox1.Text = "";
            }
            label6.Text = "Beta " + Application.ProductVersion;
            textBox2.Text = Properties.Settings.Default.Homepage;
            textBox3.Text = Properties.Settings.Default.SearchURL;
            if (Properties.Settings.Default.Homepage == "korot://newtab") { radioButton1.Enabled = true; }
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshLangList();
            if (Properties.Settings.Default.LangFile == null) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }
            LoadLangFromFile(Properties.Settings.Default.LangFile);
            refreshThemeList();
            PrintImages();
            RefreshDownloadList();
            if (Properties.Settings.Default.LastSessionURIs == "")
            {
                menuStrip1.Items.Remove(restorerStrip);
                restorerStrip.Visible = false;
            }
            else
            {
                restorerStrip.Visible = true;
                restorerStrip.Tag = Properties.Settings.Default.LastSessionURIs;
                restorerStrip.ShowDropDown();
            }
            SessionLogger.Start();
        }
        void RefreshHistory()
        {
            hlvHistory.Items.Clear();
            Debug.WriteLine("History : " + Properties.Settings.Default.History);
            string Playlist = Properties.Settings.Default.History;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                ListViewItem listV = new ListViewItem(SplittedFase[i].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 1].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 2].Replace(Environment.NewLine, ""));
                hlvHistory.Items.Add(listV);
                i += 3;

            }
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
            frmCEF form = new frmCEF(tab, this, isIncognito, url, Properties.Settings.Default.LastUser);
            form.TopMost = false;
            form.TopLevel = false;
            form.Visible = true;
            tab.Text = newtabtitle;
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

        private void Timer2_Tick(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshHistory();
            RefreshDownloadList();
        }
        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string x = hlvHistory.SelectedItems[0].SubItems[0].Text + ";";
            string y = hlvHistory.SelectedItems[0].SubItems[1].Text + ";";
            string t = hlvHistory.SelectedItems[0].SubItems[2].Text + ";";
            Properties.Settings.Default.History = Properties.Settings.Default.History.Replace(x + y + t, "");
            Properties.Settings.Default.Save();
            RefreshHistory();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.History = "";
            Properties.Settings.Default.Save();
            RefreshHistory();
        }



        private void Label2_Click(object sender, EventArgs e)
        {
            NewTab("http://korot.haltroy.com");
        }


        private void LbLang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView1_DoubleClick(null, null);
        }

        public void LoadTheme(string themeFile)
        {
            try
            {
                StreamReader ReadFile3 = new StreamReader(themeFile, Encoding.UTF8, false);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string Playlist3 = ReadFile3.ReadToEnd();
                string[] SplittedFase3 = Playlist3.Split(token);
                int backR = System.Convert.ToInt32(SplittedFase3[0].Replace(Environment.NewLine, ""));

                int backG = System.Convert.ToInt32(SplittedFase3[1].Replace(Environment.NewLine, ""));

                int backB = System.Convert.ToInt32(SplittedFase3[2].Replace(Environment.NewLine, ""));

                int ovR = System.Convert.ToInt32(SplittedFase3[3].Replace(Environment.NewLine, ""));

                int ovG = System.Convert.ToInt32(SplittedFase3[4].Replace(Environment.NewLine, ""));

                int ovB = System.Convert.ToInt32(SplittedFase3[5].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.BackColor = Color.FromArgb(255, backR, backG, backB);
                Properties.Settings.Default.OverlayColor = Color.FromArgb(255, ovR, ovG, ovB);
                Properties.Settings.Default.BackStyle = SplittedFase3[6].Replace(Environment.NewLine, "").Replace("[THEMEFOLDER]", "file://" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\");
                pictureBox3.BackColor = Properties.Settings.Default.BackColor;
                pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
                Properties.Settings.Default.ThemeFile = themeFile;

                ReadFile3.Close();
            }
            catch (Exception ex)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle,
                                                                                          ErrorTheme,
                                                                                          this.Icon,
                                                                                          MessageBoxButtons.OK,
                                                                                          Properties.Settings.Default.BackColor,
                                                                                          Yes, No, OK, Cancel, 390, 140);

                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(ex.ToString());

                LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf");
            }
        }
        public void refreshThemeList()
        {
            listBox2.Items.Clear();
            foreach (String x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"))
            {
                if (x.EndsWith(".ktf", StringComparison.OrdinalIgnoreCase))
                {
                    listBox2.Items.Add(new FileInfo(x).Name);
                }
            }
        }


        private void Button10_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter objWriter3;
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            objWriter3 = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf");
            string x = Properties.Settings.Default.BackStyle;
            string lol = Properties.Settings.Default.BackColor.R + Environment.NewLine + Properties.Settings.Default.BackColor.G + Environment.NewLine + Properties.Settings.Default.BackColor.B + Environment.NewLine + Properties.Settings.Default.OverlayColor.R + Environment.NewLine + Properties.Settings.Default.OverlayColor.G + Environment.NewLine + Properties.Settings.Default.OverlayColor.B + Environment.NewLine + x + Environment.NewLine;
            objWriter3.WriteLine(lol);
            objWriter3.Close();
            Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf";
            refreshThemeList();
        }

        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ThemesTitle, listBox2.SelectedItem.ToString() + Environment.NewLine + ThemeMessage, this.Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140);
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + listBox2.SelectedItem.ToString());
                    comboBox1.Text = listBox2.SelectedItem.ToString().Replace(".ktf", "");

                }
            }
        }
        int updateProgress = 0;
        //0 = Checking 1=UpToDate 2=UpdateAvailabe 3=Error
        #region "Translate"
        public string usesCookies = "This website uses cookies.";
        public string notUsesCookies = "This website does not use cookies.";
        public string showCertError = "Show Certificate Error";
        public string CertificateErrorMenuTitle = "Certificate Error Details";
        public string CertificateErrorTitle = "Not Safe";
        public string CertificateError = "This website is using a certificate that has errors.";
        public string CertificateOKTitle = "Safe";
        public string CertificateOK = "This website is using a certificate that has no errors.";
        public string ErrorTheme = "This theme file is corrupted or not suitable for this version.";
        public string ThemeMessage = "Do you want to change to this theme ?";
        public string UserAgentMessage = "Please enter an user agent.";
        public string installStatus = "Downloading Update...";
        public String StatusType = "[PERC]% | [CURRENT] KB downloaded out of [TOTAL] KB.";
        public string newProfileInfo = "Please enter a name for the new profile.It should not contain: ";
        public string enterAValidUrl = "Enter a Valid URL";
        public string goTotxt = "Go to \"[TEXT]\"";
        public string SearchOnWeb = "Search \"[TEXT]\"";
        public string defaultproxytext = "Default Proxy";
        public string SearchOnPage = "Search: ";
        public string CaseSensitive = "Case Sensitive";
        public string privatemode = "Incognito";
        public string updateTitle = "Korot - Update";
        public string updateMessage = "Update available.Do you want to update?";
        public string updateError = "Error while checking for the updates.";
        public string checking = "Checking for updates...";
        public string uptodate = "Your Korot is up-to-date.";
        public string updateavailable = "Update available.";
        public string newtabtitle = "New Tab";
        public string customSearchNote = "(Note: Searched text will be put after the url)";
        public string customSearchMessage = "Write Custom Search Url";
        // Here comes dat contextmenu
        public string goBack = "Go Back";
        public string goForward = "Go Forward";
        public string refresh = "Refresh";
        public string refreshNoCache = "Refresh (No Cache)";
        public string stop = "Stop";
        public string selectAll = "Select All";
        public string openLinkInNewTab = "Open Link in New Tab";
        public string copyLink = "Copy Link";
        public string saveImageAs = "Save Image as...";
        public string openImageInNewTab = "Open Image in New Tab";
        public string paste = "Paste";
        public string copy = "Copy";
        public string cut = "Cut";
        public string undo = "Undo";
        public string redo = "Redo";
        public string delete = "Delete";
        public string SearchOrOpenSelectedInNewTab = "Search/Open Selected in New Tab";
        public string developerTools = "Developer Tools";
        public string viewSource = "View Source";
        // here ends contextmenu
        public string korotdownloading = "Korot - Downloading";
        public string fromtwodot = "From : ";
        public string totwodot = "To : ";
        public string openfileafterdownload = "Open this file after download";
        public string closethisafterdownload = "Close this after download";
        public string open = "Open";
        //here ends the context menu
        public string Search = "Search";
        public string run = "Run";
        public string startatstarup = "Run at startup";
        public string empty = "(empty)";
        public string newincwindow = "New Incognito Window";
        public string newwindow = "New  Window";
        public ListBox languagedummy = new ListBox();
        //here starts the special pages
        public string ThemesTitle = "Korot - Themes";
        public string ErrorPageTitle = "Korot - Error";
        public string MonthNames = "\"January\",\"February\",\"March\",\"April\",\"May\",\"June\",\"July\",\"August\",\"September\",\"October\",\"November\",\"December\"";
        public string DayNames = "\"Sunday\",\"Monday\",\"Tuesday\",\"Wednesday\",\"Thursday\",\"Friday\",\"Saturday\"";
        public string SearchHelpText = "Search on web or enter an URL.";
        public string KT = "Korot can't display this page.";
        public string ET = "One possibility is because of one of these errors:";
        public string E1 = "1.  The URL is incorrect.";
        public string E2 = "2.  The website is not responding,too busy or too slow.";
        public string E3 = "3.  Machine disconnected from Internet or connection is too slow.";
        public string E4 = "4.  Antivirus program thinks this browser is a virus or the Website includes a virus.";
        public string RT = "We recommend:";
        public string R1 = "1.  Checking the URL for errors(like grammar errors). ";
        public string R2 = "2.  Connect the machine to Internet. ";
        public string R3 = "3.  Wait a few minutes and try again. ";
        public string R4 = "4.  Disable Antivirus or add this browser to whitelist of Antivirus.";
        //here ends the pages
        public string switchTo = "Switch to:";
        public string deleteProfile = "Delete this profile";
        public string newprofile = "New Profile";
        public string Yes = "Yes";
        public string No = "No";
        public string OK = "OK";
        public string Cancel = "Cancel";
        public string CertErrorPageTitle = "This website is not secure";
        public string CertErrorPageMessage = "This website is using a certificate that has errors. Which means your information (credit cards,passwords,messages...) can be stolen by unknown people in this website.";
        public string CertErrorPageButton = "I understand these risks.";
        #endregion



        void SetLanguage(string privatemodetxt,
                         string updatetitletxt,
                         string updatemessagetxt,
                         string updateerrortxt,
                         string newtabtext,
                         string csnote,
                         string cse,
                         string nw,
                         string niw,
                         string settingstxt,
                         string historytxt,
                         string hptxt,
                         string setxt,
                         string themetxt,
                         string customtxt,
                         string rstxt,
                         string backstyle,
                         string cleartxt,
                         string AboutText,
                         string gBack,
                         string gForward,
                         string reload,
                         string reloadnoc,
                         string stoop,
                         string selectal,
                         string olint,
                         string cl,
                         string sia,
                         string oint,
                         string pastetxt,
                         string copytxt,
                         string cuttxt,
                         string undotxt,
                         string redotxt,
                         string deletetxt,
                         string oossint,
                         string devtool,
                         string viewsrc,
                         string downloadsstring,
                         string bcolor,
                         string ocolor,
                         string korotdown,
                         string otad,
                         string ctad,
                         string _open,
                         string tarih,
                         string kaynak,
                         string hedef,
                         string kaynak2nokta,
                         string hedef2nokta,
                         string titleErrorPage,
                         string abouttxt,
                         string themename,
                         string save,
                         string defaultproxysetting,
                         string themes,
                         string openlinkinnt,
                         string openfile,
                         string openfolder,
                         string SearchOnCurrentPage,
                         string CaseSensitivity,
                         string DayName,
                         string MonthName,
                         string searchhelp,
                         string kt,
                         string et,
                         string e1,
                         string e2,
                         string e3,
                         string e4,
                         string rt,
                         string r1,
                         string r2,
                         string r3,
                         string r4,
                         string searchtxt,
                         string SwitchTo,
                         string newProfile,
                         string delProfile,
                         string goToURL,
                         string searchURL,
                         string enterValidUrl,
                         string newProfInfo,
                         string restoreLastSession,
                         string yes,
                         string no,
                         string ok,
                         string cancel,
                         string updateava,
                         string checkmessage,
                         string upToDate,
                         string checkbutton,
                         string installbutton,
                         string installstatus,
                         string statustype,
                         string themeTitle,
                         string themeMessage,
                         string themeError,
                         string cemt,
                         string cet,
                         string ce,
                         string cokt,
                         string cok,
                         string sce,
                         string uc,
                         string nuc,
                         string cept,
                         string cepm,
                         string cepb)
        {
            CertErrorPageButton = cepb.Replace(Environment.NewLine, "");
            CertErrorPageMessage = cepm.Replace(Environment.NewLine, "");
            CertErrorPageTitle = cept.Replace(Environment.NewLine, "");
            usesCookies = uc.Replace(Environment.NewLine, "");
            notUsesCookies = nuc.Replace(Environment.NewLine, "");
            showCertError = sce.Replace(Environment.NewLine, "");
            CertificateErrorMenuTitle = cemt.Replace(Environment.NewLine, "");
            CertificateErrorTitle = cet.Replace(Environment.NewLine, "");
            CertificateError = ce.Replace(Environment.NewLine, "");
            CertificateOKTitle = cokt.Replace(Environment.NewLine, "");
            CertificateOK = cok.Replace(Environment.NewLine, "");
            ErrorTheme = themeError.Replace(Environment.NewLine, "");
            ThemesTitle = themeTitle.Replace(Environment.NewLine, "");
            ThemeMessage = themeMessage.Replace(Environment.NewLine, "");
            btUpdater.Text = checkbutton.Replace(Environment.NewLine, "");
            btInstall.Text = installbutton.Replace(Environment.NewLine, "");
            checking = checkmessage.Replace(Environment.NewLine, "");
            uptodate = upToDate.Replace(Environment.NewLine, "");
            installStatus = installstatus.Replace(Environment.NewLine, "");
            StatusType = statustype.Replace(Environment.NewLine, "");
            radioButton1.Text = newtabtext.Replace(Environment.NewLine, "");
            if (updateProgress == 0)
            {
                lbUpdateStatus.Text = checkmessage.Replace(Environment.NewLine, "");
            }
            else if (updateProgress == 1)
            {
                lbUpdateStatus.Text = upToDate.Replace(Environment.NewLine, "");
            }
            else if (updateProgress == 2)
            {
                lbUpdateStatus.Text = updateava.Replace(Environment.NewLine, "");
            }
            else if (updateProgress == 3)
            {
                lbUpdateStatus.Text = updateerrortxt.Replace(Environment.NewLine, "");
            }
            updateavailable = updateava;
            privatemode = privatemodetxt.Replace(Environment.NewLine, "");
            updateTitle = updatetitletxt.Replace(Environment.NewLine, "");
            updateMessage = updatemessagetxt.Replace(Environment.NewLine, "");
            updateError = updateerrortxt.Replace(Environment.NewLine, "");
            newtabtitle = newtabtext.Replace(Environment.NewLine, "");
            customSearchNote = csnote.Replace(Environment.NewLine, "");
            customSearchMessage = cse.Replace(Environment.NewLine, "");
            label10.Text = backstyle.Replace(Environment.NewLine, "");
            textBox1.Location = new Point(label10.Location.X + label10.Width, label10.Location.Y);
            textBox1.Width = tbTheme.Width - (label10.Width + label10.Location.X + 5);
            newWindowToolStripMenuItem.Text = nw.Replace(Environment.NewLine, "");
            newIncognitoWindowToolStripMenuItem.Text = niw.Replace(Environment.NewLine, "");
            newincwindow = niw.Replace(Environment.NewLine, "");
            newwindow = nw.Replace(Environment.NewLine, "");
            tbSetting.Text = settingstxt.Replace(Environment.NewLine, "");
            tbDownload.Text = downloadsstring.Replace(Environment.NewLine, "");
            tbAbout.Text = AboutText.Replace(Environment.NewLine, "");
            label11.Text = hptxt.Replace(Environment.NewLine, "");
            label3.Text = setxt.Replace(Environment.NewLine, "");
            tbTheme.Text = themetxt.Replace(Environment.NewLine, "");
            SearchOnPage = SearchOnCurrentPage.Replace(Environment.NewLine, "");
            CaseSensitive = CaseSensitivity.Replace(Environment.NewLine, "");
            customToolStripMenuItem.Text = customtxt.Replace(Environment.NewLine, "");
            removeSelectedToolStripMenuItem.Text = rstxt.Replace(Environment.NewLine, "");
            clearToolStripMenuItem.Text = cleartxt.Replace(Environment.NewLine, "");
            frmS.Text = settingstxt.Replace(Environment.NewLine, "");
            tbHistory.Text = historytxt.Replace(Environment.NewLine, "");
            tbAbout.Text = abouttxt.Replace(Environment.NewLine, "");
            goBack = gBack.Replace(Environment.NewLine, "");
            goForward = gForward.Replace(Environment.NewLine, "");
            refresh = reload.Replace(Environment.NewLine, "");
            refreshNoCache = reloadnoc.Replace(Environment.NewLine, "");
            stop = stoop.Replace(Environment.NewLine, "");
            selectAll = selectal.Replace(Environment.NewLine, "");
            openLinkInNewTab = olint.Replace(Environment.NewLine, "");
            copyLink = cl.Replace(Environment.NewLine, "");
            saveImageAs = sia.Replace(Environment.NewLine, "");
            openImageInNewTab = oint.Replace(Environment.NewLine, "");
            paste = pastetxt.Replace(Environment.NewLine, "");
            copy = copytxt.Replace(Environment.NewLine, "");
            cut = cuttxt.Replace(Environment.NewLine, "");
            undo = undotxt.Replace(Environment.NewLine, "");
            redo = redotxt.Replace(Environment.NewLine, "");
            delete = deletetxt.Replace(Environment.NewLine, "");
            SearchOrOpenSelectedInNewTab = oossint.Replace(Environment.NewLine, "");
            developerTools = devtool.Replace(Environment.NewLine, "");
            viewSource = viewsrc.Replace(Environment.NewLine, "");
            // lightToolStripMenuItem.Text = ltxt.Replace(Environment.NewLine, "");
            //darkToolStripMenuItem.Text = dtxt.Replace(Environment.NewLine, "");
            restorerStrip.Text = restoreLastSession.Replace(Environment.NewLine, "");
            label7.Text = bcolor.Replace(Environment.NewLine, "");
            enterAValidUrl = enterValidUrl.Replace(Environment.NewLine, "");
            label8.Text = ocolor.Replace(Environment.NewLine, "");
            chDate.Text = kaynak.Replace(Environment.NewLine, "");
            fromtwodot = kaynak2nokta.Replace(Environment.NewLine, "");
            chFrom.Text = hedef.Replace(Environment.NewLine, "");
            totwodot = hedef2nokta.Replace(Environment.NewLine, "");
            korotdownloading = korotdown.Replace(Environment.NewLine, "");
            chTo.Text = tarih.Replace(Environment.NewLine, "");
            openfileafterdownload = otad.Replace(Environment.NewLine, "");
            closethisafterdownload = ctad.Replace(Environment.NewLine, "");
            open = _open.Replace(Environment.NewLine, "");
            tsYes.Text = yes.Replace(Environment.NewLine, "");
            tsNo.Text = no.Replace(Environment.NewLine, "");
            openLinkİnNewTabToolStripMenuItem.Text = openlinkinnt.Replace(Environment.NewLine, "");
            openFileToolStripMenuItem.Text = openfile.Replace(Environment.NewLine, "");
            openFileİnExplorerToolStripMenuItem.Text = openfolder.Replace(Environment.NewLine, "");
            removeSelectedToolStripMenuItem1.Text = rstxt.Replace(Environment.NewLine, "");
            clearToolStripMenuItem2.Text = cleartxt.Replace(Environment.NewLine, "");
            defaultproxytext = defaultproxysetting.Replace(Environment.NewLine, "");
            label13.Text = themename.Replace(Environment.NewLine, "");
            Yes = yes.Replace(Environment.NewLine, "");
            No = no.Replace(Environment.NewLine, "");
            OK = ok.Replace(Environment.NewLine, "");
            Cancel = cancel.Replace(Environment.NewLine, "");
            button10.Text = save.Replace(Environment.NewLine, "");
            label15.Text = themes.Replace(Environment.NewLine, "");
            SearchOnWeb = searchURL.Replace(Environment.NewLine, "");
            goTotxt = goToURL.Replace(Environment.NewLine, "");
            newProfileInfo = newProfInfo.Replace(Environment.NewLine, "");
            //pages
            MonthNames = MonthName.Replace(Environment.NewLine, "");
            DayNames = DayName.Replace(Environment.NewLine, "");
            SearchHelpText = searchhelp.Replace(Environment.NewLine, "");
            ErrorPageTitle = titleErrorPage.Replace(Environment.NewLine, "");
            KT = kt.Replace(Environment.NewLine, "");
            ET = et.Replace(Environment.NewLine, "");
            E1 = e1.Replace(Environment.NewLine, "");
            E2 = e2.Replace(Environment.NewLine, "");
            E3 = e3.Replace(Environment.NewLine, "");
            E4 = e4.Replace(Environment.NewLine, "");
            RT = rt.Replace(Environment.NewLine, "");
            R1 = r1.Replace(Environment.NewLine, "");
            R2 = r2.Replace(Environment.NewLine, "");
            R3 = r3.Replace(Environment.NewLine, "");
            R4 = r4.Replace(Environment.NewLine, "");
            Search = searchtxt.Replace(Environment.NewLine, "");
            newprofile = newProfile.Replace(Environment.NewLine, "");
            switchTo = SwitchTo.Replace(Environment.NewLine, "");
            deleteProfile = delProfile.Replace(Environment.NewLine, "");
            //Positions of about page texts.
            llGoogle.Location = new Point(llCEF.Location.X + llCEF.Width - 15, llCEF.Location.Y);
            llCEFS.Location = new Point(llGoogle.Location.X + llGoogle.Width - 5, llCEFS.Location.Y);
            llMS.Location = new Point(llVS.Location.X + llVS.Width - 10, llVS.Location.Y);
            llGIT.Location = new Point(llGNU.Location.X + llGNU.Width - 10, llGIT.Location.Y);
            foreach (frmCEF x in CefFormList)
            {
                x.Invoke(new Action(() => x.RefreshTranslation()));
            }
        }
        private void dummyCMS_Opening(object sender, CancelEventArgs e)
        {
            Process.Start(Application.StartupPath + "//Lang//");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (lbLang.SelectedItems.Count > 0)
            {
                object p = "Do you want to set the language to '" + lbLang.SelectedItem.ToString();
                HaltroyFramework.HaltroyMsgBox CustomMessageBox = new HaltroyFramework.HaltroyMsgBox("Korot", p + "' ?", this.Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, "Yes", "No", "OK", "Cancel", 390, 140);
                DialogResult result = CustomMessageBox.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    LoadLangFromFile(Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang");
                    Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang";
                    Properties.Settings.Default.Save();
                }
            }
        }
        public void LoadLangFromFile(string LangFile)
        {
            try
            {
                languagedummy.Items.Clear();
                StreamReader ReadFile = new StreamReader(LangFile, Encoding.UTF8, false);
                string Playlist = ReadFile.ReadToEnd();
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                int Count = SplittedFase.Length - 1; ; int i = 0;
                while (!(i == Count))
                {
                    languagedummy.Items.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                    i += 1;
                }
                ReadFile.Close();
                ReadLangFileFromTemp();
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.Message);
            }
        }
        public void ReadLangFileFromTemp()
        {
            try
            {
                SetLanguage(
                    languagedummy.Items[0].ToString().Substring(1),
                    languagedummy.Items[1].ToString().Substring(1),
                    languagedummy.Items[2].ToString().Substring(1),
                    languagedummy.Items[3].ToString().Substring(1),
                    languagedummy.Items[4].ToString().Substring(1),
                    languagedummy.Items[5].ToString().Substring(1),
                    languagedummy.Items[6].ToString().Substring(1),
                    languagedummy.Items[7].ToString().Substring(1),
                    languagedummy.Items[8].ToString().Substring(1),
                    languagedummy.Items[9].ToString().Substring(1),
                    languagedummy.Items[10].ToString().Substring(1),
                    languagedummy.Items[11].ToString().Substring(1),
                    languagedummy.Items[12].ToString().Substring(1),
                    languagedummy.Items[13].ToString().Substring(1),
                    languagedummy.Items[14].ToString().Substring(1),
                    languagedummy.Items[15].ToString().Substring(1),
                    languagedummy.Items[16].ToString().Substring(1),
                    languagedummy.Items[17].ToString().Substring(1),
                    languagedummy.Items[18].ToString().Substring(1),
                    languagedummy.Items[19].ToString().Substring(1),
                    languagedummy.Items[20].ToString().Substring(1),
                    languagedummy.Items[21].ToString().Substring(1),
                    languagedummy.Items[22].ToString().Substring(1),
                    languagedummy.Items[23].ToString().Substring(1),
                    languagedummy.Items[24].ToString().Substring(1),
                    languagedummy.Items[25].ToString().Substring(1),
                    languagedummy.Items[26].ToString().Substring(1),
                    languagedummy.Items[27].ToString().Substring(1),
                    languagedummy.Items[28].ToString().Substring(1),
                    languagedummy.Items[29].ToString().Substring(1),
                    languagedummy.Items[30].ToString().Substring(1),
                    languagedummy.Items[31].ToString().Substring(1),
                    languagedummy.Items[32].ToString().Substring(1),
                    languagedummy.Items[33].ToString().Substring(1),
                    languagedummy.Items[34].ToString().Substring(1),
                    languagedummy.Items[35].ToString().Substring(1),
                    languagedummy.Items[36].ToString().Substring(1),
                    languagedummy.Items[37].ToString().Substring(1),
                    languagedummy.Items[38].ToString().Substring(1),
                    languagedummy.Items[39].ToString().Substring(1),
                    languagedummy.Items[40].ToString().Substring(1),
                    languagedummy.Items[41].ToString().Substring(1),
                    languagedummy.Items[42].ToString().Substring(1),
                    languagedummy.Items[43].ToString().Substring(1),
                    languagedummy.Items[44].ToString().Substring(1),
                    languagedummy.Items[45].ToString().Substring(1),
                    languagedummy.Items[46].ToString().Substring(1),
                    languagedummy.Items[47].ToString().Substring(1),
                    languagedummy.Items[48].ToString().Substring(1),
                    languagedummy.Items[49].ToString().Substring(1),
                    languagedummy.Items[50].ToString().Substring(1),
                    languagedummy.Items[51].ToString().Substring(1),
                    languagedummy.Items[52].ToString().Substring(1),
                    languagedummy.Items[53].ToString().Substring(1),
                    languagedummy.Items[54].ToString().Substring(1),
                    languagedummy.Items[55].ToString().Substring(1),
                    languagedummy.Items[56].ToString().Substring(1),
                    languagedummy.Items[57].ToString().Substring(1),
                    languagedummy.Items[58].ToString().Substring(1),
                    languagedummy.Items[59].ToString().Substring(1),
                    languagedummy.Items[60].ToString().Substring(1),
                    languagedummy.Items[61].ToString().Substring(1),
                    languagedummy.Items[62].ToString().Substring(1),
                    languagedummy.Items[63].ToString().Substring(1),
                    languagedummy.Items[64].ToString().Substring(1),
                    languagedummy.Items[65].ToString().Substring(1),
                    languagedummy.Items[66].ToString().Substring(1),
                    languagedummy.Items[67].ToString().Substring(1),
                    languagedummy.Items[68].ToString().Substring(1),
                    languagedummy.Items[69].ToString().Substring(1),
                    languagedummy.Items[70].ToString().Substring(1),
                    languagedummy.Items[71].ToString().Substring(1),
                    languagedummy.Items[72].ToString().Substring(1),
                    languagedummy.Items[73].ToString().Substring(1),
                    languagedummy.Items[74].ToString().Substring(1),
                    languagedummy.Items[75].ToString().Substring(1),
                    languagedummy.Items[76].ToString().Substring(1),
                    languagedummy.Items[77].ToString().Substring(1),
                    languagedummy.Items[78].ToString().Substring(1),
                    languagedummy.Items[79].ToString().Substring(1),
                    languagedummy.Items[80].ToString().Substring(1),
                    languagedummy.Items[81].ToString().Substring(1),
                    languagedummy.Items[82].ToString().Substring(1),
                    languagedummy.Items[83].ToString().Substring(1),
                    languagedummy.Items[84].ToString().Substring(1),
                    languagedummy.Items[85].ToString().Substring(1),
                    languagedummy.Items[86].ToString().Substring(1),
                    languagedummy.Items[87].ToString().Substring(1),
                    languagedummy.Items[88].ToString().Substring(1),
                    languagedummy.Items[89].ToString().Substring(1),
                    languagedummy.Items[90].ToString().Substring(1),
                    languagedummy.Items[91].ToString().Substring(1),
                    languagedummy.Items[92].ToString().Substring(1),
                    languagedummy.Items[93].ToString().Substring(1),
                    languagedummy.Items[94].ToString().Substring(1),
                    languagedummy.Items[95].ToString().Substring(1),
                    languagedummy.Items[96].ToString().Substring(1),
                    languagedummy.Items[97].ToString().Substring(1),
                    languagedummy.Items[98].ToString().Substring(1),
                    languagedummy.Items[99].ToString().Substring(1),
                    languagedummy.Items[100].ToString().Substring(1),
                    languagedummy.Items[101].ToString().Substring(1),
                    languagedummy.Items[102].ToString().Substring(1),
                    languagedummy.Items[103].ToString().Substring(1),
                    languagedummy.Items[104].ToString().Substring(1),
                    languagedummy.Items[105].ToString().Substring(1),
                    languagedummy.Items[106].ToString().Substring(1),
                    languagedummy.Items[107].ToString().Substring(1),
                    languagedummy.Items[108].ToString().Substring(1));
            }
            catch (Exception ex)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle, "This file does not suitable for this version of Korot.Please ask the creator of this language to update." + Environment.NewLine + " Error : " + ex.Message, this.Icon, MessageBoxButtons.OK, Properties.Settings.Default.BackColor, Yes, No, OK, Cancel, 390, 140);
                DialogResult diyalog = mesaj.ShowDialog();
            }

        }
        public void RefreshLangList()
        {
            lbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.lang", SearchOption.TopDirectoryOnly))
            {
                lbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }

        }
        public void FullScreen(TabPage tabpage, bool enable)
        {

        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputb = new HaltroyFramework.HaltroyInputBox(customSearchNote, customSearchMessage, this.Icon, Properties.Settings.Default.SearchURL, Properties.Settings.Default.BackColor, Properties.Settings.Default.OverlayColor, OK, Cancel, 400, 150);
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (ValidHttpURL(inputb.TextValue()) && !inputb.TextValue().StartsWith("korot://") && !inputb.TextValue().StartsWith("file://") && !inputb.TextValue().StartsWith("about"))
                {
                    Properties.Settings.Default.SearchURL = inputb.TextValue();
                    textBox3.Text = Properties.Settings.Default.SearchURL;
                }
                else
                {
                    customToolStripMenuItem_Click(null, null);
                }
            }

        }
        private void SearchEngineSelection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SearchURL = ((ToolStripMenuItem)sender).Tag.ToString();
            textBox3.Text = Properties.Settings.Default.SearchURL;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.ToLower().StartsWith("korot://newtab"))
            {
                radioButton1.Checked = true;
                Properties.Settings.Default.Homepage = textBox2.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                radioButton1.Checked = false;
                Properties.Settings.Default.Homepage = textBox2.Text;
                Properties.Settings.Default.Save();
            }
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            cmsSearchEngine.Show(MousePosition);
        }
        private void openmytaginnewtab(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewTab(((LinkLabel)sender).Tag.ToString());
        }
        private void ClearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Favorites = "";
            Properties.Settings.Default.Save();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog();
            colorpicker.AnyColor = true;
            colorpicker.AllowFullOpen = true;
            colorpicker.FullOpen = true;
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackColor = colorpicker.Color;
                Properties.Settings.Default.BackColor = colorpicker.Color;
                pictureBox3.BackColor = colorpicker.Color;
                PrintImages();
                comboBox1.Text = "";
            }

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog();
            colorpicker.AnyColor = true;
            colorpicker.AllowFullOpen = true;
            colorpicker.FullOpen = true;
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.BackColor = colorpicker.Color;
                Properties.Settings.Default.OverlayColor = colorpicker.Color;
                pictureBox4.BackColor = colorpicker.Color;
                PrintImages();
                comboBox1.Text = "";
            }
        }

        private void Label14_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Process.Start(Application.StartupPath + "//Themes//");
            }

        }

        private void Label7_Click_1(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.White;
            Properties.Settings.Default.BackColor = pictureBox3.BackColor;
            Properties.Settings.Default.Save();
            PrintImages();
        }


        private void Label8_Click(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.DodgerBlue;
            Properties.Settings.Default.OverlayColor = pictureBox4.BackColor;
            Properties.Settings.Default.Save();
            PrintImages();
        }
        public void RefreshDownloadList()
        {
            hlvDownload.Items.Clear();
            string Playlist = Properties.Settings.Default.DowloadHistory;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                ListViewItem listV = new ListViewItem();
                listV.Text = SplittedFase[i].Replace(Environment.NewLine, "");
                i += 1;
                listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                i += 1;
                listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                i += 1;
                hlvDownload.Items.Add(listV);
            }

        }
        private void ListView2_DoubleClick(object sender, EventArgs e)
        {
            cmsDownload.Show(MousePosition);
        }
        private void OpenLinkİnNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab(hlvDownload.SelectedItems[0].SubItems[2].Text);
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(hlvDownload.SelectedItems[0].SubItems[1].Text);
        }

        private void OpenFileİnExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + hlvDownload.SelectedItems[0].SubItems[1].Text);
        }

        private void RemoveSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string x = hlvDownload.SelectedItems[0].Text + ";" + hlvDownload.SelectedItems[0].SubItems[1].Text + ";" + hlvDownload.SelectedItems[0].SubItems[2].Text + ";";
            Properties.Settings.Default.DowloadHistory = Properties.Settings.Default.DowloadHistory.Replace(x, "");
            Properties.Settings.Default.Save();
            RefreshDownloadList();
        }

        private void ClearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DowloadHistory = "";
            Properties.Settings.Default.Save();
            RefreshDownloadList();
        }

       
        private void Button4_Click(object sender, EventArgs e)
        {
            frmS.Show();
            frmS.Focus();
        }

        private void CmsDownload_Opening(object sender, CancelEventArgs e)
        {

        }

        private void HlvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void HlvHistory_DoubleClick(object sender, EventArgs e)
        {
            NewTab(hlvHistory.SelectedItems[0].SubItems[2].Text);
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void NewIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "-incognito");
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorpick = new ColorDialog();
            colorpick.AnyColor = true;
            colorpick.FullOpen = true;
            if (colorpick.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackStyle = "background-color: rgb(" + colorpick.Color.R + "," + colorpick.Color.G + "," + colorpick.Color.B + ")";
                textBox1.Text = Properties.Settings.Default.BackStyle;
            }
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about)|(?:about)|(?:file)|(?:korot)|(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s);
        }
        private void FromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputbox = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                             enterAValidUrl,
                                                                                             this.Icon,
                                                                                             "",
                                                                                             Properties.Settings.Default.BackColor,
                                                                                             Properties.Settings.Default.OverlayColor,
                                                                                             OK, Cancel, 400, 150);
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                if (ValidHttpURL(inputbox.TextValue()))
                {
                    Properties.Settings.Default.BackStyle = "background-image: url(\"" + inputbox.TextValue().Replace("\\", "/") + "\")";
                    textBox1.Text = Properties.Settings.Default.BackStyle;
                }
                else
                {
                    FromURLToolStripMenuItem_Click(null, null);
                }
            }
        }

        private void FromLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog();
            filedlg.Filter = "Image Files|*.jpg;*.png;*.bmp;*.jpeg;*.jfif;*.gif;*.apng;*.ico;*.svg;*.webp|All Files|*.*";
            filedlg.Title = "Select a Background Image";
            filedlg.Multiselect = false;
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackStyle = "background-image: url(\"file://" + filedlg.FileName.Replace("\\", "/") + "\")";
                textBox1.Text = Properties.Settings.Default.BackStyle;
            }
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition);
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Properties.Settings.Default.Homepage = "korot://newtab";
                textBox2.Text = Properties.Settings.Default.Homepage;
            }
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

        private void YesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(restorerStrip.Tag.ToString());
            ReadSession(restorerStrip.Tag.ToString());
            menuStrip1.Items.Remove(restorerStrip);
        }

        private void NoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuStrip1.Items.Remove(restorerStrip);
        }


        private void btInstall_Click(object sender, EventArgs e)
        {
            Form1 frmUpdate = new Form1(this);
            frmUpdate.Show();
        }

        private void btUpdater_Click(object sender, EventArgs e)
        {
            UpdateWebC.DownloadStringAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2123&authkey=!ADjFaqhHH3MjOAQ&ithint=file%2ctxt&e=5QH8I8"));
            updateProgress = 0;
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

        private void tmrRefresher_Tick(object sender, EventArgs e)
        {
            RefreshLangList();
            refreshThemeList();
            RefreshDownloadList();
                RefreshHistory();
        }

        private void label15_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                refreshThemeList();
            }else if(e.Button == MouseButtons.Right)
            {
                Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + "\"");
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
    }


}
