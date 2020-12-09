
using System;
using System.Windows.Forms;

namespace Korot_Win32
{
    partial class frmMain
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

        #region Form Without Control Box
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                // If this form is inherited, the IDE needs this style
                // set so that its coordinate system is correct.
                const int WS_CHILDWINDOW = 0x40000000;
                // The following two styles are used to clip child
                // and sibling windows in Paint events.
                const int WS_CLIPCHILDREN = 0x2000000;
                const int WS_CLIPSIBLINGS = 0x4000000;
                // Add a Minimize button (or Minimize option in Window Menu).
                const int WS_MINIMIZEBOX = 0x20000;
                // Add a Maximum/Restore Button (or Options in Window Menu).
                const int WS_MAXIMIZEBOX = 0x10000;
                // Window can be resized.
                const int WS_THICKFRAME = 0x40000;
                // Add A Window Menu
                const int WS_SYSMENU = 0x80000;

                // Detect Double Clicks
                const int CS_DBLCLKS = 0x8;
                // Add a DropShadow (WinXP or greater).
                const int CS_DROPSHADOW = 0x20000;

                CreateParams cp = base.CreateParams;

                cp.Style = WS_CLIPCHILDREN | WS_CLIPSIBLINGS
            | WS_MAXIMIZEBOX | WS_MINIMIZEBOX
            | WS_SYSMENU | WS_THICKFRAME;

                if (DesignMode)
                {
                    cp.Style = cp.Style | WS_CHILDWINDOW;
                }

                int ClassFlags = CS_DBLCLKS;
                int OSVER = Environment.OSVersion.Version.Major * 10;
                OSVER += Environment.OSVersion.Version.Minor;

                if (OSVER >= 51)
                {
                    ClassFlags = ClassFlags | CS_DROPSHADOW;
                }

                cp.ClassStyle = ClassFlags;

                return cp;
            }
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks > 1)
                {
                    OnMouseDoubleClick(e);
                }
                else
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    ReleaseCapture();
                }
            }
            Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
                WindowState = FormWindowState.Maximized;
            }
            Invalidate();
        }
        #endregion Form Without Control Box

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pTitleBar = new System.Windows.Forms.Panel();
            this.pAppDrawer = new System.Windows.Forms.Panel();
            this.pAppGrid = new System.Windows.Forms.Panel();
            this.flpFavApps = new System.Windows.Forms.FlowLayoutPanel();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.pbKorot = new System.Windows.Forms.PictureBox();
            this.tcAppMan = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tcTabs = new System.Windows.Forms.TabControl();
            this.pAppDrawer.SuspendLayout();
            this.pAppGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKorot)).BeginInit();
            this.tcAppMan.SuspendLayout();
            this.SuspendLayout();
            // 
            // pTitleBar
            // 
            this.pTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.pTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pTitleBar.Name = "pTitleBar";
            this.pTitleBar.Size = new System.Drawing.Size(809, 30);
            this.pTitleBar.TabIndex = 0;
            // 
            // pAppDrawer
            // 
            this.pAppDrawer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.pAppDrawer.Controls.Add(this.pAppGrid);
            this.pAppDrawer.Controls.Add(this.tcAppMan);
            this.pAppDrawer.Controls.Add(this.label1);
            this.pAppDrawer.Dock = System.Windows.Forms.DockStyle.Left;
            this.pAppDrawer.Location = new System.Drawing.Point(0, 30);
            this.pAppDrawer.Name = "pAppDrawer";
            this.pAppDrawer.Size = new System.Drawing.Size(60, 556);
            this.pAppDrawer.TabIndex = 1;
            // 
            // pAppGrid
            // 
            this.pAppGrid.Controls.Add(this.flpFavApps);
            this.pAppGrid.Controls.Add(this.pbSettings);
            this.pAppGrid.Controls.Add(this.pbKorot);
            this.pAppGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.pAppGrid.Location = new System.Drawing.Point(0, 0);
            this.pAppGrid.Name = "pAppGrid";
            this.pAppGrid.Size = new System.Drawing.Size(55, 556);
            this.pAppGrid.TabIndex = 3;
            // 
            // flpFavApps
            // 
            this.flpFavApps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpFavApps.Location = new System.Drawing.Point(8, 50);
            this.flpFavApps.Name = "flpFavApps";
            this.flpFavApps.Size = new System.Drawing.Size(40, 457);
            this.flpFavApps.TabIndex = 1;
            // 
            // pbSettings
            // 
            this.pbSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSettings.Image = global::Korot_Win32.Properties.Resources.Settings;
            this.pbSettings.Location = new System.Drawing.Point(8, 513);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(40, 40);
            this.pbSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSettings.TabIndex = 0;
            this.pbSettings.TabStop = false;
            // 
            // pbKorot
            // 
            this.pbKorot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbKorot.Image = global::Korot_Win32.Properties.Resources.Korot;
            this.pbKorot.Location = new System.Drawing.Point(8, 3);
            this.pbKorot.Name = "pbKorot";
            this.pbKorot.Size = new System.Drawing.Size(40, 40);
            this.pbKorot.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbKorot.TabIndex = 0;
            this.pbKorot.TabStop = false;
            // 
            // tcAppMan
            // 
            this.tcAppMan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcAppMan.Controls.Add(this.tabPage1);
            this.tcAppMan.Controls.Add(this.tabPage2);
            this.tcAppMan.Location = new System.Drawing.Point(-9, -29);
            this.tcAppMan.Name = "tcAppMan";
            this.tcAppMan.SelectedIndex = 0;
            this.tcAppMan.Size = new System.Drawing.Size(16, 602);
            this.tcAppMan.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(8, 576);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tpApps";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(8, 576);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Location = new System.Drawing.Point(55, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(5, 556);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label1_MouseMove);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tcTabs
            // 
            this.tcTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTabs.Location = new System.Drawing.Point(49, 4);
            this.tcTabs.Name = "tcTabs";
            this.tcTabs.SelectedIndex = 0;
            this.tcTabs.Size = new System.Drawing.Size(769, 595);
            this.tcTabs.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 586);
            this.Controls.Add(this.pAppDrawer);
            this.Controls.Add(this.pTitleBar);
            this.Controls.Add(this.tcTabs);
            this.Name = "frmMain";
            this.Text = "Korot";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MouseLeave += new System.EventHandler(this.Form1_MouseLeave);
            this.pAppDrawer.ResumeLayout(false);
            this.pAppGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKorot)).EndInit();
            this.tcAppMan.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTitleBar;
        private System.Windows.Forms.Panel pAppDrawer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tcAppMan;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tcTabs;
        private System.Windows.Forms.Panel pAppGrid;
        private System.Windows.Forms.FlowLayoutPanel flpFavApps;
        private System.Windows.Forms.PictureBox pbSettings;
        private System.Windows.Forms.PictureBox pbKorot;
    }
}

