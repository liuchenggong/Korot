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
        public Settings Settings;
        public frmError(Exception error, Settings settings)
        {
            Settings = settings;
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
            string[] ErrorMenu = SafeFileSettingOrganizedClass.ErrorMenu;
            label1.Text = ErrorMenu[0];
            label2.Text = ErrorMenu[1].Replace("[NEWLINE]", Environment.NewLine);
            label3.Text = ErrorMenu[2];
            btRestart.Text = ErrorMenu[3];
            BackColor = Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Color.Black : Color.White;
            textBox1.BackColor = HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
            lbErrorCode.BackColor = HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
            textBox1.ForeColor = HTAlt.Tools.IsBright(HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false)) ? Color.Black : Color.White;
            lbErrorCode.ForeColor = HTAlt.Tools.IsBright(HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false)) ? Color.Black : Color.White;
        }

        private void btRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
