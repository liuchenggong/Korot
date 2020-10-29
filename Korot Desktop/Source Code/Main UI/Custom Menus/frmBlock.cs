/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmBlock : Form
    {
        public frmCEF cefform;

        public frmBlock(frmCEF _cefform)
        {
            cefform = _cefform;
            InitializeComponent();
            GenerateUI();
        }

        private int PanelCount = 0;

        public void GenerateUI()
        {
            Controls.Clear();
            buttonList.Clear();
            PanelCount = 0;
            foreach (BlockSite x in cefform.Settings.Filters)
            {
                GeneratePanel(x);
                PanelCount++;
            }
            if (PanelCount == 0)
            {
                Controls.Add(lbEmpty);
            }
        }

        private readonly List<BlockSite> selectedSites = new List<BlockSite>();
        private readonly List<Panel> selectedPanels = new List<Panel>();

        private void item_Clicked(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            Panel panel = cntrl is Panel ? cntrl as Panel : (cntrl.Parent is FlowLayoutPanel ? cntrl.Parent.Parent as Panel : cntrl.Parent as Panel);
            if (panel.Tag == null || !(panel.Tag is BlockSite)) { return; }
            BlockSite tag = panel.Tag as BlockSite;
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

        private void edit_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            if (cntrl.Tag == null || !(cntrl.Tag is BlockSite)) { return; }
            BlockSite tag = cntrl.Tag as BlockSite;
            frmBlockSite fbs = new frmBlockSite(cefform, tag, tag.Address);
            DialogResult result = fbs.ShowDialog();
            if (result == DialogResult.OK)
            {
                GenerateUI();
            }
        }

        private readonly List<HTButton> buttonList = new List<HTButton>();

        private void GeneratePanel(BlockSite site)
        {
            Panel pSite = new Panel();
            Label lbClose = new Label();
            Label lbAddress = new Label();
            Label lbTitle = new Label();
            HTButton editButton = new HTButton();
            //
            // pSite
            //
            pSite.Controls.Add(editButton);
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
            // editButton
            //
            editButton.Text = cefform.anaform.NewTabEdit;
            editButton.Dock = DockStyle.Bottom;
            editButton.Tag = site;
            editButton.Click += edit_Click;
            buttonList.Add(editButton);
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
            lbAddress.Text = site.Filter;
            //
            // lbTitle
            //
            lbTitle.AutoSize = true;
            lbTitle.Click += item_Clicked;
            lbTitle.Font = new System.Drawing.Font("Ubuntu", 15F);
            lbTitle.Location = new System.Drawing.Point(8, 8);
            lbTitle.Size = new System.Drawing.Size(49, 25);
            lbTitle.Text = site.Address;
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

        private void frmBlock_Leave(object sender, EventArgs e)
        {
            didLostFocus = true;
        }

        private void frmBlock_Enter(object sender, EventArgs e)
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
            Color BackColor3 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefform.Settings.Theme.BackColor, 40, false);
            foreach (Control x in Controls)
            {
                if (x is Panel)
                {
                    x.BackColor = selectedPanels.Contains(x as Panel)
                        ? (cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.OverlayColor)
                        : BackColor2;
                    x.ForeColor = ForeColor;
                }
            }
            foreach (HTButton x in buttonList)
            {
                x.Text = cefform.anaform.NewTabEdit;
                x.BackColor = BackColor3;
                x.ForeColor = ForeColor;
            }
            htButton1.BackColor = BackColor2;
            htButton1.ForeColor = ForeColor;

            lbEmpty.Text = cefform.anaform.empty;
            rsMode = (selectedPanels.Count != 0 && selectedSites.Count != 0);
            htButton1.Visible = (PanelCount == 0);
            htButton1.Text = rsMode ? cefform.anaform.RemoveSelected : cefform.anaform.Clear;
        }

        private void htButton1_Click(object sender, EventArgs e)
        {
            if (rsMode)
            {
                foreach (BlockSite x in selectedSites)
                {
                    cefform.Settings.Filters.Remove(x);
                }
            }
            else
            {
                cefform.Settings.Filters.Clear();
            }
            GenerateUI();
        }
    }
}