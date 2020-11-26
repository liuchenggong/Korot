/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using CefSharp.Structs;
using HTAlt.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmHamburger : Form
    {
        private readonly frmCEF cefform;

        public frmHamburger(frmCEF _frmCEF)
        {
            cefform = _frmCEF;
            InitializeComponent();
            LoadExt();
        }

        private void frmHamburger_Leave(object sender, EventArgs e)
        {
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private bool meCol = false;
        private bool meHis = false;
        private bool meDown = false;
        private bool meTheme = false;
        private bool meSet = false;
        private bool meAb = false;
        private int tmr1int = 0;
        private Color _Back;
        private Color _Overlay;
        private bool forceReDraw = false;

        public void ForceReDraw()
        {
            forceReDraw = true;
            timer1_Tick(this, new EventArgs());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_Back != cefform.Settings.Theme.BackColor || forceReDraw)
            {
                _Back = cefform.Settings.Theme.BackColor;
                BackColor = cefform.Settings.Theme.BackColor;
                ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : cefform.Settings.Theme.ForeColor;
                bool isbright = HTAlt.Tools.IsBright(BackColor);
                Color back2 = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
                flpExtensions.BackColor = back2;
                tsSearch.BackColor = back2;
                tsSearch.ForeColor = ForeColor;
                btFullScreen.ButtonImage = cefform.Settings.NinjaMode ? null : (cefform.anaform is null ? (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.fullscreen : Properties.Resources.fullscreen_w) : (cefform.anaform.isFullScreen ? (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.normalscreen : Properties.Resources.normalscreen_w) : (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.fullscreen : Properties.Resources.fullscreen_w)));
                btFullScreen.Enabled = !(cefform.anaform is null);
                btCaseSensitive.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : (cs ? cefform.Settings.Theme.OverlayColor : cefform.Settings.Theme.ForeColor);
                btFindNext.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w);
                htButton4.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.cancel : Properties.Resources.cancel_w);
                btNewWindow.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.newwindow : Properties.Resources.newwindow_w);
                btNewIncWindow.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.inctab : Properties.Resources.inctab_w);
                btScreenShot.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.screenshot : Properties.Resources.screenshot_w);
                btSave.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.collection : Properties.Resources.collection_w);
                btTabColor.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.tab : Properties.Resources.tab_w);
                btRestore.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.restore : Properties.Resources.restore_w);
                btExtStore.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.store : Properties.Resources.store_w);
                btExtFolder.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.extfolder : Properties.Resources.extfolder_w);
                btScriptFolder.ButtonImage = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.extfolder : Properties.Resources.extfolder_w);
                pbcollections.Image = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.collections : Properties.Resources.collections_w);
                pbABout.Image = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.about : Properties.Resources.about_w);
                pbHistory.Image = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.history : Properties.Resources.history_w);
                pbThemes.Image = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.theme : Properties.Resources.theme_w);
                pbSettings.Image = cefform.Settings.NinjaMode ? null : (isbright ? Properties.Resources.Settings : Properties.Resources.Settings_w);
                pbcollections.BackColor = meCol ? back2 : BackColor;
                lbCollections.BackColor = meCol ? back2 : BackColor;
                pbHistory.BackColor = meHis ? back2 : BackColor;
                lbHistory.BackColor = meHis ? back2 : BackColor;
                pbDownloads.BackColor = meDown ? back2 : BackColor;
                lbDownloads.BackColor = meDown ? back2 : BackColor;
                pbThemes.BackColor = meTheme ? back2 : BackColor;
                lbThemes.BackColor = meTheme ? back2 : BackColor;
                lbSettings.BackColor = meSet ? back2 : BackColor;
                pbSettings.BackColor = meSet ? back2 : BackColor;
                lbAbout.BackColor = meAb ? back2 : BackColor;
                pbABout.BackColor = meAb ? back2 : BackColor;
            }

            if (_Overlay != cefform.Settings.Theme.OverlayColor)
            {
                _Overlay = cefform.Settings.Theme.OverlayColor;
                btCaseSensitive.ForeColor = cefform.Settings.NinjaMode ? cefform.Settings.Theme.BackColor : (cs ? cefform.Settings.Theme.OverlayColor : cefform.Settings.Theme.ForeColor);
            }
            bool c = cefform.Settings.IsQuietTime;
            btMute.Enabled = !cefform.Settings.QuietMode;
            tsSearch.Text = isSearchOn ? tsSearch.Text : cefform.anaform.SearchOnPage;
            btResetZoom.Text = cefform.anaform.ResetZoom;
            btDefaultProxy.Text = cefform.anaform.ResetToDefaultProxy;
            lbCollections.Text = cefform.anaform.Collections;
            lbDownloads.Text = cefform.anaform.DownloadsText;
            btBlock.Text = cefform.anaform.BlockThisSite;
            lbHistory.Text = cefform.anaform.HistoryText;
            lbThemes.Text = cefform.anaform.ThemesText;
            lbSettings.Text = cefform.anaform.SettingsText;
            lbAbout.Text = cefform.anaform.AboutText;
            btDefaultProxy.Enabled = cefform.defaultProxy != null;
            bool bright = HTAlt.Tools.IsBright(BackColor);
            pbDownloads.Image = cefform.Settings.NinjaMode ? null : (cefform.anaform is null ? (bright ? Properties.Resources.download : Properties.Resources.download_w) : (cefform.anaform.newDownload ? (bright ? Properties.Resources.download_i : Properties.Resources.download_i_w) : (bright ? Properties.Resources.download : Properties.Resources.download_w)));
            btMute.ButtonImage = cefform.Settings.NinjaMode ? null : (btMute.Enabled ? (bright ? Properties.Resources.mute : Properties.Resources.mute_w) : (cefform.isMuted ? (bright ? Properties.Resources.mute : Properties.Resources.mute_w) : (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.unmute : Properties.Resources.unmute_w)));
            if (cefform != null)
            {
                if (cefform.anaform != null)
                {
                    btRestore.Visible = cefform.anaform.OldSessions != "";
                }
            }
            if (tmr1int == 50)
            {
                tmr1int = 0;
                lbZoom.Invoke(new Action(() => lbZoom.Text = ((cefform.zoomLevel * 100) + 100) + "%"));
            }
            else
            {
                tmr1int++;
            }
            if (findLast)
            {
                lbFindStatus.Text = cefform.anaform.findC + " " + findCurrent + " " + cefform.anaform.findL + " " + cefform.anaform.findT + " " + findTotal;
            }
            else if (findCurrent == 0 && findTotal == 0)
            {
                lbFindStatus.Text = cefform.anaform.noSearch;
            }
            else
            {
                lbFindStatus.Text = cefform.anaform.findC + " " + findCurrent + " " + cefform.anaform.findL + " " + cefform.anaform.findT + " " + findTotal;
            }
        }

        private void htButton5_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void htButton6_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "-incognito");
        }

