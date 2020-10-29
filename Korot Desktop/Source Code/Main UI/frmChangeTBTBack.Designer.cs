/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
namespace Korot
{
    partial class frmChangeTBTBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangeTBTBack));
            this.label1 = new System.Windows.Forms.Label();
            this.btDefault = new HTAlt.WinForms.HTButton();
            this.btOK = new HTAlt.WinForms.HTButton();
            this.btCancel = new HTAlt.WinForms.HTButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.label1.Location = new System.Drawing.Point(11, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(424, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Click the rectangle on top to change color.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btDefault
            // 
            this.btDefault.Text = "Set to Default";
            this.btDefault.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btDefault.FlatAppearance.BorderSize = 0;
            this.btDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDefault.Location = new System.Drawing.Point(0, 205);
            this.btDefault.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btDefault.Name = "btDefault";
            this.btDefault.Size = new System.Drawing.Size(446, 26);
            this.btDefault.TabIndex = 1;
            this.btDefault.DrawImage = true;
            this.btDefault.UseVisualStyleBackColor = true;
            this.btDefault.Click += new System.EventHandler(this.button1_Click);
            // 
            // btOK
            // 
            this.btOK.Text = "OK";
            this.btOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btOK.FlatAppearance.BorderSize = 0;
            this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btOK.Location = new System.Drawing.Point(0, 231);
            this.btOK.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(446, 26);
            this.btOK.TabIndex = 1;
            this.btOK.DrawImage = true;
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.button2_Click);
            // 
            // btCancel
            // 
            this.btCancel.Text = "Cancel";
            this.btCancel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btCancel.FlatAppearance.BorderSize = 0;
            this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btCancel.Location = new System.Drawing.Point(0, 179);
            this.btCancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(446, 26);
            this.btCancel.TabIndex = 1;
            this.btCancel.DrawImage = true;
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(189, 13);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmChangeTBTBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 257);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btDefault);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Ubuntu", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MaximumSize = new System.Drawing.Size(462, 296);
            this.MinimumSize = new System.Drawing.Size(462, 296);
            this.Name = "frmChangeTBTBack";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Korot";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private HTAlt.WinForms.HTButton btDefault;
        private HTAlt.WinForms.HTButton btOK;
        private HTAlt.WinForms.HTButton btCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
    }
}