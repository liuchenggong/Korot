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
            GenerateUI();
        }
        List<Label> cookieLabels = new List<Label>();
        List<Label> notificationLabels = new List<Label>();
        List<HTSwitch> switches = new List<HTSwitch>();
        public void GenerateUI()
        {
            cookieLabels.Clear();
            notificationLabels.Clear();
            switches.Clear();
            Controls.Clear();
            foreach (Site x in cefform.Settings.Sites)
            {
                GeneratePanel(x);
            }
            if (cookieLabels.Count == 0 && notificationLabels.Count == 0 && switches.Count == 0)
            {
                Controls.Add(lbEmpty);
            }
        }

        void GeneratePanel(Site site)
        {
            Panel pSite = new Panel();
            FlowLayoutPanel flowLayoutPanel1 = new FlowLayoutPanel();
            Label lbClose = new Label();
            Label lbAddress = new Label();
            Label lbTitle = new Label();
            HTSwitch hsCookie = new HTSwitch();
            HTSwitch hsNotification = new HTSwitch();
            Label lbCookie = new Label();
            Label lbNotif = new Label();
            // 
            // pSite
            // 
            pSite.Controls.Add(flowLayoutPanel1);
            pSite.Controls.Add(lbClose);
            pSite.Controls.Add(lbAddress);
            pSite.Controls.Add(lbTitle);
            pSite.Dock = System.Windows.Forms.DockStyle.Top;
            pSite.Location = new System.Drawing.Point(0, 0);
            pSite.Margin = new System.Windows.Forms.Padding(5);
            pSite.Padding = new System.Windows.Forms.Padding(5);
            pSite.Size = new System.Drawing.Size(Width, 100);
            pSite.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(hsCookie);
            flowLayoutPanel1.Controls.Add(lbCookie);
            flowLayoutPanel1.Controls.Add(hsNotification);
            flowLayoutPanel1.Controls.Add(lbNotif);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new System.Drawing.Point(5, 68);
            flowLayoutPanel1.Size = new System.Drawing.Size(Width - 10, 27);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // hsCookie
            // 
            hsCookie.Location = new System.Drawing.Point(412, 3);
            hsCookie.Size = new System.Drawing.Size(50, 19);
            hsCookie.TabIndex = 1;
            hsCookie.Tag = site;
            hsCookie.Checked = site.AllowCookies;
            switches.Add(hsCookie);
            hsCookie.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(hsCookie_CheckedChanged);
            // 
            // lbCookie
            // 
            lbCookie.AutoSize = true;
            lbCookie.Dock = System.Windows.Forms.DockStyle.Fill;
            lbCookie.Location = new System.Drawing.Point(358, 0);
            lbCookie.Size = new System.Drawing.Size(48, 25);
            lbCookie.TabIndex = 0;
            lbCookie.Text = "Cookies:";
            cookieLabels.Add(lbCookie);
            lbCookie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hsNotification
            // 
            hsNotification.Location = new System.Drawing.Point(302, 3);
            hsNotification.Size = new System.Drawing.Size(50, 19);
            hsNotification.TabIndex = 3;
            hsNotification.Tag = site;
            hsNotification.Checked = site.AllowNotifications;
            switches.Add(hsNotification);
            hsNotification.CheckedChanged += new HTAlt.WinForms.HTSwitch.CheckedChangedDelegate(hsNotification_CheckedChanged);
            // 
            // lbNotif
            // 
            lbNotif.AutoSize = true;
            lbNotif.Dock = System.Windows.Forms.DockStyle.Fill;
            lbNotif.Location = new System.Drawing.Point(228, 0);
            lbNotif.Size = new System.Drawing.Size(68, 25);
            lbNotif.TabIndex = 2;
            lbNotif.Text = "Notifications:";
            notificationLabels.Add(lbNotif);
            lbNotif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbClose
            // 
            lbClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            lbClose.AutoSize = true;
            lbClose.Font = new System.Drawing.Font("Ubuntu", 12F);
            lbClose.Location = new System.Drawing.Point(Width - 30, 8);
            lbClose.Size = new System.Drawing.Size(20, 20);
            lbClose.TabIndex = 1;
            lbClose.Text = "X";
            lbClose.Tag = site;
            lbClose.Click += new System.EventHandler(lbClose_Click);
            // 
            // lbAddress
            // 
            lbAddress.AutoSize = true;
            lbAddress.Font = new System.Drawing.Font("Ubuntu", 10F);
            lbAddress.Location = new System.Drawing.Point(10, 33);
            lbAddress.Size = new System.Drawing.Size(60, 17);
            lbAddress.TabIndex = 0;
            lbAddress.Text = site.Url;
            // 
            // lbTitle
            // 
            lbTitle.AutoSize = true;
            lbTitle.Font = new System.Drawing.Font("Ubuntu", 15F);
            lbTitle.Location = new System.Drawing.Point(8, 8);
            lbTitle.Size = new System.Drawing.Size(49, 25);
            lbTitle.TabIndex = 0;
            lbTitle.Text = site.Name;
            this.Controls.Add(pSite);
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
            Controls.Remove(lbC.Parent);
        }
        bool didLostFocus = false;
        private void frmSites_Leave(object sender, EventArgs e)
        {
            didLostFocus = true;
        }

        private void frmSites_Enter(object sender, EventArgs e)
        {
            if (didLostFocus)
            {
                didLostFocus = false;
                GenerateUI();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.BackColor = cefform.Settings.Theme.BackColor;
            this.ForeColor = HTAlt.Tools.AutoWhiteBlack(cefform.Settings.Theme.BackColor);
            foreach (Control x in Controls)
            {
                if (x is Panel)
                {
                    x.BackColor = HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor,20,false);
                    x.ForeColor = HTAlt.Tools.AutoWhiteBlack(cefform.Settings.Theme.BackColor);
                }
            }
            foreach (HTSwitch x in switches)
            {
                x.BackColor = cefform.Settings.Theme.BackColor;
                x.ButtonColor = HTAlt.Tools.ReverseColor(HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false), false);
                x.ButtonHoverColor = HTAlt.Tools.ReverseColor(HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 40, false), false);
                x.ButtonPressedColor = HTAlt.Tools.ReverseColor(HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 60, false), false);

            }
            foreach (Label x in notificationLabels){x.Text = cefform.siteNotifications;}
            foreach (Label x in cookieLabels){x.Text = cefform.siteCookies;}
            lbEmpty.Text = cefform.empty;
        }
    }
}
