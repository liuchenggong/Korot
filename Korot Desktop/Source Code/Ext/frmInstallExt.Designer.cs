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
    partial class frmInstallExt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstallExt));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label11 = new System.Windows.Forms.Label();
            this.lbExtensionRequires = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.lbCanAccess = new System.Windows.Forms.Label();
            this.lbCanAccessInfo = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbAutoLoad = new System.Windows.Forms.Label();
            this.lbAutoLoadInfo = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbOnlineFiles = new System.Windows.Forms.Label();
            this.lbOnlineFilesInfo = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbNRContent = new System.Windows.Forms.Label();
            this.lbNRContentInfo = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbInstallIt = new System.Windows.Forms.Label();
            this.btInstall = new HTAlt.WinForms.HTButton();
            this.btClose2 = new HTAlt.WinForms.HTButton();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.htProgressBar1 = new HTAlt.WinForms.HTProgressBar();
            this.lbStatus = new System.Windows.Forms.Label();
            this.btClose = new HTAlt.WinForms.HTButton();
            this.lbInstallInfo = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btClose1 = new HTAlt.WinForms.HTButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbCannotInstall = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lbPleaseWait = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbAuthor = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbThemeExtension = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(-5, 43);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(654, 403);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(646, 377);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label11);
            this.flowLayoutPanel2.Controls.Add(this.lbExtensionRequires);
            this.flowLayoutPanel2.Controls.Add(this.panel7);
            this.flowLayoutPanel2.Controls.Add(this.panel6);
            this.flowLayoutPanel2.Controls.Add(this.panel3);
            this.flowLayoutPanel2.Controls.Add(this.panel4);
            this.flowLayoutPanel2.Controls.Add(this.lbInstallIt);
            this.flowLayoutPanel2.Controls.Add(this.btInstall);
            this.flowLayoutPanel2.Controls.Add(this.btClose2);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(640, 371);
            this.flowLayoutPanel2.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 13);
            this.label11.TabIndex = 16;
            // 
            // lbExtensionRequires
            // 
            this.lbExtensionRequires.AutoSize = true;
            this.lbExtensionRequires.Location = new System.Drawing.Point(3, 13);
            this.lbExtensionRequires.Name = "lbExtensionRequires";
            this.lbExtensionRequires.Size = new System.Drawing.Size(204, 13);
            this.lbExtensionRequires.TabIndex = 6;
            this.lbExtensionRequires.Text = "This extension requires these permissions.";
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.AutoSize = true;
            this.panel7.Controls.Add(this.lbCanAccess);
            this.panel7.Controls.Add(this.lbCanAccessInfo);
            this.panel7.Controls.Add(this.pictureBox2);
            this.panel7.Location = new System.Drawing.Point(3, 29);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(622, 66);
            this.panel7.TabIndex = 14;
            // 
            // lbCanAccess
            // 
            this.lbCanAccess.AutoSize = true;
            this.lbCanAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbCanAccess.Location = new System.Drawing.Point(51, 9);
            this.lbCanAccess.Name = "lbCanAccess";
            this.lbCanAccess.Size = new System.Drawing.Size(161, 17);
            this.lbCanAccess.TabIndex = 5;
            this.lbCanAccess.Text = "Can access web content";
            // 
            // lbCanAccessInfo
            // 
            this.lbCanAccessInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCanAccessInfo.Location = new System.Drawing.Point(51, 31);
            this.lbCanAccessInfo.Name = "lbCanAccessInfo";
            this.lbCanAccessInfo.Size = new System.Drawing.Size(568, 35);
            this.lbCanAccessInfo.TabIndex = 5;
            this.lbCanAccessInfo.Text = "This extension can see all the information about the page, including credit card " +
    "information to passwords or other private information.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Korot.Properties.Resources._2;
            this.pictureBox2.Location = new System.Drawing.Point(4, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.lbAutoLoad);
            this.panel6.Controls.Add(this.lbAutoLoadInfo);
            this.panel6.Controls.Add(this.pictureBox7);
            this.panel6.Location = new System.Drawing.Point(3, 101);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(622, 52);
            this.panel6.TabIndex = 14;
            // 
            // lbAutoLoad
            // 
            this.lbAutoLoad.AutoSize = true;
            this.lbAutoLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbAutoLoad.Location = new System.Drawing.Point(51, 9);
            this.lbAutoLoad.Name = "lbAutoLoad";
            this.lbAutoLoad.Size = new System.Drawing.Size(122, 17);
            this.lbAutoLoad.TabIndex = 5;
            this.lbAutoLoad.Text = "Loads automaticly";
            // 
            // lbAutoLoadInfo
            // 
            this.lbAutoLoadInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAutoLoadInfo.Location = new System.Drawing.Point(51, 31);
            this.lbAutoLoadInfo.Name = "lbAutoLoadInfo";
            this.lbAutoLoadInfo.Size = new System.Drawing.Size(568, 18);
            this.lbAutoLoadInfo.TabIndex = 5;
            this.lbAutoLoadInfo.Text = "This extension can load a script after the page load is finished.";
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::Korot.Properties.Resources._1;
            this.pictureBox7.Location = new System.Drawing.Point(4, 9);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(40, 40);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 10;
            this.pictureBox7.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.lbOnlineFiles);
            this.panel3.Controls.Add(this.lbOnlineFilesInfo);
            this.panel3.Controls.Add(this.pictureBox5);
            this.panel3.Location = new System.Drawing.Point(3, 159);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(622, 52);
            this.panel3.TabIndex = 11;
            // 
            // lbOnlineFiles
            // 
            this.lbOnlineFiles.AutoSize = true;
            this.lbOnlineFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbOnlineFiles.Location = new System.Drawing.Point(51, 9);
            this.lbOnlineFiles.Name = "lbOnlineFiles";
            this.lbOnlineFiles.Size = new System.Drawing.Size(82, 17);
            this.lbOnlineFiles.TabIndex = 5;
            this.lbOnlineFiles.Text = "Online Files";
            // 
            // lbOnlineFilesInfo
            // 
            this.lbOnlineFilesInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOnlineFilesInfo.Location = new System.Drawing.Point(51, 31);
            this.lbOnlineFilesInfo.Name = "lbOnlineFilesInfo";
            this.lbOnlineFilesInfo.Size = new System.Drawing.Size(568, 18);
            this.lbOnlineFilesInfo.TabIndex = 5;
            this.lbOnlineFilesInfo.Text = "This extension uses files from the Internet instead of using local files. This ca" +
    "n eat some data.";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Korot.Properties.Resources._3;
            this.pictureBox5.Location = new System.Drawing.Point(4, 9);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(40, 40);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 10;
            this.pictureBox5.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.lbNRContent);
            this.panel4.Controls.Add(this.lbNRContentInfo);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Location = new System.Drawing.Point(3, 217);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(622, 67);
            this.panel4.TabIndex = 17;
            this.panel4.Visible = false;
            // 
            // lbNRContent
            // 
            this.lbNRContent.AutoSize = true;
            this.lbNRContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbNRContent.Location = new System.Drawing.Point(51, 9);
            this.lbNRContent.Name = "lbNRContent";
            this.lbNRContent.Size = new System.Drawing.Size(125, 17);
            this.lbNRContent.TabIndex = 5;
            this.lbNRContent.Text = "Not Rated Content";
            // 
            // lbNRContentInfo
            // 
            this.lbNRContentInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNRContentInfo.Location = new System.Drawing.Point(51, 30);
            this.lbNRContentInfo.Name = "lbNRContentInfo";
            this.lbNRContentInfo.Size = new System.Drawing.Size(568, 37);
            this.lbNRContentInfo.TabIndex = 5;
            this.lbNRContentInfo.Text = "Korot does not automatically detect the content of this theme. As a result, this " +
    "theme may contain content that is epileptic and / or unsuitable for use.";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Korot.Properties.Resources._4;
            this.pictureBox3.Location = new System.Drawing.Point(4, 9);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // lbInstallIt
            // 
            this.lbInstallIt.AutoSize = true;
            this.lbInstallIt.Location = new System.Drawing.Point(3, 287);
            this.lbInstallIt.Name = "lbInstallIt";
            this.lbInstallIt.Size = new System.Drawing.Size(122, 13);
            this.lbInstallIt.TabIndex = 6;
            this.lbInstallIt.Text = "Do you want to install it?";
            // 
            // btInstall
            // 
            this.btInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btInstall.ButtonText = "Install";
            this.btInstall.FlatAppearance.BorderSize = 0;
            this.btInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btInstall.Location = new System.Drawing.Point(550, 303);
            this.btInstall.Name = "btInstall";
            this.btInstall.Size = new System.Drawing.Size(75, 23);
            this.btInstall.TabIndex = 11;
            this.btInstall.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btInstall.UseVisualStyleBackColor = true;
            this.btInstall.Click += new System.EventHandler(this.button1_Click);
            // 
            // btClose2
            // 
            this.btClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btClose2.ButtonText = "Close";
            this.btClose2.FlatAppearance.BorderSize = 0;
            this.btClose2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose2.Location = new System.Drawing.Point(550, 332);
            this.btClose2.Name = "btClose2";
            this.btClose2.Size = new System.Drawing.Size(75, 23);
            this.btClose2.TabIndex = 11;
            this.btClose2.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btClose2.UseVisualStyleBackColor = true;
            this.btClose2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.htProgressBar1);
            this.tabPage3.Controls.Add(this.lbStatus);
            this.tabPage3.Controls.Add(this.btClose);
            this.tabPage3.Controls.Add(this.lbInstallInfo);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(646, 377);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // htProgressBar1
            // 
            this.htProgressBar1.BorderThickness = 0;
            this.htProgressBar1.DrawBorder = true;
            this.htProgressBar1.Location = new System.Drawing.Point(25, 40);
            this.htProgressBar1.Name = "htProgressBar1";
            this.htProgressBar1.Size = new System.Drawing.Size(300, 10);
            this.htProgressBar1.TabIndex = 4;
            this.htProgressBar1.Text = "htProgressBar1";
            // 
            // lbStatus
            // 
            this.lbStatus.Location = new System.Drawing.Point(22, 53);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(303, 23);
            this.lbStatus.TabIndex = 3;
            this.lbStatus.Text = "Inıtializing...";
            this.lbStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btClose
            // 
            this.btClose.ButtonText = "Close";
            this.btClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btClose.FlatAppearance.BorderSize = 0;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Location = new System.Drawing.Point(3, 351);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(640, 23);
            this.btClose.TabIndex = 1;
            this.btClose.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.button3_Click);
            // 
            // lbInstallInfo
            // 
            this.lbInstallInfo.AutoSize = true;
            this.lbInstallInfo.Location = new System.Drawing.Point(22, 12);
            this.lbInstallInfo.Name = "lbInstallInfo";
            this.lbInstallInfo.Size = new System.Drawing.Size(57, 13);
            this.lbInstallInfo.TabIndex = 0;
            this.lbInstallInfo.Text = "Installing...";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btClose1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.lbCannotInstall);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(646, 377);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btClose1
            // 
            this.btClose1.ButtonText = "Close";
            this.btClose1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btClose1.FlatAppearance.BorderSize = 0;
            this.btClose1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose1.Location = new System.Drawing.Point(0, 354);
            this.btClose1.Name = "btClose1";
            this.btClose1.Size = new System.Drawing.Size(646, 23);
            this.btClose1.TabIndex = 2;
            this.btClose1.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.btClose1.UseVisualStyleBackColor = true;
            this.btClose1.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(16, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(613, 266);
            this.textBox1.TabIndex = 1;
            // 
            // lbCannotInstall
            // 
            this.lbCannotInstall.AutoSize = true;
            this.lbCannotInstall.Location = new System.Drawing.Point(13, 6);
            this.lbCannotInstall.Name = "lbCannotInstall";
            this.lbCannotInstall.Size = new System.Drawing.Size(140, 13);
            this.lbCannotInstall.TabIndex = 0;
            this.lbCannotInstall.Text = "Cannot install this extension.";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lbPleaseWait);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(646, 377);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "tabPage4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lbPleaseWait
            // 
            this.lbPleaseWait.AutoSize = true;
            this.lbPleaseWait.Location = new System.Drawing.Point(14, 10);
            this.lbPleaseWait.Name = "lbPleaseWait";
            this.lbPleaseWait.Size = new System.Drawing.Size(70, 13);
            this.lbPleaseWait.TabIndex = 0;
            this.lbPleaseWait.Text = "Please wait...";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.lbName.Location = new System.Drawing.Point(69, 3);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(133, 31);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "Extension";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(208, 3);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(42, 13);
            this.lbVersion.TabIndex = 0;
            this.lbVersion.Text = "Version";
            // 
            // lbAuthor
            // 
            this.lbAuthor.AutoSize = true;
            this.lbAuthor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbAuthor.Location = new System.Drawing.Point(69, 34);
            this.lbAuthor.Name = "lbAuthor";
            this.lbAuthor.Size = new System.Drawing.Size(70, 25);
            this.lbAuthor.TabIndex = 0;
            this.lbAuthor.Text = "Author";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::Korot.Properties.Resources.ext;
            this.pbLogo.Location = new System.Drawing.Point(7, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(56, 56);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 1;
            this.pbLogo.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbThemeExtension);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.pbLogo);
            this.panel1.Controls.Add(this.lbName);
            this.panel1.Controls.Add(this.lbAuthor);
            this.panel1.Controls.Add(this.lbVersion);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 68);
            this.panel1.TabIndex = 2;
            // 
            // lbThemeExtension
            // 
            this.lbThemeExtension.AutoSize = true;
            this.lbThemeExtension.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbThemeExtension.Location = new System.Drawing.Point(549, 0);
            this.lbThemeExtension.Name = "lbThemeExtension";
            this.lbThemeExtension.Size = new System.Drawing.Size(91, 13);
            this.lbThemeExtension.TabIndex = 3;
            this.lbThemeExtension.Text = "Extension/Theme";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.DodgerBlue;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Location = new System.Drawing.Point(0, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 5);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmInstallExt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 437);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInstallExt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Korot";
            this.Load += new System.EventHandler(this.frmInstallExt_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private HTAlt.WinForms.HTButton btInstall;
        private HTAlt.WinForms.HTButton btClose2;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label lbOnlineFilesInfo;
        private System.Windows.Forms.Label lbOnlineFiles;
        private System.Windows.Forms.Label lbExtensionRequires;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbAuthor;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage3;
        private HTAlt.WinForms.HTButton btClose;
        private System.Windows.Forms.Label lbInstallInfo;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbAutoLoad;
        private System.Windows.Forms.Label lbAutoLoadInfo;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lbCanAccess;
        private System.Windows.Forms.Label lbCanAccessInfo;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPage1;
        private HTAlt.WinForms.HTButton btClose1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbCannotInstall;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label lbPleaseWait;
        private System.Windows.Forms.Label lbInstallIt;
        private System.Windows.Forms.Label lbThemeExtension;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbNRContent;
        private System.Windows.Forms.Label lbNRContentInfo;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Timer timer2;
        private HTAlt.WinForms.HTProgressBar htProgressBar1;
    }
}