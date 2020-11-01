/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
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
            this.pbSolKenar = new System.Windows.Forms.PictureBox();
            this.pbSağKenar = new System.Windows.Forms.PictureBox();
            this.pbIncognito = new System.Windows.Forms.PictureBox();
            this.btHome = new HTAlt.WinForms.HTButton();
            this.btFav = new HTAlt.WinForms.HTButton();
            this.btNext = new HTAlt.WinForms.HTButton();
            this.cmsForward = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsSepForward = new System.Windows.Forms.ToolStripSeparator();
            this.tsForwardHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.btBack = new HTAlt.WinForms.HTButton();
            this.cmsBack = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsSepBack = new System.Windows.Forms.ToolStripSeparator();
            this.tsBackHistory = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCef = new System.Windows.Forms.TabPage();
            this.pCEF = new System.Windows.Forms.Panel();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.pbAddress = new System.Windows.Forms.PictureBox();
            this.pNavigate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPrivacy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSolKenar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSağKenar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncognito)).BeginInit();
            this.cmsForward.SuspendLayout();
            this.cmsBack.SuspendLayout();
            this.cmsFavorite.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCef.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // tbAddress
            // 
            this.tbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.tbAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tbAddress.Location = new System.Drawing.Point(162, 8);
            this.tbAddress.MaxLength = 2147483647;
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(433, 16);
            this.tbAddress.TabIndex = 5;
            this.tbAddress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbAddress.WordWrap = false;
            // 
            // pNavigate
            // 
            this.pNavigate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pNavigate.Controls.Add(this.pbIncognito);
            this.pNavigate.Controls.Add(this.pbPrivacy);
            this.pNavigate.Controls.Add(this.pbProgress);
            this.pNavigate.Controls.Add(this.mFavorites);
            this.pNavigate.Controls.Add(this.tbAddress);
            this.pNavigate.Controls.Add(this.pbSolKenar);
            this.pNavigate.Controls.Add(this.pbSağKenar);
            this.pNavigate.Controls.Add(this.btHome);
            this.pNavigate.Controls.Add(this.btFav);
            this.pNavigate.Controls.Add(this.btNext);
            this.pNavigate.Controls.Add(this.btBack);
            this.pNavigate.Controls.Add(this.btHamburger);
            this.pNavigate.Controls.Add(this.btProfile);
            this.pNavigate.Controls.Add(this.btRefresh);
            this.pNavigate.Controls.Add(this.pbAddress);
            this.pNavigate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pNavigate.Location = new System.Drawing.Point(0, 0);
            this.pNavigate.Name = "pNavigate";
            this.pNavigate.Size = new System.Drawing.Size(732, 58);
            this.pNavigate.TabIndex = 6;
            // 
            // pbPrivacy
            // 
            this.pbPrivacy.BackColor = System.Drawing.Color.Transparent;
            this.pbPrivacy.Image = global::Korot.Properties.Resources.lockg;
            this.pbPrivacy.Location = new System.Drawing.Point(140, 4);
            this.pbPrivacy.Name = "pbPrivacy";
            this.pbPrivacy.Size = new System.Drawing.Size(23, 23);
            this.pbPrivacy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
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
            // pbSolKenar
            // 
            this.pbSolKenar.BackColor = System.Drawing.Color.Transparent;
            this.pbSolKenar.Image = global::Korot.Properties.Resources.temp_left;
            this.pbSolKenar.Location = new System.Drawing.Point(131, 4);
            this.pbSolKenar.Name = "pbSolKenar";
            this.pbSolKenar.Size = new System.Drawing.Size(10, 23);
            this.pbSolKenar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSolKenar.TabIndex = 5;
            this.pbSolKenar.TabStop = false;
            this.pbSolKenar.Text = "test";
            // 
            // pbSağKenar
            // 
            this.pbSağKenar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSağKenar.BackColor = System.Drawing.Color.Transparent;
            this.pbSağKenar.Image = global::Korot.Properties.Resources.temp_right;
            this.pbSağKenar.Location = new System.Drawing.Point(616, 4);
            this.pbSağKenar.Name = "pbSağKenar";
            this.pbSağKenar.Size = new System.Drawing.Size(10, 23);
            this.pbSağKenar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSağKenar.TabIndex = 5;
            this.pbSağKenar.TabStop = false;
            this.pbSağKenar.Text = "test";
            // 
            // pbIncognito
            // 
            this.pbIncognito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbIncognito.BackColor = System.Drawing.Color.Transparent;
            this.pbIncognito.Image = global::Korot.Properties.Resources.inctab;
            this.pbIncognito.Location = new System.Drawing.Point(595, 4);
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
            this.cmsForward.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSepForward,
            this.tsForwardHistory});
            this.cmsForward.Name = "cmsBack";
            this.cmsForward.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsForward.ShowImageMargin = false;
            this.cmsForward.Size = new System.Drawing.Size(88, 32);
            this.cmsForward.Opening += new System.ComponentModel.CancelEventHandler(this.cmsForward_Opening);
            // 
            // tsSepForward
            // 
            this.tsSepForward.Name = "tsSepForward";
            this.tsSepForward.Size = new System.Drawing.Size(84, 6);
            // 
            // tsForwardHistory
            // 
            this.tsForwardHistory.Name = "tsForwardHistory";
            this.tsForwardHistory.Size = new System.Drawing.Size(87, 22);
            this.tsForwardHistory.Text = "History";
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
            this.cmsBack.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSepBack,
            this.tsBackHistory});
            this.cmsBack.Name = "cmsBack";
            this.cmsBack.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsBack.ShowImageMargin = false;
            this.cmsBack.Size = new System.Drawing.Size(88, 32);
            this.cmsBack.Opening += new System.ComponentModel.CancelEventHandler(this.cmsBack_Opening);
            // 
            // tsSepBack
            // 
            this.tsSepBack.Name = "tsSepBack";
            this.tsSepBack.Size = new System.Drawing.Size(84, 6);
            // 
            // tsBackHistory
            // 
            this.tsBackHistory.Name = "tsBackHistory";
            this.tsBackHistory.Size = new System.Drawing.Size(87, 22);
            this.tsBackHistory.Text = "History";
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
            this.lbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(3, 587);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(229, 15);
            this.lbStatus.TabIndex = 0;
            this.lbStatus.Text = "korot://technical for technical information.";
            this.lbStatus.TextChanged += new System.EventHandler(this.label2_TextChanged);
            this.lbStatus.MouseEnter += new System.EventHandler(this.lbStatus_MouseHover);
            this.lbStatus.MouseLeave += new System.EventHandler(this.lbStatus_MouseLeave);
            this.lbStatus.MouseHover += new System.EventHandler(this.lbStatus_MouseHover);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpCef);
            this.tabControl1.Controls.Add(this.tpSettings);
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
            this.pCEF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pCEF.Location = new System.Drawing.Point(3, 3);
            this.pCEF.Name = "pCEF";
            this.pCEF.Size = new System.Drawing.Size(732, 602);
            this.pCEF.TabIndex = 0;
            this.pCEF.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Panel1_PreviewKeyDown);
            // 
            // tpSettings
            // 
            this.tpSettings.Location = new System.Drawing.Point(4, 24);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Size = new System.Drawing.Size(738, 608);
            this.tpSettings.TabIndex = 9;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // pbAddress
            // 
            this.pbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbAddress.Location = new System.Drawing.Point(141, 4);
            this.pbAddress.Name = "pbAddress";
            this.pbAddress.Size = new System.Drawing.Size(476, 23);
            this.pbAddress.TabIndex = 11;
            this.pbAddress.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.pbSolKenar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSağKenar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncognito)).EndInit();
            this.cmsForward.ResumeLayout(false);
            this.cmsBack.ResumeLayout(false);
            this.cmsFavorite.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tpCef.ResumeLayout(false);
            this.tpCef.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAddress)).EndInit();
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
        public HTAlt.WinForms.HTButton btHamburger;
        private System.Windows.Forms.TabPage tpCef;
        private System.Windows.Forms.ContextMenuStrip cms4;
        private System.Windows.Forms.ContextMenuStrip cmsFavorite;
        private System.Windows.Forms.ToolStripMenuItem tsopenInNewTab;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedTSMI;
        private System.Windows.Forms.ToolStripMenuItem clearTSMI;
        public System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ToolStripSeparator tsSepFav;
        private System.Windows.Forms.ToolStripMenuItem newFavoriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem openİnNewWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openİnNewIncognitoWindowToolStripMenuItem;
        private System.Windows.Forms.Panel pCEF;
        private System.Windows.Forms.ContextMenuStrip cmsBack;
        private System.Windows.Forms.ContextMenuStrip cmsForward;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripSeparator tsSepForward;
        private System.Windows.Forms.ToolStripMenuItem tsForwardHistory;
        private System.Windows.Forms.ToolStripSeparator tsSepBack;
        private System.Windows.Forms.ToolStripMenuItem tsBackHistory;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.PictureBox pbSağKenar;
        private System.Windows.Forms.PictureBox pbSolKenar;
        private System.Windows.Forms.PictureBox pbAddress;
    }
}