/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

namespace Korot
{
    partial class frmMakeExt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMakeExt));
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.cbIcon = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbStartup = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbMenu = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbBackground = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudW = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudH = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.cbProxy = new System.Windows.Forms.ComboBox();
            this.autoLoad = new System.Windows.Forms.CheckBox();
            this.onlineFiles = new System.Windows.Forms.CheckBox();
            this.showPopupMenu = new System.Windows.Forms.CheckBox();
            this.activateScript = new System.Windows.Forms.CheckBox();
            this.hasProxy = new System.Windows.Forms.CheckBox();
            this.useHaltroyUpdater = new System.Windows.Forms.CheckBox();
            this.gbManifest = new System.Windows.Forms.GroupBox();
            this.gbFiles = new System.Windows.Forms.GroupBox();
            this.lbSafeName = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbLocation = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.proxyFİleCreatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertFrom06ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbInfo = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tmrCooldown = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudH)).BeginInit();
            this.gbManifest.SuspendLayout();
            this.gbFiles.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Tag = "Name of extrension. Required.";
            this.label2.Text = "Extension Name:";
            this.label2.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(99, 13);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(184, 20);
            this.tbName.TabIndex = 0;
            this.tbName.Tag = "Name of extrension. Required.";
            this.tbName.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.tbName.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 2;
            this.label3.Tag = "Version of extension. Required.";
            this.label3.Text = "Extension Version:";
            this.label3.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // tbVersion
            // 
            this.tbVersion.Location = new System.Drawing.Point(106, 39);
            this.tbVersion.Name = "tbVersion";
            this.tbVersion.Size = new System.Drawing.Size(177, 20);
            this.tbVersion.TabIndex = 1;
            this.tbVersion.Tag = "Version of extension. Required.";
            this.tbVersion.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.tbVersion.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 2;
            this.label4.Tag = "Author of extension. Required.";
            this.label4.Text = "Extension Author:";
            this.label4.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label4.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // tbAuthor
            // 
            this.tbAuthor.Location = new System.Drawing.Point(99, 65);
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.Size = new System.Drawing.Size(184, 20);
            this.tbAuthor.TabIndex = 2;
            this.tbAuthor.Tag = "Author of extension. Required.";
            this.tbAuthor.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.tbAuthor.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // cbIcon
            // 
            this.cbIcon.FormattingEnabled = true;
            this.cbIcon.Location = new System.Drawing.Point(92, 91);
            this.cbIcon.Name = "cbIcon";
            this.cbIcon.Size = new System.Drawing.Size(191, 21);
            this.cbIcon.TabIndex = 3;
            this.cbIcon.Tag = "Icon of extension. ";
            this.cbIcon.DropDown += new System.EventHandler(this.cbIcon_DropDown);
            this.cbIcon.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.cbIcon.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 2;
            this.label5.Tag = "Icon of extension. ";
            this.label5.Text = "Extension Icon:";
            this.label5.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label5.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 2;
            this.label6.Tag = "Javascript to run when Korot starts.";
            this.label6.Text = "Startup Script:";
            this.label6.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label6.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // cbStartup
            // 
            this.cbStartup.FormattingEnabled = true;
            this.cbStartup.Location = new System.Drawing.Point(86, 118);
            this.cbStartup.Name = "cbStartup";
            this.cbStartup.Size = new System.Drawing.Size(197, 21);
            this.cbStartup.TabIndex = 4;
            this.cbStartup.Tag = "Javascript to run when Korot starts.";
            this.cbStartup.DropDown += new System.EventHandler(this.cbIcon_DropDown);
            this.cbStartup.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.cbStartup.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 2;
            this.label7.Tag = "HTML file to display when user clickes on extension in Hamburger menu.";
            this.label7.Text = "Menu File:";
            this.label7.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label7.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // cbMenu
            // 
            this.cbMenu.FormattingEnabled = true;
            this.cbMenu.Location = new System.Drawing.Point(68, 145);
            this.cbMenu.Name = "cbMenu";
            this.cbMenu.Size = new System.Drawing.Size(215, 21);
            this.cbMenu.TabIndex = 5;
            this.cbMenu.Tag = "HTML file to display when user clickes on extension in Hamburger menu.";
            this.cbMenu.DropDown += new System.EventHandler(this.cbIcon_DropDown);
            this.cbMenu.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.cbMenu.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 2;
            this.label8.Tag = "Javascript to run background of every page.";
            this.label8.Text = "Background Script:";
            this.label8.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label8.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // cbBackground
            // 
            this.cbBackground.FormattingEnabled = true;
            this.cbBackground.Location = new System.Drawing.Point(110, 172);
            this.cbBackground.Name = "cbBackground";
            this.cbBackground.Size = new System.Drawing.Size(173, 21);
            this.cbBackground.TabIndex = 6;
            this.cbBackground.Tag = "Javascript to run background of every page.";
            this.cbBackground.DropDown += new System.EventHandler(this.cbIcon_DropDown);
            this.cbBackground.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.cbBackground.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 2;
            this.label9.Tag = "Width of HTMl menu.";
            this.label9.Text = "Menu Width:";
            this.label9.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label9.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // nudW
            // 
            this.nudW.Location = new System.Drawing.Point(80, 200);
            this.nudW.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudW.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.nudW.Name = "nudW";
            this.nudW.Size = new System.Drawing.Size(203, 20);
            this.nudW.TabIndex = 7;
            this.nudW.Tag = "Width of HTMl menu.";
            this.nudW.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 2;
            this.label10.Tag = "Height of HTML file.";
            this.label10.Text = "Menu Height:";
            this.label10.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label10.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // nudH
            // 
            this.nudH.Location = new System.Drawing.Point(80, 226);
            this.nudH.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudH.Minimum = new decimal(new int[] {
            2147483647,
            0,
            0,
            -2147483648});
            this.nudH.Name = "nudH";
            this.nudH.Size = new System.Drawing.Size(203, 20);
            this.nudH.TabIndex = 8;
            this.nudH.Tag = "Height of HTML file.";
            this.nudH.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 2;
            this.label11.Tag = "List of proxies to pick from.";
            this.label11.Text = "Proxy:";
            this.label11.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.label11.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // cbProxy
            // 
            this.cbProxy.FormattingEnabled = true;
            this.cbProxy.Location = new System.Drawing.Point(48, 252);
            this.cbProxy.Name = "cbProxy";
            this.cbProxy.Size = new System.Drawing.Size(235, 21);
            this.cbProxy.TabIndex = 9;
            this.cbProxy.Tag = "List of proxies to pick from.";
            this.cbProxy.DropDown += new System.EventHandler(this.cbIcon_DropDown);
            this.cbProxy.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.cbProxy.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // autoLoad
            // 
            this.autoLoad.AutoSize = true;
            this.autoLoad.Location = new System.Drawing.Point(6, 279);
            this.autoLoad.Name = "autoLoad";
            this.autoLoad.Size = new System.Drawing.Size(164, 17);
            this.autoLoad.TabIndex = 10;
            this.autoLoad.Tag = "Runs Background Javascript file when pages are loaded.";
            this.autoLoad.Text = "Load when pages are loaded";
            this.autoLoad.UseVisualStyleBackColor = true;
            this.autoLoad.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.autoLoad.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // onlineFiles
            // 
            this.onlineFiles.AutoSize = true;
            this.onlineFiles.Location = new System.Drawing.Point(6, 302);
            this.onlineFiles.Name = "onlineFiles";
            this.onlineFiles.Size = new System.Drawing.Size(211, 17);
            this.onlineFiles.TabIndex = 11;
            this.onlineFiles.Tag = "Allows extension to use files from Internet, otherwise Korot will stop extension " +
    "from accessing Internet.";
            this.onlineFiles.Text = "Allow this extension to connect Internet";
            this.onlineFiles.UseVisualStyleBackColor = true;
            this.onlineFiles.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.onlineFiles.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // showPopupMenu
            // 
            this.showPopupMenu.AutoSize = true;
            this.showPopupMenu.Location = new System.Drawing.Point(6, 325);
            this.showPopupMenu.Name = "showPopupMenu";
            this.showPopupMenu.Size = new System.Drawing.Size(166, 17);
            this.showPopupMenu.TabIndex = 12;
            this.showPopupMenu.Tag = "Shows HTML file when user clickes on extension.";
            this.showPopupMenu.Text = "Show menu when clicked on ";
            this.showPopupMenu.UseVisualStyleBackColor = true;
            this.showPopupMenu.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.showPopupMenu.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // activateScript
            // 
            this.activateScript.AutoSize = true;
            this.activateScript.Location = new System.Drawing.Point(6, 348);
            this.activateScript.Name = "activateScript";
            this.activateScript.Size = new System.Drawing.Size(174, 17);
            this.activateScript.TabIndex = 13;
            this.activateScript.Tag = "Activates Background Javascript file when user clickes on extension.";
            this.activateScript.Text = "Activate script when clicked on";
            this.activateScript.UseVisualStyleBackColor = true;
            this.activateScript.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.activateScript.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // hasProxy
            // 
            this.hasProxy.AutoSize = true;
            this.hasProxy.Location = new System.Drawing.Point(6, 371);
            this.hasProxy.Name = "hasProxy";
            this.hasProxy.Size = new System.Drawing.Size(142, 17);
            this.hasProxy.TabIndex = 14;
            this.hasProxy.Tag = "Check this box if this extension uses proxies.";
            this.hasProxy.Text = "This extension has proxy";
            this.hasProxy.UseVisualStyleBackColor = true;
            this.hasProxy.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.hasProxy.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // useHaltroyUpdater
            // 
            this.useHaltroyUpdater.AutoSize = true;
            this.useHaltroyUpdater.Location = new System.Drawing.Point(6, 394);
            this.useHaltroyUpdater.Name = "useHaltroyUpdater";
            this.useHaltroyUpdater.Size = new System.Drawing.Size(244, 17);
            this.useHaltroyUpdater.TabIndex = 15;
            this.useHaltroyUpdater.Tag = "Check this box if you want this extension to be auto-updated from Haltroy Web Sto" +
    "re.";
            this.useHaltroyUpdater.Text = "Update this extension from Haltroy Web Store.";
            this.useHaltroyUpdater.UseVisualStyleBackColor = true;
            this.useHaltroyUpdater.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.useHaltroyUpdater.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // gbManifest
            // 
            this.gbManifest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbManifest.Controls.Add(this.label2);
            this.gbManifest.Controls.Add(this.useHaltroyUpdater);
            this.gbManifest.Controls.Add(this.label9);
            this.gbManifest.Controls.Add(this.hasProxy);
            this.gbManifest.Controls.Add(this.tbName);
            this.gbManifest.Controls.Add(this.activateScript);
            this.gbManifest.Controls.Add(this.label10);
            this.gbManifest.Controls.Add(this.showPopupMenu);
            this.gbManifest.Controls.Add(this.label4);
            this.gbManifest.Controls.Add(this.onlineFiles);
            this.gbManifest.Controls.Add(this.label5);
            this.gbManifest.Controls.Add(this.autoLoad);
            this.gbManifest.Controls.Add(this.label11);
            this.gbManifest.Controls.Add(this.nudH);
            this.gbManifest.Controls.Add(this.tbAuthor);
            this.gbManifest.Controls.Add(this.nudW);
            this.gbManifest.Controls.Add(this.label6);
            this.gbManifest.Controls.Add(this.label3);
            this.gbManifest.Controls.Add(this.cbBackground);
            this.gbManifest.Controls.Add(this.label7);
            this.gbManifest.Controls.Add(this.cbMenu);
            this.gbManifest.Controls.Add(this.tbVersion);
            this.gbManifest.Controls.Add(this.cbStartup);
            this.gbManifest.Controls.Add(this.label8);
            this.gbManifest.Controls.Add(this.cbProxy);
            this.gbManifest.Controls.Add(this.cbIcon);
            this.gbManifest.Location = new System.Drawing.Point(12, 28);
            this.gbManifest.Name = "gbManifest";
            this.gbManifest.Size = new System.Drawing.Size(291, 415);
            this.gbManifest.TabIndex = 8;
            this.gbManifest.TabStop = false;
            this.gbManifest.Tag = "Main manifest file used to read extension.";
            this.gbManifest.Text = "Extension Manifest";
            // 
            // gbFiles
            // 
            this.gbFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFiles.Controls.Add(this.lbSafeName);
            this.gbFiles.Controls.Add(this.lbLocation);
            this.gbFiles.Location = new System.Drawing.Point(309, 27);
            this.gbFiles.Name = "gbFiles";
            this.gbFiles.Size = new System.Drawing.Size(308, 290);
            this.gbFiles.TabIndex = 9;
            this.gbFiles.TabStop = false;
            this.gbFiles.Tag = "List of files which are used in this extension.";
            this.gbFiles.Text = "Files";
            // 
            // lbSafeName
            // 
            this.lbSafeName.ContextMenuStrip = this.contextMenuStrip1;
            this.lbSafeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbSafeName.FormattingEnabled = true;
            this.lbSafeName.Location = new System.Drawing.Point(3, 16);
            this.lbSafeName.Name = "lbSafeName";
            this.lbSafeName.Size = new System.Drawing.Size(302, 271);
            this.lbSafeName.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addFileToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator4,
            this.removeToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.toolStripSeparator3,
            this.editSourceToolStripMenuItem,
            this.editTargetToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(120, 148);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // addFileToolStripMenuItem
            // 
            this.addFileToolStripMenuItem.Name = "addFileToolStripMenuItem";
            this.addFileToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.addFileToolStripMenuItem.Text = "Add File";
            this.addFileToolStripMenuItem.Click += new System.EventHandler(this.addFileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(116, 6);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(116, 6);
            // 
            // editSourceToolStripMenuItem
            // 
            this.editSourceToolStripMenuItem.Name = "editSourceToolStripMenuItem";
            this.editSourceToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.editSourceToolStripMenuItem.Text = "Edit Source";
            this.editSourceToolStripMenuItem.Click += new System.EventHandler(this.editSourceToolStripMenuItem_Click);
            // 
            // editTargetToolStripMenuItem
            // 
            this.editTargetToolStripMenuItem.Name = "editTargetToolStripMenuItem";
            this.editTargetToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.editTargetToolStripMenuItem.Text = "Edit Target";
            this.editTargetToolStripMenuItem.Click += new System.EventHandler(this.editTargetToolStripMenuItem_Click);
            // 
            // lbLocation
            // 
            this.lbLocation.ContextMenuStrip = this.contextMenuStrip1;
            this.lbLocation.FormattingEnabled = true;
            this.lbLocation.Location = new System.Drawing.Point(6, 161);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(120, 95);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.buildToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.loadToolStripMenuItem,
            this.toolStripSeparator1,
            this.proxyFİleCreatorToolStripMenuItem,
            this.convertFrom06ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.fileToolStripMenuItem.Tag = "File operations";
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.fileToolStripMenuItem.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.newToolStripMenuItem.Tag = "Clears all.";
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            this.newToolStripMenuItem.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.newToolStripMenuItem.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.loadToolStripMenuItem.Tag = "Loads extension from file.";
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            this.loadToolStripMenuItem.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.loadToolStripMenuItem.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // proxyFİleCreatorToolStripMenuItem
            // 
            this.proxyFİleCreatorToolStripMenuItem.Name = "proxyFİleCreatorToolStripMenuItem";
            this.proxyFİleCreatorToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.proxyFİleCreatorToolStripMenuItem.Text = "Proxy File Creator...";
            this.proxyFİleCreatorToolStripMenuItem.Click += new System.EventHandler(this.proxyFİleCreatorToolStripMenuItem_Click);
            // 
            // convertFrom06ToolStripMenuItem
            // 
            this.convertFrom06ToolStripMenuItem.Name = "convertFrom06ToolStripMenuItem";
            this.convertFrom06ToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.convertFrom06ToolStripMenuItem.Text = "Convert from 0.6";
            this.convertFrom06ToolStripMenuItem.Click += new System.EventHandler(this.convertFrom06ToolStripMenuItem_Click);
            // 
            // buildToolStripMenuItem
            // 
            this.buildToolStripMenuItem.Name = "buildToolStripMenuItem";
            this.buildToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.buildToolStripMenuItem.Tag = "Creates new package.";
            this.buildToolStripMenuItem.Text = "Build";
            this.buildToolStripMenuItem.Click += new System.EventHandler(this.buildToolStripMenuItem_Click);
            this.buildToolStripMenuItem.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.buildToolStripMenuItem.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // gbInfo
            // 
            this.gbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbInfo.Controls.Add(this.textBox4);
            this.gbInfo.Location = new System.Drawing.Point(309, 323);
            this.gbInfo.Name = "gbInfo";
            this.gbInfo.Size = new System.Drawing.Size(308, 120);
            this.gbInfo.TabIndex = 11;
            this.gbInfo.TabStop = false;
            this.gbInfo.Text = "Information";
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(3, 16);
            this.textBox4.MaxLength = 2147483647;
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox4.Size = new System.Drawing.Size(302, 101);
            this.textBox4.TabIndex = 0;
            this.textBox4.Tag = "Hover on anything to see their information.";
            this.textBox4.MouseEnter += new System.EventHandler(this.anything_MouseEnter);
            this.textBox4.MouseLeave += new System.EventHandler(this.anything_MouseLeave);
            // 
            // tmrCooldown
            // 
            this.tmrCooldown.Interval = 1000;
            this.tmrCooldown.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMakeExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 452);
            this.Controls.Add(this.gbInfo);
            this.Controls.Add(this.gbFiles);
            this.Controls.Add(this.gbManifest);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMakeExt";
            this.Tag = "Workspace";
            this.Text = "Extension Maker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMakeExt_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudH)).EndInit();
            this.gbManifest.ResumeLayout(false);
            this.gbManifest.PerformLayout();
            this.gbFiles.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbInfo.ResumeLayout(false);
            this.gbInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.ComboBox cbIcon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbStartup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbMenu;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbBackground;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudW;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudH;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbProxy;
        private System.Windows.Forms.CheckBox autoLoad;
        private System.Windows.Forms.CheckBox onlineFiles;
        private System.Windows.Forms.CheckBox showPopupMenu;
        private System.Windows.Forms.CheckBox activateScript;
        private System.Windows.Forms.CheckBox hasProxy;
        private System.Windows.Forms.CheckBox useHaltroyUpdater;
        private System.Windows.Forms.GroupBox gbManifest;
        private System.Windows.Forms.GroupBox gbFiles;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.GroupBox gbInfo;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem editSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem proxyFİleCreatorToolStripMenuItem;
        private System.Windows.Forms.ListBox lbLocation;
        private System.Windows.Forms.ListBox lbSafeName;
        private System.Windows.Forms.Timer tmrCooldown;
        private System.Windows.Forms.ToolStripMenuItem editTargetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertFrom06ToolStripMenuItem;
    }
}