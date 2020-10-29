/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmChangeTBTBack : Form
    {
        private readonly frmCEF cefform;

        public frmChangeTBTBack(frmCEF frm)
        {
            cefform = frm;
            InitializeComponent();
            pictureBox1.BackColor = cefform.TabColor;
            DialogResult = DialogResult.Cancel;
            label1.Text = cefform.anaform.titleBackInfo;
            btDefault.Text = cefform.anaform.SetToDefault;
            btOK.Text = cefform.anaform.OK;
            btCancel.Text = cefform.anaform.Cancel;
        }

        public Color Color
        {
            get => pictureBox1.BackColor;
            set => pictureBox1.BackColor = value;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog() { Color = pictureBox1.BackColor, AnyColor = true, AllowFullOpen = true, FullOpen = true, };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = dialog.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 40, false);
            btDefault.BackColor = BackColor2;
            btDefault.ForeColor = ForeColor;
            btOK.BackColor = BackColor2;
            btOK.ForeColor = ForeColor;
            btCancel.BackColor = BackColor2;
            btCancel.ForeColor = ForeColor;
        }
    }
}