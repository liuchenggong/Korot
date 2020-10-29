/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using CefSharp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmNotificationPermission : Form
    {
        private bool alreadyAddedAllow => cefform.Settings.GetSiteFromUrl(baseUrl) is null ? false : cefform.Settings.GetSiteFromUrl(baseUrl).AllowNotifications;
        private readonly string baseUrl;
        private readonly frmCEF cefform;

        public frmNotificationPermission(frmCEF _frmCEF, string url)
        {
            baseUrl = url;
            cefform = _frmCEF;
            InitializeComponent();
            label1.Text = cefform.anaform.notificationPermission.Replace("[URL]", baseUrl);
            button1.Text = cefform.anaform.allow;
            button2.Text = cefform.anaform.deny;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!alreadyAddedAllow)
            {
                Site x = cefform.Settings.GetSiteFromUrl(baseUrl);
                if (x is null)
                {
                    cefform.Settings.Sites.Add(new Korot.Site() { Name = cefform.Text, Url = baseUrl, AllowNotifications = true });
                }
                else
                {
                    x.AllowNotifications = true;
                }
            }
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.ExecuteScriptAsync(@"korotNotificationPermission = 'granted';")));
            cefform.Invoke(new Action(() => cefform.refreshPage()));
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (alreadyAddedAllow)
            {
                Site x = cefform.Settings.GetSiteFromUrl(baseUrl);
                if (x is null)
                {
                    cefform.Settings.Sites.Add(new Korot.Site() { Name = cefform.Text, Url = baseUrl, AllowNotifications = false });
                }
                else
                {
                    x.AllowNotifications = false;
                }
            }
            cefform.Invoke(new Action(() => cefform.chromiumWebBrowser1.ExecuteScriptAsync(@"korotNotificationPermission = 'denied';")));
            cefform.Invoke(new Action(() => cefform.refreshPage()));
            Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = cefform.anaform.notificationPermission.Replace("[URL]", baseUrl);
            button1.Text = cefform.anaform.allow;
            button2.Text = cefform.anaform.deny;
            BackColor = cefform.Settings.Theme.BackColor;
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            button1.BackColor = BackColor2;
            button2.BackColor = BackColor2;
            pUp.BackColor = ForeColor;
            pDown.BackColor = ForeColor;
            pLeft.BackColor = ForeColor;
            pRight.BackColor = ForeColor;
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