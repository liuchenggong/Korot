//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
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