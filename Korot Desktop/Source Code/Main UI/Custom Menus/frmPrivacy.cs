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
    public partial class frmPrivacy : Form
    {
        frmCEF cefform;
        public frmPrivacy(frmCEF _frmCEF)
        {
            cefform = _frmCEF;
            InitializeComponent();
        }

        private void frmPrivacy_Leave(object sender, EventArgs e)
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
            btCert.BackColor = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            btCert.ForeColor = HTAlt.Tools.AutoWhiteBlack(btCert.BackColor);
            btSite.BackColor = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            btSite.ForeColor = HTAlt.Tools.AutoWhiteBlack(btSite.BackColor);
            btSite.Enabled = !cefform._Incognito;
            btCert.Visible = cefform.certError;
            btCert.Text = cefform.showCertError;
            if (cefform.certError)
            {
                lbStatus.Text = cefform.CertificateErrorTitle;
                lbInfo.Text = cefform.CertificateError;
            }
            else
            {
                lbStatus.Text = cefform.CertificateOKTitle;
                lbInfo.Text = cefform.CertificateOK;
            }
            lbCookie.Text = cefform.cookieUsage ? cefform.usesCookies : cefform.notUsesCookies;
        }

        private void htButton1_Click(object sender, EventArgs e)
        {
            TextBox txtCertificate = new TextBox() { ScrollBars = ScrollBars.Both, Multiline = true, Dock = DockStyle.Fill, Text = cefform.certificatedetails };
            Form frmCertificate = new Form() { Icon = Icon, Text = cefform.CertificateErrorMenuTitle, FormBorderStyle = FormBorderStyle.SizableToolWindow };
            frmCertificate.Controls.Add(txtCertificate);
            frmCertificate.ShowDialog();
        }

        private void htButton2_Click(object sender, EventArgs e)
        {
            Site thisSite = cefform.Settings.GetSiteFromUrl(HTAlt.Tools.GetBaseURL(cefform.chromiumWebBrowser1.Address));
            if (thisSite == null)
            {
                Site newSite = new Site()
                {
                    Url = HTAlt.Tools.GetBaseURL(cefform.chromiumWebBrowser1.Address),
                    Name = cefform.Text,
                    AllowCookies = true,
                    AllowNotifications = false,
                };
                cefform.Settings.Sites.Add(newSite);
            }
            cefform.OpenSiteSettings();
        }
    }
}
