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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.PictureBox();
            this.mFavorites = new System.Windows.Forms.MenuStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.cmsProfiles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.profilenameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteThisProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.lErrorTitle2Text = new System.Windows.Forms.Label();
            this.lErrorTitle1Text = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lErrorTitle2 = new System.Windows.Forms.Label();
            this.lErrorTitle1 = new System.Windows.Forms.Label();
            this.lErrorTitle = new System.Windows.Forms.Label();
            this.dummyCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tmrFaster = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tmrSlower = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.haltroySwitch1 = new HaltroyFramework.HaltroySwitch();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.cmsProfiles.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.textBox1.Location = new System.Drawing.Point(129, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbProgress);
            this.panel2.Controls.Add(this.mFavorites);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.button7);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button9);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 59);
            this.panel2.TabIndex = 6;
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
            this.mFavorites.Location = new System.Drawing.Point(0, 35);
            this.mFavorites.Name = "mFavorites";
            this.mFavorites.Size = new System.Drawing.Size(400, 24);
            this.mFavorites.TabIndex = 0;
            this.mFavorites.Text = "menuStrip1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Korot.Properties.Resources.inctab;
            this.pictureBox1.Location = new System.Drawing.Point(282, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Image = global::Korot.Properties.Resources.home;
            this.button5.Location = new System.Drawing.Point(52, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(26, 26);
            this.button5.TabIndex = 4;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            this.button5.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.Transparent;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Image = global::Korot.Properties.Resources.star;
            this.button7.Location = new System.Drawing.Point(102, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(26, 26);
            this.button7.TabIndex = 0;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            this.button7.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Image = global::Korot.Properties.Resources.rightarrow;
            this.button3.Location = new System.Drawing.Point(77, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 26);
            this.button3.TabIndex = 0;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::Korot.Properties.Resources.leftarrow;
            this.button1.Location = new System.Drawing.Point(2, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 26);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.BackColor = System.Drawing.Color.Transparent;
            this.button9.ContextMenuStrip = this.cmsProfiles;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Image = global::Korot.Properties.Resources.profiles;
            this.button9.Location = new System.Drawing.Point(374, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(26, 26);
            this.button9.TabIndex = 0;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.Button9_Click);
            this.button9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // cmsProfiles
            // 
            this.cmsProfiles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profilenameToolStripMenuItem,
            this.switchToToolStripMenuItem,
            this.newProfileToolStripMenuItem});
            this.cmsProfiles.Name = "cmsProfiles";
            this.cmsProfiles.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsProfiles.ShowImageMargin = false;
            this.cmsProfiles.Size = new System.Drawing.Size(156, 92);
            this.cmsProfiles.Opening += new System.ComponentModel.CancelEventHandler(this.CmsProfiles_Opening);
            // 
            // profilenameToolStripMenuItem
            // 
            this.profilenameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteThisProfileToolStripMenuItem});
            this.profilenameToolStripMenuItem.Name = "profilenameToolStripMenuItem";
            this.profilenameToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.profilenameToolStripMenuItem.Text = "[profilename]";
            // 
            // deleteThisProfileToolStripMenuItem
            // 
            this.deleteThisProfileToolStripMenuItem.Name = "deleteThisProfileToolStripMenuItem";
            this.deleteThisProfileToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.deleteThisProfileToolStripMenuItem.Text = "Delete this Profile";
            this.deleteThisProfileToolStripMenuItem.Click += new System.EventHandler(this.DeleteThisProfileToolStripMenuItem_Click);
            // 
            // switchToToolStripMenuItem
            // 
            this.switchToToolStripMenuItem.Name = "switchToToolStripMenuItem";
            this.switchToToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.switchToToolStripMenuItem.Text = "Switch to:";
            // 
            // newProfileToolStripMenuItem
            // 
            this.newProfileToolStripMenuItem.Name = "newProfileToolStripMenuItem";
            this.newProfileToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.newProfileToolStripMenuItem.Text = "New Profile";
            this.newProfileToolStripMenuItem.Click += new System.EventHandler(this.NewProfileToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Image = global::Korot.Properties.Resources.go;
            this.button4.Location = new System.Drawing.Point(308, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(26, 26);
            this.button4.TabIndex = 0;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            this.button4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::Korot.Properties.Resources.refresh;
            this.button2.Location = new System.Drawing.Point(27, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 26);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.BackColor = System.Drawing.Color.Transparent;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Image = global::Korot.Properties.Resources.prxy;
            this.button6.Location = new System.Drawing.Point(330, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(26, 26);
            this.button6.TabIndex = 0;
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            this.button6.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Image = global::Korot.Properties.Resources.ext;
            this.button8.Location = new System.Drawing.Point(351, 5);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(26, 26);
            this.button8.TabIndex = 0;
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.Button8_Click);
            this.button8.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
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
            // dummyCMS
            // 
            this.dummyCMS.Name = "dummyCMS";
            this.dummyCMS.Size = new System.Drawing.Size(61, 4);
            // 
            // tmrFaster
            // 
            this.tmrFaster.Enabled = true;
            this.tmrFaster.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // extensionToolStripMenuItem
            // 
            this.extensionToolStripMenuItem.Name = "extensionToolStripMenuItem";
            this.extensionToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.extensionToolStripMenuItem.Text = "[extension]";
            this.extensionToolStripMenuItem.Click += new System.EventHandler(this.ExtensionToolStripMenuItem_Click);
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 79);
            this.panel1.TabIndex = 7;
            this.panel1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Panel1_PreviewKeyDown);
            // 
            // tmrSlower
            // 
            this.tmrSlower.Enabled = true;
            this.tmrSlower.Interval = 750;
            this.tmrSlower.Tick += new System.EventHandler(this.TmrSlower_Tick);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 13);
            this.label2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.haltroySwitch1);
            this.panel3.Controls.Add(this.findTextBox);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(400, 47);
            this.panel3.TabIndex = 0;
            this.panel3.Visible = false;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel3_Paint);
            // 
            // haltroySwitch1
            // 
            this.haltroySwitch1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.haltroySwitch1.Location = new System.Drawing.Point(115, 2);
            this.haltroySwitch1.Name = "haltroySwitch1";
            this.haltroySwitch1.OffFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.haltroySwitch1.OnFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.haltroySwitch1.Size = new System.Drawing.Size(46, 19);
            this.haltroySwitch1.TabIndex = 2;
            // 
            // findTextBox
            // 
            this.findTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.findTextBox.Location = new System.Drawing.Point(0, 27);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(400, 20);
            this.findTextBox.TabIndex = 1;
            this.findTextBox.TextChanged += new System.EventHandler(this.TextBox2_TextChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(383, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "X";
            this.label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Case Sensitive";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Search : ";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip2.ShowImageMargin = false;
            this.contextMenuStrip2.Size = new System.Drawing.Size(36, 4);
            // 
            // frmCEF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 150);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mFavorites;
            this.MinimumSize = new System.Drawing.Size(400, 150);
            this.Name = "frmCEF";
            this.Text = " ";
            this.Load += new System.EventHandler(this.tabform_Load);
            this.SizeChanged += new System.EventHandler(this.FrmCEF_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabform_KeyDown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProgress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.cmsProfiles.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Button button5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lErrorTitle2Text;
        private System.Windows.Forms.Label lErrorTitle1Text;
        private System.Windows.Forms.Label lErrorTitle2;
        private System.Windows.Forms.Label lErrorTitle1;
        private System.Windows.Forms.Label lErrorTitle;
        private System.Windows.Forms.ContextMenuStrip dummyCMS;
        private System.Windows.Forms.Timer tmrFaster;
        private System.Windows.Forms.MenuStrip mFavorites;
        public System.Windows.Forms.Button button7;
        public System.Windows.Forms.Button button9;
        public System.Windows.Forms.Button button8;
        private System.Windows.Forms.ContextMenuStrip cmsProfiles;
        private System.Windows.Forms.ToolStripMenuItem profilenameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteThisProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProfileToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem extensionToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer tmrSlower;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbProgress;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox findTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private HaltroyFramework.HaltroySwitch haltroySwitch1;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button button6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
    }
}