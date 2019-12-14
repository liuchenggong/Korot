using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using HaltroyTabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace Korot
{
    public partial class frmCEF : Form
    {
        int updateProgress = 0;
        //0 = Checking 1=UpToDate 2=UpdateAvailabe 3=Error
        bool noProxy = true;
        bool isLoading = false;
        string loaduri = null;
        bool _Incognito = false;
        string userName;
        string userCache;
        frmMain anaform;
        public ChromiumWebBrowser chromiumWebBrowser1;
        string defaultproxyaddress;
        // [NEWTAB]
        public frmCEF(frmMain rmmain, bool isIncognito = false, string loadurl = "korot://newtab", string profileName = "user0")
        {
            loaduri = loadurl;
            anaform = rmmain;
            _Incognito = isIncognito;
            userName = profileName;
            userCache = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + profileName + "\\cache\\";
#pragma warning disable CS0618 // Tür veya üye artık kullanılmıyor
            WebProxy proxy = (WebProxy)WebProxy.GetDefaultProxy();
#pragma warning restore CS0618 // Tür veya üye artık kullanılmıyor
            Uri resource = new Uri("http://localhost");
            Uri resourceProxy = proxy.GetProxy(resource);
            if (resourceProxy == resource)
            {
                defaultproxyaddress = null;
                noProxy = true;
            }
            else
            {
                noProxy = false;
                defaultproxyaddress = resourceProxy.AbsoluteUri.ToString();
                Output.WriteLine("[INFO] Listening on proxy : " + defaultproxyaddress);
                SetProxy(chromiumWebBrowser1, defaultproxyaddress);
            }
            InitializeComponent();
            InitializeChromium();
            foreach (Control x in this.Controls)
            {
                try { x.KeyDown += tabform_KeyDown; } catch { }
            }
        }
        void RefreshHistory()
        {
            hlvHistory.Items.Clear();
            string Playlist = Properties.Settings.Default.History;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                ListViewItem listV = new ListViewItem(SplittedFase[i].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 1].Replace(Environment.NewLine, ""));
                listV.SubItems.Add(SplittedFase[i + 2].Replace(Environment.NewLine, ""));
                hlvHistory.Items.Add(listV);
                i += 3;

            }
        }
        private void ListBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ThemesTitle, listBox2.SelectedItem.ToString() + Environment.NewLine + ThemeMessage, this.Icon, MessageBoxButtons.YesNoCancel, Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
                if (mesaj.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + listBox2.SelectedItem.ToString());
                        comboBox1.Text = listBox2.SelectedItem.ToString().Replace(".ktf", "");
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(ex.Message);
                    }

                }
            }
        }

        void SetLanguage(string privatemodetxt,
                         string updatetitletxt,
                         string updatemessagetxt,
                         string updateerrortxt,
                         string CreateTabtext,
                         string csnote,
                         string cse,
                         string nw,
                         string niw,
                         string settingstxt,
                         string historytxt,
                         string hptxt,
                         string setxt,
                         string themetxt,
                         string customtxt,
                         string rstxt,
                         string backstyle,
                         string cleartxt,
                         string AboutText,
                         string gBack,
                         string gForward,
                         string reload,
                         string reloadnoc,
                         string stoop,
                         string selectal,
                         string olint,
                         string cl,
                         string sia,
                         string oint,
                         string pastetxt,
                         string copytxt,
                         string cuttxt,
                         string undotxt,
                         string redotxt,
                         string deletetxt,
                         string oossint,
                         string devtool,
                         string viewsrc,
                         string downloadsstring,
                         string bcolor,
                         string ocolor,
                         string korotdown,
                         string otad,
                         string ctad,
                         string _open,
                         string tarih,
                         string kaynak,
                         string hedef,
                         string kaynak2nokta,
                         string hedef2nokta,
                         string titleErrorPage,
                         string abouttxt,
                         string themename,
                         string save,
                         string defaultproxysetting,
                         string themes,
                         string openlinkinnt,
                         string openfile,
                         string openfolder,
                         string SearchOnCurrentPage,
                         string CaseSensitivity,
                         string DayName,
                         string MonthName,
                         string searchhelp,
                         string kt,
                         string et,
                         string e1,
                         string e2,
                         string e3,
                         string e4,
                         string rt,
                         string r1,
                         string r2,
                         string r3,
                         string r4,
                         string searchtxt,
                         string SwitchTo,
                         string newProfile,
                         string delProfile,
                         string goToURL,
                         string searchURL,
                         string enterValidUrl,
                         string newProfInfo,
                         string restoreLastSession,
                         string yes,
                         string no,
                         string ok,
                         string cancel,
                         string updateava,
                         string checkmessage,
                         string upToDate,
                         string checkbutton,
                         string installbutton,
                         string installstatus,
                         string statustype,
                         string themeTitle,
                         string themeMessage,
                         string themeError,
                         string cemt,
                         string cet,
                         string ce,
                         string cokt,
                         string cok,
                         string sce,
                         string uc,
                         string nuc,
                         string cept,
                         string cepm,
                         string cepb,
                         string aboutkorot,
                         string licenses,
                         string uch,
                         string ucb,
                         string ucg,
                         string ucd,
                         string uca)
        {
            UCAlpha = uca.Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
            label22.Text = uch.Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
            UCBeta = ucb.Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
            UCGama = ucg.Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
            UCDelta = ucd.Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
            label21.Text = aboutkorot.Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
            linkLabel1.Text = licenses.Replace(Environment.NewLine, "");
            linkLabel1.LinkArea = new LinkArea(0, linkLabel1.Text.Length);
            linkLabel1.Location = new Point(label21.Location.X, label21.Location.Y + label21.Size.Height);
            searchToolStripMenuItem.Text = searchtxt.Replace(Environment.NewLine, "");
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
            ThemesTitle = themeTitle.Replace(Environment.NewLine, "");
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
            textBox4.Width = tbTheme.Width - (label12.Width + label12.Location.X + 5);
            newWindow = nw.Replace(Environment.NewLine, "");
            newincognitoWindow = niw.Replace(Environment.NewLine, "");
            anaform.newincwindow = niw.Replace(Environment.NewLine, "");
            anaform.newwindow = nw.Replace(Environment.NewLine, "");
            tbSetting.Text = settingstxt.Replace(Environment.NewLine, "");
            tbDownload.Text = downloadsstring.Replace(Environment.NewLine, "");
            tbAbout.Text = AboutText.Replace(Environment.NewLine, "");
            label11.Text = hptxt.Replace(Environment.NewLine, "");
            label3.Text = setxt.Replace(Environment.NewLine, "");
            tbTheme.Text = themetxt.Replace(Environment.NewLine, "");
            SearchOnPage = SearchOnCurrentPage.Replace(Environment.NewLine, "");
            CaseSensitive = CaseSensitivity.Replace(Environment.NewLine, "");
            customToolStripMenuItem.Text = customtxt.Replace(Environment.NewLine, "");
            removeSelectedToolStripMenuItem.Text = rstxt.Replace(Environment.NewLine, "");
            clearToolStripMenuItem.Text = cleartxt.Replace(Environment.NewLine, "");
            settingstitle = settingstxt.Replace(Environment.NewLine, "");
            this.Text = settingstxt.Replace(Environment.NewLine, "");
            tbHistory.Text = historytxt.Replace(Environment.NewLine, "");
            tbAbout.Text = abouttxt.Replace(Environment.NewLine, "");
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
            // lightToolStripMenuItem.Text = ltxt.Replace(Environment.NewLine, "");
            //darkToolStripMenuItem.Text = dtxt.Replace(Environment.NewLine, "");
            restoreOldSessions = restoreLastSession.Replace(Environment.NewLine, "");
            label7.Text = bcolor.Replace(Environment.NewLine, "");
            enterAValidUrl = enterValidUrl.Replace(Environment.NewLine, "");
            label8.Text = ocolor.Replace(Environment.NewLine, "");
            chDate.Text = kaynak.Replace(Environment.NewLine, "");
            fromtwodot = kaynak2nokta.Replace(Environment.NewLine, "");
            chFrom.Text = hedef.Replace(Environment.NewLine, "");
            totwodot = hedef2nokta.Replace(Environment.NewLine, "");
            korotdownloading = korotdown.Replace(Environment.NewLine, "");
            chTo.Text = tarih.Replace(Environment.NewLine, "");
            openfileafterdownload = otad.Replace(Environment.NewLine, "");
            closethisafterdownload = ctad.Replace(Environment.NewLine, "");
            open = _open.Replace(Environment.NewLine, "");
            openLinkİnNewTabToolStripMenuItem.Text = openlinkinnt.Replace(Environment.NewLine, "");
            openFileToolStripMenuItem.Text = openfile.Replace(Environment.NewLine, "");
            openFileİnExplorerToolStripMenuItem.Text = openfolder.Replace(Environment.NewLine, "");
            removeSelectedToolStripMenuItem1.Text = rstxt.Replace(Environment.NewLine, "");
            clearToolStripMenuItem2.Text = cleartxt.Replace(Environment.NewLine, "");
            defaultproxytext = defaultproxysetting.Replace(Environment.NewLine, "");
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
            //pages
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
                        Properties.Settings.Default.Save();
                    }
                    catch (Exception ex)
                    {
                        Output.WriteLine(ex.Message);
                    }

                }
            }
        }
        public void LoadLangFromFile(string LangFile)
        {
            try
            {
                languagedummy.Items.Clear();
                StreamReader ReadFile = new StreamReader(LangFile, Encoding.UTF8, false);
                string Playlist = ReadFile.ReadToEnd();
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                int Count = SplittedFase.Length - 1; ; int i = 0;
                while (!(i == Count))
                {
                    languagedummy.Items.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                    i += 1;
                }
                ReadFile.Close();
                ReadLangFileFromTemp();
            }
            catch (Exception ex)
            {
                Output.WriteLine(ex.Message);
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
                    languagedummy.Items[111].ToString().Substring(1),
                    languagedummy.Items[112].ToString().Substring(1),
                    languagedummy.Items[113].ToString().Substring(1),
                    languagedummy.Items[114].ToString().Substring(1),
                    languagedummy.Items[115].ToString().Substring(1));
            }
            catch (Exception ex)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle, "This file does not suitable for this version of Korot.Please ask the creator of this language to update." + Environment.NewLine + " Error : " + ex.Message, this.Icon, MessageBoxButtons.OK, Properties.Settings.Default.BackColor, anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);
                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(ex.ToString());
            }

        }
        public void RefreshLangList()
        {
            lbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.lang", SearchOption.TopDirectoryOnly))
            {
                lbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
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
                Properties.Settings.Default.Save();
            }
            else
            {
                radioButton1.Checked = false;
                Properties.Settings.Default.Homepage = textBox2.Text;
                Properties.Settings.Default.Save();
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
            Properties.Settings.Default.Save();
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
            Properties.Settings.Default.Save();
            ChangeTheme();
        }


        private void Label8_Click(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.DodgerBlue;
            Properties.Settings.Default.OverlayColor = pictureBox4.BackColor;
            Properties.Settings.Default.Save();
            ChangeTheme();
        }
        public void RefreshDownloadList()
        {
            hlvDownload.Items.Clear();
            string Playlist = Properties.Settings.Default.DowloadHistory;
            string[] SplittedFase = Playlist.Split(';');
            int Count = SplittedFase.Length - 1; ; int i = 0;
            while (!(i == Count))
            {
                ListViewItem listV = new ListViewItem();
                listV.Text = SplittedFase[i].Replace(Environment.NewLine, "");
                i += 1;
                listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                i += 1;
                listV.SubItems.Add(SplittedFase[i].Replace(Environment.NewLine, ""));
                i += 1;
                hlvDownload.Items.Add(listV);
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
            Process.Start(hlvDownload.SelectedItems[0].SubItems[1].Text);
        }

        private void OpenFileİnExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", "/select," + hlvDownload.SelectedItems[0].SubItems[1].Text);
        }

        private void RemoveSelectedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string x = hlvDownload.SelectedItems[0].Text + ";" + hlvDownload.SelectedItems[0].SubItems[1].Text + ";" + hlvDownload.SelectedItems[0].SubItems[2].Text + ";";
            Properties.Settings.Default.DowloadHistory = Properties.Settings.Default.DowloadHistory.Replace(x, "");
            Properties.Settings.Default.Save();
            RefreshDownloadList();
        }

        private void ClearToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DowloadHistory = "";
            Properties.Settings.Default.Save();
            RefreshDownloadList();
        }

        private void HlvHistory_DoubleClick(object sender, EventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab(hlvHistory.SelectedItems[0].SubItems[2].Text)));
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
            ColorDialog colorpick = new ColorDialog();
            colorpick.AnyColor = true;
            colorpick.FullOpen = true;
            if (colorpick.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackStyle = "background-color: rgb(" + colorpick.Color.R + "," + colorpick.Color.G + "," + colorpick.Color.B + ")";
                textBox4.Text = Properties.Settings.Default.BackStyle;
            }
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about)|(?:about)|(?:file)|(?:korot)|(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s);
        }
        private void FromURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputbox = new HaltroyFramework.HaltroyInputBox("Korot",
                                                                                            enterAValidUrl,
                                                                                            this.Icon,
                                                                                            "",
                                                                                            Properties.Settings.Default.BackColor,
                                                                                            Properties.Settings.Default.OverlayColor,
                                                                                            anaform.OK, anaform.Cancel, 400, 150);
            if (inputbox.ShowDialog() == DialogResult.OK)
            {
                if (ValidHttpURL(inputbox.TextValue()))
                {
                    Properties.Settings.Default.BackStyle = "background-image: url(\"" + inputbox.TextValue().Replace("\\", "/") + "\")";
                    textBox4.Text = Properties.Settings.Default.BackStyle;
                }
                else
                {
                    FromURLToolStripMenuItem_Click(null, null);
                }
            }
        }

        private void FromLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog filedlg = new OpenFileDialog();
            filedlg.Filter = "Image Files|*.jpg;*.png;*.bmp;*.jpeg;*.jfif;*.gif;*.apng;*.ico;*.svg;*.webp|All Files|*.*";
            filedlg.Title = "Select a Background Image";
            filedlg.Multiselect = false;
            if (filedlg.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackStyle = "background-image: url(\"file://" + filedlg.FileName.Replace("\\", "/") + "\")";
                textBox4.Text = Properties.Settings.Default.BackStyle;
            }
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(MousePosition);
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
            Form1 frmUpdate = new Form1(this);
            frmUpdate.Show();
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
            Properties.Settings.Default.Save();
            RefreshHistory();
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.History = "";
            Properties.Settings.Default.Save();
            RefreshHistory();
        }



        private void Label2_Click(object sender, EventArgs e)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab("http://korot.haltroy.com")));
        }


        private void LbLang_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView1_DoubleClick(null, null);
        }

        public void LoadTheme(string themeFile)
        {
            try
            {
                StreamReader ReadFile3 = new StreamReader(themeFile, Encoding.UTF8, false);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string Playlist3 = ReadFile3.ReadToEnd();
                string[] SplittedFase3 = Playlist3.Split(token);
                int backR = System.Convert.ToInt32(SplittedFase3[0].Replace(Environment.NewLine, ""));

                int backG = System.Convert.ToInt32(SplittedFase3[1].Replace(Environment.NewLine, ""));

                int backB = System.Convert.ToInt32(SplittedFase3[2].Replace(Environment.NewLine, ""));

                int ovR = System.Convert.ToInt32(SplittedFase3[3].Replace(Environment.NewLine, ""));

                int ovG = System.Convert.ToInt32(SplittedFase3[4].Replace(Environment.NewLine, ""));

                int ovB = System.Convert.ToInt32(SplittedFase3[5].Replace(Environment.NewLine, ""));
                Properties.Settings.Default.BackColor = Color.FromArgb(255, backR, backG, backB);
                Properties.Settings.Default.OverlayColor = Color.FromArgb(255, ovR, ovG, ovB);
                Properties.Settings.Default.BackStyle = SplittedFase3[6].Replace(Environment.NewLine, "").Replace("[THEMEFOLDER]", "file://" + Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\");
                pictureBox3.BackColor = Properties.Settings.Default.BackColor;
                pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
                Properties.Settings.Default.ThemeFile = themeFile;

                ReadFile3.Close();
            }
            catch (Exception ex)
            {
                HaltroyFramework.HaltroyMsgBox mesaj = new HaltroyFramework.HaltroyMsgBox(ErrorPageTitle,
                                                                                          ErrorTheme,
                                                                                          this.Icon,
                                                                                          MessageBoxButtons.OK,
                                                                                          Properties.Settings.Default.BackColor,
                                                                                          anaform.Yes, anaform.No, anaform.OK, anaform.Cancel, 390, 140);

                DialogResult diyalog = mesaj.ShowDialog();
                Output.WriteLine(ex.ToString());

                LoadTheme(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf");
            }
        }
        #region "Translate"
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
        public String StatusType = "[PERC]% | [CURRENT] KB downloaded out of [TOTAL] KB.";
        public string enterAValidUrl = "Enter a Valid URL";
        public string goTotxt = "Go to \"[TEXT]\"";
        public string SearchOnWeb = "Search \"[TEXT]\"";
        public string defaultproxytext = "Default Proxy";
        public string SearchOnPage = "Search: ";
        public string CaseSensitive = "Case Sensitive";
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
        // Here comes dat contextmenu
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
        // here ends contextmenu
        public string korotdownloading = "Korot - Downloading";
        public string fromtwodot = "From : ";
        public string totwodot = "To : ";
        public string openfileafterdownload = "Open this file after download";
        public string closethisafterdownload = "Close this after download";
        public string open = "Open";
        //here ends the context menu
        public string Search = "Search";
        public string run = "Run";
        public string startatstarup = "Run at startup";
        public string empty = "(empty)";

        public ListBox languagedummy = new ListBox();
        //here starts the special pages
        public string ThemesTitle = "Korot - Themes";
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
        //here ends the pages
        public string switchTo = "Switch to:";
        public string deleteProfile = "Delete this profile";
        public string newprofile = "New Profile";
        public string CertErrorPageTitle = "This website is not secure";
        public string CertErrorPageMessage = "This website is using a certificate that has errors. Which means your information (credit cards,passwords,messages...) can be stolen by unknown people in this website.";
        public string CertErrorPageButton = "I understand these risks.";
        //UpdateType
        public string UCBeta = "Beta: Receives almost every update. Only receives updates with min. 0.0.1 change in version. Good for safely bug-haunting. (0.0.0.0 -> 0.0.1.0)";
        public string UCGama = "Gama: Receives less updates than Beta. Only receives updates with min. 0.1 change in version. Good for people still wanting to get more features without taking a big risk. (0.0.0.0 -> 0.1.0.0)";
        public string UCDelta = "Delta: Receives less updates than Gama. Only receives updates with min. 1.0 change in version. Good for people that doesn't wants to take risks and don't understand computer. (0.0.0.0 -> 1.0.0.0)";
        public string UCAlpha = "Alpha: Receives every update. These updates may sometimes be small bugfixes. Good for bug-haunting. (0.0.0.0 -> 0.0.0.1)";
        #endregion
        public void refreshThemeList()
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


        private void Button10_Click(object sender, EventArgs e)
        {
            System.IO.StreamWriter objWriter3;
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\"); }
            objWriter3 = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\" + comboBox1.Text + ".ktf");
            string x = Properties.Settings.Default.BackStyle;
            string lol = Properties.Settings.Default.BackColor.R + Environment.NewLine + Properties.Settings.Default.BackColor.G + Environment.NewLine + Properties.Settings.Default.BackColor.B + Environment.NewLine + Properties.Settings.Default.OverlayColor.R + Environment.NewLine + Properties.Settings.Default.OverlayColor.G + Environment.NewLine + Properties.Settings.Default.OverlayColor.B + Environment.NewLine + x + Environment.NewLine;
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
                if (Properties.Settings.Default.UpdateChannel == 0) { UpdateResultAlpha(e.Result); }
                if (Properties.Settings.Default.UpdateChannel == 1) { UpdateResultBeta(e.Result); }
                if (Properties.Settings.Default.UpdateChannel == 2) { UpdateResultGama(e.Result); }
                if (Properties.Settings.Default.UpdateChannel == 3) { UpdateResultDelta(e.Result); }
            }
        }
        void UpdateResultAlpha(String latestversion)
        {
            Version newest = new Version(latestversion);
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
                        Form1 frmUpdate = new Form1(this);
                        frmUpdate.Show();
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
        void UpdateResultBeta(String latestversion)
        {
            Version newest = new Version(latestversion);
            Version current = new Version(Application.ProductVersion);
            if (newest.Minor > current.Minor || newest.Major > current.Major || newest.Build > current.Build)
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
                        Form1 frmUpdate = new Form1(this);
                        frmUpdate.Show();
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
        void UpdateResultGama(String latestversion)
        {
            Version newest = new Version(latestversion);
            Version current = new Version(Application.ProductVersion);
            if (newest.Minor > current.Minor || newest.Major > current.Major)
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
                        Form1 frmUpdate = new Form1(this);
                        frmUpdate.Show();
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
        void UpdateResultDelta(String latestversion)
        {
            Version newest = new Version(latestversion);
            Version current = new Version(Application.ProductVersion);
            if (newest.Major > current.Major)
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
                        Form1 frmUpdate = new Form1(this);
                        frmUpdate.Show();
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
        public TitleBarTabs ParentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }
        }
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
                if (mo != null) return mo;
            }

            return null;
        }

        public static string GetOsVer()
        {
            try
            {
                ManagementObject mo = GetMngObj("Win32_OperatingSystem");

                if (null == mo)
                    return string.Empty;

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
                + Environment.OSVersion.Platform
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
            chromiumWebBrowser1.Dock = DockStyle.Fill;
            chromiumWebBrowser1.Show();
        }
        public void executeStartupExtensions()
        {
            foreach (String x in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\", "*.*", SearchOption.AllDirectories))
            {
                if (x.EndsWith("\\ext.kem", StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        StreamReader ReadFile = new StreamReader(x, Encoding.UTF8, false);
                        string Playlist = ReadFile.ReadToEnd();
                        char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                        string[] SplittedFase = Playlist.Split(token);
                        if (SplittedFase[4].Substring(1).Replace(Environment.NewLine, "") == "1")
                        {
                            chromiumWebBrowser1.EvaluateScriptAsync(File.ReadAllText(SplittedFase[7].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x).Directory + "\\")));
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
        public void ChangeStatus(string status) => label2.Text = status;
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
                    this.InvokeOnUiThreadIfRequired(() => Korot.Properties.Settings.Default.History += DateTime.Now.ToString("dd/MM/yy hh:mm:ss") + ";" + this.Text + ";" + (tbAddress.Text) + ";");

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
                    catch { }
                }
            }
            isLoading = e.IsLoading;
        }

        public void NewTab(string url)
        {
            anaform.Invoke(new Action(() => anaform.CreateTab(url)));
        }
        public void RefreshFavorites()
        {
            mFavorites.Items.Clear();
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
                miFavorite.Click += TestToolStripMenuItem_Click;
                mFavorites.Items.Add(miFavorite);
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
            }
            textBox2.Text = Properties.Settings.Default.Homepage;
            textBox3.Text = Properties.Settings.Default.SearchURL;
            if (Properties.Settings.Default.Homepage == "korot://newtab") { radioButton1.Enabled = true; }
            pictureBox3.BackColor = Properties.Settings.Default.BackColor;
            pictureBox4.BackColor = Properties.Settings.Default.OverlayColor;
            RefreshLangList();
            LoadLangFromFile(Properties.Settings.Default.LangFile);
            refreshThemeList();
            ChangeTheme();
            RefreshDownloadList();
            if (_Incognito) { } else { pictureBox1.Visible = false; tbAddress.Size = new Size(tbAddress.Size.Width + pictureBox1.Size.Width, tbAddress.Size.Height); }
            label18.Text = Application.ProductVersion.ToString();
            label17.Visible = Environment.Is64BitProcess;
            RefreshFavorites();
            LoadProxies();
            LoadExt();
            RefreshProfiles();
            profilenameToolStripMenuItem.Text = userName;
            label3.Text = SearchOnPage;
            label6.Text = CaseSensitive;
            showCertificateErrorsToolStripMenuItem.Text = showCertError;
            chromiumWebBrowser1.Select();
            comboBox2.SelectedIndex = Properties.Settings.Default.UpdateChannel;
            updateUCI();
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
            else if (tabControl1.SelectedTab == tabPage3) //Settings Menu
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
            this.InvokeOnUiThreadIfRequired(() => tbAddress.Text = e.Address);
            if (Properties.Settings.Default.Favorites.Contains(e.Address))
            {
                isLoadedPageFavroited = true;
                button7.Image = Properties.Resources.star_on;
            }
            else
            {
                if (Brightness(Properties.Settings.Default.BackColor) > 130) { button7.Image = Properties.Resources.star; }
                else { button7.Image = Properties.Resources.star_w; }
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
                this.InvokeOnUiThreadIfRequired(() => this.Text = e.Title);
            }
            else
            {
                this.InvokeOnUiThreadIfRequired(() => this.Text = e.Title.Substring(0, 100));
            }
            this.Parent.Invoke(new Action(() => this.Parent.Text = this.Text));
        }

        private void button5_Click(object sender, EventArgs e) { allowSwitching = true; tabControl1.SelectedTab = tabPage1; chromiumWebBrowser1.Load(Properties.Settings.Default.Homepage); }

        private void tabform_KeyDown(object sender, KeyEventArgs e)
        {
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
            else if (e.KeyCode == Keys.F && e.Control)
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    panel3.Visible = !panel3.Visible;
                    findTextBox.Text = "";
                    chromiumWebBrowser1.StopFinding(true);
                }
                else
                {
                    panel3.Visible = false;
                    findTextBox.Text = "";
                    chromiumWebBrowser1.StopFinding(true);
                }
            }
            else if (e.KeyData == Keys.Enter)
            {
                button4_Click(null, null);
            }
        }
        private static int GerekiyorsaAzalt(int defaultint, int azaltma) => defaultint > azaltma ? defaultint - 20 : defaultint;
        private static int GerekiyorsaArttır(int defaultint, int arttırma, int sınır) => defaultint + arttırma > sınır ? defaultint : defaultint + arttırma;
        private Color oldBackColor;
        private Color oldOverlayColor;
        void ChangeTheme()
        {
            if (Properties.Settings.Default.OverlayColor != oldOverlayColor) { oldOverlayColor = Properties.Settings.Default.OverlayColor; pbProgress.BackColor = Properties.Settings.Default.OverlayColor; }
            if (Properties.Settings.Default.BackColor != oldBackColor)
            {
                oldBackColor = Properties.Settings.Default.BackColor;
                button13.Image = Brightness(Properties.Settings.Default.BackColor) < 130 ? Properties.Resources.leftarrow_w : Properties.Resources.leftarrow;
                lbSettings.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                tabControl2.ActiveColor = Properties.Settings.Default.OverlayColor;
                tabControl2.BackTabColor = Properties.Settings.Default.BackColor;
                tabControl2.BorderColor = Properties.Settings.Default.BackColor;
                tabControl2.HeaderColor = Properties.Settings.Default.BackColor;
                tabControl2.HorizontalLineColor = Properties.Settings.Default.OverlayColor;
                hlvDownload.BackColor = Properties.Settings.Default.BackColor;
                hlvHistory.BackColor = Properties.Settings.Default.BackColor;
                cmsDownload.BackColor = Properties.Settings.Default.BackColor;
                cmsHistory.BackColor = Properties.Settings.Default.BackColor;
                cmsSearchEngine.BackColor = Properties.Settings.Default.BackColor;
                tabControl2.SelectedTextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
                tabControl2.TextColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black;
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
                textBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                button10.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox2.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                textBox3.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                lbLang.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                lbLang.BackColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255)) : Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10));
                panel3.BackColor = Properties.Settings.Default.BackColor;
                cmsProfiles.BackColor = Properties.Settings.Default.BackColor;
                cmsHamburger.BackColor = Properties.Settings.Default.BackColor;
                cmsPrivacy.BackColor = Properties.Settings.Default.BackColor;
                label2.BackColor = Properties.Settings.Default.BackColor;
                this.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip2.BackColor = Properties.Settings.Default.BackColor;
                contextMenuStrip1.BackColor = Properties.Settings.Default.BackColor;
                findTextBox.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                tbAddress.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsHamburger.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsProfiles.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                this.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                contextMenuStrip1.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                label2.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                panel3.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                cmsPrivacy.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                contextMenuStrip2.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                textBox4.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                if (isLoadedPageFavroited) { button7.Image = Properties.Resources.star_on; } else { button7.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.star : Properties.Resources.star_w; }
                mFavorites.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White;
                findTextBox.BackColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10)) : Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                button9.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.profiles : Properties.Resources.profiles_w;
                button1.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.leftarrow : Properties.Resources.leftarrow_w;
                button2.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.refresh : Properties.Resources.refresh_w;
                button3.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.rightarrow : Properties.Resources.rightarrow_w;
                button4.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.go : Properties.Resources.go_w;
                button5.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.home : Properties.Resources.home_w;
                button11.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.hamburger : Properties.Resources.hamburger_w;
                tbAddress.BackColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10)) : Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                textBox4.BackColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10)) : Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                button6.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.prxy : Properties.Resources.prxy_w;
                mFavorites.BackColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.FromArgb(GerekiyorsaAzalt(Properties.Settings.Default.BackColor.R, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.G, 10), GerekiyorsaAzalt(Properties.Settings.Default.BackColor.B, 10)) : Color.FromArgb(GerekiyorsaArttır(Properties.Settings.Default.BackColor.R, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.G, 10, 255), GerekiyorsaArttır(Properties.Settings.Default.BackColor.B, 10, 255));
                button8.Image = Brightness(Properties.Settings.Default.BackColor) > 130 ? Properties.Resources.ext : Properties.Resources.ext_w;
                foreach (ToolStripMenuItem x in cmsProfiles.Items) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White; }
                foreach (ToolStripMenuItem x in contextMenuStrip1.Items) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) > 130 ? Color.Black : Color.White; }
                foreach (TabPage x in tabControl2.TabPages) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black; }
                foreach (TabPage x in tabControl1.TabPages) { x.BackColor = Properties.Settings.Default.BackColor; x.ForeColor = Brightness(Properties.Settings.Default.BackColor) < 130 ? Color.White : Color.Black; }
            }
        }
        int websiteprogress;
        public void ChangeProgress(int value)
        {
            websiteprogress = value;
            pbProgress.Width = (this.Width / 100) * websiteprogress;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            onCEFTab = (tabControl1.SelectedTab == tabPage1);
            try
            {
                ChangeTheme();
                this.Parent.Text = this.Text;
            }
            catch { } //ignored
            
            RefreshTranslation();
            if (anaform.restoremedaddy == "") { restoreLastSessionToolStripMenuItem.Visible = false; } else { restoreLastSessionToolStripMenuItem.Visible = true; }
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
                button7.Image = Properties.Resources.star_on;
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

            if (noProxy) { }
            else
            {
                defaultproxyItem.Text = this.defaultproxytext;
            }
            switchToToolStripMenuItem.Text = this.switchTo;
            newProfileToolStripMenuItem.Text = this.newprofile;
            deleteThisProfileToolStripMenuItem.Text = this.deleteProfile;
            showCertificateErrorsToolStripMenuItem.Text = this.showCertError;
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
            comboBox2.SelectedIndex = Properties.Settings.Default.UpdateChannel;
            updateUCI();
        }
        private void ProfilesToolStripMenuItem_Click(object sender, EventArgs e) => this.Invoke(new Action(() => anaform.SwitchProfile(((ToolStripMenuItem)sender).Text)));

        private void NewProfileToolStripMenuItem_Click(object sender, EventArgs e) => this.Invoke(new Action(() => anaform.NewProfile()));

        private void DeleteThisProfileToolStripMenuItem_Click(object sender, EventArgs e) => this.Invoke(new Action(() => anaform.DeleteProfile(userName)));

        private void Button9_Click(object sender, EventArgs e) => cmsProfiles.Show(MousePosition);

        private void ExtensionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String fileLocation = ((ToolStripMenuItem)sender).Tag.ToString();
            StreamReader ReadFile = new StreamReader(fileLocation, Encoding.UTF8, false);
            string Playlist = ReadFile.ReadToEnd();
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            if (SplittedFase[8].Substring(1).Replace(Environment.NewLine, "") == "1")
            {
                try
                {
                    chromiumWebBrowser1.EvaluateScriptAsync(File.ReadAllText(SplittedFase[9].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\")));
                }
                catch { }
            }
            bool allowWebContent = false;
            if (SplittedFase[6].Substring(1).Replace(Environment.NewLine, "") == "1") { allowWebContent = true; }
            if (SplittedFase[7].Substring(1).Replace(Environment.NewLine, "") == "1")
            {
                frmExt formext = new frmExt(this, anaform, userName, fileLocation, SplittedFase[10].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(fileLocation).Directory + "\\"), allowWebContent);
                formext.Show();
            }
        }
        public void LoadExt()
        {
            contextMenuStrip1.Items.Clear();
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"); }

            foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\"))
            {
                if (File.Exists(x + "\\ext.kem"))
                {
                    StreamReader ReadFile = new StreamReader(x + "\\ext.kem", Encoding.UTF8, false);
                    string Playlist = ReadFile.ReadToEnd();
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
                        extItem.Image = Image.FromFile(SplittedFase[3].Substring(1).Replace(Environment.NewLine, "").Replace("[EXTFOLDER]", new FileInfo(x + "\\ext.kem").DirectoryName + " \\"));
                    }
                    ToolStripMenuItem extRunItem = new ToolStripMenuItem();
                    extItem.Click += ExtensionToolStripMenuItem_Click;
                    extItem.Tag = x + "\\ext.kem";
                    contextMenuStrip1.Items.Add(extItem);

                }
            }
            if (contextMenuStrip1.Items.Count == 0)
            {
                ToolStripMenuItem emptylol = new ToolStripMenuItem();
                emptyItem = emptylol;
                emptylol.Text = this.empty;
                emptylol.Enabled = false;
                contextMenuStrip1.Items.Add(emptylol);
            }
        }
        ToolStripMenuItem emptyItem;
        ToolStripMenuItem defaultproxyItem;
        public void LoadProxies()
        {
            contextMenuStrip2.Items.Clear();
            if (noProxy)
            {
                ToolStripMenuItem noProxyItem = new ToolStripMenuItem();
                noProxyItem.Text = "No Proxies Detected.";
                noProxyItem.Enabled = false;
                contextMenuStrip2.Items.Add(noProxyItem);
            }
            else
            {

                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\")) { Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\"); }
                // add default proxy
                ToolStripMenuItem defaultProxyMenuItem = new ToolStripMenuItem();
                defaultproxyItem = defaultProxyMenuItem;
                defaultProxyMenuItem.Text = this.defaultproxytext;
                defaultProxyMenuItem.Click += ExampleProxyToolStripMenuItem_Click;
                defaultProxyMenuItem.Tag = defaultproxyaddress;
                contextMenuStrip2.Items.Add(defaultProxyMenuItem);
                foreach (string x in Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\"))
                {
                    if (File.Exists(x + "\\proxy.kem"))
                    {
                        ToolStripMenuItem extItem = new ToolStripMenuItem();
                        extItem.Text = new DirectoryInfo(x).Name;
                        extItem.Click += ExampleProxyToolStripMenuItem_Click;
                        extItem.Tag = File.ReadAllText(x + "\\proxy.kem");
                        contextMenuStrip2.Items.Add(extItem);

                    }
                }
            }
        }
        private void Button8_Click(object sender, EventArgs e) { LoadExt(); contextMenuStrip1.Show(MousePosition); }

        private void TmrSlower_Tick(object sender, EventArgs e) => RefreshFavorites();


        public void FrmCEF_SizeChanged(object sender, EventArgs e) => pbProgress.Width = (this.Width / 100) * websiteprogress;

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(findTextBox.Text)) & panel3.Visible)
            {
                chromiumWebBrowser1.Find(0, findTextBox.Text, false, haltroySwitch1.Checked, false);
            }
        }

        private void Label4_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            findTextBox.Text = "";
            chromiumWebBrowser1.StopFinding(true);
        }

        private void Panel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) => tabform_KeyDown(panel1, new KeyEventArgs(e.KeyData));

        private void ExampleProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SetProxy(chromiumWebBrowser1, ((ToolStripMenuItem)sender).Tag.ToString());
            }
            catch { }
            chromiumWebBrowser1.Reload();
        }
        public string GetElementValueByID(ChromiumWebBrowser browser, string elementID)
        {
            bool isError = false;
            string result = null;
            try
            {
                string script = string.Format("document.getElementById(\"" + elementID + "\").value;");
                browser.EvaluateScriptAsync(script).ContinueWith(x =>
                {
                    var response = x.Result;

                    if (response.Success && response.Result != null)
                    {
                        result = response.Result.ToString();
                    }
                    else
                    {
                        result = null;
                    }
                });
                if (isError)
                {
                    isError = true;
                    throw new NullReferenceException("[ERROR] [KOROT:frmCEF] GetElementValueByID :\" browser: " + browser.Name.ToString() + " elementID: " + elementID + "\"");
                }
                else { return result; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button6_Click(object sender, EventArgs e) => contextMenuStrip2.Show(MousePosition);

        private void pictureBox2_Click(object sender, EventArgs e) => cmsPrivacy.Show(pictureBox2, 0, pictureBox2.Size.Height);

        private void xToolStripMenuItem_Click(object sender, EventArgs e) => cmsPrivacy.Close();

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
            cmsHamburger.Show(button11, 0, 0);
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
            if (allowSwitching == false) { e.Cancel = true; } else { panel3.Visible = false; findTextBox.Text = ""; chromiumWebBrowser1.StopFinding(true); e.Cancel = false; allowSwitching = false; }
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

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
        }
        void updateUCI()
        {
            if (Properties.Settings.Default.UpdateChannel == 0) //alpha
            {
                label23.Text = UCAlpha;
            }
            else if (Properties.Settings.Default.UpdateChannel == 1) //Beta
            {
                label23.Text = UCBeta;
            }
            else if (Properties.Settings.Default.UpdateChannel == 2) //gama
            {
                label23.Text = UCGama;
            }
            else if (Properties.Settings.Default.UpdateChannel == 3) //delta
            {
                label23.Text = UCDelta;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox2.Text == "Beta"){Properties.Settings.Default.UpdateChannel = 0;}if (comboBox2.Text == "Gama"){Properties.Settings.Default.UpdateChannel = 1;}if (comboBox2.Text == "Delta"){Properties.Settings.Default.UpdateChannel = 2;}
            Properties.Settings.Default.UpdateChannel = comboBox2.SelectedIndex;
            updateUCI();
        }

        private void tbSetting_Click(object sender, EventArgs e)
        {

        }
    }
}
