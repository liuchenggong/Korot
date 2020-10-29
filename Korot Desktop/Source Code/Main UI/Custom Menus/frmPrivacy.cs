/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmPrivacy : Form
    {
        private readonly frmCEF cefform;

        public frmPrivacy(frmCEF _frmCEF)
        {
            cefform = _frmCEF;
            InitializeComponent();
            timer1_Tick(this, new EventArgs());
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
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(BackColor);
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            btCert.BackColor = BackColor2;
            btCert.ForeColor = ForeColor;
            btSite.BackColor = BackColor2;
            btSite.ForeColor = ForeColor;
            btSite.Enabled = !cefform._Incognito;
            btCert.Visible = cefform.certError;
            btCert.Text = cefform.anaform.showCertError;
            if (cefform.certError)
            {
                lbStatus.Text = cefform.anaform.CertificateErrorTitle;
                lbInfo.Text = cefform.anaform.CertificateError;
            }
            else
            {
                lbStatus.Text = cefform.anaform.CertificateOKTitle;
                lbInfo.Text = cefform.anaform.CertificateOK;
            }
            lbCookie.Text = cefform.cookieUsage ? cefform.anaform.usesCookies : cefform.anaform.notUsesCookies;
        }

        private void htButton1_Click(object sender, EventArgs e)
        {
            TextBox txtCertificate = new TextBox() { ScrollBars = ScrollBars.Both, Multiline = true, Dock = DockStyle.Fill, Text = cefform.certificatedetails };
            Form frmCertificate = new Form() { Icon = Icon, Text = cefform.anaform.CertificateErrorMenuTitle, FormBorderStyle = FormBorderStyle.SizableToolWindow };
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