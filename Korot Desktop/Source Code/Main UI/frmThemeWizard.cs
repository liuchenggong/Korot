using System;
using System.Windows.Forms;

namespace Korot
{
    public partial class frmThemeWizard : Form
    {
        private readonly Settings Settings;

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
            Sunrise,
            Avocado,
            Teal,
            Yellow,
            Orange,
            Brown,
            Leather,
            Gold,
            Creme,
            Purple,
            Raspberry,
            Lavender,
            Fuchsia,
            Pink,
            Brick,
            DarkBlue,
            Sea
        }

        private Themes Theme = Themes.Light;
        private bool allowSwitch = false;
        private int Brightness = 0;
        private SelectedType Type = SelectedType.Monotone;

        private void PreparetpTone()
        {
            trackBar1.Value = 0;
            switch (Type)
            {
                case SelectedType.Monotone:
                    trackBar1.Maximum = 4;
                    break;

                case SelectedType.Blue:
                    trackBar1.Maximum = 4;
                    break;

                case SelectedType.Green:
                    trackBar1.Maximum = 4;
                    break;

                case SelectedType.Red:
                    trackBar1.Maximum = 3;
                    break;

                case SelectedType.Yellow:
                    trackBar1.Maximum = 5;
                    break;

                case SelectedType.Purple:
                    trackBar1.Maximum = 4;
                    break;
            }
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
        public string Avocado = "Avocado";
        public string AvocadoDesc = "It's fresh!";
        public string Teal = "Teal";
        public string TealDesc = "Nice theme to relax a little.";
        public string Yellow = "Yellow";
        public string YellowDesc = "Because we love bananas!";
        public string Orange = "Orange";
        public string OrangeDesc = "Well sorry, this theme does not contain vitamin C.";
        public string Brown = "Brown";
        public string BrownDesc = "For coffee lovers.";
        public string Leather = "Leather";
        public string LeatherDesc = "It's cozy.";
        public string Gold = "Gold";
        public string GoldDesc = "For pigs that can walk, that also made out of cubes.";
        public string Creme = "Creme";
        public string CremeDesc = "Smells like vanilla, no please don't try to smell your monitor.";
        public string Purple = "Purple";
        public string PurpleDesc = "Haltroy hates this theme.";
        public string Raspberry = "Raspberry";
        public string RaspberryDesc = "3,142857142857143";
        public string Lavender = "Lavender";
        public string LavenderDesc = "Don't worry, this theme won't send you to hospital, if you are not Haltroy.";
        public string Fuchsia = "Fuchsia";
        public string FuchsiaDesc = "Threat your Korot like a flower.";
        public string Pink = "Pink";
        public string PinkDesc = "Made the theme b0ss.";
        public string Brick = "Brick";
        public string BrickDesc = "In the wall, of course.";
        public string DarkBlue = "DarkBlue";
        public string DarkBlueDesc = "Things getting dark, but liked it.";
        public string Sea = "Sea";
        public string SeaDesc = "Who wants to swim?";

        #endregion Translations

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (Type == SelectedType.Monotone)
            {
                switch (trackBar1.Value)
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
            }
            else if (Type == SelectedType.Blue)
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
                        lbTitle.Text = Sea;
                        lbDesc.Text = SeaDesc;
                        pbPreview.Image = Properties.Resources.ThemeSea;
                        Theme = Themes.Sea;
                        break;

                    case 2:
                        lbTitle.Text = DodgerBlue;
                        lbDesc.Text = DodgerBlueDesc;
                        pbPreview.Image = Properties.Resources.ThemeDodgerBlue;
                        Theme = Themes.DodgerBlue;
                        break;

                    case 3:
                        lbTitle.Text = Blue;
                        lbDesc.Text = BlueDesc;
                        pbPreview.Image = Properties.Resources.ThemeBlue;
                        Theme = Themes.Blue;
                        break;

                    case 4:
                        lbTitle.Text = DarkBlue;
                        lbDesc.Text = DarkBlueDesc;
                        pbPreview.Image = Properties.Resources.ThemeMidnight;
                        Theme = Themes.DarkBlue;
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
                        lbTitle.Text = Brick;
                        lbDesc.Text = BrickDesc;
                        pbPreview.Image = Properties.Resources.ThemeBrick;
                        Theme = Themes.Brick;
                        break;

                    case 3:
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
                        lbTitle.Text = Avocado;
                        lbDesc.Text = AvocadoDesc;
                        pbPreview.Image = Properties.Resources.ThemeAvocado;
                        Theme = Themes.Avocado;
                        break;

                    case 2:
                        lbTitle.Text = Green;
                        lbDesc.Text = GreenDesc;
                        pbPreview.Image = Properties.Resources.ThemeGreen;
                        Theme = Themes.Green;
                        break;

                    case 3:
                        lbTitle.Text = Teal;
                        lbDesc.Text = TealDesc;
                        pbPreview.Image = Properties.Resources.ThemeTeal;
                        Theme = Themes.Teal;
                        break;

                    case 4:
                        lbTitle.Text = DarkLeaf;
                        lbDesc.Text = DarkLeafDesc;
                        pbPreview.Image = Properties.Resources.ThemeDarkLeaf;
                        Theme = Themes.DarkLeaf;
                        break;
                }
            }
            else if (Type == SelectedType.Yellow)
            {
                switch (trackBar1.Value)
                {
                    case 0:
                        lbTitle.Text = Creme;
                        lbDesc.Text = CremeDesc;
                        pbPreview.Image = Properties.Resources.ThemeCreme;
                        Theme = Themes.Creme;
                        break;

                    case 1:
                        lbTitle.Text = Yellow;
                        lbDesc.Text = YellowDesc;
                        pbPreview.Image = Properties.Resources.ThemeYellow;
                        Theme = Themes.Yellow;
                        break;

                    case 2:
                        lbTitle.Text = Orange;
                        lbDesc.Text = OrangeDesc;
                        pbPreview.Image = Properties.Resources.ThemeOrange;
                        Theme = Themes.Orange;
                        break;

                    case 3:
                        lbTitle.Text = Gold;
                        lbDesc.Text = GoldDesc;
                        pbPreview.Image = Properties.Resources.ThemeGold;
                        Theme = Themes.Gold;
                        break;

                    case 4:
                        lbTitle.Text = Leather;
                        lbDesc.Text = LeatherDesc;
                        pbPreview.Image = Properties.Resources.ThemeLeather;
                        Theme = Themes.Leather;
                        break;

                    case 5:
                        lbTitle.Text = Brown;
                        lbDesc.Text = BrownDesc;
                        pbPreview.Image = Properties.Resources.ThemeBrown;
                        Theme = Themes.Brown;
                        break;
                }
            }
            else if (Type == SelectedType.Purple)
            {
                switch (trackBar1.Value)
                {
                    case 0:
                        lbTitle.Text = Pink;
                        lbDesc.Text = PinkDesc;
                        pbPreview.Image = Properties.Resources.ThemePink;
                        Theme = Themes.Pink;
                        break;

                    case 1:
                        lbTitle.Text = Fuchsia;
                        lbDesc.Text = FuchsiaDesc;
                        pbPreview.Image = Properties.Resources.ThemeFuchsia;
                        Theme = Themes.Fuchsia;
                        break;

                    case 2:
                        lbTitle.Text = Lavender;
                        lbDesc.Text = LavenderDesc;
                        pbPreview.Image = Properties.Resources.ThemeLavender;
                        Theme = Themes.Lavender;
                        break;

                    case 3:
                        lbTitle.Text = Purple;
                        lbDesc.Text = PurpleDesc;
                        pbPreview.Image = Properties.Resources.ThemePurple;
                        Theme = Themes.Purple;
                        break;

                    case 4:
                        lbTitle.Text = Midnight;
                        lbDesc.Text = MidnightDesc;
                        pbPreview.Image = Properties.Resources.ThemeMidnight;
                        Theme = Themes.Midnight;
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
            }
            else
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
            int rnd = new Random().Next(0, 5);
            switch (rnd)
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

                case 3:
                    pPurple_Click(sender, e);
                    break;

                case 4:
                    pYellow_Click(sender, e);
                    break;
            }
        }

        private readonly string themePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot ";

        private void htButton1_Click(object sender, EventArgs e)
        {
            KorotTools.createThemes();
            Settings.Theme = new Theme(themePath + Theme.ToString() + ".ktf");
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
            Avocado = Settings.LanguageSystem.GetItemText("ThemeAvocado");
            AvocadoDesc = Settings.LanguageSystem.GetItemText("ThemeAvocadoDesc");
            Teal = Settings.LanguageSystem.GetItemText("ThemeTeal");
            TealDesc = Settings.LanguageSystem.GetItemText("ThemeTealDesc");
            Yellow = Settings.LanguageSystem.GetItemText("ThemeYellow");
            YellowDesc = Settings.LanguageSystem.GetItemText("ThemeYellowDesc");
            Orange = Settings.LanguageSystem.GetItemText("ThemeOrange");
            OrangeDesc = Settings.LanguageSystem.GetItemText("ThemeOrangeDesc");
            Brown = Settings.LanguageSystem.GetItemText("ThemeBrown");
            BrownDesc = Settings.LanguageSystem.GetItemText("ThemeBrownDesc");
            Leather = Settings.LanguageSystem.GetItemText("ThemeLeather");
            LeatherDesc = Settings.LanguageSystem.GetItemText("ThemeLeatherDesc");
            Gold = Settings.LanguageSystem.GetItemText("ThemeGold");
            GoldDesc = Settings.LanguageSystem.GetItemText("ThemeGoldDesc");
            Creme = Settings.LanguageSystem.GetItemText("ThemeCreme");
            CremeDesc = Settings.LanguageSystem.GetItemText("ThemeCremeDesc");
            Purple = Settings.LanguageSystem.GetItemText("ThemePurple");
            PurpleDesc = Settings.LanguageSystem.GetItemText("ThemePurpleDesc");
            Raspberry = Settings.LanguageSystem.GetItemText("ThemeRaspberry");
            RaspberryDesc = Settings.LanguageSystem.GetItemText("ThemeRaspberryDesc");
            Lavender = Settings.LanguageSystem.GetItemText("ThemeLavender");
            LavenderDesc = Settings.LanguageSystem.GetItemText("ThemeLavenderDesc");
            Fuchsia = Settings.LanguageSystem.GetItemText("ThemeFuchsia");
            FuchsiaDesc = Settings.LanguageSystem.GetItemText("ThemeFuchsiaDesc");
            Pink = Settings.LanguageSystem.GetItemText("ThemePink");
            PinkDesc = Settings.LanguageSystem.GetItemText("ThemePinkDesc");
            Brick = Settings.LanguageSystem.GetItemText("ThemeBrick");
            BrickDesc = Settings.LanguageSystem.GetItemText("ThemeBrickDesc");
            DarkBlue = Settings.LanguageSystem.GetItemText("ThemeDarkBlue");
            DarkBlueDesc = Settings.LanguageSystem.GetItemText("ThemeDarkBlueDesc");
            Sea = Settings.LanguageSystem.GetItemText("ThemeSea");
            SeaDesc = Settings.LanguageSystem.GetItemText("ThemeSeaDesc");
        }

        private void pYellow_Click(object sender, EventArgs e)
        {
            Type = SelectedType.Yellow;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }

        private void pPurple_Click(object sender, EventArgs e)
        {
            Type = SelectedType.Purple;
            allowSwitch = true;
            tabControl1.SelectedTab = tpTone;
            PreparetpTone();
        }
    }
}