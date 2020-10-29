/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
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
            if (ErrorMenu.Length > 2)
            {
                label1.Text = ErrorMenu[0];
                label2.Text = ErrorMenu[1].Replace("[NEWLINE]", Environment.NewLine);
                label3.Text = ErrorMenu[2];
                btRestart.Text = ErrorMenu[3];
            }
            BackColor = Settings.Theme.BackColor;
            ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
            Color BackColor2 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
            btRestart.BackColor = BackColor2;
            textBox1.BackColor = BackColor2;
            lbErrorCode.BackColor = BackColor2;
            textBox1.ForeColor = ForeColor;
            lbErrorCode.ForeColor = ForeColor;
        }

        private void btRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}