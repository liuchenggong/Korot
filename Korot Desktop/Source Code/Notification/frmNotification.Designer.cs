/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

namespace Korot
{
    partial class frmNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            this.lbSource = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.lbMessage = new System.Windows.Forms.Label();
            this.lbClose = new System.Windows.Forms.Label();
            this.lbKorot = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pDown = new System.Windows.Forms.Panel();
            this.pLeft = new System.Windows.Forms.Panel();
            this.pUp = new System.Windows.Forms.Panel();
            this.pRight = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lbSource
            // 
            this.lbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSource.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.lbSource.Location = new System.Drawing.Point(12, 13);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(456, 18);
            this.lbSource.TabIndex = 0;
            this.lbSource.Text = "source";
            this.lbSource.Click += new System.EventHandler(this.notification_Click);
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(12, 33);
            this.pbImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(100, 100);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 1;
            this.pbImage.TabStop = false;
            this.pbImage.Click += new System.EventHandler(this.notification_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitle.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.lbTitle.Location = new System.Drawing.Point(118, 33);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(381, 18);
            this.lbTitle.TabIndex = 0;
            this.lbTitle.Text = "title";
            this.lbTitle.Click += new System.EventHandler(this.notification_Click);
            // 
            // lbMessage
            // 
            this.lbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMessage.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.lbMessage.Location = new System.Drawing.Point(118, 51);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(381, 94);
            this.lbMessage.TabIndex = 0;
            this.lbMessage.Text = "message";
            this.lbMessage.Click += new System.EventHandler(this.notification_Click);
            // 
            // lbClose
            // 
            this.lbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbClose.AutoSize = true;
            this.lbClose.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.lbClose.Location = new System.Drawing.Point(467, 10);
            this.lbClose.Name = "lbClose";
            this.lbClose.Size = new System.Drawing.Size(25, 25);
            this.lbClose.TabIndex = 0;
            this.lbClose.Text = "X";
            this.lbClose.Click += new System.EventHandler(this.lbClose_Click);
            // 
            // lbKorot
            // 
            this.lbKorot.AutoSize = true;
            this.lbKorot.Font = new System.Drawing.Font("Ubuntu", 8F);
            this.lbKorot.Location = new System.Drawing.Point(12, 136);
            this.lbKorot.Name = "lbKorot";
            this.lbKorot.Size = new System.Drawing.Size(66, 16);
            this.lbKorot.TabIndex = 2;
            this.lbKorot.Text = "Korot $ver$";
            this.lbKorot.Click += new System.EventHandler(this.notification_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pDown
            // 
            this.pDown.BackColor = System.Drawing.Color.Black;
            this.pDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pDown.Location = new System.Drawing.Point(0, 166);
            this.pDown.Name = "pDown";
            this.pDown.Size = new System.Drawing.Size(504, 1);
            this.pDown.TabIndex = 3;
            // 
            // pLeft
            // 
            this.pLeft.BackColor = System.Drawing.Color.Black;
            this.pLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pLeft.Location = new System.Drawing.Point(0, 0);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(1, 166);
            this.pLeft.TabIndex = 4;
            this.pLeft.Click += new System.EventHandler(this.notification_Click);
            // 
            // pUp
            // 
            this.pUp.BackColor = System.Drawing.Color.Black;
            this.pUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pUp.Location = new System.Drawing.Point(1, 0);
            this.pUp.Name = "pUp";
            this.pUp.Size = new System.Drawing.Size(503, 1);
            this.pUp.TabIndex = 5;
            this.pUp.Click += new System.EventHandler(this.notification_Click);
            // 
            // pRight
            // 
            this.pRight.BackColor = System.Drawing.Color.Black;
            this.pRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pRight.Location = new System.Drawing.Point(503, 1);
            this.pRight.Name = "pRight";
            this.pRight.Size = new System.Drawing.Size(1, 165);
            this.pRight.TabIndex = 6;
            this.pRight.Click += new System.EventHandler(this.notification_Click);
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 167);
            this.ControlBox = false;
            this.Controls.Add(this.pRight);
            this.Controls.Add(this.pUp);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pDown);
            this.Controls.Add(this.lbKorot);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.lbMessage);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.lbClose);
            this.Controls.Add(this.lbSource);
            this.Font = new System.Drawing.Font("Ubuntu", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNotification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Korot";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.Click += new System.EventHandler(this.notification_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.Label lbClose;
        private System.Windows.Forms.Label lbKorot;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pDown;
        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel pUp;
        private System.Windows.Forms.Panel pRight;
    }
}