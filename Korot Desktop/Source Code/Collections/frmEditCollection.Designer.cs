/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
namespace Korot
{
    partial class frmEditCollection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditCollection));
            this.lbID = new System.Windows.Forms.Label();
            this.lbText = new System.Windows.Forms.Label();
            this.lbFont = new System.Windows.Forms.Label();
            this.lbSize = new System.Windows.Forms.Label();
            this.lbProp = new System.Windows.Forms.Label();
            this.lbBackColor = new System.Windows.Forms.Label();
            this.lbForeColor = new System.Windows.Forms.Label();
            this.lbSource = new System.Windows.Forms.Label();
            this.lbW = new System.Windows.Forms.Label();
            this.lbH = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbBack = new System.Windows.Forms.PictureBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt3DOT = new HTAlt.WinForms.HTButton();
            this.flpProp = new System.Windows.Forms.FlowLayoutPanel();
            this.rbRegular = new System.Windows.Forms.RadioButton();
            this.rbBold = new System.Windows.Forms.RadioButton();
            this.rbItalic = new System.Windows.Forms.RadioButton();
            this.rbUnderline = new System.Windows.Forms.RadioButton();
            this.rbStrikeout = new System.Windows.Forms.RadioButton();
            this.nudSize = new System.Windows.Forms.NumericUpDown();
            this.pbFore = new System.Windows.Forms.PictureBox();
            this.tbFont = new System.Windows.Forms.TextBox();
            this.tbText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbSource = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.nudH = new System.Windows.Forms.NumericUpDown();
            this.nudW = new System.Windows.Forms.NumericUpDown();
            this.btDone = new HTAlt.WinForms.HTButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.flpProp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFore)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW)).BeginInit();
            this.SuspendLayout();
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbID.Location = new System.Drawing.Point(11, 20);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(25, 17);
            this.lbID.TabIndex = 0;
            this.lbID.Text = "ID:";
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbText.Location = new System.Drawing.Point(11, 20);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(39, 17);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "Text:";
            // 
            // lbFont
            // 
            this.lbFont.AutoSize = true;
            this.lbFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbFont.Location = new System.Drawing.Point(11, 48);
            this.lbFont.Name = "lbFont";
            this.lbFont.Size = new System.Drawing.Size(40, 17);
            this.lbFont.TabIndex = 0;
            this.lbFont.Text = "Font:";
            // 
            // lbSize
            // 
            this.lbSize.AutoSize = true;
            this.lbSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbSize.Location = new System.Drawing.Point(11, 75);
            this.lbSize.Name = "lbSize";
            this.lbSize.Size = new System.Drawing.Size(67, 17);
            this.lbSize.TabIndex = 0;
            this.lbSize.Text = "FontSize:";
            // 
            // lbProp
            // 
            this.lbProp.AutoSize = true;
            this.lbProp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbProp.Location = new System.Drawing.Point(11, 105);
            this.lbProp.Name = "lbProp";
            this.lbProp.Size = new System.Drawing.Size(105, 17);
            this.lbProp.TabIndex = 0;
            this.lbProp.Text = "FontProperties:";
            // 
            // lbBackColor
            // 
            this.lbBackColor.AutoSize = true;
            this.lbBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbBackColor.Location = new System.Drawing.Point(11, 57);
            this.lbBackColor.Name = "lbBackColor";
            this.lbBackColor.Size = new System.Drawing.Size(76, 17);
            this.lbBackColor.TabIndex = 0;
            this.lbBackColor.Text = "BackColor:";
            // 
            // lbForeColor
            // 
            this.lbForeColor.AutoSize = true;
            this.lbForeColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbForeColor.Location = new System.Drawing.Point(11, 134);
            this.lbForeColor.Name = "lbForeColor";
            this.lbForeColor.Size = new System.Drawing.Size(74, 17);
            this.lbForeColor.TabIndex = 0;
            this.lbForeColor.Text = "ForeColor:";
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbSource.Location = new System.Drawing.Point(11, 20);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(57, 17);
            this.lbSource.TabIndex = 0;
            this.lbSource.Text = "Source:";
            // 
            // lbW
            // 
            this.lbW.AutoSize = true;
            this.lbW.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbW.Location = new System.Drawing.Point(11, 20);
            this.lbW.Name = "lbW";
            this.lbW.Size = new System.Drawing.Size(48, 17);
            this.lbW.TabIndex = 0;
            this.lbW.Text = "Width:";
            // 
            // lbH
            // 
            this.lbH.AutoSize = true;
            this.lbH.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lbH.Location = new System.Drawing.Point(11, 48);
            this.lbH.Name = "lbH";
            this.lbH.Size = new System.Drawing.Size(53, 17);
            this.lbH.TabIndex = 0;
            this.lbH.Text = "Height:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pbBack);
            this.groupBox1.Controls.Add(this.tbID);
            this.groupBox1.Controls.Add(this.lbID);
            this.groupBox1.Controls.Add(this.lbBackColor);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(473, 102);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General";
            // 
            // pbBack
            // 
            this.pbBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBack.Location = new System.Drawing.Point(93, 57);
            this.pbBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbBack.Name = "pbBack";
            this.pbBack.Size = new System.Drawing.Size(17, 17);
            this.pbBack.TabIndex = 2;
            this.pbBack.TabStop = false;
            this.pbBack.Click += new System.EventHandler(this.pbBack_Click);
            // 
            // tbID
            // 
            this.tbID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbID.Location = new System.Drawing.Point(42, 20);
            this.tbID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(425, 20);
            this.tbID.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.bt3DOT);
            this.groupBox2.Controls.Add(this.flpProp);
            this.groupBox2.Controls.Add(this.nudSize);
            this.groupBox2.Controls.Add(this.pbFore);
            this.groupBox2.Controls.Add(this.tbFont);
            this.groupBox2.Controls.Add(this.tbText);
            this.groupBox2.Controls.Add(this.lbText);
            this.groupBox2.Controls.Add(this.lbFont);
            this.groupBox2.Controls.Add(this.lbSize);
            this.groupBox2.Controls.Add(this.lbProp);
            this.groupBox2.Controls.Add(this.lbForeColor);
            this.groupBox2.Location = new System.Drawing.Point(12, 124);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(473, 171);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text-based";
            // 
            // bt3DOT
            // 
            this.bt3DOT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bt3DOT.DrawImage = true;
            this.bt3DOT.AutoSize = true;
            this.bt3DOT.Location = new System.Drawing.Point(440, 45);
            this.bt3DOT.Name = "bt3DOT";
            this.bt3DOT.Size = new System.Drawing.Size(27, 26);
            this.bt3DOT.TabIndex = 4;
            this.bt3DOT.Click += new System.EventHandler(this.button2_Click);
            // 
            // flpProp
            // 
            this.flpProp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpProp.AutoScroll = true;
            this.flpProp.Controls.Add(this.rbRegular);
            this.flpProp.Controls.Add(this.rbBold);
            this.flpProp.Controls.Add(this.rbItalic);
            this.flpProp.Controls.Add(this.rbUnderline);
            this.flpProp.Controls.Add(this.rbStrikeout);
            this.flpProp.Location = new System.Drawing.Point(123, 103);
            this.flpProp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flpProp.Name = "flpProp";
            this.flpProp.Size = new System.Drawing.Size(346, 30);
            this.flpProp.TabIndex = 3;
            // 
            // rbRegular
            // 
            this.rbRegular.AutoSize = true;
            this.rbRegular.Location = new System.Drawing.Point(3, 4);
            this.rbRegular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbRegular.Name = "rbRegular";
            this.rbRegular.Size = new System.Drawing.Size(64, 20);
            this.rbRegular.TabIndex = 0;
            this.rbRegular.TabStop = true;
            this.rbRegular.Text = "Regular";
            this.rbRegular.UseVisualStyleBackColor = true;
            this.rbRegular.CheckedChanged += new System.EventHandler(this.rbRegular_CheckedChanged);
            // 
            // rbBold
            // 
            this.rbBold.AutoSize = true;
            this.rbBold.Location = new System.Drawing.Point(73, 4);
            this.rbBold.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbBold.Name = "rbBold";
            this.rbBold.Size = new System.Drawing.Size(48, 20);
            this.rbBold.TabIndex = 0;
            this.rbBold.TabStop = true;
            this.rbBold.Text = "Bold";
            this.rbBold.UseVisualStyleBackColor = true;
            this.rbBold.CheckedChanged += new System.EventHandler(this.rbBold_CheckedChanged);
            // 
            // rbItalic
            // 
            this.rbItalic.AutoSize = true;
            this.rbItalic.Location = new System.Drawing.Point(127, 4);
            this.rbItalic.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbItalic.Name = "rbItalic";
            this.rbItalic.Size = new System.Drawing.Size(51, 20);
            this.rbItalic.TabIndex = 0;
            this.rbItalic.TabStop = true;
            this.rbItalic.Text = "Italic";
            this.rbItalic.UseVisualStyleBackColor = true;
            this.rbItalic.CheckedChanged += new System.EventHandler(this.rbItalic_CheckedChanged);
            // 
            // rbUnderline
            // 
            this.rbUnderline.AutoSize = true;
            this.rbUnderline.Location = new System.Drawing.Point(184, 4);
            this.rbUnderline.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbUnderline.Name = "rbUnderline";
            this.rbUnderline.Size = new System.Drawing.Size(74, 20);
            this.rbUnderline.TabIndex = 0;
            this.rbUnderline.TabStop = true;
            this.rbUnderline.Text = "Underline";
            this.rbUnderline.UseVisualStyleBackColor = true;
            this.rbUnderline.CheckedChanged += new System.EventHandler(this.rbUnderline_CheckedChanged);
            // 
            // rbStrikeout
            // 
            this.rbStrikeout.AutoSize = true;
            this.rbStrikeout.Location = new System.Drawing.Point(264, 4);
            this.rbStrikeout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rbStrikeout.Name = "rbStrikeout";
            this.rbStrikeout.Size = new System.Drawing.Size(73, 20);
            this.rbStrikeout.TabIndex = 0;
            this.rbStrikeout.TabStop = true;
            this.rbStrikeout.Text = "Strikeout";
            this.rbStrikeout.UseVisualStyleBackColor = true;
            this.rbStrikeout.CheckedChanged += new System.EventHandler(this.rbStrikeout_CheckedChanged);
            // 
            // nudSize
            // 
            this.nudSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSize.Location = new System.Drawing.Point(74, 75);
            this.nudSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudSize.Name = "nudSize";
            this.nudSize.Size = new System.Drawing.Size(393, 20);
            this.nudSize.TabIndex = 1;
            // 
            // pbFore
            // 
            this.pbFore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbFore.Location = new System.Drawing.Point(91, 134);
            this.pbFore.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbFore.Name = "pbFore";
            this.pbFore.Size = new System.Drawing.Size(17, 17);
            this.pbFore.TabIndex = 2;
            this.pbFore.TabStop = false;
            this.pbFore.Click += new System.EventHandler(this.pbFore_Click);
            // 
            // tbFont
            // 
            this.tbFont.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFont.Location = new System.Drawing.Point(48, 47);
            this.tbFont.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbFont.Name = "tbFont";
            this.tbFont.Size = new System.Drawing.Size(392, 20);
            this.tbFont.TabIndex = 2;
            // 
            // tbText
            // 
            this.tbText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbText.Location = new System.Drawing.Point(48, 18);
            this.tbText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(419, 20);
            this.tbText.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lbSource);
            this.groupBox3.Controls.Add(this.tbSource);
            this.groupBox3.Location = new System.Drawing.Point(12, 303);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(473, 57);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Internet";
            // 
            // tbSource
            // 
            this.tbSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSource.Location = new System.Drawing.Point(74, 18);
            this.tbSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbSource.Name = "tbSource";
            this.tbSource.Size = new System.Drawing.Size(393, 20);
            this.tbSource.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.nudH);
            this.groupBox4.Controls.Add(this.nudW);
            this.groupBox4.Controls.Add(this.lbW);
            this.groupBox4.Controls.Add(this.lbH);
            this.groupBox4.Location = new System.Drawing.Point(12, 367);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(473, 86);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image";
            // 
            // nudH
            // 
            this.nudH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudH.Location = new System.Drawing.Point(65, 48);
            this.nudH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudH.Name = "nudH";
            this.nudH.Size = new System.Drawing.Size(402, 20);
            this.nudH.TabIndex = 1;
            // 
            // nudW
            // 
            this.nudW.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudW.Location = new System.Drawing.Point(65, 20);
            this.nudW.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nudW.Name = "nudW";
            this.nudW.Size = new System.Drawing.Size(402, 20);
            this.nudW.TabIndex = 1;
            // 
            // btDone
            // 
            this.btDone.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btDone.DrawImage = true;
            this.btDone.AutoSize = true;
            this.btDone.Location = new System.Drawing.Point(0, 474);
            this.btDone.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btDone.Name = "btDone";
            this.btDone.Size = new System.Drawing.Size(492, 28);
            this.btDone.TabIndex = 5;
            this.btDone.Text = "Done";
            this.btDone.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmEditCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 502);
            this.Controls.Add(this.btDone);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Ubuntu", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(508, 541);
            this.MinimumSize = new System.Drawing.Size(508, 541);
            this.Name = "frmEditCollection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Item";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditCollection_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBack)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flpProp.ResumeLayout(false);
            this.flpProp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbFore)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Label lbFont;
        private System.Windows.Forms.Label lbSize;
        private System.Windows.Forms.Label lbProp;
        private System.Windows.Forms.Label lbBackColor;
        private System.Windows.Forms.Label lbForeColor;
        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.Label lbW;
        private System.Windows.Forms.Label lbH;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbBack;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flpProp;
        private System.Windows.Forms.RadioButton rbRegular;
        private System.Windows.Forms.RadioButton rbBold;
        private System.Windows.Forms.RadioButton rbItalic;
        private System.Windows.Forms.RadioButton rbUnderline;
        private System.Windows.Forms.RadioButton rbStrikeout;
        private System.Windows.Forms.PictureBox pbFore;
        private System.Windows.Forms.TextBox tbFont;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nudSize;
        private System.Windows.Forms.TextBox tbSource;
        private System.Windows.Forms.NumericUpDown nudH;
        private System.Windows.Forms.NumericUpDown nudW;
        private HTAlt.WinForms.HTButton btDone;
        private HTAlt.WinForms.HTButton bt3DOT;
        private System.Windows.Forms.Timer timer1;
    }
}