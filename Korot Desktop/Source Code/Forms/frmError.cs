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
    public partial class frmError : Form
    {
        private readonly Exception Error;
        public frmError(Exception error)
        {
            Error = error;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            lbErrorCode.Text = Error.Message;
            textBox1.Text = Error.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.KorotErrorTitle;
            label2.Text = Properties.Settings.Default.KorotErrorDesc.Replace("[NEWLINE]", Environment.NewLine);
            label3.Text = Properties.Settings.Default.KorotErrorTI;
            BackColor = Properties.Settings.Default.BackColor;
            ForeColor = Tools.isBright(Properties.Settings.Default.BackColor) ? Color.Black : Color.White;
            textBox1.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            lbErrorCode.BackColor = Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false);
            textBox1.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false)) ? Color.Black : Color.White;
            lbErrorCode.ForeColor = Tools.isBright(Tools.ShiftBrightnessIfNeeded(Properties.Settings.Default.BackColor, 20, false)) ? Color.Black : Color.White;
        }
    }
}
