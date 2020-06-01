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
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmOOBE : Form
    {
        Settings Settings;
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
        public string customSearchNote = "(Note: Searched text will be put after the url)";
        public string customSearchMessage = "Write Custom Search Url";
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
            foreach (string foundfile in Directory.GetFiles(Application.StartupPath + "//Lang//", "*.lang", SearchOption.TopDirectoryOnly))
            {
                lbLang.Items.Add(Path.GetFileNameWithoutExtension(foundfile));
            }
            try { lbLang.SelectedIndex = savedValue; } catch { }
        }
        private void lbLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbLang.SelectedItem != null)
            {
                if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".lang"))
                {
                    btContinue2.Visible = false;
                }
                else
                {
                    Settings.LanguageFile = Application.StartupPath + "\\Lang\\" + lbLang.SelectedItem.ToString() + ".lang";
                    string Playlist = HTAlt.Tools.ReadFile(Settings.LanguageFile, Encoding.UTF8);
                    char[] token = new char[] { Environment.NewLine.ToCharArray()[0] };
                    string[] SplittedFase = Playlist.Split(token);
                    Text = SplittedFase[143].Substring(1).Replace(Environment.NewLine, "");
                    btContinue.ButtonText = SplittedFase[144].Substring(1).Replace(Environment.NewLine, "");
                    btContinue2.ButtonText = SplittedFase[144].Substring(1).Replace(Environment.NewLine, "");
                    btBack.ButtonText = SplittedFase[151].Substring(1).Replace(Environment.NewLine, "");
                    btBack1.ButtonText = SplittedFase[151].Substring(1).Replace(Environment.NewLine, "");
                    btFinish.ButtonText = SplittedFase[157].Substring(1).Replace(Environment.NewLine, "");
                    lbTip1.Text = SplittedFase[153].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                    lbWelcome.Text = SplittedFase[145].Substring(1).Replace(Environment.NewLine, "");
                    lbProfile.Text = SplittedFase[146].Substring(1).Replace(Environment.NewLine, "");
                    lbNotContain.Text = SplittedFase[147].Substring(1).Replace(Environment.NewLine, "");
                    lbProfileInfo.Text = SplittedFase[148].Substring(1).Replace(Environment.NewLine, "");
                    tbTip2.Text = SplittedFase[95].Substring(1).Replace(Environment.NewLine, "");
                    tbTip1.Text = SplittedFase[182].Substring(1).Replace(Environment.NewLine, "");
                    lbTipTitle.Text = SplittedFase[182].Substring(1).Replace(Environment.NewLine, "");
                    lbTip2.Text = SplittedFase[149].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                    lbTip3.Text = SplittedFase[150].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                    tbTipF.Text = SplittedFase[154].Substring(1).Replace(Environment.NewLine, "");
                    tbTip3.Text = SplittedFase[155].Substring(1).Replace(Environment.NewLine, "");
                    lbTipF.Text = SplittedFase[267].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                    label2.Text = SplittedFase[269].Substring(1).Replace(Environment.NewLine, "");
                    lbCont.Text = SplittedFase[268].Substring(1).Replace(Environment.NewLine, "");
                    lbArrowKey.Text = SplittedFase[267].Substring(1).Replace(Environment.NewLine, "");
                    lbContinueBack.Text = SplittedFase[156].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                    lbFinishBack.Text = SplittedFase[152].Substring(1).Replace(Environment.NewLine, "").Replace("[NEWLINE]", Environment.NewLine);
                    Yes = SplittedFase[84].Substring(1).Replace(Environment.NewLine, "");
                    No = SplittedFase[85].Substring(1).Replace(Environment.NewLine, "");
                    OK = SplittedFase[86].Substring(1).Replace(Environment.NewLine, "");
                    Cancel = SplittedFase[87].Substring(1).Replace(Environment.NewLine, "");
                    customSearchMessage = SplittedFase[6].Substring(1).Replace(Environment.NewLine, "");
                    customSearchNote = SplittedFase[7].Substring(1).Replace(Environment.NewLine, "");
                    closeMessage = SplittedFase[158].Substring(1).Replace(Environment.NewLine, "");
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
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\"))
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\", true);
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
            Properties.Settings.Default.LastUser = textBox1.Text;
            Tools.createFolders();
            profilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Korot\\Profiles\\" + Properties.Settings.Default.LastUser + "\\";
            Directory.CreateDirectory(profilePath);
            Tools.createThemes();
            Settings.Save();
            Properties.Settings.Default.Save();
            allowClose = true;
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }
        
        private void frmOOBE_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                HTAlt.WinForms.HTMsgBox msgBox = new HTAlt.WinForms.HTMsgBox(Text, closeMessage, new HTAlt.WinForms.HTDialogBoxContext() { Yes = true, No = true }) { Yes = Yes, No = No, OK = OK, Cancel = Cancel, BackgroundColor = Settings.Theme.BackColor, Icon = Icon };
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
                if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".lang"))
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
                if (lbLang.SelectedIndex < -1 || lbLang.SelectedIndex > lbLang.Items.Count - 1 || !File.Exists(Application.StartupPath + "//Lang//" + lbLang.SelectedItem.ToString() + ".lang"))
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
