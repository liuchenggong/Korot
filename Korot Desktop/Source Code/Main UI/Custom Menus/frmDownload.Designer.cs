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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTarih = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.htProgressBar1 = new HTAlt.WinForms.HTProgressBar();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // htButton1
            // 
            this.htButton1.ButtonText = "Clear";
            this.htButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.htButton1.FlatAppearance.BorderSize = 0;
            this.htButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.htButton1.Location = new System.Drawing.Point(0, 377);
            this.htButton1.Name = "htButton1";
            this.htButton1.Size = new System.Drawing.Size(618, 23);
            this.htButton1.TabIndex = 1;
            this.htButton1.TextImageRelation = HTAlt.WinForms.HTButton.ButtonTextImageRelation.TextBelowImage;
            this.htButton1.UseVisualStyleBackColor = true;
            this.htButton1.Click += new System.EventHandler(this.htButton1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
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
            // panel2
            // 
            this.panel2.Controls.Add(this.lbTarih);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.htProgressBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 13);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(618, 95);
            this.panel2.TabIndex = 0;
            this.panel2.Tag = "x";
            this.panel2.Text = "Text";
            // 
            // lbTarih
            // 
            this.lbTarih.AutoSize = true;
            this.lbTarih.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbTarih.Font = new System.Drawing.Font("Ubuntu", 8F);
            this.lbTarih.Location = new System.Drawing.Point(5, 26);
            this.lbTarih.Name = "lbTarih";
            this.lbTarih.Size = new System.Drawing.Size(395, 16);
            this.lbTarih.TabIndex = 3;
            this.lbTarih.Tag = "x";
            this.lbTarih.Text = "cefecik.GetDateInfo(DateTime.ParseExact(x.Date, cefecik.DateFormat, null))";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Font = new System.Drawing.Font("Ubuntu", 15F);
            this.label4.Location = new System.Drawing.Point(5, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(449, 25);
            this.label4.TabIndex = 0;
            this.label4.Tag = "x";
            this.label4.Text = "Path.GetFileNameWithoutExtension(x.LocalUrl)";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ubuntu", 12F);
            this.label5.Location = new System.Drawing.Point(595, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(18, 20);
            this.label5.TabIndex = 1;
            this.label5.Tag = "x";
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(5, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 2;
            this.label6.Tag = " x";
            this.label6.Text = "x.Url";
            // 
            // htProgressBar1
            // 
            this.htProgressBar1.BorderThickness = 0;
            this.htProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.htProgressBar1.Location = new System.Drawing.Point(5, 80);
            this.htProgressBar1.Name = "htProgressBar1";
            this.htProgressBar1.Size = new System.Drawing.Size(608, 10);
            this.htProgressBar1.TabIndex = 4;
            this.htProgressBar1.Tag = "x";
            this.htProgressBar1.Text = "htProgressBar1";
            // 
            // frmDownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(618, 400);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbEmpty);
            this.Controls.Add(this.htButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDownload";
            this.Text = "Ꮆ工ᐯ㠪 爪㠪 ㄒ廾㠪 ⼕ㄩ爪 丂⼕闩尺";
            this.Enter += new System.EventHandler(this.frmHistory_Enter);
            this.Leave += new System.EventHandler(this.frmHistory_Leave);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private HTAlt.WinForms.HTButton htButton1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbTarih;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbEmpty;
        private HTAlt.WinForms.HTProgressBar htProgressBar1;
    }
}