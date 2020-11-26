/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmThemeWizard : Form
    {
        private readonly Settings Settings;

        public frmThemeWizard(Settings _Settings)
        {
            Settings = _Settings;
            Theme = DefaultThemes.KorotLight(_Settings);
            InitializeComponent();
        }

        List<Theme> SelectedThemes;
        private Theme Theme;
        private bool allowSwitch = false;
        private Theme.Categories Type = Theme.Categories.Monotone;

        private void PreparetpTone()
        {
            SelectedThemes = Settings.Themes.GetThemesFromCategory(Type);
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i< SelectedThemes.Count;i++)
            {
                Theme theme = SelectedThemes[i];
                Panel pExample = new System.Windows.Forms.Panel();
                Label lbExampleDesc = new System.Windows.Forms.Label();
                Label lbExample = new System.Windows.Forms.Label();
                PictureBox pbExample = new System.Windows.Forms.PictureBox();

                // 
                // pExample
                // 
                pExample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pExample.Controls.Add(lbExampleDesc);
                pExample.Controls.Add(lbExample);
                pExample.Controls.Add(pbExample);
                pExample.Tag = theme;
                pExample.Size = new System.Drawing.Size(160, 220);
                pExample.Click += new System.EventHandler(pTheme_Click);
                pExample.BorderStyle = BorderStyle.FixedSingle;
                // 
                // lbExampleDesc
                // 
                lbExampleDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                lbExampleDesc.Font = new System.Drawing.Font("Ubuntu", 12F);
                lbExampleDesc.Location = new System.Drawing.Point(3, 138);
                lbExampleDesc.Size = new System.Drawing.Size(150, 75);
                lbExampleDesc.Text = theme.Desc;
                lbExampleDesc.Tag = theme;
                lbExampleDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbExampleDesc.Click += new System.EventHandler(pTheme_Click);
                // 
                // lbExample
                // 
                lbExample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                lbExample.Font = new System.Drawing.Font("Ubuntu", 12F, System.Drawing.FontStyle.Bold);
                lbExample.Location = new System.Drawing.Point(0, 105);
                lbExample.Size = new System.Drawing.Size(155, 23);
                lbExample.Text = theme.Name;
                lbExample.Tag = theme;
                lbExample.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                lbExample.Click += new System.EventHandler(pTheme_Click);
                // 
                // pbExample
                // 
                pbExample.Location = new System.Drawing.Point(3, 3);
                pbExample.Size = new System.Drawing.Size(150, 100);
                pbExample.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                pbExample.ImageLocation = theme.PreviewLocation;
                pbExample.Tag = theme;
                pbExample.Click += new System.EventHandler(pTheme_Click);


                flowLayoutPanel1.Controls.Add(pExample);
            }
        }

        private void pTheme_Click(object sender, EventArgs e)
        {
            var cntrl = sender as Control;
            var theme = cntrl.Tag as Theme;
            Theme = theme;
            lbName.Text = Theme.Name;
            lbDesc1.Text = Theme.Desc;
            pbTheme.ImageLocation = Theme.PreviewLocation;
            tabControl1.SelectedTab = tpResult;
            allowSwitch = true;
            tabControl1.SelectedTab = tpResult;
        }

        private void pBW_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Monotone;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void pC_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpColor;
            pRed.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Red).Count > 0;
            pOrange.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Orange).Count > 0;
            pYellow.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Yellow).Count > 0;
            pGreen.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Green).Count > 0;
            pBlue.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Blue).Count > 0;
            pPurple.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Purple).Count > 0;
        }

        private void htButton7_Click(object sender, EventArgs e)
        {
            bool isMono = new Random().Next(0, int.MaxValue) % 2 == 1;
            if (isMono)
            {
                pBW_Click(sender, e);
            }
            else
            {
                bool isMulti = new Random().Next(0, int.MaxValue) % 2 == 1;
                if (isMulti)
                {
                    pMultiColor_Click(sender, e);
                }else
                {
                    pC_Click(sender, e);
                }
            }
        }

        private void htButton4_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpGrayColor;
        }

        private void htButton2_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void htButton3_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpGrayColor;
        }

        private void htButton6_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = (Type == Theme.Categories.Monotone || Type == Theme.Categories.Rainbow) ? tpGrayColor : tpColor;
        }

        private void htButton8_Click(object sender, EventArgs e)
        {
            int rnd = new Random().Next(0, SelectedThemes.Count);
            Theme = SelectedThemes[rnd];
            allowSwitch = true;
            lbName.Text = Theme.Name;
            lbDesc1.Text = Theme.Desc;
            pbTheme.ImageLocation = Theme.PreviewLocation;
            tabControl1.SelectedTab = tpResult;
        }

        private void pRed_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Red;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }
        private void pOrange_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Orange;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void pGreen_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Green;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void pYellow_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Yellow;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void pPurple_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Purple;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        { if (allowSwitch) { allowSwitch = false; e.Cancel = false; } else { e.Cancel = true; } }

        private void pBlue_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Blue;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void htButton5_Click(object sender, EventArgs e)
        {
            int rnd = new Random().Next(0, 6);
            switch (rnd)
            {
                case 0:
                    pRed_Click(sender, e);
                    break;

                case 1:
                    pOrange_Click(sender, e);
                    break;

                case 2:
                    pYellow_Click(sender, e);
                    break;

                case 3:
                    pGreen_Click(sender, e);
                    break;

                case 4:
                    pBlue_Click(sender, e);
                    break;

                case 5:
                    pPurple_Click(sender, e);
                    break;


            }
        }

        private readonly string themePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot ";

        private void htButton1_Click(object sender, EventArgs e)
        {
            Settings.Theme = Theme;
            Settings.JustChangedTheme();
            Settings.Save();
            Close();
        }

        private void htButton9_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpResult;
        }

        private void frmThemeWizard_Load(object sender, EventArgs e)
        {
            pC.Enabled = (Settings.Themes.GetThemesFromCategory(Theme.Categories.Red).Count +
Settings.Themes.GetThemesFromCategory(Theme.Categories.Orange).Count +
Settings.Themes.GetThemesFromCategory(Theme.Categories.Yellow).Count +
Settings.Themes.GetThemesFromCategory(Theme.Categories.Green).Count +
Settings.Themes.GetThemesFromCategory(Theme.Categories.Blue).Count +
Settings.Themes.GetThemesFromCategory(Theme.Categories.Purple).Count) > 0;
            pBW.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Monotone).Count > 0;
            pbRainbow.Enabled = Settings.Themes.GetThemesFromCategory(Theme.Categories.Rainbow).Count > 0;
            btBack.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
            btBack2.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
            btBack3.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
            btRandom.Text = Settings.LanguageSystem.GetItemText("PickRandom");
            btRandom1.Text = Settings.LanguageSystem.GetItemText("PickRandom");
            btRandom2.Text = Settings.LanguageSystem.GetItemText("PickRandom");
            btApply.Text = Settings.LanguageSystem.GetItemText("ApplyTheme");
            btTryAgain.Text = Settings.LanguageSystem.GetItemText("TryAgain");
            lbYourTheme.Text = Settings.LanguageSystem.GetItemText("YourThemeIs");
            lbBW.Text = Settings.LanguageSystem.GetItemText("BlackWhite");
            lbBWInfo.Text = Settings.LanguageSystem.GetItemText("BlackWhiteDesc");
            lbRainbow.Text = Settings.LanguageSystem.GetItemText("Multicolor");
            lbRainbowDesc.Text = Settings.LanguageSystem.GetItemText("MulticolorDesc");
            lbOrange.Text = Settings.LanguageSystem.GetItemText("Orange");
            lbOrangeInfo.Text = Settings.LanguageSystem.GetItemText("OrangeDesc");
            lbC.Text = Settings.LanguageSystem.GetItemText("Colorful");
            lbCInfo.Text = Settings.LanguageSystem.GetItemText("ColorfulDesc");
            lbYellow.Text = Settings.LanguageSystem.GetItemText("Yellow");
            lbYellowInfo.Text = Settings.LanguageSystem.GetItemText("YellowDesc");
            lbPurple.Text = Settings.LanguageSystem.GetItemText("Purple");
            lbPurpleInfo.Text = Settings.LanguageSystem.GetItemText("PurpleDesc");
            lbRed.Text = Settings.LanguageSystem.GetItemText("Red");
            lbRedDesc.Text = Settings.LanguageSystem.GetItemText("RedDesc");
            lbGreen.Text = Settings.LanguageSystem.GetItemText("Green");
            lbGreenDesc.Text = Settings.LanguageSystem.GetItemText("GreenDesc");
            lbBlue.Text = Settings.LanguageSystem.GetItemText("Blue");
            lbBlueDesc.Text = Settings.LanguageSystem.GetItemText("BlueDesc");
            Text = Settings.LanguageSystem.GetItemText("ThemeTitle");
            lbSelectBWC.Text = Settings.LanguageSystem.GetItemText("ThemeTitle1");
            lbColor.Text = Settings.LanguageSystem.GetItemText("ThemeTitle2");
            lbTone.Text = Settings.LanguageSystem.GetItemText("ThemeTitle3");
            lbResult.Text = Settings.LanguageSystem.GetItemText("ThemeTitle4");
        }

        private void pMultiColor_Click(object sender, EventArgs e)
        {
            Type = Theme.Categories.Rainbow;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }
    }
}