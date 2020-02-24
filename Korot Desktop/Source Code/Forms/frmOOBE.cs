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
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmOOBE : Form
    {
        public frmOOBE()
        {
            InitializeComponent();
        }
        bool allowSwitch = false;
        public string customSearchNote = "(Note: Searched text will be put after the url)";
        public string customSearchMessage = "Write Custom Search Url";
        string profilePath;
        public string Yes = "Yes";
        public string No = "No";
        public string closeMessage = "The installation is not completed yet. Do you still want to close this?";
        public string OK = "OK";
        public string Cancel = "Cancel";
        public bool isDarkMode;

        void ResetSettings()
        {
            Properties.Settings.Default.Homepage = "korot://newtab";
            Properties.Settings.Default.History = "";
            Properties.Settings.Default.WindowSizeH = 0;
            Properties.Settings.Default.WindowSizeW = 0;
            Properties.Settings.Default.WindowPosX = 0;
            Properties.Settings.Default.WindowPosY = 0;
            Properties.Settings.Default.Favorites = "";
            Properties.Settings.Default.SearchURL = "https://www.google.com/search?q=";
            Properties.Settings.Default.downloadOpen = false;
            Properties.Settings.Default.LangFile = "";
            Properties.Settings.Default.BackColor = Color.White;
            Properties.Settings.Default.OverlayColor = Color.DodgerBlue;
            Properties.Settings.Default.DowloadHistory = "";
            Properties.Settings.Default.LastUser = "";
            Properties.Settings.Default.ThemeFile = "";
            Properties.Settings.Default.BackStyle = "BACKCOLOR";
            Properties.Settings.Default.LastSessionURIs = "";
            Properties.Settings.Default.DoNotTrack = false;
            Properties.Settings.Default.BStyleLayout = 0;
            Properties.Settings.Default.CookieDisallowList.Clear();
            Properties.Settings.Default.ThemeName = "";
            Properties.Settings.Default.ThemeAuthor = "";
            Properties.Settings.Default.rememberLastProxy = false;
            Properties.Settings.Default.LastProxy = "";
            Properties.Settings.Default.DownloadFolder = "";
            Properties.Settings.Default.useDownloadFolder = false;
            Properties.Settings.Default.StartupURL = "korot://newtab";
            Properties.Settings.Default.newTabColor = 2;
            Properties.Settings.Default.closeColor = 2;
            Properties.Settings.Default.showFav = true;
            Properties.Settings.Default.allowUnknownResources = false;
            Properties.Settings.Default.registeredExtensions.Clear();
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        { if (allowSwitch) { allowSwitch = false; e.Cancel = false; } else { e.Cancel = true; } }
        private void DetectDarkMode()
        {
            RegistryKey baseRegistryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
            RegistryKey subRegistryKey = baseRegistryKey.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", RegistryKeyPermissionCheck.ReadSubTree);
            if (subRegistryKey != null)
            {
                object value64 = subRegistryKey.GetValue("AppsUseLightTheme");
                if (value64 != null)
                {
                    baseRegistryKey.Close();
                    subRegistryKey.Close();
                    isDarkMode = value64.ToString() == "0" ? true : false;
                }
                subRegistryKey.Close();
            }
            baseRegistryKey.Close();
        }
        private bool DetectIsDarkMode()
        {
            return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", RegistryKeyPermissionCheck.ReadSubTree).GetValue("AppsUseLightTheme").ToString() == "0" ? true : false;
        }
        public void RefreshLangList()
        {
            int savedValue = lbLang.SelectedIndex;
            lbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.lang", SearchOption.TopDirectoryOnly))
            {
                lbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }
            try { lbLang.SelectedIndex = savedValue; } catch { }
        }
        private void lbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".lang"))
            {
                btContinue2.Visible = false;
            }
            else
            {
                Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang";
                string Playlist = FileSystem2.ReadFile(Properties.Settings.Default.LangFile, Encoding.UTF8);
                char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                string[] SplittedFase = Playlist.Split(token);
                this.Text = SplittedFase[142].Substring(1).Replace(Environment.NewLine, "");
                btContinue.Text = SplittedFase[143].Substring(1).Replace(Environment.NewLine, "");
                btContinue1.Text = SplittedFase[143].Substring(1).Replace(Environment.NewLine, "");
                btContinue2.Text = SplittedFase[143].Substring(1).Replace(Environment.NewLine, "");
                btBack.Text = SplittedFase[150].Substring(1).Replace(Environment.NewLine, "");
                btBack1.Text = SplittedFase[150].Substring(1).Replace(Environment.NewLine, "");
                btBack2.Text = SplittedFase[150].Substring(1).Replace(Environment.NewLine, "");
                btFinal.Text = SplittedFase[156].Substring(1).Replace(Environment.NewLine, "");
                lbInfoTheme.Text = SplittedFase[152].Substring(1).Replace(Environment.NewLine, "");
                lbInfoSettings.Text = SplittedFase[152].Substring(1).Replace(Environment.NewLine, "");
                lbWelcome.Text = SplittedFase[144].Substring(1).Replace(Environment.NewLine, "");
                lbProfile.Text = SplittedFase[145].Substring(1).Replace(Environment.NewLine, "");
                lbNotContain.Text = SplittedFase[146].Substring(1).Replace(Environment.NewLine, "");
                lbProfileInfo.Text = SplittedFase[147].Substring(1).Replace(Environment.NewLine, "");
                lbWelcomeSetting.Text = SplittedFase[148].Substring(1).Replace(Environment.NewLine, "");
                lbWelcomeTheme.Text = SplittedFase[149].Substring(1).Replace(Environment.NewLine, "");
                lbHomePage.Text = SplittedFase[11].Substring(1).Replace(Environment.NewLine, "");
                lbNewTab.Text = SplittedFase[151].Substring(1).Replace(Environment.NewLine, "");
                rbKorotL.Text = SplittedFase[153].Substring(1).Replace(Environment.NewLine, "");
                rbKorotD.Text = SplittedFase[154].Substring(1).Replace(Environment.NewLine, "");
                rbCustom.Text = SplittedFase[155].Substring(1).Replace(Environment.NewLine, "");
                lbBack.Text = SplittedFase[39].Substring(1).Replace(Environment.NewLine, "");
                lbOveral.Text = SplittedFase[40].Substring(1).Replace(Environment.NewLine, "");
                lbSE.Text = SplittedFase[12].Substring(1).Replace(Environment.NewLine, "");
                lbDNT.Text = SplittedFase[129].Substring(1).Replace(Environment.NewLine, "");
                Yes = SplittedFase[83].Substring(1).Replace(Environment.NewLine, "");
                No = SplittedFase[84].Substring(1).Replace(Environment.NewLine, "");
                OK = SplittedFase[85].Substring(1).Replace(Environment.NewLine, "");
                Cancel = SplittedFase[86].Substring(1).Replace(Environment.NewLine, "");
                customSearchMessage = SplittedFase[5].Substring(1).Replace(Environment.NewLine, "");
                customSearchNote = SplittedFase[6].Substring(1).Replace(Environment.NewLine, "");
                customToolStripMenuItem.Text = SplittedFase[14].Substring(1).Replace(Environment.NewLine, "");
                closeMessage = SplittedFase[157].Substring(1).Replace(Environment.NewLine, "");
                lbSettings.Text = SplittedFase[9].Substring(1).Replace(Environment.NewLine, "");
                lbThemes.Text = SplittedFase[13].Substring(1).Replace(Environment.NewLine, "");
                textBox3.Width = this.Width - (50 + lbSE.Width);
                textBox3.Location = new Point(lbSE.Width + lbSE.Location.X + 5, textBox3.Location.Y);
                haltroySwitch4.Location = new Point(lbDNT.Location.X + 5 + lbDNT.Width, haltroySwitch4.Location.Y);
                pbBack.Location = new Point(lbBack.Location.X + lbBack.Width + 5, pbBack.Location.Y);
                pbOverlay.Location = new Point(lbOveral.Location.X + lbOveral.Width + 5, pbOverlay.Location.Y);
                btContinue2.Visible = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("/", "").Replace("\\", "").Replace("\"", "").Replace(":", "").Replace("?", "").Replace("*", "").Replace("<", "").Replace(">", "").Replace("|", "");
            if (textBox1.Text.Length < 3) { btContinue.Visible = false; } else { btContinue.Visible = true; }
        }
        private void haltroySwitch1_CheckedChanged(object sender, EventArgs e)
        {
            if (!haltroySwitch1.Checked && !haltroySwitch2.Checked)
            {
                btContinue.Text = btFinal.Text;
            }
            else
            {
                btContinue.Text = btContinue2.Text;
            }
        }

        private void haltroySwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (!haltroySwitch1.Checked && !haltroySwitch2.Checked)
            {
                btContinue.Text = btFinal.Text;
            }
            else
            {
                btContinue.Text = btContinue2.Text;
            }
            if (!haltroySwitch2.Checked)
            {
                btContinue1.Text = btFinal.Text;
            }
            else
            {
                btContinue1.Text = btContinue2.Text;
            }
        }
        private void RefreshTheme()
        {
            this.BackColor = isDarkMode ? Color.Black : Color.White;
            this.ForeColor = isDarkMode ? Color.White : Color.Black;
            cmsSearchEngine.BackColor = isDarkMode ? Color.Black : Color.White;
            cmsSearchEngine.ForeColor = isDarkMode ? Color.White : Color.Black;
            foreach (TabPage x in tabControl1.TabPages)
            {
                x.BackColor = isDarkMode ? Color.Black : Color.White;
                x.ForeColor = isDarkMode ? Color.White : Color.Black;
            }
            textBox1.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btContinue.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btContinue1.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btContinue2.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btFinal.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btBack.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btBack1.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            btBack2.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            lbLang.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            textBox2.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            textBox3.BackColor = isDarkMode ? Color.FromArgb(255, 20, 20, 20) : Color.FromArgb(255, 245, 245, 245);
            textBox1.ForeColor = isDarkMode ? Color.White : Color.Black;
            btContinue.ForeColor = isDarkMode ? Color.White : Color.Black;
            btContinue1.ForeColor = isDarkMode ? Color.White : Color.Black;
            btContinue2.ForeColor = isDarkMode ? Color.White : Color.Black;
            btFinal.ForeColor = isDarkMode ? Color.White : Color.Black;
            btBack.ForeColor = isDarkMode ? Color.White : Color.Black;
            btBack1.ForeColor = isDarkMode ? Color.White : Color.Black;
            btBack2.ForeColor = isDarkMode ? Color.White : Color.Black;
            lbLang.ForeColor = isDarkMode ? Color.White : Color.Black;
            textBox2.ForeColor = isDarkMode ? Color.White : Color.Black;
            textBox3.ForeColor = isDarkMode ? Color.White : Color.Black;
        }
        private void frmOOBE_Load(object sender, EventArgs e)
        {
            try { DetectDarkMode(); } catch { isDarkMode = false; }
            RefreshTheme();
            rbKorotD.Checked = isDarkMode;
            rbKorotL.Checked = !isDarkMode;

            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\", true);
            }
            ResetSettings();
            RefreshLangList();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            allowSwitch = true;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tabPage1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = haltroySwitch1.Checked ? tabPage3 : tabPage2;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (haltroySwitch1.Checked)
            {
                allowSwitch = true;
                tabControl1.SelectedTab = tabPage3;
            }
            else
            {
                if (haltroySwitch2.Checked)
                {
                    allowSwitch = true;
                    tabControl1.SelectedTab = tabPage4;
                }
                else
                {
                    button2_Click(btContinue, e);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (haltroySwitch2.Checked)
            {
                allowSwitch = true;
                tabControl1.SelectedTab = tabPage4;
            }
            else
            {
                button2_Click(btContinue1, e);
            }
        }
        public bool IsDirectoryEmpty(string path)
        {
            try
            {
                if (Directory.GetDirectories(path).Length > 0) { return false; } else { return true; }
            }
            catch (Exception)
            {
                return true;
            }
        }
        bool allowClose = false;
        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastUser = textBox1.Text;
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\");
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
            Directory.CreateDirectory(profilePath);
            Tools.createThemes();
            Tools.SaveSettings(profilePath + "settings.ksf",
                    profilePath + "history.ksf",
                    profilePath + "favorites.ksf",
                    profilePath + "download.ksf",
                    profilePath + "cookieDisallow.ksf");
            Properties.Settings.Default.Save();
            allowClose = true;
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKorotL.Checked)
            {
                panel1.Visible = false;
                rbKorotD.Checked = false;
                rbCustom.Checked = false;
                Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf";
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKorotD.Checked)
            {
                panel1.Visible = false;
                rbKorotL.Checked = false;
                rbCustom.Checked = false;
                Properties.Settings.Default.ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCustom.Checked)
            {
                panel1.Visible = true;
                rbKorotD.Checked = false;
                rbKorotL.Checked = false;
                Properties.Settings.Default.ThemeFile = "";
            }
        }

        private void haltroySwitch3_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Visible = !haltroySwitch3.Checked;
            if (haltroySwitch3.Checked)
            {
                textBox2.Text = "korot://newtab";
                Properties.Settings.Default.Homepage = "korot://newtab";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Homepage = textBox2.Text;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            cmsSearchEngine.Show(textBox3, 0, 0);
        }
        private void SearchEngineSelection_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SearchURL = ((ToolStripMenuItem)sender).Tag.ToString();
            textBox3.Text = Properties.Settings.Default.SearchURL;
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about\:\/\/)|(?:about\:\/\/)|(?:file\:\/\/)|(?:https\:\/\/)|(?:korot\:\/\/)|(?:http:\/\/)|(?:\:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex Rgx2 = new Regex(@"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx2.IsMatch(s) || Rgx.IsMatch(s);
        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputb = new HaltroyFramework.HaltroyInputBox(
                customSearchNote,
                customSearchMessage,
                this.Icon,
                "",
                isDarkMode ? Color.Black : Color.White,
                pbOverlay.BackColor,
                OK,
                Cancel,
                400,
                150);
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

        private void pbBack_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog();
            colorpicker.AnyColor = true;
            colorpicker.AllowFullOpen = true;
            colorpicker.FullOpen = true;
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbBack.BackColor = colorpicker.Color;
                Properties.Settings.Default.BackColor = colorpicker.Color;
            }
        }

        private void pbOverlay_Click(object sender, EventArgs e)
        {
            ColorDialog colorpicker = new ColorDialog();
            colorpicker.AnyColor = true;
            colorpicker.AllowFullOpen = true;
            colorpicker.FullOpen = true;
            if (colorpicker.ShowDialog() == DialogResult.OK)
            {
                pbOverlay.BackColor = colorpicker.Color;
                Properties.Settings.Default.OverlayColor = colorpicker.Color;
            }
        }

        private void haltroySwitch4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DoNotTrack = haltroySwitch4.Checked;
        }

        private void frmOOBE_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                HaltroyFramework.HaltroyMsgBox msgBox = new HaltroyFramework.HaltroyMsgBox(this.Text, closeMessage, this.Icon, MessageBoxButtons.YesNo, isDarkMode ? Color.Black : Color.White, Yes, No, OK, Cancel, 390, 140);
                if (msgBox.ShowDialog() == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else { e.Cancel = false; }
        }

        private void lbLang_DoubleClick(object sender, EventArgs e)
        {
            if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".lang"))
            {

            }
            else
            {
                button3_Click(sender, e);
            }
        }

        private void frmOOBE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                isDarkMode = true;
                timer1.Stop();
                RefreshTheme();
            }
            else if (e.KeyData == Keys.F3)
            {
                isDarkMode = false;
                timer1.Stop();
                RefreshTheme();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                isDarkMode = DetectIsDarkMode();
                RefreshTheme();
            }
            catch
            {
                isDarkMode = false;
                RefreshTheme();
            }
        }
    }
}
