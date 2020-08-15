namespace Korot
{
    partial class frmBlockSite
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBlockSite));
            this.lbUrl = new System.Windows.Forms.Label();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbLevel = new System.Windows.Forms.Label();
            this.rbL0 = new System.Windows.Forms.RadioButton();
            this.rbL1 = new System.Windows.Forms.RadioButton();
            this.rbL2 = new System.Windows.Forms.RadioButton();
            this.rbL3 = new System.Windows.Forms.RadioButton();
            this.lbLevelInfo = new System.Windows.Forms.Label();
            this.lbFilter = new System.Windows.Forms.Label();
            this.btDone = new HTAlt.WinForms.HTButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUrl
            // 
            this.lbUrl.AutoSize = true;
            this.lbUrl.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.lbUrl.Location = new System.Drawing.Point(8, 9);
            this.lbUrl.Name = "lbUrl";
            this.lbUrl.Size = new System.Drawing.Size(34, 20);
            this.lbUrl.TabIndex = 2;
            this.lbUrl.Text = "Url:";
            // 
            // tbUrl
            // 
            this.tbUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbUrl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.tbUrl.ForeColor = System.Drawing.Color.Black;
            this.tbUrl.Location = new System.Drawing.Point(48, 9);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.Size = new System.Drawing.Size(469, 21);
            this.tbUrl.TabIndex = 3;
            this.tbUrl.TextChanged += new System.EventHandler(this.tbUrl_TextChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.lbLevel);
            this.flowLayoutPanel1.Controls.Add(this.rbL0);
            this.flowLayoutPanel1.Controls.Add(this.rbL1);
            this.flowLayoutPanel1.Controls.Add(this.rbL2);
            this.flowLayoutPanel1.Controls.Add(this.rbL3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 36);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(505, 34);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // lbLevel
            // 
            this.lbLevel.AutoSize = true;
            this.lbLevel.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.lbLevel.Location = new System.Drawing.Point(3, 0);
            this.lbLevel.Name = "lbLevel";
            this.lbLevel.Size = new System.Drawing.Size(94, 20);
            this.lbLevel.TabIndex = 3;
            this.lbLevel.Text = "Block Level:";
            // 
            // rbL0
            // 
            this.rbL0.AutoSize = true;
            this.rbL0.Location = new System.Drawing.Point(103, 3);
            this.rbL0.Name = "rbL0";
            this.rbL0.Size = new System.Drawing.Size(64, 20);
            this.rbL0.TabIndex = 4;
            this.rbL0.TabStop = true;
            this.rbL0.Text = "Level 0";
            this.rbL0.UseVisualStyleBackColor = true;
            this.rbL0.CheckedChanged += new System.EventHandler(this.rbL0_CheckedChanged);
            // 
            // rbL1
            // 
            this.rbL1.AutoSize = true;
            this.rbL1.Location = new System.Drawing.Point(173, 3);
            this.rbL1.Name = "rbL1";
            this.rbL1.Size = new System.Drawing.Size(64, 20);
            this.rbL1.TabIndex = 5;
            this.rbL1.TabStop = true;
            this.rbL1.Text = "Level 1";
            this.rbL1.UseVisualStyleBackColor = true;
            this.rbL1.CheckedChanged += new System.EventHandler(this.rbL1_CheckedChanged);
            // 
            // rbL2
            // 
            this.rbL2.AutoSize = true;
            this.rbL2.Location = new System.Drawing.Point(243, 3);
            this.rbL2.Name = "rbL2";
            this.rbL2.Size = new System.Drawing.Size(64, 20);
            this.rbL2.TabIndex = 6;
            this.rbL2.TabStop = true;
            this.rbL2.Text = "Level 2";
            this.rbL2.UseVisualStyleBackColor = true;
            this.rbL2.CheckedChanged += new System.EventHandler(this.rbL2_CheckedChanged);
            // 
            // rbL3
            // 
            this.rbL3.AutoSize = true;
            this.rbL3.Location = new System.Drawing.Point(313, 3);
            this.rbL3.Name = "rbL3";
            this.rbL3.Size = new System.Drawing.Size(64, 20);
            this.rbL3.TabIndex = 7;
            this.rbL3.TabStop = true;
            this.rbL3.Text = "Level 3";
            this.rbL3.UseVisualStyleBackColor = true;
            this.rbL3.CheckedChanged += new System.EventHandler(this.rbL3_CheckedChanged);
            // 
            // lbLevelInfo
            // 
            this.lbLevelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLevelInfo.Location = new System.Drawing.Point(12, 73);
            this.lbLevelInfo.Name = "lbLevelInfo";
            this.lbLevelInfo.Size = new System.Drawing.Size(505, 35);
            this.lbLevelInfo.TabIndex = 5;
            this.lbLevelInfo.Text = "Block a certain page in certain website only.";
            // 
            // lbFilter
            // 
            this.lbFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbFilter.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.lbFilter.Location = new System.Drawing.Point(8, 108);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new System.Drawing.Size(509, 20);
            this.lbFilter.TabIndex = 2;
            this.lbFilter.Text = "REGEX";
            // 
            // btDone
            // 
            this.btDone.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btDone.DrawImage = true;
            this.btDone.FlatAppearance.BorderSize = 0;
            this.btDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDone.Location = new System.Drawing.Point(0, 138);
            this.btDone.Name = "btDone";
            this.btDone.Size = new System.Drawing.Size(529, 23);
            this.btDone.TabIndex = 6;
            this.btDone.Text = "Done";
            this.btDone.UseVisualStyleBackColor = true;
            this.btDone.Click += new System.EventHandler(this.btDone_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmBlockSite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(529, 161);
            this.Controls.Add(this.btDone);
            this.Controls.Add(this.lbLevelInfo);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.tbUrl);
            this.Controls.Add(this.lbFilter);
            this.Controls.Add(this.lbUrl);
            this.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmBlockSite";
            this.Text = "Add new block...";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbUrl;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbLevel;
        private System.Windows.Forms.RadioButton rbL0;
        private System.Windows.Forms.RadioButton rbL1;
        private System.Windows.Forms.RadioButton rbL2;
        private System.Windows.Forms.RadioButton rbL3;
        private System.Windows.Forms.Label lbLevelInfo;
        private System.Windows.Forms.Label lbFilter;
        private HTAlt.WinForms.HTButton btDone;
        private System.Windows.Forms.Timer timer1;
    }
}