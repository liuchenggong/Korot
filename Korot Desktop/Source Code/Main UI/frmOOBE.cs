/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmOOBE : Form
    {
        private readonly Settings Settings;

        public frmOOBE(Settings settings)
        {
            Settings = settings;
            InitializeComponent();
            foreach (Control x in Controls)
            {
                try { x.Font = new Font("Ubuntu", x.Font.Size, x.Font.Style); } catch { continue; }
            }
        }

        private bool allowSwitch = false;
        private string profilePath;
        public string Yes = "Yes";
        public string No = "No";
        public string closeMessage = "The installation is not completed yet. Do you still want to close this?";
        public string OK = "OK";
        public string Cancel = "Cancel";

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        { if (allowSwitch) { allowSwitch = false; e.Cancel = false; } else { e.Cancel = true; } }

        public void RefreshLangList()
        {
            int savedValue = lbLang.SelectedIndex;
            lbLang.Items.Clear();
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.klf", SearchOption.TopDirectoryOnly))
            {
                lbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }
            try { lbLang.SelectedIndex = savedValue; } catch { }
        }

        private int switchedTimes = 0;

        private void lbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLang.SelectedItem != null)
            {
                if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".klf"))
                {
                    btContinue2.Visible = false;
                }
                else
                {
                    switchedTimes++;
                    Settings.LanguageSystem.ReadFromFile(Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".klf", true);
                    checkBox1.Text = Settings.LanguageSystem.GetItemText("OOBEThemeWizard");
                    Text = Settings.LanguageSystem.GetItemText("OOBETitle");
                    btContinue.Text = Settings.LanguageSystem.GetItemText("OOBEContinue");
                    btContinue2.Text = Settings.LanguageSystem.GetItemText("OOBEContinue");
                    btBack.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
                    btBack1.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
                    btFinish.Text = Settings.LanguageSystem.GetItemText("OOBEButtonFinish");
                    lbTip1.Text = Settings.LanguageSystem.GetItemText("OOBEExtensionInfo").Replace("[NEWLINE]", Environment.NewLine);
                    lbWelcome.Text = Settings.LanguageSystem.GetItemText("OOBEWelcome");
                    lbProfile.Text = Settings.LanguageSystem.GetItemText("OOBEProfile");
                    lbNotContain.Text = Settings.LanguageSystem.GetItemText("OOBENotContain");
                    lbProfileInfo.Text = Settings.LanguageSystem.GetItemText("OOBEProfileInfo");
                    tbTip2.Text = Settings.LanguageSystem.GetItemText("Themes");
                    tbTip1.Text = Settings.LanguageSystem.GetItemText("Extensions");
                    lbTipTitle.Text = Settings.LanguageSystem.GetItemText("Extensions");
                    lbTip2.Text = Settings.LanguageSystem.GetItemText("OOBEThemeInfo").Replace("[NEWLINE]", Environment.NewLine);
                    lbTip3.Text = Settings.LanguageSystem.GetItemText("OOBEProfilesInfo").Replace("[NEWLINE]", Environment.NewLine);
                    tbTipF.Text = Settings.LanguageSystem.GetItemText("OOBEBestWishes");
                    tbTip3.Text = Settings.LanguageSystem.GetItemText("OOBEProfilesTitle");
                    lbTipF.Text = Settings.LanguageSystem.GetItemText("YoureGoodToGo").Replace("[NEWLINE]", Environment.NewLine);
                    lbChooseLang.Text = Settings.LanguageSystem.GetItemText("ChooseALanguage");
                    lbCont.Text = Settings.LanguageSystem.GetItemText("OOBEContinueInfo");
                    lbArrowKey.Text = Settings.LanguageSystem.GetItemText("OOBEArrowKeys");
                    lbContinueBack.Text = Settings.LanguageSystem.GetItemText("OOBEContinueBack").Replace("[NEWLINE]", Environment.NewLine);
                    lbFinishBack.Text = Settings.LanguageSystem.GetItemText("OOBEFinishBack").Replace("[NEWLINE]", Environment.NewLine);
                    Yes = Settings.LanguageSystem.GetItemText("Yes");
                    No = Settings.LanguageSystem.GetItemText("No");
                    OK = Settings.LanguageSystem.GetItemText("OK");
                    Cancel = Settings.LanguageSystem.GetItemText("Cancel");
                    closeMessage = Settings.LanguageSystem.GetItemText("OOBECloseMessage");
                    lbBanned.Location = new Point(lbNotContain.Location.X + 5 + lbNotContain.Width, lbNotContain.Location.Y);
                    btContinue2.Visible = true;
                }
            }
            else
            {
                btContinue2.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("/", "").Replace("\\", "").Replace("\"", "").Replace(":", "").Replace("?", "").Replace("*", "").Replace("<", "").Replace(">", "").Replace("|", "");
            if (textBox1.Text.Length < 3) { btContinue.Visible = false; } else { btContinue.Visible = true; }
        }

        private void frmOOBE_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\"))
            {
                Program.RemoveDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\", false);
            }
            RefreshLangList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tmrLang.Stop();
            tabControl2.SelectedTab = tbTip1;
            allowSwitch = true;
            tabControl1.SelectedTab = tabPage2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tabPage1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tabPage3;
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

        private bool allowClose = false;

        private void button2_Click(object sender, EventArgs e)
        {
            SafeFileSettingOrganizedClass.LastUser = textBox1.Text;
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Profiles\\";
            Directory.CreateDirectory(profilePath);
            KorotTools.createFolders();
            KorotTools.createThemes();
            Settings.ProfileName = textBox1.Text;
            Settings.Save();
            if (checkBox1.Checked)
            {
                Hide();
                frmThemeWizard wizard = new frmThemeWizard(Settings);
                wizard.ShowDialog();
            }
            allowClose = true;
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void frmOOBE_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                HTAlt.WinForms.HTMsgBox msgBox = new HTAlt.WinForms.HTMsgBox(Text, closeMessage, new HTAlt.WinForms.HTDialogBoxContext(MessageBoxButtons.YesNo)) { Yes = Yes, No = No, OK = OK, Cancel = Cancel, AutoForeColor = false, ForeColor = Settings.Theme.ForeColor, BackColor = Settings.Theme.BackColor, Icon = Icon };
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
            if (lbLang.SelectedItem != null)
            {
                if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".klf"))
                {
                }
                else
                {
                    lbLang_SelectedIndexChanged(sender, e);
                    button3_Click(sender, e);
                    tmrLang.Stop();
                }
            }
        }

        private void pbPrev_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab != tbTip1)
            {
                if (tabControl2.SelectedTab == tbTip2)
                {
                    tabControl2.SelectedTab = tbTip1;
                }
                else if (tabControl2.SelectedTab == tbTip3)
                {
                    tabControl2.SelectedTab = tbTip2;
                }
                else if (tabControl2.SelectedTab == tbTipF)
                {
                    tabControl2.SelectedTab = tbTip3;
                }
            }
            pbNext.Visible = tabControl2.SelectedIndex != (tabControl2.TabCount - 1);
            pbPrev.Visible = tabControl2.SelectedIndex != 0;
            btFinish.Visible = tabControl2.SelectedTab == tbTipF;
            lbTipTitle.Text = tabControl2.SelectedTab.Text;
        }

        private void pbNext_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab != tbTipF)
            {
                if (tabControl2.SelectedTab == tbTip1)
                {
                    tabControl2.SelectedTab = tbTip2;
                }
                else if (tabControl2.SelectedTab == tbTip2)
                {
                    tabControl2.SelectedTab = tbTip3;
                }
                else if (tabControl2.SelectedTab == tbTip3)
                {
                    tabControl2.SelectedTab = tbTipF;
                }
            }
            lbTipTitle.Text = tabControl2.SelectedTab.Text;
            pbNext.Visible = tabControl2.SelectedIndex != (tabControl2.TabCount - 1);
            pbPrev.Visible = tabControl2.SelectedIndex != 0;
            btFinish.Visible = tabControl2.SelectedTab == tbTipF;
        }

        private void lbLang_Click(object sender, EventArgs e)
        {
            if (lbLang.SelectedItem != null)
            {
                if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".klf"))
                {
                }
                else
                {
                    lbLang_SelectedIndexChanged(sender, e);
                    tmrLang.Stop();
                }
            }
        }

        private void tmrLang_Tick(object sender, EventArgs e)
        {
            if (switchedTimes > 19)
            {
                Random rn = new Random();
                int randomgen = rn.Next(0, lbLang.Items.Count - 1);
                lbLang.SelectedIndex = randomgen;
                return;
            }
            lbLang.SelectedIndex = lbLang.SelectedIndex == lbLang.Items.Count - 1 ? 0 : lbLang.SelectedIndex + 1;
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTipTitle.Text = tabControl2.SelectedTab.Text;
            pbNext.Visible = tabControl2.SelectedIndex != (tabControl2.TabCount - 1);
            pbPrev.Visible = tabControl2.SelectedIndex != 0;
            btFinish.Visible = tabControl2.SelectedTab == tbTipF;
        }
    }
}