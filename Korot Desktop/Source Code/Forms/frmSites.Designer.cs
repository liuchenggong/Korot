namespace Korot
{
    partial class frmSites
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
            this.pExample = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.hsCookie = new HTAlt.WinForms.HTSwitch();
            this.lbCookie = new System.Windows.Forms.Label();
            this.hsNotification = new HTAlt.WinForms.HTSwitch();
            this.lbNotif = new System.Windows.Forms.Label();
            this.lbClose = new System.Windows.Forms.Label();
            this.lbAddress = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.pExample.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pExample
            // 
            this.pExample.Controls.Add(this.flowLayoutPanel1);
            this.pExample.Controls.Add(this.lbClose);
            this.pExample.Controls.Add(this.lbAddress);
            this.pExample.Controls.Add(this.lbTitle);
            this.pExample.Dock = System.Windows.Forms.DockStyle.Top;
            this.pExample.Location = new System.Drawing.Point(0, 0);
            this.pExample.Margin = new System.Windows.Forms.Padding(5);
            this.pExample.Name = "pExample";
            this.pExample.Padding = new System.Windows.Forms.Padding(5);
            this.pExample.Size = new System.Drawing.Size(475, 100);
            this.pExample.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.hsCookie);
            this.flowLayoutPanel1.Controls.Add(this.lbCookie);
            this.flowLayoutPanel1.Controls.Add(this.hsNotification);
            this.flowLayoutPanel1.Controls.Add(this.lbNotif);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 68);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(465, 27);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // hsCookie
            // 
            this.hsCookie.Location = new System.Drawing.Point(412, 3);
            this.hsCookie.Name = "hsCookie";
            this.hsCookie.Size = new System.Drawing.Size(50, 19);
            this.hsCookie.TabIndex = 1;
            this.hsCookie.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsCookie_CheckedChanged);
            // 
            // lbCookie
            // 
            this.lbCookie.AutoSize = true;
            this.lbCookie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCookie.Location = new System.Drawing.Point(358, 0);
            this.lbCookie.Name = "lbCookie";
            this.lbCookie.Size = new System.Drawing.Size(48, 25);
            this.lbCookie.TabIndex = 0;
            this.lbCookie.Text = "Cookies:";
            this.lbCookie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hsNotification
            // 
            this.hsNotification.Location = new System.Drawing.Point(302, 3);
            this.hsNotification.Name = "hsNotification";
            this.hsNotification.Size = new System.Drawing.Size(50, 19);
            this.hsNotification.TabIndex = 3;
            this.hsNotification.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(this.hsNotification_CheckedChanged);
            // 
            // lbNotif
            // 
            this.lbNotif.AutoSize = true;
            this.lbNotif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbNotif.Location = new System.Drawing.Point(228, 0);
            this.lbNotif.Name = "lbNotif";
            this.lbNotif.Size = new System.Drawing.Size(68, 25);
            this.lbNotif.TabIndex = 2;
            this.lbNotif.Text = "Notifications:";
            this.lbNotif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbClose
            // 
            this.lbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClose.AutoSize = true;
            this.lbClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbClose.Location = new System.Drawing.Point(445, 8);
            this.lbClose.Name = "lbClose";
            this.lbClose.Size = new System.Drawing.Size(20, 20);
            this.lbClose.TabIndex = 1;
            this.lbClose.Text = "X";
            this.lbClose.Click += new System.EventHandler(this.lbClose_Click);
            // 
            // lbAddress
            // 
            this.lbAddress.AutoSize = true;
            this.lbAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbAddress.Location = new System.Drawing.Point(10, 33);
            this.lbAddress.Name = "lbAddress";
            this.lbAddress.Size = new System.Drawing.Size(60, 17);
            this.lbAddress.TabIndex = 0;
            this.lbAddress.Text = "Address";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lbTitle.Location = new System.Drawing.Point(8, 8);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(49, 25);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "Title";
            // 
            // frmSites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 450);
            this.Controls.Add(this.pExample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSites";
            this.Text = "frmSites";
            this.pExample.ResumeLayout(false);
            this.pExample.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pExample;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private HTAlt.WinForms.HTSwitch hsCookie;
        private System.Windows.Forms.Label lbCookie;
        private HTAlt.WinForms.HTSwitch hsNotification;
        private System.Windows.Forms.Label lbNotif;
        private System.Windows.Forms.Label lbClose;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.Label lbTitle;
    }
}