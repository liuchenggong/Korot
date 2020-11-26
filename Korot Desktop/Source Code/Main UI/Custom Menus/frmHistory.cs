/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmHistory : Form
    {
        private readonly frmCEF cefecik;

        public frmHistory(frmCEF cefcik)
        {
            cefecik = cefcik; //removed. oha kotu kelmıe yazmıs :O Ö 😮
            InitializeComponent();
            timer1_Tick(this,new EventArgs());
        }

        private readonly List<Panel> panelList = new List<Panel>();

        public void RefreshList()
        {
            foreach (Site x in cefecik.Settings.History)
            {
                // Search and find an existing panel with same Site tag, if exist then don't duplicate it (return).
                if (panelList.Find(i => i.Tag == x) != null)
                {
                    return;
                }
                // otherwise, create new one.
                Panel panel2 = new System.Windows.Forms.Panel();
                Label lbTarih = new System.Windows.Forms.Label();
                Label label4 = new System.Windows.Forms.Label();
                Label label5 = new System.Windows.Forms.Label();
                Label label6 = new System.Windows.Forms.Label();
                panel2.SuspendLayout();
                //
                // panel2
                //
                panel2.BackColor = HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false);
                panel2.ForeColor = HTAlt.Tools.AutoWhiteBlack(cefecik.Settings.Theme.BackColor);
                panel2.Controls.Add(label4);
                panel2.Controls.Add(label5);
                panel2.Controls.Add(label6);
                panel2.Controls.Add(lbTarih);
                panel2.Tag = x;
                panel2.Dock = System.Windows.Forms.DockStyle.Top;
                panel2.DoubleClick += historyItem_DoubleClick;
                panel2.Click += historyItem_Click;
                panel2.Location = new System.Drawing.Point(0, 0);
                panel2.Margin = new System.Windows.Forms.Padding(5);
                panel2.Padding = new System.Windows.Forms.Padding(5);
                panel2.Size = new System.Drawing.Size(Width, 70);
                //
                // lbTarih
                //
                lbTarih.Font = new System.Drawing.Font("Ubuntu", 10F);
                lbTarih.Location = new System.Drawing.Point(0, 0);
                lbTarih.AutoSize = true;
                lbTarih.DoubleClick += historyItem_DoubleClick;
                lbTarih.Click += historyItem_Click;
                lbTarih.Text = cefecik.GetDateInfo(DateTime.ParseExact(x.Date, cefecik.DateFormat, null));
                lbTarih.Tag = x;
                //
                // label4
                //
                label4.Dock = System.Windows.Forms.DockStyle.Bottom;
                label4.Font = new System.Drawing.Font("Ubuntu", 15F);
                label4.Location = new System.Drawing.Point(5, 27);
                label4.Size = new System.Drawing.Size(Width, 25);
                label4.DoubleClick += historyItem_DoubleClick;
                label4.Click += historyItem_Click;
                label4.Text = x.Name;
                label4.Tag = x;
                //
                // label5
                //
                label5.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                label5.AutoSize = true;
                label5.Font = new System.Drawing.Font("Ubuntu", 12F);
                label5.Location = new System.Drawing.Point(Width - 25, 9);
                label5.Click += lbClose_Click;
                label5.Size = new System.Drawing.Size(20, 20);
                label5.Text = "X";
                label5.Tag = x;
                //
                // label6
                //
                label6.Dock = System.Windows.Forms.DockStyle.Bottom;
                label6.Location = new System.Drawing.Point(5, 52);
                label6.Size = new System.Drawing.Size(Width, 13);
                label6.DoubleClick += historyItem_DoubleClick;
                label6.Click += historyItem_Click;
                label6.Tag = x;
                label6.Text = x.Url;
                panel2.ResumeLayout(false);
                panel2.PerformLayout();
                Controls.Add(panel2);
                panelList.Add(panel2);
            }
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (sender == null) { return; }
            object tag = lb.Tag;
            if (tag == null) { return; }
            Site site = tag as Site;
            if (site == null) { return; }
            Panel panel = lb.Parent as Panel;
            if (panel == null) { return; }
            panelList.Remove(panel);
            Controls.Remove(panel);
            cefecik.Settings.History.Remove(site);
        }

        private void historyItem_DoubleClick(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (sender == null) { return; }
            object tag = lb.Tag;
            if (tag == null) { return; }
            Site site = tag as Site;
            if (site == null) { return; }
            cefecik.NewTab(site.Url);
        }

        private readonly List<Site> selectedSites = new List<Site>();
        private readonly List<Panel> selectedPanels = new List<Panel>();

        private void historyItem_Click(object sender, EventArgs e)
        {
            Control cntrl = sender as Control;
            if (cntrl == null) { return; }
            Panel panelcik = cntrl is Panel ? (cntrl as Panel) : (cntrl.Parent as Panel);
            if (panelcik.Tag == null) { return; }
            Site site = panelcik.Tag as Site;
            if (site == null) { return; }
            if (panelcik.BackColor == cefecik.Settings.Theme.BackColor || (!selectedPanels.Contains(panelcik) && !selectedSites.Contains(site)))
            {
                // Not selected, select it
                selectedSites.Add(site);
                selectedPanels.Add(panelcik);
                panelcik.BackColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.OverlayColor;
                panelcik.ForeColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.ForeColor;
                switchRSMode();
            }
            else if (panelcik.BackColor == cefecik.Settings.Theme.OverlayColor || selectedPanels.Contains(panelcik) || selectedSites.Contains(site))
            {
                // Selected, unselect it
                selectedSites.Remove(site);
                selectedPanels.Remove(panelcik);
                panelcik.BackColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false);
                panelcik.ForeColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.ForeColor;
                switchRSMode();
            }
        }

        private void switchRSMode()
        {
            rsMode = !(selectedPanels.Count == 0 && selectedSites.Count == 0);
            htButton1.Text = rsMode ? cefecik.anaform.RemoveSelected : cefecik.anaform.Clear;
        }

        private bool rsMode = false;

        private void htButton1_Click(object sender, EventArgs e)
        {
            if (rsMode)
            {
                foreach (Site site in selectedSites)
                {
                    cefecik.Settings.History.Remove(site);
                }
                foreach (Panel panel in selectedPanels)
                {
                    Controls.Remove(panel);
                    panelList.Remove(panel);
                }
                selectedPanels.Clear();
                selectedSites.Clear();
                rsMode = false;
            }
            else
            {
                cefecik.Settings.History.Clear();
                foreach (Panel x in panelList)
                {
                    Controls.Remove(x);
                }
                panelList.Clear();
                selectedPanels.Clear();
                selectedSites.Clear();
            }
        }

        private bool didLostFocus = false;

        private void frmHistory_Leave(object sender, EventArgs e)
        {
            didLostFocus = true;
        }

        private void frmHistory_Enter(object sender, EventArgs e)
        {
            if (didLostFocus)
            {
                didLostFocus = false;
                RefreshList();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Enabled = !cefecik._Incognito;
            lbEmpty.Visible = panelList.Count == 0;
            htButton1.Visible = panelList.Count != 0;
            lbEmpty.Text = cefecik.anaform.empty;
            BackColor = cefecik.Settings.Theme.BackColor;
            ForeColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.ForeColor;
            htButton1.BackColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            htButton1.ForeColor = ForeColor;
            foreach (Panel x in panelList)
            {
                x.BackColor = selectedPanels.Contains(x) ? (cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.OverlayColor) : (cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false));
                x.ForeColor = ForeColor;
            }
            switchRSMode();
        }
    }
}