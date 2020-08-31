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
using CefSharp;
using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Animation;

namespace Korot
{
    internal class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        public frmMain anaform()
        {
            return ((frmMain)CefForm.ParentTabs);
        }
        private class Attemption
        {
            public Attemption(string _Url,int _Attempt)
            {
                Url = _Url;
                Attempt = _Attempt;
            }
            public string Url { get; set; }
            public int Attempt { get; set; }
        }

        private readonly frmCEF CefForm;
        public bool isExt = false;
        public Extension ext;
        public frmExt extForm;
        public SchemeHandlerFactory(frmCEF _CefForm)
        {
            CefForm = _CefForm;
        }
        public static bool ValidHaltroyWebsite(string s)
        {
            string Pattern = @"(?:http\:\/\/haltroy\.com)|(?:https\:\/\/haltroy\.com)";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s.Substring(0, 19));
        }
        public string GetBackStyle()
        {
            if (CefForm.Settings.Theme.BackgroundStyle == "BACKCOLOR")
            {
                return "background-color: rgb(" + CefForm.Settings.Theme.BackColor.R + " ," + CefForm.Settings.Theme.BackColor.G + " , " + CefForm.Settings.Theme.BackColor.B + ");";
            }
            else
            {
                return CefForm.Settings.Theme.BackgroundStyle;
            }
        }
        public string GetOverlay()
        {
            return "color: rgb(" + CefForm.Settings.Theme.OverlayColor.R + " ," + CefForm.Settings.Theme.OverlayColor.G + " , " + CefForm.Settings.Theme.OverlayColor.B + ");";
        }
        public string GetBackStyle2()
        {
            return "background-color: rgb(" + CefForm.Settings.Theme.BackColor.R + " ," + CefForm.Settings.Theme.BackColor.G + " , " + CefForm.Settings.Theme.BackColor.B + "); color: " + (HTAlt.Tools.IsBright(CefForm.Settings.Theme.BackColor) ? "black" : "white") + ";";
        }
        public string GetBackStyle3()
        {
            Color altBackColor = HTAlt.Tools.ShiftBrightness(CefForm.Settings.Theme.BackColor, 20, false);
            return "background-color: rgb(" + altBackColor.R + " ," + altBackColor.G + " , " + altBackColor.B + "); color: " + (HTAlt.Tools.IsBright(altBackColor) ? "black" : "white") + ";";
        }

        public static bool isValidKorotPage(string url)
        {
            string[] KorotPages = { "korot://newtab", "korot://incognito", "korot://search", "korot://empty", "korot://licenses", "korot://error", "korot://dad", "korot://me", "korot://sister", "korot://links", "korot://extension", "korot://refresh", "korot://folder", "korot://root" };
            for (int i =0; i < KorotPages.Length;i++)
            {
                if (url.ToLower().StartsWith(KorotPages[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public string GetNewTabItems()
        {
            string x = "";
            if (CefForm.Settings.NewTabSites.FavoritedSite0 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite0) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite1 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite1) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite2 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite2) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite3 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite3) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite4 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite4) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite5 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite5) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite6 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite6) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite7 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite7) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite8 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite8) + "</div>" + Environment.NewLine; }
            if (CefForm.Settings.NewTabSites.FavoritedSite9 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite9) + "</div>" + Environment.NewLine; }
            return x;
        }
        private Attemption lastattempt;
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Url))
            {
                return ResourceHandler.FromString("<html><head><title>Test</title><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><script type=\"text/javascript\">function codeAddress() {history.goBack()}window.onload = codeAddress;</script></head><body></body></html>");
            }
                if (CefForm.Settings.IsUrlAllowed(request.Url))
                { 
                if (schemeName == "korot")
                {
                    if (request.Url.ToLower().StartsWith("korot://newtab"))
                    {
                        return ResourceHandler.FromString(Properties.Resources.newtab.Replace("§ITEMS§", GetNewTabItems()).Replace("§BACKSTYLE3§", GetBackStyle3()).Replace("§BACKSTYLE2§", GetBackStyle2()).Replace("§BACKSTYLE§", GetBackStyle()).Replace("§SEARCHHELP§", CefForm.anaform.SearchHelpText).Replace("§SEARCH§", CefForm.anaform.Search).Replace("§DAYS§", CefForm.anaform.DayNames).Replace("§MONTHS§", CefForm.anaform.MonthNames).Replace("§TITLE§", CefForm.anaform.NewTabtitle).Replace("§EDIT§", CefForm.anaform.NewTabEdit));
                    }
                    else if (request.Url.ToLower().StartsWith("korot://incognito"))
                    {

                        return ResourceHandler.FromString(Properties.Resources.incognito.Replace("§TITLE§", CefForm.anaform.IncognitoT).Replace("§INCTITLE§", CefForm.anaform.IncognitoTitle).Replace("§INCTITLE1§", CefForm.anaform.IncognitoTitle1).Replace("§INCTITLE2§", CefForm.anaform.IncognitoTitle2).Replace("§INCTITLE1M1§", CefForm.anaform.IncognitoT1M1).Replace("§INCTITLE1M2§", CefForm.anaform.IncognitoT1M2).Replace("§INCTITLE1M3§", CefForm.anaform.IncognitoT1M3).Replace("§INCTITLE2M1§", CefForm.anaform.IncognitoT2M1).Replace("§INCTITLE2M2§", CefForm.anaform.IncognitoT2M2).Replace("§INCTITLE2M3§", CefForm.anaform.IncognitoT2M3));
                    }

                    else if (request.Url.StartsWith("korot://search/?q="))
                    {
                        string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                        if (string.IsNullOrWhiteSpace(x))
                        {
                            Console.WriteLine("[EMPTY URL]");
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = korot://error/?e=-4?t=InvalidArgument?u=" + request.Url + " \" />");
                        }
                        if (HTAlt.Tools.ValidUrl(x, CefForm.customProts))
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + x + "\" />");
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + CefForm.Settings.SearchEngine + x + "\" />");
                        }
                    }
                    else if (request.Url.StartsWith("korot://search"))
                    {
                        string x = request.Url.Substring(request.Url.IndexOf("/", 11) + 1);
                        if (string.IsNullOrWhiteSpace(x))
                        {
                            Console.WriteLine("[EMPTY URL]");
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = korot://error/?e=-4?t=InvalidArgument?u=" + request.Url + " \" />");
                        }
                        if (HTAlt.Tools.ValidUrl(x, CefForm.customProts))
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + x + "\" />");
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + CefForm.Settings.SearchEngine + x + "\" />");
                        }
                    }
                    else if (request.Url.ToLower().StartsWith("korot://empty"))
                    {
                        return ResourceHandler.FromString("");
                    }
                    else if (request.Url.ToLower().StartsWith("korot://licenses"))
                    {
                        return ResourceHandler.FromString(Properties.Resources.licenses.Replace("§BACKSTYLE2§", GetBackStyle2()).Replace("§BACKSTYLE§", GetBackStyle()).Replace("§UF§", CefForm.anaform.ubuntuLicense).Replace("§TITLE§", CefForm.anaform.licenseTitle).Replace("§ET§", CefForm.anaform.etLicense).Replace("§K§", CefForm.anaform.kLicense).Replace("§VS§", CefForm.anaform.vsLicense).Replace("§CH§", CefForm.anaform.chLicense).Replace("§CEF§", CefForm.anaform.cefLicense).Replace("§ST§", CefForm.anaform.specialThanks));
                    }
                    else if (request.Url.StartsWith("korot://error"))
                    {
                        string url = request.Url;

                        if (string.IsNullOrWhiteSpace(url.Substring(url.IndexOf("?") + 1)) || url.Remove(url.Length - 1) == "korot://error" || url.Remove(url.Length - 1) == "korot://error/" || url.Remove(url.Length - 1) == "korot://error/?" || url.Remove(url.Length - 1) == "korot://error/?e" || url.Remove(url.Length - 1) == "korot://error/?e=" || url.Remove(url.Length - 1) == "korot://error/?e=?" || url.Remove(url.Length - 1) == "korot://error/?e=?t" || url.Remove(url.Length - 1) == "korot://error/?e=?t=" || url.Remove(url.Length - 1) == "korot://error/?e=?t=?" || url.Remove(url.Length - 1) == "korot://error/?e=?t=?u" || url.Remove(url.Length - 1) == "korot://error/?e=?t=?u=")
                        {
                            Console.WriteLine("[EMPTY URL]");
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = 'korot://error/?e=-4?t=InvalidArgument?u=" + request.Url + "' \" />");
                        } else {
                            int eIndex = url.IndexOf("?e=");
                            int tIndex = url.IndexOf("?t=");
                            int uIndex = url.IndexOf("?u=");
                            string x = url.Substring(eIndex + 3, tIndex - eIndex - 3);
                            string y = url.Substring(tIndex + 3, uIndex - tIndex - 3);
                            string z = url.Substring(uIndex + 3, url.Length - uIndex - 3);
                            if (isValidKorotPage(z))
                            {
                                if (lastattempt != null)
                                {
                                    if (lastattempt.Attempt <= 9)
                                    {
                                        lastattempt.Attempt++;
                                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = " + z + "/ \" />");
                                    }
                                }
                                else
                                {
                                    lastattempt = new Attemption(z, 0);
                                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = " + z + "/ \" />");
                                }
                            }
                            return ResourceHandler.FromString(Properties.Resources.errorpage.Replace("§URL§", z).Replace("§ETEXT§", y).Replace("§OVERLAY§", GetOverlay()).Replace("§BACKSTYLE2§", GetBackStyle2()).Replace("§BACKSTYLE§", GetBackStyle()).Replace("§TITLE§", CefForm.anaform.ErrorPageTitle).Replace("§KT§", CefForm.anaform.KT).Replace("§ET§", CefForm.anaform.ET).Replace("§E1§", CefForm.anaform.E1).Replace("§E2§", CefForm.anaform.E2).Replace("§E3§", CefForm.anaform.E3).Replace("§E4§", CefForm.anaform.E4).Replace("§RT§", CefForm.anaform.RT).Replace("§R1§", CefForm.anaform.R1).Replace("§R2§", CefForm.anaform.R2).Replace("§R3§", CefForm.anaform.R3).Replace("§R4§", CefForm.anaform.R4).Replace("§ERROR§", x));
                        }
                    }
                    else if (request.Url.ToLower().StartsWith("korot://dad"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com \" />");
                    }
                    else if (request.Url.ToLower().StartsWith("korot://me"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com/Korot.html \" />");
                    }
                    else if (request.Url.ToLower().StartsWith("korot://sister"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com/playtroy.html \" />");
                    }
                    else if (request.Url.ToLower().StartsWith("korot://links"))
                    {
                        return ResourceHandler.FromString(Properties.Resources.korotlinks.Replace("§BACKSTYLE2§", GetBackStyle2()));
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
                                        return ResourceHandler.FromString(HTAlt.Tools.ReadFile(cext.Folder + fileLoc, Encoding.UTF8));
                                    }
                                }
                                else
                                {
                                    return ResourceHandler.FromString(HTAlt.Tools.ReadFile(cext.Popup, Encoding.UTF8));
                                }
                            }
                        }
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=BLOCKED \" />");
                    }
                    else if (request.Url.ToLower().StartsWith("korot://refresh"))
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
                                return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=NOT_ACTUAL_KOROT_PAGE \" />");
                            }
                        }
                        else
                        {
                            return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=NOT_ACTUAL_KOROT_PAGE \" />");
                        }
                    }
                    else if (request.Url.ToLower().StartsWith("korot://folder") || request.Url.ToLower().StartsWith("korot://root"))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=NOT_KOROT_PAGE \" />");
                    }
                    else
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?e=FILE_NOT_FOUND \" />");
                    }
                }
                return new ResourceHandler();
            }
            else
            {
                return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =  http://korot://error/?e=BLOCKED \" />");
            }
        }
    }
    
}
