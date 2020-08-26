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
namespace Korot
{
    partial class frmCEF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCEF));
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.pNavigate = new System.Windows.Forms.Panel();
            this.pbPrivacy = new System.Windows.Forms.PictureBox();
            this.pbProgress = new System.Windows.Forms.PictureBox();
            this.mFavorites = new System.Windows.Forms.MenuStrip();
            this.pbIncognito = new System.Windows.Forms.PictureBox();
            this.btHome = new HTAlt.WinForms.HTButton();
            this.btFav = new HTAlt.WinForms.HTButton();
            this.btNext = new HTAlt.WinForms.HTButton();
            this.cmsForward = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btBack = new HTAlt.WinForms.HTButton();
            this.cmsBack = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btHamburger = new HTAlt.WinForms.HTButton();
            this.btProfile = new HTAlt.WinForms.HTButton();
            this.btRefresh = new HTAlt.WinForms.HTButton();
            this.cmsFavorite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsopenInNewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.openİnNewWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openİnNewIncognitoWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSepFav = new System.Windows.Forms.ToolStripSeparator();
            this.newFavoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.removeSelectedTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.cms4 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lErrorTitle2Text = new System.Windows.Forms.Label();
            this.lErrorTitle1Text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lErrorTitle2 = new System.Windows.Forms.Label();
            this.lErrorTitle1 = new System.Windows.Forms.Label();
            this.lErrorTitle = new System.Windows.Forms.Label();
            this.tmrFaster = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.pnlCert = new System.Windows.Forms.Panel();
            this.btCertError = new HTAlt.WinForms.HTButton();
            this.lbCertErrorInfo = new System.Windows.Forms.Label();
            this.lbCertErrorTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCef = new System.Windows.Forms.TabPage();
            this.pCEF = new System.Windows.Forms.Panel();
            this.lbTitle = new System.Windows.Forms.ListBox();
            this.lbURL = new System.Windows.Forms.ListBox();
            this.tpCert = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.btLangStore = new HTAlt.WinForms.HTButton();
            this.btlangFolder = new HTAlt.WinForms.HTButton();
            this.cbLang = new System.Windows.Forms.ComboBox();
            this.btNewTab = new HTAlt.WinForms.HTButton();
            this.lbFlashInfo = new System.Windows.Forms.Label();
            this.lbFlash = new System.Windows.Forms.Label();
            this.hsFlash = new HTAlt.WinForms.HTSwitch();
            this.btCookie = new HTAlt.WinForms.HTButton();
            this.btNotification = new HTAlt.WinForms.HTButton();
            this.btReset = new HTAlt.WinForms.HTButton();
            this.btCleanLog = new HTAlt.WinForms.HTButton();
            this.lbDNT = new System.Windows.Forms.Label();
            this.hsDoNotTrack = new HTAlt.WinForms.HTSwitch();
            this.lbautoRestore = new System.Windows.Forms.Label();
            this.lbLastProxy = new System.Windows.Forms.Label();
            this.hsAutoRestore = new HTAlt.WinForms.HTSwitch();
            this.hsProxy = new HTAlt.WinForms.HTSwitch();
            this.btClose = new HTAlt.WinForms.HTButton();
            this.lbAtStartup = new System.Windows.Forms.Label();
            this.lbShowFavorites = new System.Windows.Forms.Label();
            this.lbSettings = new System.Windows.Forms.Label();
            this.hsFav = new HTAlt.WinForms.HTSwitch();
            this.rbNewTab = new System.Windows.Forms.RadioButton();
            this.lbHomepage = new System.Windows.Forms.Label();
            this.lbSearchEngine = new System.Windows.Forms.Label();
            this.tbHomepage = new System.Windows.Forms.TextBox();
            this.tbStartup = new System.Windows.Forms.TextBox();
            this.tbSearchEngine = new System.Windows.Forms.TextBox();
            this.tpTheme = new System.Windows.Forms.TabPage();
            this.btThemeWizard = new HTAlt.WinForms.HTButton();
            this.flpClose = new System.Windows.Forms.FlowLayoutPanel();
            this.rbBackColor1 = new System.Windows.Forms.RadioButton();
            this.rbForeColor1 = new System.Windows.Forms.RadioButton();
            this.rbOverlayColor1 = new System.Windows.Forms.RadioButton();
            this.flpNewTab = new System.Windows.Forms.FlowLayoutPanel();
            this.rbBackColor = new System.Windows.Forms.RadioButton();
            this.rbForeColor = new System.Windows.Forms.RadioButton();
            this.rbOverlayColor = new System.Windows.Forms.RadioButton();
            this.flpLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.rbNone = new System.Windows.Forms.RadioButton();
            this.rbTile = new System.Windows.Forms.RadioButton();
            this.rbCenter = new System.Windows.Forms.RadioButton();
            this.rbStretch = new System.Windows.Forms.RadioButton();
            this.rbZoom = new System.Windows.Forms.RadioButton();
            this.btClose2 = new HTAlt.WinForms.HTButton();
            this.lbBackImageStyle = new System.Windows.Forms.Label();
            this.lbCloseColor = new System.Windows.Forms.Label();
            this.lbNewTabColor = new System.Windows.Forms.Label();
            this.lbBackImage = new System.Windows.Forms.Label();
            this.pbStore = new System.Windows.Forms.PictureBox();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.pbOverlay = new System.Windows.Forms.PictureBox();
            this.lbBackColor = new System.Windows.Forms.Label();
            this.lbOveralColor = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.lbThemeName = new System.Windows.Forms.Label();
            this.lbThemes = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button12 = new HTAlt.WinForms.HTButton();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.lbTheme = new System.Windows.Forms.Label();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.pHisMan = new System.Windows.Forms.Panel();
            this.btClose6 = new HTAlt.WinForms.HTButton();
            this.lbHistory = new System.Windows.Forms.Label();
            this.tpDownload = new System.Windows.Forms.TabPage();
            this.pDowMan = new System.Windows.Forms.Panel();
            this.btClose7 = new HTAlt.WinForms.HTButton();
            this.lbDownloads = new System.Windows.Forms.Label();
            this.hsOpen = new HTAlt.WinForms.HTSwitch();
            this.hsDownload = new HTAlt.WinForms.HTSwitch();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.btDownloadFolder = new HTAlt.WinForms.HTButton();
            this.lbDownloadFolder = new System.Windows.Forms.Label();
            this.lbOpen = new System.Windows.Forms.Label();
            this.lbAutoDownload = new System.Windows.Forms.Label();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.btClose10 = new HTAlt.WinForms.HTButton();
            this.lbAbout = new System.Windows.Forms.Label();
            this.lbUpdateStatus = new System.Windows.Forms.Label();
            this.btInstall = new HTAlt.WinForms.HTButton();
            this.btUpdater = new HTAlt.WinForms.HTButton();
            this.llLicenses = new System.Windows.Forms.LinkLabel();
            this.label21 = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbKorot = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.tpSite = new System.Windows.Forms.TabPage();
            this.btBlocked = new HTAlt.WinForms.HTButton();
            this.pSite = new System.Windows.Forms.Panel();
            this.btCookieBack = new HTAlt.WinForms.HTButton();
            this.btClose8 = new HTAlt.WinForms.HTButton();
            this.lbSiteSettings = new System.Windows.Forms.Label();
            this.tpCollection = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btClose9 = new HTAlt.WinForms.HTButton();
            this.lbCollections = new System.Windows.Forms.Label();
            this.tpNotification = new System.Windows.Forms.TabPage();
            this.btNotifBack = new HTAlt.WinForms.HTButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flpFrom = new System.Windows.Forms.FlowLayoutPanel();
            this.fromHour = new System.Windows.Forms.NumericUpDown();
            this.label40 = new System.Windows.Forms.Label();
            this.fromMin = new System.Windows.Forms.NumericUpDown();
            this.flpEvery = new System.Windows.Forms.FlowLayoutPanel();
            this.lbSunday = new System.Windows.Forms.Label();
            this.lbMonday = new System.Windows.Forms.Label();
            this.lbTuesday = new System.Windows.Forms.Label();
            this.lbWednesday = new System.Windows.Forms.Label();
            this.lbThursday = new System.Windows.Forms.Label();
            this.lbFriday = new System.Windows.Forms.Label();
            this.lbSaturday = new System.Windows.Forms.Label();
            this.lb24HType = new System.Windows.Forms.Label();
            this.scheduleFrom = new System.Windows.Forms.Label();
            this.flpTo = new System.Windows.Forms.FlowLayoutPanel();
            this.toHour = new System.Windows.Forms.NumericUpDown();
            this.label41 = new System.Windows.Forms.Label();
            this.toMin = new System.Windows.Forms.NumericUpDown();
            this.scheduleEvery = new System.Windows.Forms.Label();
            this.scheduleTo = new System.Windows.Forms.Label();
            this.lbSchedule = new System.Windows.Forms.Label();
            this.lbSilentMode = new System.Windows.Forms.Label();
            this.hsSchedule = new HTAlt.WinForms.HTSwitch();
            this.hsSilent = new HTAlt.WinForms.HTSwitch();
            this.lbPlayNotifSound = new System.Windows.Forms.Label();
            this.hsNotificationSound = new HTAlt.WinForms.HTSwitch();
            this.btClose3 = new HTAlt.WinForms.HTButton();
            this.lbNotifSetting = new System.Windows.Forms.Label();
            this.tpNewTab = new System.Windows.Forms.TabPage();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tlpNewTab = new System.Windows.Forms.TableLayoutPanel();
            this.L9 = new System.Windows.Forms.Panel();
            this.L9T = new System.Windows.Forms.Label();
            this.L9U = new System.Windows.Forms.Label();
            this.L8 = new System.Windows.Forms.Panel();
            this.L8T = new System.Windows.Forms.Label();
            this.L8U = new System.Windows.Forms.Label();
            this.L7 = new System.Windows.Forms.Panel();
            this.L7T = new System.Windows.Forms.Label();
            this.L7U = new System.Windows.Forms.Label();
            this.L6 = new System.Windows.Forms.Panel();
            this.L6T = new System.Windows.Forms.Label();
            this.L6U = new System.Windows.Forms.Label();
            this.L5 = new System.Windows.Forms.Panel();
            this.L5T = new System.Windows.Forms.Label();
            this.L5U = new System.Windows.Forms.Label();
            this.L4 = new System.Windows.Forms.Panel();
            this.L4T = new System.Windows.Forms.Label();
            this.L4U = new System.Windows.Forms.Label();
            this.L3 = new System.Windows.Forms.Panel();
            this.L3T = new System.Windows.Forms.Label();
            this.L3U = new System.Windows.Forms.Label();
            this.L2 = new System.Windows.Forms.Panel();
            this.L2T = new System.Windows.Forms.Label();
            this.L2U = new System.Windows.Forms.Label();
            this.L1 = new System.Windows.Forms.Panel();
            this.L1T = new System.Windows.Forms.Label();
            this.L1U = new System.Windows.Forms.Label();
            this.L0 = new System.Windows.Forms.Panel();
            this.L0T = new System.Windows.Forms.Label();
            this.L0U = new System.Windows.Forms.Label();
            this.btClear = new HTAlt.WinForms.HTButton();
            this.lbNTUrl = new System.Windows.Forms.Label();
            this.lbNTTitle = new System.Windows.Forms.Label();
            this.btNewTabBack = new HTAlt.WinForms.HTButton();
            this.btClose5 = new HTAlt.WinForms.HTButton();
            this.lbNewTabTitle = new System.Windows.Forms.Label();
            this.tpBlock = new System.Windows.Forms.TabPage();
            this.pBlock = new System.Windows.Forms.Panel();
            this.btBlockBack = new HTAlt.WinForms.HTButton();
            this.btClose4 = new HTAlt.WinForms.HTButton();
            this.lbBlockedSites = new System.Windows.Forms.Label();
            this.cmsSearchEngine = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.googleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yandexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yaaniToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duckDuckGoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baiduToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wolframalphaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aOLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yahooToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.askToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ınternetArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsBStyle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ımageFromLocalFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ımageFromURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsStartup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showNewTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pNavigate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrivacy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncognito)).BeginInit();
            this.cmsFavorite.SuspendLayout();
            this.pnlCert.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCef.SuspendLayout();
            this.pCEF.SuspendLayout();
            this.tpCert.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.tpTheme.SuspendLayout();
            this.flpClose.SuspendLayout();
            this.flpNewTab.SuspendLayout();
            this.flpLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOverlay)).BeginInit();
            this.tpHistory.SuspendLayout();
            this.tpDownload.SuspendLayout();
            this.tpAbout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.tpSite.SuspendLayout();
            this.tpCollection.SuspendLayout();
            this.tpNotification.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flpFrom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fromHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromMin)).BeginInit();
            this.flpEvery.SuspendLayout();
            this.flpTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toMin)).BeginInit();
            this.tpNewTab.SuspendLayout();
            this.tlpNewTab.SuspendLayout();
            this.L9.SuspendLayout();
            this.L8.SuspendLayout();
            this.L7.SuspendLayout();
            this.L6.SuspendLayout();
            this.L5.SuspendLayout();
            this.L4.SuspendLayout();
            this.L3.SuspendLayout();
            this.L2.SuspendLayout();
            this.L1.SuspendLayout();
            this.L0.SuspendLayout();
            this.tpBlock.SuspendLayout();
            this.cmsSearchEngine.SuspendLayout();
            this.cmsBStyle.SuspendLayout();
            this.cmsStartup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAddress
            // 
            this.tbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbAddress.Location = new System.Drawing.Point(140, 4);
            this.tbAddress.MaxLength = 2147483647;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(475, 23);
            this.tbAddress.TabIndex = 5;
            // 
            // pNavigate
            // 
            this.pNavigate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNavigate.Controls.Add(this.pbPrivacy);
            this.pNavigate.Controls.Add(this.pbProgress);
            this.pNavigate.Controls.Add(this.mFavorites);
            this.pNavigate.Controls.Add(this.tbAddress);
            this.pNavigate.Controls.Add(this.pbIncognito);
            this.pNavigate.Controls.Add(this.btHome);
            this.pNavigate.Controls.Add(this.btFav);
            this.pNavigate.Controls.Add(this.btNext);
            this.pNavigate.Controls.Add(this.btBack);
            this.pNavigate.Controls.Add(this.btHamburger);
            this.pNavigate.Controls.Add(this.btProfile);
            this.pNavigate.Controls.Add(this.btRefresh);
            this.pNavigate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pNavigate.Location = new System.Drawing.Point(0, 0);
            this.pNavigate.Name = "pNavigate";
            this.pNavigate.Size = new System.Drawing.Size(732, 58);
            this.pNavigate.TabIndex = 6;
            // 
            // pbPrivacy
            // 
            this.pbPrivacy.BackColor = System.Drawing.Color.Transparent;
            this.pbPrivacy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPrivacy.Image = global::Korot.Properties.Resources.lockg;
            this.pbPrivacy.Location = new System.Drawing.Point(118, 4);
            this.pbPrivacy.Name = "pbPrivacy";
            this.pbPrivacy.Size = new System.Drawing.Size(23, 23);
            this.pbPrivacy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPrivacy.TabIndex = 7;
            this.pbPrivacy.TabStop = false;
            this.pbPrivacy.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.BackColor = System.Drawing.Color.DodgerBlue;
            this.pbProgress.Location = new System.Drawing.Point(0, 0);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(0, 2);
            this.pbProgress.TabIndex = 6;
            this.pbProgress.TabStop = false;
            // 
            // mFavorites
            // 
            this.mFavorites.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mFavorites.Location = new System.Drawing.Point(0, 32);
            this.mFavorites.Name = "mFavorites";
            this.mFavorites.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.mFavorites.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mFavorites.Size = new System.Drawing.Size(730, 24);
            this.mFavorites.TabIndex = 10;
            this.mFavorites.Text = "menuStrip1";
            this.mFavorites.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mFavorites_MouseClick);
            // 
            // pbIncognito
            // 
            this.pbIncognito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbIncognito.BackColor = System.Drawing.Color.Transparent;
            this.pbIncognito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbIncognito.Image = global::Korot.Properties.Resources.inctab;
            this.pbIncognito.Location = new System.Drawing.Point(614, 4);
            this.pbIncognito.Name = "pbIncognito";
            this.pbIncognito.Size = new System.Drawing.Size(23, 23);
            this.pbIncognito.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbIncognito.TabIndex = 5;
            this.pbIncognito.TabStop = false;
            this.pbIncognito.Text = "test";
            this.pbIncognito.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btHome
            // 
            this.btHome.BackColor = System.Drawing.Color.Transparent;
            this.btHome.ButtonImage = global::Korot.Properties.Resources.home;
            this.btHome.DrawImage = true;
            this.btHome.FlatAppearance.BorderSize = 0;
            this.btHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btHome.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btHome.Location = new System.Drawing.Point(58, 0);
            this.btHome.Name = "btHome";
            this.btHome.Size = new System.Drawing.Size(30, 28);
            this.btHome.TabIndex = 2;
            this.btHome.UseVisualStyleBackColor = false;
            this.btHome.Click += new System.EventHandler(this.button5_Click);
            this.btHome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // btFav
            // 
            this.btFav.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btFav.BackColor = System.Drawing.Color.Transparent;
            this.btFav.ButtonImage = global::Korot.Properties.Resources.star;
            this.btFav.DrawImage = true;
            this.btFav.FlatAppearance.BorderSize = 0;
            this.btFav.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFav.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btFav.Location = new System.Drawing.Point(642, 0);
            this.btFav.Name = "btFav";
            this.btFav.Size = new System.Drawing.Size(30, 28);
            this.btFav.TabIndex = 7;
            this.btFav.UseVisualStyleBackColor = false;
            this.btFav.Click += new System.EventHandler(this.Button7_Click);
            this.btFav.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // btNext
            // 
            this.btNext.BackColor = System.Drawing.Color.Transparent;
            this.btNext.ButtonImage = global::Korot.Properties.Resources.rightarrow;
            this.btNext.ContextMenuStrip = this.cmsForward;
            this.btNext.DrawImage = true;
            this.btNext.FlatAppearance.BorderSize = 0;
            this.btNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNext.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btNext.Location = new System.Drawing.Point(87, 0);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(30, 28);
            this.btNext.TabIndex = 3;
            this.btNext.UseVisualStyleBackColor = false;
            this.btNext.Click += new System.EventHandler(this.button3_Click);
            this.btNext.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // cmsForward
            // 
            this.cmsForward.Name = "cmsBack";
            this.cmsForward.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsForward.ShowImageMargin = false;
            this.cmsForward.Size = new System.Drawing.Size(36, 4);
            this.cmsForward.Opening += new System.ComponentModel.CancelEventHandler(this.cmsForward_Opening);
            // 
            // btBack
            // 
            this.btBack.BackColor = System.Drawing.Color.Transparent;
            this.btBack.ButtonImage = global::Korot.Properties.Resources.leftarrow;
            this.btBack.ContextMenuStrip = this.cmsBack;
            this.btBack.DrawImage = true;
            this.btBack.FlatAppearance.BorderSize = 0;
            this.btBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBack.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btBack.Location = new System.Drawing.Point(0, 0);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(30, 28);
            this.btBack.TabIndex = 0;
            this.btBack.UseVisualStyleBackColor = false;
            this.btBack.Click += new System.EventHandler(this.button1_Click);
            this.btBack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // cmsBack
            // 
            this.cmsBack.Name = "cmsBack";
            this.cmsBack.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsBack.ShowImageMargin = false;
            this.cmsBack.Size = new System.Drawing.Size(36, 4);
            this.cmsBack.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBack_Opening);
            // 
            // btHamburger
            // 
            this.btHamburger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btHamburger.BackColor = System.Drawing.Color.Transparent;
            this.btHamburger.ButtonImage = global::Korot.Properties.Resources.hamburger;
            this.btHamburger.DrawImage = true;
            this.btHamburger.FlatAppearance.BorderSize = 0;
            this.btHamburger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btHamburger.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btHamburger.Location = new System.Drawing.Point(700, 0);
            this.btHamburger.Name = "btHamburger";
            this.btHamburger.Size = new System.Drawing.Size(30, 28);
            this.btHamburger.TabIndex = 9;
            this.btHamburger.UseVisualStyleBackColor = false;
            this.btHamburger.Click += new System.EventHandler(this.button11_Click);
            this.btHamburger.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // btProfile
            // 
            this.btProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btProfile.BackColor = System.Drawing.Color.Transparent;
            this.btProfile.ButtonImage = global::Korot.Properties.Resources.profiles;
            this.btProfile.DrawImage = true;
            this.btProfile.FlatAppearance.BorderSize = 0;
            this.btProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btProfile.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btProfile.Location = new System.Drawing.Point(671, 0);
            this.btProfile.Name = "btProfile";
            this.btProfile.Size = new System.Drawing.Size(30, 28);
            this.btProfile.TabIndex = 8;
            this.btProfile.UseVisualStyleBackColor = false;
            this.btProfile.Click += new System.EventHandler(this.Button9_Click);
            this.btProfile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // btRefresh
            // 
            this.btRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btRefresh.ButtonImage = global::Korot.Properties.Resources.refresh;
            this.btRefresh.DrawImage = true;
            this.btRefresh.FlatAppearance.BorderSize = 0;
            this.btRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btRefresh.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btRefresh.Location = new System.Drawing.Point(29, 0);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Size = new System.Drawing.Size(30, 28);
            this.btRefresh.TabIndex = 1;
            this.btRefresh.UseVisualStyleBackColor = false;
            this.btRefresh.Click += new System.EventHandler(this.button2_Click);
            this.btRefresh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // cmsFavorite
            // 
            this.cmsFavorite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsopenInNewTab,
            this.openİnNewWindowToolStripMenuItem,
            this.openİnNewIncognitoWindowToolStripMenuItem,
            this.tsSepFav,
            this.newFavoriteToolStripMenuItem,
            this.newFolderToolStripMenuItem,
            this.toolStripSeparator9,
            this.removeSelectedTSMI,
            this.clearTSMI});
            this.cmsFavorite.Name = "cmsFavorite";
            this.cmsFavorite.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsFavorite.ShowImageMargin = false;
            this.cmsFavorite.Size = new System.Drawing.Size(220, 170);
            this.cmsFavorite.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.cmsFavorite_Closing);
            this.cmsFavorite.Opening += new System.ComponentModel.CancelEventHandler(this.cmsFavorite_Opening);
            this.cmsFavorite.Opened += new System.EventHandler(this.cmsFavorite_Opened);
            // 
            // tsopenInNewTab
            // 
            this.tsopenInNewTab.Name = "tsopenInNewTab";
            this.tsopenInNewTab.Size = new System.Drawing.Size(219, 22);
            this.tsopenInNewTab.Text = "Open in New Tab";
            this.tsopenInNewTab.Click += new System.EventHandler(this.openInNewTab_Click);
            // 
            // openİnNewWindowToolStripMenuItem
            // 
            this.openİnNewWindowToolStripMenuItem.Name = "openİnNewWindowToolStripMenuItem";
            this.openİnNewWindowToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openİnNewWindowToolStripMenuItem.Text = "Open in New Window";
            this.openİnNewWindowToolStripMenuItem.Click += new System.EventHandler(this.openİnNewWindowToolStripMenuItem_Click);
            // 
            // openİnNewIncognitoWindowToolStripMenuItem
            // 
            this.openİnNewIncognitoWindowToolStripMenuItem.Name = "openİnNewIncognitoWindowToolStripMenuItem";
            this.openİnNewIncognitoWindowToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openİnNewIncognitoWindowToolStripMenuItem.Text = "Open in New Incognito Window";
            this.openİnNewIncognitoWindowToolStripMenuItem.Click += new System.EventHandler(this.openİnNewIncognitoWindowToolStripMenuItem_Click);
            // 
            // tsSepFav
            // 
            this.tsSepFav.Name = "tsSepFav";
            this.tsSepFav.Size = new System.Drawing.Size(216, 6);
            // 
            // newFavoriteToolStripMenuItem
            // 
            this.newFavoriteToolStripMenuItem.Name = "newFavoriteToolStripMenuItem";
            this.newFavoriteToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.newFavoriteToolStripMenuItem.Text = "New Favorite...";
            this.newFavoriteToolStripMenuItem.Click += new System.EventHandler(this.newFavoriteToolStripMenuItem_Click);
            // 
            // newFolderToolStripMenuItem
            // 
            this.newFolderToolStripMenuItem.Name = "newFolderToolStripMenuItem";
            this.newFolderToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.newFolderToolStripMenuItem.Text = "New Folder...";
            this.newFolderToolStripMenuItem.Click += new System.EventHandler(this.newFolderToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(216, 6);
            // 
            // removeSelectedTSMI
            // 
            this.removeSelectedTSMI.Name = "removeSelectedTSMI";
            this.removeSelectedTSMI.Size = new System.Drawing.Size(219, 22);
            this.removeSelectedTSMI.Text = "Remove Selected";
            this.removeSelectedTSMI.Click += new System.EventHandler(this.removeSelectedTSMI_Click);
            // 
            // clearTSMI
            // 
            this.clearTSMI.Name = "clearTSMI";
            this.clearTSMI.Size = new System.Drawing.Size(219, 22);
            this.clearTSMI.Text = "Clear";
            this.clearTSMI.Click += new System.EventHandler(this.clearTSMI_Click);
            // 
            // cms4
            // 
            this.cms4.Name = "cms4";
            this.cms4.Size = new System.Drawing.Size(61, 4);
            this.cms4.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip4_Opening);
            // 
            // lErrorTitle2Text
            // 
            this.lErrorTitle2Text.AutoSize = true;
            this.lErrorTitle2Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lErrorTitle2Text.Location = new System.Drawing.Point(8, 165);
            this.lErrorTitle2Text.Name = "lErrorTitle2Text";
            this.lErrorTitle2Text.Size = new System.Drawing.Size(393, 68);
            this.lErrorTitle2Text.TabIndex = 0;
            // 
            // lErrorTitle1Text
            // 
            this.lErrorTitle1Text.AutoSize = true;
            this.lErrorTitle1Text.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lErrorTitle1Text.Location = new System.Drawing.Point(8, 63);
            this.lErrorTitle1Text.Name = "lErrorTitle1Text";
            this.lErrorTitle1Text.Size = new System.Drawing.Size(510, 68);
            this.lErrorTitle1Text.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(7, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 0;
            // 
            // lErrorTitle2
            // 
            this.lErrorTitle2.AutoSize = true;
            this.lErrorTitle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lErrorTitle2.Location = new System.Drawing.Point(6, 140);
            this.lErrorTitle2.Name = "lErrorTitle2";
            this.lErrorTitle2.Size = new System.Drawing.Size(157, 25);
            this.lErrorTitle2.TabIndex = 0;
            // 
            // lErrorTitle1
            // 
            this.lErrorTitle1.AutoSize = true;
            this.lErrorTitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lErrorTitle1.Location = new System.Drawing.Point(6, 38);
            this.lErrorTitle1.Name = "lErrorTitle1";
            this.lErrorTitle1.Size = new System.Drawing.Size(432, 25);
            this.lErrorTitle1.TabIndex = 0;
            // 
            // lErrorTitle
            // 
            this.lErrorTitle.AutoSize = true;
            this.lErrorTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lErrorTitle.Location = new System.Drawing.Point(6, 7);
            this.lErrorTitle.Name = "lErrorTitle";
            this.lErrorTitle.Size = new System.Drawing.Size(319, 31);
            this.lErrorTitle.TabIndex = 0;
            // 
            // tmrFaster
            // 
            this.tmrFaster.Enabled = true;
            this.tmrFaster.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 13);
            this.label5.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 13);
            this.label10.TabIndex = 0;
            // 
            // lbStatus
            // 
            this.lbStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbStatus.Location = new System.Drawing.Point(3, 587);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(732, 18);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.TextChanged += new System.EventHandler(this.label2_TextChanged);
            // 
            // pnlCert
            // 
            this.pnlCert.BackColor = System.Drawing.Color.Maroon;
            this.pnlCert.Controls.Add(this.btCertError);
            this.pnlCert.Controls.Add(this.lbCertErrorInfo);
            this.pnlCert.Controls.Add(this.lbCertErrorTitle);
            this.pnlCert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCert.ForeColor = System.Drawing.Color.White;
            this.pnlCert.Location = new System.Drawing.Point(3, 3);
            this.pnlCert.Name = "pnlCert";
            this.pnlCert.Size = new System.Drawing.Size(732, 602);
            this.pnlCert.TabIndex = 8;
            this.pnlCert.Visible = false;
            // 
            // btCertError
            // 
            this.btCertError.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btCertError.DrawImage = true;
            this.btCertError.FlatAppearance.BorderSize = 0;
            this.btCertError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCertError.Location = new System.Drawing.Point(0, 576);
            this.btCertError.Name = "btCertError";
            this.btCertError.Size = new System.Drawing.Size(732, 26);
            this.btCertError.TabIndex = 0;
            this.btCertError.Text = "most of the time";
            this.btCertError.UseVisualStyleBackColor = true;
            this.btCertError.Click += new System.EventHandler(this.button10_Click);
            // 
            // lbCertErrorInfo
            // 
            this.lbCertErrorInfo.AutoSize = true;
            this.lbCertErrorInfo.Location = new System.Drawing.Point(16, 53);
            this.lbCertErrorInfo.Name = "lbCertErrorInfo";
            this.lbCertErrorInfo.Size = new System.Drawing.Size(44, 15);
            this.lbCertErrorInfo.TabIndex = 1;
            this.lbCertErrorInfo.Text = "is hard";
            // 
            // lbCertErrorTitle
            // 
            this.lbCertErrorTitle.AutoSize = true;
            this.lbCertErrorTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbCertErrorTitle.Location = new System.Drawing.Point(14, 17);
            this.lbCertErrorTitle.Name = "lbCertErrorTitle";
            this.lbCertErrorTitle.Size = new System.Drawing.Size(124, 25);
            this.lbCertErrorTitle.TabIndex = 1;
            this.lbCertErrorTitle.Text = "development";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpCef);
            this.tabControl1.Controls.Add(this.tpCert);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Controls.Add(this.tpTheme);
            this.tabControl1.Controls.Add(this.tpHistory);
            this.tabControl1.Controls.Add(this.tpDownload);
            this.tabControl1.Controls.Add(this.tpAbout);
            this.tabControl1.Controls.Add(this.tpSite);
            this.tabControl1.Controls.Add(this.tpCollection);
            this.tabControl1.Controls.Add(this.tpNotification);
            this.tabControl1.Controls.Add(this.tpNewTab);
            this.tabControl1.Controls.Add(this.tpBlock);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Location = new System.Drawing.Point(-7, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(746, 636);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpCef
            // 
            this.tpCef.Controls.Add(this.lbStatus);
            this.tpCef.Controls.Add(this.pCEF);
            this.tpCef.Location = new System.Drawing.Point(4, 24);
            this.tpCef.Name = "tpCef";
            this.tpCef.Padding = new System.Windows.Forms.Padding(3);
            this.tpCef.Size = new System.Drawing.Size(738, 608);
            this.tpCef.TabIndex = 0;
            this.tpCef.Text = "Korot";
            this.tpCef.UseVisualStyleBackColor = true;
            // 
            // pCEF
            // 
            this.pCEF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pCEF.Controls.Add(this.lbTitle);
            this.pCEF.Controls.Add(this.lbURL);
            this.pCEF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCEF.Location = new System.Drawing.Point(3, 3);
            this.pCEF.Name = "pCEF";
            this.pCEF.Size = new System.Drawing.Size(732, 602);
            this.pCEF.TabIndex = 0;
            this.pCEF.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Panel1_PreviewKeyDown);
            // 
            // lbTitle
            // 
            this.lbTitle.BackColor = System.Drawing.Color.Black;
            this.lbTitle.ForeColor = System.Drawing.Color.White;
            this.lbTitle.FormattingEnabled = true;
            this.lbTitle.ItemHeight = 15;
            this.lbTitle.Location = new System.Drawing.Point(118, 248);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(46, 19);
            this.lbTitle.TabIndex = 1;
            this.lbTitle.Visible = false;
            // 
            // lbURL
            // 
            this.lbURL.BackColor = System.Drawing.Color.Black;
            this.lbURL.ForeColor = System.Drawing.Color.White;
            this.lbURL.FormattingEnabled = true;
            this.lbURL.ItemHeight = 15;
            this.lbURL.Location = new System.Drawing.Point(118, 47);
            this.lbURL.Name = "lbURL";
            this.lbURL.Size = new System.Drawing.Size(46, 19);
            this.lbURL.TabIndex = 0;
            this.lbURL.Visible = false;
            this.lbURL.SelectedIndexChanged += new System.EventHandler(this.lbURL_SelectedIndexChanged);
            // 
            // tpCert
            // 
            this.tpCert.Controls.Add(this.pnlCert);
            this.tpCert.Location = new System.Drawing.Point(4, 24);
            this.tpCert.Name = "tpCert";
            this.tpCert.Padding = new System.Windows.Forms.Padding(3);
            this.tpCert.Size = new System.Drawing.Size(738, 608);
            this.tpCert.TabIndex = 1;
            this.tpCert.Text = "Certificate Error";
            this.tpCert.UseVisualStyleBackColor = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.label2);
            this.tpSettings.Controls.Add(this.btLangStore);
            this.tpSettings.Controls.Add(this.btlangFolder);
            this.tpSettings.Controls.Add(this.cbLang);
            this.tpSettings.Controls.Add(this.btNewTab);
            this.tpSettings.Controls.Add(this.lbFlashInfo);
            this.tpSettings.Controls.Add(this.lbFlash);
            this.tpSettings.Controls.Add(this.hsFlash);
            this.tpSettings.Controls.Add(this.btCookie);
            this.tpSettings.Controls.Add(this.btNotification);
            this.tpSettings.Controls.Add(this.btReset);
            this.tpSettings.Controls.Add(this.btCleanLog);
            this.tpSettings.Controls.Add(this.lbDNT);
            this.tpSettings.Controls.Add(this.hsDoNotTrack);
            this.tpSettings.Controls.Add(this.lbautoRestore);
            this.tpSettings.Controls.Add(this.lbLastProxy);
            this.tpSettings.Controls.Add(this.hsAutoRestore);
            this.tpSettings.Controls.Add(this.hsProxy);
            this.tpSettings.Controls.Add(this.btClose);
            this.tpSettings.Controls.Add(this.lbAtStartup);
            this.tpSettings.Controls.Add(this.lbShowFavorites);
            this.tpSettings.Controls.Add(this.lbSettings);
            this.tpSettings.Controls.Add(this.hsFav);
            this.tpSettings.Controls.Add(this.rbNewTab);
            this.tpSettings.Controls.Add(this.lbHomepage);
            this.tpSettings.Controls.Add(this.lbSearchEngine);
            this.tpSettings.Controls.Add(this.tbHomepage);
            this.tpSettings.Controls.Add(this.tbStartup);
            this.tpSettings.Controls.Add(this.tbSearchEngine);
            this.tpSettings.Location = new System.Drawing.Point(4, 24);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Size = new System.Drawing.Size(738, 608);
            this.tpSettings.TabIndex = 2;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(10, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 15);
            this.label2.TabIndex = 44;
            this.label2.Text = "Language:";
            // 
            // btLangStore
            // 
            this.btLangStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btLangStore.ButtonImage = global::Korot.Properties.Resources.store;
            this.btLangStore.DrawImage = true;
            this.btLangStore.FlatAppearance.BorderSize = 0;
            this.btLangStore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLangStore.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btLangStore.Location = new System.Drawing.Point(672, 163);
            this.btLangStore.Name = "btLangStore";
            this.btLangStore.Size = new System.Drawing.Size(20, 20);
            this.btLangStore.TabIndex = 42;
            this.btLangStore.UseVisualStyleBackColor = true;
            this.btLangStore.Click += new System.EventHandler(this.btLangStore_Click);
            // 
            // btlangFolder
            // 
            this.btlangFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btlangFolder.ButtonImage = global::Korot.Properties.Resources.extfolder;
            this.btlangFolder.DrawImage = true;
            this.btlangFolder.FlatAppearance.BorderSize = 0;
            this.btlangFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btlangFolder.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btlangFolder.Location = new System.Drawing.Point(700, 163);
            this.btlangFolder.Name = "btlangFolder";
            this.btlangFolder.Size = new System.Drawing.Size(20, 20);
            this.btlangFolder.TabIndex = 43;
            this.btlangFolder.UseVisualStyleBackColor = true;
            this.btlangFolder.Click += new System.EventHandler(this.btlangFolder_Click);
            // 
            // cbLang
            // 
            this.cbLang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLang.FormattingEnabled = true;
            this.cbLang.Location = new System.Drawing.Point(83, 160);
            this.cbLang.Name = "cbLang";
            this.cbLang.Size = new System.Drawing.Size(583, 23);
            this.cbLang.TabIndex = 41;
            this.cbLang.SelectedIndexChanged += new System.EventHandler(this.cbLang_SelectedIndexChanged);
            // 
            // btNewTab
            // 
            this.btNewTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btNewTab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btNewTab.DrawImage = true;
            this.btNewTab.FlatAppearance.BorderSize = 0;
            this.btNewTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNewTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btNewTab.Location = new System.Drawing.Point(0, 468);
            this.btNewTab.Name = "btNewTab";
            this.btNewTab.Size = new System.Drawing.Size(738, 28);
            this.btNewTab.TabIndex = 39;
            this.btNewTab.Text = "New Tab Content...";
            this.btNewTab.UseVisualStyleBackColor = false;
            this.btNewTab.Click += new System.EventHandler(this.btNewTab_Click);
            // 
            // lbFlashInfo
            // 
            this.lbFlashInfo.AutoSize = true;
            this.lbFlashInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbFlashInfo.Location = new System.Drawing.Point(10, 358);
            this.lbFlashInfo.Name = "lbFlashInfo";
            this.lbFlashInfo.Size = new System.Drawing.Size(310, 15);
            this.lbFlashInfo.TabIndex = 36;
            this.lbFlashInfo.Text = "Changes to Flash setting requires a restart to take effect.";
            // 
            // lbFlash
            // 
            this.lbFlash.AutoSize = true;
            this.lbFlash.BackColor = System.Drawing.Color.Transparent;
            this.lbFlash.Location = new System.Drawing.Point(10, 336);
            this.lbFlash.Name = "lbFlash";
            this.lbFlash.Size = new System.Drawing.Size(85, 15);
            this.lbFlash.TabIndex = 36;
            this.lbFlash.Text = "Enable Flash :";
            // 
            // hsFlash
            // 
            this.hsFlash.Location = new System.Drawing.Point(98, 336);
            this.hsFlash.Name = "hsFlash";
            this.hsFlash.Size = new System.Drawing.Size(50, 19);
            this.hsFlash.TabIndex = 35;
            this.hsFlash.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsFlash_CheckedChanged);
            // 
            // btCookie
            // 
            this.btCookie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btCookie.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btCookie.DrawImage = true;
            this.btCookie.FlatAppearance.BorderSize = 0;
            this.btCookie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCookie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btCookie.Location = new System.Drawing.Point(0, 496);
            this.btCookie.Name = "btCookie";
            this.btCookie.Size = new System.Drawing.Size(738, 28);
            this.btCookie.TabIndex = 11;
            this.btCookie.Text = "Site Settings..";
            this.btCookie.UseVisualStyleBackColor = false;
            this.btCookie.Click += new System.EventHandler(this.button15_Click);
            // 
            // btNotification
            // 
            this.btNotification.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btNotification.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btNotification.DrawImage = true;
            this.btNotification.FlatAppearance.BorderSize = 0;
            this.btNotification.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNotification.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btNotification.Location = new System.Drawing.Point(0, 524);
            this.btNotification.Name = "btNotification";
            this.btNotification.Size = new System.Drawing.Size(738, 28);
            this.btNotification.TabIndex = 12;
            this.btNotification.Text = "Notification Settings..";
            this.btNotification.UseVisualStyleBackColor = false;
            this.btNotification.Click += new System.EventHandler(this.button19_Click_1);
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btReset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btReset.DrawImage = true;
            this.btReset.FlatAppearance.BorderSize = 0;
            this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btReset.Location = new System.Drawing.Point(0, 552);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(738, 28);
            this.btReset.TabIndex = 14;
            this.btReset.Text = "Reset Korot...";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.button18_Click);
            // 
            // btCleanLog
            // 
            this.btCleanLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btCleanLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btCleanLog.DrawImage = true;
            this.btCleanLog.FlatAppearance.BorderSize = 0;
            this.btCleanLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCleanLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btCleanLog.Location = new System.Drawing.Point(0, 580);
            this.btCleanLog.Name = "btCleanLog";
            this.btCleanLog.Size = new System.Drawing.Size(738, 28);
            this.btCleanLog.TabIndex = 13;
            this.btCleanLog.Text = "Clean Log Data";
            this.btCleanLog.UseVisualStyleBackColor = false;
            this.btCleanLog.Click += new System.EventHandler(this.btCleanLog_Click);
            // 
            // lbDNT
            // 
            this.lbDNT.AutoSize = true;
            this.lbDNT.BackColor = System.Drawing.Color.Transparent;
            this.lbDNT.Location = new System.Drawing.Point(10, 302);
            this.lbDNT.Name = "lbDNT";
            this.lbDNT.Size = new System.Drawing.Size(120, 15);
            this.lbDNT.TabIndex = 29;
            this.lbDNT.Text = "Enable DoNotTrack :";
            // 
            // hsDoNotTrack
            // 
            this.hsDoNotTrack.Checked = true;
            this.hsDoNotTrack.Location = new System.Drawing.Point(139, 302);
            this.hsDoNotTrack.Name = "hsDoNotTrack";
            this.hsDoNotTrack.Size = new System.Drawing.Size(50, 19);
            this.hsDoNotTrack.TabIndex = 7;
            this.hsDoNotTrack.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsDoNotTrack_CheckedChanged);
            // 
            // lbautoRestore
            // 
            this.lbautoRestore.AutoSize = true;
            this.lbautoRestore.BackColor = System.Drawing.Color.Transparent;
            this.lbautoRestore.Location = new System.Drawing.Point(10, 266);
            this.lbautoRestore.Name = "lbautoRestore";
            this.lbautoRestore.Size = new System.Drawing.Size(163, 15);
            this.lbautoRestore.TabIndex = 29;
            this.lbautoRestore.Text = "Restore Old Session at Start:";
            // 
            // lbLastProxy
            // 
            this.lbLastProxy.AutoSize = true;
            this.lbLastProxy.BackColor = System.Drawing.Color.Transparent;
            this.lbLastProxy.Location = new System.Drawing.Point(10, 232);
            this.lbLastProxy.Name = "lbLastProxy";
            this.lbLastProxy.Size = new System.Drawing.Size(147, 15);
            this.lbLastProxy.TabIndex = 29;
            this.lbLastProxy.Text = "Remember the last proxy:";
            // 
            // hsAutoRestore
            // 
            this.hsAutoRestore.Location = new System.Drawing.Point(187, 265);
            this.hsAutoRestore.Name = "hsAutoRestore";
            this.hsAutoRestore.Size = new System.Drawing.Size(50, 19);
            this.hsAutoRestore.TabIndex = 9;
            this.hsAutoRestore.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsAutoRestore_CheckedChanged);
            // 
            // hsProxy
            // 
            this.hsProxy.Location = new System.Drawing.Point(170, 231);
            this.hsProxy.Name = "hsProxy";
            this.hsProxy.Size = new System.Drawing.Size(50, 19);
            this.hsProxy.TabIndex = 8;
            this.hsProxy.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsProxy_CheckedChanged);
            // 
            // btClose
            // 
            this.btClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose.BackColor = System.Drawing.Color.Transparent;
            this.btClose.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose.DrawImage = true;
            this.btClose.FlatAppearance.BorderSize = 0;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose.Location = new System.Drawing.Point(697, 5);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 0;
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.button1_Click);
            this.btClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // lbAtStartup
            // 
            this.lbAtStartup.AutoSize = true;
            this.lbAtStartup.BackColor = System.Drawing.Color.Transparent;
            this.lbAtStartup.Location = new System.Drawing.Point(10, 128);
            this.lbAtStartup.Name = "lbAtStartup";
            this.lbAtStartup.Size = new System.Drawing.Size(65, 15);
            this.lbAtStartup.TabIndex = 29;
            this.lbAtStartup.Text = "At Startup: ";
            // 
            // lbShowFavorites
            // 
            this.lbShowFavorites.AutoSize = true;
            this.lbShowFavorites.BackColor = System.Drawing.Color.Transparent;
            this.lbShowFavorites.Location = new System.Drawing.Point(10, 202);
            this.lbShowFavorites.Name = "lbShowFavorites";
            this.lbShowFavorites.Size = new System.Drawing.Size(128, 15);
            this.lbShowFavorites.TabIndex = 29;
            this.lbShowFavorites.Text = "Show Favorites Menu:";
            // 
            // lbSettings
            // 
            this.lbSettings.AutoSize = true;
            this.lbSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbSettings.Location = new System.Drawing.Point(8, 6);
            this.lbSettings.Name = "lbSettings";
            this.lbSettings.Size = new System.Drawing.Size(83, 25);
            this.lbSettings.TabIndex = 34;
            this.lbSettings.Text = "Settings";
            // 
            // hsFav
            // 
            this.hsFav.Checked = true;
            this.hsFav.Location = new System.Drawing.Point(141, 201);
            this.hsFav.Name = "hsFav";
            this.hsFav.Size = new System.Drawing.Size(50, 19);
            this.hsFav.TabIndex = 5;
            this.hsFav.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsFav_CheckedChanged);
            // 
            // rbNewTab
            // 
            this.rbNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbNewTab.AutoSize = true;
            this.rbNewTab.BackColor = System.Drawing.Color.Transparent;
            this.rbNewTab.Location = new System.Drawing.Point(650, 48);
            this.rbNewTab.Name = "rbNewTab";
            this.rbNewTab.Size = new System.Drawing.Size(74, 19);
            this.rbNewTab.TabIndex = 2;
            this.rbNewTab.TabStop = true;
            this.rbNewTab.Tag = "";
            this.rbNewTab.Text = "New Tab";
            this.rbNewTab.UseVisualStyleBackColor = false;
            this.rbNewTab.CheckedChanged += new System.EventHandler(this.RadioButton1_CheckedChanged);
            // 
            // lbHomepage
            // 
            this.lbHomepage.AutoSize = true;
            this.lbHomepage.BackColor = System.Drawing.Color.Transparent;
            this.lbHomepage.Location = new System.Drawing.Point(10, 50);
            this.lbHomepage.Name = "lbHomepage";
            this.lbHomepage.Size = new System.Drawing.Size(79, 15);
            this.lbHomepage.TabIndex = 17;
            this.lbHomepage.Tag = "";
            this.lbHomepage.Text = "Home Page :";
            // 
            // lbSearchEngine
            // 
            this.lbSearchEngine.AutoSize = true;
            this.lbSearchEngine.BackColor = System.Drawing.Color.Transparent;
            this.lbSearchEngine.Location = new System.Drawing.Point(10, 89);
            this.lbSearchEngine.Name = "lbSearchEngine";
            this.lbSearchEngine.Size = new System.Drawing.Size(94, 15);
            this.lbSearchEngine.TabIndex = 16;
            this.lbSearchEngine.Tag = "";
            this.lbSearchEngine.Text = "Search Engine :";
            // 
            // tbHomepage
            // 
            this.tbHomepage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHomepage.Location = new System.Drawing.Point(90, 47);
            this.tbHomepage.Name = "tbHomepage";
            this.tbHomepage.Size = new System.Drawing.Size(554, 21);
            this.tbHomepage.TabIndex = 1;
            this.tbHomepage.Tag = "";
            this.tbHomepage.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // tbStartup
            // 
            this.tbStartup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStartup.Location = new System.Drawing.Point(88, 125);
            this.tbStartup.Name = "tbStartup";
            this.tbStartup.ReadOnly = true;
            this.tbStartup.Size = new System.Drawing.Size(636, 21);
            this.tbStartup.TabIndex = 6;
            this.tbStartup.Tag = "";
            this.tbStartup.Click += new System.EventHandler(this.tbStartup_Click);
            // 
            // tbSearchEngine
            // 
            this.tbSearchEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchEngine.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbSearchEngine.Location = new System.Drawing.Point(109, 86);
            this.tbSearchEngine.Name = "tbSearchEngine";
            this.tbSearchEngine.ReadOnly = true;
            this.tbSearchEngine.Size = new System.Drawing.Size(615, 21);
            this.tbSearchEngine.TabIndex = 3;
            this.tbSearchEngine.Tag = "";
            this.tbSearchEngine.Click += new System.EventHandler(this.textBox3_Click);
            // 
            // tpTheme
            // 
            this.tpTheme.Controls.Add(this.btThemeWizard);
            this.tpTheme.Controls.Add(this.flpClose);
            this.tpTheme.Controls.Add(this.flpNewTab);
            this.tpTheme.Controls.Add(this.flpLayout);
            this.tpTheme.Controls.Add(this.btClose2);
            this.tpTheme.Controls.Add(this.lbBackImageStyle);
            this.tpTheme.Controls.Add(this.lbCloseColor);
            this.tpTheme.Controls.Add(this.lbNewTabColor);
            this.tpTheme.Controls.Add(this.lbBackImage);
            this.tpTheme.Controls.Add(this.pbStore);
            this.tpTheme.Controls.Add(this.pbBack);
            this.tpTheme.Controls.Add(this.pbOverlay);
            this.tpTheme.Controls.Add(this.lbBackColor);
            this.tpTheme.Controls.Add(this.lbOveralColor);
            this.tpTheme.Controls.Add(this.textBox4);
            this.tpTheme.Controls.Add(this.lbThemeName);
            this.tpTheme.Controls.Add(this.lbThemes);
            this.tpTheme.Controls.Add(this.comboBox1);
            this.tpTheme.Controls.Add(this.button12);
            this.tpTheme.Controls.Add(this.listBox2);
            this.tpTheme.Controls.Add(this.lbTheme);
            this.tpTheme.Location = new System.Drawing.Point(4, 24);
            this.tpTheme.Name = "tpTheme";
            this.tpTheme.Size = new System.Drawing.Size(738, 608);
            this.tpTheme.TabIndex = 6;
            this.tpTheme.Text = "Theme";
            this.tpTheme.UseVisualStyleBackColor = true;
            // 
            // btThemeWizard
            // 
            this.btThemeWizard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btThemeWizard.FlatAppearance.BorderSize = 0;
            this.btThemeWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btThemeWizard.Location = new System.Drawing.Point(0, 569);
            this.btThemeWizard.Name = "btThemeWizard";
            this.btThemeWizard.Size = new System.Drawing.Size(738, 39);
            this.btThemeWizard.TabIndex = 37;
            this.btThemeWizard.Text = "Run Theme Wizard";
            this.btThemeWizard.UseVisualStyleBackColor = true;
            this.btThemeWizard.Click += new System.EventHandler(this.btThemeWizard_Click);
            // 
            // flpClose
            // 
            this.flpClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpClose.AutoScroll = true;
            this.flpClose.Controls.Add(this.rbBackColor1);
            this.flpClose.Controls.Add(this.rbForeColor1);
            this.flpClose.Controls.Add(this.rbOverlayColor1);
            this.flpClose.Location = new System.Drawing.Point(152, 193);
            this.flpClose.Name = "flpClose";
            this.flpClose.Size = new System.Drawing.Size(571, 27);
            this.flpClose.TabIndex = 36;
            // 
            // rbBackColor1
            // 
            this.rbBackColor1.AutoSize = true;
            this.rbBackColor1.Location = new System.Drawing.Point(3, 3);
            this.rbBackColor1.Name = "rbBackColor1";
            this.rbBackColor1.Size = new System.Drawing.Size(84, 19);
            this.rbBackColor1.TabIndex = 6;
            this.rbBackColor1.TabStop = true;
            this.rbBackColor1.Text = "Back Color";
            this.rbBackColor1.UseVisualStyleBackColor = true;
            this.rbBackColor1.CheckedChanged += new System.EventHandler(this.rbBackColor1_CheckedChanged);
            // 
            // rbForeColor1
            // 
            this.rbForeColor1.AutoSize = true;
            this.rbForeColor1.Location = new System.Drawing.Point(93, 3);
            this.rbForeColor1.Name = "rbForeColor1";
            this.rbForeColor1.Size = new System.Drawing.Size(82, 19);
            this.rbForeColor1.TabIndex = 6;
            this.rbForeColor1.TabStop = true;
            this.rbForeColor1.Text = "Fore Color";
            this.rbForeColor1.UseVisualStyleBackColor = true;
            this.rbForeColor1.CheckedChanged += new System.EventHandler(this.rbForeColor1_CheckedChanged);
            // 
            // rbOverlayColor1
            // 
            this.rbOverlayColor1.AutoSize = true;
            this.rbOverlayColor1.Location = new System.Drawing.Point(181, 3);
            this.rbOverlayColor1.Name = "rbOverlayColor1";
            this.rbOverlayColor1.Size = new System.Drawing.Size(97, 19);
            this.rbOverlayColor1.TabIndex = 6;
            this.rbOverlayColor1.TabStop = true;
            this.rbOverlayColor1.Text = "Overlay Color";
            this.rbOverlayColor1.UseVisualStyleBackColor = true;
            this.rbOverlayColor1.CheckedChanged += new System.EventHandler(this.rbOverlayColor1_CheckedChanged);
            // 
            // flpNewTab
            // 
            this.flpNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpNewTab.AutoScroll = true;
            this.flpNewTab.Controls.Add(this.rbBackColor);
            this.flpNewTab.Controls.Add(this.rbForeColor);
            this.flpNewTab.Controls.Add(this.rbOverlayColor);
            this.flpNewTab.Location = new System.Drawing.Point(167, 162);
            this.flpNewTab.Name = "flpNewTab";
            this.flpNewTab.Size = new System.Drawing.Size(557, 27);
            this.flpNewTab.TabIndex = 36;
            // 
            // rbBackColor
            // 
            this.rbBackColor.AutoSize = true;
            this.rbBackColor.Location = new System.Drawing.Point(3, 3);
            this.rbBackColor.Name = "rbBackColor";
            this.rbBackColor.Size = new System.Drawing.Size(84, 19);
            this.rbBackColor.TabIndex = 5;
            this.rbBackColor.TabStop = true;
            this.rbBackColor.Text = "Back Color";
            this.rbBackColor.UseVisualStyleBackColor = true;
            this.rbBackColor.CheckedChanged += new System.EventHandler(this.rbBackColor_CheckedChanged);
            // 
            // rbForeColor
            // 
            this.rbForeColor.AutoSize = true;
            this.rbForeColor.Location = new System.Drawing.Point(93, 3);
            this.rbForeColor.Name = "rbForeColor";
            this.rbForeColor.Size = new System.Drawing.Size(82, 19);
            this.rbForeColor.TabIndex = 5;
            this.rbForeColor.TabStop = true;
            this.rbForeColor.Text = "Fore Color";
            this.rbForeColor.UseVisualStyleBackColor = true;
            this.rbForeColor.CheckedChanged += new System.EventHandler(this.rbForeColor_CheckedChanged);
            // 
            // rbOverlayColor
            // 
            this.rbOverlayColor.AutoSize = true;
            this.rbOverlayColor.Location = new System.Drawing.Point(181, 3);
            this.rbOverlayColor.Name = "rbOverlayColor";
            this.rbOverlayColor.Size = new System.Drawing.Size(97, 19);
            this.rbOverlayColor.TabIndex = 5;
            this.rbOverlayColor.TabStop = true;
            this.rbOverlayColor.Text = "Overlay Color";
            this.rbOverlayColor.UseVisualStyleBackColor = true;
            this.rbOverlayColor.CheckedChanged += new System.EventHandler(this.rbOverlayColor_CheckedChanged);
            // 
            // flpLayout
            // 
            this.flpLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpLayout.AutoScroll = true;
            this.flpLayout.Controls.Add(this.rbNone);
            this.flpLayout.Controls.Add(this.rbTile);
            this.flpLayout.Controls.Add(this.rbCenter);
            this.flpLayout.Controls.Add(this.rbStretch);
            this.flpLayout.Controls.Add(this.rbZoom);
            this.flpLayout.Location = new System.Drawing.Point(182, 96);
            this.flpLayout.Name = "flpLayout";
            this.flpLayout.Size = new System.Drawing.Size(541, 27);
            this.flpLayout.TabIndex = 36;
            // 
            // rbNone
            // 
            this.rbNone.AutoSize = true;
            this.rbNone.Location = new System.Drawing.Point(3, 3);
            this.rbNone.Name = "rbNone";
            this.rbNone.Size = new System.Drawing.Size(55, 19);
            this.rbNone.TabIndex = 3;
            this.rbNone.TabStop = true;
            this.rbNone.Text = "None";
            this.rbNone.UseVisualStyleBackColor = true;
            this.rbNone.CheckedChanged += new System.EventHandler(this.rbNone_CheckedChanged);
            // 
            // rbTile
            // 
            this.rbTile.AutoSize = true;
            this.rbTile.Location = new System.Drawing.Point(64, 3);
            this.rbTile.Name = "rbTile";
            this.rbTile.Size = new System.Drawing.Size(45, 19);
            this.rbTile.TabIndex = 3;
            this.rbTile.TabStop = true;
            this.rbTile.Text = "Tile";
            this.rbTile.UseVisualStyleBackColor = true;
            this.rbTile.CheckedChanged += new System.EventHandler(this.rbTile_CheckedChanged);
            // 
            // rbCenter
            // 
            this.rbCenter.AutoSize = true;
            this.rbCenter.Location = new System.Drawing.Point(115, 3);
            this.rbCenter.Name = "rbCenter";
            this.rbCenter.Size = new System.Drawing.Size(61, 19);
            this.rbCenter.TabIndex = 3;
            this.rbCenter.TabStop = true;
            this.rbCenter.Text = "Center";
            this.rbCenter.UseVisualStyleBackColor = true;
            this.rbCenter.CheckedChanged += new System.EventHandler(this.rbCenter_CheckedChanged);
            // 
            // rbStretch
            // 
            this.rbStretch.AutoSize = true;
            this.rbStretch.Location = new System.Drawing.Point(182, 3);
            this.rbStretch.Name = "rbStretch";
            this.rbStretch.Size = new System.Drawing.Size(63, 19);
            this.rbStretch.TabIndex = 3;
            this.rbStretch.TabStop = true;
            this.rbStretch.Text = "Stretch";
            this.rbStretch.UseVisualStyleBackColor = true;
            this.rbStretch.CheckedChanged += new System.EventHandler(this.rbStretch_CheckedChanged);
            // 
            // rbZoom
            // 
            this.rbZoom.AutoSize = true;
            this.rbZoom.Location = new System.Drawing.Point(251, 3);
            this.rbZoom.Name = "rbZoom";
            this.rbZoom.Size = new System.Drawing.Size(57, 19);
            this.rbZoom.TabIndex = 3;
            this.rbZoom.TabStop = true;
            this.rbZoom.Text = "Zoom";
            this.rbZoom.UseVisualStyleBackColor = true;
            this.rbZoom.CheckedChanged += new System.EventHandler(this.rbZoom_CheckedChanged);
            // 
            // btClose2
            // 
            this.btClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose2.BackColor = System.Drawing.Color.Transparent;
            this.btClose2.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose2.DrawImage = true;
            this.btClose2.FlatAppearance.BorderSize = 0;
            this.btClose2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose2.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose2.Location = new System.Drawing.Point(693, 5);
            this.btClose2.Name = "btClose2";
            this.btClose2.Size = new System.Drawing.Size(30, 30);
            this.btClose2.TabIndex = 0;
            this.btClose2.UseVisualStyleBackColor = false;
            this.btClose2.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbBackImageStyle
            // 
            this.lbBackImageStyle.AutoSize = true;
            this.lbBackImageStyle.BackColor = System.Drawing.Color.Transparent;
            this.lbBackImageStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbBackImageStyle.Location = new System.Drawing.Point(18, 100);
            this.lbBackImageStyle.Name = "lbBackImageStyle";
            this.lbBackImageStyle.Size = new System.Drawing.Size(158, 16);
            this.lbBackImageStyle.TabIndex = 25;
            this.lbBackImageStyle.Tag = "";
            this.lbBackImageStyle.Text = "Background Image Style:";
            // 
            // lbCloseColor
            // 
            this.lbCloseColor.AutoSize = true;
            this.lbCloseColor.BackColor = System.Drawing.Color.Transparent;
            this.lbCloseColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbCloseColor.Location = new System.Drawing.Point(18, 197);
            this.lbCloseColor.Name = "lbCloseColor";
            this.lbCloseColor.Size = new System.Drawing.Size(121, 16);
            this.lbCloseColor.TabIndex = 25;
            this.lbCloseColor.Tag = "";
            this.lbCloseColor.Text = "Close Button Color:";
            // 
            // lbNewTabColor
            // 
            this.lbNewTabColor.AutoSize = true;
            this.lbNewTabColor.BackColor = System.Drawing.Color.Transparent;
            this.lbNewTabColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbNewTabColor.Location = new System.Drawing.Point(17, 166);
            this.lbNewTabColor.Name = "lbNewTabColor";
            this.lbNewTabColor.Size = new System.Drawing.Size(141, 16);
            this.lbNewTabColor.TabIndex = 25;
            this.lbNewTabColor.Tag = "";
            this.lbNewTabColor.Text = "New Tab Button Color:";
            // 
            // lbBackImage
            // 
            this.lbBackImage.AutoSize = true;
            this.lbBackImage.BackColor = System.Drawing.Color.Transparent;
            this.lbBackImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbBackImage.Location = new System.Drawing.Point(17, 132);
            this.lbBackImage.Name = "lbBackImage";
            this.lbBackImage.Size = new System.Drawing.Size(128, 16);
            this.lbBackImage.TabIndex = 25;
            this.lbBackImage.Tag = "";
            this.lbBackImage.Text = "Background Image :";
            // 
            // pbStore
            // 
            this.pbStore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbStore.Image = global::Korot.Properties.Resources.store;
            this.pbStore.Location = new System.Drawing.Point(700, 246);
            this.pbStore.Name = "pbStore";
            this.pbStore.Size = new System.Drawing.Size(23, 23);
            this.pbStore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbStore.TabIndex = 27;
            this.pbStore.TabStop = false;
            this.pbStore.Tag = "";
            this.pbStore.Click += new System.EventHandler(this.pictureBox6_Click);
            // 
            // pbBack
            // 
            this.pbBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBack.Location = new System.Drawing.Point(173, 38);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(23, 23);
            this.pbBack.TabIndex = 27;
            this.pbBack.TabStop = false;
            this.pbBack.Tag = "";
            this.pbBack.Click += new System.EventHandler(this.PictureBox3_Click);
            // 
            // pbOverlay
            // 
            this.pbOverlay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOverlay.Location = new System.Drawing.Point(173, 67);
            this.pbOverlay.Name = "pbOverlay";
            this.pbOverlay.Size = new System.Drawing.Size(23, 23);
            this.pbOverlay.TabIndex = 28;
            this.pbOverlay.TabStop = false;
            this.pbOverlay.Tag = "";
            this.pbOverlay.Click += new System.EventHandler(this.PictureBox4_Click);
            // 
            // lbBackColor
            // 
            this.lbBackColor.AutoSize = true;
            this.lbBackColor.BackColor = System.Drawing.Color.Transparent;
            this.lbBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbBackColor.Location = new System.Drawing.Point(18, 38);
            this.lbBackColor.Name = "lbBackColor";
            this.lbBackColor.Size = new System.Drawing.Size(125, 16);
            this.lbBackColor.TabIndex = 25;
            this.lbBackColor.Tag = "";
            this.lbBackColor.Text = "Background Color : ";
            // 
            // lbOveralColor
            // 
            this.lbOveralColor.AutoSize = true;
            this.lbOveralColor.BackColor = System.Drawing.Color.Transparent;
            this.lbOveralColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbOveralColor.Location = new System.Drawing.Point(18, 67);
            this.lbOveralColor.Name = "lbOveralColor";
            this.lbOveralColor.Size = new System.Drawing.Size(92, 16);
            this.lbOveralColor.TabIndex = 26;
            this.lbOveralColor.Tag = "Overall theme (Selected Tab background,Loading Bar Color etc.)";
            this.lbOveralColor.Text = "Overal Color : ";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(157, 132);
            this.textBox4.MaxLength = 2147483647;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(566, 21);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "this is a certified home classic";
            this.textBox4.Click += new System.EventHandler(this.textBox4_Click);
            // 
            // lbThemeName
            // 
            this.lbThemeName.AutoSize = true;
            this.lbThemeName.BackColor = System.Drawing.Color.Transparent;
            this.lbThemeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbThemeName.Location = new System.Drawing.Point(17, 224);
            this.lbThemeName.Name = "lbThemeName";
            this.lbThemeName.Size = new System.Drawing.Size(100, 16);
            this.lbThemeName.TabIndex = 25;
            this.lbThemeName.Tag = "";
            this.lbThemeName.Text = "Theme Name : ";
            // 
            // lbThemes
            // 
            this.lbThemes.AutoSize = true;
            this.lbThemes.BackColor = System.Drawing.Color.Transparent;
            this.lbThemes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbThemes.Location = new System.Drawing.Point(17, 252);
            this.lbThemes.Name = "lbThemes";
            this.lbThemes.Size = new System.Drawing.Size(67, 16);
            this.lbThemes.TabIndex = 25;
            this.lbThemes.Tag = "";
            this.lbThemes.Text = "Themes : ";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.comboBox1.Location = new System.Drawing.Point(130, 221);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(546, 24);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.Tag = "";
            this.comboBox1.Enter += new System.EventHandler(this.comboBox1_Enter);
            this.comboBox1.Leave += new System.EventHandler(this.comboBox1_Leave);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.ButtonImage = global::Korot.Properties.Resources.collection;
            this.button12.DrawImage = true;
            this.button12.FlatAppearance.BorderSize = 0;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button12.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.button12.Location = new System.Drawing.Point(675, 220);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(48, 26);
            this.button12.TabIndex = 8;
            this.button12.Tag = "";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.Button12_Click);
            this.button12.Enter += new System.EventHandler(this.comboBox1_Enter);
            this.button12.Leave += new System.EventHandler(this.comboBox1_Leave);
            // 
            // listBox2
            // 
            this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 15;
            this.listBox2.Location = new System.Drawing.Point(20, 275);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(703, 289);
            this.listBox2.TabIndex = 10;
            this.listBox2.DoubleClick += new System.EventHandler(this.ListBox2_DoubleClick);
            // 
            // lbTheme
            // 
            this.lbTheme.AutoSize = true;
            this.lbTheme.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbTheme.Location = new System.Drawing.Point(15, 6);
            this.lbTheme.Name = "lbTheme";
            this.lbTheme.Size = new System.Drawing.Size(74, 25);
            this.lbTheme.TabIndex = 34;
            this.lbTheme.Text = "Theme";
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.pHisMan);
            this.tpHistory.Controls.Add(this.btClose6);
            this.tpHistory.Controls.Add(this.lbHistory);
            this.tpHistory.Location = new System.Drawing.Point(4, 24);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Size = new System.Drawing.Size(738, 608);
            this.tpHistory.TabIndex = 3;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            this.tpHistory.Enter += new System.EventHandler(this.tpHistory_Enter);
            // 
            // pHisMan
            // 
            this.pHisMan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pHisMan.Location = new System.Drawing.Point(13, 45);
            this.pHisMan.Name = "pHisMan";
            this.pHisMan.Size = new System.Drawing.Size(710, 548);
            this.pHisMan.TabIndex = 37;
            // 
            // btClose6
            // 
            this.btClose6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose6.BackColor = System.Drawing.Color.Transparent;
            this.btClose6.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose6.DrawImage = true;
            this.btClose6.FlatAppearance.BorderSize = 0;
            this.btClose6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose6.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose6.Location = new System.Drawing.Point(693, 5);
            this.btClose6.Name = "btClose6";
            this.btClose6.Size = new System.Drawing.Size(30, 30);
            this.btClose6.TabIndex = 0;
            this.btClose6.UseVisualStyleBackColor = false;
            this.btClose6.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbHistory
            // 
            this.lbHistory.AutoSize = true;
            this.lbHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbHistory.Location = new System.Drawing.Point(8, 6);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.Size = new System.Drawing.Size(72, 25);
            this.lbHistory.TabIndex = 36;
            this.lbHistory.Text = "History";
            // 
            // tpDownload
            // 
            this.tpDownload.Controls.Add(this.pDowMan);
            this.tpDownload.Controls.Add(this.btClose7);
            this.tpDownload.Controls.Add(this.lbDownloads);
            this.tpDownload.Controls.Add(this.hsOpen);
            this.tpDownload.Controls.Add(this.hsDownload);
            this.tpDownload.Controls.Add(this.tbFolder);
            this.tpDownload.Controls.Add(this.btDownloadFolder);
            this.tpDownload.Controls.Add(this.lbDownloadFolder);
            this.tpDownload.Controls.Add(this.lbOpen);
            this.tpDownload.Controls.Add(this.lbAutoDownload);
            this.tpDownload.Location = new System.Drawing.Point(4, 24);
            this.tpDownload.Name = "tpDownload";
            this.tpDownload.Size = new System.Drawing.Size(738, 608);
            this.tpDownload.TabIndex = 4;
            this.tpDownload.Text = "Downloads";
            this.tpDownload.UseVisualStyleBackColor = true;
            // 
            // pDowMan
            // 
            this.pDowMan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pDowMan.Location = new System.Drawing.Point(13, 111);
            this.pDowMan.Name = "pDowMan";
            this.pDowMan.Size = new System.Drawing.Size(710, 482);
            this.pDowMan.TabIndex = 37;
            // 
            // btClose7
            // 
            this.btClose7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose7.BackColor = System.Drawing.Color.Transparent;
            this.btClose7.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose7.DrawImage = true;
            this.btClose7.FlatAppearance.BorderSize = 0;
            this.btClose7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose7.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose7.Location = new System.Drawing.Point(693, 5);
            this.btClose7.Name = "btClose7";
            this.btClose7.Size = new System.Drawing.Size(30, 30);
            this.btClose7.TabIndex = 0;
            this.btClose7.UseVisualStyleBackColor = false;
            this.btClose7.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbDownloads
            // 
            this.lbDownloads.AutoSize = true;
            this.lbDownloads.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbDownloads.Location = new System.Drawing.Point(8, 6);
            this.lbDownloads.Name = "lbDownloads";
            this.lbDownloads.Size = new System.Drawing.Size(109, 25);
            this.lbDownloads.TabIndex = 36;
            this.lbDownloads.Text = "Downloads";
            // 
            // hsOpen
            // 
            this.hsOpen.Location = new System.Drawing.Point(168, 34);
            this.hsOpen.Name = "hsOpen";
            this.hsOpen.Size = new System.Drawing.Size(50, 19);
            this.hsOpen.TabIndex = 1;
            this.hsOpen.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsOpen_CheckedChanged);
            // 
            // hsDownload
            // 
            this.hsDownload.Location = new System.Drawing.Point(174, 59);
            this.hsDownload.Name = "hsDownload";
            this.hsDownload.Size = new System.Drawing.Size(50, 19);
            this.hsDownload.TabIndex = 2;
            this.hsDownload.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsDownload_CheckedChanged);
            // 
            // tbFolder
            // 
            this.tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolder.Location = new System.Drawing.Point(155, 83);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(535, 21);
            this.tbFolder.TabIndex = 3;
            this.tbFolder.Tag = "";
            this.tbFolder.TextChanged += new System.EventHandler(this.tbFolder_TextChanged);
            // 
            // btDownloadFolder
            // 
            this.btDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDownloadFolder.DrawImage = true;
            this.btDownloadFolder.FlatAppearance.BorderSize = 0;
            this.btDownloadFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDownloadFolder.Location = new System.Drawing.Point(696, 80);
            this.btDownloadFolder.Name = "btDownloadFolder";
            this.btDownloadFolder.Size = new System.Drawing.Size(27, 26);
            this.btDownloadFolder.TabIndex = 4;
            this.btDownloadFolder.Text = "...";
            this.btDownloadFolder.UseVisualStyleBackColor = true;
            this.btDownloadFolder.Click += new System.EventHandler(this.button17_Click);
            // 
            // lbDownloadFolder
            // 
            this.lbDownloadFolder.AutoSize = true;
            this.lbDownloadFolder.BackColor = System.Drawing.Color.Transparent;
            this.lbDownloadFolder.Location = new System.Drawing.Point(10, 86);
            this.lbDownloadFolder.Name = "lbDownloadFolder";
            this.lbDownloadFolder.Size = new System.Drawing.Size(135, 15);
            this.lbDownloadFolder.TabIndex = 17;
            this.lbDownloadFolder.Tag = "";
            this.lbDownloadFolder.Text = "Download to this folder:";
            // 
            // lbOpen
            // 
            this.lbOpen.AutoSize = true;
            this.lbOpen.BackColor = System.Drawing.Color.Transparent;
            this.lbOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbOpen.Location = new System.Drawing.Point(10, 38);
            this.lbOpen.Name = "lbOpen";
            this.lbOpen.Size = new System.Drawing.Size(149, 15);
            this.lbOpen.TabIndex = 36;
            this.lbOpen.Tag = "";
            this.lbOpen.Text = "Open files after download:";
            // 
            // lbAutoDownload
            // 
            this.lbAutoDownload.AutoSize = true;
            this.lbAutoDownload.BackColor = System.Drawing.Color.Transparent;
            this.lbAutoDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbAutoDownload.Location = new System.Drawing.Point(10, 62);
            this.lbAutoDownload.Name = "lbAutoDownload";
            this.lbAutoDownload.Size = new System.Drawing.Size(149, 15);
            this.lbAutoDownload.TabIndex = 36;
            this.lbAutoDownload.Tag = "";
            this.lbAutoDownload.Text = "Auto-download to a folder:";
            // 
            // tpAbout
            // 
            this.tpAbout.AutoScroll = true;
            this.tpAbout.BackColor = System.Drawing.Color.White;
            this.tpAbout.Controls.Add(this.btClose10);
            this.tpAbout.Controls.Add(this.lbAbout);
            this.tpAbout.Controls.Add(this.lbUpdateStatus);
            this.tpAbout.Controls.Add(this.btInstall);
            this.tpAbout.Controls.Add(this.btUpdater);
            this.tpAbout.Controls.Add(this.llLicenses);
            this.tpAbout.Controls.Add(this.label21);
            this.tpAbout.Controls.Add(this.lbVersion);
            this.tpAbout.Controls.Add(this.label20);
            this.tpAbout.Controls.Add(this.lbKorot);
            this.tpAbout.Controls.Add(this.pictureBox5);
            this.tpAbout.Location = new System.Drawing.Point(4, 24);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tpAbout.Size = new System.Drawing.Size(738, 608);
            this.tpAbout.TabIndex = 5;
            this.tpAbout.Text = "About";
            // 
            // btClose10
            // 
            this.btClose10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose10.BackColor = System.Drawing.Color.Transparent;
            this.btClose10.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose10.DrawImage = true;
            this.btClose10.FlatAppearance.BorderSize = 0;
            this.btClose10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose10.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose10.Location = new System.Drawing.Point(693, 5);
            this.btClose10.Name = "btClose10";
            this.btClose10.Size = new System.Drawing.Size(30, 30);
            this.btClose10.TabIndex = 0;
            this.btClose10.UseVisualStyleBackColor = false;
            this.btClose10.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbAbout
            // 
            this.lbAbout.AutoSize = true;
            this.lbAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbAbout.Location = new System.Drawing.Point(8, 6);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(64, 25);
            this.lbAbout.TabIndex = 36;
            this.lbAbout.Text = "About";
            // 
            // lbUpdateStatus
            // 
            this.lbUpdateStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbUpdateStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbUpdateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbUpdateStatus.Location = new System.Drawing.Point(3, 526);
            this.lbUpdateStatus.Name = "lbUpdateStatus";
            this.lbUpdateStatus.Size = new System.Drawing.Size(732, 17);
            this.lbUpdateStatus.TabIndex = 1;
            this.lbUpdateStatus.Text = "Checking for Updates...";
            this.lbUpdateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btInstall
            // 
            this.btInstall.BackColor = System.Drawing.Color.Transparent;
            this.btInstall.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btInstall.DrawImage = true;
            this.btInstall.FlatAppearance.BorderSize = 0;
            this.btInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInstall.Location = new System.Drawing.Point(3, 543);
            this.btInstall.Name = "btInstall";
            this.btInstall.Size = new System.Drawing.Size(732, 31);
            this.btInstall.TabIndex = 3;
            this.btInstall.Text = "Install the update";
            this.btInstall.UseVisualStyleBackColor = false;
            this.btInstall.Visible = false;
            this.btInstall.Click += new System.EventHandler(this.btInstall_Click);
            // 
            // btUpdater
            // 
            this.btUpdater.BackColor = System.Drawing.Color.Transparent;
            this.btUpdater.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btUpdater.DrawImage = true;
            this.btUpdater.FlatAppearance.BorderSize = 0;
            this.btUpdater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUpdater.Location = new System.Drawing.Point(3, 574);
            this.btUpdater.Name = "btUpdater";
            this.btUpdater.Size = new System.Drawing.Size(732, 31);
            this.btUpdater.TabIndex = 2;
            this.btUpdater.Text = "Check for Updates";
            this.btUpdater.UseVisualStyleBackColor = false;
            this.btUpdater.Click += new System.EventHandler(this.btUpdater_Click);
            // 
            // llLicenses
            // 
            this.llLicenses.AutoSize = true;
            this.llLicenses.BackColor = System.Drawing.Color.Transparent;
            this.llLicenses.Location = new System.Drawing.Point(15, 172);
            this.llLicenses.Name = "llLicenses";
            this.llLicenses.Size = new System.Drawing.Size(163, 15);
            this.llLicenses.TabIndex = 1;
            this.llLicenses.TabStop = true;
            this.llLicenses.Text = "Licenses && Special Thanks...";
            this.llLicenses.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(15, 124);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(361, 45);
            this.label21.TabIndex = 4;
            this.label21.Text = "Korot uses Chromium by Google using CefSharp.\r\nKorot is written in C# using Visua" +
    "l Studio Community by Microsoft.\r\nKorot uses modified version of EasyTabs.";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.BackColor = System.Drawing.Color.Transparent;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbVersion.Location = new System.Drawing.Point(171, 40);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(52, 16);
            this.lbVersion.TabIndex = 1;
            this.lbVersion.Text = "version";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label20.Location = new System.Drawing.Point(71, 82);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 25);
            this.label20.TabIndex = 1;
            this.label20.Text = "Haltroy";
            this.label20.MouseClick += new System.Windows.Forms.MouseEventHandler(this.label20_MouseClick);
            // 
            // lbKorot
            // 
            this.lbKorot.AutoSize = true;
            this.lbKorot.BackColor = System.Drawing.Color.Transparent;
            this.lbKorot.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKorot.Location = new System.Drawing.Point(64, 40);
            this.lbKorot.Name = "lbKorot";
            this.lbKorot.Size = new System.Drawing.Size(95, 38);
            this.lbKorot.TabIndex = 1;
            this.lbKorot.Text = "Korot";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::Korot.Properties.Resources.Korot;
            this.pictureBox5.Location = new System.Drawing.Point(18, 41);
            this.pictureBox5.MaximumSize = new System.Drawing.Size(44, 41);
            this.pictureBox5.MinimumSize = new System.Drawing.Size(44, 41);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(44, 41);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 0;
            this.pictureBox5.TabStop = false;
            // 
            // tpSite
            // 
            this.tpSite.Controls.Add(this.btBlocked);
            this.tpSite.Controls.Add(this.pSite);
            this.tpSite.Controls.Add(this.btCookieBack);
            this.tpSite.Controls.Add(this.btClose8);
            this.tpSite.Controls.Add(this.lbSiteSettings);
            this.tpSite.Location = new System.Drawing.Point(4, 24);
            this.tpSite.Name = "tpSite";
            this.tpSite.Size = new System.Drawing.Size(738, 608);
            this.tpSite.TabIndex = 7;
            this.tpSite.Text = "Site Settings";
            this.tpSite.UseVisualStyleBackColor = true;
            // 
            // btBlocked
            // 
            this.btBlocked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btBlocked.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btBlocked.DrawImage = true;
            this.btBlocked.FlatAppearance.BorderSize = 0;
            this.btBlocked.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBlocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btBlocked.Location = new System.Drawing.Point(0, 580);
            this.btBlocked.Name = "btBlocked";
            this.btBlocked.Size = new System.Drawing.Size(738, 28);
            this.btBlocked.TabIndex = 47;
            this.btBlocked.Text = "Blocked Sites...";
            this.btBlocked.UseVisualStyleBackColor = false;
            this.btBlocked.Click += new System.EventHandler(this.btBlocked_Click);
            // 
            // pSite
            // 
            this.pSite.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pSite.Location = new System.Drawing.Point(19, 39);
            this.pSite.Name = "pSite";
            this.pSite.Size = new System.Drawing.Size(704, 535);
            this.pSite.TabIndex = 46;
            // 
            // btCookieBack
            // 
            this.btCookieBack.BackColor = System.Drawing.Color.Transparent;
            this.btCookieBack.ButtonImage = global::Korot.Properties.Resources.leftarrow;
            this.btCookieBack.ContextMenuStrip = this.cmsBack;
            this.btCookieBack.DrawImage = true;
            this.btCookieBack.FlatAppearance.BorderSize = 0;
            this.btCookieBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCookieBack.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btCookieBack.Location = new System.Drawing.Point(14, 5);
            this.btCookieBack.Name = "btCookieBack";
            this.btCookieBack.Size = new System.Drawing.Size(30, 28);
            this.btCookieBack.TabIndex = 45;
            this.btCookieBack.UseVisualStyleBackColor = false;
            this.btCookieBack.Click += new System.EventHandler(this.btNotifBack_Click);
            // 
            // btClose8
            // 
            this.btClose8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose8.BackColor = System.Drawing.Color.Transparent;
            this.btClose8.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose8.DrawImage = true;
            this.btClose8.FlatAppearance.BorderSize = 0;
            this.btClose8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose8.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose8.Location = new System.Drawing.Point(693, 5);
            this.btClose8.Name = "btClose8";
            this.btClose8.Size = new System.Drawing.Size(30, 30);
            this.btClose8.TabIndex = 0;
            this.btClose8.UseVisualStyleBackColor = false;
            this.btClose8.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbSiteSettings
            // 
            this.lbSiteSettings.AutoSize = true;
            this.lbSiteSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbSiteSettings.Location = new System.Drawing.Point(50, 4);
            this.lbSiteSettings.Name = "lbSiteSettings";
            this.lbSiteSettings.Size = new System.Drawing.Size(122, 25);
            this.lbSiteSettings.TabIndex = 0;
            this.lbSiteSettings.Text = "Site Settings";
            // 
            // tpCollection
            // 
            this.tpCollection.Controls.Add(this.panel3);
            this.tpCollection.Controls.Add(this.btClose9);
            this.tpCollection.Controls.Add(this.lbCollections);
            this.tpCollection.Location = new System.Drawing.Point(4, 24);
            this.tpCollection.Name = "tpCollection";
            this.tpCollection.Size = new System.Drawing.Size(738, 608);
            this.tpCollection.TabIndex = 8;
            this.tpCollection.Text = "Collection";
            this.tpCollection.UseVisualStyleBackColor = true;
            this.tpCollection.Enter += new System.EventHandler(this.tpCollection_Enter);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Location = new System.Drawing.Point(16, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(707, 552);
            this.panel3.TabIndex = 1;
            // 
            // btClose9
            // 
            this.btClose9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose9.BackColor = System.Drawing.Color.Transparent;
            this.btClose9.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose9.DrawImage = true;
            this.btClose9.FlatAppearance.BorderSize = 0;
            this.btClose9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose9.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose9.Location = new System.Drawing.Point(693, 5);
            this.btClose9.Name = "btClose9";
            this.btClose9.Size = new System.Drawing.Size(30, 30);
            this.btClose9.TabIndex = 0;
            this.btClose9.UseVisualStyleBackColor = false;
            this.btClose9.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbCollections
            // 
            this.lbCollections.AutoSize = true;
            this.lbCollections.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbCollections.Location = new System.Drawing.Point(8, 6);
            this.lbCollections.Name = "lbCollections";
            this.lbCollections.Size = new System.Drawing.Size(108, 25);
            this.lbCollections.TabIndex = 38;
            this.lbCollections.Text = "Collections";
            // 
            // tpNotification
            // 
            this.tpNotification.Controls.Add(this.btNotifBack);
            this.tpNotification.Controls.Add(this.panel1);
            this.tpNotification.Controls.Add(this.lbSchedule);
            this.tpNotification.Controls.Add(this.lbSilentMode);
            this.tpNotification.Controls.Add(this.hsSchedule);
            this.tpNotification.Controls.Add(this.hsSilent);
            this.tpNotification.Controls.Add(this.lbPlayNotifSound);
            this.tpNotification.Controls.Add(this.hsNotificationSound);
            this.tpNotification.Controls.Add(this.btClose3);
            this.tpNotification.Controls.Add(this.lbNotifSetting);
            this.tpNotification.Location = new System.Drawing.Point(4, 24);
            this.tpNotification.Name = "tpNotification";
            this.tpNotification.Size = new System.Drawing.Size(738, 608);
            this.tpNotification.TabIndex = 9;
            this.tpNotification.Text = "Notification Setting";
            this.tpNotification.UseVisualStyleBackColor = true;
            // 
            // btNotifBack
            // 
            this.btNotifBack.BackColor = System.Drawing.Color.Transparent;
            this.btNotifBack.ButtonImage = global::Korot.Properties.Resources.leftarrow;
            this.btNotifBack.ContextMenuStrip = this.cmsBack;
            this.btNotifBack.DrawImage = true;
            this.btNotifBack.FlatAppearance.BorderSize = 0;
            this.btNotifBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNotifBack.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btNotifBack.Location = new System.Drawing.Point(15, 7);
            this.btNotifBack.Name = "btNotifBack";
            this.btNotifBack.Size = new System.Drawing.Size(30, 28);
            this.btNotifBack.TabIndex = 44;
            this.btNotifBack.UseVisualStyleBackColor = false;
            this.btNotifBack.Click += new System.EventHandler(this.btNotifBack_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.flpFrom);
            this.panel1.Controls.Add(this.flpEvery);
            this.panel1.Controls.Add(this.lb24HType);
            this.panel1.Controls.Add(this.scheduleFrom);
            this.panel1.Controls.Add(this.flpTo);
            this.panel1.Controls.Add(this.scheduleEvery);
            this.panel1.Controls.Add(this.scheduleTo);
            this.panel1.Location = new System.Drawing.Point(16, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 116);
            this.panel1.TabIndex = 42;
            // 
            // flpFrom
            // 
            this.flpFrom.AutoSize = true;
            this.flpFrom.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpFrom.Controls.Add(this.fromHour);
            this.flpFrom.Controls.Add(this.label40);
            this.flpFrom.Controls.Add(this.fromMin);
            this.flpFrom.Location = new System.Drawing.Point(58, 11);
            this.flpFrom.Name = "flpFrom";
            this.flpFrom.Size = new System.Drawing.Size(96, 27);
            this.flpFrom.TabIndex = 43;
            // 
            // fromHour
            // 
            this.fromHour.Location = new System.Drawing.Point(3, 3);
            this.fromHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.fromHour.Name = "fromHour";
            this.fromHour.Size = new System.Drawing.Size(34, 21);
            this.fromHour.TabIndex = 46;
            this.fromHour.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.fromHour.ValueChanged += new System.EventHandler(this.fromHour_ValueChanged);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(43, 3);
            this.label40.Margin = new System.Windows.Forms.Padding(3);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(10, 15);
            this.label40.TabIndex = 45;
            this.label40.Text = ":";
            // 
            // fromMin
            // 
            this.fromMin.Location = new System.Drawing.Point(59, 3);
            this.fromMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.fromMin.Name = "fromMin";
            this.fromMin.Size = new System.Drawing.Size(34, 21);
            this.fromMin.TabIndex = 47;
            this.fromMin.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.fromMin.ValueChanged += new System.EventHandler(this.fromHour_ValueChanged);
            // 
            // flpEvery
            // 
            this.flpEvery.AutoSize = true;
            this.flpEvery.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpEvery.Controls.Add(this.lbSunday);
            this.flpEvery.Controls.Add(this.lbMonday);
            this.flpEvery.Controls.Add(this.lbTuesday);
            this.flpEvery.Controls.Add(this.lbWednesday);
            this.flpEvery.Controls.Add(this.lbThursday);
            this.flpEvery.Controls.Add(this.lbFriday);
            this.flpEvery.Controls.Add(this.lbSaturday);
            this.flpEvery.Location = new System.Drawing.Point(60, 77);
            this.flpEvery.Name = "flpEvery";
            this.flpEvery.Size = new System.Drawing.Size(208, 27);
            this.flpEvery.TabIndex = 43;
            // 
            // lbSunday
            // 
            this.lbSunday.AutoSize = true;
            this.lbSunday.Location = new System.Drawing.Point(3, 3);
            this.lbSunday.Margin = new System.Windows.Forms.Padding(3);
            this.lbSunday.Name = "lbSunday";
            this.lbSunday.Padding = new System.Windows.Forms.Padding(3);
            this.lbSunday.Size = new System.Drawing.Size(28, 21);
            this.lbSunday.TabIndex = 0;
            this.lbSunday.Tag = "0";
            this.lbSunday.Text = "Su";
            this.lbSunday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lbMonday
            // 
            this.lbMonday.AutoSize = true;
            this.lbMonday.Location = new System.Drawing.Point(37, 3);
            this.lbMonday.Margin = new System.Windows.Forms.Padding(3);
            this.lbMonday.Name = "lbMonday";
            this.lbMonday.Padding = new System.Windows.Forms.Padding(3);
            this.lbMonday.Size = new System.Drawing.Size(24, 21);
            this.lbMonday.TabIndex = 1;
            this.lbMonday.Tag = "0";
            this.lbMonday.Text = "M";
            this.lbMonday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lbTuesday
            // 
            this.lbTuesday.AutoSize = true;
            this.lbTuesday.Location = new System.Drawing.Point(67, 3);
            this.lbTuesday.Margin = new System.Windows.Forms.Padding(3);
            this.lbTuesday.Name = "lbTuesday";
            this.lbTuesday.Padding = new System.Windows.Forms.Padding(3);
            this.lbTuesday.Size = new System.Drawing.Size(20, 21);
            this.lbTuesday.TabIndex = 2;
            this.lbTuesday.Tag = "0";
            this.lbTuesday.Text = "T";
            this.lbTuesday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lbWednesday
            // 
            this.lbWednesday.AutoSize = true;
            this.lbWednesday.Location = new System.Drawing.Point(93, 3);
            this.lbWednesday.Margin = new System.Windows.Forms.Padding(3);
            this.lbWednesday.Name = "lbWednesday";
            this.lbWednesday.Padding = new System.Windows.Forms.Padding(3);
            this.lbWednesday.Size = new System.Drawing.Size(24, 21);
            this.lbWednesday.TabIndex = 3;
            this.lbWednesday.Tag = "0";
            this.lbWednesday.Text = "W";
            this.lbWednesday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lbThursday
            // 
            this.lbThursday.AutoSize = true;
            this.lbThursday.Location = new System.Drawing.Point(123, 3);
            this.lbThursday.Margin = new System.Windows.Forms.Padding(3);
            this.lbThursday.Name = "lbThursday";
            this.lbThursday.Padding = new System.Windows.Forms.Padding(3);
            this.lbThursday.Size = new System.Drawing.Size(29, 21);
            this.lbThursday.TabIndex = 4;
            this.lbThursday.Tag = "0";
            this.lbThursday.Text = "TH";
            this.lbThursday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lbFriday
            // 
            this.lbFriday.AutoSize = true;
            this.lbFriday.Location = new System.Drawing.Point(158, 3);
            this.lbFriday.Margin = new System.Windows.Forms.Padding(3);
            this.lbFriday.Name = "lbFriday";
            this.lbFriday.Padding = new System.Windows.Forms.Padding(3);
            this.lbFriday.Size = new System.Drawing.Size(20, 21);
            this.lbFriday.TabIndex = 5;
            this.lbFriday.Tag = "0";
            this.lbFriday.Text = "F";
            this.lbFriday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lbSaturday
            // 
            this.lbSaturday.AutoSize = true;
            this.lbSaturday.Location = new System.Drawing.Point(184, 3);
            this.lbSaturday.Margin = new System.Windows.Forms.Padding(3);
            this.lbSaturday.Name = "lbSaturday";
            this.lbSaturday.Padding = new System.Windows.Forms.Padding(3);
            this.lbSaturday.Size = new System.Drawing.Size(21, 21);
            this.lbSaturday.TabIndex = 6;
            this.lbSaturday.Tag = "0";
            this.lbSaturday.Text = "S";
            this.lbSaturday.Click += new System.EventHandler(this.lbHaftaGunu_Click);
            // 
            // lb24HType
            // 
            this.lb24HType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb24HType.BackColor = System.Drawing.Color.Transparent;
            this.lb24HType.Location = new System.Drawing.Point(12, 47);
            this.lb24HType.Name = "lb24HType";
            this.lb24HType.Size = new System.Drawing.Size(692, 19);
            this.lb24HType.TabIndex = 40;
            this.lb24HType.Text = "Based on 24-hour type. Add 12 to hours on PM.";
            // 
            // scheduleFrom
            // 
            this.scheduleFrom.AutoSize = true;
            this.scheduleFrom.Location = new System.Drawing.Point(12, 16);
            this.scheduleFrom.Name = "scheduleFrom";
            this.scheduleFrom.Size = new System.Drawing.Size(39, 15);
            this.scheduleFrom.TabIndex = 41;
            this.scheduleFrom.Text = "From:";
            // 
            // flpTo
            // 
            this.flpTo.AutoSize = true;
            this.flpTo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpTo.Controls.Add(this.toHour);
            this.flpTo.Controls.Add(this.label41);
            this.flpTo.Controls.Add(this.toMin);
            this.flpTo.Location = new System.Drawing.Point(193, 11);
            this.flpTo.Name = "flpTo";
            this.flpTo.Size = new System.Drawing.Size(96, 27);
            this.flpTo.TabIndex = 48;
            // 
            // toHour
            // 
            this.toHour.Location = new System.Drawing.Point(3, 3);
            this.toHour.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.toHour.Name = "toHour";
            this.toHour.Size = new System.Drawing.Size(34, 21);
            this.toHour.TabIndex = 46;
            this.toHour.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.toHour.ValueChanged += new System.EventHandler(this.fromHour_ValueChanged);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(43, 3);
            this.label41.Margin = new System.Windows.Forms.Padding(3);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(10, 15);
            this.label41.TabIndex = 45;
            this.label41.Text = ":";
            // 
            // toMin
            // 
            this.toMin.Location = new System.Drawing.Point(59, 3);
            this.toMin.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.toMin.Name = "toMin";
            this.toMin.Size = new System.Drawing.Size(34, 21);
            this.toMin.TabIndex = 47;
            this.toMin.Value = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.toMin.ValueChanged += new System.EventHandler(this.fromHour_ValueChanged);
            // 
            // scheduleEvery
            // 
            this.scheduleEvery.AutoSize = true;
            this.scheduleEvery.Location = new System.Drawing.Point(14, 83);
            this.scheduleEvery.Name = "scheduleEvery";
            this.scheduleEvery.Size = new System.Drawing.Size(39, 15);
            this.scheduleEvery.TabIndex = 41;
            this.scheduleEvery.Text = "Every:";
            // 
            // scheduleTo
            // 
            this.scheduleTo.AutoSize = true;
            this.scheduleTo.Location = new System.Drawing.Point(163, 17);
            this.scheduleTo.Name = "scheduleTo";
            this.scheduleTo.Size = new System.Drawing.Size(24, 15);
            this.scheduleTo.TabIndex = 41;
            this.scheduleTo.Text = "To:";
            // 
            // lbSchedule
            // 
            this.lbSchedule.AutoSize = true;
            this.lbSchedule.BackColor = System.Drawing.Color.Transparent;
            this.lbSchedule.Location = new System.Drawing.Point(15, 119);
            this.lbSchedule.Name = "lbSchedule";
            this.lbSchedule.Size = new System.Drawing.Size(131, 15);
            this.lbSchedule.TabIndex = 40;
            this.lbSchedule.Text = "Schedule Silent Mode:";
            // 
            // lbSilentMode
            // 
            this.lbSilentMode.AutoSize = true;
            this.lbSilentMode.BackColor = System.Drawing.Color.Transparent;
            this.lbSilentMode.Location = new System.Drawing.Point(13, 88);
            this.lbSilentMode.Name = "lbSilentMode";
            this.lbSilentMode.Size = new System.Drawing.Size(76, 15);
            this.lbSilentMode.TabIndex = 40;
            this.lbSilentMode.Text = "Silent Mode:";
            // 
            // hsSchedule
            // 
            this.hsSchedule.Location = new System.Drawing.Point(153, 119);
            this.hsSchedule.Name = "hsSchedule";
            this.hsSchedule.Size = new System.Drawing.Size(50, 19);
            this.hsSchedule.TabIndex = 39;
            this.hsSchedule.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsSchedule_CheckedChanged);
            // 
            // hsSilent
            // 
            this.hsSilent.Location = new System.Drawing.Point(97, 88);
            this.hsSilent.Name = "hsSilent";
            this.hsSilent.Size = new System.Drawing.Size(50, 19);
            this.hsSilent.TabIndex = 39;
            this.hsSilent.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsSilent_CheckedChanged);
            // 
            // lbPlayNotifSound
            // 
            this.lbPlayNotifSound.AutoSize = true;
            this.lbPlayNotifSound.BackColor = System.Drawing.Color.Transparent;
            this.lbPlayNotifSound.Location = new System.Drawing.Point(13, 56);
            this.lbPlayNotifSound.Name = "lbPlayNotifSound";
            this.lbPlayNotifSound.Size = new System.Drawing.Size(136, 15);
            this.lbPlayNotifSound.TabIndex = 40;
            this.lbPlayNotifSound.Text = "Play Notification Sound:";
            // 
            // hsNotificationSound
            // 
            this.hsNotificationSound.Location = new System.Drawing.Point(160, 56);
            this.hsNotificationSound.Name = "hsNotificationSound";
            this.hsNotificationSound.Size = new System.Drawing.Size(50, 19);
            this.hsNotificationSound.TabIndex = 39;
            this.hsNotificationSound.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsNotificationSound_CheckedChanged);
            // 
            // btClose3
            // 
            this.btClose3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose3.BackColor = System.Drawing.Color.Transparent;
            this.btClose3.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose3.DrawImage = true;
            this.btClose3.FlatAppearance.BorderSize = 0;
            this.btClose3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose3.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose3.Location = new System.Drawing.Point(693, 5);
            this.btClose3.Name = "btClose3";
            this.btClose3.Size = new System.Drawing.Size(30, 30);
            this.btClose3.TabIndex = 37;
            this.btClose3.UseVisualStyleBackColor = false;
            this.btClose3.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbNotifSetting
            // 
            this.lbNotifSetting.AutoSize = true;
            this.lbNotifSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbNotifSetting.Location = new System.Drawing.Point(50, 8);
            this.lbNotifSetting.Name = "lbNotifSetting";
            this.lbNotifSetting.Size = new System.Drawing.Size(183, 25);
            this.lbNotifSetting.TabIndex = 38;
            this.lbNotifSetting.Text = "Notification Settings";
            // 
            // tpNewTab
            // 
            this.tpNewTab.Controls.Add(this.tbUrl);
            this.tpNewTab.Controls.Add(this.tbTitle);
            this.tpNewTab.Controls.Add(this.tlpNewTab);
            this.tpNewTab.Controls.Add(this.btClear);
            this.tpNewTab.Controls.Add(this.lbNTUrl);
            this.tpNewTab.Controls.Add(this.lbNTTitle);
            this.tpNewTab.Controls.Add(this.btNewTabBack);
            this.tpNewTab.Controls.Add(this.btClose5);
            this.tpNewTab.Controls.Add(this.lbNewTabTitle);
            this.tpNewTab.Location = new System.Drawing.Point(4, 24);
            this.tpNewTab.Name = "tpNewTab";
            this.tpNewTab.Size = new System.Drawing.Size(738, 608);
            this.tpNewTab.TabIndex = 10;
            this.tpNewTab.Text = "New Tab Page Content";
            this.tpNewTab.UseVisualStyleBackColor = true;
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.tbUrl.Location = new System.Drawing.Point(54, 86);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(669, 21);
            this.tbUrl.TabIndex = 54;
            this.tbUrl.TextChanged += new System.EventHandler(this.tbUrl_TextChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.tbTitle.Location = new System.Drawing.Point(58, 53);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(665, 21);
            this.tbTitle.TabIndex = 53;
            this.tbTitle.TextChanged += new System.EventHandler(this.tbTitle_TextChanged);
            // 
            // tlpNewTab
            // 
            this.tlpNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpNewTab.ColumnCount = 5;
            this.tlpNewTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNewTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNewTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNewTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNewTab.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpNewTab.Controls.Add(this.L9, 4, 1);
            this.tlpNewTab.Controls.Add(this.L8, 3, 1);
            this.tlpNewTab.Controls.Add(this.L7, 2, 1);
            this.tlpNewTab.Controls.Add(this.L6, 1, 1);
            this.tlpNewTab.Controls.Add(this.L5, 0, 1);
            this.tlpNewTab.Controls.Add(this.L4, 4, 0);
            this.tlpNewTab.Controls.Add(this.L3, 3, 0);
            this.tlpNewTab.Controls.Add(this.L2, 2, 0);
            this.tlpNewTab.Controls.Add(this.L1, 1, 0);
            this.tlpNewTab.Controls.Add(this.L0, 0, 0);
            this.tlpNewTab.Location = new System.Drawing.Point(19, 147);
            this.tlpNewTab.Name = "tlpNewTab";
            this.tlpNewTab.RowCount = 2;
            this.tlpNewTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNewTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNewTab.Size = new System.Drawing.Size(704, 133);
            this.tlpNewTab.TabIndex = 52;
            // 
            // L9
            // 
            this.L9.Controls.Add(this.L9T);
            this.L9.Controls.Add(this.L9U);
            this.L9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L9.Location = new System.Drawing.Point(563, 69);
            this.L9.Name = "L9";
            this.L9.Size = new System.Drawing.Size(138, 61);
            this.L9.TabIndex = 9;
            this.L9.Tag = "9";
            this.L9.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L9T
            // 
            this.L9T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L9T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L9T.Location = new System.Drawing.Point(0, 20);
            this.L9T.Name = "L9T";
            this.L9T.Size = new System.Drawing.Size(138, 22);
            this.L9T.TabIndex = 0;
            this.L9T.Tag = "9";
            this.L9T.Text = "Title";
            this.L9T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L9T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L9U
            // 
            this.L9U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L9U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L9U.Location = new System.Drawing.Point(0, 42);
            this.L9U.Name = "L9U";
            this.L9U.Size = new System.Drawing.Size(138, 19);
            this.L9U.TabIndex = 1;
            this.L9U.Tag = "9";
            this.L9U.Text = "Title";
            this.L9U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L9U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L8
            // 
            this.L8.Controls.Add(this.L8T);
            this.L8.Controls.Add(this.L8U);
            this.L8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L8.Location = new System.Drawing.Point(423, 69);
            this.L8.Name = "L8";
            this.L8.Size = new System.Drawing.Size(134, 61);
            this.L8.TabIndex = 8;
            this.L8.Tag = "8";
            this.L8.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L8T
            // 
            this.L8T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L8T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L8T.Location = new System.Drawing.Point(0, 20);
            this.L8T.Name = "L8T";
            this.L8T.Size = new System.Drawing.Size(134, 22);
            this.L8T.TabIndex = 0;
            this.L8T.Tag = "8";
            this.L8T.Text = "Title";
            this.L8T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L8T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L8U
            // 
            this.L8U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L8U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L8U.Location = new System.Drawing.Point(0, 42);
            this.L8U.Name = "L8U";
            this.L8U.Size = new System.Drawing.Size(134, 19);
            this.L8U.TabIndex = 1;
            this.L8U.Tag = "8";
            this.L8U.Text = "Title";
            this.L8U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L8U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L7
            // 
            this.L7.Controls.Add(this.L7T);
            this.L7.Controls.Add(this.L7U);
            this.L7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L7.Location = new System.Drawing.Point(283, 69);
            this.L7.Name = "L7";
            this.L7.Size = new System.Drawing.Size(134, 61);
            this.L7.TabIndex = 7;
            this.L7.Tag = "7";
            this.L7.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L7T
            // 
            this.L7T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L7T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L7T.Location = new System.Drawing.Point(0, 20);
            this.L7T.Name = "L7T";
            this.L7T.Size = new System.Drawing.Size(134, 22);
            this.L7T.TabIndex = 0;
            this.L7T.Tag = "7";
            this.L7T.Text = "Title";
            this.L7T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L7T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L7U
            // 
            this.L7U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L7U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L7U.Location = new System.Drawing.Point(0, 42);
            this.L7U.Name = "L7U";
            this.L7U.Size = new System.Drawing.Size(134, 19);
            this.L7U.TabIndex = 1;
            this.L7U.Tag = "7";
            this.L7U.Text = "Title";
            this.L7U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L7U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L6
            // 
            this.L6.Controls.Add(this.L6T);
            this.L6.Controls.Add(this.L6U);
            this.L6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L6.Location = new System.Drawing.Point(143, 69);
            this.L6.Name = "L6";
            this.L6.Size = new System.Drawing.Size(134, 61);
            this.L6.TabIndex = 6;
            this.L6.Tag = "6";
            this.L6.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L6T
            // 
            this.L6T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L6T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L6T.Location = new System.Drawing.Point(0, 20);
            this.L6T.Name = "L6T";
            this.L6T.Size = new System.Drawing.Size(134, 22);
            this.L6T.TabIndex = 0;
            this.L6T.Tag = "6";
            this.L6T.Text = "Title";
            this.L6T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L6T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L6U
            // 
            this.L6U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L6U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L6U.Location = new System.Drawing.Point(0, 42);
            this.L6U.Name = "L6U";
            this.L6U.Size = new System.Drawing.Size(134, 19);
            this.L6U.TabIndex = 1;
            this.L6U.Tag = "6";
            this.L6U.Text = "Title";
            this.L6U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L6U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L5
            // 
            this.L5.Controls.Add(this.L5T);
            this.L5.Controls.Add(this.L5U);
            this.L5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L5.Location = new System.Drawing.Point(3, 69);
            this.L5.Name = "L5";
            this.L5.Size = new System.Drawing.Size(134, 61);
            this.L5.TabIndex = 5;
            this.L5.Tag = "5";
            this.L5.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L5T
            // 
            this.L5T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L5T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L5T.Location = new System.Drawing.Point(0, 20);
            this.L5T.Name = "L5T";
            this.L5T.Size = new System.Drawing.Size(134, 22);
            this.L5T.TabIndex = 0;
            this.L5T.Tag = "5";
            this.L5T.Text = "Title";
            this.L5T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L5T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L5U
            // 
            this.L5U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L5U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L5U.Location = new System.Drawing.Point(0, 42);
            this.L5U.Name = "L5U";
            this.L5U.Size = new System.Drawing.Size(134, 19);
            this.L5U.TabIndex = 1;
            this.L5U.Tag = "5";
            this.L5U.Text = "Title";
            this.L5U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L5U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L4
            // 
            this.L4.Controls.Add(this.L4T);
            this.L4.Controls.Add(this.L4U);
            this.L4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L4.Location = new System.Drawing.Point(563, 3);
            this.L4.Name = "L4";
            this.L4.Size = new System.Drawing.Size(138, 60);
            this.L4.TabIndex = 4;
            this.L4.Tag = "4";
            this.L4.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L4T
            // 
            this.L4T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L4T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L4T.Location = new System.Drawing.Point(0, 19);
            this.L4T.Name = "L4T";
            this.L4T.Size = new System.Drawing.Size(138, 22);
            this.L4T.TabIndex = 0;
            this.L4T.Tag = "4";
            this.L4T.Text = "Title";
            this.L4T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L4T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L4U
            // 
            this.L4U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L4U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L4U.Location = new System.Drawing.Point(0, 41);
            this.L4U.Name = "L4U";
            this.L4U.Size = new System.Drawing.Size(138, 19);
            this.L4U.TabIndex = 1;
            this.L4U.Tag = "4";
            this.L4U.Text = "Title";
            this.L4U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L4U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L3
            // 
            this.L3.Controls.Add(this.L3T);
            this.L3.Controls.Add(this.L3U);
            this.L3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L3.Location = new System.Drawing.Point(423, 3);
            this.L3.Name = "L3";
            this.L3.Size = new System.Drawing.Size(134, 60);
            this.L3.TabIndex = 3;
            this.L3.Tag = "3";
            this.L3.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L3T
            // 
            this.L3T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L3T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L3T.Location = new System.Drawing.Point(0, 19);
            this.L3T.Name = "L3T";
            this.L3T.Size = new System.Drawing.Size(134, 22);
            this.L3T.TabIndex = 0;
            this.L3T.Tag = "3";
            this.L3T.Text = "Title";
            this.L3T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L3T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L3U
            // 
            this.L3U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L3U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L3U.Location = new System.Drawing.Point(0, 41);
            this.L3U.Name = "L3U";
            this.L3U.Size = new System.Drawing.Size(134, 19);
            this.L3U.TabIndex = 1;
            this.L3U.Tag = "3";
            this.L3U.Text = "Title";
            this.L3U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L3U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L2
            // 
            this.L2.Controls.Add(this.L2T);
            this.L2.Controls.Add(this.L2U);
            this.L2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L2.Location = new System.Drawing.Point(283, 3);
            this.L2.Name = "L2";
            this.L2.Size = new System.Drawing.Size(134, 60);
            this.L2.TabIndex = 2;
            this.L2.Tag = "2";
            this.L2.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L2T
            // 
            this.L2T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L2T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L2T.Location = new System.Drawing.Point(0, 19);
            this.L2T.Name = "L2T";
            this.L2T.Size = new System.Drawing.Size(134, 22);
            this.L2T.TabIndex = 0;
            this.L2T.Tag = "2";
            this.L2T.Text = "Title";
            this.L2T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L2T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L2U
            // 
            this.L2U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L2U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L2U.Location = new System.Drawing.Point(0, 41);
            this.L2U.Name = "L2U";
            this.L2U.Size = new System.Drawing.Size(134, 19);
            this.L2U.TabIndex = 1;
            this.L2U.Tag = "2";
            this.L2U.Text = "Title";
            this.L2U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L2U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L1
            // 
            this.L1.Controls.Add(this.L1T);
            this.L1.Controls.Add(this.L1U);
            this.L1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L1.Location = new System.Drawing.Point(143, 3);
            this.L1.Name = "L1";
            this.L1.Size = new System.Drawing.Size(134, 60);
            this.L1.TabIndex = 1;
            this.L1.Tag = "1";
            this.L1.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L1T
            // 
            this.L1T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L1T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L1T.Location = new System.Drawing.Point(0, 19);
            this.L1T.Name = "L1T";
            this.L1T.Size = new System.Drawing.Size(134, 22);
            this.L1T.TabIndex = 0;
            this.L1T.Tag = "1";
            this.L1T.Text = "Title";
            this.L1T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L1T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L1U
            // 
            this.L1U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L1U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L1U.Location = new System.Drawing.Point(0, 41);
            this.L1U.Name = "L1U";
            this.L1U.Size = new System.Drawing.Size(134, 19);
            this.L1U.TabIndex = 1;
            this.L1U.Tag = "1";
            this.L1U.Text = "Title";
            this.L1U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L1U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L0
            // 
            this.L0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.L0.Controls.Add(this.L0T);
            this.L0.Controls.Add(this.L0U);
            this.L0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L0.Location = new System.Drawing.Point(3, 3);
            this.L0.Name = "L0";
            this.L0.Size = new System.Drawing.Size(134, 60);
            this.L0.TabIndex = 0;
            this.L0.Tag = "0";
            this.L0.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L0T
            // 
            this.L0T.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L0T.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.L0T.Location = new System.Drawing.Point(0, 17);
            this.L0T.Name = "L0T";
            this.L0T.Size = new System.Drawing.Size(132, 22);
            this.L0T.TabIndex = 0;
            this.L0T.Tag = "0";
            this.L0T.Text = "Title";
            this.L0T.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L0T.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // L0U
            // 
            this.L0U.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.L0U.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.L0U.Location = new System.Drawing.Point(0, 39);
            this.L0U.Name = "L0U";
            this.L0U.Size = new System.Drawing.Size(132, 19);
            this.L0U.TabIndex = 1;
            this.L0U.Tag = "0";
            this.L0U.Text = "Title";
            this.L0U.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.L0U.Click += new System.EventHandler(this.siteItem_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btClear.DrawImage = true;
            this.btClear.FlatAppearance.BorderSize = 0;
            this.btClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClear.Location = new System.Drawing.Point(19, 118);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(704, 23);
            this.btClear.TabIndex = 51;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // lbNTUrl
            // 
            this.lbNTUrl.AutoSize = true;
            this.lbNTUrl.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbNTUrl.Location = new System.Drawing.Point(15, 86);
            this.lbNTUrl.Name = "lbNTUrl";
            this.lbNTUrl.Size = new System.Drawing.Size(33, 19);
            this.lbNTUrl.TabIndex = 49;
            this.lbNTUrl.Text = "Url:";
            // 
            // lbNTTitle
            // 
            this.lbNTTitle.AutoSize = true;
            this.lbNTTitle.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbNTTitle.Location = new System.Drawing.Point(15, 51);
            this.lbNTTitle.Name = "lbNTTitle";
            this.lbNTTitle.Size = new System.Drawing.Size(44, 19);
            this.lbNTTitle.TabIndex = 48;
            this.lbNTTitle.Text = "Title:";
            // 
            // btNewTabBack
            // 
            this.btNewTabBack.BackColor = System.Drawing.Color.Transparent;
            this.btNewTabBack.ButtonImage = global::Korot.Properties.Resources.leftarrow;
            this.btNewTabBack.ContextMenuStrip = this.cmsBack;
            this.btNewTabBack.DrawImage = true;
            this.btNewTabBack.FlatAppearance.BorderSize = 0;
            this.btNewTabBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNewTabBack.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btNewTabBack.Location = new System.Drawing.Point(13, 8);
            this.btNewTabBack.Name = "btNewTabBack";
            this.btNewTabBack.Size = new System.Drawing.Size(30, 28);
            this.btNewTabBack.TabIndex = 47;
            this.btNewTabBack.UseVisualStyleBackColor = false;
            this.btNewTabBack.Click += new System.EventHandler(this.htButton1_Click);
            // 
            // btClose5
            // 
            this.btClose5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose5.BackColor = System.Drawing.Color.Transparent;
            this.btClose5.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose5.DrawImage = true;
            this.btClose5.FlatAppearance.BorderSize = 0;
            this.btClose5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose5.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose5.Location = new System.Drawing.Point(693, 6);
            this.btClose5.Name = "btClose5";
            this.btClose5.Size = new System.Drawing.Size(30, 30);
            this.btClose5.TabIndex = 45;
            this.btClose5.UseVisualStyleBackColor = false;
            this.btClose5.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbNewTabTitle
            // 
            this.lbNewTabTitle.AutoSize = true;
            this.lbNewTabTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbNewTabTitle.Location = new System.Drawing.Point(48, 9);
            this.lbNewTabTitle.Name = "lbNewTabTitle";
            this.lbNewTabTitle.Size = new System.Drawing.Size(216, 25);
            this.lbNewTabTitle.TabIndex = 46;
            this.lbNewTabTitle.Text = "New Tab Page Content";
            // 
            // tpBlock
            // 
            this.tpBlock.Controls.Add(this.pBlock);
            this.tpBlock.Controls.Add(this.btBlockBack);
            this.tpBlock.Controls.Add(this.btClose4);
            this.tpBlock.Controls.Add(this.lbBlockedSites);
            this.tpBlock.Location = new System.Drawing.Point(4, 24);
            this.tpBlock.Name = "tpBlock";
            this.tpBlock.Size = new System.Drawing.Size(738, 608);
            this.tpBlock.TabIndex = 11;
            this.tpBlock.Text = "Blocked Sites";
            this.tpBlock.UseVisualStyleBackColor = true;
            // 
            // pBlock
            // 
            this.pBlock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBlock.Location = new System.Drawing.Point(15, 40);
            this.pBlock.Name = "pBlock";
            this.pBlock.Size = new System.Drawing.Size(708, 553);
            this.pBlock.TabIndex = 48;
            // 
            // btBlockBack
            // 
            this.btBlockBack.BackColor = System.Drawing.Color.Transparent;
            this.btBlockBack.ButtonImage = global::Korot.Properties.Resources.leftarrow;
            this.btBlockBack.ContextMenuStrip = this.cmsBack;
            this.btBlockBack.DrawImage = true;
            this.btBlockBack.FlatAppearance.BorderSize = 0;
            this.btBlockBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBlockBack.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btBlockBack.Location = new System.Drawing.Point(15, 5);
            this.btBlockBack.Name = "btBlockBack";
            this.btBlockBack.Size = new System.Drawing.Size(30, 28);
            this.btBlockBack.TabIndex = 47;
            this.btBlockBack.UseVisualStyleBackColor = false;
            this.btBlockBack.Click += new System.EventHandler(this.btBlockBack_Click);
            // 
            // btClose4
            // 
            this.btClose4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose4.BackColor = System.Drawing.Color.Transparent;
            this.btClose4.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btClose4.DrawImage = true;
            this.btClose4.FlatAppearance.BorderSize = 0;
            this.btClose4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose4.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btClose4.Location = new System.Drawing.Point(693, 6);
            this.btClose4.Name = "btClose4";
            this.btClose4.Size = new System.Drawing.Size(30, 30);
            this.btClose4.TabIndex = 45;
            this.btClose4.UseVisualStyleBackColor = false;
            this.btClose4.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbBlockedSites
            // 
            this.lbBlockedSites.AutoSize = true;
            this.lbBlockedSites.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbBlockedSites.Location = new System.Drawing.Point(49, 8);
            this.lbBlockedSites.Name = "lbBlockedSites";
            this.lbBlockedSites.Size = new System.Drawing.Size(131, 25);
            this.lbBlockedSites.TabIndex = 46;
            this.lbBlockedSites.Text = "Blocked Sites";
            // 
            // cmsSearchEngine
            // 
            this.cmsSearchEngine.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleToolStripMenuItem,
            this.yandexToolStripMenuItem,
            this.bingToolStripMenuItem,
            this.yaaniToolStripMenuItem,
            this.duckDuckGoToolStripMenuItem,
            this.baiduToolStripMenuItem,
            this.wolframalphaToolStripMenuItem,
            this.aOLToolStripMenuItem,
            this.yahooToolStripMenuItem,
            this.askToolStripMenuItem,
            this.ınternetArchiveToolStripMenuItem,
            this.customToolStripMenuItem});
            this.cmsSearchEngine.Name = "contextMenuStrip2";
            this.cmsSearchEngine.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsSearchEngine.ShowImageMargin = false;
            this.cmsSearchEngine.Size = new System.Drawing.Size(134, 268);
            // 
            // googleToolStripMenuItem
            // 
            this.googleToolStripMenuItem.Name = "googleToolStripMenuItem";
            this.googleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.googleToolStripMenuItem.Tag = "https://www.google.com/search?q=";
            this.googleToolStripMenuItem.Text = "Google";
            this.googleToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // yandexToolStripMenuItem
            // 
            this.yandexToolStripMenuItem.Name = "yandexToolStripMenuItem";
            this.yandexToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.yandexToolStripMenuItem.Tag = "https://yandex.com.tr/search/?lr=103873&text=";
            this.yandexToolStripMenuItem.Text = "Yandex";
            this.yandexToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // bingToolStripMenuItem
            // 
            this.bingToolStripMenuItem.Name = "bingToolStripMenuItem";
            this.bingToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.bingToolStripMenuItem.Tag = "https://www.bing.com/search?q=";
            this.bingToolStripMenuItem.Text = "Bing";
            this.bingToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // yaaniToolStripMenuItem
            // 
            this.yaaniToolStripMenuItem.Name = "yaaniToolStripMenuItem";
            this.yaaniToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.yaaniToolStripMenuItem.Tag = "https://www.yaani.com/#q=";
            this.yaaniToolStripMenuItem.Text = "Yaani";
            this.yaaniToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // duckDuckGoToolStripMenuItem
            // 
            this.duckDuckGoToolStripMenuItem.Name = "duckDuckGoToolStripMenuItem";
            this.duckDuckGoToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.duckDuckGoToolStripMenuItem.Tag = "https://duckduckgo.com/?q=";
            this.duckDuckGoToolStripMenuItem.Text = "DuckDuckGo";
            this.duckDuckGoToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // baiduToolStripMenuItem
            // 
            this.baiduToolStripMenuItem.Name = "baiduToolStripMenuItem";
            this.baiduToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.baiduToolStripMenuItem.Tag = "https://www.baidu.com/s?&wd=";
            this.baiduToolStripMenuItem.Text = "Baidu";
            this.baiduToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // wolframalphaToolStripMenuItem
            // 
            this.wolframalphaToolStripMenuItem.Name = "wolframalphaToolStripMenuItem";
            this.wolframalphaToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.wolframalphaToolStripMenuItem.Tag = "https://www.wolframalpha.com/input/?i=";
            this.wolframalphaToolStripMenuItem.Text = "Wolframalpha";
            this.wolframalphaToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // aOLToolStripMenuItem
            // 
            this.aOLToolStripMenuItem.Name = "aOLToolStripMenuItem";
            this.aOLToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.aOLToolStripMenuItem.Tag = "https://search.aol.com/aol/search?&q=";
            this.aOLToolStripMenuItem.Text = "AOL";
            this.aOLToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // yahooToolStripMenuItem
            // 
            this.yahooToolStripMenuItem.Name = "yahooToolStripMenuItem";
            this.yahooToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.yahooToolStripMenuItem.Tag = "https://search.yahoo.com/search?p=";
            this.yahooToolStripMenuItem.Text = "Yahoo";
            this.yahooToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // askToolStripMenuItem
            // 
            this.askToolStripMenuItem.Name = "askToolStripMenuItem";
            this.askToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.askToolStripMenuItem.Tag = "https://www.ask.com/web?q=";
            this.askToolStripMenuItem.Text = "Ask";
            this.askToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // ınternetArchiveToolStripMenuItem
            // 
            this.ınternetArchiveToolStripMenuItem.Name = "ınternetArchiveToolStripMenuItem";
            this.ınternetArchiveToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.ınternetArchiveToolStripMenuItem.Tag = "https://web.archive.org/web/*/";
            this.ınternetArchiveToolStripMenuItem.Text = "Internet Archive";
            this.ınternetArchiveToolStripMenuItem.Click += new System.EventHandler(this.SearchEngineSelection_Click);
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.customToolStripMenuItem.Text = "Custom";
            this.customToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
            // 
            // cmsBStyle
            // 
            this.cmsBStyle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colorToolStripMenuItem,
            this.ımageFromLocalFileToolStripMenuItem,
            this.ımageFromURLToolStripMenuItem});
            this.cmsBStyle.Name = "contextMenuStrip1";
            this.cmsBStyle.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsBStyle.ShowCheckMargin = true;
            this.cmsBStyle.ShowImageMargin = false;
            this.cmsBStyle.Size = new System.Drawing.Size(189, 70);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.CheckOnClick = true;
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.colorToolStripMenuItem.Text = "Color";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.ColorToolStripMenuItem_Click);
            // 
            // ımageFromLocalFileToolStripMenuItem
            // 
            this.ımageFromLocalFileToolStripMenuItem.Name = "ımageFromLocalFileToolStripMenuItem";
            this.ımageFromLocalFileToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ımageFromLocalFileToolStripMenuItem.Text = "Image from Local File";
            this.ımageFromLocalFileToolStripMenuItem.Click += new System.EventHandler(this.FromLocalFileToolStripMenuItem_Click);
            // 
            // ımageFromURLToolStripMenuItem
            // 
            this.ımageFromURLToolStripMenuItem.Name = "ımageFromURLToolStripMenuItem";
            this.ımageFromURLToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ımageFromURLToolStripMenuItem.Text = "Image from Code";
            this.ımageFromURLToolStripMenuItem.Click += new System.EventHandler(this.FromURLToolStripMenuItem_Click);
            // 
            // cmsStartup
            // 
            this.cmsStartup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showNewTabPageToolStripMenuItem,
            this.showHomepageToolStripMenuItem,
            this.showAWebsiteToolStripMenuItem});
            this.cmsStartup.Name = "cmsStartup";
            this.cmsStartup.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsStartup.ShowImageMargin = false;
            this.cmsStartup.Size = new System.Drawing.Size(156, 70);
            // 
            // showNewTabPageToolStripMenuItem
            // 
            this.showNewTabPageToolStripMenuItem.Name = "showNewTabPageToolStripMenuItem";
            this.showNewTabPageToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.showNewTabPageToolStripMenuItem.Text = "Show New Tab page";
            this.showNewTabPageToolStripMenuItem.Click += new System.EventHandler(this.showNewTabPageToolStripMenuItem_Click);
            // 
            // showHomepageToolStripMenuItem
            // 
            this.showHomepageToolStripMenuItem.Name = "showHomepageToolStripMenuItem";
            this.showHomepageToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.showHomepageToolStripMenuItem.Text = "Show homepage";
            this.showHomepageToolStripMenuItem.Click += new System.EventHandler(this.showHomepageToolStripMenuItem_Click);
            // 
            // showAWebsiteToolStripMenuItem
            // 
            this.showAWebsiteToolStripMenuItem.Name = "showAWebsiteToolStripMenuItem";
            this.showAWebsiteToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.showAWebsiteToolStripMenuItem.Text = "Show a website";
            this.showAWebsiteToolStripMenuItem.Click += new System.EventHandler(this.showAWebsiteToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.tmrNotifListener_Tick);
            // 
            // frmCEF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(732, 661);
            this.Controls.Add(this.pNavigate);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mFavorites;
            this.MinimumSize = new System.Drawing.Size(350, 258);
            this.Name = "frmCEF";
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCEF_FormClosing);
            this.Load += new System.EventHandler(this.tabform_Load);
            this.SizeChanged += new System.EventHandler(this.FrmCEF_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmCEF_KeyUp);
            this.Resize += new System.EventHandler(this.FrmCEF_SizeChanged);
            this.pNavigate.ResumeLayout(false);
            this.pNavigate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrivacy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncognito)).EndInit();
            this.cmsFavorite.ResumeLayout(false);
            this.pnlCert.ResumeLayout(false);
            this.pnlCert.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpCef.ResumeLayout(false);
            this.pCEF.ResumeLayout(false);
            this.tpCert.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.tpTheme.ResumeLayout(false);
            this.tpTheme.PerformLayout();
            this.flpClose.ResumeLayout(false);
            this.flpClose.PerformLayout();
            this.flpNewTab.ResumeLayout(false);
            this.flpNewTab.PerformLayout();
            this.flpLayout.ResumeLayout(false);
            this.flpLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbStore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOverlay)).EndInit();
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            this.tpDownload.ResumeLayout(false);
            this.tpDownload.PerformLayout();
            this.tpAbout.ResumeLayout(false);
            this.tpAbout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.tpSite.ResumeLayout(false);
            this.tpSite.PerformLayout();
            this.tpCollection.ResumeLayout(false);
            this.tpCollection.PerformLayout();
            this.tpNotification.ResumeLayout(false);
            this.tpNotification.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flpFrom.ResumeLayout(false);
            this.flpFrom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fromHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromMin)).EndInit();
            this.flpEvery.ResumeLayout(false);
            this.flpEvery.PerformLayout();
            this.flpTo.ResumeLayout(false);
            this.flpTo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toMin)).EndInit();
            this.tpNewTab.ResumeLayout(false);
            this.tpNewTab.PerformLayout();
            this.tlpNewTab.ResumeLayout(false);
            this.L9.ResumeLayout(false);
            this.L8.ResumeLayout(false);
            this.L7.ResumeLayout(false);
            this.L6.ResumeLayout(false);
            this.L5.ResumeLayout(false);
            this.L4.ResumeLayout(false);
            this.L3.ResumeLayout(false);
            this.L2.ResumeLayout(false);
            this.L1.ResumeLayout(false);
            this.L0.ResumeLayout(false);
            this.tpBlock.ResumeLayout(false);
            this.tpBlock.PerformLayout();
            this.cmsSearchEngine.ResumeLayout(false);
            this.cmsBStyle.ResumeLayout(false);
            this.cmsStartup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public HTAlt.WinForms.HTButton btBack;
        public HTAlt.WinForms.HTButton btRefresh;
        public HTAlt.WinForms.HTButton btNext;
        public System.Windows.Forms.TextBox tbAddress;
        public HTAlt.WinForms.HTButton btHome;
        private System.Windows.Forms.PictureBox pbIncognito;
        private System.Windows.Forms.Panel pNavigate;
        private System.Windows.Forms.Label lErrorTitle2Text;
        private System.Windows.Forms.Label lErrorTitle1Text;
        private System.Windows.Forms.Label lErrorTitle2;
        private System.Windows.Forms.Label lErrorTitle1;
        private System.Windows.Forms.Label lErrorTitle;
        private System.Windows.Forms.Timer tmrFaster;
        private System.Windows.Forms.MenuStrip mFavorites;
        public HTAlt.WinForms.HTButton btFav;
        public HTAlt.WinForms.HTButton btProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pbProgress;
        public System.Windows.Forms.PictureBox pbPrivacy;
        public System.Windows.Forms.Panel pnlCert;
        public System.Windows.Forms.Label lbCertErrorInfo;
        public System.Windows.Forms.Label lbCertErrorTitle;
        public HTAlt.WinForms.HTButton btHamburger;
        private System.Windows.Forms.TabPage tpCef;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.RadioButton rbNewTab;
        private System.Windows.Forms.Label lbHomepage;
        private System.Windows.Forms.Label lbSearchEngine;
        private System.Windows.Forms.TextBox tbSearchEngine;
        private System.Windows.Forms.TextBox tbHomepage;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ListBox listBox2;
        public System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbBackImage;
        private System.Windows.Forms.Label lbThemes;
        private System.Windows.Forms.Label lbThemeName;
        private System.Windows.Forms.Label lbBackColor;
        private System.Windows.Forms.Label lbOveralColor;
        private System.Windows.Forms.PictureBox pbOverlay;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.TabPage tpAbout;
        private HTAlt.WinForms.HTButton btInstall;
        private HTAlt.WinForms.HTButton btUpdater;
        private System.Windows.Forms.Label lbUpdateStatus;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbKorot;
        private System.Windows.Forms.ContextMenuStrip cmsSearchEngine;
        private System.Windows.Forms.ToolStripMenuItem googleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yandexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duckDuckGoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baiduToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wolframalphaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aOLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yahooToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem askToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsBStyle;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.Label lbSettings;
        public HTAlt.WinForms.HTButton btClose;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.LinkLabel llLicenses;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ToolStripMenuItem ınternetArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaaniToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms4;
        private System.Windows.Forms.Label lbDNT;
        private HTAlt.WinForms.HTSwitch hsDoNotTrack;
        private System.Windows.Forms.ToolStripMenuItem ımageFromURLToolStripMenuItem;
        private System.Windows.Forms.Label lbBackImageStyle;
        private System.Windows.Forms.ContextMenuStrip cmsFavorite;
        private System.Windows.Forms.ToolStripMenuItem tsopenInNewTab;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedTSMI;
        private System.Windows.Forms.ToolStripMenuItem clearTSMI;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.TabPage tpCert;
        private System.Windows.Forms.TabPage tpHistory;
        public HTAlt.WinForms.HTButton btClose6;
        private System.Windows.Forms.Label lbHistory;
        private System.Windows.Forms.TabPage tpDownload;
        public HTAlt.WinForms.HTButton btClose7;
        private System.Windows.Forms.Label lbDownloads;
        private System.Windows.Forms.Label lbStatus;
        public HTAlt.WinForms.HTButton btClose10;
        private System.Windows.Forms.Label lbAbout;
        private System.Windows.Forms.Label lbTheme;
        private System.Windows.Forms.ToolStripMenuItem ımageFromLocalFileToolStripMenuItem;
        private System.Windows.Forms.TabPage tpTheme;
        public HTAlt.WinForms.HTButton btClose2;
        private System.Windows.Forms.Label lbLastProxy;
        private HTAlt.WinForms.HTSwitch hsProxy;
        private HTAlt.WinForms.HTButton btCookie;
        private System.Windows.Forms.TabPage tpSite;
        public HTAlt.WinForms.HTButton btClose8;
        private System.Windows.Forms.Label lbSiteSettings;
        private System.Windows.Forms.Label lbAtStartup;
        private System.Windows.Forms.TextBox tbStartup;
        private HTAlt.WinForms.HTButton btDownloadFolder;
        private System.Windows.Forms.Label lbAutoDownload;
        private HTAlt.WinForms.HTSwitch hsDownload;
        private System.Windows.Forms.Label lbDownloadFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.ContextMenuStrip cmsStartup;
        private System.Windows.Forms.ToolStripMenuItem showNewTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHomepageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAWebsiteToolStripMenuItem;
        private HTAlt.WinForms.HTButton btReset;
        private System.Windows.Forms.Label lbCloseColor;
        private System.Windows.Forms.Label lbNewTabColor;
        private System.Windows.Forms.Label lbShowFavorites;
        private HTAlt.WinForms.HTSwitch hsFav;
        private HTAlt.WinForms.HTButton btCleanLog;
        private HTAlt.WinForms.HTSwitch hsOpen;
        private System.Windows.Forms.Label lbOpen;
        private System.Windows.Forms.PictureBox pbStore;
        private System.Windows.Forms.ToolStripSeparator tsSepFav;
        private System.Windows.Forms.ToolStripMenuItem newFavoriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem openİnNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openİnNewIncognitoWindowToolStripMenuItem;
        private System.Windows.Forms.Panel pCEF;
        private System.Windows.Forms.ContextMenuStrip cmsBack;
        private System.Windows.Forms.ContextMenuStrip cmsForward;
        public System.Windows.Forms.ListBox lbURL;
        public System.Windows.Forms.ListBox lbTitle;
        private System.Windows.Forms.Label lbautoRestore;
        private HTAlt.WinForms.HTSwitch hsAutoRestore;
        private System.Windows.Forms.TabPage tpCollection;
        private System.Windows.Forms.Panel panel3;
        public HTAlt.WinForms.HTButton btClose9;
        private System.Windows.Forms.Label lbCollections;
        private System.Windows.Forms.FlowLayoutPanel flpClose;
        private System.Windows.Forms.RadioButton rbBackColor1;
        private System.Windows.Forms.RadioButton rbForeColor1;
        private System.Windows.Forms.RadioButton rbOverlayColor1;
        private System.Windows.Forms.FlowLayoutPanel flpNewTab;
        private System.Windows.Forms.RadioButton rbBackColor;
        private System.Windows.Forms.RadioButton rbForeColor;
        private System.Windows.Forms.RadioButton rbOverlayColor;
        private System.Windows.Forms.FlowLayoutPanel flpLayout;
        private System.Windows.Forms.RadioButton rbNone;
        private System.Windows.Forms.RadioButton rbTile;
        private System.Windows.Forms.RadioButton rbCenter;
        private System.Windows.Forms.RadioButton rbStretch;
        private System.Windows.Forms.RadioButton rbZoom;
        private System.Windows.Forms.TabPage tpNotification;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpFrom;
        private System.Windows.Forms.NumericUpDown fromHour;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.NumericUpDown fromMin;
        private System.Windows.Forms.FlowLayoutPanel flpEvery;
        private System.Windows.Forms.Label lbSunday;
        private System.Windows.Forms.Label lbMonday;
        private System.Windows.Forms.Label lbTuesday;
        private System.Windows.Forms.Label lbWednesday;
        private System.Windows.Forms.Label lbThursday;
        private System.Windows.Forms.Label lbFriday;
        private System.Windows.Forms.Label lbSaturday;
        private System.Windows.Forms.Label scheduleFrom;
        private System.Windows.Forms.FlowLayoutPanel flpTo;
        private System.Windows.Forms.NumericUpDown toHour;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown toMin;
        private System.Windows.Forms.Label scheduleEvery;
        private System.Windows.Forms.Label scheduleTo;
        private System.Windows.Forms.Label lbSchedule;
        private System.Windows.Forms.Label lbSilentMode;
        private HTAlt.WinForms.HTSwitch hsSchedule;
        private HTAlt.WinForms.HTSwitch hsSilent;
        private System.Windows.Forms.Label lbPlayNotifSound;
        private HTAlt.WinForms.HTSwitch hsNotificationSound;
        public HTAlt.WinForms.HTButton btClose3;
        private System.Windows.Forms.Label lbNotifSetting;
        private HTAlt.WinForms.HTButton btNotification;
        private System.Windows.Forms.Label lb24HType;
        public HTAlt.WinForms.HTButton btNotifBack;
        public HTAlt.WinForms.HTButton btCookieBack;
        public HTAlt.WinForms.HTButton btCertError;
        private HTAlt.WinForms.HTButton button12;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pSite;
        private System.Windows.Forms.Label lbFlash;
        private HTAlt.WinForms.HTSwitch hsFlash;
        private System.Windows.Forms.Label lbFlashInfo;
        private System.Windows.Forms.Panel pHisMan;
        private System.Windows.Forms.Panel pDowMan;
        private System.Windows.Forms.TabPage tpNewTab;
        private System.Windows.Forms.Label lbNTUrl;
        private System.Windows.Forms.Label lbNTTitle;
        public HTAlt.WinForms.HTButton btNewTabBack;
        public HTAlt.WinForms.HTButton btClose5;
        private System.Windows.Forms.Label lbNewTabTitle;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TableLayoutPanel tlpNewTab;
        private HTAlt.WinForms.HTButton btClear;
        private System.Windows.Forms.Panel L9;
        private System.Windows.Forms.Label L9T;
        private System.Windows.Forms.Label L9U;
        private System.Windows.Forms.Panel L8;
        private System.Windows.Forms.Label L8T;
        private System.Windows.Forms.Label L8U;
        private System.Windows.Forms.Panel L7;
        private System.Windows.Forms.Label L7T;
        private System.Windows.Forms.Label L7U;
        private System.Windows.Forms.Panel L6;
        private System.Windows.Forms.Label L6T;
        private System.Windows.Forms.Label L6U;
        private System.Windows.Forms.Panel L5;
        private System.Windows.Forms.Label L5T;
        private System.Windows.Forms.Label L5U;
        private System.Windows.Forms.Panel L4;
        private System.Windows.Forms.Label L4T;
        private System.Windows.Forms.Label L4U;
        private System.Windows.Forms.Panel L3;
        private System.Windows.Forms.Label L3T;
        private System.Windows.Forms.Label L3U;
        private System.Windows.Forms.Panel L2;
        private System.Windows.Forms.Label L2T;
        private System.Windows.Forms.Label L2U;
        private System.Windows.Forms.Panel L1;
        private System.Windows.Forms.Label L1T;
        private System.Windows.Forms.Label L1U;
        private System.Windows.Forms.Panel L0;
        private System.Windows.Forms.Label L0T;
        private System.Windows.Forms.Label L0U;
        private HTAlt.WinForms.HTButton btNewTab;
        private System.Windows.Forms.Label label2;
        private HTAlt.WinForms.HTButton btLangStore;
        private HTAlt.WinForms.HTButton btlangFolder;
        private System.Windows.Forms.ComboBox cbLang;
        private System.Windows.Forms.TabPage tpBlock;
        public HTAlt.WinForms.HTButton btBlockBack;
        public HTAlt.WinForms.HTButton btClose4;
        private System.Windows.Forms.Label lbBlockedSites;
        private HTAlt.WinForms.HTButton btBlocked;
        private System.Windows.Forms.Panel pBlock;
        private HTAlt.WinForms.HTButton btThemeWizard;
    }
}