#pragma warning disable IDE0052
        private int findIdentifier;
#pragma warning restore IDE0052
        private int findTotal;
        private int findCurrent;
        private bool findLast;

        public void FindUpdate(int identifier, int count, int activeMatchOrdinal, bool finalUpdate)
        {
            findIdentifier = identifier;
            findTotal = count;
            findCurrent = activeMatchOrdinal;
            findLast = finalUpdate;
        }

        private void btFindNext_Click(object sender, EventArgs e)
        {
            if (tsSearch.Text != cefform.anaform.SearchOnPage && !string.IsNullOrWhiteSpace(tsSearch.Text))
            {
                cefform.chromiumWebBrowser1.Find(0, tsSearch.Text, true, cs, true);
            }
        }

        private bool cs = false;

        private void btCaseSensitive_Click(object sender, EventArgs e)
        {
            cs = !cs;
            btCaseSensitive.ForeColor = cs ? cefform.Settings.Theme.OverlayColor : HTAlt.Tools.AutoWhiteBlack(cefform.Settings.Theme.BackColor);
        }

        private void tsSearch_Click(object sender, EventArgs e)
        {
            if (!tsSearch.Focused) { tsSearch.SelectAll(); }
        }

        private bool isSearchOn = false;

        private void tsSearch_TextChanged(object sender, EventArgs e)
        {
            if (cefform.chromiumWebBrowser1.IsBrowserInitialized)
            {
                if ((!string.IsNullOrEmpty(tsSearch.Text)) && tsSearch.Text != cefform.anaform.SearchOnPage)
                {
                    isSearchOn = true;
                    cefform.chromiumWebBrowser1.Find(0, tsSearch.Text, true, cs, false);
                }
                else
                {
                    isSearchOn = false;
                    cefform.chromiumWebBrowser1.StopFinding(true);
                }
            }
            else
            {
                isSearchOn = false;
            }
        }

        private void btDefaultProxy_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SetProxyAddress(cefform.defaultProxy)));
            btDefaultProxy.Enabled = false;
        }

        private void btScreenShot_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.GetScreenShot()));
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SavePageAs()));
        }

        private void btFullScreen_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.Fullscreenmode(!cefform.anaform.isFullScreen)));
            btFullScreen.ButtonImage = cefform.anaform.isFullScreen ? (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.normalscreen : Properties.Resources.normalscreen_w) : (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.fullscreen : Properties.Resources.fullscreen_w);
        }

        private void btTabColor_Click(object sender, EventArgs e)
        {
            frmChangeTBTBack dialog = new frmChangeTBTBack(cefform);
            switch (dialog.ShowDialog())
            {
                case DialogResult.OK:
                    cefform.TabColor = dialog.Color;
                    cefform.AutoTabColor = false;
                    cefform.anaform.Invalidate();
                    break;

                case DialogResult.Abort:
                    cefform.TabColor = BackColor;
                    cefform.AutoTabColor = true;
                    cefform.anaform.Invalidate();
                    break;
            }
        }

        private void btRestore_Click(object sender, EventArgs e)
        {
            cefform.anaform.Invoke(new Action(() => cefform.anaform.ReadSession(cefform.anaform.OldSessions)));
            btRestore.Visible = false;
        }

        private void btMute_Click(object sender, EventArgs e)
        {
            cefform.isMuted = !cefform.isMuted;
            btMute.ButtonImage = cefform.isMuted ? (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.mute : Properties.Resources.mute_w) : (HTAlt.Tools.IsBright(BackColor) ? Properties.Resources.unmute : Properties.Resources.unmute_w);
            cefform.chromiumWebBrowser1.GetBrowserHost().SetAudioMuted(cefform.isMuted);
        }

        private void btResetZoom_Click(object sender, EventArgs e)
        {
            cefform.chromiumWebBrowser1.SetZoomLevel(0.0);
        }

        private void btExtFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + cefform.Settings.ProfileName + "\\Extensions\\\"");
        }

        private void btExtStore_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.NewTab("https://haltroy.com/store/Korot/Extensions/index.html")));
        }

        private void lbCollections_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SwitchToCollections()));
            Hide();
        }

        private void lbHistory_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SwitchToHistory()));
            Hide();
        }

        private void pbDownloads_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SwitchToDownloads()));
            Hide();
        }

        private void pbThemes_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SwitchToThemes()));
            Hide();
        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SwitchToSettings()));
            Hide();
        }

        private void pbABout_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.SwitchToAbout()));
            Hide();
        }

        private void extItem_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            if (cntrl.Tag == null) { return; }
            if (!(cntrl.Tag is Extension) || !(cntrl.Tag is string)) { return; }
            if (cntrl.Tag is Extension)
            {
                Extension ext = cntrl.Tag as Extension;
                cefform.applyExtension(ext);
            }
            else if (cntrl.Tag is string)
            {
                string loc = cntrl.Tag as string;
                cefform.chromiumWebBrowser1.Invoke(new Action(() => cefform.chromiumWebBrowser1.ExecuteScriptAsyncWhenPageLoaded(HTAlt.Tools.ReadFile(loc, Encoding.Unicode))));
            }
        }

        public void LoadExt()
        {
            flpExtensions.Controls.Clear();
            foreach (Extension x in cefform.Settings.Extensions.ExtensionList)
            {
                HTButton itemButton = new HTButton()
                {
                    ImageSizeMode = HTButton.ButtonImageSizeMode.Zoom,
                    ButtonImage = File.Exists(x.Icon) ? HTAlt.Tools.ReadFile(x.Icon, "ignore") : (HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Properties.Resources.ext : Properties.Resources.ext_w),
                    Size = new System.Drawing.Size(32, 32),
                    Tag = x,
                };
                itemButton.Click += extItem_Click;
                flpExtensions.Controls.Add(itemButton);
            }
            foreach (string x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + cefform.Settings.ProfileName + "\\Scripts\\"))
            {
                if (x.EndsWith(".js", StringComparison.InvariantCultureIgnoreCase))
                {
                    HTButton itemButton = new HTButton()
                    {
                        ImageSizeMode = HTButton.ButtonImageSizeMode.Zoom,
                        ButtonImage = HTAlt.Tools.IsBright(cefform.Settings.Theme.BackColor) ? Properties.Resources.script : Properties.Resources.script_2,
                        Size = new System.Drawing.Size(32, 32),
                        Tag = x,
                    };
                    itemButton.Click += extItem_Click;
                    flpExtensions.Controls.Add(itemButton);
                }
            }
        }

        private void htButton4_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Collections_MouseEnter(object sender, EventArgs e)
        {
            meCol = true;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbcollections.BackColor = meCol ? back2 : BackColor;
            lbCollections.BackColor = meCol ? back2 : BackColor;
        }

        private void Collections_MouseLeave(object sender, EventArgs e)
        {
            meCol = false;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbcollections.BackColor = meCol ? back2 : BackColor;
            lbCollections.BackColor = meCol ? back2 : BackColor;
        }

        private void Downloads_MouseEnter(object sender, EventArgs e)
        {
            meDown = true;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbDownloads.BackColor = meDown ? back2 : BackColor;
            lbDownloads.BackColor = meDown ? back2 : BackColor;
        }

        private void Downloads_MouseLeave(object sender, EventArgs e)
        {
            meDown = false;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbDownloads.BackColor = meDown ? back2 : BackColor;
            lbDownloads.BackColor = meDown ? back2 : BackColor;
        }

        private void Settings_MouseEnter(object sender, EventArgs e)
        {
            meSet = true;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            lbSettings.BackColor = meSet ? back2 : BackColor;
            pbSettings.BackColor = meSet ? back2 : BackColor;
        }

        private void Settings_MouseLeave(object sender, EventArgs e)
        {
            meSet = false;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            lbSettings.BackColor = meSet ? back2 : BackColor;
            pbSettings.BackColor = meSet ? back2 : BackColor;
        }

        private void History_MouseEnter(object sender, EventArgs e)
        {
            meHis = true;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbHistory.BackColor = meHis ? back2 : BackColor;
            lbHistory.BackColor = meHis ? back2 : BackColor;
        }

        private void History_MouseLeave(object sender, EventArgs e)
        {
            meHis = false;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbHistory.BackColor = meHis ? back2 : BackColor;
            lbHistory.BackColor = meHis ? back2 : BackColor;
        }

        private void About_MouseEnter(object sender, EventArgs e)
        {
            meAb = true;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            lbAbout.BackColor = meAb ? back2 : BackColor;
            pbABout.BackColor = meAb ? back2 : BackColor;
        }

        private void About_MouseLeave(object sender, EventArgs e)
        {
            meAb = false;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            lbAbout.BackColor = meAb ? back2 : BackColor;
            pbABout.BackColor = meAb ? back2 : BackColor;
        }

        private void Themes_MouseEnter(object sender, EventArgs e)
        {
            meTheme = true;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbThemes.BackColor = meTheme ? back2 : BackColor;
            lbThemes.BackColor = meTheme ? back2 : BackColor;
        }

        private void Themes_MouseLeave(object sender, EventArgs e)
        {
            meTheme = false;
            Color back2 = HTAlt.Tools.ShiftBrightness(BackColor, 20, false);
            pbThemes.BackColor = meTheme ? back2 : BackColor;
            lbThemes.BackColor = meTheme ? back2 : BackColor;
        }

        private void btBlock_Click(object sender, EventArgs e)
        {
            BlockSite item = cefform.Settings.Filters.Find(i => i.Address == cefform.chromiumWebBrowser1.Address);
            if (item != null)
            {
                frmBlockSite fbs = new frmBlockSite(cefform, item, item.Address);
                fbs.ShowDialog();
            }
            else
            {
                frmBlockSite fbs = new frmBlockSite(cefform, null, cefform.chromiumWebBrowser1.Address);
                fbs.ShowDialog();
            }
        }

        private void btScriptFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + cefform.Settings.ProfileName + "\\Scripts\\\"");
        }

        private void btZoomPlus_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.zoomIn()));
        }

        private void btZoomMinus_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.zoomOut()));
        }

        private void frmHamburger_Load(object sender, EventArgs e)
        {
            lbZoom.Invoke(new Action(() => lbZoom.Text = ((cefform.zoomLevel * 100) + 100) + "%"));
        }
    }

    internal class FindHandler : IFindHandler
    {
        private readonly frmHamburger frmHam;

        public FindHandler(frmHamburger _frmHam)
        {
            frmHam = _frmHam;
        }

        public void OnFindResult(IWebBrowser chromiumWebBrowser, IBrowser browser, int identifier, int count, Rect selectionRect, int activeMatchOrdinal, bool finalUpdate)
        {
            frmHam.Invoke(new Action(() => frmHam.FindUpdate(identifier, count, activeMatchOrdinal, finalUpdate)));
        }
    }
}