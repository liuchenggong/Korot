/*

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by an MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE

*/

using CefSharp;
using HTAlt;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Korot
{
    internal class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        public frmMain anaform()
        {
            return ((frmMain)CefForm.ParentTabs);
        }

        private readonly frmCEF CefForm;
        public bool isExt = false;
        public Extension ext;
        public frmExt extForm;

        public SchemeHandlerFactory(frmCEF _CefForm)
        {
            CefForm = _CefForm;
        }

        public string StylizePage(string htmlCode)
        {
            return htmlCode.Replace("§BACKCOLOR§", Tools.ColorToHex(CefForm.Settings.Theme.BackColor))
                .Replace("§BACKCOLOR2§", Tools.ColorToHex(Tools.ShiftBrightness(CefForm.Settings.Theme.BackColor, 20, false)))
                .Replace("§BACKCOLOR3§", Tools.ColorToHex(Tools.ShiftBrightness(CefForm.Settings.Theme.BackColor, 60, false)))
                .Replace("§FORECOLOR§", Tools.ColorToHex(CefForm.Settings.Theme.ForeColor))
                .Replace("§OVERLAY§",Tools.ColorToHex(CefForm.Settings.Theme.OverlayColor))
                .Replace("§OVERLAY2§", Tools.ColorToHex(Tools.ShiftBrightness(CefForm.Settings.Theme.OverlayColor,20,false)))
                .Replace("§OCOLOR§", Tools.IsBright(CefForm.Settings.Theme.OverlayColor) ? "black" : "white");
        }

        public string GetOverlay()
        {
            return "color: rgb(" + CefForm.Settings.Theme.OverlayColor.R + " ," + CefForm.Settings.Theme.OverlayColor.G + " , " + CefForm.Settings.Theme.OverlayColor.B + ");";
        }

        private bool isBirthDay()
        {
            string today = DateTime.Now.ToString("dd//MM//yyyy");
            return CefForm.Settings.CelebrateBirthday ? (CefForm.Settings.Birthday == today) : false;
        }

        public string GetNewTabItems()
        {
            string x = "";
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite0 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite0) : "") + "</div>" + Environment.NewLine;
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite1 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite1) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite2 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite2) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite3 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite3) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite4 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite4) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite5 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite5) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite6 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite6) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite7 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite7) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite8 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite8) : "")  + "</div>" + Environment.NewLine; 
            x += "<div class=\"grid-container-div\">" + (CefForm.Settings.NewTabSites.FavoritedSite9 != null ? CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite9) : "")  + "</div>" + Environment.NewLine; 
            return x;
        }

        private string SearchPrettify(string x)
        {
            if (x.ToLowerInvariant().StartsWith("http") ||
                x.ToLowerInvariant().StartsWith("about") ||
                x.ToLowerInvariant().StartsWith("korot") ||
                x.ToLowerInvariant().StartsWith("file") ||
                x.ToLowerInvariant().StartsWith("ftp") ||
                x.ToLowerInvariant().StartsWith("smtp") ||
                x.ToLowerInvariant().StartsWith("pop") ||
                x.ToLowerInvariant().StartsWith("chrome"))
            {
                return x;
            }
            else
            {
                return "http://" + x;
            }
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (CefForm.Settings.IsUrlAllowed(request.Url))
            {
                if (schemeName == "korot")
                {
                    if (request.Url.ToLowerInvariant().StartsWith("korot://newtab"))
                    {
                        return ResourceHandler.FromString(
                            StylizePage(Properties.Resources.newtab.Replace("§BDAY§", isBirthDay() ? CefForm.anaform.HappyBDay : "")
                            .Replace("§ITEMS§", GetNewTabItems())
                            .Replace("§BORED§", CefForm.anaform.ImBored)
                            .Replace("§SEARCHHELP§", CefForm.anaform.SearchHelpText)
                            .Replace("§SEARCH§", CefForm.anaform.Search)
                            .Replace("§DAYS§", CefForm.anaform.DayNames)
                            .Replace("§MONTHS§", CefForm.anaform.MonthNames)
                            .Replace("§TITLE§", CefForm.anaform.NewTabtitle)
                            .Replace("§EDIT§", CefForm.anaform.NewTabEdit)));
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://incognito"))
                    {
                        return ResourceHandler.FromString(Properties.Resources.incognito.Replace("§TITLE§", CefForm.anaform.IncognitoT).Replace("§INCTITLE§", CefForm.anaform.IncognitoTitle).Replace("§INCTITLE1§", CefForm.anaform.IncognitoTitle1).Replace("§INCTITLE2§", CefForm.anaform.IncognitoTitle2).Replace("§INCTITLE1M1§", CefForm.anaform.IncognitoT1M1).Replace("§INCTITLE1M2§", CefForm.anaform.IncognitoT1M2).Replace("§INCTITLE1M3§", CefForm.anaform.IncognitoT1M3).Replace("§INCTITLE2M1§", CefForm.anaform.IncognitoT2M1).Replace("§INCTITLE2M2§", CefForm.anaform.IncognitoT2M2).Replace("§INCTITLE2M3§", CefForm.anaform.IncognitoT2M3));
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://technical"))
                    {
                        string KorotVersion = Application.ProductVersion.ToString();
                        HTInfo htinfo = new HTAlt.HTInfo();
                        string Arch = Environment.Is64BitProcess ? "amd64" : "i86";
                        string uAgent = KorotTools.GetUserAgent();
                        return ResourceHandler.FromString(StylizePage(Properties.Resources.technical
                            .Replace("§KOROTVER§", KorotVersion)
                            .Replace("§VER§", VersionInfo.VersionNumber.ToString())
                            .Replace("§CNAME§", VersionInfo.CodeName)
                            .Replace("§ARCH§", Arch)
                            .Replace("§OS§", System.Runtime.InteropServices.RuntimeInformation.OSDescription)
                            .Replace("§ARGS§", string.Join(" ", Environment.GetCommandLineArgs())).Replace("§APPPATH§", Application.ExecutablePath)
                            .Replace("§PROFILEPATH§", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Korot\\")
                            .Replace("§LANGPATH§", CefForm.Settings.LanguageSystem.LangFile)
                            .Replace("§LANGCOUNT§", "" + CefForm.Settings.LanguageSystem.ItemCount)
                            .Replace("§HTALTS§", htinfo.ProjectVersion + "[" + htinfo.ProjectCodeName + "]")
                            .Replace("§HTALTW§", htinfo.ProjectVersion + "[" + htinfo.ProjectCodeName + "]")
                            .Replace("§EASYTABS§", "2.0.0 [modified]")
                            .Replace("§CHROMIUM§", Cef.ChromiumVersion)
                            .Replace("§CEF§", Cef.CefVersion)
                            .Replace("§CEFSHARP§", Cef.CefSharpVersion)
                            .Replace("§AGENT§", uAgent)));
                    }
                    else if (request.Url.StartsWith("korot://rndwallpaper"))
                    {
                        if (browser.MainFrame.Url.StartsWith("korot"))
                        {
                            var themeimg = CefForm.Settings.GetRandomImageFromTheme();
                            if (themeimg == null)
                            {
                                return ResourceHandler.FromString("");
                            }
                            else
                            {
                                var img = Tools.ReadFile(themeimg.ActualLocation);
                                var type = new FileInfo(themeimg.ActualLocation).Extension.ToLowerInvariant().Substring(1); // ToLowerInvariant() => "I" - "ı" ToLowerInvariany() => "I" - "i"
                                return ResourceHandler.FromStream(img, "image/" + type, false, null);
                            }
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =  http://korot://error/?e=BLOCKED?u=" + browser.MainFrame.Url +" \" />");
                        }
                    }
                    else if (request.Url.StartsWith("korot://search/?q="))
                    {
                        Console.WriteLine("");
                        string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                        if (KorotTools.ValidHttpURL(x))
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url=" + SearchPrettify(x) + "\" />");
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url=" + CefForm.Settings.SearchEngine + x + "\" />");
                        }
                    }
                    else if (request.Url.StartsWith("korot://test"))
                    {
                        return ResourceHandler.FromString(Properties.Resources.test);
                    }
                    else if (request.Url.StartsWith("korot://search/"))
                    {
                        string x = request.Url.Substring(request.Url.IndexOf("/", 10) + 1);
                        if (KorotTools.ValidHttpURL(x))
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + SearchPrettify(x) + "\" />");
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + CefForm.Settings.SearchEngine + x + "\" />");
                        }
                    }
                    else if (request.Url.StartsWith("korot://command"))
                    {
                        if (frame.IsMain && CefForm.SessionSystem.Sessions.Count == 0)
                        {
                            string x = request.Url.Substring(request.Url.IndexOf("=", 10) + 1);
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =  " + x +  " \" />");
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =  http://korot://error/?e=BLOCKED?u=" + browser.MainFrame.Url + " \" />");
                        }
                    }
                    else if (request.Url.StartsWith("korot://completerandom"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url=" + CefForm.Settings.GiveRandomSite(true) + "\" />");
                    }
                    else if (request.Url.StartsWith("korot://random"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url=" + CefForm.Settings.GiveRandomSite(false) + "\" />");
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://certerror"))
                    {
                        return ResourceHandler.FromString(Properties.Resources.certerror.Replace("§TITLE§", CefForm.anaform.CertErrorPageTitle)
                            .Replace("§DESC§", CefForm.anaform.CertErrorPageMessage)
                            .Replace("§CONTINUE§", CefForm.anaform.CertErrorPageButton)
                            .Replace("§CERT§", CefForm.certificatedetails));
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://certcontinue"))
                    {
                        CefForm.CertAllowedUrls.Add(CefForm.certErrorUrl);
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + CefForm.certErrorUrl + "\" />");
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://empty"))
                    {
                        return ResourceHandler.FromString("");
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://licenses"))
                    {
                        return ResourceHandler.FromString(StylizePage(Properties.Resources.licenses
                            .Replace("§TITLE§", CefForm.anaform.licenseTitle)));
                    }
                    else if (request.Url.StartsWith("korot://error"))
                    {
                        string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                        string errorPage = "";
                        if (x.Contains('='))
                        {
                            errorPage = x.Substring(x.IndexOf('=') + 1);
                            x = x.Replace("?u" + errorPage, "");
                            errorPage = SearchPrettify(errorPage);
                        }
                        if (!string.IsNullOrWhiteSpace(errorPage))
                        {
                            CheckInternetConnection(errorPage, frame);
                        }
                        return ResourceHandler.FromString(StylizePage(Properties.Resources.errorpage.Replace("§RELOAD§", CefForm.anaform.Reload)
                            .Replace("§ERROR§", x)
                            .Replace("§URL§", (string.IsNullOrWhiteSpace(errorPage) ? "korot://empty" : errorPage))
                            .Replace("§TITLE§", CefForm.anaform.ErrorPageTitle)
                            .Replace("§KT§", CefForm.anaform.KT)
                            .Replace("§ET§", CefForm.anaform.ET)
                            .Replace("§E1§", CefForm.anaform.E1)
                            .Replace("§E2§", CefForm.anaform.E2)
                            .Replace("§E3§", CefForm.anaform.E3)
                            .Replace("§E4§", CefForm.anaform.E4)
                            .Replace("§RT§", CefForm.anaform.RT)
                            .Replace("§R1§", CefForm.anaform.R1)
                            .Replace("§R2§", CefForm.anaform.R2)
                            .Replace("§R3§", CefForm.anaform.R3)
                            .Replace("§R4§", CefForm.anaform.R4)));
                    }
                    else if (request.Url.StartsWith("korot://noint"))
                    {
                        string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                        return ResourceHandler.FromString(StylizePage(Properties.Resources.nointernet.Replace("§RELOAD§", CefForm.anaform.Reload)
                            .Replace("§URL§", (string.IsNullOrWhiteSpace(x) ? "korot://empty" : x))
                            .Replace("§TITLE§", CefForm.anaform.ErrorPageTitle)
                            .Replace("§NI1§", CefForm.anaform.NoInt1)
                            .Replace("§NI2§", CefForm.anaform.NoInt2)
                            .Replace("§NI3§", CefForm.anaform.NoInt3)));
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://dad"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com \" />");
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://me"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com/Korot.html \" />");
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://links"))
                    {
                        return ResourceHandler.FromString(StylizePage(Properties.Resources.korotlinks));
                    }
                    else if (request.Url.StartsWith("korot://extension"))
                    {
                        string x = request.Url.Substring(request.Url.IndexOf("/", 11) + 1);
                        // "<meta http-equiv=\"Refresh\" content=\"0; url =" + x + "\" />"
                        if (x.Count(i => (i == '/')) >= 4)
                        {
                            string codename = x.Substring(0, x.IndexOf(""));
                            if (CefForm.Settings.Extensions.Exists(codename))
                            {
                                Extension cext = CefForm.Settings.Extensions.GetExtensionByCodeName(codename);
                                if (x.Count(i => (i == '/')) >= 5)
                                {
                                    string fileLoc = x.Substring(x.IndexOf(codename + ""));
                                    if (cext.FileExists(fileLoc))
                                    {
                                        return ResourceHandler.FromString(HTAlt.Tools.ReadFile(cext.Folder + fileLoc, Encoding.Unicode));
                                    }
                                }
                                else
                                {
                                    return ResourceHandler.FromString(HTAlt.Tools.ReadFile(cext.Popup, Encoding.Unicode));
                                }
                            }
                        }
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=BLOCKED?u=" + browser.MainFrame.Url + " \" />");
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://refresh"))
                    {
                        if (isExt)
                        {
                            if (!string.IsNullOrWhiteSpace(ext.ManifestFile) && extForm != null)
                            {
                                CefForm.Invoke(new Action(() => CefForm.applyExtension(ext)));
                                extForm.Invoke(new Action(() => extForm.Close()));
                                return ResourceHandler.FromString("");
                            }
                            else
                            {
                                return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=NOT_ACTUAL_KOROT_PAGE?u=" + browser.MainFrame.Url + " \" />");
                            }
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=NOT_ACTUAL_KOROT_PAGE?u=" + browser.MainFrame.Url + " \" />");
                        }
                    }
                    else if (request.Url.ToLowerInvariant().StartsWith("korot://folder") || request.Url.ToLowerInvariant().StartsWith("korot://root"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=NOT_KOROT_PAGE?u=" + browser.MainFrame.Url + " \" />");
                    }
                    else
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=FILE_NOT_FOUND?u=" + browser.MainFrame.Url + " \" />");
                    }
                }
                return new ResourceHandler();
            }
            else
            {
                return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =  http://korot://error/?e=BLOCKED?u=" + browser.MainFrame.Url + " \" />");
            }
        }

        private async void CheckInternetConnection(string url, IFrame frame)
        {
            await Task.Run(() =>
            {
                if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                {
                    frame.LoadUrl("korot://noint/?u=" + url);
                }
            });
        }
    }
}