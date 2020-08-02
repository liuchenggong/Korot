namespace Korot
{
    partial class frmPrivacy
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
            this.label1 = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbInfo = new System.Windows.Forms.Label();
            this.lbCookie = new System.Windows.Forms.Label();
            this.btCert = new HTAlt.WinForms.HTButton();
            this.btSite = new HTAlt.WinForms.HTButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.label1.Location = new System.Drawing.Point(295, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Ubuntu", 12.5F);
            this.lbStatus.Location = new System.Drawing.Point(11, 29);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(60, 21);
            this.lbStatus.TabIndex = 1;
            this.lbStatus.Text = "Status";
            // 
            // lbInfo
            // 
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.lbInfo.Location = new System.Drawing.Point(12, 50);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(32, 18);
            this.lbInfo.TabIndex = 2;
            this.lbInfo.Text = "Info";
            // 
            // lbCookie
            // 
            this.lbCookie.AutoSize = true;
            this.lbCookie.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.lbCookie.Location = new System.Drawing.Point(12, 68);
            this.lbCookie.Name = "lbCookie";
            this.lbCookie.Size = new System.Drawing.Size(76, 18);
            this.lbCookie.TabIndex = 3;
            this.lbCookie.Text = "CookieInfo";
            // 
            // btCert
            // 
            this.btCert.Text = "Show certificate error...";
            this.btCert.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btCert.FlatAppearance.BorderSize = 0;
            this.btCert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCert.Location = new System.Drawing.Point(0, 112);
            this.btCert.Name = "btCert";
            this.btCert.Size = new System.Drawing.Size(325, 23);
            this.btCert.TabIndex = 4;
            this.btCert.DrawImage = true;
            this.btCert.UseVisualStyleBackColor = true;
            this.btCert.Click += new System.EventHandler(this.htButton1_Click);
            // 
            // btSite
            // 
            this.btSite.Text = "Manage settings...";
            this.btSite.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btSite.FlatAppearance.BorderSize = 0;
            this.btSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSite.Location = new System.Drawing.Point(0, 89);
            this.btSite.Name = "btSite";
            this.btSite.Size = new System.Drawing.Size(325, 23);
            this.btSite.TabIndex = 5;
            this.btSite.DrawImage = true;
            this.btSite.UseVisualStyleBackColor = true;
            this.btSite.Click += new System.EventHandler(this.htButton2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmPrivacy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 135);
            this.ControlBox = false;
            this.Controls.Add(this.btSite);
            this.Controls.Add(this.btCert);
            this.Controls.Add(this.lbCookie);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrivacy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmPrivacy";
            this.Leave += new System.EventHandler(this.frmPrivacy_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Label lbCookie;
        private HTAlt.WinForms.HTButton btCert;
        private HTAlt.WinForms.HTButton btSite;
        private System.Windows.Forms.Timer timer1;
    }
}