/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

namespace Korot
{
    partial class frmSettings
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
            this.lbTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.pCleanDownload = new System.Windows.Forms.Panel();
            this.nudCDOld = new System.Windows.Forms.NumericUpDown();
            this.nudCDDay = new System.Windows.Forms.NumericUpDown();
            this.nudCDFile = new System.Windows.Forms.NumericUpDown();
            this.lbCD6 = new System.Windows.Forms.Label();
            this.lbCD4 = new System.Windows.Forms.Label();
            this.lbCD2 = new System.Windows.Forms.Label();
            this.lbCD5 = new System.Windows.Forms.Label();
            this.lbCD3 = new System.Windows.Forms.Label();
            this.hsCDOld = new HTAlt.WinForms.HTSwitch();
            this.lbCD1 = new System.Windows.Forms.Label();
            this.hsCDDay = new HTAlt.WinForms.HTSwitch();
            this.hsCDFile = new HTAlt.WinForms.HTSwitch();
            this.lbCleanDownload = new System.Windows.Forms.Label();
            this.hsCleanDownload = new HTAlt.WinForms.HTSwitch();
            this.pCleanLog = new System.Windows.Forms.Panel();
            this.nudCLOld = new System.Windows.Forms.NumericUpDown();
            this.nudCLDay = new System.Windows.Forms.NumericUpDown();
            this.nudCLFile = new System.Windows.Forms.NumericUpDown();
            this.lbCL4 = new System.Windows.Forms.Label();
            this.lbCL6 = new System.Windows.Forms.Label();
            this.lbCL2 = new System.Windows.Forms.Label();
            this.lbCL3 = new System.Windows.Forms.Label();
            this.lbCL1 = new System.Windows.Forms.Label();
            this.lbCL5 = new System.Windows.Forms.Label();
            this.hsCLDay = new HTAlt.WinForms.HTSwitch();
            this.hsCLFile = new HTAlt.WinForms.HTSwitch();
            this.hsCLOld = new HTAlt.WinForms.HTSwitch();
            this.lbCleanLog = new System.Windows.Forms.Label();
            this.hsCleanLog = new HTAlt.WinForms.HTSwitch();
            this.btClean = new HTAlt.WinForms.HTButton();
            this.pCleanHistory = new System.Windows.Forms.Panel();
            this.nudCHOld = new System.Windows.Forms.NumericUpDown();
            this.nudCHDay = new System.Windows.Forms.NumericUpDown();
            this.nudCHFile = new System.Windows.Forms.NumericUpDown();
            this.lbCH4 = new System.Windows.Forms.Label();
            this.lbCH2 = new System.Windows.Forms.Label();
            this.lbCH6 = new System.Windows.Forms.Label();
            this.lbCH3 = new System.Windows.Forms.Label();
            this.lbCH1 = new System.Windows.Forms.Label();
            this.hsCHDay = new HTAlt.WinForms.HTSwitch();
            this.hsCHFile = new HTAlt.WinForms.HTSwitch();
            this.lbCH5 = new System.Windows.Forms.Label();
            this.hsCHOld = new HTAlt.WinForms.HTSwitch();
            this.pCleanCache = new System.Windows.Forms.Panel();
            this.nudCC2 = new System.Windows.Forms.NumericUpDown();
            this.nudCC1 = new System.Windows.Forms.NumericUpDown();
            this.lbCC4 = new System.Windows.Forms.Label();
            this.lbCC2 = new System.Windows.Forms.Label();
            this.lbCC3 = new System.Windows.Forms.Label();
            this.lbCC1 = new System.Windows.Forms.Label();
            this.hsCC2 = new HTAlt.WinForms.HTSwitch();
            this.hsCC1 = new HTAlt.WinForms.HTSwitch();
            this.tbTheme = new System.Windows.Forms.TextBox();
            this.pSchedule = new System.Windows.Forms.Panel();
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
            this.btThemeWizard = new HTAlt.WinForms.HTButton();
            this.btBlocked = new HTAlt.WinForms.HTButton();
            this.btCookie = new HTAlt.WinForms.HTButton();
            this.hsAutoForeColor = new HTAlt.WinForms.HTSwitch();
            this.lbAutoSelect = new System.Windows.Forms.Label();
            this.hsNinja = new HTAlt.WinForms.HTSwitch();
            this.lbNinja = new System.Windows.Forms.Label();
            this.pbForeColor = new System.Windows.Forms.PictureBox();
            this.lbForeColor = new System.Windows.Forms.Label();
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
            this.lbBackImageStyle = new System.Windows.Forms.Label();
            this.lbCloseColor = new System.Windows.Forms.Label();
            this.lbNewTabColor = new System.Windows.Forms.Label();
            this.lbBackImage = new System.Windows.Forms.Label();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.pbOverlay = new System.Windows.Forms.PictureBox();
            this.lbBackColor = new System.Windows.Forms.Label();
            this.lbOveralColor = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.lbThemeName = new System.Windows.Forms.Label();
            this.lbLayout = new System.Windows.Forms.Label();
            this.lbTheme = new System.Windows.Forms.Label();
            this.btOpenSound = new HTAlt.WinForms.HTButton();
            this.tbSoundLoc = new System.Windows.Forms.TextBox();
            this.lbSchedule = new System.Windows.Forms.Label();
            this.lbSilentMode = new System.Windows.Forms.Label();
            this.hsSchedule = new HTAlt.WinForms.HTSwitch();
            this.hsSilent = new HTAlt.WinForms.HTSwitch();
            this.lbDefaultNotifSound = new System.Windows.Forms.Label();
            this.hsDefaultSound = new HTAlt.WinForms.HTSwitch();
            this.lbCleanHistory = new System.Windows.Forms.Label();
            this.lbCleanCache = new System.Windows.Forms.Label();
            this.hsCleanHistory = new HTAlt.WinForms.HTSwitch();
            this.lbPlayNotifSound = new System.Windows.Forms.Label();
            this.hsCleanCache = new HTAlt.WinForms.HTSwitch();
            this.hsNotificationSound = new HTAlt.WinForms.HTSwitch();
            this.lbAutoClean = new System.Windows.Forms.Label();
            this.lbNotifSetting = new System.Windows.Forms.Label();
            this.lbDownloads = new System.Windows.Forms.Label();
            this.lbGeneral = new System.Windows.Forms.Label();
            this.hsOpen = new HTAlt.WinForms.HTSwitch();
            this.hsDownload = new HTAlt.WinForms.HTSwitch();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.btDownloadFolder = new HTAlt.WinForms.HTButton();
            this.lbDownloadFolder = new System.Windows.Forms.Label();
            this.lbOpen = new System.Windows.Forms.Label();
            this.lbAutoDownload = new System.Windows.Forms.Label();
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
            this.lbNewTabTitle = new System.Windows.Forms.Label();
            this.lbLang = new System.Windows.Forms.Label();
            this.lbFlashInfo = new System.Windows.Forms.Label();
            this.lbFlash = new System.Windows.Forms.Label();
            this.hsFlash = new HTAlt.WinForms.HTSwitch();
            this.lbDNT = new System.Windows.Forms.Label();
            this.hsDoNotTrack = new HTAlt.WinForms.HTSwitch();
            this.lbautoRestore = new System.Windows.Forms.Label();
            this.lbLastProxy = new System.Windows.Forms.Label();
            this.hsAutoRestore = new HTAlt.WinForms.HTSwitch();
            this.hsProxy = new HTAlt.WinForms.HTSwitch();
            this.lbAtStartup = new System.Windows.Forms.Label();
            this.lbShowFavorites = new System.Windows.Forms.Label();
            this.hsFav = new HTAlt.WinForms.HTSwitch();
            this.rbNewTab = new System.Windows.Forms.RadioButton();
            this.lbHomepage = new System.Windows.Forms.Label();
            this.lbSearchEngine = new System.Windows.Forms.Label();
            this.tbHomepage = new System.Windows.Forms.TextBox();
            this.tbLang = new System.Windows.Forms.TextBox();
            this.tbStartup = new System.Windows.Forms.TextBox();
            this.tbSearchEngine = new System.Windows.Forms.TextBox();
            this.tpBlock = new System.Windows.Forms.TabPage();
            this.tpSite = new System.Windows.Forms.TabPage();
            this.tpSchedules = new System.Windows.Forms.TabPage();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.tpDownloads = new System.Windows.Forms.TabPage();
            this.tpCollections = new System.Windows.Forms.TabPage();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.p32bit = new System.Windows.Forms.FlowLayoutPanel();
            this.l32title = new System.Windows.Forms.Label();
            this.l32desc = new System.Windows.Forms.Label();
            this.ll32bit = new System.Windows.Forms.LinkLabel();
            this.btReset = new HTAlt.WinForms.HTButton();
            this.lbUpdateStatus = new System.Windows.Forms.Label();
            this.btUpdater = new HTAlt.WinForms.HTButton();
            this.llLicenses = new System.Windows.Forms.LinkLabel();
            this.label21 = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbKorot = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pTitle = new System.Windows.Forms.Panel();
            this.btBack = new HTAlt.WinForms.HTButton();
            this.btClose = new HTAlt.WinForms.HTButton();
            this.pSidebar = new System.Windows.Forms.Panel();
            this.lbAbout = new System.Windows.Forms.Label();
            this.btSidebar = new HTAlt.WinForms.HTButton();
            this.lbCollections = new System.Windows.Forms.Label();
            this.lbDownload = new System.Windows.Forms.Label();
            this.lbHistory = new System.Windows.Forms.Label();
            this.lbSettings = new System.Windows.Forms.Label();
            this.cmsStartup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showNewTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHomepageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmsLanguage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.langTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSepLang = new System.Windows.Forms.ToolStripSeparator();
            this.LOpenTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.LWebStoreTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTheme = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.themeTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSepTheme = new System.Windows.Forms.ToolStripSeparator();
            this.TOpenTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.TWebStoreTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.lbSynthVol = new System.Windows.Forms.Label();
            this.lbSynthRate = new System.Windows.Forms.Label();
            this.nudSynthVol = new System.Windows.Forms.NumericUpDown();
            this.nudSynthRate = new System.Windows.Forms.NumericUpDown();
            this.tabControl1.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.pCleanDownload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCDOld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCDDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCDFile)).BeginInit();
            this.pCleanLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCLOld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCLDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCLFile)).BeginInit();
            this.pCleanHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCHOld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCHDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCHFile)).BeginInit();
            this.pCleanCache.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCC2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCC1)).BeginInit();
            this.pSchedule.SuspendLayout();
            this.flpFrom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fromHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromMin)).BeginInit();
            this.flpEvery.SuspendLayout();
            this.flpTo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbForeColor)).BeginInit();
            this.flpClose.SuspendLayout();
            this.flpNewTab.SuspendLayout();
            this.flpLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOverlay)).BeginInit();
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
            this.tpAbout.SuspendLayout();
            this.p32bit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.pTitle.SuspendLayout();
            this.pSidebar.SuspendLayout();
            this.cmsStartup.SuspendLayout();
            this.cmsSearchEngine.SuspendLayout();
            this.cmsBStyle.SuspendLayout();
            this.cmsLanguage.SuspendLayout();
            this.cmsTheme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSynthVol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSynthRate)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbTitle.Location = new System.Drawing.Point(50, 10);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(89, 25);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Settings";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Controls.Add(this.tpBlock);
            this.tabControl1.Controls.Add(this.tpSite);
            this.tabControl1.Controls.Add(this.tpSchedules);
            this.tabControl1.Controls.Add(this.tpHistory);
            this.tabControl1.Controls.Add(this.tpDownloads);
            this.tabControl1.Controls.Add(this.tpCollections);
            this.tabControl1.Controls.Add(this.tpAbout);
            this.tabControl1.Location = new System.Drawing.Point(137, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(605, 501);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpSettings
            // 
            this.tpSettings.AutoScroll = true;
            this.tpSettings.Controls.Add(this.nudSynthRate);
            this.tpSettings.Controls.Add(this.nudSynthVol);
            this.tpSettings.Controls.Add(this.pCleanDownload);
            this.tpSettings.Controls.Add(this.lbCleanDownload);
            this.tpSettings.Controls.Add(this.hsCleanDownload);
            this.tpSettings.Controls.Add(this.pCleanLog);
            this.tpSettings.Controls.Add(this.lbCleanLog);
            this.tpSettings.Controls.Add(this.hsCleanLog);
            this.tpSettings.Controls.Add(this.btClean);
            this.tpSettings.Controls.Add(this.pCleanHistory);
            this.tpSettings.Controls.Add(this.pCleanCache);
            this.tpSettings.Controls.Add(this.tbTheme);
            this.tpSettings.Controls.Add(this.pSchedule);
            this.tpSettings.Controls.Add(this.btThemeWizard);
            this.tpSettings.Controls.Add(this.btBlocked);
            this.tpSettings.Controls.Add(this.btCookie);
            this.tpSettings.Controls.Add(this.hsAutoForeColor);
            this.tpSettings.Controls.Add(this.lbAutoSelect);
            this.tpSettings.Controls.Add(this.hsNinja);
            this.tpSettings.Controls.Add(this.lbNinja);
            this.tpSettings.Controls.Add(this.pbForeColor);
            this.tpSettings.Controls.Add(this.lbForeColor);
            this.tpSettings.Controls.Add(this.flpClose);
            this.tpSettings.Controls.Add(this.flpNewTab);
            this.tpSettings.Controls.Add(this.flpLayout);
            this.tpSettings.Controls.Add(this.lbBackImageStyle);
            this.tpSettings.Controls.Add(this.lbCloseColor);
            this.tpSettings.Controls.Add(this.lbNewTabColor);
            this.tpSettings.Controls.Add(this.lbBackImage);
            this.tpSettings.Controls.Add(this.pbBack);
            this.tpSettings.Controls.Add(this.pbOverlay);
            this.tpSettings.Controls.Add(this.lbBackColor);
            this.tpSettings.Controls.Add(this.lbOveralColor);
            this.tpSettings.Controls.Add(this.textBox4);
            this.tpSettings.Controls.Add(this.lbThemeName);
            this.tpSettings.Controls.Add(this.lbLayout);
            this.tpSettings.Controls.Add(this.lbTheme);
            this.tpSettings.Controls.Add(this.btOpenSound);
            this.tpSettings.Controls.Add(this.tbSoundLoc);
            this.tpSettings.Controls.Add(this.lbSchedule);
            this.tpSettings.Controls.Add(this.lbSilentMode);
            this.tpSettings.Controls.Add(this.hsSchedule);
            this.tpSettings.Controls.Add(this.hsSilent);
            this.tpSettings.Controls.Add(this.lbDefaultNotifSound);
            this.tpSettings.Controls.Add(this.hsDefaultSound);
            this.tpSettings.Controls.Add(this.lbCleanHistory);
            this.tpSettings.Controls.Add(this.lbCleanCache);
            this.tpSettings.Controls.Add(this.hsCleanHistory);
            this.tpSettings.Controls.Add(this.lbPlayNotifSound);
            this.tpSettings.Controls.Add(this.hsCleanCache);
            this.tpSettings.Controls.Add(this.hsNotificationSound);
            this.tpSettings.Controls.Add(this.lbAutoClean);
            this.tpSettings.Controls.Add(this.lbNotifSetting);
            this.tpSettings.Controls.Add(this.lbDownloads);
            this.tpSettings.Controls.Add(this.lbGeneral);
            this.tpSettings.Controls.Add(this.hsOpen);
            this.tpSettings.Controls.Add(this.hsDownload);
            this.tpSettings.Controls.Add(this.tbFolder);
            this.tpSettings.Controls.Add(this.btDownloadFolder);
            this.tpSettings.Controls.Add(this.lbDownloadFolder);
            this.tpSettings.Controls.Add(this.lbOpen);
            this.tpSettings.Controls.Add(this.lbAutoDownload);
            this.tpSettings.Controls.Add(this.tbUrl);
            this.tpSettings.Controls.Add(this.tbTitle);
            this.tpSettings.Controls.Add(this.tlpNewTab);
            this.tpSettings.Controls.Add(this.btClear);
            this.tpSettings.Controls.Add(this.lbNTUrl);
            this.tpSettings.Controls.Add(this.lbNTTitle);
            this.tpSettings.Controls.Add(this.lbNewTabTitle);
            this.tpSettings.Controls.Add(this.lbLang);
            this.tpSettings.Controls.Add(this.lbFlashInfo);
            this.tpSettings.Controls.Add(this.lbFlash);
            this.tpSettings.Controls.Add(this.hsFlash);
            this.tpSettings.Controls.Add(this.lbSynthRate);
            this.tpSettings.Controls.Add(this.lbSynthVol);
            this.tpSettings.Controls.Add(this.lbDNT);
            this.tpSettings.Controls.Add(this.hsDoNotTrack);
            this.tpSettings.Controls.Add(this.lbautoRestore);
            this.tpSettings.Controls.Add(this.lbLastProxy);
            this.tpSettings.Controls.Add(this.hsAutoRestore);
            this.tpSettings.Controls.Add(this.hsProxy);
            this.tpSettings.Controls.Add(this.lbAtStartup);
            this.tpSettings.Controls.Add(this.lbShowFavorites);
            this.tpSettings.Controls.Add(this.hsFav);
            this.tpSettings.Controls.Add(this.rbNewTab);
            this.tpSettings.Controls.Add(this.lbHomepage);
            this.tpSettings.Controls.Add(this.lbSearchEngine);
            this.tpSettings.Controls.Add(this.tbHomepage);
            this.tpSettings.Controls.Add(this.tbLang);
            this.tpSettings.Controls.Add(this.tbStartup);
            this.tpSettings.Controls.Add(this.tbSearchEngine);
            this.tpSettings.Location = new System.Drawing.Point(4, 24);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(597, 473);
            this.tpSettings.TabIndex = 0;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // pCleanDownload
            // 
            this.pCleanDownload.Controls.Add(this.nudCDOld);
            this.pCleanDownload.Controls.Add(this.nudCDDay);
            this.pCleanDownload.Controls.Add(this.nudCDFile);
            this.pCleanDownload.Controls.Add(this.lbCD6);
            this.pCleanDownload.Controls.Add(this.lbCD4);
            this.pCleanDownload.Controls.Add(this.lbCD2);
            this.pCleanDownload.Controls.Add(this.lbCD5);
            this.pCleanDownload.Controls.Add(this.lbCD3);
            this.pCleanDownload.Controls.Add(this.hsCDOld);
            this.pCleanDownload.Controls.Add(this.lbCD1);
            this.pCleanDownload.Controls.Add(this.hsCDDay);
            this.pCleanDownload.Controls.Add(this.hsCDFile);
            this.pCleanDownload.Location = new System.Drawing.Point(19, 1748);
            this.pCleanDownload.Name = "pCleanDownload";
            this.pCleanDownload.Size = new System.Drawing.Size(538, 88);
            this.pCleanDownload.TabIndex = 130;
            // 
            // nudCDOld
            // 
            this.nudCDOld.Location = new System.Drawing.Point(174, 63);
            this.nudCDOld.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCDOld.Name = "nudCDOld";
            this.nudCDOld.Size = new System.Drawing.Size(34, 21);
            this.nudCDOld.TabIndex = 46;
            this.nudCDOld.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCDOld.ValueChanged += new System.EventHandler(this.nudCDOld_ValueChanged);
            // 
            // nudCDDay
            // 
            this.nudCDDay.Location = new System.Drawing.Point(88, 36);
            this.nudCDDay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCDDay.Name = "nudCDDay";
            this.nudCDDay.Size = new System.Drawing.Size(34, 21);
            this.nudCDDay.TabIndex = 46;
            this.nudCDDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCDDay.ValueChanged += new System.EventHandler(this.nudCDDay_ValueChanged);
            // 
            // nudCDFile
            // 
            this.nudCDFile.Location = new System.Drawing.Point(144, 7);
            this.nudCDFile.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudCDFile.Name = "nudCDFile";
            this.nudCDFile.Size = new System.Drawing.Size(34, 21);
            this.nudCDFile.TabIndex = 46;
            this.nudCDFile.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCDFile.ValueChanged += new System.EventHandler(this.nudCDFile_ValueChanged);
            // 
            // lbCD6
            // 
            this.lbCD6.AutoSize = true;
            this.lbCD6.Location = new System.Drawing.Point(213, 65);
            this.lbCD6.Name = "lbCD6";
            this.lbCD6.Size = new System.Drawing.Size(35, 15);
            this.lbCD6.TabIndex = 0;
            this.lbCD6.Text = "days:";
            // 
            // lbCD4
            // 
            this.lbCD4.AutoSize = true;
            this.lbCD4.Location = new System.Drawing.Point(127, 38);
            this.lbCD4.Name = "lbCD4";
            this.lbCD4.Size = new System.Drawing.Size(35, 15);
            this.lbCD4.TabIndex = 0;
            this.lbCD4.Text = "days:";
            // 
            // lbCD2
            // 
            this.lbCD2.AutoSize = true;
            this.lbCD2.Location = new System.Drawing.Point(184, 9);
            this.lbCD2.Name = "lbCD2";
            this.lbCD2.Size = new System.Drawing.Size(29, 15);
            this.lbCD2.TabIndex = 0;
            this.lbCD2.Text = "MB:";
            // 
            // lbCD5
            // 
            this.lbCD5.AutoSize = true;
            this.lbCD5.Location = new System.Drawing.Point(8, 65);
            this.lbCD5.Name = "lbCD5";
            this.lbCD5.Size = new System.Drawing.Size(160, 15);
            this.lbCD5.TabIndex = 0;
            this.lbCD5.Text = "Clean downloads older than";
            // 
            // lbCD3
            // 
            this.lbCD3.AutoSize = true;
            this.lbCD3.Location = new System.Drawing.Point(8, 38);
            this.lbCD3.Name = "lbCD3";
            this.lbCD3.Size = new System.Drawing.Size(70, 15);
            this.lbCD3.TabIndex = 0;
            this.lbCD3.Text = "Clean every";
            // 
            // hsCDOld
            // 
            this.hsCDOld.Location = new System.Drawing.Point(252, 65);
            this.hsCDOld.Name = "hsCDOld";
            this.hsCDOld.Size = new System.Drawing.Size(50, 19);
            this.hsCDOld.TabIndex = 86;
            this.hsCDOld.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCDOld_CheckedChanged);
            // 
            // lbCD1
            // 
            this.lbCD1.AutoSize = true;
            this.lbCD1.Location = new System.Drawing.Point(8, 9);
            this.lbCD1.Name = "lbCD1";
            this.lbCD1.Size = new System.Drawing.Size(131, 15);
            this.lbCD1.TabIndex = 0;
            this.lbCD1.Text = "When user data is over";
            // 
            // hsCDDay
            // 
            this.hsCDDay.Location = new System.Drawing.Point(166, 37);
            this.hsCDDay.Name = "hsCDDay";
            this.hsCDDay.Size = new System.Drawing.Size(50, 19);
            this.hsCDDay.TabIndex = 86;
            this.hsCDDay.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCDDay_CheckedChanged);
            // 
            // hsCDFile
            // 
            this.hsCDFile.Location = new System.Drawing.Point(225, 7);
            this.hsCDFile.Name = "hsCDFile";
            this.hsCDFile.Size = new System.Drawing.Size(50, 19);
            this.hsCDFile.TabIndex = 86;
            this.hsCDFile.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCDFile_CheckedChanged);
            // 
            // lbCleanDownload
            // 
            this.lbCleanDownload.AutoSize = true;
            this.lbCleanDownload.BackColor = System.Drawing.Color.Transparent;
            this.lbCleanDownload.Location = new System.Drawing.Point(16, 1723);
            this.lbCleanDownload.Name = "lbCleanDownload";
            this.lbCleanDownload.Size = new System.Drawing.Size(135, 15);
            this.lbCleanDownload.TabIndex = 129;
            this.lbCleanDownload.Text = "Auto-Clean Downloads:";
            // 
            // hsCleanDownload
            // 
            this.hsCleanDownload.Location = new System.Drawing.Point(156, 1723);
            this.hsCleanDownload.Name = "hsCleanDownload";
            this.hsCleanDownload.Size = new System.Drawing.Size(50, 19);
            this.hsCleanDownload.TabIndex = 128;
            this.hsCleanDownload.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCleanDownload_CheckedChanged);
            // 
            // pCleanLog
            // 
            this.pCleanLog.Controls.Add(this.nudCLOld);
            this.pCleanLog.Controls.Add(this.nudCLDay);
            this.pCleanLog.Controls.Add(this.nudCLFile);
            this.pCleanLog.Controls.Add(this.lbCL4);
            this.pCleanLog.Controls.Add(this.lbCL6);
            this.pCleanLog.Controls.Add(this.lbCL2);
            this.pCleanLog.Controls.Add(this.lbCL3);
            this.pCleanLog.Controls.Add(this.lbCL1);
            this.pCleanLog.Controls.Add(this.lbCL5);
            this.pCleanLog.Controls.Add(this.hsCLDay);
            this.pCleanLog.Controls.Add(this.hsCLFile);
            this.pCleanLog.Controls.Add(this.hsCLOld);
            this.pCleanLog.Location = new System.Drawing.Point(19, 1618);
            this.pCleanLog.Name = "pCleanLog";
            this.pCleanLog.Size = new System.Drawing.Size(538, 83);
            this.pCleanLog.TabIndex = 126;
            // 
            // nudCLOld
            // 
            this.nudCLOld.Location = new System.Drawing.Point(137, 59);
            this.nudCLOld.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCLOld.Name = "nudCLOld";
            this.nudCLOld.Size = new System.Drawing.Size(34, 21);
            this.nudCLOld.TabIndex = 46;
            this.nudCLOld.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCLOld.ValueChanged += new System.EventHandler(this.nudCLOld_ValueChanged);
            // 
            // nudCLDay
            // 
            this.nudCLDay.Location = new System.Drawing.Point(88, 34);
            this.nudCLDay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCLDay.Name = "nudCLDay";
            this.nudCLDay.Size = new System.Drawing.Size(34, 21);
            this.nudCLDay.TabIndex = 46;
            this.nudCLDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCLDay.ValueChanged += new System.EventHandler(this.nudCLDay_ValueChanged);
            // 
            // nudCLFile
            // 
            this.nudCLFile.Location = new System.Drawing.Point(125, 7);
            this.nudCLFile.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudCLFile.Name = "nudCLFile";
            this.nudCLFile.Size = new System.Drawing.Size(34, 21);
            this.nudCLFile.TabIndex = 46;
            this.nudCLFile.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCLFile.ValueChanged += new System.EventHandler(this.nudCLFile_ValueChanged);
            // 
            // lbCL4
            // 
            this.lbCL4.AutoSize = true;
            this.lbCL4.Location = new System.Drawing.Point(127, 36);
            this.lbCL4.Name = "lbCL4";
            this.lbCL4.Size = new System.Drawing.Size(35, 15);
            this.lbCL4.TabIndex = 0;
            this.lbCL4.Text = "days:";
            // 
            // lbCL6
            // 
            this.lbCL6.AutoSize = true;
            this.lbCL6.Location = new System.Drawing.Point(177, 62);
            this.lbCL6.Name = "lbCL6";
            this.lbCL6.Size = new System.Drawing.Size(35, 15);
            this.lbCL6.TabIndex = 0;
            this.lbCL6.Text = "days:";
            // 
            // lbCL2
            // 
            this.lbCL2.AutoSize = true;
            this.lbCL2.Location = new System.Drawing.Point(165, 9);
            this.lbCL2.Name = "lbCL2";
            this.lbCL2.Size = new System.Drawing.Size(29, 15);
            this.lbCL2.TabIndex = 0;
            this.lbCL2.Text = "MB:";
            // 
            // lbCL3
            // 
            this.lbCL3.AutoSize = true;
            this.lbCL3.Location = new System.Drawing.Point(8, 36);
            this.lbCL3.Name = "lbCL3";
            this.lbCL3.Size = new System.Drawing.Size(70, 15);
            this.lbCL3.TabIndex = 0;
            this.lbCL3.Text = "Clean every";
            // 
            // lbCL1
            // 
            this.lbCL1.AutoSize = true;
            this.lbCL1.Location = new System.Drawing.Point(8, 9);
            this.lbCL1.Name = "lbCL1";
            this.lbCL1.Size = new System.Drawing.Size(112, 15);
            this.lbCL1.TabIndex = 0;
            this.lbCL1.Text = "When logs are over";
            // 
            // lbCL5
            // 
            this.lbCL5.AutoSize = true;
            this.lbCL5.Location = new System.Drawing.Point(8, 61);
            this.lbCL5.Name = "lbCL5";
            this.lbCL5.Size = new System.Drawing.Size(123, 15);
            this.lbCL5.TabIndex = 0;
            this.lbCL5.Text = "Clean logs older than";
            // 
            // hsCLDay
            // 
            this.hsCLDay.Location = new System.Drawing.Point(166, 35);
            this.hsCLDay.Name = "hsCLDay";
            this.hsCLDay.Size = new System.Drawing.Size(50, 19);
            this.hsCLDay.TabIndex = 86;
            this.hsCLDay.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCLDay_CheckedChanged);
            // 
            // hsCLFile
            // 
            this.hsCLFile.Location = new System.Drawing.Point(206, 7);
            this.hsCLFile.Name = "hsCLFile";
            this.hsCLFile.Size = new System.Drawing.Size(50, 19);
            this.hsCLFile.TabIndex = 86;
            this.hsCLFile.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCLFile_CheckedChanged);
            // 
            // hsCLOld
            // 
            this.hsCLOld.Location = new System.Drawing.Point(218, 61);
            this.hsCLOld.Name = "hsCLOld";
            this.hsCLOld.Size = new System.Drawing.Size(50, 19);
            this.hsCLOld.TabIndex = 86;
            this.hsCLOld.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCLOld_CheckedChanged);
            // 
            // lbCleanLog
            // 
            this.lbCleanLog.AutoSize = true;
            this.lbCleanLog.BackColor = System.Drawing.Color.Transparent;
            this.lbCleanLog.Location = new System.Drawing.Point(16, 1593);
            this.lbCleanLog.Name = "lbCleanLog";
            this.lbCleanLog.Size = new System.Drawing.Size(100, 15);
            this.lbCleanLog.TabIndex = 125;
            this.lbCleanLog.Text = "Auto-Clean Logs:";
            // 
            // hsCleanLog
            // 
            this.hsCleanLog.Location = new System.Drawing.Point(121, 1593);
            this.hsCleanLog.Name = "hsCleanLog";
            this.hsCleanLog.Size = new System.Drawing.Size(50, 19);
            this.hsCleanLog.TabIndex = 124;
            this.hsCleanLog.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCleanLog_CheckedChanged);
            // 
            // btClean
            // 
            this.btClean.AutoSize = true;
            this.btClean.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btClean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btClean.DrawImage = true;
            this.btClean.FlatAppearance.BorderSize = 0;
            this.btClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClean.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btClean.Location = new System.Drawing.Point(472, 1313);
            this.btClean.Name = "btClean";
            this.btClean.Size = new System.Drawing.Size(85, 27);
            this.btClean.TabIndex = 123;
            this.btClean.Text = "Clean Now";
            this.btClean.UseVisualStyleBackColor = false;
            this.btClean.Click += new System.EventHandler(this.btCleanCache_Click);
            // 
            // pCleanHistory
            // 
            this.pCleanHistory.Controls.Add(this.nudCHOld);
            this.pCleanHistory.Controls.Add(this.nudCHDay);
            this.pCleanHistory.Controls.Add(this.nudCHFile);
            this.pCleanHistory.Controls.Add(this.lbCH4);
            this.pCleanHistory.Controls.Add(this.lbCH2);
            this.pCleanHistory.Controls.Add(this.lbCH6);
            this.pCleanHistory.Controls.Add(this.lbCH3);
            this.pCleanHistory.Controls.Add(this.lbCH1);
            this.pCleanHistory.Controls.Add(this.hsCHDay);
            this.pCleanHistory.Controls.Add(this.hsCHFile);
            this.pCleanHistory.Controls.Add(this.lbCH5);
            this.pCleanHistory.Controls.Add(this.hsCHOld);
            this.pCleanHistory.Location = new System.Drawing.Point(19, 1483);
            this.pCleanHistory.Name = "pCleanHistory";
            this.pCleanHistory.Size = new System.Drawing.Size(538, 87);
            this.pCleanHistory.TabIndex = 122;
            // 
            // nudCHOld
            // 
            this.nudCHOld.Location = new System.Drawing.Point(149, 62);
            this.nudCHOld.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCHOld.Name = "nudCHOld";
            this.nudCHOld.Size = new System.Drawing.Size(34, 21);
            this.nudCHOld.TabIndex = 46;
            this.nudCHOld.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCHOld.ValueChanged += new System.EventHandler(this.nudCHOld_ValueChanged);
            // 
            // nudCHDay
            // 
            this.nudCHDay.Location = new System.Drawing.Point(88, 36);
            this.nudCHDay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCHDay.Name = "nudCHDay";
            this.nudCHDay.Size = new System.Drawing.Size(34, 21);
            this.nudCHDay.TabIndex = 46;
            this.nudCHDay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCHDay.ValueChanged += new System.EventHandler(this.nudCHDay_ValueChanged);
            this.nudCHDay.Click += new System.EventHandler(this.schedule_Click);
            // 
            // nudCHFile
            // 
            this.nudCHFile.Location = new System.Drawing.Point(144, 7);
            this.nudCHFile.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudCHFile.Name = "nudCHFile";
            this.nudCHFile.Size = new System.Drawing.Size(34, 21);
            this.nudCHFile.TabIndex = 46;
            this.nudCHFile.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCHFile.ValueChanged += new System.EventHandler(this.nudCHFile_ValueChanged);
            this.nudCHFile.Click += new System.EventHandler(this.schedule_Click);
            // 
            // lbCH4
            // 
            this.lbCH4.AutoSize = true;
            this.lbCH4.Location = new System.Drawing.Point(127, 38);
            this.lbCH4.Name = "lbCH4";
            this.lbCH4.Size = new System.Drawing.Size(35, 15);
            this.lbCH4.TabIndex = 0;
            this.lbCH4.Text = "days:";
            // 
            // lbCH2
            // 
            this.lbCH2.AutoSize = true;
            this.lbCH2.Location = new System.Drawing.Point(184, 9);
            this.lbCH2.Name = "lbCH2";
            this.lbCH2.Size = new System.Drawing.Size(29, 15);
            this.lbCH2.TabIndex = 0;
            this.lbCH2.Text = "MB:";
            // 
            // lbCH6
            // 
            this.lbCH6.AutoSize = true;
            this.lbCH6.Location = new System.Drawing.Point(189, 65);
            this.lbCH6.Name = "lbCH6";
            this.lbCH6.Size = new System.Drawing.Size(35, 15);
            this.lbCH6.TabIndex = 0;
            this.lbCH6.Text = "days:";
            // 
            // lbCH3
            // 
            this.lbCH3.AutoSize = true;
            this.lbCH3.Location = new System.Drawing.Point(8, 38);
            this.lbCH3.Name = "lbCH3";
            this.lbCH3.Size = new System.Drawing.Size(70, 15);
            this.lbCH3.TabIndex = 0;
            this.lbCH3.Text = "Clean every";
            // 
            // lbCH1
            // 
            this.lbCH1.AutoSize = true;
            this.lbCH1.Location = new System.Drawing.Point(8, 9);
            this.lbCH1.Name = "lbCH1";
            this.lbCH1.Size = new System.Drawing.Size(131, 15);
            this.lbCH1.TabIndex = 0;
            this.lbCH1.Text = "When user data is over";
            // 
            // hsCHDay
            // 
            this.hsCHDay.Location = new System.Drawing.Point(166, 38);
            this.hsCHDay.Name = "hsCHDay";
            this.hsCHDay.Size = new System.Drawing.Size(50, 19);
            this.hsCHDay.TabIndex = 86;
            this.hsCHDay.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCHDay_CheckedChanged);
            // 
            // hsCHFile
            // 
            this.hsCHFile.Location = new System.Drawing.Point(225, 7);
            this.hsCHFile.Name = "hsCHFile";
            this.hsCHFile.Size = new System.Drawing.Size(50, 19);
            this.hsCHFile.TabIndex = 86;
            this.hsCHFile.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCHFile_CheckedChanged);
            // 
            // lbCH5
            // 
            this.lbCH5.AutoSize = true;
            this.lbCH5.Location = new System.Drawing.Point(8, 64);
            this.lbCH5.Name = "lbCH5";
            this.lbCH5.Size = new System.Drawing.Size(135, 15);
            this.lbCH5.TabIndex = 0;
            this.lbCH5.Text = "Clean history older than";
            // 
            // hsCHOld
            // 
            this.hsCHOld.Location = new System.Drawing.Point(230, 64);
            this.hsCHOld.Name = "hsCHOld";
            this.hsCHOld.Size = new System.Drawing.Size(50, 19);
            this.hsCHOld.TabIndex = 86;
            this.hsCHOld.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCHOld_CheckedChanged);
            // 
            // pCleanCache
            // 
            this.pCleanCache.Controls.Add(this.nudCC2);
            this.pCleanCache.Controls.Add(this.nudCC1);
            this.pCleanCache.Controls.Add(this.lbCC4);
            this.pCleanCache.Controls.Add(this.lbCC2);
            this.pCleanCache.Controls.Add(this.lbCC3);
            this.pCleanCache.Controls.Add(this.lbCC1);
            this.pCleanCache.Controls.Add(this.hsCC2);
            this.pCleanCache.Controls.Add(this.hsCC1);
            this.pCleanCache.Location = new System.Drawing.Point(19, 1378);
            this.pCleanCache.Name = "pCleanCache";
            this.pCleanCache.Size = new System.Drawing.Size(538, 62);
            this.pCleanCache.TabIndex = 122;
            // 
            // nudCC2
            // 
            this.nudCC2.Location = new System.Drawing.Point(88, 36);
            this.nudCC2.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudCC2.Name = "nudCC2";
            this.nudCC2.Size = new System.Drawing.Size(34, 21);
            this.nudCC2.TabIndex = 46;
            this.nudCC2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCC2.ValueChanged += new System.EventHandler(this.nudCC2_ValueChanged);
            this.nudCC2.Click += new System.EventHandler(this.schedule_Click);
            // 
            // nudCC1
            // 
            this.nudCC1.Location = new System.Drawing.Point(130, 7);
            this.nudCC1.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudCC1.Name = "nudCC1";
            this.nudCC1.Size = new System.Drawing.Size(34, 21);
            this.nudCC1.TabIndex = 46;
            this.nudCC1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCC1.ValueChanged += new System.EventHandler(this.nudCC1_ValueChanged);
            this.nudCC1.Click += new System.EventHandler(this.schedule_Click);
            // 
            // lbCC4
            // 
            this.lbCC4.AutoSize = true;
            this.lbCC4.Location = new System.Drawing.Point(127, 38);
            this.lbCC4.Name = "lbCC4";
            this.lbCC4.Size = new System.Drawing.Size(35, 15);
            this.lbCC4.TabIndex = 0;
            this.lbCC4.Text = "days:";
            // 
            // lbCC2
            // 
            this.lbCC2.AutoSize = true;
            this.lbCC2.Location = new System.Drawing.Point(170, 9);
            this.lbCC2.Name = "lbCC2";
            this.lbCC2.Size = new System.Drawing.Size(29, 15);
            this.lbCC2.TabIndex = 0;
            this.lbCC2.Text = "MB:";
            // 
            // lbCC3
            // 
            this.lbCC3.AutoSize = true;
            this.lbCC3.Location = new System.Drawing.Point(8, 38);
            this.lbCC3.Name = "lbCC3";
            this.lbCC3.Size = new System.Drawing.Size(70, 15);
            this.lbCC3.TabIndex = 0;
            this.lbCC3.Text = "Clean every";
            // 
            // lbCC1
            // 
            this.lbCC1.AutoSize = true;
            this.lbCC1.Location = new System.Drawing.Point(8, 9);
            this.lbCC1.Name = "lbCC1";
            this.lbCC1.Size = new System.Drawing.Size(113, 15);
            this.lbCC1.TabIndex = 0;
            this.lbCC1.Text = "When cache is over";
            // 
            // hsCC2
            // 
            this.hsCC2.Location = new System.Drawing.Point(166, 38);
            this.hsCC2.Name = "hsCC2";
            this.hsCC2.Size = new System.Drawing.Size(50, 19);
            this.hsCC2.TabIndex = 86;
            this.hsCC2.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCC2_CheckedChanged);
            // 
            // hsCC1
            // 
            this.hsCC1.Location = new System.Drawing.Point(211, 7);
            this.hsCC1.Name = "hsCC1";
            this.hsCC1.Size = new System.Drawing.Size(50, 19);
            this.hsCC1.TabIndex = 86;
            this.hsCC1.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCC1_CheckedChanged);
            // 
            // tbTheme
            // 
            this.tbTheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTheme.Location = new System.Drawing.Point(77, 2137);
            this.tbTheme.Name = "tbTheme";
            this.tbTheme.ReadOnly = true;
            this.tbTheme.Size = new System.Drawing.Size(378, 21);
            this.tbTheme.TabIndex = 121;
            this.tbTheme.Tag = "";
            this.tbTheme.Click += new System.EventHandler(this.tbTheme_Click);
            // 
            // pSchedule
            // 
            this.pSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pSchedule.Controls.Add(this.flpFrom);
            this.pSchedule.Controls.Add(this.flpEvery);
            this.pSchedule.Controls.Add(this.lb24HType);
            this.pSchedule.Controls.Add(this.scheduleFrom);
            this.pSchedule.Controls.Add(this.flpTo);
            this.pSchedule.Controls.Add(this.scheduleEvery);
            this.pSchedule.Controls.Add(this.scheduleTo);
            this.pSchedule.Location = new System.Drawing.Point(19, 1182);
            this.pSchedule.Name = "pSchedule";
            this.pSchedule.Size = new System.Drawing.Size(453, 116);
            this.pSchedule.TabIndex = 120;
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
            this.fromHour.Click += new System.EventHandler(this.schedule_Click);
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
            this.fromMin.Click += new System.EventHandler(this.schedule_Click);
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
            this.lb24HType.Size = new System.Drawing.Size(438, 19);
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
            this.toHour.Click += new System.EventHandler(this.schedule_Click);
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
            this.toMin.Click += new System.EventHandler(this.schedule_Click);
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
            // btThemeWizard
            // 
            this.btThemeWizard.AutoSize = true;
            this.btThemeWizard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btThemeWizard.FlatAppearance.BorderSize = 0;
            this.btThemeWizard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btThemeWizard.Location = new System.Drawing.Point(19, 2176);
            this.btThemeWizard.Name = "btThemeWizard";
            this.btThemeWizard.Size = new System.Drawing.Size(123, 25);
            this.btThemeWizard.TabIndex = 119;
            this.btThemeWizard.Text = "Run Theme Wizard";
            this.btThemeWizard.UseVisualStyleBackColor = true;
            this.btThemeWizard.Click += new System.EventHandler(this.btThemeWizard_Click);
            // 
            // btBlocked
            // 
            this.btBlocked.AutoSize = true;
            this.btBlocked.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btBlocked.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btBlocked.DrawImage = true;
            this.btBlocked.FlatAppearance.BorderSize = 0;
            this.btBlocked.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBlocked.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btBlocked.Location = new System.Drawing.Point(19, 494);
            this.btBlocked.Name = "btBlocked";
            this.btBlocked.Size = new System.Drawing.Size(115, 27);
            this.btBlocked.TabIndex = 118;
            this.btBlocked.Text = "Blocked Sites...";
            this.btBlocked.UseVisualStyleBackColor = false;
            this.btBlocked.Click += new System.EventHandler(this.htButton3_Click);
            // 
            // btCookie
            // 
            this.btCookie.AutoSize = true;
            this.btCookie.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btCookie.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btCookie.DrawImage = true;
            this.btCookie.FlatAppearance.BorderSize = 0;
            this.btCookie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCookie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btCookie.Location = new System.Drawing.Point(19, 458);
            this.btCookie.Name = "btCookie";
            this.btCookie.Size = new System.Drawing.Size(105, 27);
            this.btCookie.TabIndex = 118;
            this.btCookie.Text = "Site Settings..";
            this.btCookie.UseVisualStyleBackColor = false;
            this.btCookie.Click += new System.EventHandler(this.btCookie_Click);
            // 
            // hsAutoForeColor
            // 
            this.hsAutoForeColor.Location = new System.Drawing.Point(286, 1908);
            this.hsAutoForeColor.Name = "hsAutoForeColor";
            this.hsAutoForeColor.Size = new System.Drawing.Size(50, 19);
            this.hsAutoForeColor.TabIndex = 116;
            this.hsAutoForeColor.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsAutoForeColor_CheckedChanged);
            // 
            // lbAutoSelect
            // 
            this.lbAutoSelect.AutoSize = true;
            this.lbAutoSelect.BackColor = System.Drawing.Color.Transparent;
            this.lbAutoSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbAutoSelect.Location = new System.Drawing.Point(200, 1909);
            this.lbAutoSelect.Name = "lbAutoSelect";
            this.lbAutoSelect.Size = new System.Drawing.Size(80, 16);
            this.lbAutoSelect.TabIndex = 114;
            this.lbAutoSelect.Tag = "";
            this.lbAutoSelect.Text = "Auto-Select:";
            // 
            // hsNinja
            // 
            this.hsNinja.Location = new System.Drawing.Point(166, 1972);
            this.hsNinja.Name = "hsNinja";
            this.hsNinja.Size = new System.Drawing.Size(50, 19);
            this.hsNinja.TabIndex = 115;
            this.hsNinja.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsNinja_CheckedChanged);
            // 
            // lbNinja
            // 
            this.lbNinja.AutoSize = true;
            this.lbNinja.BackColor = System.Drawing.Color.Transparent;
            this.lbNinja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbNinja.Location = new System.Drawing.Point(17, 1975);
            this.lbNinja.Name = "lbNinja";
            this.lbNinja.Size = new System.Drawing.Size(80, 16);
            this.lbNinja.TabIndex = 113;
            this.lbNinja.Tag = "";
            this.lbNinja.Text = "Ninja Mode:";
            // 
            // pbForeColor
            // 
            this.pbForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbForeColor.Location = new System.Drawing.Point(171, 1903);
            this.pbForeColor.Name = "pbForeColor";
            this.pbForeColor.Size = new System.Drawing.Size(23, 23);
            this.pbForeColor.TabIndex = 112;
            this.pbForeColor.TabStop = false;
            this.pbForeColor.Tag = "";
            this.pbForeColor.Click += new System.EventHandler(this.pbForeColor_Click);
            // 
            // lbForeColor
            // 
            this.lbForeColor.AutoSize = true;
            this.lbForeColor.BackColor = System.Drawing.Color.Transparent;
            this.lbForeColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbForeColor.Location = new System.Drawing.Point(16, 1908);
            this.lbForeColor.Name = "lbForeColor";
            this.lbForeColor.Size = new System.Drawing.Size(122, 16);
            this.lbForeColor.TabIndex = 111;
            this.lbForeColor.Tag = "";
            this.lbForeColor.Text = "Foreground Color : ";
            // 
            // flpClose
            // 
            this.flpClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpClose.AutoScroll = true;
            this.flpClose.Controls.Add(this.rbBackColor1);
            this.flpClose.Controls.Add(this.rbForeColor1);
            this.flpClose.Controls.Add(this.rbOverlayColor1);
            this.flpClose.Location = new System.Drawing.Point(151, 2100);
            this.flpClose.Name = "flpClose";
            this.flpClose.Size = new System.Drawing.Size(304, 27);
            this.flpClose.TabIndex = 109;
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
            this.rbBackColor1.Click += new System.EventHandler(this.rbBackColor1_CheckedChanged);
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
            this.rbForeColor1.Click += new System.EventHandler(this.rbForeColor1_CheckedChanged);
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
            this.rbOverlayColor1.Click += new System.EventHandler(this.rbOverlayColor1_CheckedChanged);
            // 
            // flpNewTab
            // 
            this.flpNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpNewTab.AutoScroll = true;
            this.flpNewTab.Controls.Add(this.rbBackColor);
            this.flpNewTab.Controls.Add(this.rbForeColor);
            this.flpNewTab.Controls.Add(this.rbOverlayColor);
            this.flpNewTab.Location = new System.Drawing.Point(166, 2069);
            this.flpNewTab.Name = "flpNewTab";
            this.flpNewTab.Size = new System.Drawing.Size(289, 27);
            this.flpNewTab.TabIndex = 108;
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
            this.rbBackColor.Click += new System.EventHandler(this.rbBackColor_CheckedChanged);
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
            this.rbForeColor.Click += new System.EventHandler(this.rbForeColor_CheckedChanged);
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
            this.rbOverlayColor.Click += new System.EventHandler(this.rbOverlayColor_CheckedChanged);
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
            this.flpLayout.Location = new System.Drawing.Point(181, 2003);
            this.flpLayout.Name = "flpLayout";
            this.flpLayout.Size = new System.Drawing.Size(274, 27);
            this.flpLayout.TabIndex = 110;
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
            this.rbNone.Click += new System.EventHandler(this.rbNone_CheckedChanged);
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
            this.rbTile.Click += new System.EventHandler(this.rbTile_CheckedChanged);
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
            this.rbCenter.Click += new System.EventHandler(this.rbCenter_CheckedChanged);
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
            this.rbStretch.Click += new System.EventHandler(this.rbStretch_CheckedChanged);
            // 
            // rbZoom
            // 
            this.rbZoom.AutoSize = true;
            this.rbZoom.Location = new System.Drawing.Point(3, 28);
            this.rbZoom.Name = "rbZoom";
            this.rbZoom.Size = new System.Drawing.Size(57, 19);
            this.rbZoom.TabIndex = 3;
            this.rbZoom.TabStop = true;
            this.rbZoom.Text = "Zoom";
            this.rbZoom.UseVisualStyleBackColor = true;
            this.rbZoom.Click += new System.EventHandler(this.rbZoom_CheckedChanged);
            // 
            // lbBackImageStyle
            // 
            this.lbBackImageStyle.AutoSize = true;
            this.lbBackImageStyle.BackColor = System.Drawing.Color.Transparent;
            this.lbBackImageStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbBackImageStyle.Location = new System.Drawing.Point(17, 2007);
            this.lbBackImageStyle.Name = "lbBackImageStyle";
            this.lbBackImageStyle.Size = new System.Drawing.Size(158, 16);
            this.lbBackImageStyle.TabIndex = 100;
            this.lbBackImageStyle.Tag = "";
            this.lbBackImageStyle.Text = "Background Image Style:";
            // 
            // lbCloseColor
            // 
            this.lbCloseColor.AutoSize = true;
            this.lbCloseColor.BackColor = System.Drawing.Color.Transparent;
            this.lbCloseColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbCloseColor.Location = new System.Drawing.Point(17, 2104);
            this.lbCloseColor.Name = "lbCloseColor";
            this.lbCloseColor.Size = new System.Drawing.Size(121, 16);
            this.lbCloseColor.TabIndex = 101;
            this.lbCloseColor.Tag = "";
            this.lbCloseColor.Text = "Close Button Color:";
            // 
            // lbNewTabColor
            // 
            this.lbNewTabColor.AutoSize = true;
            this.lbNewTabColor.BackColor = System.Drawing.Color.Transparent;
            this.lbNewTabColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbNewTabColor.Location = new System.Drawing.Point(16, 2073);
            this.lbNewTabColor.Name = "lbNewTabColor";
            this.lbNewTabColor.Size = new System.Drawing.Size(141, 16);
            this.lbNewTabColor.TabIndex = 96;
            this.lbNewTabColor.Tag = "";
            this.lbNewTabColor.Text = "New Tab Button Color:";
            // 
            // lbBackImage
            // 
            this.lbBackImage.AutoSize = true;
            this.lbBackImage.BackColor = System.Drawing.Color.Transparent;
            this.lbBackImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbBackImage.Location = new System.Drawing.Point(16, 2039);
            this.lbBackImage.Name = "lbBackImage";
            this.lbBackImage.Size = new System.Drawing.Size(128, 16);
            this.lbBackImage.TabIndex = 102;
            this.lbBackImage.Tag = "";
            this.lbBackImage.Text = "Background Image :";
            // 
            // pbBack
            // 
            this.pbBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBack.Location = new System.Drawing.Point(171, 1871);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(23, 23);
            this.pbBack.TabIndex = 104;
            this.pbBack.TabStop = false;
            this.pbBack.Tag = "";
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // pbOverlay
            // 
            this.pbOverlay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOverlay.Location = new System.Drawing.Point(171, 1938);
            this.pbOverlay.Name = "pbOverlay";
            this.pbOverlay.Size = new System.Drawing.Size(23, 23);
            this.pbOverlay.TabIndex = 106;
            this.pbOverlay.TabStop = false;
            this.pbOverlay.Tag = "";
            this.pbOverlay.Click += new System.EventHandler(this.pbOverlay_Click);
            // 
            // lbBackColor
            // 
            this.lbBackColor.AutoSize = true;
            this.lbBackColor.BackColor = System.Drawing.Color.Transparent;
            this.lbBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbBackColor.Location = new System.Drawing.Point(16, 1875);
            this.lbBackColor.Name = "lbBackColor";
            this.lbBackColor.Size = new System.Drawing.Size(125, 16);
            this.lbBackColor.TabIndex = 99;
            this.lbBackColor.Tag = "";
            this.lbBackColor.Text = "Background Color : ";
            // 
            // lbOveralColor
            // 
            this.lbOveralColor.AutoSize = true;
            this.lbOveralColor.BackColor = System.Drawing.Color.Transparent;
            this.lbOveralColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbOveralColor.Location = new System.Drawing.Point(17, 1940);
            this.lbOveralColor.Name = "lbOveralColor";
            this.lbOveralColor.Size = new System.Drawing.Size(92, 16);
            this.lbOveralColor.TabIndex = 103;
            this.lbOveralColor.Tag = "";
            this.lbOveralColor.Text = "Overal Color : ";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(156, 2039);
            this.textBox4.MaxLength = 2147483647;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(299, 21);
            this.textBox4.TabIndex = 93;
            this.textBox4.Text = "download ivan\'s sexy calculator pls";
            this.textBox4.Click += new System.EventHandler(this.textBox4_Click);
            // 
            // lbThemeName
            // 
            this.lbThemeName.AutoSize = true;
            this.lbThemeName.BackColor = System.Drawing.Color.Transparent;
            this.lbThemeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbThemeName.Location = new System.Drawing.Point(16, 2140);
            this.lbThemeName.Name = "lbThemeName";
            this.lbThemeName.Size = new System.Drawing.Size(57, 16);
            this.lbThemeName.TabIndex = 98;
            this.lbThemeName.Tag = "";
            this.lbThemeName.Text = "Theme: ";
            // 
            // lbLayout
            // 
            this.lbLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLayout.BackColor = System.Drawing.Color.Transparent;
            this.lbLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbLayout.Location = new System.Drawing.Point(16, 2221);
            this.lbLayout.Name = "lbLayout";
            this.lbLayout.Size = new System.Drawing.Size(439, 16);
            this.lbLayout.TabIndex = 97;
            this.lbLayout.Tag = "";
            // 
            // lbTheme
            // 
            this.lbTheme.AutoSize = true;
            this.lbTheme.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbTheme.Location = new System.Drawing.Point(14, 1839);
            this.lbTheme.Name = "lbTheme";
            this.lbTheme.Size = new System.Drawing.Size(74, 25);
            this.lbTheme.TabIndex = 107;
            this.lbTheme.Text = "Theme";
            // 
            // btOpenSound
            // 
            this.btOpenSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btOpenSound.AutoSize = true;
            this.btOpenSound.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btOpenSound.FlatAppearance.BorderSize = 0;
            this.btOpenSound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOpenSound.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.btOpenSound.Location = new System.Drawing.Point(416, 1063);
            this.btOpenSound.Name = "btOpenSound";
            this.btOpenSound.Size = new System.Drawing.Size(27, 26);
            this.btOpenSound.TabIndex = 92;
            this.btOpenSound.Text = "...";
            this.btOpenSound.UseVisualStyleBackColor = true;
            this.btOpenSound.Click += new System.EventHandler(this.btOpenSound_Click);
            // 
            // tbSoundLoc
            // 
            this.tbSoundLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSoundLoc.Location = new System.Drawing.Point(19, 1068);
            this.tbSoundLoc.Name = "tbSoundLoc";
            this.tbSoundLoc.Size = new System.Drawing.Size(397, 21);
            this.tbSoundLoc.TabIndex = 91;
            this.tbSoundLoc.TextChanged += new System.EventHandler(this.tbSoundLoc_TextChanged);
            // 
            // lbSchedule
            // 
            this.lbSchedule.AutoSize = true;
            this.lbSchedule.BackColor = System.Drawing.Color.Transparent;
            this.lbSchedule.Location = new System.Drawing.Point(16, 1140);
            this.lbSchedule.Name = "lbSchedule";
            this.lbSchedule.Size = new System.Drawing.Size(131, 15);
            this.lbSchedule.TabIndex = 87;
            this.lbSchedule.Text = "Schedule Silent Mode:";
            // 
            // lbSilentMode
            // 
            this.lbSilentMode.AutoSize = true;
            this.lbSilentMode.BackColor = System.Drawing.Color.Transparent;
            this.lbSilentMode.Location = new System.Drawing.Point(16, 1107);
            this.lbSilentMode.Name = "lbSilentMode";
            this.lbSilentMode.Size = new System.Drawing.Size(76, 15);
            this.lbSilentMode.TabIndex = 88;
            this.lbSilentMode.Text = "Silent Mode:";
            // 
            // hsSchedule
            // 
            this.hsSchedule.Location = new System.Drawing.Point(164, 1138);
            this.hsSchedule.Name = "hsSchedule";
            this.hsSchedule.Size = new System.Drawing.Size(50, 19);
            this.hsSchedule.TabIndex = 83;
            this.hsSchedule.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsSchedule_CheckedChanged);
            // 
            // hsSilent
            // 
            this.hsSilent.Location = new System.Drawing.Point(97, 1104);
            this.hsSilent.Name = "hsSilent";
            this.hsSilent.Size = new System.Drawing.Size(50, 19);
            this.hsSilent.TabIndex = 84;
            this.hsSilent.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsSilent_CheckedChanged);
            // 
            // lbDefaultNotifSound
            // 
            this.lbDefaultNotifSound.AutoSize = true;
            this.lbDefaultNotifSound.BackColor = System.Drawing.Color.Transparent;
            this.lbDefaultNotifSound.Location = new System.Drawing.Point(16, 1038);
            this.lbDefaultNotifSound.Name = "lbDefaultNotifSound";
            this.lbDefaultNotifSound.Size = new System.Drawing.Size(171, 15);
            this.lbDefaultNotifSound.TabIndex = 89;
            this.lbDefaultNotifSound.Text = "Use default notification sound:";
            // 
            // hsDefaultSound
            // 
            this.hsDefaultSound.Location = new System.Drawing.Point(204, 1038);
            this.hsDefaultSound.Name = "hsDefaultSound";
            this.hsDefaultSound.Size = new System.Drawing.Size(50, 19);
            this.hsDefaultSound.TabIndex = 85;
            this.hsDefaultSound.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsDefaultSound_CheckedChanged);
            // 
            // lbCleanHistory
            // 
            this.lbCleanHistory.AutoSize = true;
            this.lbCleanHistory.BackColor = System.Drawing.Color.Transparent;
            this.lbCleanHistory.Location = new System.Drawing.Point(16, 1458);
            this.lbCleanHistory.Name = "lbCleanHistory";
            this.lbCleanHistory.Size = new System.Drawing.Size(110, 15);
            this.lbCleanHistory.TabIndex = 90;
            this.lbCleanHistory.Text = "Auto-Clean History:";
            // 
            // lbCleanCache
            // 
            this.lbCleanCache.AutoSize = true;
            this.lbCleanCache.BackColor = System.Drawing.Color.Transparent;
            this.lbCleanCache.Location = new System.Drawing.Point(16, 1353);
            this.lbCleanCache.Name = "lbCleanCache";
            this.lbCleanCache.Size = new System.Drawing.Size(108, 15);
            this.lbCleanCache.TabIndex = 90;
            this.lbCleanCache.Text = "Auto-Clean Cache:";
            // 
            // hsCleanHistory
            // 
            this.hsCleanHistory.Location = new System.Drawing.Point(131, 1458);
            this.hsCleanHistory.Name = "hsCleanHistory";
            this.hsCleanHistory.Size = new System.Drawing.Size(50, 19);
            this.hsCleanHistory.TabIndex = 86;
            this.hsCleanHistory.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCleanHistory_CheckedChanged);
            // 
            // lbPlayNotifSound
            // 
            this.lbPlayNotifSound.AutoSize = true;
            this.lbPlayNotifSound.BackColor = System.Drawing.Color.Transparent;
            this.lbPlayNotifSound.Location = new System.Drawing.Point(16, 999);
            this.lbPlayNotifSound.Name = "lbPlayNotifSound";
            this.lbPlayNotifSound.Size = new System.Drawing.Size(136, 15);
            this.lbPlayNotifSound.TabIndex = 90;
            this.lbPlayNotifSound.Text = "Play Notification Sound:";
            // 
            // hsCleanCache
            // 
            this.hsCleanCache.Location = new System.Drawing.Point(131, 1353);
            this.hsCleanCache.Name = "hsCleanCache";
            this.hsCleanCache.Size = new System.Drawing.Size(50, 19);
            this.hsCleanCache.TabIndex = 86;
            this.hsCleanCache.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCleanCache_CheckedChanged);
            // 
            // hsNotificationSound
            // 
            this.hsNotificationSound.Location = new System.Drawing.Point(162, 998);
            this.hsNotificationSound.Name = "hsNotificationSound";
            this.hsNotificationSound.Size = new System.Drawing.Size(50, 19);
            this.hsNotificationSound.TabIndex = 86;
            this.hsNotificationSound.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsNotificationSound_CheckedChanged);
            // 
            // lbAutoClean
            // 
            this.lbAutoClean.AutoSize = true;
            this.lbAutoClean.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbAutoClean.Location = new System.Drawing.Point(14, 1310);
            this.lbAutoClean.Name = "lbAutoClean";
            this.lbAutoClean.Size = new System.Drawing.Size(115, 25);
            this.lbAutoClean.TabIndex = 82;
            this.lbAutoClean.Text = "Auto-Clean";
            // 
            // lbNotifSetting
            // 
            this.lbNotifSetting.AutoSize = true;
            this.lbNotifSetting.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbNotifSetting.Location = new System.Drawing.Point(14, 956);
            this.lbNotifSetting.Name = "lbNotifSetting";
            this.lbNotifSetting.Size = new System.Drawing.Size(133, 25);
            this.lbNotifSetting.TabIndex = 82;
            this.lbNotifSetting.Text = "Notifications";
            // 
            // lbDownloads
            // 
            this.lbDownloads.AutoSize = true;
            this.lbDownloads.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbDownloads.Location = new System.Drawing.Point(14, 816);
            this.lbDownloads.Name = "lbDownloads";
            this.lbDownloads.Size = new System.Drawing.Size(114, 25);
            this.lbDownloads.TabIndex = 79;
            this.lbDownloads.Text = "Downloads";
            // 
            // lbGeneral
            // 
            this.lbGeneral.AutoSize = true;
            this.lbGeneral.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbGeneral.Location = new System.Drawing.Point(14, 4);
            this.lbGeneral.Name = "lbGeneral";
            this.lbGeneral.Size = new System.Drawing.Size(83, 25);
            this.lbGeneral.TabIndex = 0;
            this.lbGeneral.Text = "General";
            // 
            // hsOpen
            // 
            this.hsOpen.Location = new System.Drawing.Point(174, 850);
            this.hsOpen.Name = "hsOpen";
            this.hsOpen.Size = new System.Drawing.Size(50, 19);
            this.hsOpen.TabIndex = 74;
            this.hsOpen.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsOpen_CheckedChanged);
            // 
            // hsDownload
            // 
            this.hsDownload.Location = new System.Drawing.Point(180, 881);
            this.hsDownload.Name = "hsDownload";
            this.hsDownload.Size = new System.Drawing.Size(50, 19);
            this.hsDownload.TabIndex = 75;
            this.hsDownload.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsDownload_CheckedChanged);
            // 
            // tbFolder
            // 
            this.tbFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFolder.Location = new System.Drawing.Point(161, 913);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(255, 21);
            this.tbFolder.TabIndex = 76;
            this.tbFolder.Tag = "";
            this.tbFolder.TextChanged += new System.EventHandler(this.tbFolder_TextChanged);
            // 
            // btDownloadFolder
            // 
            this.btDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDownloadFolder.AutoSize = true;
            this.btDownloadFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btDownloadFolder.DrawImage = true;
            this.btDownloadFolder.FlatAppearance.BorderSize = 0;
            this.btDownloadFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDownloadFolder.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.btDownloadFolder.Location = new System.Drawing.Point(416, 910);
            this.btDownloadFolder.Name = "btDownloadFolder";
            this.btDownloadFolder.Size = new System.Drawing.Size(27, 26);
            this.btDownloadFolder.TabIndex = 77;
            this.btDownloadFolder.Text = "...";
            this.btDownloadFolder.UseVisualStyleBackColor = true;
            this.btDownloadFolder.Click += new System.EventHandler(this.btDownloadFolder_Click);
            // 
            // lbDownloadFolder
            // 
            this.lbDownloadFolder.AutoSize = true;
            this.lbDownloadFolder.BackColor = System.Drawing.Color.Transparent;
            this.lbDownloadFolder.Location = new System.Drawing.Point(16, 916);
            this.lbDownloadFolder.Name = "lbDownloadFolder";
            this.lbDownloadFolder.Size = new System.Drawing.Size(135, 15);
            this.lbDownloadFolder.TabIndex = 78;
            this.lbDownloadFolder.Tag = "";
            this.lbDownloadFolder.Text = "Download to this folder:";
            // 
            // lbOpen
            // 
            this.lbOpen.AutoSize = true;
            this.lbOpen.BackColor = System.Drawing.Color.Transparent;
            this.lbOpen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbOpen.Location = new System.Drawing.Point(16, 854);
            this.lbOpen.Name = "lbOpen";
            this.lbOpen.Size = new System.Drawing.Size(149, 15);
            this.lbOpen.TabIndex = 80;
            this.lbOpen.Tag = "";
            this.lbOpen.Text = "Open files after download:";
            // 
            // lbAutoDownload
            // 
            this.lbAutoDownload.AutoSize = true;
            this.lbAutoDownload.BackColor = System.Drawing.Color.Transparent;
            this.lbAutoDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbAutoDownload.Location = new System.Drawing.Point(16, 884);
            this.lbAutoDownload.Name = "lbAutoDownload";
            this.lbAutoDownload.Size = new System.Drawing.Size(149, 15);
            this.lbAutoDownload.TabIndex = 81;
            this.lbAutoDownload.Tag = "";
            this.lbAutoDownload.Text = "Auto-download to a folder:";
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.tbUrl.Location = new System.Drawing.Point(54, 613);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(395, 21);
            this.tbUrl.TabIndex = 73;
            this.tbUrl.TextChanged += new System.EventHandler(this.tbUrl_TextChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.tbTitle.Location = new System.Drawing.Point(65, 580);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(384, 21);
            this.tbTitle.TabIndex = 72;
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
            this.tlpNewTab.Location = new System.Drawing.Point(19, 674);
            this.tlpNewTab.Name = "tlpNewTab";
            this.tlpNewTab.RowCount = 2;
            this.tlpNewTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNewTab.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNewTab.Size = new System.Drawing.Size(430, 133);
            this.tlpNewTab.TabIndex = 71;
            // 
            // L9
            // 
            this.L9.Controls.Add(this.L9T);
            this.L9.Controls.Add(this.L9U);
            this.L9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.L9.Location = new System.Drawing.Point(347, 69);
            this.L9.Name = "L9";
            this.L9.Size = new System.Drawing.Size(80, 61);
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
            this.L9T.Size = new System.Drawing.Size(80, 22);
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
            this.L9U.Size = new System.Drawing.Size(80, 19);
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
            this.L8.Location = new System.Drawing.Point(261, 69);
            this.L8.Name = "L8";
            this.L8.Size = new System.Drawing.Size(80, 61);
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
            this.L8T.Size = new System.Drawing.Size(80, 22);
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
            this.L8U.Size = new System.Drawing.Size(80, 19);
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
            this.L7.Location = new System.Drawing.Point(175, 69);
            this.L7.Name = "L7";
            this.L7.Size = new System.Drawing.Size(80, 61);
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
            this.L7T.Size = new System.Drawing.Size(80, 22);
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
            this.L7U.Size = new System.Drawing.Size(80, 19);
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
            this.L6.Location = new System.Drawing.Point(89, 69);
            this.L6.Name = "L6";
            this.L6.Size = new System.Drawing.Size(80, 61);
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
            this.L6T.Size = new System.Drawing.Size(80, 22);
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
            this.L6U.Size = new System.Drawing.Size(80, 19);
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
            this.L5.Size = new System.Drawing.Size(80, 61);
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
            this.L5T.Size = new System.Drawing.Size(80, 22);
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
            this.L5U.Size = new System.Drawing.Size(80, 19);
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
            this.L4.Location = new System.Drawing.Point(347, 3);
            this.L4.Name = "L4";
            this.L4.Size = new System.Drawing.Size(80, 60);
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
            this.L4T.Size = new System.Drawing.Size(80, 22);
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
            this.L4U.Size = new System.Drawing.Size(80, 19);
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
            this.L3.Location = new System.Drawing.Point(261, 3);
            this.L3.Name = "L3";
            this.L3.Size = new System.Drawing.Size(80, 60);
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
            this.L3T.Size = new System.Drawing.Size(80, 22);
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
            this.L3U.Size = new System.Drawing.Size(80, 19);
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
            this.L2.Location = new System.Drawing.Point(175, 3);
            this.L2.Name = "L2";
            this.L2.Size = new System.Drawing.Size(80, 60);
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
            this.L2T.Size = new System.Drawing.Size(80, 22);
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
            this.L2U.Size = new System.Drawing.Size(80, 19);
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
            this.L1.Location = new System.Drawing.Point(89, 3);
            this.L1.Name = "L1";
            this.L1.Size = new System.Drawing.Size(80, 60);
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
            this.L1T.Size = new System.Drawing.Size(80, 22);
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
            this.L1U.Size = new System.Drawing.Size(80, 19);
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
            this.L0.Size = new System.Drawing.Size(80, 60);
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
            this.L0T.Size = new System.Drawing.Size(78, 22);
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
            this.L0U.Size = new System.Drawing.Size(78, 19);
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
            this.btClear.Location = new System.Drawing.Point(19, 645);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(430, 23);
            this.btClear.TabIndex = 70;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // lbNTUrl
            // 
            this.lbNTUrl.AutoSize = true;
            this.lbNTUrl.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbNTUrl.Location = new System.Drawing.Point(15, 613);
            this.lbNTUrl.Name = "lbNTUrl";
            this.lbNTUrl.Size = new System.Drawing.Size(33, 19);
            this.lbNTUrl.TabIndex = 69;
            this.lbNTUrl.Text = "Url:";
            // 
            // lbNTTitle
            // 
            this.lbNTTitle.AutoSize = true;
            this.lbNTTitle.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.lbNTTitle.Location = new System.Drawing.Point(15, 580);
            this.lbNTTitle.Name = "lbNTTitle";
            this.lbNTTitle.Size = new System.Drawing.Size(44, 19);
            this.lbNTTitle.TabIndex = 68;
            this.lbNTTitle.Text = "Title:";
            // 
            // lbNewTabTitle
            // 
            this.lbNewTabTitle.AutoSize = true;
            this.lbNewTabTitle.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbNewTabTitle.Location = new System.Drawing.Point(14, 535);
            this.lbNewTabTitle.Name = "lbNewTabTitle";
            this.lbNewTabTitle.Size = new System.Drawing.Size(219, 25);
            this.lbNewTabTitle.TabIndex = 67;
            this.lbNewTabTitle.Text = "New Tab Page Content";
            // 
            // lbLang
            // 
            this.lbLang.AutoSize = true;
            this.lbLang.BackColor = System.Drawing.Color.Transparent;
            this.lbLang.Location = new System.Drawing.Point(16, 156);
            this.lbLang.Name = "lbLang";
            this.lbLang.Size = new System.Drawing.Size(66, 15);
            this.lbLang.TabIndex = 66;
            this.lbLang.Text = "Language:";
            // 
            // lbFlashInfo
            // 
            this.lbFlashInfo.AutoSize = true;
            this.lbFlashInfo.BackColor = System.Drawing.Color.Transparent;
            this.lbFlashInfo.Location = new System.Drawing.Point(16, 426);
            this.lbFlashInfo.Name = "lbFlashInfo";
            this.lbFlashInfo.Size = new System.Drawing.Size(310, 15);
            this.lbFlashInfo.TabIndex = 62;
            this.lbFlashInfo.Text = "Changes to Flash setting requires a restart to take effect.";
            // 
            // lbFlash
            // 
            this.lbFlash.AutoSize = true;
            this.lbFlash.BackColor = System.Drawing.Color.Transparent;
            this.lbFlash.Location = new System.Drawing.Point(16, 396);
            this.lbFlash.Name = "lbFlash";
            this.lbFlash.Size = new System.Drawing.Size(85, 15);
            this.lbFlash.TabIndex = 61;
            this.lbFlash.Text = "Enable Flash :";
            // 
            // hsFlash
            // 
            this.hsFlash.Location = new System.Drawing.Point(104, 396);
            this.hsFlash.Name = "hsFlash";
            this.hsFlash.Size = new System.Drawing.Size(50, 19);
            this.hsFlash.TabIndex = 60;
            this.hsFlash.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsFlash_CheckedChanged);
            // 
            // lbDNT
            // 
            this.lbDNT.AutoSize = true;
            this.lbDNT.BackColor = System.Drawing.Color.Transparent;
            this.lbDNT.Location = new System.Drawing.Point(16, 298);
            this.lbDNT.Name = "lbDNT";
            this.lbDNT.Size = new System.Drawing.Size(120, 15);
            this.lbDNT.TabIndex = 58;
            this.lbDNT.Text = "Enable DoNotTrack :";
            // 
            // hsDoNotTrack
            // 
            this.hsDoNotTrack.Checked = true;
            this.hsDoNotTrack.Location = new System.Drawing.Point(145, 298);
            this.hsDoNotTrack.Name = "hsDoNotTrack";
            this.hsDoNotTrack.Size = new System.Drawing.Size(50, 19);
            this.hsDoNotTrack.TabIndex = 50;
            this.hsDoNotTrack.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsDoNotTrack_CheckedChanged);
            // 
            // lbautoRestore
            // 
            this.lbautoRestore.AutoSize = true;
            this.lbautoRestore.BackColor = System.Drawing.Color.Transparent;
            this.lbautoRestore.Location = new System.Drawing.Point(16, 262);
            this.lbautoRestore.Name = "lbautoRestore";
            this.lbautoRestore.Size = new System.Drawing.Size(163, 15);
            this.lbautoRestore.TabIndex = 59;
            this.lbautoRestore.Text = "Restore Old Session at Start:";
            // 
            // lbLastProxy
            // 
            this.lbLastProxy.AutoSize = true;
            this.lbLastProxy.BackColor = System.Drawing.Color.Transparent;
            this.lbLastProxy.Location = new System.Drawing.Point(16, 228);
            this.lbLastProxy.Name = "lbLastProxy";
            this.lbLastProxy.Size = new System.Drawing.Size(147, 15);
            this.lbLastProxy.TabIndex = 57;
            this.lbLastProxy.Text = "Remember the last proxy:";
            // 
            // hsAutoRestore
            // 
            this.hsAutoRestore.Location = new System.Drawing.Point(193, 261);
            this.hsAutoRestore.Name = "hsAutoRestore";
            this.hsAutoRestore.Size = new System.Drawing.Size(50, 19);
            this.hsAutoRestore.TabIndex = 52;
            this.hsAutoRestore.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsAutoRestore_CheckedChanged);
            // 
            // hsProxy
            // 
            this.hsProxy.Location = new System.Drawing.Point(176, 227);
            this.hsProxy.Name = "hsProxy";
            this.hsProxy.Size = new System.Drawing.Size(50, 19);
            this.hsProxy.TabIndex = 51;
            this.hsProxy.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsProxy_CheckedChanged);
            // 
            // lbAtStartup
            // 
            this.lbAtStartup.AutoSize = true;
            this.lbAtStartup.BackColor = System.Drawing.Color.Transparent;
            this.lbAtStartup.Location = new System.Drawing.Point(16, 121);
            this.lbAtStartup.Name = "lbAtStartup";
            this.lbAtStartup.Size = new System.Drawing.Size(65, 15);
            this.lbAtStartup.TabIndex = 56;
            this.lbAtStartup.Text = "At Startup: ";
            // 
            // lbShowFavorites
            // 
            this.lbShowFavorites.AutoSize = true;
            this.lbShowFavorites.BackColor = System.Drawing.Color.Transparent;
            this.lbShowFavorites.Location = new System.Drawing.Point(16, 198);
            this.lbShowFavorites.Name = "lbShowFavorites";
            this.lbShowFavorites.Size = new System.Drawing.Size(128, 15);
            this.lbShowFavorites.TabIndex = 55;
            this.lbShowFavorites.Text = "Show Favorites Menu:";
            // 
            // hsFav
            // 
            this.hsFav.Checked = true;
            this.hsFav.Location = new System.Drawing.Point(147, 197);
            this.hsFav.Name = "hsFav";
            this.hsFav.Size = new System.Drawing.Size(50, 19);
            this.hsFav.TabIndex = 48;
            this.hsFav.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsFav_CheckedChanged);
            // 
            // rbNewTab
            // 
            this.rbNewTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbNewTab.AutoSize = true;
            this.rbNewTab.BackColor = System.Drawing.Color.Transparent;
            this.rbNewTab.Location = new System.Drawing.Point(378, 41);
            this.rbNewTab.Name = "rbNewTab";
            this.rbNewTab.Size = new System.Drawing.Size(74, 19);
            this.rbNewTab.TabIndex = 46;
            this.rbNewTab.TabStop = true;
            this.rbNewTab.Tag = "";
            this.rbNewTab.Text = "New Tab";
            this.rbNewTab.UseVisualStyleBackColor = false;
            this.rbNewTab.CheckedChanged += new System.EventHandler(this.rbNewTab_CheckedChanged);
            // 
            // lbHomepage
            // 
            this.lbHomepage.AutoSize = true;
            this.lbHomepage.BackColor = System.Drawing.Color.Transparent;
            this.lbHomepage.Location = new System.Drawing.Point(16, 43);
            this.lbHomepage.Name = "lbHomepage";
            this.lbHomepage.Size = new System.Drawing.Size(79, 15);
            this.lbHomepage.TabIndex = 54;
            this.lbHomepage.Tag = "";
            this.lbHomepage.Text = "Home Page :";
            // 
            // lbSearchEngine
            // 
            this.lbSearchEngine.AutoSize = true;
            this.lbSearchEngine.BackColor = System.Drawing.Color.Transparent;
            this.lbSearchEngine.Location = new System.Drawing.Point(16, 82);
            this.lbSearchEngine.Name = "lbSearchEngine";
            this.lbSearchEngine.Size = new System.Drawing.Size(94, 15);
            this.lbSearchEngine.TabIndex = 53;
            this.lbSearchEngine.Tag = "";
            this.lbSearchEngine.Text = "Search Engine :";
            // 
            // tbHomepage
            // 
            this.tbHomepage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbHomepage.Location = new System.Drawing.Point(104, 39);
            this.tbHomepage.Name = "tbHomepage";
            this.tbHomepage.Size = new System.Drawing.Size(266, 21);
            this.tbHomepage.TabIndex = 45;
            this.tbHomepage.Tag = "";
            this.tbHomepage.TextChanged += new System.EventHandler(this.tbHomepage_TextChanged);
            // 
            // tbLang
            // 
            this.tbLang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLang.Location = new System.Drawing.Point(87, 153);
            this.tbLang.Name = "tbLang";
            this.tbLang.ReadOnly = true;
            this.tbLang.Size = new System.Drawing.Size(365, 21);
            this.tbLang.TabIndex = 49;
            this.tbLang.Tag = "";
            this.tbLang.Click += new System.EventHandler(this.tbLang_Click);
            // 
            // tbStartup
            // 
            this.tbStartup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStartup.Location = new System.Drawing.Point(87, 118);
            this.tbStartup.Name = "tbStartup";
            this.tbStartup.ReadOnly = true;
            this.tbStartup.Size = new System.Drawing.Size(365, 21);
            this.tbStartup.TabIndex = 49;
            this.tbStartup.Tag = "";
            this.tbStartup.Click += new System.EventHandler(this.tbStartup_Click);
            // 
            // tbSearchEngine
            // 
            this.tbSearchEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearchEngine.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbSearchEngine.Location = new System.Drawing.Point(116, 79);
            this.tbSearchEngine.Name = "tbSearchEngine";
            this.tbSearchEngine.ReadOnly = true;
            this.tbSearchEngine.Size = new System.Drawing.Size(336, 21);
            this.tbSearchEngine.TabIndex = 47;
            this.tbSearchEngine.Tag = "";
            this.tbSearchEngine.Click += new System.EventHandler(this.tbSearchEngine_Click);
            // 
            // tpBlock
            // 
            this.tpBlock.Location = new System.Drawing.Point(4, 24);
            this.tpBlock.Name = "tpBlock";
            this.tpBlock.Size = new System.Drawing.Size(597, 473);
            this.tpBlock.TabIndex = 7;
            this.tpBlock.Text = "Blocked Sites";
            this.tpBlock.UseVisualStyleBackColor = true;
            // 
            // tpSite
            // 
            this.tpSite.Location = new System.Drawing.Point(4, 24);
            this.tpSite.Name = "tpSite";
            this.tpSite.Padding = new System.Windows.Forms.Padding(3);
            this.tpSite.Size = new System.Drawing.Size(597, 473);
            this.tpSite.TabIndex = 1;
            this.tpSite.Text = "Site Settings";
            this.tpSite.UseVisualStyleBackColor = true;
            // 
            // tpSchedules
            // 
            this.tpSchedules.Location = new System.Drawing.Point(4, 24);
            this.tpSchedules.Name = "tpSchedules";
            this.tpSchedules.Size = new System.Drawing.Size(597, 473);
            this.tpSchedules.TabIndex = 2;
            this.tpSchedules.Text = "Schedules";
            this.tpSchedules.UseVisualStyleBackColor = true;
            // 
            // tpHistory
            // 
            this.tpHistory.Location = new System.Drawing.Point(4, 24);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Size = new System.Drawing.Size(597, 473);
            this.tpHistory.TabIndex = 3;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // tpDownloads
            // 
            this.tpDownloads.Location = new System.Drawing.Point(4, 24);
            this.tpDownloads.Name = "tpDownloads";
            this.tpDownloads.Size = new System.Drawing.Size(597, 473);
            this.tpDownloads.TabIndex = 4;
            this.tpDownloads.Text = "Downloads";
            this.tpDownloads.UseVisualStyleBackColor = true;
            // 
            // tpCollections
            // 
            this.tpCollections.Location = new System.Drawing.Point(4, 24);
            this.tpCollections.Name = "tpCollections";
            this.tpCollections.Size = new System.Drawing.Size(597, 473);
            this.tpCollections.TabIndex = 5;
            this.tpCollections.Text = "Collections";
            this.tpCollections.UseVisualStyleBackColor = true;
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.p32bit);
            this.tpAbout.Controls.Add(this.btReset);
            this.tpAbout.Controls.Add(this.lbUpdateStatus);
            this.tpAbout.Controls.Add(this.btUpdater);
            this.tpAbout.Controls.Add(this.llLicenses);
            this.tpAbout.Controls.Add(this.label21);
            this.tpAbout.Controls.Add(this.lbVersion);
            this.tpAbout.Controls.Add(this.label20);
            this.tpAbout.Controls.Add(this.lbKorot);
            this.tpAbout.Controls.Add(this.pictureBox5);
            this.tpAbout.Location = new System.Drawing.Point(4, 24);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Size = new System.Drawing.Size(597, 473);
            this.tpAbout.TabIndex = 6;
            this.tpAbout.Text = "About";
            this.tpAbout.UseVisualStyleBackColor = true;
            // 
            // p32bit
            // 
            this.p32bit.Controls.Add(this.l32title);
            this.p32bit.Controls.Add(this.l32desc);
            this.p32bit.Controls.Add(this.ll32bit);
            this.p32bit.Location = new System.Drawing.Point(18, 287);
            this.p32bit.Name = "p32bit";
            this.p32bit.Size = new System.Drawing.Size(560, 100);
            this.p32bit.TabIndex = 16;
            // 
            // l32title
            // 
            this.l32title.AutoSize = true;
            this.l32title.BackColor = System.Drawing.Color.Transparent;
            this.l32title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.l32title.Location = new System.Drawing.Point(3, 0);
            this.l32title.Name = "l32title";
            this.l32title.Size = new System.Drawing.Size(370, 25);
            this.l32title.TabIndex = 9;
            this.l32title.Text = "You are using unsupported build of Korot.";
            // 
            // l32desc
            // 
            this.l32desc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.l32desc.AutoSize = true;
            this.l32desc.BackColor = System.Drawing.Color.Transparent;
            this.l32desc.Location = new System.Drawing.Point(3, 25);
            this.l32desc.Name = "l32desc";
            this.l32desc.Size = new System.Drawing.Size(459, 30);
            this.l32desc.TabIndex = 13;
            this.l32desc.Text = "32-bit builds for Korot are now unsupported and updates for your machine is halte" +
    "d.\r\nYou still can build your own Korot from source code.";
            // 
            // ll32bit
            // 
            this.ll32bit.AutoSize = true;
            this.ll32bit.BackColor = System.Drawing.Color.Transparent;
            this.ll32bit.Location = new System.Drawing.Point(468, 25);
            this.ll32bit.Name = "ll32bit";
            this.ll32bit.Size = new System.Drawing.Size(80, 15);
            this.ll32bit.TabIndex = 7;
            this.ll32bit.TabStop = true;
            this.ll32bit.Text = "Learn more...";
            this.ll32bit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ll32bit_LinkClicked);
            // 
            // btReset
            // 
            this.btReset.AutoSize = true;
            this.btReset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.btReset.DrawImage = true;
            this.btReset.FlatAppearance.BorderSize = 0;
            this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btReset.Location = new System.Drawing.Point(18, 183);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(105, 27);
            this.btReset.TabIndex = 15;
            this.btReset.Text = "Reset Korot...";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // lbUpdateStatus
            // 
            this.lbUpdateStatus.AutoSize = true;
            this.lbUpdateStatus.BackColor = System.Drawing.Color.Transparent;
            this.lbUpdateStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbUpdateStatus.Location = new System.Drawing.Point(15, 227);
            this.lbUpdateStatus.Name = "lbUpdateStatus";
            this.lbUpdateStatus.Size = new System.Drawing.Size(156, 17);
            this.lbUpdateStatus.TabIndex = 6;
            this.lbUpdateStatus.Text = "Checking for Updates...";
            this.lbUpdateStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btUpdater
            // 
            this.btUpdater.AutoSize = true;
            this.btUpdater.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btUpdater.BackColor = System.Drawing.Color.Transparent;
            this.btUpdater.DrawImage = true;
            this.btUpdater.FlatAppearance.BorderSize = 0;
            this.btUpdater.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUpdater.Location = new System.Drawing.Point(18, 247);
            this.btUpdater.Name = "btUpdater";
            this.btUpdater.Size = new System.Drawing.Size(117, 25);
            this.btUpdater.TabIndex = 11;
            this.btUpdater.Text = "Check for Updates";
            this.btUpdater.UseVisualStyleBackColor = false;
            this.btUpdater.Click += new System.EventHandler(this.btUpdater_Click);
            // 
            // llLicenses
            // 
            this.llLicenses.AutoSize = true;
            this.llLicenses.BackColor = System.Drawing.Color.Transparent;
            this.llLicenses.Location = new System.Drawing.Point(15, 152);
            this.llLicenses.Name = "llLicenses";
            this.llLicenses.Size = new System.Drawing.Size(163, 15);
            this.llLicenses.TabIndex = 7;
            this.llLicenses.TabStop = true;
            this.llLicenses.Text = "Licenses && Special Thanks...";
            this.llLicenses.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llLicenses_LinkClicked);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Location = new System.Drawing.Point(15, 104);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(361, 45);
            this.label21.TabIndex = 13;
            this.label21.Text = "Korot uses Chromium by Google using CefSharp.\r\nKorot is written in C# using Visua" +
    "l Studio Community by Microsoft.\r\nKorot uses modified version of EasyTabs.";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.BackColor = System.Drawing.Color.Transparent;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbVersion.Location = new System.Drawing.Point(171, 20);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(52, 16);
            this.lbVersion.TabIndex = 8;
            this.lbVersion.Text = "version";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label20.Location = new System.Drawing.Point(71, 62);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 25);
            this.label20.TabIndex = 9;
            this.label20.Text = "Haltroy";
            // 
            // lbKorot
            // 
            this.lbKorot.AutoSize = true;
            this.lbKorot.BackColor = System.Drawing.Color.Transparent;
            this.lbKorot.Font = new System.Drawing.Font("Microsoft Sans Serif", 24.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKorot.Location = new System.Drawing.Point(64, 20);
            this.lbKorot.Name = "lbKorot";
            this.lbKorot.Size = new System.Drawing.Size(95, 38);
            this.lbKorot.TabIndex = 10;
            this.lbKorot.Text = "Korot";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::Korot.Properties.Resources.Korot;
            this.pictureBox5.Location = new System.Drawing.Point(18, 21);
            this.pictureBox5.MaximumSize = new System.Drawing.Size(44, 41);
            this.pictureBox5.MinimumSize = new System.Drawing.Size(44, 41);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(44, 41);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // pTitle
            // 
            this.pTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pTitle.Controls.Add(this.btBack);
            this.pTitle.Controls.Add(this.btClose);
            this.pTitle.Controls.Add(this.lbTitle);
            this.pTitle.Location = new System.Drawing.Point(137, 0);
            this.pTitle.Name = "pTitle";
            this.pTitle.Size = new System.Drawing.Size(595, 46);
            this.pTitle.TabIndex = 2;
            // 
            // btBack
            // 
            this.btBack.BackColor = System.Drawing.Color.Transparent;
            this.btBack.ButtonImage = global::Korot.Properties.Resources.leftarrow;
            this.btBack.DrawImage = true;
            this.btBack.FlatAppearance.BorderSize = 0;
            this.btBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBack.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btBack.Location = new System.Drawing.Point(9, 7);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(30, 30);
            this.btBack.TabIndex = 1;
            this.btBack.UseVisualStyleBackColor = false;
            this.btBack.Click += new System.EventHandler(this.htButton1_Click);
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
            this.btClose.Location = new System.Drawing.Point(554, 9);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(30, 30);
            this.btClose.TabIndex = 1;
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // pSidebar
            // 
            this.pSidebar.Controls.Add(this.lbAbout);
            this.pSidebar.Controls.Add(this.btSidebar);
            this.pSidebar.Controls.Add(this.lbCollections);
            this.pSidebar.Controls.Add(this.lbDownload);
            this.pSidebar.Controls.Add(this.lbHistory);
            this.pSidebar.Controls.Add(this.lbSettings);
            this.pSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pSidebar.Location = new System.Drawing.Point(0, 0);
            this.pSidebar.Name = "pSidebar";
            this.pSidebar.Size = new System.Drawing.Size(137, 516);
            this.pSidebar.TabIndex = 3;
            // 
            // lbAbout
            // 
            this.lbAbout.AutoSize = true;
            this.lbAbout.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbAbout.Location = new System.Drawing.Point(12, 189);
            this.lbAbout.Name = "lbAbout";
            this.lbAbout.Size = new System.Drawing.Size(69, 25);
            this.lbAbout.TabIndex = 0;
            this.lbAbout.Text = "About";
            this.lbAbout.Click += new System.EventHandler(this.label8_Click);
            // 
            // btSidebar
            // 
            this.btSidebar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btSidebar.BackColor = System.Drawing.Color.Transparent;
            this.btSidebar.ButtonImage = global::Korot.Properties.Resources.cancel;
            this.btSidebar.DrawImage = true;
            this.btSidebar.FlatAppearance.BorderSize = 0;
            this.btSidebar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSidebar.ImageSizeMode = HTAlt.WinForms.HTButton.ButtonImageSizeMode.Zoom;
            this.btSidebar.Location = new System.Drawing.Point(96, 7);
            this.btSidebar.Name = "btSidebar";
            this.btSidebar.Size = new System.Drawing.Size(30, 30);
            this.btSidebar.TabIndex = 1;
            this.btSidebar.UseVisualStyleBackColor = false;
            this.btSidebar.Click += new System.EventHandler(this.btSidebar_Click);
            // 
            // lbCollections
            // 
            this.lbCollections.AutoSize = true;
            this.lbCollections.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbCollections.Location = new System.Drawing.Point(12, 150);
            this.lbCollections.Name = "lbCollections";
            this.lbCollections.Size = new System.Drawing.Size(116, 25);
            this.lbCollections.TabIndex = 0;
            this.lbCollections.Text = "Collections";
            this.lbCollections.Click += new System.EventHandler(this.label7_Click);
            // 
            // lbDownload
            // 
            this.lbDownload.AutoSize = true;
            this.lbDownload.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbDownload.Location = new System.Drawing.Point(12, 114);
            this.lbDownload.Name = "lbDownload";
            this.lbDownload.Size = new System.Drawing.Size(114, 25);
            this.lbDownload.TabIndex = 0;
            this.lbDownload.Text = "Downloads";
            this.lbDownload.Click += new System.EventHandler(this.label6_Click);
            // 
            // lbHistory
            // 
            this.lbHistory.AutoSize = true;
            this.lbHistory.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbHistory.Location = new System.Drawing.Point(12, 79);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.Size = new System.Drawing.Size(78, 25);
            this.lbHistory.TabIndex = 0;
            this.lbHistory.Text = "History";
            this.lbHistory.Click += new System.EventHandler(this.label5_Click);
            // 
            // lbSettings
            // 
            this.lbSettings.AutoSize = true;
            this.lbSettings.Font = new System.Drawing.Font("Ubuntu", 15F, System.Drawing.FontStyle.Bold);
            this.lbSettings.Location = new System.Drawing.Point(12, 47);
            this.lbSettings.Name = "lbSettings";
            this.lbSettings.Size = new System.Drawing.Size(93, 26);
            this.lbSettings.TabIndex = 0;
            this.lbSettings.Text = "Settings";
            this.lbSettings.Click += new System.EventHandler(this.label4_Click);
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
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // ımageFromLocalFileToolStripMenuItem
            // 
            this.ımageFromLocalFileToolStripMenuItem.Name = "ımageFromLocalFileToolStripMenuItem";
            this.ımageFromLocalFileToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ımageFromLocalFileToolStripMenuItem.Text = "Image from Local File";
            this.ımageFromLocalFileToolStripMenuItem.Click += new System.EventHandler(this.ımageFromLocalFileToolStripMenuItem_Click);
            // 
            // ımageFromURLToolStripMenuItem
            // 
            this.ımageFromURLToolStripMenuItem.Name = "ımageFromURLToolStripMenuItem";
            this.ımageFromURLToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.ımageFromURLToolStripMenuItem.Text = "Image from Code";
            this.ımageFromURLToolStripMenuItem.Click += new System.EventHandler(this.ımageFromURLToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmsLanguage
            // 
            this.cmsLanguage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.langTSMI,
            this.tsSepLang,
            this.LOpenTSMI,
            this.LWebStoreTSMI});
            this.cmsLanguage.Name = "cmsLanguage";
            this.cmsLanguage.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsLanguage.Size = new System.Drawing.Size(140, 76);
            this.cmsLanguage.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLanguage_Opening);
            // 
            // langTSMI
            // 
            this.langTSMI.Name = "langTSMI";
            this.langTSMI.Size = new System.Drawing.Size(139, 22);
            this.langTSMI.Text = "[Lang]";
            this.langTSMI.Click += new System.EventHandler(this.langTSMI_Click);
            // 
            // tsSepLang
            // 
            this.tsSepLang.Name = "tsSepLang";
            this.tsSepLang.Size = new System.Drawing.Size(136, 6);
            // 
            // LOpenTSMI
            // 
            this.LOpenTSMI.Name = "LOpenTSMI";
            this.LOpenTSMI.Size = new System.Drawing.Size(139, 22);
            this.LOpenTSMI.Text = "Open Folder";
            this.LOpenTSMI.Click += new System.EventHandler(this.LOpenTSMI_Click);
            // 
            // LWebStoreTSMI
            // 
            this.LWebStoreTSMI.Name = "LWebStoreTSMI";
            this.LWebStoreTSMI.Size = new System.Drawing.Size(139, 22);
            this.LWebStoreTSMI.Text = "Web Store";
            this.LWebStoreTSMI.Click += new System.EventHandler(this.LWebStoreTSMI_Click);
            // 
            // cmsTheme
            // 
            this.cmsTheme.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newTSMI,
            this.themeTSMI,
            this.tsSepTheme,
            this.TOpenTSMI,
            this.TWebStoreTSMI});
            this.cmsTheme.Name = "cmsLanguage";
            this.cmsTheme.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsTheme.Size = new System.Drawing.Size(140, 98);
            this.cmsTheme.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTheme_Opening);
            // 
            // newTSMI
            // 
            this.newTSMI.Name = "newTSMI";
            this.newTSMI.Size = new System.Drawing.Size(139, 22);
            this.newTSMI.Text = "<new>";
            this.newTSMI.Click += new System.EventHandler(this.newTSMI_Click);
            // 
            // themeTSMI
            // 
            this.themeTSMI.Name = "themeTSMI";
            this.themeTSMI.Size = new System.Drawing.Size(139, 22);
            this.themeTSMI.Text = "[Theme]";
            this.themeTSMI.Click += new System.EventHandler(this.themeTSMI_Click);
            // 
            // tsSepTheme
            // 
            this.tsSepTheme.Name = "tsSepTheme";
            this.tsSepTheme.Size = new System.Drawing.Size(136, 6);
            // 
            // TOpenTSMI
            // 
            this.TOpenTSMI.Name = "TOpenTSMI";
            this.TOpenTSMI.Size = new System.Drawing.Size(139, 22);
            this.TOpenTSMI.Text = "Open Folder";
            this.TOpenTSMI.Click += new System.EventHandler(this.TOpenTSMI_Click);
            // 
            // TWebStoreTSMI
            // 
            this.TWebStoreTSMI.Name = "TWebStoreTSMI";
            this.TWebStoreTSMI.Size = new System.Drawing.Size(139, 22);
            this.TWebStoreTSMI.Text = "Web Store";
            this.TWebStoreTSMI.Click += new System.EventHandler(this.TWebStoreTSMI_Click);
            // 
            // lbSynthVol
            // 
            this.lbSynthVol.AutoSize = true;
            this.lbSynthVol.BackColor = System.Drawing.Color.Transparent;
            this.lbSynthVol.Location = new System.Drawing.Point(16, 331);
            this.lbSynthVol.Name = "lbSynthVol";
            this.lbSynthVol.Size = new System.Drawing.Size(130, 15);
            this.lbSynthVol.TabIndex = 58;
            this.lbSynthVol.Text = "Speech Synth Volume:";
            // 
            // lbSynthRate
            // 
            this.lbSynthRate.AutoSize = true;
            this.lbSynthRate.BackColor = System.Drawing.Color.Transparent;
            this.lbSynthRate.Location = new System.Drawing.Point(16, 360);
            this.lbSynthRate.Name = "lbSynthRate";
            this.lbSynthRate.Size = new System.Drawing.Size(114, 15);
            this.lbSynthRate.TabIndex = 58;
            this.lbSynthRate.Text = "Speech Synth Rate:";
            // 
            // nudSynthVol
            // 
            this.nudSynthVol.Location = new System.Drawing.Point(158, 329);
            this.nudSynthVol.Name = "nudSynthVol";
            this.nudSynthVol.Size = new System.Drawing.Size(55, 21);
            this.nudSynthVol.TabIndex = 131;
            this.nudSynthVol.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudSynthVol.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // nudSynthRate
            // 
            this.nudSynthRate.Location = new System.Drawing.Point(147, 358);
            this.nudSynthRate.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudSynthRate.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.nudSynthRate.Name = "nudSynthRate";
            this.nudSynthRate.Size = new System.Drawing.Size(55, 21);
            this.nudSynthRate.TabIndex = 131;
            this.nudSynthRate.Value = new decimal(new int[] {
            2,
            0,
            0,
            -2147483648});
            this.nudSynthRate.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 516);
            this.Controls.Add(this.pSidebar);
            this.Controls.Add(this.pTitle);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSettings";
            this.Text = "frmSettings";
            this.tabControl1.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.pCleanDownload.ResumeLayout(false);
            this.pCleanDownload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCDOld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCDDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCDFile)).EndInit();
            this.pCleanLog.ResumeLayout(false);
            this.pCleanLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCLOld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCLDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCLFile)).EndInit();
            this.pCleanHistory.ResumeLayout(false);
            this.pCleanHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCHOld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCHDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCHFile)).EndInit();
            this.pCleanCache.ResumeLayout(false);
            this.pCleanCache.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCC2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCC1)).EndInit();
            this.pSchedule.ResumeLayout(false);
            this.pSchedule.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.pbForeColor)).EndInit();
            this.flpClose.ResumeLayout(false);
            this.flpClose.PerformLayout();
            this.flpNewTab.ResumeLayout(false);
            this.flpNewTab.PerformLayout();
            this.flpLayout.ResumeLayout(false);
            this.flpLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOverlay)).EndInit();
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
            this.tpAbout.ResumeLayout(false);
            this.tpAbout.PerformLayout();
            this.p32bit.ResumeLayout(false);
            this.p32bit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.pTitle.ResumeLayout(false);
            this.pTitle.PerformLayout();
            this.pSidebar.ResumeLayout(false);
            this.pSidebar.PerformLayout();
            this.cmsStartup.ResumeLayout(false);
            this.cmsSearchEngine.ResumeLayout(false);
            this.cmsBStyle.ResumeLayout(false);
            this.cmsLanguage.ResumeLayout(false);
            this.cmsTheme.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSynthVol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSynthRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.TabPage tpSite;
        private System.Windows.Forms.Label lbLang;
        private System.Windows.Forms.Label lbFlashInfo;
        private System.Windows.Forms.Label lbFlash;
        private HTAlt.WinForms.HTSwitch hsFlash;
        private System.Windows.Forms.Label lbDNT;
        private HTAlt.WinForms.HTSwitch hsDoNotTrack;
        private System.Windows.Forms.Label lbautoRestore;
        private System.Windows.Forms.Label lbLastProxy;
        private HTAlt.WinForms.HTSwitch hsAutoRestore;
        private HTAlt.WinForms.HTSwitch hsProxy;
        private System.Windows.Forms.Label lbAtStartup;
        private System.Windows.Forms.Label lbShowFavorites;
        private HTAlt.WinForms.HTSwitch hsFav;
        private System.Windows.Forms.RadioButton rbNewTab;
        private System.Windows.Forms.Label lbHomepage;
        private System.Windows.Forms.Label lbSearchEngine;
        private System.Windows.Forms.TextBox tbHomepage;
        private System.Windows.Forms.TextBox tbStartup;
        private System.Windows.Forms.TextBox tbSearchEngine;
        private System.Windows.Forms.Panel pTitle;
        public HTAlt.WinForms.HTButton btClose;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TableLayoutPanel tlpNewTab;
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
        private HTAlt.WinForms.HTButton btClear;
        private System.Windows.Forms.Label lbNTUrl;
        private System.Windows.Forms.Label lbNTTitle;
        private System.Windows.Forms.Label lbNewTabTitle;
        private System.Windows.Forms.Label lbDownloads;
        private HTAlt.WinForms.HTSwitch hsOpen;
        private HTAlt.WinForms.HTSwitch hsDownload;
        private System.Windows.Forms.TextBox tbFolder;
        private HTAlt.WinForms.HTButton btDownloadFolder;
        private System.Windows.Forms.Label lbDownloadFolder;
        private System.Windows.Forms.Label lbOpen;
        private System.Windows.Forms.Label lbAutoDownload;
        private HTAlt.WinForms.HTButton btOpenSound;
        private System.Windows.Forms.TextBox tbSoundLoc;
        private System.Windows.Forms.Label lbSchedule;
        private System.Windows.Forms.Label lbSilentMode;
        private HTAlt.WinForms.HTSwitch hsSchedule;
        private HTAlt.WinForms.HTSwitch hsSilent;
        private System.Windows.Forms.Label lbDefaultNotifSound;
        private HTAlt.WinForms.HTSwitch hsDefaultSound;
        private System.Windows.Forms.Label lbPlayNotifSound;
        private HTAlt.WinForms.HTSwitch hsNotificationSound;
        private System.Windows.Forms.Label lbNotifSetting;
        private System.Windows.Forms.Label lbGeneral;
        private HTAlt.WinForms.HTSwitch hsAutoForeColor;
        private System.Windows.Forms.Label lbAutoSelect;
        private HTAlt.WinForms.HTSwitch hsNinja;
        private System.Windows.Forms.Label lbNinja;
        private System.Windows.Forms.PictureBox pbForeColor;
        private System.Windows.Forms.Label lbForeColor;
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
        private System.Windows.Forms.Label lbBackImageStyle;
        private System.Windows.Forms.Label lbCloseColor;
        private System.Windows.Forms.Label lbNewTabColor;
        private System.Windows.Forms.Label lbBackImage;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.PictureBox pbOverlay;
        private System.Windows.Forms.Label lbBackColor;
        private System.Windows.Forms.Label lbOveralColor;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label lbThemeName;
        private System.Windows.Forms.Label lbLayout;
        private System.Windows.Forms.Label lbTheme;
        public HTAlt.WinForms.HTButton btBack;
        private System.Windows.Forms.Panel pSidebar;
        private System.Windows.Forms.Label lbAbout;
        private System.Windows.Forms.Label lbCollections;
        private System.Windows.Forms.Label lbDownload;
        private System.Windows.Forms.Label lbHistory;
        private System.Windows.Forms.Label lbSettings;
        private HTAlt.WinForms.HTButton btCookie;
        private System.Windows.Forms.TabPage tpSchedules;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.TabPage tpDownloads;
        private System.Windows.Forms.TabPage tpCollections;
        private System.Windows.Forms.TabPage tpAbout;
        private System.Windows.Forms.Label lbUpdateStatus;
        private HTAlt.WinForms.HTButton btUpdater;
        private System.Windows.Forms.LinkLabel llLicenses;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lbKorot;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.ContextMenuStrip cmsStartup;
        private System.Windows.Forms.ToolStripMenuItem showNewTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHomepageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAWebsiteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsSearchEngine;
        private System.Windows.Forms.ToolStripMenuItem googleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yandexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yaaniToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duckDuckGoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem baiduToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wolframalphaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aOLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yahooToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem askToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ınternetArchiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsBStyle;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ımageFromLocalFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ımageFromURLToolStripMenuItem;
        private HTAlt.WinForms.HTButton btReset;
        private HTAlt.WinForms.HTButton btThemeWizard;
        private HTAlt.WinForms.HTButton btBlocked;
        private System.Windows.Forms.TabPage tpBlock;
        private System.Windows.Forms.Panel pSchedule;
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
        private System.Windows.Forms.Label lb24HType;
        private System.Windows.Forms.Label scheduleFrom;
        private System.Windows.Forms.FlowLayoutPanel flpTo;
        private System.Windows.Forms.NumericUpDown toHour;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown toMin;
        private System.Windows.Forms.Label scheduleEvery;
        private System.Windows.Forms.Label scheduleTo;
        private System.Windows.Forms.Timer timer1;
        public HTAlt.WinForms.HTButton btSidebar;
        private System.Windows.Forms.ContextMenuStrip cmsLanguage;
        private System.Windows.Forms.ToolStripMenuItem langTSMI;
        private System.Windows.Forms.ToolStripSeparator tsSepLang;
        private System.Windows.Forms.ToolStripMenuItem LOpenTSMI;
        private System.Windows.Forms.ToolStripMenuItem LWebStoreTSMI;
        private System.Windows.Forms.ContextMenuStrip cmsTheme;
        private System.Windows.Forms.ToolStripMenuItem newTSMI;
        private System.Windows.Forms.ToolStripMenuItem themeTSMI;
        private System.Windows.Forms.ToolStripSeparator tsSepTheme;
        private System.Windows.Forms.ToolStripMenuItem TOpenTSMI;
        private System.Windows.Forms.ToolStripMenuItem TWebStoreTSMI;
        private System.Windows.Forms.TextBox tbLang;
        private System.Windows.Forms.TextBox tbTheme;
        private System.Windows.Forms.Label lbCleanCache;
        private HTAlt.WinForms.HTSwitch hsCleanCache;
        private System.Windows.Forms.Label lbAutoClean;
        private System.Windows.Forms.Panel pCleanDownload;
        private System.Windows.Forms.NumericUpDown nudCDOld;
        private System.Windows.Forms.NumericUpDown nudCDDay;
        private System.Windows.Forms.NumericUpDown nudCDFile;
        private System.Windows.Forms.Label lbCD6;
        private System.Windows.Forms.Label lbCD4;
        private System.Windows.Forms.Label lbCD2;
        private System.Windows.Forms.Label lbCD5;
        private System.Windows.Forms.Label lbCD3;
        private HTAlt.WinForms.HTSwitch hsCDOld;
        private System.Windows.Forms.Label lbCD1;
        private HTAlt.WinForms.HTSwitch hsCDDay;
        private HTAlt.WinForms.HTSwitch hsCDFile;
        private System.Windows.Forms.Label lbCleanDownload;
        private HTAlt.WinForms.HTSwitch hsCleanDownload;
        private System.Windows.Forms.Panel pCleanLog;
        private System.Windows.Forms.NumericUpDown nudCLOld;
        private System.Windows.Forms.NumericUpDown nudCLDay;
        private System.Windows.Forms.NumericUpDown nudCLFile;
        private System.Windows.Forms.Label lbCL4;
        private System.Windows.Forms.Label lbCL6;
        private System.Windows.Forms.Label lbCL2;
        private System.Windows.Forms.Label lbCL3;
        private System.Windows.Forms.Label lbCL1;
        private System.Windows.Forms.Label lbCL5;
        private HTAlt.WinForms.HTSwitch hsCLDay;
        private HTAlt.WinForms.HTSwitch hsCLFile;
        private HTAlt.WinForms.HTSwitch hsCLOld;
        private System.Windows.Forms.Label lbCleanLog;
        private HTAlt.WinForms.HTSwitch hsCleanLog;
        private HTAlt.WinForms.HTButton btClean;
        private System.Windows.Forms.Panel pCleanHistory;
        private System.Windows.Forms.NumericUpDown nudCHOld;
        private System.Windows.Forms.NumericUpDown nudCHDay;
        private System.Windows.Forms.NumericUpDown nudCHFile;
        private System.Windows.Forms.Label lbCH4;
        private System.Windows.Forms.Label lbCH2;
        private System.Windows.Forms.Label lbCH6;
        private System.Windows.Forms.Label lbCH3;
        private System.Windows.Forms.Label lbCH1;
        private HTAlt.WinForms.HTSwitch hsCHDay;
        private HTAlt.WinForms.HTSwitch hsCHFile;
        private System.Windows.Forms.Label lbCH5;
        private HTAlt.WinForms.HTSwitch hsCHOld;
        private System.Windows.Forms.Panel pCleanCache;
        private System.Windows.Forms.NumericUpDown nudCC2;
        private System.Windows.Forms.NumericUpDown nudCC1;
        private System.Windows.Forms.Label lbCC4;
        private System.Windows.Forms.Label lbCC2;
        private System.Windows.Forms.Label lbCC3;
        private System.Windows.Forms.Label lbCC1;
        private HTAlt.WinForms.HTSwitch hsCC2;
        private HTAlt.WinForms.HTSwitch hsCC1;
        private System.Windows.Forms.Label lbCleanHistory;
        private HTAlt.WinForms.HTSwitch hsCleanHistory;
        private System.Windows.Forms.Label l32title;
        private System.Windows.Forms.Label l32desc;
        private System.Windows.Forms.LinkLabel ll32bit;
        private System.Windows.Forms.FlowLayoutPanel p32bit;
        private System.Windows.Forms.NumericUpDown nudSynthRate;
        private System.Windows.Forms.NumericUpDown nudSynthVol;
        private System.Windows.Forms.Label lbSynthRate;
        private System.Windows.Forms.Label lbSynthVol;
    }
}