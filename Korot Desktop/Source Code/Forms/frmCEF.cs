//MIT License
//
//Copyright (c) 2020 Eren "Haltroy" Kanat
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Korot
{
    public partial class frmCEF : Form
    {
        int updateProgress = 0;
        //0 = Checking 1=UpToDate 2=UpdateAvailabe 3=Error
        bool isLoading = false;
        string loaduri = null;
        bool _Incognito = false;
        string userName;
        string userCache;
        frmMain anaform;
        int findIdentifier;
        int findTotal;
        int findCurrent;
        bool findLast;
        string defaultProxy = null;
        public ChromiumWebBrowser chromiumWebBrowser1;
        // [NEWTAB]
        public frmCEF(frmMain rmmain, bool isIncognito = false, string loadurl = "korot://newtab", string profileName = "user0")
        {
            loaduri = loadurl;
            anaform = rmmain;
            _Incognito = isIncognito;
            userName = profileName;
            userCache = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profileName + "\\cache\\";
            InitializeComponent();
            InitializeChromium();
            foreach (Control x in this.Controls)
            {
                try { x.KeyDown += tabform_KeyDown; x.MouseWheel += MouseScroll; }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.debugLogExceptions)
                    {
                        Output.WriteLine(" [Korot.frmCEF.New()] Error: " + ex.ToString());
                    }
                }
            }
            Uri testUri = new Uri("https://haltroy.com");
            Uri aUri = WebRequest.GetSystemWebProxy().GetProxy(testUri);
            if (aUri != testUri)
            {
                defaultProxy = aUri.AbsoluteUri;
            }

            if (defaultProxy == null) { DefaultProxyts.Visible = false; DefaultProxyts.Enabled = false; }
            else
            {
                if (Properties.Settings.Default.rememberLastProxy && !string.IsNullOrWhiteSpace(Properties.Settings.Default.LastProxy)) { SetProxy(chromiumWebBrowser1, Properties.Settings.Default.LastProxy); }
            }
        }
        void RefreshHistory()
        {
            int selectedValue = hlvHistory.SelectedIndices.Count > 0 ? hlvHistory.SelectedIndices[0] : 0;
            hlvHistory.Items.Clear();
            string Playlist = Properties.Settings.Default.History;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                try
                {
                    ListViewItem listV = new ListViewItem(SplittedFase[i].Replace(Environment.NewLine, ""));
                    listV.SubItems.Add(SplittedFase[i + 1].Replace(Environment.NewLine, ""));
                    listV.SubItems.Add(SplittedFase[i + 2].Replace(Environment.NewLine, ""));
                    hlvHistory.Items.Add(listV);
                    i += 3;
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.debugLogExceptions)
                    {
                        Output.WriteLine(" [Korot.frmCEF.RefreshHistory] (Refresh) i=" + i + " Count=" + Count + "Error: " + ex.ToString());
                    }
                }
            }
            try
            {
                hlvHistory.SelectedIndices.Clear();
                if (selectedValue < (hlvHistory.Items.Count - 1))
                {
                    hlvHistory.Items[selectedValue].Selected = true;
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.frmCEF.RefreshHistory] Error: " + ex.ToString());
                }
            }
        }
        public void FindUpdate(int identifier, int count, int activeMatchOrdinal, bool finalUpdate)
        {
            findIdentifier = identifier;
            findTotal = count;
            findCurrent = activeMatchOrdinal;
            findLast = finalUpdate;
        }
        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox("Korot", listBox2.SelectedItem.ToString() + Environment.NewLine + ThemeMessage, this.Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + listBox2.SelectedItem.ToString());
                        comboBox1.Text = listBox2.SelectedItem.ToString().Replace(".ktf", "");
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(" [KOROT] Error at applying theme : " + ex.ToString());
                    }

                }
            }
        }
        #region "Translate"
        public string findC = "Current: ";
        public string findT = "Total: ";
        public string findL = "(Last)";
        public string noSearch = "Not Searching or No Results";
        public string aboutInfo = "Korot uses Chromium by Google using CefSharp. [NEWLINE]Korot is written in C# using Visual Studio Community by Microsoft. [NEWLINE]Korot uses modified version of EasyTabs. [NEWLINE]Translation made by Haltroy. [NEWLINE][THEMENAME] theme made by [THEMEAUTHOR].";
        public string htmlFiles = "HTML File";
        public string print = "Print";
        public string IncognitoT = "Incognito";
        public string IncognitoTitle = "You are now in Incognito Mode!";
        public string IncognitoTitle1 = "Korot will not going to:";
        public string IncognitoT1M1 = "Record your history and downloads";
        public string IncognitoT1M2 = "Record cookies, sessions and form details";
        public string IncognitoT1M3 = "Record settings";
        public string IncognitoTitle2 = "But your activity can recorded by:";
        public string IncognitoT2M1 = "Websites";
        public string IncognitoT2M2 = "Your Internet service provider or your local network owner";
        public string IncognitoT2M3 = "Other viewers";
        public string disallowCookie = "Disallow this page using cookies";
        public string allowCookie = "Allow this page using cookie";
        public string imageFiles = "Image Files";
        public string allFiles = "All Files";
        public string selectBackImage = "Select a background image...";
        public string usingBC = "Using background color";
        public string settingstitle = "Settings";
        public string restoreOldSessions = "Restore last session";
        public string newWindow = "New Window";
        public string newincognitoWindow = "New Incognito Window";
        public string usesCookies = "This website uses cookies.";
        public string notUsesCookies = "This website does not use cookies.";
        public string showCertError = "Show Certificate Error";
        public string CertificateErrorMenuTitle = "Certificate Error Details";
        public string CertificateErrorTitle = "Not Safe";
        public string CertificateError = "This website is using a certificate that has errors.";
        public string CertificateOKTitle = "Safe";
        public string CertificateOK = "This website is using a certificate that has no errors.";
        public string ErrorTheme = "This theme file is corrupted or not suitable for this version.";
        public string ThemeMessage = "Do you want to change to this theme ?";
        public string UserAgentMessage = "Please enter an user agent.";
        public string installStatus = "Downloading Update...";
        public string StatusType = "[PERC]% | [CURRENT] KB downloaded out of [TOTAL] KB.";
        public string enterAValidCode = "Please enter a Valid Base64 Code.";
        public string enterAValidUrl = "Enter a Valid URL";
        public string goTotxt = "Go to \"[TEXT]\"";
        public string SearchOnWeb = "Search \"[TEXT]\"";
        public string defaultproxytext = "Default Proxy";
        public string SearchOnPage = "Search: ";
        //public string CaseSensitive = "Case Sensitive";
        public string privatemode = "Incognito";
        public string updateTitle = "Korot - Update";
        public string updateMessage = "Update available.Do you want to update?";
        public string updateError = "Error while checking for the updates.";
        public string checking = "Checking for updates...";
        public string uptodate = "Your Korot is up-to-date.";
        public string updateavailable = "Update available.";
        public string NewTabtitle = "New Tab";
        public string customSearchNote = "(Note: Searched text will be put after the url)";
        public string customSearchMessage = "Write Custom Search Url";
        public string goBack = "Go Back";
        public string goForward = "Go Forward";
        public string refresh = "Refresh";
        public string refreshNoCache = "Refresh (No Cache)";
        public string stop = "Stop";
        public string selectAll = "Select All";
        public string openLinkInNewTab = "Open Link in New Tab";
        public string copyLink = "Copy Link";
        public string saveImageAs = "Save Image as...";
        public string openImageInNewTab = "Open Image in New Tab";
        public string paste = "Paste";
        public string copy = "Copy";
        public string cut = "Cut";
        public string undo = "Undo";
        public string redo = "Redo";
        public string delete = "Delete";
        public string SearchOrOpenSelectedInNewTab = "Search/Open Selected in New Tab";
        public string developerTools = "Developer Tools";
        public string viewSource = "View Source";
        public string korotdownloading = "Korot - Downloading";
        public string fromtwodot = "From : ";
        public string totwodot = "To : ";
        public string openfileafterdownload = "Open this file after download";
        public string closethisafterdownload = "Close this after download";
        public string open = "Open";
        public string Search = "Search";
        public string run = "Run";
        public string startatstarup = "Run at startup";
        public string empty = "(empty)";
        public ListBox languagedummy = new ListBox();
        public string ErrorPageTitle = "Korot - Error";
        public string MonthNames = "\"January\",\"February\",\"March\",\"April\",\"May\",\"June\",\"July\",\"August\",\"September\",\"October\",\"November\",\"December\"";
        public string DayNames = "\"Sunday\",\"Monday\",\"Tuesday\",\"Wednesday\",\"Thursday\",\"Friday\",\"Saturday\"";
        public string SearchHelpText = "Search on web or enter an URL.";
        public string KT = "Korot can't display this page.";
        public string ET = "One possibility is because of one of these errors:";
        public string E1 = "1.  The URL is incorrect.";
        public string E2 = "2.  The website is not responding,too busy or too slow.";
        public string E3 = "3.  Machine disconnected from Internet or connection is too slow.";
        public string E4 = "4.  Antivirus program thinks this browser is a virus or the Website includes a virus.";
        public string RT = "We recommend:";
        public string R1 = "1.  Checking the URL for errors(like grammar errors). ";
        public string R2 = "2.  Connect the machine to Internet. ";
        public string R3 = "3.  Wait a few minutes and try again. ";
        public string R4 = "4.  Disable Antivirus or add this browser to whitelist of Antivirus.";
        public string switchTo = "Switch to:";
        public string deleteProfile = "Delete this profile";
        public string newprofile = "New Profile";
        public string CertErrorPageTitle = "This website is not secure";
        public string CertErrorPageMessage = "This website is using a certificate that has errors. Which means your information (credit cards,passwords,messages...) can be stolen by unknown people in this website.";
        public string CertErrorPageButton = "I understand these risks.";
        public string renderProcessDies = "Render Process Terminated. Closing application...";
        public string themeInfo = "[THEMENAME] theme made by [THEMEAUTHOR].";
        public string anon = "an unknown person";
        public string noname = "This unknown";
        #endregion
        void SetLanguage(string privatemodetxt, string updatetitletxt, string updatemessagetxt, string updateerrortxt,
                         string CreateTabtext, string csnote, string cse, string nw, string niw, string settingstxt,
                         string historytxt, string hptxt, string setxt, string themetxt, string customtxt, string rstxt,
                         string backstyle, string cleartxt, string AboutText, string gBack, string gForward,
                         string reload, string reloadnoc, string stoop, string selectal, string olint, string cl,
                         string sia, string oint, string pastetxt, string copytxt, string cuttxt, string undotxt,
                         string redotxt, string deletetxt, string oossint, string devtool, string viewsrc,
                         string downloadsstring, string bcolor, string ocolor, string korotdown, string otad,
                         string ctad, string _open, string tarih, string kaynak, string hedef, string kaynak2nokta,
                         string hedef2nokta, string titleErrorPage, string abouttxt, string themename, string save,
                         string defaultproxysetting, string themes, string ont, string openfile,
                         string openfolder, string SearchOnCurrentPage, string CaseSensitivity, string DayName,
                         string MonthName, string searchhelp, string kt, string et, string e1, string e2, string e3,
                         string e4, string rt, string r1, string r2, string r3, string r4, string searchtxt,
                         string SwitchTo, string newProfile, string delProfile, string goToURL, string searchURL,
                         string enterValidUrl, string newProfInfo, string restoreLastSession, string yes, string no,
                         string ok, string cancel, string updateava, string checkmessage, string upToDate,
                         string checkbutton, string installbutton, string installstatus, string statustype,
                         string themeTitle, string themeMessage, string themeError, string cemt, string cet, string ce,
                         string cokt, string cok, string sce, string uc, string nuc, string cept, string cepm,
                         string cepb, string aboutkorot, string licenses, string enablednt, string useBackColor,
                         string _usingBackColor, string imageFromCode, string imageFromFile, string iFiles, string aFiles,
                         string selectABI, string backStyleLay, string dca, string aca, string ititle, string ititle0, string ititle1
            , string it1m1, string it1m2, string it1m3, string ititle2, string it2m1, string it2m2, string it2m3, string _print,
            string hFile, string takeSS, string savePage, string zoomIn, string resetZoom, string zoomOut, string validCode,
            string renderProcessIsKil, string extensionstxt, string themeInfoTXT, string anan, string naname, string fNext,
            string rTP, string fC, string fT, string fL, string fN, string status,string incMode,string incHelp,string incMore)
        {
            ıncognitoModeToolStripMenuItem.Text = incMode.Replace(Environment.NewLine, "");
            thisSessionİsNotGoingToBeSavedToolStripMenuItem.Text = incHelp.Replace(Environment.NewLine, "");
            clickHereToLearnMoreToolStripMenuItem.Text = incMore.Replace(Environment.NewLine, "");
            chStatus.Text = status.Replace(Environment.NewLine, "");
            label23.Text = rTP.Replace(Environment.NewLine, "");
            //tsSearchPrev.Text = fPrev.Replace(Environment.NewLine, "");
            findC = fC.Replace(Environment.NewLine, "");
            findT = fT.Replace(Environment.NewLine, "");
            findL = fL.Replace(Environment.NewLine, "");
            noSearch = fN.Replace(Environment.NewLine, "");
            tsSearchNext.Text = fNext.Replace(Environment.NewLine, "");
            anon = anan.Replace(Environment.NewLine, "");
            noname = naname.Replace(Environment.NewLine, "");
            themeInfo = themeInfoTXT.Replace(Environment.NewLine, "");
            extensionToolStripMenuItem1.Text = extensionstxt.Replace(Environment.NewLine, "");
            renderProcessDies = renderProcessIsKil.Replace(Environment.NewLine, "");
            enterAValidCode = validCode.Replace(Environment.NewLine, "");
            zoomInToolStripMenuItem.Text = zoomIn.Replace(Environment.NewLine, "");
            resetZoomToolStripMenuItem.Text = resetZoom.Replace(Environment.NewLine, "");
            zoomOutToolStripMenuItem.Text = zoomOut.Replace(Environment.NewLine, "");
            htmlFiles = hFile.Replace(Environment.NewLine, "");
            takeAScreenshotToolStripMenuItem.Text = takeSS.Replace(Environment.NewLine, "");
            saveThisPageToolStripMenuItem.Text = savePage.Replace(Environment.NewLine, "");
            print = _print.Replace(Environment.NewLine, "");
            IncognitoT = ititle.Replace(Environment.NewLine, "");
            IncognitoTitle = ititle0.Replace(Environment.NewLine, "");
            IncognitoTitle1 = ititle1.Replace(Environment.NewLine, "");
            IncognitoT1M1 = it1m1.Replace(Environment.NewLine, "");
            IncognitoT1M2 = it1m2.Replace(Environment.NewLine, "");
            IncognitoT1M3 = it1m3.Replace(Environment.NewLine, "");
            IncognitoTitle2 = ititle2.Replace(Environment.NewLine, "");
            IncognitoT2M1 = it2m1.Replace(Environment.NewLine, "");
            IncognitoT2M2 = it2m2.Replace(Environment.NewLine, "");
            IncognitoT2M3 = it2m3.Replace(Environment.NewLine, "");
            disallowCookie = dca.Replace(Environment.NewLine, "");
            allowCookie = aca.Replace(Environment.NewLine, "");
            if (Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser1.Address))
            {
                disallowThisPageForCookieAccessToolStripMenuItem.Text = allowCookie;
            }
            else
            {
                disallowThisPageForCookieAccessToolStripMenuItem.Text = disallowCookie;
            }
            label25.Text = backStyleLay.Replace(Environment.NewLine, "");
            imageFiles = iFiles.Replace(Environment.NewLine, "");
            allFiles = aFiles.Replace(Environment.NewLine, "");
            selectBackImage = selectABI.Replace(Environment.NewLine, "");
            colorToolStripMenuItem.Text = useBackColor.Replace(Environment.NewLine, "");
            usingBC = _usingBackColor.Replace(Environment.NewLine, "");
            ımageFromURLToolStripMenuItem.Text = imageFromCode.Replace(Environment.NewLine, "");
            ımageFromLocalFileToolStripMenuItem.Text = imageFromFile.Replace(Environment.NewLine, "");
            label24.Text = enablednt.Replace(Environment.NewLine, "");
            hsDoNotTrack.Location = new Point(label24.Location.X + label24.Width + 5, label24.Location.Y);
            hsProxy.Location = new Point(label23.Location.X + label23.Width + 5, label23.Location.Y);
            aboutInfo = aboutkorot.Replace(Environment.NewLine, "");
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeAuthor) && string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeName))
            {
                label21.Text = aboutInfo.Replace("[NEWLINE]", Environment.NewLine);
            }
            else
            {
                label21.Text = aboutInfo.Replace("[NEWLINE]", Environment.NewLine) + Environment.NewLine + themeInfo.Replace("[THEMEAUTHOR]", string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeAuthor) ? anon : Properties.Settings.Default.ThemeAuthor).Replace("[THEMENAME]", string.IsNullOrWhiteSpace(Properties.Settings.Default.ThemeName) ? noname : Properties.Settings.Default.ThemeName);
            }
            linkLabel1.Text = licenses.Replace(Environment.NewLine, "");
            linkLabel1.LinkArea = new LinkArea(0, linkLabel1.Text.Length);
            linkLabel1.Location = new Point(label21.Location.X, label21.Location.Y + label21.Size.Height);
            lbSettings.Text = settingstxt.Replace(Environment.NewLine, "");
            CertErrorPageButton = cepb.Replace(Environment.NewLine, "");
            CertErrorPageMessage = cepm.Replace(Environment.NewLine, "");
            CertErrorPageTitle = cept.Replace(Environment.NewLine, "");
            usesCookies = uc.Replace(Environment.NewLine, "");
            notUsesCookies = nuc.Replace(Environment.NewLine, "");
            showCertError = sce.Replace(Environment.NewLine, "");
            CertificateErrorMenuTitle = cemt.Replace(Environment.NewLine, "");
            CertificateErrorTitle = cet.Replace(Environment.NewLine, "");
            CertificateError = ce.Replace(Environment.NewLine, "");
            CertificateOKTitle = cokt.Replace(Environment.NewLine, "");
            CertificateOK = cok.Replace(Environment.NewLine, "");
            ErrorTheme = themeError.Replace(Environment.NewLine, "");
            ThemeMessage = themeMessage.Replace(Environment.NewLine, "");
            btUpdater.Text = checkbutton.Replace(Environment.NewLine, "");
            btInstall.Text = installbutton.Replace(Environment.NewLine, "");
            checking = checkmessage.Replace(Environment.NewLine, "");
            uptodate = upToDate.Replace(Environment.NewLine, "");
            installStatus = installstatus.Replace(Environment.NewLine, "");
            StatusType = statustype.Replace(Environment.NewLine, "");
            radioButton1.Text = CreateTabtext.Replace(Environment.NewLine, "");
            if (updateProgress == 0)
            {
                lbUpdateStatus.Text = checkmessage.Replace(Environment.NewLine, "");
            }
            else if (updateProgress == 1)
            {
                lbUpdateStatus.Text = upToDate.Replace(Environment.NewLine, "");
            }
            else if (updateProgress == 2)
            {
                lbUpdateStatus.Text = updateava.Replace(Environment.NewLine, "");
            }
            else if (updateProgress == 3)
            {
                lbUpdateStatus.Text = updateerrortxt.Replace(Environment.NewLine, "");
            }
            updateavailable = updateava;
            privatemode = privatemodetxt.Replace(Environment.NewLine, "");
            updateTitle = updatetitletxt.Replace(Environment.NewLine, "");
            updateMessage = updatemessagetxt.Replace(Environment.NewLine, "");
            updateError = updateerrortxt.Replace(Environment.NewLine, "");
            NewTabtitle = CreateTabtext.Replace(Environment.NewLine, "");
            customSearchNote = csnote.Replace(Environment.NewLine, "");
            customSearchMessage = cse.Replace(Environment.NewLine, "");
            label12.Text = backstyle.Replace(Environment.NewLine, "");
            textBox4.Location = new Point(label12.Location.X + label12.Width, label12.Location.Y);
            textBox4.Width = tabPage3.Width - (label12.Width + label12.Location.X + 25);
            comboBox3.Location = new Point(label25.Location.X + label25.Width, label25.Location.Y);
            comboBox3.Width = tabPage3.Width - (label25.Width + label25.Location.X + 25);
            newWindow = nw.Replace(Environment.NewLine, "");
            newincognitoWindow = niw.Replace(Environment.NewLine, "");
            anaform.newincwindow = niw.Replace(Environment.NewLine, "");
            anaform.newwindow = nw.Replace(Environment.NewLine, "");
            label6.Text = downloadsstring.Replace(Environment.NewLine, "");
            downloadsToolStripMenuItem.Text = downloadsstring.Replace(Environment.NewLine, "");
            aboutToolStripMenuItem.Text = AboutText.Replace(Environment.NewLine, "");
            label11.Text = hptxt.Replace(Environment.NewLine, "");
            SearchOnPage = SearchOnCurrentPage.Replace(Environment.NewLine, "");
            label26.Text = themetxt.Replace(Environment.NewLine, "");
            tsThemes.Text = themeTitle.Replace(Environment.NewLine, "");
            caseSensitiveToolStripMenuItem.Text = CaseSensitivity.Replace(Environment.NewLine, "");
            //CaseSensitive = CaseSensitivity.Replace(Environment.NewLine, "");
            customToolStripMenuItem.Text = customtxt.Replace(Environment.NewLine, "");
            removeSelectedToolStripMenuItem.Text = rstxt.Replace(Environment.NewLine, "");
            clearToolStripMenuItem.Text = cleartxt.Replace(Environment.NewLine, "");
            settingstitle = settingstxt.Replace(Environment.NewLine, "");
            historyToolStripMenuItem.Text = historytxt.Replace(Environment.NewLine, "");
            label4.Text = historytxt.Replace(Environment.NewLine, "");
            label22.Text = abouttxt.Replace(Environment.NewLine, "");
            goBack = gBack.Replace(Environment.NewLine, "");
            goForward = gForward.Replace(Environment.NewLine, "");
            refresh = reload.Replace(Environment.NewLine, "");
            refreshNoCache = reloadnoc.Replace(Environment.NewLine, "");
            stop = stoop.Replace(Environment.NewLine, "");
            selectAll = selectal.Replace(Environment.NewLine, "");
            openLinkInNewTab = olint.Replace(Environment.NewLine, "");
            copyLink = cl.Replace(Environment.NewLine, "");
            saveImageAs = sia.Replace(Environment.NewLine, "");
            openImageInNewTab = oint.Replace(Environment.NewLine, "");
            paste = pastetxt.Replace(Environment.NewLine, "");
            copy = copytxt.Replace(Environment.NewLine, "");
            cut = cuttxt.Replace(Environment.NewLine, "");
            undo = undotxt.Replace(Environment.NewLine, "");
            redo = redotxt.Replace(Environment.NewLine, "");
            delete = deletetxt.Replace(Environment.NewLine, "");
            SearchOrOpenSelectedInNewTab = oossint.Replace(Environment.NewLine, "");
            developerTools = devtool.Replace(Environment.NewLine, "");
            viewSource = viewsrc.Replace(Environment.NewLine, "");
            restoreOldSessions = restoreLastSession.Replace(Environment.NewLine, "");
            label14.Text = bcolor.Replace(Environment.NewLine, "");
            enterAValidUrl = enterValidUrl.Replace(Environment.NewLine, "");
            label16.Text = ocolor.Replace(Environment.NewLine, "");
            chDate.Text = kaynak.Replace(Environment.NewLine, "");
            fromtwodot = kaynak2nokta.Replace(Environment.NewLine, "");
            chFrom.Text = hedef.Replace(Environment.NewLine, "");
            totwodot = hedef2nokta.Replace(Environment.NewLine, "");
            korotdownloading = korotdown.Replace(Environment.NewLine, "");
            chTo.Text = tarih.Replace(Environment.NewLine, "");
            openfileafterdownload = otad.Replace(Environment.NewLine, "");
            closethisafterdownload = ctad.Replace(Environment.NewLine, "");
            open = _open.Replace(Environment.NewLine, "");
            openLinkİnNewTabToolStripMenuItem.Text = olint.Replace(Environment.NewLine, "");
            openInNewTab.Text = ont.Replace(Environment.NewLine, "");
            removeSelectedTSMI.Text = rstxt.Replace(Environment.NewLine, "");
            clearTSMI.Text = cleartxt.Replace(Environment.NewLine, "");
            openFileToolStripMenuItem.Text = openfile.Replace(Environment.NewLine, "");
            openFileİnExplorerToolStripMenuItem.Text = openfolder.Replace(Environment.NewLine, "");
            removeSelectedToolStripMenuItem1.Text = rstxt.Replace(Environment.NewLine, "");
            clearToolStripMenuItem2.Text = cleartxt.Replace(Environment.NewLine, "");
            DefaultProxyts.Text = defaultproxysetting.Replace(Environment.NewLine, "");
            label13.Text = themename.Replace(Environment.NewLine, "");
            anaform.Yes = yes.Replace(Environment.NewLine, "");
            anaform.No = no.Replace(Environment.NewLine, "");
            anaform.OK = ok.Replace(Environment.NewLine, "");
            anaform.Cancel = cancel.Replace(Environment.NewLine, "");
            button10.Text = save.Replace(Environment.NewLine, "");
            label15.Text = themes.Replace(Environment.NewLine, "");
            SearchOnWeb = searchURL.Replace(Environment.NewLine, "");
            goTotxt = goToURL.Replace(Environment.NewLine, "");
            anaform.newProfileInfo = newProfInfo.Replace(Environment.NewLine, "");
            label9.Text = setxt.Replace(Environment.NewLine, "");
            MonthNames = MonthName.Replace(Environment.NewLine, "");
            DayNames = DayName.Replace(Environment.NewLine, "");
            SearchHelpText = searchhelp.Replace(Environment.NewLine, "");
            ErrorPageTitle = titleErrorPage.Replace(Environment.NewLine, "");
            KT = kt.Replace(Environment.NewLine, "");
            ET = et.Replace(Environment.NewLine, "");
            E1 = e1.Replace(Environment.NewLine, "");
            E2 = e2.Replace(Environment.NewLine, "");
            E3 = e3.Replace(Environment.NewLine, "");
            E4 = e4.Replace(Environment.NewLine, "");
            RT = rt.Replace(Environment.NewLine, "");
            R1 = r1.Replace(Environment.NewLine, "");
            R2 = r2.Replace(Environment.NewLine, "");
            R3 = r3.Replace(Environment.NewLine, "");
            R4 = r4.Replace(Environment.NewLine, "");
            Search = searchtxt.Replace(Environment.NewLine, "");
            newprofile = newProfile.Replace(Environment.NewLine, "");
            switchTo = SwitchTo.Replace(Environment.NewLine, "");
            deleteProfile = delProfile.Replace(Environment.NewLine, "");
            pictureBox3.Location = new Point(label14.Location.X + label14.Width, pictureBox3.Location.Y);
            pictureBox4.Location = new Point(label16.Location.X + label16.Width, pictureBox4.Location.Y);
        }
        private void dummyCMS_Opening(object sender, CancelEventArgs e)
        {
            Process.Start(Application.StartupPath + "//Lang//");
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (lbLang.SelectedItems.Count > 0)
            {
                object p = "Do you want to set the language to '" + lbLang.SelectedItem.ToString();
                HaltroyFramework.HaltroyMsgBox CustomMessageBox = new HaltroyFramework.HaltroyMsgBox("Korot", p + "' ?", this.Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, "Yes", "No", "OK", "Cancel", 390, 140);
                DialogResult result = CustomMessageBox.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        LoadLangFromFile(Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang");
                        Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang";
                        if (!_Incognito) { Properties.Settings.Default.Save(); }
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(" [KOROT] Error at applying a language file : " + ex.ToString());
                    }

                }
            }
        }
        public void LoadLangFromFile(string LangFile)
        {
            try
            {
                languagedummy.Items.Clear();
                string Playlist = FileSystem2.ReadFile(LangFile, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                int Count = SplittedFase.Length - 1; ; int i = 0;
                while (!(i == Count))
                {
                    languagedummy.Items.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                    i += 1;
                }
                ReadLangFileFromTemp();
            }
            catch (Exception ex)
            {
                Output.WriteLine(" [KOROT] Error at applying a language file : " + ex.ToString());
            }
        }
        public void ReadLangFileFromTemp()
        {
            try
            {
                SetLanguage(
                    languagedummy.Items[0].ToString().Substring(1),
                    languagedummy.Items[1].ToString().Substring(1),
                    languagedummy.Items[2].ToString().Substring(1),
                    languagedummy.Items[3].ToString().Substring(1),
                    languagedummy.Items[4].ToString().Substring(1),
                    languagedummy.Items[5].ToString().Substring(1),
                    languagedummy.Items[6].ToString().Substring(1),
                    languagedummy.Items[7].ToString().Substring(1),
                    languagedummy.Items[8].ToString().Substring(1),
                    languagedummy.Items[9].ToString().Substring(1),
                    languagedummy.Items[10].ToString().Substring(1),
                    languagedummy.Items[11].ToString().Substring(1),
                    languagedummy.Items[12].ToString().Substring(1),
                    languagedummy.Items[13].ToString().Substring(1),
                    languagedummy.Items[14].ToString().Substring(1),
                    languagedummy.Items[15].ToString().Substring(1),
                    languagedummy.Items[16].ToString().Substring(1),
                    languagedummy.Items[17].ToString().Substring(1),
                    languagedummy.Items[18].ToString().Substring(1),
                    languagedummy.Items[19].ToString().Substring(1),
                    languagedummy.Items[20].ToString().Substring(1),
                    languagedummy.Items[21].ToString().Substring(1),
                    languagedummy.Items[22].ToString().Substring(1),
                    languagedummy.Items[23].ToString().Substring(1),
                    languagedummy.Items[24].ToString().Substring(1),
                    languagedummy.Items[25].ToString().Substring(1),
                    languagedummy.Items[26].ToString().Substring(1),
                    languagedummy.Items[27].ToString().Substring(1),
                    languagedummy.Items[28].ToString().Substring(1),
                    languagedummy.Items[29].ToString().Substring(1),
                    languagedummy.Items[30].ToString().Substring(1),
                    languagedummy.Items[31].ToString().Substring(1),
                    languagedummy.Items[32].ToString().Substring(1),
                    languagedummy.Items[33].ToString().Substring(1),
                    languagedummy.Items[34].ToString().Substring(1),
                    languagedummy.Items[35].ToString().Substring(1),
                    languagedummy.Items[36].ToString().Substring(1),
                    languagedummy.Items[37].ToString().Substring(1),
                    languagedummy.Items[38].ToString().Substring(1),
                    languagedummy.Items[39].ToString().Substring(1),
                    languagedummy.Items[40].ToString().Substring(1),
                    languagedummy.Items[41].ToString().Substring(1),
                    languagedummy.Items[42].ToString().Substring(1),
                    languagedummy.Items[43].ToString().Substring(1),
                    languagedummy.Items[44].ToString().Substring(1),
                    languagedummy.Items[45].ToString().Substring(1),
                    languagedummy.Items[46].ToString().Substring(1),
                    languagedummy.Items[47].ToString().Substring(1),
                    languagedummy.Items[48].ToString().Substring(1),
                    languagedummy.Items[49].ToString().Substring(1),
                    languagedummy.Items[50].ToString().Substring(1),
                    languagedummy.Items[51].ToString().Substring(1),
                    languagedummy.Items[52].ToString().Substring(1),
                    languagedummy.Items[53].ToString().Substring(1),
                    languagedummy.Items[54].ToString().Substring(1),
                    languagedummy.Items[55].ToString().Substring(1),
                    languagedummy.Items[56].ToString().Substring(1),
                    languagedummy.Items[57].ToString().Substring(1),
                    languagedummy.Items[58].ToString().Substring(1),
                    languagedummy.Items[59].ToString().Substring(1),
                    languagedummy.Items[60].ToString().Substring(1),
                    languagedummy.Items[61].ToString().Substring(1),
                    languagedummy.Items[62].ToString().Substring(1),
                    languagedummy.Items[63].ToString().Substring(1),
                    languagedummy.Items[64].ToString().Substring(1),
                    languagedummy.Items[65].ToString().Substring(1),
                    languagedummy.Items[66].ToString().Substring(1),
                    languagedummy.Items[67].ToString().Substring(1),
                    languagedummy.Items[68].ToString().Substring(1),
                    languagedummy.Items[69].ToString().Substring(1),
                    languagedummy.Items[70].ToString().Substring(1),
                    languagedummy.Items[71].ToString().Substring(1),
                    languagedummy.Items[72].ToString().Substring(1),
                    languagedummy.Items[73].ToString().Substring(1),
                    languagedummy.Items[74].ToString().Substring(1),
                    languagedummy.Items[75].ToString().Substring(1),
                    languagedummy.Items[76].ToString().Substring(1),
                    languagedummy.Items[77].ToString().Substring(1),
                    languagedummy.Items[78].ToString().Substring(1),
                    languagedummy.Items[79].ToString().Substring(1),
                    languagedummy.Items[80].ToString().Substring(1),
                    languagedummy.Items[81].ToString().Substring(1),
                    languagedummy.Items[82].ToString().Substring(1),
                    languagedummy.Items[83].ToString().Substring(1),
                    languagedummy.Items[84].ToString().Substring(1),
                    languagedummy.Items[85].ToString().Substring(1),
                    languagedummy.Items[86].ToString().Substring(1),
                    languagedummy.Items[87].ToString().Substring(1),
                    languagedummy.Items[88].ToString().Substring(1),
                    languagedummy.Items[89].ToString().Substring(1),
                    languagedummy.Items[90].ToString().Substring(1),
                    languagedummy.Items[91].ToString().Substring(1),
                    languagedummy.Items[92].ToString().Substring(1),
                    languagedummy.Items[93].ToString().Substring(1),
                    languagedummy.Items[94].ToString().Substring(1),
                    languagedummy.Items[95].ToString().Substring(1),
                    languagedummy.Items[96].ToString().Substring(1),
                    languagedummy.Items[97].ToString().Substring(1),
                    languagedummy.Items[98].ToString().Substring(1),
                    languagedummy.Items[99].ToString().Substring(1),
                    languagedummy.Items[100].ToString().Substring(1),
                    languagedummy.Items[101].ToString().Substring(1),
                    languagedummy.Items[102].ToString().Substring(1),
                    languagedummy.Items[103].ToString().Substring(1),
                    languagedummy.Items[104].ToString().Substring(1),
                    languagedummy.Items[105].ToString().Substring(1),
                    languagedummy.Items[106].ToString().Substring(1),
                    languagedummy.Items[107].ToString().Substring(1),
                    languagedummy.Items[108].ToString().Substring(1),
                    languagedummy.Items[109].ToString().Substring(1),
                    languagedummy.Items[110].ToString().Substring(1),
                    languagedummy.Items[130].ToString().Substring(1),
                    languagedummy.Items[133].ToString().Substring(1),
                    languagedummy.Items[134].ToString().Substring(1),
                    languagedummy.Items[135].ToString().Substring(1),
                    languagedummy.Items[136].ToString().Substring(1),
                    languagedummy.Items[137].ToString().Substring(1),
                    languagedummy.Items[138].ToString().Substring(1),
                    languagedummy.Items[139].ToString().Substring(1),
                    languagedummy.Items[140].ToString().Substring(1),
                    languagedummy.Items[159].ToString().Substring(1),
                    languagedummy.Items[160].ToString().Substring(1),
                    languagedummy.Items[161].ToString().Substring(1),
                    languagedummy.Items[162].ToString().Substring(1),
                    languagedummy.Items[163].ToString().Substring(1),
                    languagedummy.Items[164].ToString().Substring(1),
                    languagedummy.Items[165].ToString().Substring(1),
                    languagedummy.Items[166].ToString().Substring(1),
                    languagedummy.Items[167].ToString().Substring(1),
                    languagedummy.Items[168].ToString().Substring(1),
                    languagedummy.Items[169].ToString().Substring(1),
                    languagedummy.Items[170].ToString().Substring(1),
                    languagedummy.Items[171].ToString().Substring(1),
                    languagedummy.Items[172].ToString().Substring(1),
                    languagedummy.Items[173].ToString().Substring(1),
                    languagedummy.Items[174].ToString().Substring(1),
                    languagedummy.Items[175].ToString().Substring(1),
                    languagedummy.Items[176].ToString().Substring(1),
                    languagedummy.Items[177].ToString().Substring(1),
                    languagedummy.Items[178].ToString().Substring(1),
                    languagedummy.Items[179].ToString().Substring(1),
                    languagedummy.Items[182].ToString().Substring(1),
                    languagedummy.Items[183].ToString().Substring(1),
                    languagedummy.Items[184].ToString().Substring(1),
                    languagedummy.Items[185].ToString().Substring(1),
                    languagedummy.Items[186].ToString().Substring(1),
                    languagedummy.Items[187].ToString().Substring(1),
                    languagedummy.Items[188].ToString().Substring(1),
                    languagedummy.Items[189].ToString().Substring(1),
                    languagedummy.Items[190].ToString().Substring(1),
                    languagedummy.Items[191].ToString().Substring(1),
                    languagedummy.Items[193].ToString().Substring(1),
                    languagedummy.Items[194].ToString().Substring(1),
                    languagedummy.Items[195].ToString().Substring(1),
                    languagedummy.Items[196].ToString().Substring(1));
            }
            catch (Exception ex)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle, "This file does not suitable for this version of Korot.Please ask the creator of this language to update." + Environment.NewLine + " Error : " + ex.Message, this.Icon, MessageBoxButtons.OK, Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(" [KOROT] Error at applying a language file : " + ex.ToString());
            }

        }
        private void OnIsBrowserInitializedChanged(object sender, EventArgs e)
        {
            //Get the underlying browser host wrapper
            var browserHost = chromiumWebBrowser1.GetBrowser().GetHost();
            var requestContext = browserHost.RequestContext;
            string errorMessage;
            // Browser must be initialized before getting/setting preferences
            if (Properties.Settings.Default.DoNotTrack)
            {
                var success = requestContext.SetPreference("enable_do_not_track", true, out errorMessage);
                if (!success)
                {
                    this.Invoke(new Action(() => Output.WriteLine(" [KOROT] Unable to set preference enable_do_not_track errorMessage: " + errorMessage)));
                }
            }
        }
        public void RefreshLangList()
        {
            int savedValue = lbLang.SelectedIndex;
            lbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.lang", SearchOption.TopDirectoryOnly))
            {
                lbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }
            try { lbLang.SelectedIndex = savedValue; }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.FrmCEF.RefreshLangList] Error: " + ex.ToString());
                }
            }

        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputb = new HaltroyFramework.HaltroyInputBox(customSearchNote, customSearchMessage, this.Icon, Properties.Settings.Default.SearchURL, Properties.Settings.Default.BackColor, Properties.Settings.Default.OverlayColor, anaform.OK, anaform.Cancel, 400, 150);
            DialogResult diagres = inputb.ShowDialog();
            if (diagres == DialogResult.OK)
            {
                if (ValidHttpURL(inputb.TextValue()) && !inputb.TextValue().StartsWith("korot://") && !inputb.TextValue().StartsWith("file://") && !inputb.TextValue().StartsWith("about"))
                {
                    Properties.Settings.Default.SearchURL = inputb.TextValue();
                    textBox3.Text = Properties.Settings.Default.SearchURL;
                }
                else
                {
                    customToolStripMenuItem_Click(null, null);
                }
            }
        }
        private void SearchEngineSelection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SearchURL = ((ToolStripMenuItem)sender).Tag.ToString();
            textBox3.Text = Properties.Settings.Default.SearchURL;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.ToLower().StartsWith("korot://newtab"))
            {
                radioButton1.Checked = true;
                Properties.Settings.Default.Homepage = textBox2.Text;
                if (!_Incognito) { Properties.Settings.Default.Save(); }
            }
            else
            {
                radioButton1.Checked = false;
                Properties.Settings.Default.Homepage = textBox2.Text;
                if (!_Incognito) { Properties.Settings.Default.Save(); }
            }
        }
        private void textBox3_Click(object sender, EventArgs e)
        {
            cmsSearchEngine.Show(MousePosition);
        }
        private void openmytaginnewtab(object sender, LinkLabelLinkClickedEventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab(((LinkLabel)sender).Tag.ToString())));
        }
        private void ClearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Favorites = "";
            if (!_Incognito) { Properties.Settings.Default.Save(); }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog();
            colorpicker.AnyColor = true;
            colorpicker.AllowFullOpen = true;
            colorpicker.FullOpen = true;
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.BackColor = colorpicker.Color;
                Properties.Settings.Default.BackColor = colorpicker.Color;
                pictureBox3.BackColor = colorpicker.Color;
                Properties.Settings.Default.ThemeAuthor = "";
                Properties.Settings.Default.ThemeName = "";
                ChangeTheme();
                comboBox1.Text = "";
            }

        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog();
            colorpicker.AnyColor = true;
            colorpicker.AllowFullOpen = true;
            colorpicker.FullOpen = true;
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pictureBox4.BackColor = colorpicker.Color;
                Properties.Settings.Default.OverlayColor = colorpicker.Color;
                pictureBox4.BackColor = colorpicker.Color;
                Properties.Settings.Default.ThemeAuthor = "";
                Properties.Settings.Default.ThemeName = "";
                ChangeTheme();
                comboBox1.Text = "";
            }
        }

        private void Label14_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Process.Start(Application.StartupPath + "//Themes//");
            }

        }

        private void Label7_Click_1(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.White;
            Properties.Settings.Default.BackColor = pictureBox3.BackColor;
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            ChangeTheme();
        }


        private void Label8_Click(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.DodgerBlue;
            Properties.Settings.Default.OverlayColor = pictureBox4.BackColor;
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            ChangeTheme();
        }

        public void RefreshDownloadList()
        {
            int selectedValue = hlvDownload.SelectedIndices.Count > 0 ? hlvDownload.SelectedIndices[0] : 0;
            hlvDownload.Items.Clear();
            try
            {
                foreach (DownloadItem item in anaform.CurrentDownloads)
                {
                    ListViewItem listV = new ListViewItem();
                    listV.Text = item.PercentComplete + "%";
                    listV.SubItems.Add(DateTime.Now.ToString("dd/MM/yy hh:mm:ss"));
                    listV.SubItems.Add(item.FullPath);
                    listV.SubItems.Add(item.Url);
                    hlvDownload.Items.Add(listV);
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.frmCEF.RefreshDownloadList] (Current Downloads) Error: " + ex.ToString());
                }
            }
            string Playlist = Properties.Settings.Default.DowloadHistory;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                try
                {
                    ListViewItem listV = new ListViewItem();
                    listV.Text = SplittedFase[i].Replace(Environment.NewLine, "");
                    i += 1;
                    listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                    i += 1;
                    listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                    i += 1;
                    listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                    i += 1;
                    hlvDownload.Items.Add(listV);
                }
                catch (Exception ex) { if (Properties.Settings.Default.debugLogExceptions) { Output.WriteLine(" [Korot.frmCEF.RefreshDownloadList] (Finished Downloads) Error: " + ex.ToString()); } if (Properties.Settings.Default.debugForceContinue) { continue; } else { i = Count; } }

            }
            try
            {
                hlvDownload.SelectedIndices.Clear();
                if (selectedValue < (hlvDownload.Items.Count - 1))
                {
                    hlvDownload.Items[selectedValue].Selected = true;
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.frmCEF.RefreshDownloadList] Error: " + ex.ToString());
                }
            }

        }
        private void ListView2_DoubleClick(object sender, EventArgs e)
        {
            cmsDownload.Show(MousePosition);
        }
        private void OpenLinkİnNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab(hlvDownload.SelectedItems[0].SubItems[2].Text)));
        }

        private void OpenFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(hlvDownload.SelectedItems[0].SubItems[3].Text);
        }

        private void OpenFileİnExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + hlvDownload.SelectedItems[0].SubItems[3].Text);
        }

        private void RemoveSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (hlvDownload.SelectedItems.Count > 0)
            {
                if (hlvDownload.SelectedItems[0].Text == "X" || hlvDownload.SelectedItems[0].Text == "✓")
                {
                    string x = hlvDownload.SelectedItems[0].Text + ";" + hlvDownload.SelectedItems[0].SubItems[1].Text + ";" + hlvDownload.SelectedItems[0].SubItems[2].Text + ";" + hlvDownload.SelectedItems[0].SubItems[3].Text + ";";
                    Properties.Settings.Default.DowloadHistory = Properties.Settings.Default.DowloadHistory.Replace(x, "");
                }
                else { anaform.CancelledDownloads.Add(hlvDownload.SelectedItems[0].SubItems[3].Text); }
                if (!_Incognito) { Properties.Settings.Default.Save(); }
                RefreshDownloadList();
            }
        }

        private void ClearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DowloadHistory = "";
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            RefreshDownloadList();
        }

        private void HlvHistory_DoubleClick(object sender, EventArgs e)
        {
            if (hlvHistory.SelectedItems.Count > 0)
            {
                anaform.Invoke(new Action(() => anaform.CreateTab(hlvHistory.SelectedItems[0].SubItems[2].Text)));
            }
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void NewIncognitoWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "-incognito");
        }

        private void ColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackStyle = "BACKCOLOR";
            textBox4.Text = usingBC;
            colorToolStripMenuItem.Checked = true;
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about\:\/\/)|(?:about\:\/\/)|(?:file\:\/\/)|(?:https\:\/\/)|(?:korot\:\/\/)|(?:http:\/\/)|(?:\:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex Rgx2 = new Regex(@"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx2.IsMatch(s) || Rgx.IsMatch(s);
        }
        private void FromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputbox = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                            enterAValidCode,
                                                                                            this.Icon,
                                                                                            "",
                                                                                            Properties.Settings.Default.BackColor,
                                                                                            Properties.Settings.Default.OverlayColor,
                                                                                            anaform.OK, anaform.Cancel, 400, 150);
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackStyle = inputbox.TextValue() + ";";
                textBox4.Text = Properties.Settings.Default.BackStyle;
                colorToolStripMenuItem.Checked = false;
            }
        }

        private void FromLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog();
            filedlg.Filter = imageFiles + "|*.jpg;*.png;*.bmp;*.jpeg;*.jfif;*.gif;*.apng;*.ico;*.svg;*.webp|" + allFiles + "|*.*";
            filedlg.Title = selectBackImage;
            filedlg.Multiselect = false;
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                int fileSize = Convert.ToInt32(new FileInfo(filedlg.FileName).Length);
                if (fileSize <= 131072)
                {
                    string imageType = Path.GetExtension(filedlg.FileName).Replace(".", "");
                    Properties.Settings.Default.BackStyle = "background-image: url('data:image/" + imageType + ";base64," + FileSystem2.ImageToBase64(Image.FromFile(filedlg.FileName)) + "');";
                    textBox4.Text = Properties.Settings.Default.BackStyle;
                    colorToolStripMenuItem.Checked = false;
                }
                else
                {
                    FromLocalFileToolStripMenuItem_Click(sender, e);
                }
            }
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Properties.Settings.Default.Homepage = "korot://newtab";
                textBox2.Text = Properties.Settings.Default.Homepage;
            }
        }

        private void tmrRefresher_Tick(object sender, EventArgs e)
        {
            hsDoNotTrack.Checked = Properties.Settings.Default.DoNotTrack;
            textBox2.Text = Properties.Settings.Default.Homepage;
            radioButton1.Checked = Properties.Settings.Default.Homepage == "korot://newtab";
            textBox3.Text = Properties.Settings.Default.SearchURL;
            hsProxy.Checked = Properties.Settings.Default.rememberLastProxy;
            RefreshLangList();
            refreshThemeList();
            RefreshDownloadList();
            RefreshHistory();
        }

        private void label15_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                refreshThemeList();
            }
            else if (e.Button == MouseButtons.Right)
            {
                Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + "\"");
            }
        }


        private void btInstall_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath, "-update");
        }

        private void btUpdater_Click(object sender, EventArgs e)
        {
            if (UpdateWebC.IsBusy) { UpdateWebC.CancelAsync(); }
            UpdateWebC.DownloadStringAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2123&authkey=!ADjFaqhHH3MjOAQ&ithint=file%2ctxt&e=5QH8I8"));
            updateProgress = 0;
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshHistory();
            RefreshDownloadList();
        }
        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string x = hlvHistory.SelectedItems[0].SubItems[0].Text + ";";
            string y = hlvHistory.SelectedItems[0].SubItems[1].Text + ";";
            string t = hlvHistory.SelectedItems[0].SubItems[2].Text + ";";
            Properties.Settings.Default.History = Properties.Settings.Default.History.Replace(x + y + t, "");
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            RefreshHistory();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.History = "";
            if (!_Incognito) { Properties.Settings.Default.Save(); }
            RefreshHistory();
        }



        private void Label2_Click(object sender, EventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab("https://haltroy.com/Korot.html")));
        }


        private void LbLang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView1_DoubleClick(null, null);
        }

        public void LoadTheme(string themeFile)
        {
            try
            {
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string Playlist3 = FileSystem2.ReadFile(themeFile, Encoding.UTF8);
                string[] SplittedFase3 = Playlist3.Split(token);
                int backR = Convert.ToInt32(SplittedFase3[0].Replace(Environment.NewLine, ""));
                int backG = Convert.ToInt32(SplittedFase3[1].Replace(Environment.NewLine, ""));
                int backB = Convert.ToInt32(SplittedFase3[2].Replace(Environment.NewLine, ""));
                int ovR = Convert.ToInt32(SplittedFase3[3].Replace(Environment.NewLine, ""));
                int ovG = Convert.ToInt32(SplittedFase3[4].Replace(Environment.NewLine, ""));
                int ovB = Convert.ToInt32(SplittedFase3[5].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.BackColor = Color.FromArgb(255, backR, backG, backB);
                Properties.Settings.Default.OverlayColor = Color.FromArgb(255, ovR, ovG, ovB);
                if (SplittedFase3[6].Substring(1).Replace(Environment.NewLine, "").Length > 131072) { throw new OverflowException("Theme includes a Background Style bigger than 128KiB."); }
                Properties.Settings.Default.BackStyle = SplittedFase3[6].Substring(1).Replace(Environment.NewLine, "");
                Properties.Settings.Default.BStyleLayout = Convert.ToInt32(SplittedFase3[7].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.ThemeName = SplittedFase3[8].Substring(1).Replace(Environment.NewLine, "");
                Properties.Settings.Default.ThemeAuthor = SplittedFase3[9].Substring(1).Replace(Environment.NewLine, "");
                pictureBox3.BackColor = Properties.Settings.Default.BackColor;
                pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
                Properties.Settings.Default.ThemeFile = themeFile;
                textBox4.Text = Properties.Settings.Default.BackStyle == "BACKCOLOR" ? usingBC : Properties.Settings.Default.BackStyle;
                label21.Text = aboutInfo.Replace("[NEWLINE]", Environment.NewLine).Replace("[THEMEAUTHOR]", Properties.Settings.Default.ThemeAuthor).Replace("[THEMENAME]", Properties.Settings.Default.ThemeName);
            }
            catch (Exception ex)
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot"))
                {

                }
                else
                {
                    if (themeFile == Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf")
                    {
                        string newTheme = "255" + Environment.NewLine +
    "255" + Environment.NewLine +
    "255" + Environment.NewLine +
    "30" + Environment.NewLine +
    "144" + Environment.NewLine +
    "255" + Environment.NewLine +
    "BACKCOLOR" + Environment.NewLine +
    "0" + Environment.NewLine +
    "Korot Light" + Environment.NewLine +
    "Haltroy";
                        FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf", newTheme, Encoding.UTF8);

                    }
                }
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle,
                                                                                          ErrorTheme,
                                                                                          this.Icon,
                                                                                          MessageBoxButtons.OK,
                                                                                          Properties.Settings.Default.BackColor,
                                                                                          anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);

                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(" [KOROT] Error at applying a theme : " + ex.ToString());
                LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf");

            }
        }
        public void refreshThemeList()
        {
            int savedValue = listBox2.SelectedIndex;
            try
            {

                listBox2.Items.Clear();
                foreach (String x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"))
                {
                    if (x.EndsWith(".ktf", StringComparison.OrdinalIgnoreCase))
                    {
                        listBox2.Items.Add(new FileInfo(x).Name);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.frmCEF.RefreshThemeList] (Reload) Error: " + ex.ToString());
                }
            }
            try
            {
                listBox2.SelectedIndex = savedValue;
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.frmCEF.RefreshThemeList] Error: " + ex.ToString());
                }
            }
        }


        private void Button12_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter objWriter3;
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            objWriter3 = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf");
            string x = Properties.Settings.Default.BackStyle;
            string lol = Properties.Settings.Default.BackColor.R + Environment.NewLine + Properties.Settings.Default.BackColor.G + Environment.NewLine + Properties.Settings.Default.BackColor.B + Environment.NewLine + Properties.Settings.Default.OverlayColor.R + Environment.NewLine + Properties.Settings.Default.OverlayColor.G + Environment.NewLine + Properties.Settings.Default.OverlayColor.B + Environment.NewLine + x + Environment.NewLine + Properties.Settings.Default.BStyleLayout + Environment.NewLine + comboBox1.Text + Environment.NewLine + Environment.UserName + Environment.NewLine;
            objWriter3.WriteLine(lol);
            objWriter3.Close();
            Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf";
            refreshThemeList();
        }

        WebClient UpdateWebC = new WebClient();
        public void Updater()
        {
            UpdateWebC.DownloadStringCompleted += Updater_DownloadStringCompleted;
            UpdateWebC.DownloadProgressChanged += updater_checking;
            UpdateWebC.DownloadStringAsync(new Uri("https://onedrive.live.com/download?resid=3FD0899CA240B9B!2123&authkey=!ADjFaqhHH3MjOAQ&ithint=file%2ctxt&e=5QH8I8"));
            updateProgress = 0;
        }
        bool alreadyCheckedForUpdatesOnce = false;
        private void updater_checking(object sender, DownloadProgressChangedEventArgs e)
        {
            lbUpdateStatus.Text = checking;
            updateProgress = 0;
            btUpdater.Visible = false;
        }
        private void Updater_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                updateProgress = 3;
                btInstall.Visible = false;
                btUpdater.Visible = true;
                lbUpdateStatus.Text = updateError;
            }
            else
            {
                UpdateResult(e.Result);
            }
        }
        void UpdateResult(String info)
        {
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase3 = info.Split(token);
            Version newest = new Version(SplittedFase3[0].Replace(Environment.NewLine, ""));
            Version current = new Version(Application.ProductVersion);
            if (newest > current)
            {
                if (alreadyCheckedForUpdatesOnce)
                {
                    updateProgress = 2;
                    lbUpdateStatus.Text = updateavailable;
                    btUpdater.Visible = true;
                    btInstall.Visible = true;
                }
                else
                {
                    alreadyCheckedForUpdatesOnce = true;
                    updateProgress = 2;
                    lbUpdateStatus.Text = updateavailable;
                    btInstall.Visible = true;
                    btUpdater.Visible = true;
                    HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(updateTitle, updateMessage, this.Icon, MessageBoxButtons.YesNo, Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
                    DialogResult diagres = mesaj.ShowDialog();
                    if (diagres == DialogResult.Yes)
                    {
                        if (Application.OpenForms.OfType<Form1>().Count() < 1)
                        {
                            Process.Start(Application.ExecutablePath, "-update");
                        }
                        else
                        {
                            foreach (Form1 x in Application.OpenForms)
                            {
                                x.Focus();
                            }
                        }
                    }
                }
            }
            else
            {
                btUpdater.Visible = true;
                btInstall.Visible = false;
                updateProgress = 1;
                lbUpdateStatus.Text = uptodate;
            }
        }
        Font CreateFont(string fontFile, float size, FontStyle style)
        {
            using (var pfc = new PrivateFontCollection())
            {
                pfc.AddFontFile(fontFile);
                using (var fontFamily = new FontFamily(pfc.Families[0].Name, pfc))
                {
                    return new Font(fontFamily, size, style);
                }
            }
        }
        public TitleBarTabs ParentTabs => (ParentForm as TitleBarTabs);
        async private void SetProxy(ChromiumWebBrowser cwb, string Address)
        {
            if (Address == null) { }
            else
            {
                await Cef.UIThreadTaskFactory.StartNew(delegate
                {
                    var rc = cwb.GetBrowser().GetHost().RequestContext;
                    var v = new Dictionary<string, object>();
                    v["mode"] = "fixed_servers";
                    v["server"] = Address;
                    string error;
                    bool success = rc.SetPreference("proxy", v, out error);
                });
            }
        }
        private static ManagementObject GetMngObj(string className)
        {
            var wmi = new ManagementClass(className);

            foreach (var o in wmi.GetInstances())
            {
                var mo = (ManagementObject)o;
                if (mo != null)
                {
                    return mo;
                }
            }

            return null;
        }

        public static string GetOsVer()
        {
            try
            {
                ManagementObject mo = GetMngObj("Win32_OperatingSystem");

                if (null == mo)
                {
                    return string.Empty;
                }

                return mo["Version"] as string;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static int Brightness(System.Drawing.Color c)
        {
            return (int)Math.Sqrt(
               c.R * c.R * .241 +
               c.G * c.G * .691 +
               c.B * c.B * .068);
        }
        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.UserAgent = "Mozilla/5.0 ( Windows NT "
                + GetOsVer()
                + "; "
                + (Environment.Is64BitProcess ? "WOW64" : "Win32NT")
                + ") AppleWebKit/537.36 (KHTML, like Gecko) Chrome/"
                + Cef.ChromiumVersion
                + " Safari/537.36 Korot/"
                + Application.ProductVersion.ToString();
            if (_Incognito) { settings.CachePath = null; settings.PersistSessionCookies = false; settings.RootCachePath = null; }
            else { settings.CachePath = userCache; settings.RootCachePath = userCache; }
            settings.RegisterScheme(new CefCustomScheme
            {
                SchemeName = "korot",
                SchemeHandlerFactory = new SchemeHandlerFactory(anaform, this)
            });
            // Initialize cef with the provided settings
            if (Cef.IsInitialized == false) { Cef.Initialize(settings); }
            chromiumWebBrowser1 = new ChromiumWebBrowser(loaduri);
            panel1.Controls.Add(chromiumWebBrowser1);
            chromiumWebBrowser1.FindHandler = new FindHandler(this);
            chromiumWebBrowser1.KeyboardHandler = new KeyboardHandler(this, anaform);
            chromiumWebBrowser1.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            chromiumWebBrowser1.RequestHandler = new RequestHandlerKorot(anaform, this);
            chromiumWebBrowser1.DisplayHandler = new DisplayHandler(this, anaform);
            chromiumWebBrowser1.LoadingStateChanged += loadingstatechanged;
            chromiumWebBrowser1.TitleChanged += cef_TitleChanged;
            chromiumWebBrowser1.AddressChanged += cef_AddressChanged;
            chromiumWebBrowser1.LoadError += cef_onLoadError;
            chromiumWebBrowser1.KeyDown += tabform_KeyDown;
            chromiumWebBrowser1.MenuHandler = new ContextMenuHandler(this, anaform);
            chromiumWebBrowser1.LifeSpanHandler = new BrowserLifeSpanHandler(this);
            chromiumWebBrowser1.DownloadHandler = new DownloadHandler(this, anaform);
            chromiumWebBrowser1.JsDialogHandler = new JsHandler(this, anaform);
            chromiumWebBrowser1.DialogHandler = new MyDialogHandler();
            chromiumWebBrowser1.MouseWheel += MouseScroll;
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
            if (defaultProxy != null && Properties.Settings.Default.rememberLastProxy && !string.IsNullOrWhiteSpace(Properties.Settings.Default.LastProxy))
            {
                SetProxy(chromiumWebBrowser1, Properties.Settings.Default.LastProxy);
            }
        }
        public void executeStartupExtensions()
        {
            foreach (String x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\", "*.*", SearchOption.AllDirectories))
            {
                if (x.EndsWith("\\ext.kem", StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {

                        string Playlist = FileSystem2.ReadFile(x, Encoding.UTF8);
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(0, 1) == "1" && (new FileInfo(x).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\")).Length < 5242880))
                        {
                            chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\"), Encoding.UTF8));
                        }
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine("[Korot] Script Execute Error : { KEM: " + x + " , ErrorMessage: " + ex.Message + " } ");
                        continue;
                    }
                }
            }
        }
        public bool certError = false;
        public bool cookieUsage = false;
        public void ChangeStatus(string status)
        {
            label2.Text = status;
        }

        public void loadingstatechanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading)
            {
                certError = false;
                cookieUsage = false;
                pictureBox2.Invoke(new Action(() => pictureBox2.Image = Properties.Resources.lockg));
                this.Invoke(new Action(() => showCertificateErrorsToolStripMenuItem.Tag = null));
                this.Invoke(new Action(() => showCertificateErrorsToolStripMenuItem.Visible = false));
                this.Invoke(new Action(() => safeStatusToolStripMenuItem.Text = this.CertificateOKTitle));
                this.Invoke(new Action(() => ınfoToolStripMenuItem.Text = this.CertificateOK));
                this.Invoke(new Action(() => cookieInfoToolStripMenuItem.Text = this.notUsesCookies));
                if (Brightness(Properties.Settings.Default.BackColor) > 130)
                {
                    button2.Image = Korot.Properties.Resources.cancel;
                }
                else { button2.Image = Korot.Properties.Resources.cancel_w; }
            }
            else
            {
                if (_Incognito) { }
                else
                {
                    this.Invoke(new Action(() => Korot.Properties.Settings.Default.History += DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + this.Text + ";" + (tbAddress.Text) + ";"));

                }
                executeStartupExtensions();
                if (Brightness(Properties.Settings.Default.BackColor) > 130)
                {
                    button2.Image = Korot.Properties.Resources.refresh;
                }
                else
                { button2.Image = Korot.Properties.Resources.refresh_w; }
            }
            if (onCEFTab)
            {
                try
                {
                    button1.Invoke(new Action(() => button1.Enabled = e.CanGoBack));
                    button3.Invoke(new Action(() => button3.Enabled = e.CanGoForward));
                }
                catch
                {
                    try
                    {
                        button1.Invoke(new Action(() => button1.Enabled = false));
                        button3.Invoke(new Action(() => button3.Enabled = false));
                    }
                    catch (Exception ex)
                    {
                        if (Properties.Settings.Default.debugLogExceptions)
                        {
                            Output.WriteLine(" [Korot.frmCEF.LoadingStateChanged.Catch] Error: " + ex.ToString());
                        }
                    }
                }
            }
            isLoading = e.IsLoading;
            if (Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser1.Address))
            {
                disallowThisPageForCookieAccessToolStripMenuItem.Text = allowCookie;
            }
            else
            {
                disallowThisPageForCookieAccessToolStripMenuItem.Text = disallowCookie;
            }
        }

        public void NewTab(string url)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab(url)));
        }
        ToolStripMenuItem selectedFavorite = null;
        public void RefreshFavorites()
        {
            mFavorites.Items.Clear();
            try
            {
                string Playlist = Properties.Settings.Default.Favorites;
                string[] SplittedFase = Playlist.Split(';');
                int Count = SplittedFase.Length - 1; ; int i = 0;
                while (!(i == Count))
                {
                    ToolStripMenuItem miFavorite = new ToolStripMenuItem();
                    miFavorite.Tag = SplittedFase[i].Replace(Environment.NewLine, "");
                    i += 1;
                    miFavorite.Text = SplittedFase[i].Replace(Environment.NewLine, "");
                    i += 1;
                    miFavorite.Click += new EventHandler((sender, e) => { selectedFavorite = miFavorite; cmsFavorite.Show(MousePosition.X, MousePosition.Y - 5); });
                    miFavorite.DoubleClick += TestToolStripMenuItem_Click;
                    mFavorites.Items.Add(miFavorite);
                }
            }
            catch (Exception ex)
            {
                Output.WriteLine(" [KOROT] Error while refreshing favorites : " + ex.ToString());
            }
        }
        private void tabform_Load(object sender, EventArgs e)
        {
            Updater();
            try
            {
                comboBox1.Text = new FileInfo(Properties.Settings.Default.ThemeFile).Name.Replace(".ktf", "");
            }
            catch
            {
                comboBox1.Text = "";
                Properties.Settings.Default.ThemeAuthor = "";
                Properties.Settings.Default.ThemeName = "";
            }
            textBox2.Text = Properties.Settings.Default.Homepage;
            textBox3.Text = Properties.Settings.Default.SearchURL;
            if (Properties.Settings.Default.Homepage == "korot://newtab") { radioButton1.Enabled = true; }
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshLangList();

            refreshThemeList();
            ChangeTheme();
            RefreshDownloadList();
            label18.Text = Application.ProductVersion.ToString();
            label17.Visible = Environment.Is64BitProcess;
            RefreshFavorites();
            LoadExt();
            RefreshProfiles();
            profilenameToolStripMenuItem.Text = userName;
            //caseSensitiveToolStripMenuItem.Text = CaseSensitive;
            showCertificateErrorsToolStripMenuItem.Text = showCertError;
            chromiumWebBrowser1.Select();
            hsDoNotTrack.Checked = Properties.Settings.Default.DoNotTrack;
            RefreshTranslation();
            if (_Incognito)
            {
                foreach (Control x in tabPage3.Controls)
                {
                    if (x != button13) { x.Enabled = false; }
                }
                btInstall.Enabled = false;
                btUpdater.Enabled = false;
                button9.Enabled = false;
                contextMenuStrip3.Enabled = false;
                cmsSearchEngine.Enabled = false;
                button7.Enabled = false;
                cmsHistory.Enabled = false;
                cmsProfiles.Enabled = false;
                removeSelectedToolStripMenuItem1.Enabled = false;
                clearToolStripMenuItem2.Enabled = false;
                removeSelectedToolStripMenuItem.Enabled = false;
                clearToolStripMenuItem.Enabled = false;
                disallowThisPageForCookieAccessToolStripMenuItem.Enabled = false;
                removeSelectedTSMI.Enabled = false;
                clearTSMI.Enabled = false;
                Properties.Settings.Default.BackColor = Color.FromArgb(255, 64, 64, 64);
                Properties.Settings.Default.OverlayColor = Color.DodgerBlue;
            }
            else
            {
                pictureBox1.Visible = false;
                tbAddress.Size = new Size(tbAddress.Size.Width + pictureBox1.Size.Width, tbAddress.Size.Height);
            }
            LoadLangFromFile(Properties.Settings.Default.LangFile);
            LoadLangFromFile(Properties.Settings.Default.LangFile);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            string urlLower = tbAddress.Text.ToLower();
            if (ValidHttpURL(urlLower))
            {
                chromiumWebBrowser1.Load(urlLower);
            }

            else
            {
                chromiumWebBrowser1.Load(Properties.Settings.Default.SearchURL + urlLower);
                button1.Enabled = true;

            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1) //CEF
            {
                chromiumWebBrowser1.Back();
            }
            else if (tabControl1.SelectedTab == tabPage2) //Certificate Error Menu
            {
                chromiumWebBrowser1.Back();
                allowSwitching = true;
                tabControl1.SelectedTab = tabPage1;
            }
            else if (tabControl1.SelectedTab == tabPage3 || tabControl1.SelectedTab == tabPage4 || tabControl1.SelectedTab == tabPage5 || tabControl1.SelectedTab == tabPage6 || tabControl1.SelectedTab == tabPage7) //Menu
            {
                allowSwitching = true;
                tabControl1.SelectedTab = tabPage1;
            }

        }

        private void button3_Click(object sender, EventArgs e) { allowSwitching = true; tabControl1.SelectedTab = tabPage1; chromiumWebBrowser1.Forward(); }

        private void button2_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            if (isLoading)
            {
                chromiumWebBrowser1.Stop();
            }
            else { chromiumWebBrowser1.Reload(); }
        }
        private void cef_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new Action(() => tbAddress.Text = e.Address));
            if (Properties.Settings.Default.Favorites.Contains(e.Address))
            {
                isLoadedPageFavroited = true;
                button7.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.star_on_w : Properties.Resources.star_on;
            }
            else
            {
                button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
                isLoadedPageFavroited = false;
            }
            if (!ValidHttpURL(e.Address))
            {
                chromiumWebBrowser1.Load(Properties.Settings.Default.SearchURL + e.Address);
            }
        }
        private void cef_onLoadError(object sender, LoadErrorEventArgs e)
        {
            if (e == null) //User Asked
            {
                chromiumWebBrowser1.Load("korot://error/?e=TEST");
            }
            else
            {
                if (e.Frame.IsMain)
                {
                    chromiumWebBrowser1.Load("korot://error/?e=" + e.ErrorText);
                }
                else
                {
                    e.Frame.LoadUrl("korot://error/?e=" + e.ErrorText);
                }
            }
        }


        private void cef_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            if (e.Title.Length < 101)
            {
                this.Invoke(new Action(() => this.Text = e.Title));
            }
            else
            {
                this.Invoke(new Action(() => this.Text = e.Title.Substring(0, 100)));
            }
            this.Parent.Invoke(new Action(() => this.Parent.Text = this.Text));
        }

        public void showHideSearchMenu()
        {
            if (cmsHamburger.Visible)
            {
                cmsHamburger.Close();
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
            }
            else
            {
                cmsHamburger.Show(button11, 0, 0);
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
            }
        }
        public void retrieveKey(int code)
        {
            if (code == 0) //BrowserBack
            {
                button1_Click(null, null);
            }
            else if (code == 1) //BrowserForward
            {
                button3_Click(null, null);
            }
            else if (code == 2) //BrowserRefresh
            {
                button2_Click(null, null);
            }
            else if (code == 3) //BrowserStop
            {
                button2_Click(null, null);
            }
            else if (code == 4) //BrowserHome
            {
                button5_Click(null, null);
            }
        }
        private void button5_Click(object sender, EventArgs e) { allowSwitching = true; tabControl1.SelectedTab = tabPage1; chromiumWebBrowser1.Load(Properties.Settings.Default.Homepage); }

        public bool isControlKeyPressed = false;
        public void tabform_KeyDown(object sender, KeyEventArgs e)
        {
            if (Properties.Settings.Default.debugLogKeys)
            {
                Output.WriteLine(" [Korot.frmCEF.KeyDown] KeyeventArgs: " + e.ToString());
            }

            isControlKeyPressed = e.Control;
            if (e.KeyData == Keys.BrowserBack)
            {
                button1_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserForward)
            {
                button3_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserRefresh)
            {
                button2_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserStop)
            {
                button2_Click(null, null);
            }
            else if (e.KeyData == Keys.BrowserHome)
            {
                button5_Click(null, null);
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed)
            {
                NewTab("korot://newtab");
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed && e.Shift)
            {
                Process.Start(Application.ExecutablePath, "-incognito");
            }
            else if (e.KeyCode == Keys.N && isControlKeyPressed && e.Alt)
            {
                Process.Start(Application.ExecutablePath);
            }
            else if (e.KeyCode == Keys.F && isControlKeyPressed)
            {
                showHideSearchMenu();
            }
            else if (e.KeyData == Keys.Enter)
            {
                button4_Click(null, null);
            }
            else if ((e.KeyData == Keys.Up || e.KeyData == Keys.PageUp) && isControlKeyPressed)
            {
                zoomInToolStripMenuItem_Click(sender, null);
            }
            else if ((e.KeyData == Keys.Down || e.KeyData == Keys.PageDown) && isControlKeyPressed)
            {
                zoomOutToolStripMenuItem_Click(sender, null);
            }
            else if (e.KeyData == Keys.PrintScreen && isControlKeyPressed)
            {
                takeScreenShot();
            }
            else if (e.KeyData == Keys.S && isControlKeyPressed)
            {
                savePage();
            }
            else if (isControlKeyPressed && e.Shift && e.KeyData == Keys.N)
            {
                NewIncognitoWindowToolStripMenuItem_Click(null, null);
            }
            else if (isControlKeyPressed && e.Alt && e.KeyData == Keys.N)
            {
                NewWindowToolStripMenuItem_Click(null, null);
            }
            else if (isControlKeyPressed && e.KeyData == Keys.N)
            {
                NewTab("korot://newtab");
            }
        }
        private Image GetImageFromURL(string URL)
        {
            if (URL.ToLower().StartsWith("file://"))
            {
                return Image.FromFile(URL.ToLower().Replace("file://", "").Replace("/", "\\"));
            }
            else if (URL == "BACKCOLOR") { return null; }
            else
            {
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        byte[] data = webClient.DownloadData(URL);

                        using (MemoryStream mem = new MemoryStream(data))
                        {
                            return Image.FromStream(mem);
                        }

                    }
                }
                catch
                {
                    return null;
                }
            }
        }
        private static int GerekiyorsaAzalt(int defaultint, int azaltma)
        {
            return defaultint > azaltma ? defaultint - 20 : defaultint;
        }

        private static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır)
        {
            return defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        }

        private Color oldBackColor;
        private Color oldOverlayColor;
        private string oldStyle;
        void ChangeTheme()
        {
            anaform.tabRenderer.ChangeColors(Properties.Settings.Default.BackColor, Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White, Properties.Settings.Default.OverlayColor);
            if (Properties.Settings.Default.OverlayColor != oldOverlayColor) { oldOverlayColor = Properties.Settings.Default.OverlayColor; pbProgress.BackColor = Properties.Settings.Default.OverlayColor; }
            if (Properties.Settings.Default.BackColor != oldBackColor)
            {
                cmsFavorite.BackColor = Properties.Settings.Default.BackColor;
                cmsIncognito.BackColor = Properties.Settings.Default.BackColor;
                oldBackColor = Properties.Settings.Default.BackColor;
                cmsIncognito.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsFavorite.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                button13.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                tsThemes.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.theme_w : Properties.Resources.theme;
                button6.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button4.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button8.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                button14.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.cancel_w : Properties.Resources.cancel;
                lbSettings.BackColor = Color.Transparent;
                lbSettings.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hlvDownload.BackColor = Properties.Settings.Default.BackColor;
                hlvHistory.BackColor = Properties.Settings.Default.BackColor;
                cmsDownload.BackColor = Properties.Settings.Default.BackColor;
                cmsHistory.BackColor = Properties.Settings.Default.BackColor;
                cmsSearchEngine.BackColor = Properties.Settings.Default.BackColor;
                profilenameToolStripMenuItem.DropDown.BackColor = Properties.Settings.Default.BackColor;
                profilenameToolStripMenuItem.DropDown.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsDownload.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                listBox2.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                comboBox1.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                textBox2.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                textBox3.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                button10.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hlvDownload.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                lbLang.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                hlvHistory.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                lbLang.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsHistory.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                cmsSearchEngine.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                listBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                comboBox1.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                btInstall.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                btUpdater.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                button12.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                button10.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                lbLang.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                lbLang.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                toolStripTextBox1.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                comboBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                comboBox3.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsProfiles.BackColor = Properties.Settings.Default.BackColor;
                cmsHamburger.BackColor = Properties.Settings.Default.BackColor;
                cmsPrivacy.BackColor = Properties.Settings.Default.BackColor;
                label2.BackColor = Properties.Settings.Default.BackColor;
                this.BackColor = Properties.Settings.Default.BackColor;
                extensionToolStripMenuItem1.DropDown.BackColor = Properties.Settings.Default.BackColor;
                aboutToolStripMenuItem.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.about_w : Properties.Resources.about;
                downloadsToolStripMenuItem.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.download_w : Properties.Resources.download;
                historyToolStripMenuItem.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.history_w : Properties.Resources.history;
                pictureBox1.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.inctab_w : Properties.Resources.inctab;
                tbAddress.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsHamburger.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsProfiles.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                this.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                label2.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                toolStripTextBox1.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsPrivacy.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                extensionToolStripMenuItem1.DropDown.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                textBox4.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                if (isLoadedPageFavroited) { button7.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.star_on_w : Properties.Resources.star_on; } else { button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; }
                mFavorites.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                settingsToolStripMenuItem.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.Settings : Properties.Resources.Settings_w;
                newWindowToolStripMenuItem.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.newwindow : Properties.Resources.newwindow_w;
                newIncognitoWindowToolStripMenuItem.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.inctab : Properties.Resources.inctab_w;
                button9.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.profiles : Properties.Resources.profiles_w;
                button1.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                button2.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.refresh : Properties.Resources.refresh_w;
                button3.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w;
                //button4.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.go : Properties.Resources.go_w;
                button5.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.home : Properties.Resources.home_w;
                button11.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.hamburger : Properties.Resources.hamburger_w;
                tbAddress.BackColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10)) : Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                textBox4.BackColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10)) : Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                mFavorites.BackColor = Properties.Settings.Default.BackColor;
                extensionToolStripMenuItem1.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.ext : Properties.Resources.ext_w;
                contextMenuStrip3.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip3.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;

                switchToToolStripMenuItem.DropDown.BackColor = Properties.Settings.Default.BackColor; switchToToolStripMenuItem.DropDown.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                foreach (ToolStripMenuItem x in cmsProfiles.Items) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White; }
                foreach (ToolStripMenuItem x in extensionToolStripMenuItem1.DropDownItems) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White; }
                foreach (TabPage x in tabControl1.TabPages) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black; }
            }
            if (Properties.Settings.Default.BackStyle != "BACKCOLOR")
            {
                if (Properties.Settings.Default.BackStyle != oldStyle)
                {
                    oldStyle = Properties.Settings.Default.BackStyle;
                    Image backStyle = GetImageFromURL(Properties.Settings.Default.BackStyle);
                    panel2.BackgroundImage = backStyle;
                    mFavorites.BackgroundImage = backStyle;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImage = backStyle; }
                    tabPage3.BackgroundImage = backStyle;
                }
            }
            else
            {
                if (Properties.Settings.Default.BackStyle != oldStyle)
                {
                    oldStyle = Properties.Settings.Default.BackStyle;
                    panel2.BackgroundImage = null;
                    mFavorites.BackgroundImage = null;
                    foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImage = null; }
                    tabPage3.BackgroundImage = null;
                }
            }
            if (Properties.Settings.Default.BStyleLayout == 0) //NONE
            {
                panel2.BackgroundImageLayout = ImageLayout.None;
                mFavorites.BackgroundImageLayout = ImageLayout.None;
                tabPage3.BackgroundImageLayout = ImageLayout.None;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.None; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 1) //TILE
            {
                panel2.BackgroundImageLayout = ImageLayout.Tile;
                mFavorites.BackgroundImageLayout = ImageLayout.Tile;
                tabPage3.BackgroundImageLayout = ImageLayout.Tile;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Tile; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 2) //CENTER
            {
                panel2.BackgroundImageLayout = ImageLayout.Center;
                mFavorites.BackgroundImageLayout = ImageLayout.Center;
                tabPage3.BackgroundImageLayout = ImageLayout.Center;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Center; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 3) //STRETCH
            {
                panel2.BackgroundImageLayout = ImageLayout.Stretch;
                mFavorites.BackgroundImageLayout = ImageLayout.Stretch;
                tabPage3.BackgroundImageLayout = ImageLayout.Stretch;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Stretch; }
            }
            else if (Properties.Settings.Default.BStyleLayout == 4) //ZOOM
            {
                panel2.BackgroundImageLayout = ImageLayout.Zoom;
                mFavorites.BackgroundImageLayout = ImageLayout.Zoom;
                tabPage3.BackgroundImageLayout = ImageLayout.Zoom;
                foreach (TabPage x in tabControl1.TabPages) { x.BackgroundImageLayout = ImageLayout.Zoom; }
            }
        }
        double websiteprogress;
        public void ChangeProgress(double value)
        {
            if (value == 1) { websiteprogress = value; pbProgress.Width = this.Width; }
            else
            {
                websiteprogress = value;
                pbProgress.Width = Convert.ToInt32(Convert.ToDouble(this.Width / 100) * Convert.ToDouble(value * 100));
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (chromiumWebBrowser1.IsDisposed)
            {
                this.Close();
            }

            if (findLast)
            {
                tsSearchStatus.Text = findC + " " + findCurrent + " " + findL + " " + findT + " " + findTotal;
            }
            else if (findCurrent == 0 && findTotal == 0)
            {
                tsSearchStatus.Text = noSearch;
            }
            else
            {
                tsSearchStatus.Text = findC + " " + findCurrent + " " + findL + " " + findT + " " + findTotal;
            }

            onCEFTab = (tabControl1.SelectedTab == tabPage1);
            try
            {
                ChangeTheme();
                this.Parent.Text = this.Text;
            }
            catch (Exception ex)
            {
                if (Properties.Settings.Default.debugLogExceptions)
                {
                    Output.WriteLine(" [Korot.frmCEF.timer1_Tick] Error: " + ex.ToString());
                }
            }

            RefreshTranslation();
            if (anaform.restoremedaddy == "") { spRestorer.Visible = false; restoreLastSessionToolStripMenuItem.Visible = false; } else { spRestorer.Visible = true; restoreLastSessionToolStripMenuItem.Visible = true; }
        }

        private void TestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load(((ToolStripMenuItem)sender).Tag.ToString());
        }
        bool isLoadedPageFavroited = false;

        private void Button7_Click(object sender, EventArgs e)
        {
            if (isLoadedPageFavroited)
            {
                Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(chromiumWebBrowser1.Address + ";", "");
                Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(this.Text + ";", "");
                button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w;
                isLoadedPageFavroited = false;
            }
            else
            {
                Properties.Settings.Default.Favorites += (chromiumWebBrowser1.Address + ";");
                Properties.Settings.Default.Favorites += (this.Text + ";");
                button7.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.star_on_w : Properties.Resources.star_on;
                isLoadedPageFavroited = true;
            }
            RefreshFavorites();
        }
        void RefreshProfiles()
        {
            switchToToolStripMenuItem.DropDownItems.Clear();
            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\"))
            {
                ToolStripMenuItem profileItem = new ToolStripMenuItem();
                profileItem.Text = new DirectoryInfo(x).Name;
                profileItem.Click += ProfilesToolStripMenuItem_Click;
                switchToToolStripMenuItem.DropDownItems.Add(profileItem);
            }
        }
        public void RefreshTranslation()
        {
            if (emptyItem != null) { emptyItem.Text = this.empty; }
            comboBox3.SelectedIndex = Properties.Settings.Default.BStyleLayout;
            colorToolStripMenuItem.Checked = Properties.Settings.Default.BackStyle == "BACKCOLOR" ? true : false;
            switchToToolStripMenuItem.Text = this.switchTo;
            newProfileToolStripMenuItem.Text = this.newprofile;
            deleteThisProfileToolStripMenuItem.Text = this.deleteProfile;
            showCertificateErrorsToolStripMenuItem.Text = this.showCertError;
            textBox4.Text = Properties.Settings.Default.BackStyle == "BACKCOLOR" ? usingBC : Properties.Settings.Default.BackStyle;
            if (certError)
            {
                safeStatusToolStripMenuItem.Text = this.CertificateErrorTitle;
                ınfoToolStripMenuItem.Text = this.CertificateError;
            }
            else
            {
                safeStatusToolStripMenuItem.Text = this.CertificateOKTitle;
                ınfoToolStripMenuItem.Text = this.CertificateOK;
            }
            if (cookieUsage) { cookieInfoToolStripMenuItem.Text = this.usesCookies; } else { cookieInfoToolStripMenuItem.Text = this.notUsesCookies; }
            label7.Text = this.CertErrorPageTitle;
            label8.Text = this.CertErrorPageMessage;
            button10.Text = this.CertErrorPageButton;
            newWindowToolStripMenuItem.Text = this.newWindow;
            newIncognitoWindowToolStripMenuItem.Text = this.newincognitoWindow;
            settingsToolStripMenuItem.Text = this.settingstitle;
            restoreLastSessionToolStripMenuItem.Text = this.restoreOldSessions;
        }
        private void ProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => anaform.SwitchProfile(((ToolStripMenuItem)sender).Text)));
        }

        private void NewProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => anaform.NewProfile()));
        }

        private void DeleteThisProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke(new Action(() => anaform.DeleteProfile(userName)));
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            cmsProfiles.Show(MousePosition);
        }
        private void DefaultProxyts_Click(object sender, EventArgs e)
        {
            SetProxy(chromiumWebBrowser1, defaultProxy);
            DefaultProxyts.Enabled = false;
        }
        private void ExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String fileLocation = ((ToolStripMenuItem)sender).Tag.ToString();
            string Playlist = FileSystem2.ReadFile(fileLocation, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(3, 1) == "1" && (new FileInfo(((ToolStripMenuItem)sender).Tag.ToString()).Length < 1048576) && (new FileInfo(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(((ToolStripMenuItem)sender).Tag.ToString()).Directory + "\\")).Length < 5242880))
            {
                try
                {
                    chromiumWebBrowser1.GetMainFrame().ExecuteJavaScriptAsync(FileSystem2.ReadFile(SplittedFase[5].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\"), Encoding.UTF8));
                }
                catch (Exception ex)
                {
                    if (Properties.Settings.Default.debugLogExceptions)
                    {
                        Output.WriteLine(" [Korot.frmCEF.ExtensionToolStripMenuItem] Error: " + ex.ToString());
                    }
                }
            }
            if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(4, 1) == "1" && !string.IsNullOrWhiteSpace(SplittedFase[7].Substring(1).Replace(Environment.NewLine, "")) && defaultProxy != null)
            {
                SetProxy(chromiumWebBrowser1, SplittedFase[7].Substring(1).Replace(Environment.NewLine, ""));
                DefaultProxyts.Enabled = true;
                if (Properties.Settings.Default.rememberLastProxy) { Properties.Settings.Default.LastProxy = SplittedFase[7].Substring(1).Replace(Environment.NewLine, ""); }
            }
            bool allowWebContent = false;
            if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(1, 1) == "1") { allowWebContent = true; }
            if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "").Substring(2, 1) == "1")
            {
                frmExt formext = new frmExt(this, anaform, userName, fileLocation, SplittedFase[6].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\"), allowWebContent);
                formext.Show();
            }
        }
        public void LoadExt()
        {
            extensionToolStripMenuItem1.DropDownItems.Clear();
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }

            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"))
            {
                if (File.Exists(x + "\\ext.kem") && new FileInfo(x + "\\ext.kem").Length < 1048576)
                {
                    string Playlist = FileSystem2.ReadFile(x + "\\ext.kem", Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    ToolStripMenuItem extItem = new ToolStripMenuItem();
                    extItem.Text = SplittedFase[0].Substring(0).Replace(Environment.NewLine, "");
                    if (!File.Exists(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\")))
                    {
                        if (Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                        else { extItem.Image = Properties.Resources.ext_w; }
                    }
                    else
                    {
                        if (new FileInfo(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\")).Length < 5242880)
                        {
                            extItem.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\"));
                        }
                        else
                        {
                            if (Brightness(Properties.Settings.Default.BackColor) > 130) { extItem.Image = Properties.Resources.ext; }
                            else { extItem.Image = Properties.Resources.ext_w; }
                        }
                    }
                    ToolStripMenuItem extRunItem = new ToolStripMenuItem();
                    extItem.Click += ExtensionToolStripMenuItem_Click;
                    extItem.Tag = x + "\\ext.kem";
                    extensionToolStripMenuItem1.DropDownItems.Add(extItem);

                }
            }
            if (extensionToolStripMenuItem1.DropDownItems.Count == 0)
            {
                ToolStripMenuItem emptylol = new ToolStripMenuItem();
                emptyItem = emptylol;
                emptylol.Text = this.empty;
                emptylol.Enabled = false;
                extensionToolStripMenuItem1.DropDownItems.Add(emptylol);
            }
        }
        ToolStripMenuItem emptyItem;

        private void TmrSlower_Tick(object sender, EventArgs e)
        {
            RefreshFavorites();
        }

        public void FrmCEF_SizeChanged(object sender, EventArgs e)
        {
            ChangeProgress(websiteprogress);
        }


        private void Panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            tabform_KeyDown(panel1, new KeyEventArgs(e.KeyData));
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cmsPrivacy.Show(pictureBox2, 0, pictureBox2.Size.Height);
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmsPrivacy.Close();
        }

        private void showCertificateErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showCertificateErrorsToolStripMenuItem.Tag != null)
            {
                TextBox txtCertificate = new TextBox() { ScrollBars = ScrollBars.Both, Multiline = true, Dock = DockStyle.Fill, Text = showCertificateErrorsToolStripMenuItem.Tag.ToString() };
                Form frmCertificate = new Form() { Icon = this.Icon, Text = this.CertificateErrorMenuTitle, FormBorderStyle = FormBorderStyle.SizableToolWindow };
                frmCertificate.Controls.Add(txtCertificate);
                frmCertificate.ShowDialog();
            }
        }
        public List<string> CertAllowedUrls = new List<string>();
        private void button10_Click(object sender, EventArgs e)
        {
            CertAllowedUrls.Add(button10.Tag.ToString());
            chromiumWebBrowser1.Refresh();
            pnlCert.Visible = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage3;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (cmsHamburger.Visible) { cmsHamburger.Close(); } else { cmsHamburger.Show(button11, 0, 0); }
            button11.FlatAppearance.BorderSize = 0;
        }

        private void restoreLastSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.ReadSession(anaform.restoremedaddy)));
            restoreLastSessionToolStripMenuItem.Visible = false;
        }
        bool allowSwitching = false;
        bool onCEFTab = true;
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (allowSwitching == false) { e.Cancel = true; } else { toolStripTextBox1.Text = SearchOnPage; chromiumWebBrowser1.StopFinding(true); e.Cancel = false; allowSwitching = false; }
            onCEFTab = (tabControl1.SelectedTab == tabPage1);
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            contextMenuStrip3.Show(textBox4, 0, 0);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            chromiumWebBrowser1.Load("korot://licenses");
        }
        private void contextMenuStrip4_Opening(object sender, CancelEventArgs e)
        {
            Process.Start("explorer.exe", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\\"");
            e.Cancel = true;
        }

        private void hsDoNotTrack_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DoNotTrack = hsDoNotTrack.Checked;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BStyleLayout = comboBox3.SelectedIndex;
        }

        private void Button10_Click(object sender, EventArgs e)
        {

        }

        private void disallowThisPageForCookieAccessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.CookieDisallowList.Contains(chromiumWebBrowser1.Address))
            {
                Properties.Settings.Default.CookieDisallowList.Remove(chromiumWebBrowser1.Address);
                chromiumWebBrowser1.Reload();
            }
            else
            {
                Properties.Settings.Default.CookieDisallowList.Add(chromiumWebBrowser1.Address);
                chromiumWebBrowser1.Reload();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            cmsIncognito.Show(pictureBox1, 0, 0);
        }

        private void openInNewTab_Click(object sender, EventArgs e)
        {
            if (selectedFavorite != null) { NewTab(selectedFavorite.Tag.ToString()); }
        }

        private void removeSelectedTSMI_Click(object sender, EventArgs e)
        {
            if (selectedFavorite.Tag.ToString() == chromiumWebBrowser1.Address)
            {
                isLoadedPageFavroited = false;
                button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
            }
            Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(selectedFavorite.Tag.ToString() + ";", "");
            Properties.Settings.Default.Favorites = Properties.Settings.Default.Favorites.Replace(selectedFavorite.Text + ";", "");
            RefreshFavorites();
        }

        private void clearTSMI_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Favorites = "";
            button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; ;
            isLoadedPageFavroited = false;
            RefreshFavorites();
        }

        private void cmsFavorite_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            selectedFavorite = null;
        }

        private void cmsFavorite_Opening(object sender, CancelEventArgs e)
        {
            if (selectedFavorite == null)
            {
                removeSelectedTSMI.Enabled = false;
                openInNewTab.Enabled = false;
            }
            else
            {
                removeSelectedTSMI.Enabled = !_Incognito;
                openInNewTab.Enabled = !_Incognito;
            }
        }
        public void takeScreenShot()
        {
            takeAScreenshotToolStripMenuItem_Click(null, null);
        }

        public void savePage()
        {
            saveThisPageToolStripMenuItem_Click(null, null);
        }

        private void takeAScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                FileName = "Korot Screenshot.png",
                Filter = imageFiles + "|*.png|" + allFiles + "|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                FileSystem2.WriteFile(save.FileName, TakeScrenshot.ImageToByte2(TakeScrenshot.Snapshot(chromiumWebBrowser1)));
            }
        }

        private void saveThisPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            SaveFileDialog save = new SaveFileDialog()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                FileName = this.Text + ".html",
                Filter = htmlFiles + "|*.html;*.htm|" + allFiles + "|*.*"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                Task<String> htmlText = chromiumWebBrowser1.GetSourceAsync();
                FileSystem2.WriteFile(save.FileName, htmlText.Result, Encoding.UTF8);
            }
        }

        private void frmCEF_KeyUp(object sender, KeyEventArgs e)
        {
            isControlKeyPressed = !e.Control;
        }

        private void MouseScroll(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.debugLogMouse)
            {
                Output.WriteLine(" [Korot.frmCEF.MosueScroll] MosueEventArgs: " + e.ToString());
            }

            if (isControlKeyPressed)
            {
                allowSwitching = true;
                tabControl1.SelectedTab = tabPage1;
                if (e.Delta > 0)
                {
                    zoomInToolStripMenuItem_Click(sender, null);
                }
                else if (e.Delta < 0)
                {
                    zoomOutToolStripMenuItem_Click(sender, null);
                }
            }
        }

        private void resetZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            chromiumWebBrowser1.SetZoomLevel(0.0);
            cmsHamburger.Show(button11, 0, 0);
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        public void zoomIn()
        {
            Task<double> zoomLevel = chromiumWebBrowser1.GetZoomLevelAsync();
            if (zoomLevel.Result <= 8)
            {
                chromiumWebBrowser1.SetZoomLevel(zoomLevel.Result + 0.25);
            }
        }
        public void zoomOut()
        {
            Task<double> zoomLevel = chromiumWebBrowser1.GetZoomLevelAsync();
            if (zoomLevel.Result >= -0.75)
            {
                chromiumWebBrowser1.SetZoomLevel(zoomLevel.Result - 0.25);
            }
        }
        public void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            zoomIn();
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);

        }

        public void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage1;
            zoomOut();
            cmsHamburger.Show(button11, 0, 0);
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        bool doNotDestroyFind = false;
        private void cmsHamburger_Opening(object sender, CancelEventArgs e)
        {
            if (!doNotDestroyFind)
            {
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
                doNotDestroyFind = false;
            }
            Task.Run(new Action(() => getZoomLevel()));
        }
        async void getZoomLevel()
        {
            Task<double> zoomLevel = chromiumWebBrowser1.GetZoomLevelAsync();
            zOOMLEVELToolStripMenuItem.Text = ((zoomLevel.Result * 100) + 100) + "%";
        }
        string searchPrev;
        private void toolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(toolStripTextBox1.Text)) & toolStripTextBox1.Text != SearchOnPage)
            {
                searchPrev = toolStripTextBox1.Text;
                chromiumWebBrowser1.Find(0, toolStripTextBox1.Text, true, caseSensitiveToolStripMenuItem.Checked, false);
            }
            else
            {
                doNotDestroyFind = false;
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
            }
        }

        private void cmsHamburger_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (!doNotDestroyFind)
            {
                toolStripTextBox1.Text = SearchOnPage;
                chromiumWebBrowser1.StopFinding(true);
                doNotDestroyFind = false;
            }
        }

        private void caseSensitiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            if (!toolStripTextBox1.Selected)
            {
                toolStripTextBox1.SelectAll();
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage4;
        }

        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage5;
        }
        private void tsSearchNext_Click(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Find(0, toolStripTextBox1.Text, true, caseSensitiveToolStripMenuItem.Checked, true);
            cmsHamburger.Show(button11, 0, 0);
            doNotDestroyFind = true;
            toolStripTextBox1.Text = searchPrev;
            toolStripTextBox_TextChanged(null, e);
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage6;
        }

        private void hsProxy_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.rememberLastProxy = hsProxy.Checked;
        }

        private void tsThemes_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            allowSwitching = true;
            tabControl1.SelectedTab = tabPage7;
        }

        private void clickHereToLearnMoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab("korot://incognito");
            anaform.Invoke(new Action(() => anaform.SelectedTabIndex = anaform.Tabs.Count - 1));
        }

        private void ıncognitoModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmsIncognito.Close();
        }
    }
}
