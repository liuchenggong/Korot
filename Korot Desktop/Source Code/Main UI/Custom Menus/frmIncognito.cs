/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmIncognito : Form
    {
        private readonly frmCEF cefform;

        public frmIncognito(frmCEF _frmCEF)
        {
            cefform = _frmCEF;
            InitializeComponent();
            timer1_Tick(this, new EventArgs());
        }

        private void frmIncognito_Leave(object sender, EventArgs e)
        {
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            btSite.BackColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            btSite.ForeColor = ForeColor;
            lbStatus.Text = cefform.anaform.IncognitoModeTitle;
            lbInfo.Text = cefform.anaform.IncognitoModeInfo;
            btSite.Text = cefform.anaform.LearnMore;
        }

        private void htButton2_Click(object sender, EventArgs e)
        {
            cefform.NewTab("korot://incognito");
            cefform.anaform.Invoke(new Action(() => cefform.anaform.SelectedTabIndex = cefform.anaform.Tabs.Count - 1));
        }
    }
}