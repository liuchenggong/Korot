namespace Korot
{
    partial class frmMain : HaltroyFramework.HaltroyForms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.cmsPlusRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newIncognitoWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrSlower = new System.Windows.Forms.Timer(this.components);
            this.ımageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new HaltroyFramework.HaltroyTabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.SessionLogger = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.button1 = new System.Windows.Forms.ToolStripMenuItem();
            this.button2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.betaTS = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPlusRightClick.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cmsPlusRightClick
            // 
            this.cmsPlusRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newWindowToolStripMenuItem,
            this.newIncognitoWindowToolStripMenuItem});
            this.cmsPlusRightClick.Name = "contextMenuStrip3";
            this.cmsPlusRightClick.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.cmsPlusRightClick.Size = new System.Drawing.Size(200, 48);
            // 
            // newWindowToolStripMenuItem
            // 
            this.newWindowToolStripMenuItem.Image = global::Korot.Properties.Resources.newwindow;
            this.newWindowToolStripMenuItem.Name = "newWindowToolStripMenuItem";
            this.newWindowToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.newWindowToolStripMenuItem.Text = "New Window";
            // 
            // newIncognitoWindowToolStripMenuItem
            // 
            this.newIncognitoWindowToolStripMenuItem.Image = global::Korot.Properties.Resources.newincwindow;
            this.newIncognitoWindowToolStripMenuItem.Name = "newIncognitoWindowToolStripMenuItem";
            this.newIncognitoWindowToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.newIncognitoWindowToolStripMenuItem.Text = "New Incognito Window";
            // 
            // tmrSlower
            // 
            this.tmrSlower.Enabled = true;
            this.tmrSlower.Interval = 750;
            this.tmrSlower.Tick += new System.EventHandler(this.TmrSlower_Tick);
            // 
            // ımageList2
            // 
            this.ımageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ımageList2.ImageStream")));
            this.ımageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.ımageList2.Images.SetKeyName(0, "Korot.png");
            this.ımageList2.Images.SetKeyName(1, "Settings.png");
            this.ımageList2.Images.SetKeyName(2, "Settings-w.png");
            // 
            // tabControl1
            // 
            this.tabControl1.ActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BackTabColor = System.Drawing.Color.White;
            this.tabControl1.BorderColor = System.Drawing.Color.White;
            this.tabControl1.ClosingMessage = null;
            this.tabControl1.ContextMenuStrip = this.cmsPlusRightClick;
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.EnableRepositioning = true;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.tabControl1.HeaderColor = System.Drawing.Color.White;
            this.tabControl1.HorizontalLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabControl1.ImageList = this.ımageList2;
            this.tabControl1.ItemSize = new System.Drawing.Size(240, 17);
            this.tabControl1.Location = new System.Drawing.Point(-1, 24);
            this.tabControl1.LockedFirstTab = null;
            this.tabControl1.LockedLastTab = this.tabPage2;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedTextColor = System.Drawing.Color.Black;
            this.tabControl1.ShowClosingButton = true;
            this.tabControl1.ShowClosingMessage = false;
            this.tabControl1.Size = new System.Drawing.Size(657, 361);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TextColor = System.Drawing.Color.Black;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.ForeColor = System.Drawing.Color.Black;
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(649, 336);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "+";
            // 
            // SessionLogger
            // 
            this.SessionLogger.Tick += new System.EventHandler(this.SessionLogger_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button1,
            this.button2,
            this.toolStripMenuItem3,
            this.betaTS});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(649, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.menuStrip1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDoubleClick);
            this.menuStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // button1
            // 
            this.button1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.button1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(25, 20);
            this.button1.Text = "x";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.button2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 20);
            this.button2.Text = "□";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem3.Text = "-";
            this.toolStripMenuItem3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem3_Click);
            // 
            // betaTS
            // 
            this.betaTS.Image = global::Korot.Properties.Resources.Korot;
            this.betaTS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.betaTS.Name = "betaTS";
            this.betaTS.Size = new System.Drawing.Size(117, 20);
            this.betaTS.Text = "Korot Beta 4.5.3";
            this.betaTS.Click += new System.EventHandler(this.korotBeta453ToolStripMenuItem_Click);
            this.betaTS.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(649, 368);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(663, 339);
            this.Name = "frmMain";
            this.Text = "Korot Beta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.SizeChanged += new System.EventHandler(this.frmMain_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMain_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.cmsPlusRightClick.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        public HaltroyFramework.HaltroyTabControl tabControl1;
        public System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip cmsPlusRightClick;
        private System.Windows.Forms.Timer tmrSlower;
        public System.Windows.Forms.ImageList ımageList2;
        private System.Windows.Forms.Timer SessionLogger;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem button1;
        private System.Windows.Forms.ToolStripMenuItem button2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem betaTS;
        public System.Windows.Forms.ToolStripMenuItem newWindowToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem newIncognitoWindowToolStripMenuItem;
    }
}