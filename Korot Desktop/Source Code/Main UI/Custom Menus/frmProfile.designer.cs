namespace Korot
{
    partial class frmProfile
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
            this.lbName = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbExport = new System.Windows.Forms.LinkLabel();
            this.lbSwitch = new System.Windows.Forms.LinkLabel();
            this.lbDelete = new System.Windows.Forms.LinkLabel();
            this.lbChangePic = new System.Windows.Forms.LinkLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsImport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNew = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.label1.Location = new System.Drawing.Point(395, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Font = new System.Drawing.Font("Ubuntu", 12.5F);
            this.lbName.Location = new System.Drawing.Point(82, 23);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(126, 21);
            this.lbName.TabIndex = 1;
            this.lbName.Text = "Hello, [NAME] !";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // lbExport
            // 
            this.lbExport.AutoSize = true;
            this.lbExport.Location = new System.Drawing.Point(12, 81);
            this.lbExport.Name = "lbExport";
            this.lbExport.Size = new System.Drawing.Size(120, 16);
            this.lbExport.TabIndex = 7;
            this.lbExport.TabStop = true;
            this.lbExport.Text = "Export this profile...";
            this.lbExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbExport_LinkClicked);
            // 
            // lbSwitch
            // 
            this.lbSwitch.AutoSize = true;
            this.lbSwitch.Location = new System.Drawing.Point(11, 101);
            this.lbSwitch.Name = "lbSwitch";
            this.lbSwitch.Size = new System.Drawing.Size(159, 16);
            this.lbSwitch.TabIndex = 7;
            this.lbSwitch.TabStop = true;
            this.lbSwitch.Text = "Switch to another profile...";
            this.lbSwitch.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbSwitch_LinkClicked);
            // 
            // lbDelete
            // 
            this.lbDelete.AutoSize = true;
            this.lbDelete.Location = new System.Drawing.Point(11, 121);
            this.lbDelete.Name = "lbDelete";
            this.lbDelete.Size = new System.Drawing.Size(121, 16);
            this.lbDelete.TabIndex = 7;
            this.lbDelete.TabStop = true;
            this.lbDelete.Text = "Delete this profile...";
            this.lbDelete.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbDelete_LinkClicked);
            // 
            // lbChangePic
            // 
            this.lbChangePic.AutoSize = true;
            this.lbChangePic.Location = new System.Drawing.Point(83, 50);
            this.lbChangePic.Name = "lbChangePic";
            this.lbChangePic.Size = new System.Drawing.Size(104, 16);
            this.lbChangePic.TabIndex = 7;
            this.lbChangePic.TabStop = true;
            this.lbChangePic.Text = "Change picture...";
            this.lbChangePic.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbChangePic_LinkClicked);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEmpty,
            this.toolStripSeparator1,
            this.tsImport,
            this.tsNew});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 76);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsEmpty
            // 
            this.tsEmpty.Enabled = false;
            this.tsEmpty.Name = "tsEmpty";
            this.tsEmpty.Size = new System.Drawing.Size(154, 22);
            this.tsEmpty.Text = "((empty))";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // tsImport
            // 
            this.tsImport.Name = "tsImport";
            this.tsImport.Size = new System.Drawing.Size(154, 22);
            this.tsImport.Text = "Import profile";
            this.tsImport.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // tsNew
            // 
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(154, 22);
            this.tsNew.Text = "Create a new profile";
            this.tsNew.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // frmProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 150);
            this.ControlBox = false;
            this.Controls.Add(this.lbDelete);
            this.Controls.Add(this.lbSwitch);
            this.Controls.Add(this.lbChangePic);
            this.Controls.Add(this.lbExport);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Ubuntu", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProfile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmProfile";
            this.Leave += new System.EventHandler(this.frmProfile_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lbExport;
        private System.Windows.Forms.LinkLabel lbSwitch;
        private System.Windows.Forms.LinkLabel lbDelete;
        private System.Windows.Forms.LinkLabel lbChangePic;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsImport;
        private System.Windows.Forms.ToolStripMenuItem tsNew;
        private System.Windows.Forms.ToolStripMenuItem tsEmpty;
    }
}