using CefSharp;
using HTAlt.WinForms;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        frmCEF cefecik;
        public frmDownload(frmCEF cefcik) 
        {
            cefecik = cefcik;
            InitializeComponent();
        }
        List<Panel> panelList = new List<Panel>();
        List<HTProgressBar> pbList = new List<HTProgressBar>();
        public void RefreshList()
        {
            List<DownloadItemSiteHybrid> dishList = new List<DownloadItemSiteHybrid>();
            foreach (Site x in cefecik.Settings.Downloads.Downloads)
            {
                DownloadItemSiteHybrid dish = new DownloadItemSiteHybrid();
                dish.IsSite = true;
                dish.Site = x;
                dishList.Add(dish);
            }
            foreach (DownloadItem x in cefecik.anaform.CurrentDownloads)
            {
                DownloadItemSiteHybrid dish = new DownloadItemSiteHybrid();
                dish.IsSite = false;
                dish.DownloadItem = x;
                dishList.Add(dish);
            }
            foreach (DownloadItemSiteHybrid x in dishList)
            {
                // Search and find an existing panel with same Site tag, if exist then don't duplicate it (return).
                if (panelList.Find(i => i.Tag == x) != null)
                {
                    return;
                }
                // otherwise, create new one.

                panelList.Add(panel2);
            }
        }

        private void ItemText_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            var cntrl = sender as Control;
            if (cntrl.Tag == null) { return; }
            var dish = cntrl.Tag as DownloadItemSiteHybrid;
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
            }else
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
            var lb = sender as Label;
            if (sender == null) { return; }
            if (lb.Tag == null) { return; }
            var panel = lb.Parent as Panel;
            if (panel == null) { return; }
            var dish = lb.Tag as DownloadItemSiteHybrid;
            if (dish.IsSite)
            {
                cefecik.Settings.Downloads.Downloads.Remove(dish.Site);
            }else
            {
                cefecik.anaform.CancelledDownloads.Add(dish.DownloadItem.FullPath);
            }
            panelList.Remove(panel);
            Controls.Remove(panel);
        }
        private void ItemUrl_Click(object sender,EventArgs e)
        {
            var lb = sender as Label;
            if (sender == null) { return; }
            var tag = lb.Tag;
            if (tag == null) { return; }
            var dish = tag as DownloadItemSiteHybrid;
            cefecik.NewTab(dish.IsSite ? dish.Site.Url : dish.DownloadItem.Url);
        }
        List<DownloadItemSiteHybrid> selectedDISHes = new List<DownloadItemSiteHybrid>();
        List<Panel> selectedPanels = new List<Panel>();
        private void Item_Click(object sender, EventArgs e)
        {
            var cntrl = sender as Control;
            if (cntrl == null) { return; }
            Panel panelcik = cntrl is Panel ? (cntrl as Panel) : (cntrl.Parent as Panel);
            if (panelcik.Tag == null) { return; }
            var site = panelcik.Tag as DownloadItemSiteHybrid;
            if (site == null) { return; }
            if (panelcik.BackColor == cefecik.Settings.Theme.BackColor || ( !selectedPanels.Contains(panelcik) && !selectedDISHes.Contains(site)))
            {
                // Not selected, select it
                selectedDISHes.Add(site);
                selectedPanels.Add(panelcik);
                panelcik.BackColor = cefecik.Settings.Theme.OverlayColor;
                panelcik.ForeColor = HTAlt.Tools.AutoWhiteBlack(panelcik.BackColor);
                switchRSMode();
            }
            else if (panelcik.BackColor == cefecik.Settings.Theme.OverlayColor || selectedPanels.Contains(panelcik) || selectedDISHes.Contains(site))
            {
                // Selected, unselect it
                selectedDISHes.Remove(site);
                selectedPanels.Remove (panelcik);
                panelcik.BackColor = HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false);
                panelcik.ForeColor = HTAlt.Tools.AutoWhiteBlack(panelcik.BackColor);
                switchRSMode();
            }
        }
        void switchRSMode()
        {
            rsMode = !(selectedPanels.Count == 0 && selectedDISHes.Count == 0);
            htButton1.ButtonText = rsMode ? cefecik.RemoveSelected : cefecik.Clear;
        }
        bool rsMode = false;
        private void htButton1_Click(object sender, EventArgs e)
        {
            if (rsMode)
            {
                foreach(DownloadItemSiteHybrid site in selectedDISHes)
                {
                    if (site.IsSite)
                    {
                        cefecik.Settings.Downloads.Downloads.Remove(site.Site);
                    }else
                    {
                        cefecik.anaform.CancelledDownloads.Add(site.DownloadItem.FullPath);
                    }
                }
                foreach(Panel panel in selectedPanels)
                {
                    Controls.Remove(panel);
                    panelList.Remove(panel);
                }
                selectedPanels.Clear();
                selectedDISHes.Clear();
                rsMode = false;
            }else
            {
                cefecik.Settings.Downloads.Downloads.Clear();
                foreach (Panel x in panelList)
                {
                    Controls.Remove(x);
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
            lbEmpty.Text = cefecik.empty;
            BackColor = cefecik.Settings.Theme.BackColor;
            ForeColor = HTAlt.Tools.AutoWhiteBlack(BackColor);
            htButton1.BackColor = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            htButton1.ForeColor = HTAlt.Tools.AutoWhiteBlack(htButton1.BackColor);
            foreach (HTProgressBar x in pbList)
            {
                x.BackColor = HTAlt.Tools.ShiftBrightness(BackColor, 40, false);
                x.BarColor = cefecik.Settings.Theme.OverlayColor;
                x.DrawBorder = true;
                x.BorderThickness = 1;
                x.BorderColor = HTAlt.Tools.AutoWhiteBlack(x.BackColor);
            }
            foreach (Panel x in panelList)
            {
                x.BackColor = selectedPanels.Contains(x) ? cefecik.Settings.Theme.OverlayColor : HTAlt.Tools.ShiftBrightness(cefecik.Settings.Theme.BackColor, 20, false);
                x.ForeColor = HTAlt.Tools.AutoWhiteBlack(x.BackColor);
            }
            switchRSMode();
        }
    }
}
