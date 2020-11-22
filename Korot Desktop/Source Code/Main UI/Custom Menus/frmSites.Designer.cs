/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbEmpty = new System.Windows.Forms.Label();
            this.htButton1 = new HTAlt.WinForms.HTButton();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbEmpty
            // 
            this.lbEmpty.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbEmpty.Location = new System.Drawing.Point(0, 0);
            this.lbEmpty.Name = "lbEmpty";
            this.lbEmpty.Size = new System.Drawing.Size(475, 13);
            this.lbEmpty.TabIndex = 0;
            this.lbEmpty.Text = "((empty))";
            this.lbEmpty.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // htButton1
            // 
            this.htButton1.Text = "Clear";
            this.htButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.htButton1.Location = new System.Drawing.Point(0, 427);
            this.htButton1.Name = "htButton1";
            this.htButton1.Size = new System.Drawing.Size(475, 23);
            this.htButton1.TabIndex = 2;
            this.htButton1.DrawImage = true;
            this.htButton1.Click += new System.EventHandler(this.htButton1_Click);
            // 
            // frmSites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 450);
            this.Controls.Add(this.htButton1);
            this.Controls.Add(this.lbEmpty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSites";
            this.Text = "frmSites";
            this.Enter += new System.EventHandler(this.frmSites_Enter);
            this.Leave += new System.EventHandler(this.frmSites_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbEmpty;
        private HTAlt.WinForms.HTButton htButton1;
    }
}