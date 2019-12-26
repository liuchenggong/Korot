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
        public string customSearchNote;
        public string customSearchMessage;
        string profilePath;
        public string OK;
        public string Cancel;

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        { if (allowSwitch) { allowSwitch = false; e.Cancel = false; } else { e.Cancel = true; } }
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
            if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1)
            {
                button3.Visible = false;
            }
            else
            {
                button3.Visible = true;
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
                btContinue.Text = button3.Text;
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
                btContinue.Text = button3.Text;
            }
            if (!haltroySwitch2.Checked)
            {
                btContinue1.Text = btFinal.Text;
            }
            else
            {
                btContinue1.Text = button3.Text;
            }
        }

        private void frmOOBE_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\", true);
            }
            RefreshLangList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LangFile = Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang";
            string Playlist = FileSystem2.ReadFile(Properties.Settings.Default.LangFile, Encoding.UTF8);
            char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
            string[] SplittedFase = Playlist.Split(token);
            this.Text = SplittedFase[148].Substring(1).Replace(Environment.NewLine, "");
            btContinue.Text = SplittedFase[149].Substring(1).Replace(Environment.NewLine, "");
            btContinue1.Text = SplittedFase[149].Substring(1).Replace(Environment.NewLine, "");
            button3.Text = SplittedFase[149].Substring(1).Replace(Environment.NewLine, "");
            btBack.Text = SplittedFase[156].Substring(1).Replace(Environment.NewLine, "");
            btBack1.Text = SplittedFase[156].Substring(1).Replace(Environment.NewLine, "");
            btBack2.Text = SplittedFase[156].Substring(1).Replace(Environment.NewLine, "");
            btFinal.Text = SplittedFase[162].Substring(1).Replace(Environment.NewLine, "");
            lbInfoTheme.Text = SplittedFase[158].Substring(1).Replace(Environment.NewLine, "");
            lbInfoSettings.Text = SplittedFase[158].Substring(1).Replace(Environment.NewLine, "");
            lbWelcome.Text = SplittedFase[150].Substring(1).Replace(Environment.NewLine, "");
            lbProfile.Text = SplittedFase[151].Substring(1).Replace(Environment.NewLine, "");
            lbNotContain.Text = SplittedFase[152].Substring(1).Replace(Environment.NewLine, "");
            lbProfileInfo.Text = SplittedFase[153].Substring(1).Replace(Environment.NewLine, "");
            lbWelcomeSetting.Text = SplittedFase[154].Substring(1).Replace(Environment.NewLine, "");
            lbWelcomeTheme.Text = SplittedFase[155].Substring(1).Replace(Environment.NewLine, "");
            lbHomePage.Text = SplittedFase[11].Substring(1).Replace(Environment.NewLine, "");
            lbNewTab.Text = SplittedFase[157].Substring(1).Replace(Environment.NewLine, "");
            rbKorotL.Text = SplittedFase[159].Substring(1).Replace(Environment.NewLine, "");
            rbKorotD.Text = SplittedFase[160].Substring(1).Replace(Environment.NewLine, "");
            rbCustom.Text = SplittedFase[161].Substring(1).Replace(Environment.NewLine, "");
            lbBack.Text = SplittedFase[39].Substring(1).Replace(Environment.NewLine, "");
            lbOveral.Text = SplittedFase[40].Substring(1).Replace(Environment.NewLine, "");
            lbSE.Text = SplittedFase[12].Substring(1).Replace(Environment.NewLine, "");
            lbDNT.Text = SplittedFase[135].Substring(1).Replace(Environment.NewLine, "");
            customToolStripMenuItem.Text = SplittedFase[14].Substring(1).Replace(Environment.NewLine, "");
            textBox3.Width = this.Width - (50 + lbSE.Width);
            textBox3.Location = new Point(lbSE.Width + lbSE.Location.X + 5, textBox3.Location.Y);
            haltroySwitch4.Location = new Point(lbDNT.Location.X + 5 + lbDNT.Width, haltroySwitch4.Location.Y);
            pbBack.Location = new Point(lbBack.Location.X + lbBack.Width + 5, pbBack.Location.Y);
            pbOverlay.Location = new Point(lbOveral.Location.X + lbOveral.Width + 5, pbOverlay.Location.Y);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.LastUser = textBox1.Text;
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Extensions\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Logs\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Proxies\\");
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\");
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
            Directory.CreateDirectory(profilePath);
            FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Light.ktf",
                                "255" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "30" + Environment.NewLine +
                                "144" + Environment.NewLine +
                                "255" + Environment.NewLine +
                                "BACKCOLOR" + Environment.NewLine +
                                "0", Encoding.UTF8);
           FileSystem2.WriteFile(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Themes\\Korot Dark.ktf",
                                 "0" + Environment.NewLine +
                                 "0" + Environment.NewLine +
                                 "0" + Environment.NewLine +
                                 "30" + Environment.NewLine +
                                 "144" + Environment.NewLine +
                                 "255" + Environment.NewLine +
                                 "BACKCOLOR" + Environment.NewLine +
                                 "0", Encoding.UTF8);
            SaveSettings(profilePath + "settings.ksf",
                    profilePath + "history.ksf",
                    profilePath + "favorites.ksf",
                    profilePath + "download.ksf");
            Properties.Settings.Default.Save();
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }
        void SaveSettings(string settingFile, string historyFile, string favoritesFile, string downloadHistory)
        {
            try
            {
                // Settings
                string settingsString = Properties.Settings.Default.Homepage + Environment.NewLine +
                Properties.Settings.Default.SearchURL + Environment.NewLine +
                (Properties.Settings.Default.downloadOpen ? "1" : "0") + Environment.NewLine +
                (Properties.Settings.Default.downloadClose ? "1" : "0") + Environment.NewLine +
                Properties.Settings.Default.ThemeFile + Environment.NewLine +
                (Properties.Settings.Default.DoNotTrack ? "1" : "0") + Environment.NewLine;
                FileSystem2.WriteFile(settingFile, settingsString, Encoding.UTF8);
                // History
                FileSystem2.WriteFile(historyFile, Properties.Settings.Default.History, Encoding.UTF8);
                // Favorites
                FileSystem2.WriteFile(favoritesFile, Properties.Settings.Default.Favorites, Encoding.UTF8);

                // Download
                FileSystem2.WriteFile(downloadHistory, Properties.Settings.Default.DowloadHistory, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at saving settings(" + settingFile + ") : " + ex.ToString());
            }
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
            string Pattern = @"^(?:about)|(?:about)|(?:file)|(?:korot)|(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s);
        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HaltroyFramework.HaltroyInputBox inputb = new HaltroyFramework.HaltroyInputBox(customSearchNote, customSearchMessage, this.Icon, Properties.Settings.Default.SearchURL, Properties.Settings.Default.BackColor, Properties.Settings.Default.OverlayColor, OK, Cancel, 400, 150);
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
    }
}
