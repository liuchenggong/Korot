using CefSharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmNotificationPermission : Form
    {
        private bool alreadyAddedAllow => cefform.Settings.GetSiteFromUrl(baseUrl).AllowNotifications;
        private readonly string baseUrl;
        private readonly frmCEF cefform;
        public frmNotificationPermission(frmCEF _frmCEF, string url)
        {
            baseUrl = url;
            cefform = _frmCEF;
            InitializeComponent();
            label1.Text = cefform.notificationPermission.Replace("[URL]", baseUrl);
            button1.Text = cefform.allow;
            button2.Text = cefform.deny;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!alreadyAddedAllow)
            {
                cefform.Settings.GetSiteFromUrl(baseUrl).AllowNotifications = true;
            }
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.ExecuteScriptAsync(@"korotNotificationPermission = 'granted';")));
            cefform.Invoke(new Action(() => cefform.refreshPage()));
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (alreadyAddedAllow)
            {
                cefform.Settings.GetSiteFromUrl(baseUrl).AllowNotifications = false;
            }
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.ExecuteScriptAsync(@"korotNotificationPermission = 'denied';")));
            cefform.Invoke(new Action(() => cefform.refreshPage()));
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = cefform.notificationPermission.Replace("[URL]", baseUrl);
            button1.ButtonText = cefform.allow;
            button2.ButtonText = cefform.deny;
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            button1.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            button2.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            pUp.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pDown.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pLeft.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
            pRight.BackColor = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Color.Black : Color.White;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Close();
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.Refresh()));
        }

        private void frmNotificationPermission_Leave(object sender, EventArgs e)
        {
            Close();
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.Refresh()));
        }
    }
}
