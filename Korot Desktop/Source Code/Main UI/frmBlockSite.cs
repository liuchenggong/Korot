/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmBlockSite : Form
    {
        private readonly frmCEF cefform;
        private readonly BlockSite site;
        private readonly BlockSite msite;

        public frmBlockSite(frmCEF _frmCEF, BlockSite _site, string Url)
        {
            cefform = _frmCEF;

            if (_site is null)
            {
                msite = null;
                site = new BlockSite() { Address = Url, BlockLevel = 0, Filter = Settings.BlockLevels.ConvertToLevel0(Url) };
            }
            else
            {
                msite = _site;
                site = new BlockSite() { Address = msite.Address, Filter = msite.Filter, BlockLevel = msite.BlockLevel };
            }
            InitializeComponent();
            tbUrl.Text = site.Address;
            lbFilter.Text = site.Filter;
            if (site.BlockLevel == 0)
            {
                rbL0.Checked = true;
                rbL1.Checked = false;
                rbL2.Checked = false;
                rbL3.Checked = false;
            }
            else if (site.BlockLevel == 1)
            {
                rbL0.Checked = false;
                rbL1.Checked = true;
                rbL2.Checked = false;
                rbL3.Checked = false;
            }
            else if (site.BlockLevel == 2)
            {
                rbL0.Checked = false;
                rbL1.Checked = false;
                rbL2.Checked = true;
                rbL3.Checked = false;
            }
            else if (site.BlockLevel == 3)
            {
                rbL0.Checked = false;
                rbL1.Checked = false;
                rbL2.Checked = false;
                rbL3.Checked = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = cefform.Settings.Theme.BackColor;
            ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
            Color BackColor2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            tbUrl.BackColor = BackColor2;
            tbUrl.ForeColor = ForeColor;
            btDone.BackColor = BackColor2;
            btDone.ForeColor = ForeColor;
            if (site.BlockLevel == 0)
            {
                lbLevelInfo.Text = cefform.anaform.lv0info;
            }
            else if (site.BlockLevel == 1)
            {
                lbLevelInfo.Text = cefform.anaform.lv1info;
            }
            else if (site.BlockLevel == 2)
            {
                lbLevelInfo.Text = cefform.anaform.lv2info;
            }
            else if (site.BlockLevel == 3)
            {
                lbLevelInfo.Text = cefform.anaform.lv3info;
            }
            Text = (msite != null) ? cefform.anaform.editblockitem : cefform.anaform.addblockitem;
            lbLevel.Text = cefform.anaform.blocklevel;
            rbL3.Text = cefform.anaform.lv3;
            rbL2.Text = cefform.anaform.lv2;
            rbL1.Text = cefform.anaform.lv1;
            rbL0.Text = cefform.anaform.lv0;
            btDone.Text = cefform.anaform.Done;
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            site.Address = tbUrl.Text;
            if (site.BlockLevel == 0)
            {
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel0(tbUrl.Text);
            }
            else if (site.BlockLevel == 1)
            {
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel1(tbUrl.Text);
            }
            else if (site.BlockLevel == 2)
            {
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel2(tbUrl.Text);
            }
            else if (site.BlockLevel == 3)
            {
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel3(tbUrl.Text);
            }
        }

        private void rbL0_CheckedChanged(object sender, EventArgs e)
        {
            if (rbL0.Checked)
            {
                site.BlockLevel = 0;
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel0(tbUrl.Text);
                rbL1.Checked = false;
                rbL2.Checked = false;
                rbL3.Checked = false;
            }
        }

        private void rbL1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbL1.Checked)
            {
                site.BlockLevel = 1;
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel1(tbUrl.Text);
                rbL0.Checked = false;
                rbL2.Checked = false;
                rbL3.Checked = false;
            }
        }

        private void rbL2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbL2.Checked)
            {
                site.BlockLevel = 2;
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel2(tbUrl.Text);
                rbL0.Checked = false;
                rbL1.Checked = false;
                rbL3.Checked = false;
            }
        }

        private void rbL3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbL3.Checked)
            {
                site.BlockLevel = 3;
                lbFilter.Text = Settings.BlockLevels.ConvertToLevel3(tbUrl.Text);
                rbL0.Checked = false;
                rbL1.Checked = false;
                rbL2.Checked = false;
            }
        }

        private void btDone_Click(object sender, EventArgs e)
        {
            if (msite == null)
            {
                cefform.Settings.Filters.Add(site);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                msite.Address = site.Address;
                msite.BlockLevel = site.BlockLevel;
                msite.Filter = site.Filter;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}