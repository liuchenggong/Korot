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
            ForeColor = HTAlt.Tools.AutoWhiteBlack(BackColor);
            btSite.BackColor = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            btSite.ForeColor = HTAlt.Tools.AutoWhiteBlack(btSite.BackColor);
            lbStatus.Text = cefform.IncognitoModeTitle;
            lbInfo.Text = cefform.IncognitoModeInfo;
            btSite.ButtonText = cefform.LearnMore;
        }

        private void htButton2_Click(object sender, EventArgs e)
        {
            cefform.NewTab("korot://incognito");
            cefform.anaform.Invoke(new Action(() => cefform.anaform.SelectedTabIndex = cefform.anaform.Tabs.Count - 1));
        }
    }
}
