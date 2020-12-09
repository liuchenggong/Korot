using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Korot
{
    public class Themes 
    {
        private Settings Settings;
        private List<Theme> _Themes;
        public List<Theme> ThemeList { get => _Themes; set => _Themes = value; }
        public string ThemesFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\";

        public Themes(Settings settings)
        {
            Settings = settings;
            _Themes = new List<Theme>();
            string[] themeFiles = Directory.GetFiles(ThemesFolder, "*.ktm", SearchOption.TopDirectoryOnly);
            for (int i = 0; i< themeFiles.Length;i++)
            {
                _Themes.Add(new Theme(themeFiles[i], settings));
            }
        }
        public List<Theme> GetThemesFromCategory(Theme.Categories Category) => _Themes.FindAll(i => i.Category == Category);
        public bool ThemeExists(Theme theme)
        {
            bool foundTheme = false;
            for (int i = 0; i< ThemeList.Count;i++)
            {
                Theme workTheme = ThemeList[i];
                if (workTheme.Equals(theme))
                {
                    foundTheme = true;
                }
            }

            return foundTheme;
        }
        public int GetThemeIndex(Theme theme)
        {
            int themeIndex = -1;
            for (int i = 0; i < ThemeList.Count; i++)
            {
                Theme workTheme = ThemeList[i];
                if (workTheme.Equals(theme))
                {
                    themeIndex = i;
                }
            }

            return themeIndex;
        }
    }
    public static class DefaultThemes
    {
        public static Theme KorotLight(Settings settings) => new Theme() { 
            BackColor = Color.FromArgb(255, 255, 255, 255), 
            AutoForeColor = true, 
            OverlayColor = Color.FromArgb(255, 64, 128, 255), 
            Desc = "Light theme for Korot.",
            Author = "Haltroy",
            CloseButtonColor = TabColors.OverlayColor,
            NewTabColor = TabColors.OverlayColor,
            MininmumKorotVersion = VersionInfo.VersionNumber,
            Version = 1,
            Name = "Korot Light",
            Category = Theme.Categories.Monotone,
            UseHaltroyUpdate = false,
            Settings = settings,
            ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Haltroy.KorotLight.ktm"
        };
        public static Theme KorotDark(Settings settings) => new Theme() { 
            BackColor = Color.FromArgb(255, 0,0,0), 
            AutoForeColor = true, 
            OverlayColor = Color.FromArgb(255, 64, 128, 255), 
            Author = "Haltroy",
            Desc = "Dark theme for Korot.",
            CloseButtonColor = TabColors.OverlayColor,
            NewTabColor = TabColors.OverlayColor,
            MininmumKorotVersion = VersionInfo.VersionNumber,
            Category = Theme.Categories.Monotone,
            Version = 1,
            Name = "Korot Dark",
            UseHaltroyUpdate = false,
            Settings = settings,
            ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Haltroy.KorotDark.ktm"
        };
        public static Theme KorotMidnight(Settings settings) => new Theme()
        {
            BackColor = Color.FromArgb(255, 5,0,36),
            AutoForeColor = false,
            OverlayColor = Color.FromArgb(255, 153, 255, 204),
            ForeColor = Color.FromArgb(255, 153, 255, 204),
            Author = "Haltroy",
            Desc = "Website theme of Haltroy.",
            CloseButtonColor = TabColors.OverlayColor,
            NewTabColor = TabColors.OverlayColor,
            MininmumKorotVersion = VersionInfo.VersionNumber,
            Category = Theme.Categories.Rainbow,
            Version = 1,
            Name = "Korot Midnight",
            UseHaltroyUpdate = false,
            Settings = settings,
            ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Haltroy.KorotMidnight.ktm"
        };
    }
    public class ThemeImage
    {
        public ThemeImage(string location, string themeName)
        {
            IsUserWallpaper = false;
            Location = location;
            ThemeName = themeName;
        }

        public ThemeImage(string location)
        {
            IsUserWallpaper = true;
            Location = location;
        }
        public bool IsUserWallpaper { get; set; }
        public string Location { get; set; }
        public string ThemeName { get; set; }
        public string ActualLocation => (!IsUserWallpaper ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + ThemeName + "\\"  : "") + Location;
    }
    public class Theme
    {
        public static string[] SupportedWallpaperTypes { get => new string[] { "bmp" ,
"emf" ,
"wmf" ,
"gif" ,
"jpeg" ,
"jpg" ,
"png" ,
"tiff" ,
"ico"};
        }
        private string GetWallpaperList()
        {
            string x = "";
            for (int i = 0; i < Wallpapers.Count;i++)
            {
                x += "<Wallpaper>" + Wallpapers[i].Location + "</Wallpaper>" + Environment.NewLine;
            }
            return x;
        }
        public string HTUpdate { get; set; }
        public void SaveTheme() => SaveThemeTo(ThemeFile);
        public void SaveThemeTo(string fileLoc)
        {
            string x = "<?xml version=\"1.0\" encoding=\"UTF-16\"?>" + Environment.NewLine + "<Theme>" + Environment.NewLine +
                "<Version>" + Version.ToString() + "</Version>" + Environment.NewLine +
                "<MinimumKorotVersion>" + MininmumKorotVersion.ToString() + "</MinimumKorotVersion>" + Environment.NewLine +
                "<UseHaltroyUpdate>" + (UseHaltroyUpdate ? "true" : "false") + "</UseHaltroyUpdate>" + Environment.NewLine +
                "<Desc>" + Desc.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Desc>" + Environment.NewLine +
                "<Category>" + (Category == Categories.Rainbow ? "Multicolor" : Category.ToString()) + "</Category>" + Environment.NewLine +
            "<Name>" + Name.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Name>" + Environment.NewLine +
            "<Author>" + Author.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("'", "&apos;") + "</Author>" + Environment.NewLine +
            "<BackColor>" + HTAlt.Tools.ColorToHex(BackColor) + "</BackColor>" + Environment.NewLine +
            (AutoForeColor ? "" : ("<ForeColor>" + HTAlt.Tools.ColorToHex(ForeColor) + "</ForeColor>" + Environment.NewLine)) +
            "<OverlayColor>" + HTAlt.Tools.ColorToHex(OverlayColor) + "</OverlayColor>" + Environment.NewLine +
            "<NewTabColor>" + (int)NewTabColor + "</NewTabColor>" + Environment.NewLine +
            "<CloseButtonColor>" + (int)CloseButtonColor + "</CloseButtonColor>" + Environment.NewLine +
            (Wallpapers.Count > 0 ? ("<Wallpapers>" + Environment.NewLine + GetWallpaperList() + "</Wallpapers>") : "") +
            "</Theme>";
            HTAlt.Tools.WriteFile(fileLoc, x, Encoding.Unicode);
        }
        public Theme()
        {
        }
        public List<ThemeImage> Wallpapers { get; set; } = new List<ThemeImage>();
        public void LoadFromFile(string themeFile)
        {
            if (string.IsNullOrWhiteSpace(themeFile) || !File.Exists(themeFile))
            {
                return;
            }
            ThemeFile = themeFile;
            string ManifestXML = HTAlt.Tools.ReadFile(ThemeFile, Encoding.Unicode);
            XmlDocument document = new XmlDocument();
            document.LoadXml(ManifestXML);
            XmlNode workNode = document.FirstChild;
            if (document.FirstChild.Name.ToLowerInvariant() == "xml") { workNode = document.FirstChild.NextSibling; }
            foreach (XmlNode node in workNode.ChildNodes)
            {
                switch (node.Name.ToLowerInvariant())
                {
                    case "name":
                        Name = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "author":
                        Author = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "desc":
                        Desc = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "usehaltroyupdate":
                        UseHaltroyUpdate = node.InnerText == "true";
                        break;
                    case "htupdate":
                        HTUpdate = node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                        break;
                    case "version":
                        Version = Convert.ToInt32(node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"));
                        break;
                    case "wallpapers":
                        foreach(XmlNode subnode in node.ChildNodes)
                        {
                            if (subnode.Name.ToLowerInvariant() == "wallpaper")
                            {
                                var themesFoler = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\";
                                var themeFolder = themesFoler + CodeName + "\\";
                                var file = subnode.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'");
                                var type = new FileInfo(themeFolder + file).Extension.ToLowerInvariant().Substring(1); // ToLowerInvariant() => "I" - "ı" ToLowerInvariany() => "I" - "i"
                                if (SupportedWallpaperTypes.Contains(type))
                                {
                                    Wallpapers.Add(new ThemeImage(file, CodeName));
                                }else
                                {
                                    Output.WriteLine(" [Theme] Error while importing wallpaper: \"" + type + "\" is not supported. (\"" + file + "\")");
                                }
                            }
                        }
                        break;
                    case "category":
                        switch(node.InnerText.ToLowerInvariant()) 
                        {
                            case "monotone":
                                Category = Categories.Monotone;
                                break;
                            case "red":
                                Category = Categories.Red;
                                break;
                            case "orange":
                                Category = Categories.Orange;
                                break;
                            case "yellow":
                                Category = Categories.Yellow;
                                break;
                            case "green":
                                Category = Categories.Green;
                                break;
                            case "blue":
                                Category = Categories.Blue;
                                break;
                            case "purple":
                                Category = Categories.Purple;
                                break;
                            case "multicolor":
                            case "": //taşak
                            default:
                                Category = Categories.Rainbow;
                                break;

                        }
                        break;
                    case "minimumkorotversion":
                        MininmumKorotVersion = Convert.ToInt32(node.InnerText.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&apos;", "'"));
                        break;
                    case "backcolor":
                        BackColor = HTAlt.Tools.HexToColor(node.InnerText);
                        break;
                    case "forecolor":
                        AutoForeColor = false;
                        ForeColor = HTAlt.Tools.HexToColor(node.InnerText);
                        break;
                    case "overlaycolor":
                        OverlayColor = HTAlt.Tools.HexToColor(node.InnerText);
                        break;
                    case "newtabcolor":
                        switch (node.InnerText)
                        {
                            case "0":
                                NewTabColor = TabColors.BackColor;
                                break;
                            case "1":
                                NewTabColor = TabColors.ForeColor;
                                break;
                            case "2":
                                NewTabColor = TabColors.OverlayColor;
                                break;
                            case "3":
                                NewTabColor = TabColors.OverlayBackColor;
                                break;
                        }
                        break;
                    case "closebuttoncolor":
                        switch (node.InnerText)
                        {
                            case "0":
                                CloseButtonColor = TabColors.BackColor;
                                break;
                            case "1":
                                CloseButtonColor = TabColors.ForeColor;
                                break;
                            case "2":
                                CloseButtonColor = TabColors.OverlayColor;
                                break;
                            case "3":
                                CloseButtonColor = TabColors.OverlayBackColor;
                                break;
                        }
                        break;
                }
            }
            if (ForeColor == Color.Empty || ForeColor == null)
            {
                AutoForeColor = true;
                ForeColor = HTAlt.Tools.AutoWhiteBlack(BackColor);
            }
            else
            {
                AutoForeColor = false;
            }
        }

        public string PreviewLocation => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\" + CodeName + ".png";
        public bool AutoForeColor { get; set; }

        public Theme(string themeFile, Settings settings)
        {
            Settings = settings;
            Name = "Korot Light";
            ThemeFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\" + SafeFileSettingOrganizedClass.LastUser + "\\Themes\\Korot Light.ktf";
            Author = "Haltroy";
            UseHaltroyUpdate = false;
            Version = VersionInfo.VersionNumber;
            MininmumKorotVersion = Version;
            BackColor = Color.FromArgb(255, 255, 255, 255);
            OverlayColor = Color.FromArgb(255, 85, 180, 212);
            NewTabColor = TabColors.OverlayColor;
            CloseButtonColor = TabColors.OverlayColor;
            LoadFromFile(themeFile);
        }

        public Settings Settings { get; set; } = null;
        public int Version { get; set; }
        public bool UseHaltroyUpdate { get; set; }
        public string CodeName => Author + "." + Name;
        public int MininmumKorotVersion { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Author { get; set; }
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color OverlayColor { get; set; }
        public string ThemeFile { get; set; }
        public TabColors NewTabColor { get; set; }
        public TabColors CloseButtonColor { get; set; }
        public Categories Category { get; set; }

        public enum Categories
        {
            Monotone,
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Purple,
            Rainbow
        }

        public void Update()
        {
            if (Settings != null)
            {
                if (UseHaltroyUpdate)
                {
                    frmUpdateExt frmUpdate = new frmUpdateExt(this, Settings);
                    frmUpdate.Show();
                }
            }
        }

        public override int GetHashCode()
        {
            int hashCode = 868791981;
            hashCode = hashCode * -1521134295 + AutoForeColor.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Settings>.Default.GetHashCode(Settings);
            hashCode = hashCode * -1521134295 + Version.GetHashCode();
            hashCode = hashCode * -1521134295 + UseHaltroyUpdate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CodeName);
            hashCode = hashCode * -1521134295 + MininmumKorotVersion.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Author);
            hashCode = hashCode * -1521134295 + BackColor.GetHashCode();
            hashCode = hashCode * -1521134295 + ForeColor.GetHashCode();
            hashCode = hashCode * -1521134295 + OverlayColor.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ThemeFile);
            hashCode = hashCode * -1521134295 + NewTabColor.GetHashCode();
            hashCode = hashCode * -1521134295 + CloseButtonColor.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is Theme theme &&
                   HTUpdate == theme.HTUpdate &&
                   EqualityComparer<List<ThemeImage>>.Default.Equals(Wallpapers, theme.Wallpapers) &&
                   PreviewLocation == theme.PreviewLocation &&
                   AutoForeColor == theme.AutoForeColor &&
                   UseHaltroyUpdate == theme.UseHaltroyUpdate &&
                   CodeName == theme.CodeName &&
                   Name == theme.Name &&
                   Desc == theme.Desc &&
                   Author == theme.Author &&
                   EqualityComparer<Color>.Default.Equals(BackColor, theme.BackColor) &&
                   EqualityComparer<Color>.Default.Equals(ForeColor, theme.ForeColor) &&
                   EqualityComparer<Color>.Default.Equals(OverlayColor, theme.OverlayColor) &&
                   ThemeFile == theme.ThemeFile &&
                   NewTabColor == theme.NewTabColor &&
                   CloseButtonColor == theme.CloseButtonColor &&
                   Category == theme.Category;
        }
    }
}
