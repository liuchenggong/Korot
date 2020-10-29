/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmAskBirthday : Form
    {
        private readonly Settings Settings;

        public frmAskBirthday(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void nudMonth_ValueChanged(object sender, EventArgs e)
        {
            if (nudMonth.Value == 1 || nudMonth.Value == 3 || nudMonth.Value == 5 || nudMonth.Value == 7 || nudMonth.Value == 8 || nudMonth.Value == 10 || nudMonth.Value == 12)
            {
                nudDay.Maximum = 31;
            }
            else if (nudMonth.Value == 2)
            {
                nudDay.Maximum = nudYear.Value % 4 == 0 ? 29 : 28;
            }
            else
            {
                nudDay.Maximum = 30;
            }
        }

        private string BirthdayOK = "Thank you ♥.";

        private void frmAskBirthday_Load(object sender, EventArgs e)
        {
            nudYear.Maximum = DateTime.Now.Year;
            BackColor = Settings.Theme.BackColor;
            ForeColor = Settings.Theme.ForeColor;
            Color BackColor2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            nudDay.BackColor = BackColor2;
            nudMonth.BackColor = BackColor2;
            nudYear.BackColor = BackColor2;
            nudDay.ForeColor = ForeColor;
            nudMonth.ForeColor = ForeColor;
            nudYear.ForeColor = ForeColor;
            btOK.BackColor = BackColor2;
            btOK.ForeColor = ForeColor;
            btOK.Text = Settings.LanguageSystem.GetItemText("OK");
            lbDay.Text = Settings.LanguageSystem.GetItemText("BDDay");
            lbMonth.Text = Settings.LanguageSystem.GetItemText("BDMonth");
            lbYear.Text = Settings.LanguageSystem.GetItemText("BDYear");
            lbDesc.Text = Settings.LanguageSystem.GetItemText("BDDesc");
            BirthdayOK = Settings.LanguageSystem.GetItemText("BDOK");
            nudDay.Location = new Point(lbDay.Location.X + lbDay.Width, nudDay.Location.Y); nudDay.Width = lbDesc.Width - lbDay.Width;
            nudMonth.Location = new Point(lbMonth.Location.X + lbMonth.Width, nudMonth.Location.Y); nudMonth.Width = lbDesc.Width - lbMonth.Width;
            nudYear.Location = new Point(lbYear.Location.X + lbYear.Width, nudYear.Location.Y); nudYear.Width = lbDesc.Width - lbYear.Width;
        }

        private string month(decimal val)
        {
            if (val > 9)
            {
                return val.ToString();
            }
            else
            {
                return "0" + val;
            }
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            Settings.Birthday = nudDay.Value + "//" + month(nudMonth.Value) + "//" + nudYear.Value;
            Settings.BirthdayCount = 0;
            Settings.CelebrateBirthday = true;
            lbDesc.Text = BirthdayOK;
            btOK.Visible = false;
            lbDay.Visible = false;
            nudDay.Visible = false;
            lbMonth.Visible = false;
            nudMonth.Visible = false;
            lbYear.Visible = false;
            nudYear.Visible = false;
            btOK.Enabled = false;
            lbDay.Enabled = false;
            nudDay.Enabled = false;
            lbMonth.Enabled = false;
            nudMonth.Enabled = false;
            lbYear.Enabled = false;
            nudYear.Enabled = false;
        }
    }
}