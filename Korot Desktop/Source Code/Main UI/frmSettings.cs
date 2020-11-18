/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using HTAlt.WinForms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmSettings : Form
    {
        #region Managers & Factory

        private frmBlock blockman;
        private frmSites siteman;
        private frmHistory hisman;
        private frmDownload dowman;
        private frmCollection colman;
        private readonly Settings Settings;
        private readonly frmCEF cefform;

        public frmSettings(Settings _Settings, frmCEF _cefform)
        {
            cefform = _cefform;
            Settings = _Settings;
            InitializeComponent();
            EasterEggs();
            if (!cefform.anaform.isUpdateAvailable)
            {
                btUpdater.Enabled = true;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = cefform.anaform.KorotUpToDate;
            }
            else if (cefform.anaform.isUpdateAvailable)
            {
                btUpdater.Enabled = false;
                btUpdater.Visible = false;
                lbUpdateStatus.Text = cefform.anaform.KorotUpdated;
            }
            else if (cefform.anaform.isUpdateError)
            {
                btUpdater.Enabled = true;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = cefform.anaform.KorotUpdateError;
            }
            ReloadTheme(true);
        }

        #endregion Managers & Factory

        #region UI

        private void btClose_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.button1_Click(this, new EventArgs())));
        }

        private bool isSideBarClosed = false;

        private void btSidebar_Click(object sender, EventArgs e)
        {
            btSidebar.ButtonImage = isSideBarClosed ? (HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.cancel : Properties.Resources.cancel_w) : (HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.hamburger : Properties.Resources.hamburger_w);
            isSideBarClosed = !isSideBarClosed;
            ResizeUI();
        }

        private int tmr50event = 50;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Text = tabControl1.SelectedTab.Text + " - Korot";
            lbTitle.Text = tabControl1.SelectedTab.Text;
            ReloadSettings();
            ReloadLanguage();
            if (tmr50event == 50)
            {
                tmr50event = 0;
                ResizeUI();
                if (hisman != null)
                {
                    hisman.RefreshList();
                }
                if (siteman != null)
                {
                    siteman.GenerateUI();
                }
                if (dowman != null)
                {
                    dowman.RefreshList();
                }
                if (colman != null)
                {
                    colman.genColList();
                }
            }
            else
            {
                tmr50event++;
            }
            if (Settings.ThemeChangeForm.Contains(this))
            {
                Settings.ThemeChangeForm.Remove(this);
                ReloadTheme();
            }
            if (!cefform.anaform.isUpdateAvailable)
            {
                btUpdater.Enabled = true;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = cefform.anaform.KorotUpToDate;
            }
            else if (cefform.anaform.isUpdateAvailable)
            {
                btUpdater.Enabled = false;
                btUpdater.Visible = false;
                lbUpdateStatus.Text = cefform.anaform.KorotUpdated;
            }
            else if (cefform.anaform.isUpdateError)
            {
                btUpdater.Enabled = true;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = cefform.anaform.KorotUpdateError;
            }
        }

        internal void SwitchSettings()
        {
            label4_Click(this, new EventArgs());
        }

        internal void SwitchThemes()
        {
            lbThemes_Click(this, new EventArgs());
        }

        internal void SwitchSite()
        {
            lbSiteSettings_Click(this, new EventArgs());
        }

        internal void SwitchBlock()
        {
            lbBlock_Click(this, new EventArgs());
        }

        internal void SwitchDownloads()
        {
            label6_Click(this, new EventArgs());
        }

        internal void SwitchAbout()
        {
            label8_Click(this, new EventArgs());
        }

        internal void SwitchHistory()
        {
            label5_Click(this, new EventArgs());
        }

        internal void SwitchCollections()
        {
            label7_Click(this, new EventArgs());
        }

        private Theme loadedTheme;

        private void ReloadTheme(bool force = false)
        {
            if (loadedTheme != Settings.Theme || force)
            {
                loadedTheme = Settings.Theme;
                BackColor = Settings.Theme.BackColor;
                ForeColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.ForeColor;
                bool isbright = HTAlt.Tools.IsBright(Settings.Theme.BackColor);
                Color backcolor2 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 20, false);
                Color backcolor3 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 40, false);
                Color backcolor4 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.ShiftBrightness(Settings.Theme.BackColor, 60, false);
                Color rbc2 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(backcolor2);
                Color rbc3 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(backcolor3);
                Color rbc4 = Settings.NinjaMode ? Settings.Theme.BackColor : HTAlt.Tools.AutoWhiteBlack(backcolor4);
                pbOverlay.BackColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsDownload.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsDoNotTrack.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsFav.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;
                hsOpen.OverlayColor = Settings.NinjaMode ? Settings.Theme.BackColor : Settings.Theme.OverlayColor;


                pbPrev.Image = Settings.NinjaMode ? null : (isbright ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w);
                pbNextTheme.Image = Settings.NinjaMode ? null : (isbright ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w);

                llLicenses.ActiveLinkColor = Settings.Theme.OverlayColor;
                llLicenses.DisabledLinkColor = Settings.Theme.OverlayColor;
                llLicenses.LinkColor = Settings.Theme.OverlayColor;
                llLicenses.VisitedLinkColor = Settings.Theme.OverlayColor;
                llLicenses.ForeColor = Settings.Theme.OverlayColor;

                btClose.ButtonImage = Settings.NinjaMode ? null : (!isbright ? Properties.Resources.cancel_w : Properties.Resources.cancel);
                pSidebar.BackColor = backcolor3;
                pSidebar.ForeColor = ForeColor;
                pTitle.BackColor = backcolor4;
                pTitle.ForeColor = ForeColor;
                btSidebar.ButtonImage = !isSideBarClosed ? (HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.cancel : Properties.Resources.cancel_w) : (HTAlt.Tools.IsBright(Settings.Theme.BackColor) ? Properties.Resources.hamburger : Properties.Resources.hamburger_w);


                btACClean.BackColor = backcolor2;
                btACClean.ForeColor = ForeColor;


                hsCleanCache.BackColor = Settings.Theme.BackColor; hsCleanCache.ButtonColor = rbc2; hsCleanCache.ButtonHoverColor = rbc3; hsCleanCache.ButtonPressedColor = rbc4;
                hsCC1.BackColor = Settings.Theme.BackColor; hsCC1.ButtonColor = rbc2; hsCC1.ButtonHoverColor = rbc3; hsCC1.ButtonPressedColor = rbc4;
                hsCC2.BackColor = Settings.Theme.BackColor; hsCC2.ButtonColor = rbc2; hsCC2.ButtonHoverColor = rbc3; hsCC2.ButtonPressedColor = rbc4;
                hsCleanHistory.BackColor = Settings.Theme.BackColor; hsCleanHistory.ButtonColor = rbc2; hsCleanHistory.ButtonHoverColor = rbc3; hsCleanHistory.ButtonPressedColor = rbc4;
                hsCHFile.BackColor = Settings.Theme.BackColor; hsCHFile.ButtonColor = rbc2; hsCHFile.ButtonHoverColor = rbc3; hsCHFile.ButtonPressedColor = rbc4;
                hsCHDay.BackColor = Settings.Theme.BackColor; hsCHDay.ButtonColor = rbc2; hsCHDay.ButtonHoverColor = rbc3; hsCHDay.ButtonPressedColor = rbc4;
                hsCHOld.BackColor = Settings.Theme.BackColor; hsCHOld.ButtonColor = rbc2; hsCHOld.ButtonHoverColor = rbc3; hsCHOld.ButtonPressedColor = rbc4;
                hsCleanDownload.BackColor = Settings.Theme.BackColor; hsCleanDownload.ButtonColor = rbc2; hsCleanDownload.ButtonHoverColor = rbc3; hsCleanDownload.ButtonPressedColor = rbc4;

                pCleanCache.BackColor = backcolor2; pCleanCache.ForeColor = ForeColor;
                pCleanHistory.BackColor = backcolor2; pCleanHistory.ForeColor = ForeColor;


                for (int i = 0; i < tabControl1.TabPages.Count;i++)
                {
                    var tp = tabControl1.TabPages[i];
                    tp.BackColor = BackColor;
                    tp.ForeColor = ForeColor;
                }

                lbSettings.BackColor = Color.Transparent;
                lbSettings.ForeColor = ForeColor;
                pbBack.BackColor = Settings.Theme.BackColor;
                fromHour.BackColor = backcolor2;
                fromHour.ForeColor = ForeColor;
                fromMin.BackColor = backcolor2;
                fromMin.ForeColor = ForeColor;
                toHour.BackColor = backcolor2;
                toHour.ForeColor = ForeColor;
                toMin.BackColor = backcolor2;
                toMin.ForeColor = ForeColor;
                cmsSearchEngine.BackColor = Settings.Theme.BackColor;
                tbHomepage.ForeColor = ForeColor;
                tbSearchEngine.ForeColor = ForeColor;
                hsNotificationSound.BackColor = Settings.Theme.BackColor;
                hsNotificationSound.ButtonColor = rbc2;
                hsNotificationSound.ButtonHoverColor = rbc3;
                hsNotificationSound.ButtonPressedColor = rbc4;
                hsSilent.BackColor = Settings.Theme.BackColor;
                hsSilent.ButtonColor = rbc2;
                hsSilent.ButtonHoverColor = rbc3;
                hsSilent.ButtonPressedColor = rbc4;
                hsSchedule.BackColor = Settings.Theme.BackColor;
                hsSchedule.ButtonColor = rbc2;
                hsSchedule.ButtonHoverColor = rbc3;
                hsSchedule.ButtonPressedColor = rbc4;
                tbSoundLoc.BackColor = backcolor2;
                tbSoundLoc.ForeColor = ForeColor;
                hsDefaultSound.BackColor = Settings.Theme.BackColor;
                hsDefaultSound.ButtonColor = rbc2;
                hsDefaultSound.ButtonHoverColor = rbc3;
                hsDefaultSound.ButtonPressedColor = rbc4;
                btOpenSound.BackColor = backcolor3;
                btOpenSound.ForeColor = ForeColor;
                hsAutoRestore.BackColor = Settings.Theme.BackColor;
                hsAutoRestore.ButtonColor = rbc2;
                hsAutoRestore.ButtonHoverColor = rbc3;
                hsAutoRestore.ButtonPressedColor = rbc4;
                hsAutoForeColor.BackColor = Settings.Theme.BackColor;
                hsAutoForeColor.ButtonColor = rbc2;
                hsAutoForeColor.ButtonHoverColor = rbc3;
                hsAutoForeColor.ButtonPressedColor = rbc4;
                hsNinja.BackColor = Settings.Theme.BackColor;
                hsNinja.ButtonColor = rbc2;
                hsNinja.ButtonHoverColor = rbc3;
                hsNinja.ButtonPressedColor = rbc4;
                hsDownload.BackColor = Settings.Theme.BackColor;
                hsDownload.ButtonColor = rbc2;
                hsDownload.ButtonHoverColor = rbc3;
                hsDownload.ButtonPressedColor = rbc4;

                hsDoNotTrack.BackColor = Settings.Theme.BackColor;
                hsDoNotTrack.ButtonColor = rbc2;
                hsDoNotTrack.ButtonHoverColor = rbc3;
                hsDoNotTrack.ButtonPressedColor = rbc4;
                hsProxy.BackColor = Settings.Theme.BackColor;
                hsProxy.ButtonColor = rbc2;
                hsProxy.ButtonHoverColor = rbc3;
                hsProxy.ButtonPressedColor = rbc4;

                hsDefaultBrowser.BackColor = Settings.Theme.BackColor;
                hsDefaultBrowser.ButtonColor = rbc2;
                hsDefaultBrowser.ButtonHoverColor = rbc3;
                hsDefaultBrowser.ButtonPressedColor = rbc4;

                hsFav.BackColor = Settings.Theme.BackColor;
                hsFav.ButtonColor = rbc2;
                hsFav.ButtonHoverColor = rbc3;
                hsFav.ButtonPressedColor = rbc4;
                hsOpen.BackColor = Settings.Theme.BackColor;
                hsOpen.ButtonColor = rbc2;
                hsOpen.ButtonHoverColor = rbc3;
                hsOpen.ButtonPressedColor = rbc4;
                cmsSearchEngine.ForeColor = ForeColor;
                btUpdater.BackColor = backcolor2;
                tbHomepage.BackColor = backcolor2;
                tbFolder.BackColor = backcolor2;
                tbStartup.BackColor = backcolor2;
                cmsStartup.BackColor = Settings.Theme.BackColor;
                cmsStartup.ForeColor = ForeColor;
                tbFolder.ForeColor = ForeColor;
                tbStartup.ForeColor = ForeColor;
                btReset.BackColor = backcolor2;
                btDownloadFolder.BackColor = backcolor2;
                tbSearchEngine.BackColor = backcolor2;
                pTitle.BackColor = backcolor2;
                pTitle.ForeColor = ForeColor;
                tbHomepage.BackColor = backcolor2;
                tbSearchEngine.BackColor = backcolor2;
                flpLayout.BackColor = Settings.Theme.BackColor;
                flpLayout.ForeColor = ForeColor;
                flpNewTab.BackColor = Settings.Theme.BackColor;
                flpNewTab.ForeColor = ForeColor;
                flpClose.BackColor = Settings.Theme.BackColor;
                flpClose.ForeColor = ForeColor;
                tbTitle.BackColor = backcolor2;
                tbTitle.ForeColor = ForeColor;
                tbUrl.BackColor = backcolor2;
                tbUrl.ForeColor = ForeColor;
                L0.BackColor = backcolor2;
                L0.ForeColor = ForeColor;
                L1.BackColor = backcolor2;
                L1.ForeColor = ForeColor;
                L2.BackColor = backcolor2;
                L2.ForeColor = ForeColor;
                L3.BackColor = backcolor2;
                L3.ForeColor = ForeColor;
                L4.BackColor = backcolor2;
                L4.ForeColor = ForeColor;
                L5.BackColor = backcolor2;
                L5.ForeColor = ForeColor;
                L6.BackColor = backcolor2;
                L6.ForeColor = ForeColor;
                L7.BackColor = backcolor2;
                L7.ForeColor = ForeColor;
                L8.BackColor = backcolor2;
                L8.ForeColor = ForeColor;
                L9.BackColor = backcolor2;
                L9.ForeColor = ForeColor;
                nudCC1.BackColor = backcolor3; nudCC1.ForeColor = ForeColor;
                nudCC2.BackColor = backcolor3; nudCC2.ForeColor = ForeColor;
                nudCHDay.BackColor = backcolor3; nudCHDay.ForeColor = ForeColor;
                nudCHFile.BackColor = backcolor3; nudCHFile.ForeColor = ForeColor;
                nudCHOld.BackColor = backcolor3; nudCHOld.ForeColor = ForeColor;
                nudSynthRate.BackColor = backcolor3; nudSynthRate.ForeColor = ForeColor;
                nudSynthVol.BackColor = backcolor3; nudSynthVol.ForeColor = ForeColor;
                btNTClear.BackColor = backcolor2;
                btNTClear.ForeColor = ForeColor;
                cmsBStyle.BackColor = Settings.Theme.BackColor;
                cmsBStyle.ForeColor = ForeColor;
            }
        }
        private string loadedLang;

        private void ReloadLanguage(bool force = false)
        {
            if (loadedLang != Settings.LanguageSystem.LangFile || force)
            {
                loadedLang = Settings.LanguageSystem.LangFile;
                lbDefaultBrowser.Text = Settings.LanguageSystem.GetItemText("DefaultBrowserSetting");
                lbSynthRate.Text = Settings.LanguageSystem.GetItemText("SynthRate");
                lbSynthVol.Text = Settings.LanguageSystem.GetItemText("SynthVol");
                lbDefaultNotifSound.Text = Settings.LanguageSystem.GetItemText("UseDefaultSound");
                lbForeColor.Text = Settings.LanguageSystem.GetItemText("ForeColor");
                lbAutoSelect.Text = Settings.LanguageSystem.GetItemText("AutoForeColor");
                lbNinja.Text = Settings.LanguageSystem.GetItemText("NinjaMode");
                tpBlock.Text = Settings.LanguageSystem.GetItemText("BlockMenuTitle");
                lbNTTitle.Text = Settings.LanguageSystem.GetItemText("NewTabEditTitle");
                lbNTUrl.Text = Settings.LanguageSystem.GetItemText("NewTabEditUrl");
                btNTClear.Text = Settings.LanguageSystem.GetItemText("NewTabEditClear");
                lbCollections.Text = Settings.LanguageSystem.GetItemText("Collections");
                tpSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
                lbSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
                lbPlayNotifSound.Text = Settings.LanguageSystem.GetItemText("PlayNotificationSound");
                lbSilentMode.Text = Settings.LanguageSystem.GetItemText("SilentMode");
                lbSchedule.Text = Settings.LanguageSystem.GetItemText("ScheduleSilentMode");
                scheduleFrom.Text = Settings.LanguageSystem.GetItemText("StartFrom");
                scheduleTo.Text = Settings.LanguageSystem.GetItemText("EndAt");
                lb24HType.Text = Settings.LanguageSystem.GetItemText("24HourInfo");
                scheduleEvery.Text = Settings.LanguageSystem.GetItemText("Every");
                lbSunday.Text = Settings.LanguageSystem.GetItemText("Su");
                lbMonday.Text = Settings.LanguageSystem.GetItemText("M");
                lbTuesday.Text = Settings.LanguageSystem.GetItemText("T");
                lbWednesday.Text = Settings.LanguageSystem.GetItemText("W");
                lbThursday.Text = Settings.LanguageSystem.GetItemText("Th");
                lbFriday.Text = Settings.LanguageSystem.GetItemText("F");
                lbSaturday.Text = Settings.LanguageSystem.GetItemText("S");
                tpAbout.Text = Settings.LanguageSystem.GetItemText("About");
                tpSite.Text = Settings.LanguageSystem.GetItemText("SiteSettings");
                tpCollections.Text = Settings.LanguageSystem.GetItemText("Collections");
                tpDownloads.Text = Settings.LanguageSystem.GetItemText("Downloads");
                tpHistory.Text = Settings.LanguageSystem.GetItemText("History");
                lbautoRestore.Text = Settings.LanguageSystem.GetItemText("RestoreOldSessions");
                lbShowFavorites.Text = Settings.LanguageSystem.GetItemText("ShowFavoritesMenu");
                lbNewTabColor.Text = Settings.LanguageSystem.GetItemText("NewTabButtonColor");
                lbCloseColor.Text = Settings.LanguageSystem.GetItemText("CloseButtonColor");
                rbNone.Text = Settings.LanguageSystem.GetItemText("None");
                rbTile.Text = Settings.LanguageSystem.GetItemText("Tile");
                rbCenter.Text = Settings.LanguageSystem.GetItemText("Center");
                rbStretch.Text = Settings.LanguageSystem.GetItemText("Stretch");
                rbZoom.Text = Settings.LanguageSystem.GetItemText("Zoom");
                rbBackColor.Text = Settings.LanguageSystem.GetItemText("BackColor");
                rbForeColor.Text = Settings.LanguageSystem.GetItemText("ForeColor");
                rbOverlayColor.Text = Settings.LanguageSystem.GetItemText("OverlayColor2");
                rbBackColor1.Text = Settings.LanguageSystem.GetItemText("BackColor");
                rbForeColor1.Text = Settings.LanguageSystem.GetItemText("ForeColor");
                rbOverlayColor1.Text = Settings.LanguageSystem.GetItemText("OverlayColor2");
                showNewTabPageToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ShowNewTabPage");
                showHomepageToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ShowHomepage");
                showAWebsiteToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("GoToURL");
                lbDownloadFolder.Text = Settings.LanguageSystem.GetItemText("DownloadToFolder");
                lbAutoDownload.Text = Settings.LanguageSystem.GetItemText("Auto-downloadFolder");
                lbAtStartup.Text = Settings.LanguageSystem.GetItemText("AtStartup");
                btReset.Text = Settings.LanguageSystem.GetItemText("ResetKorotButton");
                lbLastProxy.Text = Settings.LanguageSystem.GetItemText("RememberLastProxy");
                ımageFromURLToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ImageFromBase64");
                ımageFromLocalFileToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("ImageFromFile");
                lbDNT.Text = Settings.LanguageSystem.GetItemText("EnableDoNotTrack");
                llLicenses.Text = Settings.LanguageSystem.GetItemText("LicensesSpecialThanks");
                lbSettings.Text = Settings.LanguageSystem.GetItemText("Settings");
                colorToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("UseBackgroundColor");
                lbBackImageStyle.Text = Settings.LanguageSystem.GetItemText("BackgroundImageLayout");
                btUpdater.Text = Settings.LanguageSystem.GetItemText("CheckForUpdates");
                rbNewTab.Text = Settings.LanguageSystem.GetItemText("NewTab");
                lbDownload.Text = Settings.LanguageSystem.GetItemText("Downloads");
                lbHomepage.Text = Settings.LanguageSystem.GetItemText("HomePage");
                customToolStripMenuItem.Text = Settings.LanguageSystem.GetItemText("Custom");
                lbHistory.Text = Settings.LanguageSystem.GetItemText("History");
                lbAbout.Text = Settings.LanguageSystem.GetItemText("About");
                lbBackColor.Text = Settings.LanguageSystem.GetItemText("BackgroundColor");
                lbOveralColor.Text = Settings.LanguageSystem.GetItemText("OverlayColor");
                lbOpen.Text = Settings.LanguageSystem.GetItemText("OpenFilesAfterDownload");
                lbThemeName.Text = Settings.LanguageSystem.GetItemText("ThemeName");
                lbSearchEngine.Text = Settings.LanguageSystem.GetItemText("SearchEngine");
                btACClean.Text = Settings.LanguageSystem.GetItemText("AutoCleanButton");
                lbCleanCache.Text = Settings.LanguageSystem.GetItemText("AutoCleanCache");
                lbCleanDownload.Text = Settings.LanguageSystem.GetItemText("AutoCleanDownloads");
                lbCleanHistory.Text = Settings.LanguageSystem.GetItemText("AutoCleanHistory");
                string AutoCleanSplitter = "[VAL]";
                string Cache1 = Settings.LanguageSystem.GetItemText("AutoCleanCacheT1");
                string Cache2 = Settings.LanguageSystem.GetItemText("AutoCleanCacheT2");
                lbCC1.Text = Cache1.Substring(0, Cache1.IndexOf(AutoCleanSplitter));
                lbCC2.Text = Cache1.Substring(Cache1.IndexOf(AutoCleanSplitter) + AutoCleanSplitter.Length);
                lbCC3.Text = Cache2.Substring(0, Cache2.IndexOf(AutoCleanSplitter));
                lbCC4.Text = Cache2.Substring(Cache2.IndexOf(AutoCleanSplitter) + AutoCleanSplitter.Length);
                string CH1 = Settings.LanguageSystem.GetItemText("AutoCleanHistoryT1");
                string CH2 = Settings.LanguageSystem.GetItemText("AutoCleanHistoryT2");
                string CH3 = Settings.LanguageSystem.GetItemText("AutoCleanHistoryT3");
                lbCH1.Text = CH1.Substring(0, CH1.IndexOf(AutoCleanSplitter));
                lbCH2.Text = CH1.Substring(CH1.IndexOf(AutoCleanSplitter) + AutoCleanSplitter.Length);
                lbCH3.Text = CH2.Substring(0, CH2.IndexOf(AutoCleanSplitter));
                lbCH4.Text = CH2.Substring(CH2.IndexOf(AutoCleanSplitter) + AutoCleanSplitter.Length);
                lbCH5.Text = CH3.Substring(0, CH3.IndexOf(AutoCleanSplitter));
                lbCH6.Text = CH3.Substring(CH3.IndexOf(AutoCleanSplitter) + AutoCleanSplitter.Length);
                label21.Text = cefform.anaform.aboutInfo + Environment.NewLine + ((!(string.IsNullOrWhiteSpace(Settings.Theme.Author) && string.IsNullOrWhiteSpace(Settings.Theme.Name))) ? Settings.LanguageSystem.GetItemText("AboutInfoTheme").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Settings.Theme.Author) ? cefform.anaform.anon : Settings.Theme.Author).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Settings.Theme.Name) ? cefform.anaform.noname : Settings.Theme.Name) : "");
            }
        }

        private void EasterEggs()
        {
            switch (new Random().Next(0, 100000))
            {
                case 6:
                    lbKorot.Text = "Another Chromium-based web browser";
                    break;

                case 17:
                    lbKorot.Text = "StoneBrowser";
                    break;

                case 45:
                    lbKorot.Text = "null";
                    break;

                case 71:
                    lbKorot.Text = "web browser made by retarded";
                    break;

                case 21:
                    lbKorot.Text = "web browser designed to lag and eat ram";
                    break;

                case 9:
                    lbKorot.Text = "the dog with a butter on him";
                    break;

                case 91:
                    lbKorot.Text = "hey shitass wanna make browser";
                    break;

                case 35:
                    lbKorot.Text = "StoneHomepage";
                    break;

                case 48:
                    lbKorot.Text = "ZStone";
                    break;

                case 31:
                    lbKorot.Text = "Ivan's Sexy Web Browser";
                    break;

                case 69:
                    lbKorot.Text = "haltroy browser";
                    break;

                case 420:
                    lbKorot.Text = "The browser that no one knows";
                    break;

                case 520:
                    lbKorot.Text = "hamood habibi";
                    break;

                case 620:
                    lbKorot.Text = "Korot: TempleOS Edition???r";
                    break;

                case 666:
                    lbKorot.Text = "Korot: Hell Edition";
                    break;

                case 3301:
                    lbKorot.Text = "*crypted text saying Korot*";
                    break;

                case 720:
                    lbKorot.Text = "Korot: Web Simulator + Space Chalice";
                    break;

                case 1080:
                    lbKorot.Text = "It's just a browser, a Chromium-based browser!";
                    break;

                case 22000:
                    lbKorot.Text = "a";
                    break;

                case 12121:
                    lbKorot.Text = "Turkish browser homoseg K12121";
                    break;

                case 34000:
                    lbKorot.Text = "Korot: Android Edition???";
                    break;

                case 32000:
                    lbKorot.Text = "Korot: Linux Edition???";
                    break;

                case 3:
                    lbKorot.Text = "Korot: Afyon Edition???";
                    break;

                case 7:
                    lbKorot.Text = (new Random().Next(0, int.MaxValue) % 2 == 0 ? "Pell" : "Kolme") + " Browser";
                    break;

                case 33:
                    lbKorot.Text = new Random().Next(0, int.MaxValue) % 2 == 0 ? "Webtroy" : "Ninova";
                    break;

                default:
                    lbKorot.Text = new Random().Next(int.MinValue,int.MaxValue) % 2 == 0  ? "Korot" : "korot";
                    break;
            }
        }

        private void ReloadSettings()
        {
            tbHomepage.Text = Settings.Homepage;
            LoadNewTabSites();
            tbSearchEngine.Text = Settings.SearchEngine;
            if (Settings.Homepage == "korot://newtab") { rbNewTab.Checked = true; }
            pbBack.BackColor = Settings.Theme.BackColor;
            pbForeColor.BackColor = Settings.Theme.ForeColor;
            pbOverlay.BackColor = Settings.Theme.OverlayColor;
            lbVersion.Text = Application.ProductVersion.ToString() + " " + "[" + VersionInfo.CodeName + "]" + " " + (Environment.Is64BitProcess ? "(64 bit)" : "(32 bit)");
            label21.Text = cefform.anaform.aboutInfo + Environment.NewLine + ((!(string.IsNullOrWhiteSpace(Settings.Theme.Author) && string.IsNullOrWhiteSpace(Settings.Theme.Name))) ? Settings.LanguageSystem.GetItemText("AboutInfoTheme").Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Settings.Theme.Author) ? cefform.anaform.anon : Settings.Theme.Author).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Settings.Theme.Name) ? cefform.anaform.noname : Settings.Theme.Name) : "");
            hsOpen.Checked = Settings.Downloads.OpenDownload;
            hsAutoRestore.Checked = Settings.AutoRestore;
            nudSynthRate.Value = Settings.SynthRate;
            nudSynthVol.Value = Settings.SynthVolume;
            hsFav.Checked = Settings.Favorites.ShowFavorites;
            hsDefaultBrowser.Checked = Settings.CheckIfDefault;
            switch ((int)Settings.Theme.CloseButtonColor)
            {
                case 0:
                    rbBackColor1.Checked = true;
                    break;

                case 1:
                    rbForeColor1.Checked = true;
                    break;

                case 2:
                    rbOverlayColor1.Checked = true;
                    break;
            }
            switch ((int)Settings.Theme.NewTabColor)
            {
                case 0:
                    rbBackColor.Checked = true;
                    break;

                case 1:
                    rbForeColor.Checked = true;
                    break;

                case 2:
                    rbOverlayColor.Checked = true;
                    break;
            }
            switch (Settings.Theme.BackgroundStyleLayout)
            {
                case 0:
                    rbNone.Checked = true;
                    break;

                case 1:
                    rbTile.Checked = true;
                    break;

                case 2:
                    rbCenter.Checked = true;
                    break;

                case 3:
                    rbStretch.Checked = true;
                    break;

                case 4:
                    rbZoom.Checked = true;
                    break;
            }
            pCleanCache.Enabled = Settings.AutoCleaner.CleanCache;
            hsCleanCache.Checked = Settings.AutoCleaner.CleanCache;
            hsCleanHistory.Checked = Settings.AutoCleaner.CleanHistory;
            pCleanHistory.Enabled = Settings.AutoCleaner.CleanHistory;
            hsCleanDownload.Checked = Settings.AutoCleaner.CleanDownloads;
            hsCleanDownload.Checked = Settings.AutoCleaner.CleanDownloads;
            hsCC1.Checked = Settings.AutoCleaner.CleanCacheFile;
            hsCC2.Checked = Settings.AutoCleaner.CleanCacheDaily;
            hsCHFile.Checked = Settings.AutoCleaner.CleanHistoryFile;
            hsCHDay.Checked = Settings.AutoCleaner.CleanHistoryDaily;
            hsCHOld.Checked = Settings.AutoCleaner.CleanOldHistory;
            nudCC1.Value = Convert.ToDecimal(Settings.AutoCleaner.CacheFileSize);
            nudCC2.Value = Convert.ToDecimal(Settings.AutoCleaner.CleanCacheDay);
            nudCHFile.Value = Convert.ToDecimal(Settings.AutoCleaner.HistoryFileSize);
            nudCHDay.Value = Convert.ToDecimal(Settings.AutoCleaner.CleanHistoryDay);
            nudCHOld.Value = Convert.ToDecimal(Settings.AutoCleaner.OldHistoryDay);
            hsDefaultSound.Checked = Settings.UseDefaultSound;
            tbSoundLoc.Enabled = !hsDefaultSound.Checked;
            tbSoundLoc.Text = Settings.SoundLocation;
            btOpenSound.Enabled = !hsDefaultSound.Checked;
            colorToolStripMenuItem.Checked = Settings.Theme.BackgroundStyle == "BACKCOLOR" ? true : false;
            if (Settings.Startup.ToLower() == "korot://newtab")
            {
                tbStartup.Text = showNewTabPageToolStripMenuItem.Text;
            }
            else if (Settings.Startup.ToLower() == "korot://homepage" || Settings.Startup.ToLower() == Settings.Homepage.ToLower())
            {
                tbStartup.Text = showHomepageToolStripMenuItem.Text;
            }
            else
            {
                tbStartup.Text = Settings.Startup;
            }
            hsAutoForeColor.Checked = Settings.Theme.AutoForeColor;
            hsNinja.Checked = Settings.NinjaMode;
            hsDownload.Checked = Settings.Downloads.UseDownloadFolder;
            lbDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Enabled = hsDownload.Checked;
            btDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Text = Settings.Downloads.DownloadDirectory;
        }

        private void ResizeUI()
        {
            if (!isSideBarClosed)
            {
                // Sidebar
                int[] biggestpp = new int[] { 
                    lbSettings.Location.X + lbSettings.Width + 5,
                    lbHistory.Location.X + lbHistory.Width + 5,
                    lbDownload.Location.X + lbDownload.Width + 5,
                    lbCollections.Location.X + lbCollections.Width + 5,
                    lbThemes.Location.X + lbThemes.Width + 5,
                    lbSiteSettings.Location.X + lbSiteSettings.Width + 5,
                    lbBlocks.Location.X + lbBlocks.Width + 5,
                    lbAutoClean.Location.X + lbAutoClean.Width + 5,
                    lbNewTab.Location.X + lbNewTab.Width + 5,
                    lbLanguage.Location.X + lbLanguage.Width + 5,
                    lbNotifications.Location.X + lbNotifications.Width + 5,
                    lbAbout.Location.X + lbAbout.Width + 5 
                };
                int? maxVal = null;
                int index = -1;
                for (int i = 0; i < biggestpp.Length; i++)
                {
                    int thisNum = biggestpp[i];
                    if (!maxVal.HasValue || thisNum > maxVal.Value)
                    {
                        maxVal = thisNum;
                        index = i;
                    }
                }
                if (maxVal != null && maxVal > 0)
                {
                    pSidebar.Width = (int)maxVal;
                }
            }
            else
            {
                pSidebar.Width = btSidebar.Width + 20;
            }
            lbSettings.Visible = !isSideBarClosed; lbSettings.Enabled = !isSideBarClosed;
            lbDownload.Visible = !isSideBarClosed; lbDownload.Enabled = !isSideBarClosed;
            lbHistory.Visible = !isSideBarClosed; lbHistory.Enabled = !isSideBarClosed;
            lbCollections.Visible = !isSideBarClosed; lbCollections.Enabled = !isSideBarClosed;
            lbAbout.Visible = !isSideBarClosed; lbAbout.Enabled = !isSideBarClosed;
            lbThemes.Visible = !isSideBarClosed; lbThemes.Enabled = !isSideBarClosed;
            lbSiteSettings.Visible = !isSideBarClosed; lbSiteSettings.Enabled = !isSideBarClosed;
            lbBlocks.Visible = !isSideBarClosed; lbBlocks.Enabled = !isSideBarClosed;
            lbAutoClean.Visible = !isSideBarClosed; lbAutoClean.Enabled = !isSideBarClosed;
            lbNewTab.Visible = !isSideBarClosed; lbNewTab.Enabled = !isSideBarClosed;
            lbLanguage.Visible = !isSideBarClosed; lbLanguage.Enabled = !isSideBarClosed;
            lbNotifications.Visible = !isSideBarClosed; lbNotifications.Enabled = !isSideBarClosed;

            pTitle.Location = new Point(pSidebar.Location.X + pSidebar.Width, pTitle.Location.Y); pTitle.Width = Width - pSidebar.Width;
            tabControl1.Location = new Point(pSidebar.Location.X + pSidebar.Width - 5, tabControl1.Location.Y); tabControl1.Width = Width - pSidebar.Width + 5;
            hsCleanCache.Location = new Point(lbCleanCache.Location.X + lbCleanCache.Width, hsCleanCache.Location.Y);
            hsCleanHistory.Location = new Point(lbCleanHistory.Location.X + lbCleanHistory.Width, hsCleanHistory.Location.Y);
            hsCleanDownload.Location = new Point(lbCleanDownload.Location.X + lbCleanDownload.Width, hsCleanDownload.Location.Y);
            btACClean.Location = new Point(( tpSettings.Width - 16 ) - btACClean.Width, btACClean.Location.Y);
            nudCC1.Location = new Point(lbCC1.Location.X + lbCC1.Width, nudCC1.Location.Y);
            lbCC2.Location = new Point(nudCC1.Location.X + nudCC1.Width, lbCC2.Location.Y);
            hsCC1.Location = new Point(lbCC2.Location.X + lbCC2.Width, hsCC1.Location.Y);
            nudCC2.Location = new Point(lbCC3.Location.X + lbCC3.Width, nudCC2.Location.Y);
            lbCC4.Location = new Point(nudCC2.Location.X + nudCC2.Width, lbCC4.Location.Y);
            hsCC2.Location = new Point(lbCC4.Location.X + lbCC4.Width, hsCC2.Location.Y);
            nudCHFile.Location = new Point(lbCH1.Location.X + lbCH1.Width, nudCHFile.Location.Y);
            lbCH2.Location = new Point(nudCHFile.Location.X + nudCHFile.Width, lbCH2.Location.Y);
            hsCHFile.Location = new Point(lbCH2.Location.X + lbCH2.Width, hsCHFile.Location.Y);
            hsDefaultBrowser.Location = new Point(lbDefaultBrowser.Location.X + lbDefaultBrowser.Width, hsDefaultBrowser.Location.Y);
            nudCHDay.Location = new Point(lbCH3.Location.X + lbCH3.Width, nudCHDay.Location.Y);
            lbCH4.Location = new Point(nudCHDay.Location.X + nudCHDay.Width, lbCH4.Location.Y);
            hsCHDay.Location = new Point(lbCH4.Location.X + lbCH4.Width, hsCHDay.Location.Y);
            nudCHOld.Location = new Point(lbCH5.Location.X + lbCH5.Width, nudCHOld.Location.Y);
            lbCH6.Location = new Point(nudCHOld.Location.X + nudCHOld.Width, lbCH6.Location.Y);
            hsCHOld.Location = new Point(lbCH6.Location.X + lbCH6.Width, hsCHOld.Location.Y);
            nudSynthRate.Location = new Point(lbSynthRate.Location.X + lbSynthRate.Width, nudSynthRate.Location.Y);
            nudSynthVol.Location = new Point(lbSynthVol.Location.X + lbSynthVol.Width, nudSynthVol.Location.Y);
            flpFrom.Location = new Point(scheduleFrom.Location.X + scheduleFrom.Width, flpFrom.Location.Y);
            scheduleTo.Location = new Point(flpFrom.Location.X + flpFrom.Width, scheduleTo.Location.Y);
            flpTo.Location = new Point(scheduleTo.Location.X + scheduleTo.Width, flpTo.Location.Y);
            flpEvery.Location = new Point(scheduleEvery.Location.X + scheduleEvery.Width, flpEvery.Location.Y);
            lbVersion.Location = new Point(lbKorot.Location.X + lbKorot.Width, lbVersion.Location.Y);
            flpClose.Location = new Point(lbCloseColor.Location.X + lbCloseColor.Width, flpClose.Location.Y);
            flpClose.Width = ( tpSettings.Width - 16 ) - (lbCloseColor.Width + lbCloseColor.Location.X);
            flpNewTab.Location = new Point(lbNewTabColor.Location.X + lbNewTabColor.Width, flpNewTab.Location.Y);
            flpNewTab.Width = ( tpSettings.Width - 16 ) - (lbNewTabColor.Width + lbNewTabColor.Location.X);
            hsAutoRestore.Location = new Point(lbautoRestore.Location.X + lbautoRestore.Width, hsAutoRestore.Location.Y);
            hsFav.Location = new Point(lbShowFavorites.Location.X + lbShowFavorites.Width, hsFav.Location.Y);
            hsDoNotTrack.Location = new Point(lbDNT.Location.X + lbDNT.Width, hsDoNotTrack.Location.Y);
            hsOpen.Location = new Point(lbOpen.Location.X + lbOpen.Width, hsOpen.Location.Y);
            hsDownload.Location = new Point(lbAutoDownload.Location.X + lbAutoDownload.Width, hsDownload.Location.Y);
            hsProxy.Location = new Point(lbLastProxy.Location.X + lbLastProxy.Width, hsProxy.Location.Y);
            llLicenses.LinkArea = new LinkArea(0, llLicenses.Text.Length);
            llLicenses.Location = new Point(label21.Location.X, label21.Location.Y + label21.Size.Height);
            tbStartup.Location = new Point(lbAtStartup.Location.X + lbAtStartup.Width, tbStartup.Location.Y);
            tbStartup.Width = ( tpSettings.Width - 16 ) - (lbAtStartup.Width + lbAtStartup.Location.X);
            tbTitle.Location = new Point(lbNTTitle.Location.X + lbNTTitle.Width, tbTitle.Location.Y);
            tbTitle.Width = ( tpSettings.Width - 16 ) - (lbNTTitle.Width + lbNTTitle.Location.X);
            tbSoundLoc.Width = ( tpSettings.Width - 16 ) - btOpenSound.Width;
            btOpenSound.Location = new Point(tbSoundLoc.Location.X + tbSoundLoc.Width, btOpenSound.Location.Y);
            tbUrl.Location = new Point(lbNTUrl.Location.X + lbNTUrl.Width, tbUrl.Location.Y);
            tbUrl.Width = ( tpSettings.Width - 16 ) - (lbNTUrl.Width + lbNTUrl.Location.X);
            flpLayout.Location = new Point(lbBackImageStyle.Location.X + lbBackImageStyle.Width, flpLayout.Location.Y);
            flpLayout.Width = ( tpSettings.Width - 16 ) - (lbBackImageStyle.Width + lbBackImageStyle.Location.X);
            pbBack.Location = new Point(lbBackColor.Location.X + lbBackColor.Width, pbBack.Location.Y);
            pbForeColor.Location = new Point(lbForeColor.Location.X + lbForeColor.Width, pbForeColor.Location.Y);
            lbAutoSelect.Location = new Point(pbForeColor.Location.X + pbForeColor.Width, lbAutoSelect.Location.Y);
            hsDefaultSound.Location = new Point(lbDefaultNotifSound.Location.X + lbDefaultNotifSound.Width, hsDefaultSound.Location.Y);
            hsAutoForeColor.Location = new Point(lbAutoSelect.Location.X + lbAutoSelect.Width, hsAutoForeColor.Location.Y);
            hsNinja.Location = new Point(lbNinja.Location.X + lbNinja.Width, hsNinja.Location.Y);
            pbOverlay.Location = new Point(lbOveralColor.Location.X + lbOveralColor.Width, pbOverlay.Location.Y);
            tbFolder.Location = new Point(lbDownloadFolder.Location.X + lbDownloadFolder.Width, tbFolder.Location.Y);
            tbFolder.Width = ( tpSettings.Width - 16 ) - (lbDownloadFolder.Location.X + lbDownloadFolder.Width + btDownloadFolder.Width);
            btDownloadFolder.Location = new Point(tbFolder.Location.X + tbFolder.Width, btDownloadFolder.Location.Y);
            tbHomepage.Location = new Point(lbHomepage.Location.X + lbHomepage.Width, tbHomepage.Location.Y);
            tbHomepage.Width = ( tpSettings.Width - 16 ) - (lbHomepage.Location.X + lbHomepage.Width + rbNewTab.Width);
            rbNewTab.Location = new Point(tbHomepage.Location.X + tbHomepage.Width, rbNewTab.Location.Y);
            tbSearchEngine.Location = new Point(lbSearchEngine.Location.X + lbSearchEngine.Width, tbSearchEngine.Location.Y);
            tbSearchEngine.Width = ( tpSettings.Width - 16 ) - (lbSearchEngine.Location.X + lbSearchEngine.Width);
            btReset.Location = new Point(llLicenses.Location.X, llLicenses.Location.Y + llLicenses.Height);
            lbUpdateStatus.Location = new Point(btReset.Location.X, btReset.Location.Y + btReset.Height);
            btUpdater.Location = new Point(lbUpdateStatus.Location.X, lbUpdateStatus.Location.Y + lbUpdateStatus.Height);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbHistory.Font = new Font(lbHistory.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpHistory;
            
            
            if (hisman is null)
            {
                hisman = new frmHistory(cefform)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    Visible = true,
                    ShowInTaskbar = false,
                };
                tpHistory.Controls.Add(hisman);
                hisman.Show();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbDownload.Font = new Font(lbDownload.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpDownloads;
            
            
            if (dowman is null)
            {
                dowman = new frmDownload(cefform)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    Visible = true,
                    ShowInTaskbar = false,
                };
                tpDownloads.Controls.Add(dowman);
                dowman.Show();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbCollections.Font = new Font(lbCollections.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpCollections;
            
            
            if (colman is null)
            {
                colman = new frmCollection(cefform)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    Visible = true,
                    ShowInTaskbar = false,
                };
                tpCollections.Controls.Add(colman);
                colman.Show();
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbAbout.Font = new Font(lbAbout.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpAbout;
            
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbSettings.Font = new Font(lbSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpSettings;
            
            
        }

        private void lbThemes_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbThemes.Font = new Font(lbSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpTheme;
            
            
        }

        private void lbAutoClean_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbAutoClean.Font = new Font(lbSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpAutoClear;
            
            
        }

        private void lbNewTab_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbNewTab.Font = new Font(lbSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpNewTab;
            
            
        }

        private void lbLanguage_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbLanguage.Font = new Font(lbSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpLang;
            
            
        }

        private void lbNotifications_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbNotifications.Font = new Font(lbSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpNotifications;
            
            
        }

        private void ResetSidebarText()
        {
            lbSettings.Font = new Font("Ubuntu", 15F);
            lbDownload.Font = new Font("Ubuntu", 15F);
            lbHistory.Font = new Font("Ubuntu", 15F);
            lbCollections.Font = new Font("Ubuntu", 15F);
            lbAbout.Font = new Font("Ubuntu", 15F);
            lbThemes.Font = new Font("Ubuntu", 15F);
            lbAutoClean.Font = new Font("Ubuntu", 15F);
            lbLanguage.Font = new Font("Ubuntu", 15F);
            lbNotifications.Font = new Font("Ubuntu", 15F);
            lbNewTab.Font = new Font("Ubuntu", 15F);
            lbSiteSettings.Font = new Font("Ubuntu", 15F);
            lbBlocks.Font = new Font("Ubuntu", 15F);
        }

        #endregion UI

        #region Settings

        #region AutoCleaner

        #region Switches

        #region Main

        private void hsCleanCache_CheckedChanged(object sender, EventArgs e)
        {
            pCleanCache.Enabled = hsCleanCache.Checked;
            Settings.AutoCleaner.CleanCache = hsCleanCache.Checked;
        }

        private void hsCleanHistory_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanHistory = hsCleanHistory.Checked;
            pCleanHistory.Enabled = hsCleanHistory.Checked;
        }

        #endregion Main

        #region Cache

        private void hsCC1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanCacheFile = hsCC1.Checked;
        }

        private void hsCC2_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanCacheDaily = hsCC2.Checked;
        }

        #endregion Cache

        #region History

        private void hsCHFile_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanHistoryFile = hsCHFile.Checked;
        }

        private void hsCHDay_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanHistoryDaily = hsCHDay.Checked;
        }

        private void hsCHOld_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanOldHistory = hsCHOld.Checked;
        }

        #endregion History

        #endregion Switches

        #region Buttons

        private void btCleanCache_Click(object sender, EventArgs e)
        {
            cefform.anaform.CleanNow(false);
        }

        #endregion Buttons

        #region NumericUpDowns

        #region Cache

        private void nudCC1_ValueChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CacheFileSize = Convert.ToInt32(nudCC1.Value);
        }

        private void nudCC2_ValueChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanCacheDay = Convert.ToInt32(nudCC2.Value);
        }

        #endregion Cache

        #region History

        private void nudCHFile_ValueChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.HistoryFileSize = Convert.ToInt32(nudCHFile.Value);
        }

        private void nudCHDay_ValueChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.CleanHistoryDay = Convert.ToInt32(nudCHDay.Value);
        }

        private void nudCHOld_ValueChanged(object sender, EventArgs e)
        {
            Settings.AutoCleaner.OldHistoryDay = Convert.ToInt32(nudCHOld.Value);
        }

        #endregion History

        #endregion NumericUpDowns

        #endregion AutoCleaner

        #region Theme

        private void pbBack_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                Color = Settings.Theme.BackColor,
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbBack.BackColor = colorpicker.Color;
                Settings.Theme.BackColor = colorpicker.Color;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void pbForeColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                Color = Settings.Theme.ForeColor,
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbForeColor.BackColor = colorpicker.Color;
                Settings.Theme.AutoForeColor = false;
                Settings.Theme.ForeColor = colorpicker.Color;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void hsAutoForeColor_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Theme.AutoForeColor = hsAutoForeColor.Checked;
            Settings.Theme.ForeColor = hsAutoForeColor.Checked ? (HTAlt.Tools.AutoWhiteBlack(Settings.Theme.BackColor)) : Settings.Theme.ForeColor;
            Settings.JustChangedTheme(); ReloadTheme(true);
        }

        private void pbOverlay_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog
            {
                Color = Settings.Theme.OverlayColor,
                AnyColor = true,
                AllowFullOpen = true,
                FullOpen = true
            };
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbOverlay.BackColor = colorpicker.Color;
                Settings.Theme.OverlayColor = colorpicker.Color;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbNone_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNone.Checked)
            {
                rbTile.Checked = false;
                rbCenter.Checked = false;
                rbStretch.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 0;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbTile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTile.Checked)
            {
                rbNone.Checked = false;
                rbCenter.Checked = false;
                rbStretch.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 1;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbCenter_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCenter.Checked)
            {
                rbTile.Checked = false;
                rbNone.Checked = false;
                rbStretch.Checked = false;
                rbZoom.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 2;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbStretch_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStretch.Checked)
            {
                rbTile.Checked = false;
                rbCenter.Checked = false;
                rbZoom.Checked = false;
                rbNone.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 3;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbZoom.Checked)
            {
                rbTile.Checked = false;
                rbCenter.Checked = false;
                rbStretch.Checked = false;
                rbNone.Checked = false;
                Settings.Theme.BackgroundStyleLayout = 4;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbBackColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBackColor.Checked)
            {
                rbForeColor.Checked = false;
                rbOverlayColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.BackColor;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbForeColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForeColor.Checked)
            {
                rbBackColor.Checked = false;
                rbOverlayColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.ForeColor;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbOverlayColor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOverlayColor.Checked)
            {
                rbForeColor.Checked = false;
                rbBackColor.Checked = false;
                Settings.Theme.NewTabColor = TabColors.OverlayColor;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbBackColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBackColor1.Checked)
            {
                rbForeColor1.Checked = false;
                rbOverlayColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.BackColor;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbForeColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbForeColor1.Checked)
            {
                rbBackColor1.Checked = false;
                rbOverlayColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.ForeColor;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void rbOverlayColor1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOverlayColor1.Checked)
            {
                rbForeColor1.Checked = false;
                rbBackColor1.Checked = false;
                Settings.Theme.CloseButtonColor = TabColors.OverlayColor;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void hsNinja_CheckedChanged(object sender, EventArgs e)
        {
            Settings.NinjaMode = hsNinja.Checked;
            Settings.JustChangedTheme(); ReloadTheme(true);
        }

        private void btThemeWizard_Click(object sender, EventArgs e)
        {
            if (!(cefform.anaform is null))
            {
                cefform.anaform.Invoke(new Action(() =>
                {
                    frmThemeWizard wizard = new frmThemeWizard(Settings);
                    wizard.ShowDialog();
                }));
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        private void TWebStoreTSMI_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.NewTab("https://haltroy.com/store/Korot/Themes/index.html")));
        }

        private void TOpenTSMI_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\\"");
        }

        private void newTSMI_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\"); }
            HTInputBox input = new HTInputBox("Korot", cefform.anaform.ThemeSaveInfo, new HTDialogBoxContext(MessageBoxButtons.OKCancel, false, true), Path.GetFileNameWithoutExtension(Settings.Theme.ThemeFile)) { Icon = cefform.anaform.Icon, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, SetToDefault = cefform.anaform.SetToDefault, BackColor = Settings.Theme.BackColor, AutoForeColor = false, ForeColor = Settings.Theme.ForeColor };
            DialogResult result = input.ShowDialog();
            if (result == DialogResult.OK)
            {
                string themeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + input.TextValue + ".ktf";
                Theme saveTheme = new Theme("", Settings)
                {
                    ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + input.TextValue + ".ktf",
                    BackColor = Settings.Theme.BackColor,
                    OverlayColor = Settings.Theme.OverlayColor,
                    MininmumKorotVersion = new Version(Application.ProductVersion),
                    Version = new Version(Application.ProductVersion),
                    Name = input.TextValue,
                    Author = cefform.userName,
                    BackgroundStyle = Settings.Theme.BackgroundStyle,
                    BackgroundStyleLayout = Settings.Theme.BackgroundStyleLayout,
                    CloseButtonColor = Settings.Theme.CloseButtonColor,
                    NewTabColor = Settings.Theme.NewTabColor
                };
                saveTheme.SaveTheme();
                Settings.Theme.ThemeFile = themeFile;
            }
        }

        private void themeTSMI_Click(object sender, EventArgs e)
        {
            Settings.Theme.LoadFromFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + ((ToolStripMenuItem)sender).Text + ".ktf");
            Settings.JustChangedTheme(); ReloadTheme(true);
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Theme.BackgroundStyle = "BACKCOLOR";
            colorToolStripMenuItem.Checked = true;
            Settings.JustChangedTheme(); ReloadTheme(true);
        }

        private void ımageFromLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog
            {
                Filter = cefform.anaform.imageFiles + "|*.jpg;*.png;*.bmp;*.jpeg;*.jfif;*.gif;*.apng;*.ico;*.svg;*.webp|" + cefform.anaform.allFiles + "|*.*",
                Title = cefform.anaform.selectBackImage,
                Multiselect = false
            };
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                if (HTAlt.Tools.ImageToBase64(Image.FromFile(filedlg.FileName)).Length <= 131072)
                {
                    string imageType = Path.GetExtension(filedlg.FileName).Replace(".", "");
                    Settings.Theme.BackgroundStyle = "background-image: url('data:image/" + imageType + ";base64," + HTAlt.Tools.ImageToBase64(Image.FromFile(filedlg.FileName)) + "');";
                    colorToolStripMenuItem.Checked = false;
                    Settings.JustChangedTheme(); ReloadTheme(true);
                }
                else
                {
                    ımageFromLocalFileToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private void ımageFromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox inputbox = new HTAlt.WinForms.HTInputBox("Korot",
                                                                                            cefform.anaform.enterAValidCode,
                                                                                            "")
            { Icon = Icon, SetToDefault = cefform.anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = Settings.Theme.BackColor, AutoForeColor = false, ForeColor = Settings.Theme.ForeColor };
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                Settings.Theme.BackgroundStyle = inputbox.TextValue + ";";
                colorToolStripMenuItem.Checked = false;
                Settings.JustChangedTheme(); ReloadTheme(true);
            }
        }

        #endregion Theme

        #region Startup

        private void showNewTabPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbStartup.Text = showNewTabPageToolStripMenuItem.Text;
            Settings.Startup = "korot://newtab";
        }

        private void showHomepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbStartup.Text = showHomepageToolStripMenuItem.Text;
            Settings.Startup = Settings.Homepage;
        }

        private void showAWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox inputb = new HTAlt.WinForms.HTInputBox("Korot", cefform.anaform.enterAValidUrl, Settings.SearchEngine) { Icon = Icon, SetToDefault = cefform.anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = Settings.Theme.BackColor, AutoForeColor = false, ForeColor = Settings.Theme.ForeColor };
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(inputb.TextValue) || (inputb.TextValue.ToLower() == "korot://newtab") || inputb.TextValue.ToLower() == Settings.Homepage.ToLower() || inputb.TextValue.ToLower() == "korot://homepage")
                {
                    showAWebsiteToolStripMenuItem_Click(sender, e);
                }
                else
                {
                    Settings.Startup = inputb.TextValue;
                    tbStartup.Text = inputb.TextValue;
                }
            }
        }

        #endregion Startup

        #region General

        private void nudSynthVol_ValueChanged(object sender, EventArgs e)
        {
            Settings.SynthVolume = Convert.ToInt32(nudSynthVol.Value);
        }

        private void nudSynthRate_ValueChanged(object sender, EventArgs e)
        {
            Settings.SynthRate = Convert.ToInt32(nudSynthRate.Value);
        }

        private void lbBlock_Click(object sender, EventArgs e)
        {
            if (blockman is null)
            {
                blockman = new frmBlock(cefform)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    Visible = true,
                    ShowInTaskbar = false,
                };
                tpBlock.Controls.Add(blockman);
                blockman.Show();
            }
            allowSwtich = true;
            tabControl1.SelectedTab = tpBlock;
            ResetSidebarText();
            lbBlocks.Font = new Font(lbBlocks.Font, FontStyle.Bold);
            lbTitle.Text = tabControl1.SelectedTab.Text;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HTAlt.WinForms.HTInputBox inputb = new HTAlt.WinForms.HTInputBox(cefform.anaform.customSearchNote, cefform.anaform.customSearchMessage, Settings.SearchEngine) { Icon = Icon, SetToDefault = cefform.anaform.SetToDefault, StartPosition = FormStartPosition.CenterParent, OK = cefform.anaform.OK, Cancel = cefform.anaform.Cancel, BackColor = Settings.Theme.BackColor, AutoForeColor = false, ForeColor = Settings.Theme.ForeColor };
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (HTAlt.Tools.ValidUrl(inputb.TextValue, new string[] { "http", "https", "about", "ftp", "smtp", "pop", "korot" }) && !inputb.TextValue.StartsWith("korot://") && !inputb.TextValue.StartsWith("file://") && !inputb.TextValue.StartsWith("about"))
                {
                    Settings.SearchEngine = inputb.TextValue;
                    tbSearchEngine.Text = Settings.SearchEngine;
                }
                else
                {
                    customToolStripMenuItem_Click(null, null);
                }
            }
        }

        private void SearchEngineSelection_Click(object sender, EventArgs e)
        {
            Settings.SearchEngine = ((ToolStripMenuItem)sender).Tag.ToString();
            tbSearchEngine.Text = Settings.SearchEngine;
        }

        private void tbHomepage_TextChanged(object sender, EventArgs e)
        {
            Settings.Homepage = tbHomepage.Text;
            rbNewTab.Checked = tbHomepage.Text == "korot://newtab";
        }

        private void rbNewTab_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNewTab.Checked)
            {
                tbHomepage.Text = "korot://newtab";
                Settings.Homepage = tbHomepage.Text;
            }
        }

        private void tbSearchEngine_Click(object sender, EventArgs e)
        {
            cmsSearchEngine.Show(MousePosition);
        }

        private void tbStartup_Click(object sender, EventArgs e)
        {
            cmsStartup.Show(MousePosition);
        }

        private void LOpenTSMI_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Application.StartupPath + "\\Lang\\\"");
        }

        private void LWebStoreTSMI_Click(object sender, EventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.NewTab("https://haltroy.com/store/Korot/Languages/index.html")));
        }

        private void hsFav_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Favorites.ShowFavorites = hsFav.Checked;
            Settings.UpdateFavList();
        }

        private void hsProxy_CheckedChanged(object sender, EventArgs e)
        {
            Settings.RememberLastProxy = hsProxy.Checked;
        }

        private void hsAutoRestore_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoRestore = hsAutoRestore.Checked;
        }

        private void hsDoNotTrack_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DoNotTrack = hsDoNotTrack.Checked;
        }


        private bool allowSwtich = false;

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwtich) { allowSwtich = false; } else { e.Cancel = true; }
        }

        private void htSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            Settings.CheckIfDefault = hsDefaultBrowser.Checked;
        }

        private void lbSiteSettings_Click(object sender, EventArgs e)
        {
            allowSwtich = true;
            ResetSidebarText();
            lbSiteSettings.Font = new Font(lbSiteSettings.Font, FontStyle.Bold);
            tabControl1.SelectedTab = tpSite;
            if (siteman is null)
            {
                siteman = new frmSites(cefform)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill,
                    Visible = true,
                    ShowInTaskbar = false,
                };
                tpSite.Controls.Add(siteman);
                siteman.Show();
            }
            lbTitle.Text = tabControl1.SelectedTab.Text;
        }

        #endregion General

        #region NewTab

        private void LoadNewTabSites()
        {
            NTRefreshNotDone = true;
            //l0
            bool fs0 = Settings.NewTabSites.FavoritedSite0 != null;
            L0T.Text = fs0 ? Settings.NewTabSites.FavoritedSite0.Name : "...";
            L0U.Text = fs0 ? Settings.NewTabSites.FavoritedSite0.Url : "...";
            //l1
            bool fs1 = Settings.NewTabSites.FavoritedSite1 != null;
            L1T.Text = fs1 ? Settings.NewTabSites.FavoritedSite1.Name : "...";
            L1U.Text = fs1 ? Settings.NewTabSites.FavoritedSite1.Url : "...";
            //l2
            bool fs2 = Settings.NewTabSites.FavoritedSite2 != null;
            L2T.Text = fs2 ? Settings.NewTabSites.FavoritedSite2.Name : "...";
            L2U.Text = fs2 ? Settings.NewTabSites.FavoritedSite2.Url : "...";
            //l3
            bool fs3 = Settings.NewTabSites.FavoritedSite3 != null;
            L3T.Text = fs3 ? Settings.NewTabSites.FavoritedSite3.Name : "...";
            L3U.Text = fs3 ? Settings.NewTabSites.FavoritedSite3.Url : "...";
            //l4
            bool fs4 = Settings.NewTabSites.FavoritedSite4 != null;
            L4T.Text = fs4 ? Settings.NewTabSites.FavoritedSite4.Name : "...";
            L4U.Text = fs4 ? Settings.NewTabSites.FavoritedSite4.Url : "...";
            //l5
            bool fs5 = Settings.NewTabSites.FavoritedSite5 != null;
            L5T.Text = fs5 ? Settings.NewTabSites.FavoritedSite5.Name : "...";
            L5U.Text = fs5 ? Settings.NewTabSites.FavoritedSite5.Url : "...";
            //l6
            bool fs6 = Settings.NewTabSites.FavoritedSite6 != null;
            L6T.Text = fs6 ? Settings.NewTabSites.FavoritedSite6.Name : "...";
            L6U.Text = fs6 ? Settings.NewTabSites.FavoritedSite6.Url : "...";
            //l7
            bool fs7 = Settings.NewTabSites.FavoritedSite7 != null;
            L7T.Text = fs7 ? Settings.NewTabSites.FavoritedSite7.Name : "...";
            L7U.Text = fs7 ? Settings.NewTabSites.FavoritedSite7.Url : "...";
            //l8
            bool fs8 = Settings.NewTabSites.FavoritedSite8 != null;
            L8T.Text = fs8 ? Settings.NewTabSites.FavoritedSite8.Name : "...";
            L8U.Text = fs8 ? Settings.NewTabSites.FavoritedSite8.Url : "...";
            //l9
            bool fs9 = Settings.NewTabSites.FavoritedSite9 != null;
            L9T.Text = fs9 ? Settings.NewTabSites.FavoritedSite9.Name : "...";
            L9U.Text = fs9 ? Settings.NewTabSites.FavoritedSite9.Url : "...";
            L0.BorderStyle = editL == 0 ? BorderStyle.FixedSingle : BorderStyle.None;
            L1.BorderStyle = editL == 1 ? BorderStyle.FixedSingle : BorderStyle.None;
            L2.BorderStyle = editL == 2 ? BorderStyle.FixedSingle : BorderStyle.None;
            L3.BorderStyle = editL == 3 ? BorderStyle.FixedSingle : BorderStyle.None;
            L4.BorderStyle = editL == 4 ? BorderStyle.FixedSingle : BorderStyle.None;
            L5.BorderStyle = editL == 5 ? BorderStyle.FixedSingle : BorderStyle.None;
            L6.BorderStyle = editL == 6 ? BorderStyle.FixedSingle : BorderStyle.None;
            L7.BorderStyle = editL == 7 ? BorderStyle.FixedSingle : BorderStyle.None;
            L8.BorderStyle = editL == 8 ? BorderStyle.FixedSingle : BorderStyle.None;
            L9.BorderStyle = editL == 9 ? BorderStyle.FixedSingle : BorderStyle.None;
            NTRefreshNotDone = false;
        }

        private int editL = 0;

        private void tbTitle_TextChanged(object sender, EventArgs e)
        {
            if (NTRefreshNotDone) { return; }
            switch (editL)
            {
                case 0:
                    if (Settings.NewTabSites.FavoritedSite0 == null) { Settings.NewTabSites.FavoritedSite0 = new Site(); }
                    Settings.NewTabSites.FavoritedSite0.Name = tbTitle.Text;
                    break;

                case 1:
                    if (Settings.NewTabSites.FavoritedSite1 == null) { Settings.NewTabSites.FavoritedSite1 = new Site(); }
                    Settings.NewTabSites.FavoritedSite1.Name = tbTitle.Text;
                    break;

                case 2:
                    if (Settings.NewTabSites.FavoritedSite2 == null) { Settings.NewTabSites.FavoritedSite2 = new Site(); }
                    Settings.NewTabSites.FavoritedSite2.Name = tbTitle.Text;
                    break;

                case 3:
                    if (Settings.NewTabSites.FavoritedSite3 == null) { Settings.NewTabSites.FavoritedSite3 = new Site(); }
                    Settings.NewTabSites.FavoritedSite3.Name = tbTitle.Text;
                    break;

                case 4:
                    if (Settings.NewTabSites.FavoritedSite4 == null) { Settings.NewTabSites.FavoritedSite4 = new Site(); }
                    Settings.NewTabSites.FavoritedSite4.Name = tbTitle.Text;
                    break;

                case 5:
                    if (Settings.NewTabSites.FavoritedSite5 == null) { Settings.NewTabSites.FavoritedSite5 = new Site(); }
                    Settings.NewTabSites.FavoritedSite5.Name = tbTitle.Text;
                    break;

                case 6:
                    if (Settings.NewTabSites.FavoritedSite6 == null) { Settings.NewTabSites.FavoritedSite6 = new Site(); }
                    Settings.NewTabSites.FavoritedSite6.Name = tbTitle.Text;
                    break;

                case 7:
                    if (Settings.NewTabSites.FavoritedSite7 == null) { Settings.NewTabSites.FavoritedSite7 = new Site(); }
                    Settings.NewTabSites.FavoritedSite7.Name = tbTitle.Text;
                    break;

                case 8:
                    if (Settings.NewTabSites.FavoritedSite8 == null) { Settings.NewTabSites.FavoritedSite8 = new Site(); }
                    Settings.NewTabSites.FavoritedSite8.Name = tbTitle.Text;
                    break;

                case 9:
                    if (Settings.NewTabSites.FavoritedSite9 == null) { Settings.NewTabSites.FavoritedSite9 = new Site(); }
                    Settings.NewTabSites.FavoritedSite9.Name = tbTitle.Text;
                    break;
            }
            LoadNewTabSites();
        }

        private void tbUrl_TextChanged(object sender, EventArgs e)
        {
            if (NTRefreshNotDone) { return; }
            switch (editL)
            {
                case 0:
                    if (Settings.NewTabSites.FavoritedSite0 == null) { Settings.NewTabSites.FavoritedSite0 = new Site(); }
                    Settings.NewTabSites.FavoritedSite0.Url = tbUrl.Text;
                    break;

                case 1:
                    if (Settings.NewTabSites.FavoritedSite1 == null) { Settings.NewTabSites.FavoritedSite1 = new Site(); }
                    Settings.NewTabSites.FavoritedSite1.Url = tbUrl.Text;
                    break;

                case 2:
                    if (Settings.NewTabSites.FavoritedSite2 == null) { Settings.NewTabSites.FavoritedSite2 = new Site(); }
                    Settings.NewTabSites.FavoritedSite2.Url = tbUrl.Text;
                    break;

                case 3:
                    if (Settings.NewTabSites.FavoritedSite3 == null) { Settings.NewTabSites.FavoritedSite3 = new Site(); }
                    Settings.NewTabSites.FavoritedSite3.Url = tbUrl.Text;
                    break;

                case 4:
                    if (Settings.NewTabSites.FavoritedSite4 == null) { Settings.NewTabSites.FavoritedSite4 = new Site(); }
                    Settings.NewTabSites.FavoritedSite4.Url = tbUrl.Text;
                    break;

                case 5:
                    if (Settings.NewTabSites.FavoritedSite5 == null) { Settings.NewTabSites.FavoritedSite5 = new Site(); }
                    Settings.NewTabSites.FavoritedSite5.Url = tbUrl.Text;
                    break;

                case 6:
                    if (Settings.NewTabSites.FavoritedSite6 == null) { Settings.NewTabSites.FavoritedSite6 = new Site(); }
                    Settings.NewTabSites.FavoritedSite6.Url = tbUrl.Text;
                    break;

                case 7:
                    if (Settings.NewTabSites.FavoritedSite7 == null) { Settings.NewTabSites.FavoritedSite7 = new Site(); }
                    Settings.NewTabSites.FavoritedSite7.Url = tbUrl.Text;
                    break;

                case 8:
                    if (Settings.NewTabSites.FavoritedSite8 == null) { Settings.NewTabSites.FavoritedSite8 = new Site(); }
                    Settings.NewTabSites.FavoritedSite8.Url = tbUrl.Text;
                    break;

                case 9:
                    if (Settings.NewTabSites.FavoritedSite9 == null) { Settings.NewTabSites.FavoritedSite9 = new Site(); }
                    Settings.NewTabSites.FavoritedSite9.Url = tbUrl.Text;
                    break;
            }
            LoadNewTabSites();
        }

        private bool NTRefreshNotDone = false;

        private void siteItem_Click(object sender, EventArgs e)
        {
            if (sender == null) { return; }
            Control cntrl = sender as Control;
            Panel pnl = cntrl is Panel ? cntrl as Panel : cntrl.Parent as Panel;
            if (pnl == null) { return; }
            if (pnl.Tag == null) { return; }
            int itemid = Convert.ToInt32(pnl.Tag);
            editL = itemid;
            NTRefreshNotDone = true;
            LoadNewTabSites();
            switch (itemid)
            {
                case 0:
                    if (Settings.NewTabSites.FavoritedSite0 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite0.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite0.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 1:
                    if (Settings.NewTabSites.FavoritedSite1 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite1.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite1.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 2:
                    if (Settings.NewTabSites.FavoritedSite2 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite2.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite2.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 3:
                    if (Settings.NewTabSites.FavoritedSite3 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite3.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite3.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 4:
                    if (Settings.NewTabSites.FavoritedSite4 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite4.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite4.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 5:
                    if (Settings.NewTabSites.FavoritedSite5 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite5.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite5.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 6:
                    if (Settings.NewTabSites.FavoritedSite6 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite6.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite6.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 7:
                    if (Settings.NewTabSites.FavoritedSite7 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite7.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite7.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 8:
                    if (Settings.NewTabSites.FavoritedSite8 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite8.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite8.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;

                case 9:
                    if (Settings.NewTabSites.FavoritedSite9 == null) { tbTitle.Text = ""; tbUrl.Text = ""; } else { tbTitle.Text = Settings.NewTabSites.FavoritedSite9.Name; tbUrl.Text = Settings.NewTabSites.FavoritedSite9.Url; }
                    tbTitle.Enabled = true;
                    tbUrl.Enabled = true;
                    btNTClear.Enabled = true;
                    break;
            }
            NTRefreshNotDone = false;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            if (!NTRefreshNotDone)
            {
                switch (editL)
                {
                    case 0:
                        Settings.NewTabSites.FavoritedSite0 = null;
                        break;

                    case 1:
                        Settings.NewTabSites.FavoritedSite1 = null;
                        break;

                    case 2:
                        Settings.NewTabSites.FavoritedSite2 = null;
                        break;

                    case 3:
                        Settings.NewTabSites.FavoritedSite3 = null;
                        break;

                    case 4:
                        Settings.NewTabSites.FavoritedSite4 = null;
                        break;

                    case 5:
                        Settings.NewTabSites.FavoritedSite5 = null;
                        break;

                    case 6:
                        Settings.NewTabSites.FavoritedSite6 = null;
                        break;

                    case 7:
                        Settings.NewTabSites.FavoritedSite7 = null;
                        break;

                    case 8:
                        Settings.NewTabSites.FavoritedSite8 = null;
                        break;

                    case 9:
                        Settings.NewTabSites.FavoritedSite9 = null;
                        break;
                }
                tbTitle.Text = "";
                tbUrl.Text = "";
                LoadNewTabSites();
            }
        }

        #endregion NewTab

        #region Download

        private void hsOpen_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Downloads.OpenDownload = hsOpen.Checked;
        }

        private void hsDownload_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Downloads.UseDownloadFolder = hsDownload.Checked;
            lbDownloadFolder.Enabled = hsDownload.Checked;
            tbFolder.Enabled = hsDownload.Checked;
            btDownloadFolder.Enabled = hsDownload.Checked;
        }

        private void tbFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Downloads.DownloadDirectory = tbFolder.Text;
        }

        private void btDownloadFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog() { Description = cefform.anaform.selectAFolder };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbFolder.Text = dialog.SelectedPath;
                Settings.Downloads.DownloadDirectory = tbFolder.Text;
            }
        }

        #endregion Download

        #region Notification

        public int fromH = -1;
        public int fromM = -1;
        public int toH = -1;
        public int toM = -1;
        public bool Nsunday = false;
        public bool Nmonday = false;
        public bool Ntuesday = false;
        public bool Nwednesday = false;
        public bool Nthursday = false;
        public bool Nfriday = false;
        public bool Nsaturday = false;

        public void RefreshScheduledSiletMode()
        {
            if (Settings.AutoSilent)
            {
                string Playlist = Settings.AutoSilentMode;
                string[] SplittedFase = Playlist.Split(':');
                if (SplittedFase.Length - 1 > 9)
                {
                    fromHour.Value = Convert.ToInt32(SplittedFase[0]);
                    fromMin.Value = Convert.ToInt32(SplittedFase[1]);
                    toHour.Value = Convert.ToInt32(SplittedFase[2]);
                    toMin.Value = Convert.ToInt32(SplittedFase[3]);
                    bool sunday = SplittedFase[4] == "1";
                    bool monday = SplittedFase[5] == "1";
                    bool tuesday = SplittedFase[6] == "1";
                    bool wednesday = SplittedFase[7] == "1";
                    bool thursday = SplittedFase[8] == "1";
                    bool friday = SplittedFase[9] == "1";
                    bool saturday = SplittedFase[10] == "1";
                    fromH = Convert.ToInt32(SplittedFase[0]);
                    fromM = Convert.ToInt32(SplittedFase[1]);
                    toH = Convert.ToInt32(SplittedFase[2]);
                    toM = Convert.ToInt32(SplittedFase[3]);
                    Nsunday = sunday;
                    Nmonday = monday;
                    Ntuesday = tuesday;
                    Nwednesday = wednesday;
                    Nthursday = thursday;
                    Nfriday = friday;
                    Nsaturday = saturday;
                    lbSunday.BackColor = sunday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbMonday.BackColor = monday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbTuesday.BackColor = tuesday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbWednesday.BackColor = wednesday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbThursday.BackColor = thursday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbFriday.BackColor = friday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbSaturday.BackColor = saturday ? Settings.Theme.OverlayColor : Settings.Theme.BackColor;
                    lbSunday.Tag = sunday ? "1" : "0";
                    lbMonday.Tag = monday ? "1" : "0";
                    lbTuesday.Tag = tuesday ? "1" : "0";
                    lbWednesday.Tag = wednesday ? "1" : "0";
                    lbThursday.Tag = thursday ? "1" : "0";
                    lbFriday.Tag = friday ? "1" : "0";
                    lbSaturday.Tag = saturday ? "1" : "0";
                }
            }
        }

        public void writeSchedules()
        {
            string ScheduleBuild = fromHour.Value + ":";
            ScheduleBuild += fromMin.Value + ":";
            ScheduleBuild += toHour.Value + ":";
            ScheduleBuild += toMin.Value + ":";
            ScheduleBuild += (lbSunday.Tag != null ? lbSunday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbMonday.Tag != null ? lbMonday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbTuesday.Tag != null ? lbTuesday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbWednesday.Tag != null ? lbWednesday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbThursday.Tag != null ? lbThursday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbFriday.Tag != null ? lbFriday.Tag.ToString() : "0") + ":";
            ScheduleBuild += (lbSaturday.Tag != null ? lbSaturday.Tag.ToString() : "0") + ":";
            Settings.AutoSilentMode = ScheduleBuild;
        }

        private void lbHaftaGunu_Click(object sender, EventArgs e)
        {
            Label myLabel = sender as Label;
            if (myLabel.Tag.ToString() == "1")
            {
                myLabel.Tag = "0";
                myLabel.BackColor = pTitle.BackColor;
            }
            else if (myLabel.Tag.ToString() == "0")
            {
                myLabel.Tag = "1";
                myLabel.BackColor = Settings.Theme.OverlayColor;
            }
            writeSchedules();
            RefreshScheduledSiletMode();
        }

        public void schedule_Click(object sender, EventArgs e)
        {
            writeSchedules();
        }

        private void hsNotificationSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.QuietMode = !hsNotificationSound.Checked;
        }

        private void hsDefaultSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.UseDefaultSound = hsDefaultSound.Checked;
            tbSoundLoc.Enabled = !hsDefaultSound.Checked;
            btOpenSound.Enabled = !hsDefaultSound.Checked;
        }

        private void tbSoundLoc_TextChanged(object sender, EventArgs e)
        {
            Settings.SoundLocation = tbSoundLoc.Text;
        }

        private void btOpenSound_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                InitialDirectory = Settings.SoundLocation,
                FileName = Settings.SoundLocation,
                Multiselect = false,
                Filter = cefform.anaform.soundFiles + "|*.mp3;*.wav;*.aac;*.midi|" + cefform.anaform.allFiles + "|*.*"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Settings.SoundLocation = dialog.FileName;
                tbSoundLoc.Text = dialog.FileName;
            }
        }

        private void hsSilent_CheckedChanged(object sender, EventArgs e)
        {
            Settings.DoNotPlaySound = hsSilent.Checked;
        }

        private void hsSchedule_CheckedChanged(object sender, EventArgs e)
        {
            Settings.AutoSilent = hsSchedule.Checked;
            pSchedule.Enabled = hsSchedule.Checked;
        }

        #endregion Notification

        #endregion Settings

        #region About

        private void llLicenses_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.NewTab("korot://licenses")));
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            cefform.anaform.Invoke(new Action(() =>
            {
                HTMsgBox mesaj = new HTMsgBox("Korot", cefform.anaform.resetConfirm, new HTDialogBoxContext(MessageBoxButtons.YesNoCancel))
                {
                    Icon = Icon,
                    Yes = cefform.anaform.Yes,
                    No = cefform.anaform.No,
                    StartPosition = FormStartPosition.CenterParent,
                    Cancel = cefform.anaform.Cancel,
                    BackColor = Settings.Theme.BackColor,
                    AutoForeColor = false,
                    ForeColor = Settings.Theme.ForeColor
                };
                DialogResult res = mesaj.ShowDialog();
                if (res == DialogResult.Yes)
                {
                    Process.Start(Application.ExecutablePath, "-oobe");
                    Application.Exit();
                }
            }));
        }

        private void btUpdater_Click(object sender, EventArgs e)
        {
            cefform.anaform.CheckForUpdates();
        }

        private void ll32bit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            cefform.Invoke(new Action(() => cefform.NewTab("https://github.com/Haltroy/Korot/issues/142")));
        }

        #endregion About
    }
}