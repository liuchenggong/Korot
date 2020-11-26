/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            timer1_Tick(this, new EventArgs());
        }

        private readonly List<Label> cookieLabels = new List<Label>();
        private readonly List<Label> notificationLabels = new List<Label>();
        private readonly List<HTSwitch> switches = new List<HTSwitch>();

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

        private readonly List<Site> selectedSites = new List<Site>();
        private readonly List<Panel> selectedPanels = new List<Panel>();

        private void item_Clicked(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            Panel panel = cntrl is Panel ? cntrl as Panel : (cntrl.Parent is FlowLayoutPanel ? cntrl.Parent.Parent as Panel : cntrl.Parent as Panel);
            if (panel.Tag == null || !(panel.Tag is Site)) { return; }
            Site tag = panel.Tag as Site;
            if (selectedPanels.Contains(panel) && selectedSites.Contains(tag))
            {
                selectedPanels.Remove(panel);
                selectedSites.Remove(tag);
            }
            else
            {
                selectedPanels.Add(panel);
                selectedSites.Add(tag);
            }
        }

        private void GeneratePanel(Site site)
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
            pSite.Click += item_Clicked;
            pSite.Location = new System.Drawing.Point(0, 0);
            pSite.Margin = new System.Windows.Forms.Padding(5);
            pSite.Padding = new System.Windows.Forms.Padding(5);
            pSite.Size = new System.Drawing.Size(Width, 100);
            //
            // flowLayoutPanel1
            //
            flowLayoutPanel1.Controls.Add(hsCookie);
            flowLayoutPanel1.Controls.Add(lbCookie);
            flowLayoutPanel1.Controls.Add(hsNotification);
            flowLayoutPanel1.Controls.Add(lbNotif);
            flowLayoutPanel1.Click += item_Clicked;
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flowLayoutPanel1.Location = new System.Drawing.Point(5, 68);
            flowLayoutPanel1.Size = new System.Drawing.Size(Width - 10, 27);
            //
            // hsCookie
            //
            hsCookie.Location = new System.Drawing.Point(412, 3);
            hsCookie.Size = new System.Drawing.Size(50, 19);
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
            lbCookie.Text = "Cookies:";
            cookieLabels.Add(lbCookie);
            lbCookie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // hsNotification
            //
            hsNotification.Location = new System.Drawing.Point(302, 3);
            hsNotification.Size = new System.Drawing.Size(50, 19);
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
            lbNotif.Text = "Notifications:";
            notificationLabels.Add(lbNotif);
            lbNotif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lbClose
            //
            lbClose.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            lbClose.AutoSize = true;
            lbClose.Font = new System.Drawing.Font("Ubuntu", 12F);
            lbClose.Location = new System.Drawing.Point(Width - 30, 8);
            lbClose.Size = new System.Drawing.Size(20, 20);
            lbClose.Text = "X";
            lbClose.Tag = site;
            lbClose.Click += new System.EventHandler(lbClose_Click);
            //
            // lbAddress
            //
            lbAddress.AutoSize = true;
            lbAddress.Click += item_Clicked;
            lbAddress.Font = new System.Drawing.Font("Ubuntu", 10F);
            lbAddress.Location = new System.Drawing.Point(10, 33);
            lbAddress.Size = new System.Drawing.Size(60, 17);
            lbAddress.Text = site.Url;
            //
            // lbTitle
            //
            lbTitle.AutoSize = true;
            lbTitle.Click += item_Clicked;
            lbTitle.Font = new System.Drawing.Font("Ubuntu", 15F);
            lbTitle.Location = new System.Drawing.Point(8, 8);
            lbTitle.Size = new System.Drawing.Size(49, 25);
            lbTitle.Text = site.Name;
            Controls.Add(pSite);
        }

        private bool rsMode = false;

        private void hsNotification_CheckedChanged(object sender, EventArgs e)
        {
            HTSwitch hsN = sender as HTSwitch;
            Site site = hsN.Tag as Site;
            if (hsN == null || site == null) { return; }
            site.AllowNotifications = hsN.Checked;
        }

        private void hsCookie_CheckedChanged(object sender, EventArgs e)
        {
            HTSwitch hsC = sender as HTSwitch;
            Site site = hsC.Tag as Site;
            if (hsC == null || site == null) { return; }
            site.AllowCookies = hsC.Checked;
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Label lbC = sender as Label;
            Site site = lbC.Tag as Site;
            if (lbC == null || site == null) { return; }
            cefform.Settings.Sites.Remove(site);
            Controls.Remove(lbC.Parent);
        }

        private bool didLostFocus = false;

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
            Enabled = !cefform._Incognito;
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 20, false);
            foreach (Control x in Controls)
            {
                if (x is Panel)
                {
                    x.BackColor = selectedPanels.Contains(x as Panel)
                        ? (cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor)
                        : (cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : BackColor2);
                    x.ForeColor = ForeColor;
                }
            }
            foreach (HTSwitch x in switches)
            {
                x.BackColor = cefform.Settings.Theme.BackColor;
                x.ButtonColor = ForeColor;
                x.ButtonHoverColor = ForeColor;
                x.ButtonPressedColor = ForeColor;
            }
            htButton1.BackColor = BackColor2;
            htButton1.ForeColor = ForeColor;
            foreach (Label x in notificationLabels) { x.Text = cefform.anaform.siteNotifications; }
            foreach (Label x in cookieLabels) { x.Text = cefform.anaform.siteCookies; }
            lbEmpty.Text = cefform.anaform.empty;
            rsMode = (selectedPanels.Count != 0 && selectedSites.Count != 0);
            htButton1.Visible = (cookieLabels.Count == 0 && notificationLabels.Count == 0 && switches.Count == 0);
            htButton1.Text = rsMode ? cefform.anaform.RemoveSelected : cefform.anaform.Clear;
        }

        private void htButton1_Click(object sender, EventArgs e)
        {
            if (rsMode)
            {
                foreach (Site x in selectedSites)
                {
                    cefform.Settings.Sites.Remove(x);
                }
            }
            else
            {
                cefform.Settings.Sites.Clear();
            }
            GenerateUI();
        }
    }
}