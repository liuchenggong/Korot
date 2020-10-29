/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using CefSharp;
using HTAlt.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmDownload : Form
    {
        internal class DownloadItemSiteHybrid
        {
            public bool IsSite { get; set; }
            public Site Site { get; set; }
            public DownloadItem DownloadItem { get; set; }
            public HTProgressBar ProgressBar { get; set; }
        }

        private readonly frmCEF cefecik;

        public frmDownload(frmCEF cefcik)
        {
            cefecik = cefcik;
            InitializeComponent();
        }

        public Image GetStatusImage(DownloadStatus? status)
        {
            switch (status)
            {
                case DownloadStatus.Cancelled:
                    return Properties.Resources.cancelled;

                case DownloadStatus.Downloaded:
                    return Properties.Resources.downloaded;

                case DownloadStatus.Error:
                    return Properties.Resources.error;

                case DownloadStatus.None:
                    return Properties.Resources.unknown;

                case DownloadStatus.Downloading:
                    return Properties.Resources.downloading;

                default:
                    return Properties.Resources.unknown;
            }
        }

        private readonly List<Panel> panelList = new List<Panel>();
        private readonly List<HTProgressBar> pbList = new List<HTProgressBar>();

        public void RefreshList()
        {
            List<DownloadItemSiteHybrid> dishList = new List<DownloadItemSiteHybrid>();
            foreach (Site x in cefecik.Settings.Downloads.Downloads)
            {
                DownloadItemSiteHybrid dish = new DownloadItemSiteHybrid
                {
                    IsSite = true,
                    Site = x
                };
                dishList.Add(dish);
            }
            foreach (DownloadItem x in cefecik.anaform.CurrentDownloads)
            {
                DownloadItemSiteHybrid dish = new DownloadItemSiteHybrid
                {
                    IsSite = false,
                    DownloadItem = x
                };
                dishList.Add(dish);
            }
            SuspendLayout();
            foreach (DownloadItemSiteHybrid x in dishList)
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
                HTProgressBar htProgressBar1 = new HTAlt.WinForms.HTProgressBar();
                panel2.SuspendLayout();
                //
                // panel2
                //
                panel2.Controls.Add(lbTarih);
                panel2.Controls.Add(label4);
                panel2.Controls.Add(label5);
                panel2.Controls.Add(label6);
                if (!x.IsSite) { panel2.Controls.Add(htProgressBar1); }
                panel2.Dock = System.Windows.Forms.DockStyle.Top;
                panel2.Location = new System.Drawing.Point(0, 13);
                panel2.Margin = new System.Windows.Forms.Padding(5);
                panel2.Padding = new System.Windows.Forms.Padding(5);
                panel2.Size = new System.Drawing.Size(Width, x.IsSite ? 85 : 95);
                panel2.Tag = x;
                panel2.Click += Item_Click;
                //
                // lbTarih
                //
                lbTarih.AutoSize = true;
                lbTarih.Dock = System.Windows.Forms.DockStyle.Bottom;
                lbTarih.Font = new System.Drawing.Font("Ubuntu", 8F);
                lbTarih.Location = new System.Drawing.Point(5, 26);
                lbTarih.ImageAlign = ContentAlignment.MiddleLeft;
                lbTarih.Image = GetStatusImage(x.IsSite ? x.Site.Status : DownloadStatus.Downloading);
                lbTarih.Tag = x;
                lbTarih.Click += Item_Click;
                lbTarih.Text = "       " + cefecik.GetDateInfo(x.IsSite ? DateTime.ParseExact(x.Site.Date, cefecik.DateFormat, null) : DateTime.Now);
                //
                // label4
                //
                label4.AutoSize = true;
                label4.Dock = System.Windows.Forms.DockStyle.Bottom;
                label4.Font = new System.Drawing.Font("Ubuntu", 15F);
                label4.Location = new System.Drawing.Point(5, 42);
                label4.Name = "label4";
                label4.TabIndex = 0;
                label4.Tag = x;
                label4.Text = Path.GetFileNameWithoutExtension(x.IsSite ? x.Site.LocalUrl : x.DownloadItem.FullPath);
                label4.Click += ItemText_Click;
                if (!x.IsSite)
                {
                    //
                    // htProgressBar1
                    //
                    htProgressBar1.BorderThickness = 1;
                    htProgressBar1.DrawBorder = true;
                    htProgressBar1.BorderColor = HTAlt.Tools.AutoWhiteBlack(BackColor);
                    htProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
                    htProgressBar1.Location = new System.Drawing.Point(5, 80);
                    htProgressBar1.Size = new System.Drawing.Size(Width, 10);
                    htProgressBar1.Value = Math.Abs(x.DownloadItem.PercentComplete);
                    htProgressBar1.Click += Item_Click;
                    htProgressBar1.Tag = x;
                    x.ProgressBar = htProgressBar1;
                }
                //
                // label5
                //
                label5.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
                label5.AutoSize = true;
                label5.Font = new System.Drawing.Font("Ubuntu", 12F);
                label5.Location = new System.Drawing.Point(595, 0);
                label5.Click += lbClose_Click;
                label5.Tag = x;
                label5.Text = "X";
                //
                // label6
                //
                label6.AutoSize = true;
                label6.Dock = System.Windows.Forms.DockStyle.Bottom;
                label6.Location = new System.Drawing.Point(5, 67);
                label6.Tag = x;
                label6.Click += ItemUrl_Click;
                label6.Text = x.IsSite ? x.Site.Url : x.DownloadItem.Url;
                Controls.Add(panel2);
                panelList.Add(panel2);
                panel2.ResumeLayout(false);
                panel2.PerformLayout();
            }
            ResumeLayout(false);
            PerformLayout();
        }

        private void ItemText_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            if (cntrl.Tag == null) { return; }
            DownloadItemSiteHybrid dish = cntrl.Tag as DownloadItemSiteHybrid;
            if (dish.IsSite)
            {
                try
                {
                    Process.Start(dish.Site.LocalUrl);
                }
                catch (Exception)
                {
                    Process.Start("explorer.exe", "/select," + dish.Site.LocalUrl);
                }
            }
            else
            {
                try
                {
                    Process.Start(dish.DownloadItem.FullPath);
                }
                catch (Exception)
                {
                    Process.Start("explorer.exe", "/select," + dish.DownloadItem.FullPath);
                }
            }
        }

        private void lbClose_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (sender == null) { return; }
            if (lb.Tag == null) { return; }
            Panel panel = lb.Parent as Panel;
            if (panel == null) { return; }
            DownloadItemSiteHybrid dish = lb.Tag as DownloadItemSiteHybrid;
            if (dish.IsSite)
            {
                cefecik.Settings.Downloads.Downloads.Remove(dish.Site);
            }
            else
            {
                cefecik.anaform.CancelledDownloads.Add(dish.DownloadItem.FullPath);
            }
            panelList.Remove(panel);
            Controls.Remove(panel);
        }

        private void ItemUrl_Click(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            if (sender == null) { return; }
            object tag = lb.Tag;
            if (tag == null) { return; }
            DownloadItemSiteHybrid dish = tag as DownloadItemSiteHybrid;
            cefecik.NewTab(dish.IsSite ? dish.Site.Url : dish.DownloadItem.Url);
        }

        private readonly List<DownloadItemSiteHybrid> selectedDISHes = new List<DownloadItemSiteHybrid>();
        private readonly List<Panel> selectedPanels = new List<Panel>();

        private void Item_Click(object sender, EventArgs e)
        {
            Control cntrl = sender as Control;
            if (cntrl == null) { return; }
            Panel panelcik = cntrl is Panel ? (cntrl as Panel) : (cntrl.Parent as Panel);
            if (panelcik.Tag == null) { return; }
            DownloadItemSiteHybrid site = panelcik.Tag as DownloadItemSiteHybrid;
            if (site == null) { return; }
            if (panelcik.BackColor == cefecik.Settings.Theme.BackColor || (!selectedPanels.Contains(panelcik) && !selectedDISHes.Contains(site)))
            {
                // Not selected, select it
                selectedDISHes.Add(site);
                selectedPanels.Add(panelcik);
                panelcik.BackColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.OverlayColor;
                panelcik.ForeColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.ForeColor;
                switchRSMode();
            }
            else if (panelcik.BackColor == cefecik.Settings.Theme.OverlayColor || selectedPanels.Contains(panelcik) || selectedDISHes.Contains(site))
            {
                // Selected, unselect it
                selectedDISHes.Remove(site);
                selectedPanels.Remove(panelcik);
                panelcik.BackColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false);
                panelcik.ForeColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.ForeColor;
                switchRSMode();
            }
        }

        private void switchRSMode()
        {
            rsMode = !(selectedPanels.Count == 0 && selectedDISHes.Count == 0);
            htButton1.Text = rsMode ? cefecik.anaform.RemoveSelected : cefecik.anaform.Clear;
        }

        private bool rsMode = false;

        private void htButton1_Click(object sender, EventArgs e)
        {
            if (rsMode)
            {
                foreach (DownloadItemSiteHybrid site in selectedDISHes)
                {
                    if (site.IsSite)
                    {
                        cefecik.Settings.Downloads.Downloads.Remove(site.Site);
                    }
                    else
                    {
                        cefecik.anaform.CancelledDownloads.Add(site.DownloadItem.FullPath);
                    }
                }
                foreach (Panel panel in selectedPanels)
                {
                    Controls.Remove(panel);
                    panelList.Remove(panel);
                }
                selectedPanels.Clear();
                selectedDISHes.Clear();
                rsMode = false;
            }
            else
            {
                cefecik.Settings.Downloads.Downloads.Clear();
                foreach (Panel x in panelList)
                {
                    if (x.Tag == null) { return; }
                    DownloadItemSiteHybrid tag = x.Tag as DownloadItemSiteHybrid;
                    if (tag.IsSite) { Controls.Remove(x); }
                }
                panelList.Clear();
                selectedPanels.Clear();
                selectedDISHes.Clear();
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
            foreach (HTProgressBar x in pbList)
            {
                x.BackColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(BackColor, 40, false);
                x.BarColor = cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.OverlayColor;
                x.DrawBorder = true;
                x.BorderThickness = 1;
                x.BorderColor = ForeColor;
            }
            foreach (Panel x in panelList)
            {
                x.BackColor = selectedPanels.Contains(x) ? (cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : cefecik.Settings.Theme.OverlayColor) : (cefecik.Settings.NinjaMode ? cefecik.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false));
                x.ForeColor = ForeColor;
            }
            switchRSMode();
        }
    }
}