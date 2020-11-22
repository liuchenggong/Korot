/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using System.Windows.Controls;

namespace Korot
{
    partial class frmDownload
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
            this.htButton1 = new HTAlt.WinForms.HTButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbEmpty = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // htButton1
            // 
            this.htButton1.Text = "Clear";
            this.htButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.htButton1.Location = new System.Drawing.Point(0, 377);
            this.htButton1.Name = "htButton1";
            this.htButton1.Size = new System.Drawing.Size(618, 23);
            this.htButton1.TabIndex = 1;
            this.htButton1.DrawImage = true;
            this.htButton1.Click += new System.EventHandler(this.htButton1_Click);
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
            this.lbEmpty.Size = new System.Drawing.Size(618, 13);
            this.lbEmpty.TabIndex = 2;
            this.lbEmpty.Text = "((empty))";
            this.lbEmpty.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(618, 400);
            this.Controls.Add(this.lbEmpty);
            this.Controls.Add(this.htButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDownload";
            this.Text = "Ꮆ工ᐯ㠪 爪㠪 ㄒ廾㠪 ⼕ㄩ爪 丂⼕闩尺";
            this.Enter += new System.EventHandler(this.frmHistory_Enter);
            this.Leave += new System.EventHandler(this.frmHistory_Leave);
            this.ResumeLayout(false);

        }

        #endregion
        private HTAlt.WinForms.HTButton htButton1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbEmpty;
    }
}