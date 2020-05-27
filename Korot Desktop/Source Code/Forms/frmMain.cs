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
using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Win32Interop.Enums;

namespace Korot
{

    public partial class frmMain : HTAlt.WinForms.HTTitleTabs
    {
        private MyJumplist list;
        public bool isPreRelease = false;
        public int preVer = 0;
        public List<DownloadItem> CurrentDownloads = new List<DownloadItem>();
        public List<string> CancelledDownloads = new List<string>();
        public List<string> notificationAsked = new List<string>();
        public List<frmNotification> notifications { get; set; }
        public Collection<frmCEF> NotifListeners { get => notifListeners; set => notifListeners = value; }

        public bool isIncognito = false;
        public HTTabRenderer tabRenderer;
        public CollectionManager colman;
        public HTTitleTab settingTab = null;
        public HTTitleTab themeTab = null;
        public HTTitleTab historyTab = null;
        public HTTitleTab downloadTab = null;
        public HTTitleTab aboutTab = null;
        public HTTitleTab cookieTab = null;
        public HTTitleTab collectionTab = null;
        public HTTitleTab nallowTab = null;
        public HTTitleTab nblockTab = null;
        public HTTitleTab notificationTab = null;

        #region Notificatin Listener
        private string closeKorotMessage = "Do you want to close Korot?";
#pragma warning disable 414
        private string closeAllMessage = "Do you really want to close them all?";
#pragma warning restore 414
        private string closeAll = "Clsoe all";
        private string closeKorot = "Close Korot";
        private string Yes = "Yes";
        private string No = "No";
        private string Cancel = "Cancel";
        ToolStripMenuItem tsCloseK;
        ToolStripMenuItem tsCloseAll;
        ToolStripSeparator tsSepNL;
        public Collection<frmCEF> notifListeners = new Collection<frmCEF>();
        ContextMenuStrip cmsNL = new ContextMenuStrip() { RenderMode = ToolStripRenderMode.System, ShowImageMargin = false,};
        private NotifyIcon NLEditor = new NotifyIcon() { Text = "Korot", Icon = Properties.Resources.KorotIcon, Visible = true};
        private void InitNL()
        {
            string Playlist = HTAlt.Tools.ReadFile(Properties.Settings.Default.LangFile, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SF = Playlist.Split(token);
            closeAll = SF[346].Substring(1).Replace(Environment.NewLine, "");
            closeAllMessage = SF[348].Substring(1).Replace(Environment.NewLine, "");
            closeKorot = SF[347].Substring(1).Replace(Environment.NewLine, "");
            closeKorotMessage = SF[349].Substring(1).Replace(Environment.NewLine, "");
            Yes = SF[84].Substring(1).Replace(Environment.NewLine, "");
            No = SF[85].Substring(1).Replace(Environment.NewLine, "");
            Cancel = SF[87].Substring(1).Replace(Environment.NewLine, "");
            cmsNL.Items.Clear();
            foreach (frmCEF x in notifListeners)
            {
                ToolStripMenuItem tsItem = new ToolStripMenuItem();
                tsItem.Text = x.Text;
                tsItem.Tag = x;
                tsItem.Name = HTAlt.Tools.GenerateRandomText(12);
                tsItem.Click += closeNL_Click;
                cmsNL.Items.Add(tsItem);
            }
            tsSepNL = new ToolStripSeparator()
            {
                Name = "tsSepNL"
            };
            cmsNL.Items.Add(tsSepNL);
            tsCloseAll = new ToolStripMenuItem()
            {
                Text = closeAll,
                Name = "CloseAllTS"
            };
            tsCloseAll.Click += closeall_Click;
            cmsNL.Items.Add(tsCloseAll);
            tsCloseK = new ToolStripMenuItem()
            {
                Text = closeKorot,
                Name = "CloseKorotTS"
            };
            tsCloseK.Click += closekorot_Click;
            cmsNL.Items.Add(tsCloseK);
            NLEditor.Visible = (notifListeners.Count > 0);
            cmsNL.BackColor = Properties.Settings.Default.BackColor;
            cmsNL.ForeColor = HTAlt.Tools.AutoWhiteBlack(Properties.Settings.Default.BackColor);
            foreach (ToolStripItem x in cmsNL.Items)
            {
                x.BackColor = Properties.Settings.Default.BackColor;
                x.ForeColor = HTAlt.Tools.AutoWhiteBlack(Properties.Settings.Default.BackColor);
            } 
        }
        private void tmrNL_Tick(object sender,EventArgs e)
        {
            InitNL();
        }
        private void closeNL_Click(object sender,EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                var tsSender = sender as ToolStripItem;
                var tsForm = tsSender.Tag as Form;
                cmsNL.Items.Remove(tsSender);
                tsForm.Close();
                NotifListeners.Remove(tsForm as frmCEF);
            }
        }
        private void closeall_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot",
                                                      closeAllMessage,
                                                      new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true }) 
            { Icon = Properties.Resources.KorotIcon, Yes = Yes, No = No, Cancel = Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
            DialogResult diares = mesaj.ShowDialog();
            if (diares == DialogResult.Yes)
            {
                foreach (ToolStripItem x in cmsNL.Items)
                {
                    if (x.Name != "CloseAllTS" && x.Name != "CloseKorotTS" && x.Name != "tsSepNL")
                    {
                        closeNL_Click(x, e);
                    }
                }
            }
        }
        private void closekorot_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTMsgBox mesaj = new HTAlt.WinForms.HTMsgBox("Korot",
                                                      closeKorotMessage,
                                                      new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true, Cancel = true })
            { Icon = Properties.Resources.KorotIcon, Yes = Yes, No = No, Cancel = Cancel, BackgroundColor = Properties.Settings.Default.BackColor };
            DialogResult diares = mesaj.ShowDialog();
            if (diares == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        #endregion
        public frmMain()
        {
            AeroPeekEnabled = true;
            tabRenderer = new HTTabRenderer(this);
            TabRenderer = tabRenderer;
            Icon = Properties.Resources.KorotIcon;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
            bool exists = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
            if (!exists)
            {
                foreach (string x in Properties.Settings.Default.notificationAllow)
                {
                    frmCEF notfiListener = new frmCEF(isIncognito, x, Properties.Settings.Default.LastUser, true) { isPreRelease = isPreRelease, preVer = preVer, colManager = colman, };
                    notfiListener.Visible = true;
                    notfiListener.Enabled = true;
                    notfiListener.Show();
                    NotifListeners.Add(notfiListener);
                    notfiListener.Hide();
                }
                InitNL();
                Timer tmrNL = new Timer() { Interval = 5000, };
                tmrNL.Tick += tmrNL_Tick;
                tmrNL.Start();
            }
            list = new MyJumplist(this.Handle, this);
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
            ForeColor = HTAlt.Tools.Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
        }
        public string OldSessions;
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
                List<string> KorotNameHistory = new List<string>() { "StoneHomepage (not browser) (First code written by Haltroy)", "StoneBrowser (Trident) (First ever program written by Haltroy)", "ZStone (Trident)", "Pell Browser (Trident)", "Kolme Browser (Trident)", "Ninova (Gecko)", "Webtroy (Gecko,CEF)", "Korot (CEF)" };
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
                        profilePath + "cookieDisallow.ksf",
                        profilePath + "notificationAllow.ksf",
                        profilePath + "notificationBlock.ksf");
                }
                else
                {
                    Tools.SaveSettings(profilePath + "settings.ksf",
                        profilePath + "history.ksf",
                        profilePath + "favorites.ksf",
                        profilePath + "download.ksf",
                        profilePath + "cookieDisallow.ksf",
                        profilePath + "notificationAllow.ksf",
                        profilePath + "notificationBlock.ksf");
                }
                if (!File.Exists(profilePath + "collections.kcf"))
                {
                    HTAlt.Tools.WriteFile(profilePath + "collections.kcf", "[root][/root]", Encoding.UTF8);
                }
                colman.readCollections(profilePath + "collections.kcf");
            }
            else
            { Process.Start(Application.ExecutablePath, "-oobe"); Close(); }

            Location = new Point(Korot.Properties.Settings.Default.WindowPosX, Korot.Properties.Settings.Default.WindowPosY);
            Size = new Size(Korot.Properties.Settings.Default.WindowSizeW, Korot.Properties.Settings.Default.WindowSizeH);
            PrintImages();
            if (Properties.Settings.Default.LangFile == null) { Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\English.lang"; }
            if (Properties.Settings.Default.autoRestoreSessions)
            {
                ReadSession(Properties.Settings.Default.LastSessionURIs);
            }
            else
            {
                OldSessions = Properties.Settings.Default.LastSessionURIs;
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
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(Session.Replace("[", "<").Replace("]", ">"));
            writer.Flush();
            stream.Position = 0;
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            foreach (XmlNode node in document.FirstChild.ChildNodes)
            {
                frmCEF cefform = new frmCEF(isIncognito, "korot://newtab", Properties.Settings.Default.LastUser);
                cefform.lbURL.Items.Clear();
                cefform.lbTitle.Items.Clear();
                string[] SplittedFase = node.Attributes["Content"].Value.Split(';');
                int Count = SplittedFase.Length - 1; ; int i = 0;
                while (!(i == Count))
                {
                    cefform.lbURL.Items.Add(SplittedFase[i]);
                    i += 1;
                    cefform.lbTitle.Items.Add(SplittedFase[i]);
                    i += 1;
                }
                cefform.lbURL.SelectedIndex = Convert.ToInt32(node.Attributes["Index"].Value);
                cefform.lbTitle.SelectedIndex = Convert.ToInt32(node.Attributes["Index"].Value);
                HTTitleTab tab = new HTTitleTab(this)
                {
                    Content = cefform
                };
                Tabs.Add(tab);
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
        public void WriteCurrentSession()
        {
            string CurrentSessionURIs = "[root]";
            foreach (HTTitleTab x in Tabs)
            {
                frmCEF cefform = (frmCEF)x.Content;
                string text = "";
                int i = 0; int Count = cefform.lbURL.Items.Count - 1;
                if (cefform.lbURL.Items.Count > 0)
                {
                    while (i != Count)
                    {
                        text += cefform.lbURL.Items[i].ToString() +
                            ";" +
                            cefform.lbTitle.Items[i].ToString() +
                            ";";
                        i += 1;
                    }
                }
                CurrentSessionURIs += "[Session Index=\"" + cefform.lbURL.SelectedIndex + "\" Content=\"" + text + "\" /]";
            }
            CurrentSessionURIs += "[/root]";
            WriteSessions(CurrentSessionURIs);
        }
        public bool closing = false;
        public void CreateTab(HTTitleTab referenceTab, string url = "korot://newtab")
        {
            if (!Directory.Exists(profilePath) && profilePath != null) { Directory.CreateDirectory(profilePath); }
            HTTitleTab newTab = new HTTitleTab(this)
            {
                BackColor = referenceTab.BackColor,
                UseDefaultBackColor = referenceTab.UseDefaultBackColor,
                Content = new frmCEF(isIncognito, url, Properties.Settings.Default.LastUser) { isPreRelease = isPreRelease, preVer = preVer, colManager = colman, }
            };
            Tabs.Insert(Tabs.IndexOf(referenceTab) + 1, newTab);
            SelectedTabIndex = Tabs.IndexOf(referenceTab) + 1;
            //Tabs.Add(newTab);
        }
        public void CreateTab(string url = "korot://newtab")
        {
            if (!Directory.Exists(profilePath) && profilePath != null) { Directory.CreateDirectory(profilePath); }
            HTTitleTab newTab = new HTTitleTab(this)
            {
                BackColor = Properties.Settings.Default.BackColor,
                UseDefaultBackColor = true,
                Content = new frmCEF(isIncognito, url, Properties.Settings.Default.LastUser) { isPreRelease = isPreRelease, preVer = preVer, colManager = colman, }
            };
            Tabs.Add(newTab);
            SelectedTabIndex = Tabs.Count - 1;
        }
        public override HTTitleTab CreateTab()
        {
            if (!Directory.Exists(profilePath)) { Directory.CreateDirectory(profilePath); }
            return new HTTitleTab(this)
            {
                BackColor = Properties.Settings.Default.BackColor,
                UseDefaultBackColor = true,
                Content = new frmCEF(isIncognito, "korot://newtab", Properties.Settings.Default.LastUser) { isPreRelease = isPreRelease, preVer = preVer, colManager = colman, }
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
                WindowState = FormWindowState.Normal;
                Taskbar.Hide();
                MaximizedBounds = Screen.FromHandle(Handle).Bounds;
                WindowState = FormWindowState.Maximized;
                isFullScreen = true;
            }
            else
            {
                Taskbar.Show();
                MaximizedBounds = Screen.GetWorkingArea(this);
                if (!wasMaximized)
                {
                    WindowState = FormWindowState.Normal;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    WindowState = FormWindowState.Maximized;
                }
                isFullScreen = false;

            }
            FormBorderStyle = fullscreen ? FormBorderStyle.None : FormBorderStyle.Sizable;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Taskbar.Show();
            closing = true;
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
                colman.writeCollections(profilePath + "collections.kcf");
                if (e.CloseReason != CloseReason.None || e.CloseReason != CloseReason.WindowsShutDown || e.CloseReason != CloseReason.TaskManagerClosing)
                {
                    Korot.Properties.Settings.Default.LastSessionURIs = "";
                }
                else
                {
                    Korot.Properties.Settings.Default.LastSessionURIs = "[root]";
                    foreach (HTTitleTab x in Tabs)
                    {
                        frmCEF cefform = (frmCEF)x.Content;
                        string text = "";
                        int i = 0; int Count = cefform.lbURL.Items.Count - 1;
                        while (i != Count)
                        {
                            text += cefform.lbURL.Items[i].ToString() +
                                ";" +
                                cefform.lbTitle.Items[i].ToString() +
                                ";";
                            i += 1;
                        }
                        Korot.Properties.Settings.Default.LastSessionURIs += "[Session Index=\"" + cefform.lbURL.SelectedIndex + "\" Content=\"" + text + "\" /]";
                    }
                    Korot.Properties.Settings.Default.LastSessionURIs += "[/root]";

                }
                if (!isIncognito) { Properties.Settings.Default.Save(); }
                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\")) { } else { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\"); }
                Tools.SaveSettings(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\settings.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\history.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\favorites.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\download.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\cookieDisallow.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "notificationAllow.ksf",
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "notificationBlock.ksf");
            }
        }

        private void SessionLogger_Tick(object sender, EventArgs e)
        {
            WriteCurrentSession();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            foreach (HTTitleTab x in Tabs)
            {
                ((frmCEF)x.Content).Invoke(new Action(() => ((frmCEF)x.Content).FrmCEF_SizeChanged(null, null)));
            }

        }
    }
}
