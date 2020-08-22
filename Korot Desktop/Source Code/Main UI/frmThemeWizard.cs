using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmThemeWizard : Form
    {
        Settings Settings;
        public frmThemeWizard(Settings _Settings)
        {
            Settings = _Settings;
            InitializeComponent();
        }
        private enum SelectedType
        {
            Monotone,
            Red,
            Green,
            Blue,
            Yellow,
            Purple //behind the slaughter or something metal body idk never watched it
        }
        private enum Themes
        {
            Blue,
            Cement,
            Crimson,
            Dark,
            DarkLeaf,
            DodgerBlue,
            Emerald,
            Gray,
            Green,
            Light,
            Midnight,
            Red,
            Shadow,
            Strawberry,
            Sunrise
        }
        Themes Theme = Themes.Light;
        bool allowSwitch = false;
        int Brightness = 0;
        SelectedType Type = SelectedType.Monotone;
        private void PreparetpTone()
        {
            trackBar1.Value = 0;
            trackBar1.Maximum = Type == SelectedType.Monotone ? 4 : (Type == SelectedType.Blue ? 3 : 2);
            Brightness = 0;
            trackBar1_Scroll(this, new EventArgs());
        }
        private void pBW_Click(object sender, EventArgs e)
        {
            Type = SelectedType.Monotone;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        #region Translations
        public string Light = "Light";
        public string LightDesc = "Beautiful photons for my eyes.";
        public string Cement = "Cement";
        public string CementDesc = "Finally, a browser made of stone, or cement.";
        public string Gray = "Gray";
        public string GrayDesc = "For people who are in between Light and Dark.";
        public string Shadow = "Shadow";
        public string ShadowDesc = "A nice not-so-dark theme.";
        public string Dark = "Dark";
        public string DarkDesc = "Finally, my eyes are not going to burn.";
        public string Blue = "Blue";
        public string BlueDesc = "It's blue like in 1999.";
        public string Sunrise = "Sunrise";
        public string SunriseDesc = "Maybe a good kingdom name but it's suppose to be different color.";
        public string DodgerBlue = "DodgerBlue";
        public string DodgerBlueDesc = "A theme from past, even before the name Haltroy.";
        public string Midnight = "Midnight";
        public string MidnightDesc = "Perfect color for a website background.";
        public string Red = "Red";
        public string RedDesc = "Meet this color in a shooting game from 2007.";
        public string Strawberry = "Strawberry";
        public string StrawberryDesc = "OMG need this rn asap xoxo";
        public string Crimson = "Crimson";
        public string CrimsonDesc = "It's alive!";
        public string Green = "Green";
        public string GreenDesc = "Join this team, they might find a way to trace those beams.";
        public string Emerald = "Emerald";
        public string EmeraldDesc = "You can use this theme for trading, just kidding. Cool theme for collecting bugs.";
        public string DarkLeaf = "DarkLeaf";
        public string DarkLeafDesc = "They look good on trees, not in your hand burning.";
        #endregion
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (Type == SelectedType.Monotone)
            {
                switch(trackBar1.Value)
                {
                    case 0:
                        lbTitle.Text = Light;
                        lbDesc.Text = LightDesc;
                        pbPreview.Image = Properties.Resources.ThemeLight;
                        Theme = Themes.Light;
                        break;
                    case 1:
                        lbTitle.Text = Cement;
                        lbDesc.Text = CementDesc;
                        pbPreview.Image = Properties.Resources.ThemeCement;
                        Theme = Themes.Cement;
                        break;
                    case 2:
                        lbTitle.Text = Gray;
                        lbDesc.Text = GrayDesc;
                        pbPreview.Image = Properties.Resources.ThemeGray;
                        Theme = Themes.Gray;
                        break;
                    case 3:
                        lbTitle.Text = Shadow;
                        lbDesc.Text = ShadowDesc;
                        pbPreview.Image = Properties.Resources.ThemeShadow;
                        Theme = Themes.Shadow;
                        break;
                    case 4:
                        lbTitle.Text = Dark;
                        lbDesc.Text = DarkDesc;
                        pbPreview.Image = Properties.Resources.ThemeDark;
                        Theme = Themes.Dark;
                        break;
                }
            }else if (Type == SelectedType.Blue)
            {
                switch (trackBar1.Value)
                {
                    case 0:
                        lbTitle.Text = Sunrise;
                        lbDesc.Text = SunriseDesc;
                        pbPreview.Image = Properties.Resources.ThemeSunrise;
                        Theme = Themes.Sunrise;
                        break;
                    case 1:
                        lbTitle.Text = DodgerBlue;
                        lbDesc.Text = DodgerBlueDesc;
                        pbPreview.Image = Properties.Resources.ThemeDodgerBlue;
                        Theme = Themes.DodgerBlue;
                        break;
                    case 2:
                        lbTitle.Text = Blue;
                        lbDesc.Text = BlueDesc;
                        pbPreview.Image = Properties.Resources.ThemeBlue;
                        Theme = Themes.Blue;
                        break;
                    case 3:
                        lbTitle.Text = Midnight;
                        lbDesc.Text = MidnightDesc;
                        pbPreview.Image = Properties.Resources.THemeMidnight;
                        Theme = Themes.Midnight;
                        break;
                }
            }
            else if (Type == SelectedType.Red)
            {
                switch (trackBar1.Value)
                {
                    case 0:
                        lbTitle.Text = Strawberry;
                        lbDesc.Text = StrawberryDesc;
                        pbPreview.Image = Properties.Resources.ThemeStrawberry;
                        Theme = Themes.Strawberry;
                        break;
                    case 1:
                        lbTitle.Text = Red;
                        lbDesc.Text = RedDesc;
                        pbPreview.Image = Properties.Resources.ThemeRed;
                        Theme = Themes.Red;
                        break;
                    case 2:
                        lbTitle.Text = Crimson;
                        lbDesc.Text = CrimsonDesc;
                        pbPreview.Image = Properties.Resources.ThemeCrimson;
                        Theme = Themes.Crimson;
                        break;
                }
            }
            else if (Type == SelectedType.Green)
            {
                switch (trackBar1.Value)
                {
                    case 0:
                        lbTitle.Text = Emerald;
                        lbDesc.Text = EmeraldDesc;
                        pbPreview.Image = Properties.Resources.ThemeEmerald;
                        Theme = Themes.Emerald;
                        break;
                    case 1:
                        lbTitle.Text = Green;
                        lbDesc.Text = GreenDesc;
                        pbPreview.Image = Properties.Resources.ThemeGreen;
                        Theme = Themes.Green;
                        break;
                    case 2:
                        lbTitle.Text = DarkLeaf;
                        lbDesc.Text = DarkLeafDesc;
                        pbPreview.Image = Properties.Resources.ThemeDarkLeaf;
                        Theme = Themes.DarkLeaf;
                        break;
                }
            }
            lbName.Text = lbTitle.Text;
            lbDesc1.Text = lbDesc.Text;
            pbTheme.Image = pbPreview.Image;
            Brightness = trackBar1.Value;
        }

        private void pC_Click(object sender, EventArgs e)
        {
            allowSwitch = true;
            tabControl1.SelectedTab = tpColor;
        }

        private void htButton7_Click(object sender, EventArgs e)
        {
            bool isMono = new Random().Next(0, int.MaxValue) % 2 == 1;
            if (isMono)
            {
                pBW_Click(sender, e);
            }else
            {
                pC_Click(sender, e);
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
            tabControl1.SelectedTab = Type == SelectedType.Monotone ? tpGrayColor : tpColor;
        }

        private void htButton8_Click(object sender, EventArgs e)
        {
            int rnd = new Random().Next(0, trackBar1.Maximum + 1);
            trackBar1.Value = rnd;
            Brightness = rnd;
            trackBar1_Scroll(sender, e);
        }

        private void pRed_Click(object sender, EventArgs e)
        {
            Type = SelectedType.Red;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void pGreen_Click(object sender, EventArgs e)
        {
            Type = SelectedType.Green;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        { if (allowSwitch) { allowSwitch = false; e.Cancel = false; } else { e.Cancel = true; } }
        private void pBlue_Click(object sender, EventArgs e)
        {
            Type = SelectedType.Blue;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void htButton5_Click(object sender, EventArgs e)
        {
            int rnd = new Random().Next(0, 3);
            switch(rnd)
            {
                case 0:
                    pRed_Click(sender, e);
                    break;
                case 1:
                    pGreen_Click(sender, e);
                    break;
                case 2:
                    pBlue_Click(sender, e);
                    break;
            }
        }
        string themePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot ";
        private void htButton1_Click(object sender, EventArgs e)
        {
            KorotTools.createThemes();
            Settings.Theme = new Theme(themePath + Theme.ToString() + ".ktf");
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
            btBack.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
            btBack2.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
            btBack3.Text = Settings.LanguageSystem.GetItemText("OOBEBack");
            btRandom.Text = Settings.LanguageSystem.GetItemText("PickRandom");
            btRandom1.Text = Settings.LanguageSystem.GetItemText("PickRandom");
            btRandom2.Text = Settings.LanguageSystem.GetItemText("PickRandom");
            btApply.Text = Settings.LanguageSystem.GetItemText("ApplyTheme");
            btSelect.Text = Settings.LanguageSystem.GetItemText("SelectThisTheme");
            btTryAgain.Text = Settings.LanguageSystem.GetItemText("TryAgain");
            lbYourTheme.Text = Settings.LanguageSystem.GetItemText("YourThemeIs");
            lbBW.Text = Settings.LanguageSystem.GetItemText("BlackWhite");
            lbBWInfo.Text = Settings.LanguageSystem.GetItemText("BlackWhiteDesc");
            lbC.Text = Settings.LanguageSystem.GetItemText("Colorful");
            lbCInfo.Text = Settings.LanguageSystem.GetItemText("ColorfulDesc");
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
            Light = Settings.LanguageSystem.GetItemText("ThemeLight");
            LightDesc = Settings.LanguageSystem.GetItemText("ThemeLightDesc");
            Cement = Settings.LanguageSystem.GetItemText("ThemeCement");
            CementDesc = Settings.LanguageSystem.GetItemText("ThemeCementDesc");
            Gray = Settings.LanguageSystem.GetItemText("ThemeGray");
            GrayDesc = Settings.LanguageSystem.GetItemText("ThemeGrayDesc");
            Shadow = Settings.LanguageSystem.GetItemText("ThemeShadow");
            ShadowDesc = Settings.LanguageSystem.GetItemText("ThemeShadowDesc");
            Dark = Settings.LanguageSystem.GetItemText("ThemeDark");
            DarkDesc = Settings.LanguageSystem.GetItemText("ThemeDarkDesc");
            Blue = Settings.LanguageSystem.GetItemText("ThemeBlue");
            BlueDesc = Settings.LanguageSystem.GetItemText("ThemeBlueDesc");
            Sunrise = Settings.LanguageSystem.GetItemText("ThemeSunrise");
            SunriseDesc = Settings.LanguageSystem.GetItemText("ThemeSunriseDesc");
            DodgerBlue = Settings.LanguageSystem.GetItemText("ThemeDodgerBlue");
            DodgerBlueDesc = Settings.LanguageSystem.GetItemText("ThemeDodgerBlueDesc");
            Midnight = Settings.LanguageSystem.GetItemText("ThemeMidnight");
            MidnightDesc = Settings.LanguageSystem.GetItemText("ThemeMidnightDesc");
            Red = Settings.LanguageSystem.GetItemText("ThemeRed");
            RedDesc = Settings.LanguageSystem.GetItemText("ThemeRedDesc");
            Strawberry = Settings.LanguageSystem.GetItemText("ThemeStrawberry");
            StrawberryDesc = Settings.LanguageSystem.GetItemText("ThemeStrawberryDesc");
            Crimson = Settings.LanguageSystem.GetItemText("ThemeCrimson");
            CrimsonDesc = Settings.LanguageSystem.GetItemText("ThemeCrimsonDesc");
            Green = Settings.LanguageSystem.GetItemText("ThemeGreen");
            GreenDesc = Settings.LanguageSystem.GetItemText("ThemeGreenDesc");
            Emerald = Settings.LanguageSystem.GetItemText("ThemeEmerald");
            EmeraldDesc = Settings.LanguageSystem.GetItemText("ThemeEmeraldDesc");
            DarkLeaf = Settings.LanguageSystem.GetItemText("ThemeDarkLeaf");
            DarkLeafDesc = Settings.LanguageSystem.GetItemText("ThemeDarkLeafDesc");
        }
    }
}
