using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmSites : Form
    {
        public frmCEF cefform;
        public frmSites(frmCEF _cefform)
        {
            cefform = _cefform;
            InitializeComponent();
        }

        private void hsNotification_CheckedChanged(object sender, EventArgs e)
        {
            var hsN = sender as HTSwitch;
            var site = hsN.Tag as Site;
            if (hsN == null || site == null) { return; }
            site.AllowNotifications = hsN.Checked;
        }

        private void hsCookie_CheckedChanged(object sender, EventArgs e)
        {
            var hsC = sender as HTSwitch;
            var site = hsC.Tag as Site;
            if (hsC == null || site == null) { return; }
            site.AllowCookies = hsC.Checked;
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            var lbC = sender as Label;
            var site = lbC.Tag as Site;
            if (lbC == null || site == null) { return; }
            cefform.Settings.Sites.Remove(site);
        }
    }
}
