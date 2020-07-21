﻿//MIT License
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
        public string GetNewTabItems()
        {
            string x = "";
            if(CefForm.Settings.NewTabSites.FavoritedSite0 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite0) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite1 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite1) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite2 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite2) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite3 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite3) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite4 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite4) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite5 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite5) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite6 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite6) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite7 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite7) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite8 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite8) + "</div>" + Environment.NewLine; }
            if(CefForm.Settings.NewTabSites.FavoritedSite9 != null) { x += "<div>" + CefForm.Settings.NewTabSites.SiteToHTMLData(CefForm.Settings.NewTabSites.FavoritedSite9) + "</div>" + Environment.NewLine; }
            return x;
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:chrome\:\/\/)|(?:about\:\/\/)|(?:file\:\/\/)|(?:https\:\/\/)|(?:korot\:\/\/)|(?:http:\/\/)|(?:\:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:\/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex Rgx2 = new Regex(@"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx2.IsMatch(s) || Rgx.IsMatch(s);
        }
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (schemeName == "korot")
            {
                if (request.Url == "korot://newtab/")
                {
                    return ResourceHandler.FromString(Properties.Resources.newtab.Replace("§ITEMS§",GetNewTabItems()).Replace("§BACKSTYLE3§",GetBackStyle3()).Replace("§BACKSTYLE2§", GetBackStyle2()).Replace("§BACKSTYLE§", GetBackStyle()).Replace("§SEARCHHELP§", CefForm.SearchHelpText).Replace("§SEARCH§", CefForm.Search).Replace("§DAYS§", CefForm.DayNames).Replace("§MONTHS§", CefForm.MonthNames).Replace("§TITLE§", CefForm.NewTabtitle).Replace("§EDIT§", CefForm.NewTabEdit));
                }
                else if (request.Url == "korot://incognito/")
                {

                    return ResourceHandler.FromString(Properties.Resources.incognito.Replace("§TITLE§", CefForm.IncognitoT).Replace("§INCTITLE§", CefForm.IncognitoTitle).Replace("§INCTITLE1§", CefForm.IncognitoTitle1).Replace("§INCTITLE2§", CefForm.IncognitoTitle2).Replace("§INCTITLE1M1§", CefForm.IncognitoT1M1).Replace("§INCTITLE1M2§", CefForm.IncognitoT1M2).Replace("§INCTITLE1M3§", CefForm.IncognitoT1M3).Replace("§INCTITLE2M1§", CefForm.IncognitoT2M1).Replace("§INCTITLE2M2§", CefForm.IncognitoT2M2).Replace("§INCTITLE2M3§", CefForm.IncognitoT2M3));
                }

                else if (request.Url.StartsWith("korot://search/?q="))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                    if (ValidHttpURL(x))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + x + "\" />");
                    }
                    else
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + CefForm.Settings.SearchEngine + x + "\" />");
                    }
                }
                else if (request.Url.StartsWith("korot://search/"))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("/",11) + 1);
                    if (ValidHttpURL(x))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + x + "\" />");
                    }
                    else
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + CefForm.Settings.SearchEngine + x + "\" />");
                    }
                }
                else if (request.Url == "korot://empty/")
                {
                    return ResourceHandler.FromString("");
                }
                else if (request.Url == "korot://licenses/")
                {
                    return ResourceHandler.FromString(Properties.Resources.licenses.Replace("§BACKSTYLE2§", GetBackStyle2()).Replace("§BACKSTYLE§", GetBackStyle()).Replace("§UF§", CefForm.ubuntuLicense).Replace("§TITLE§", CefForm.licenseTitle).Replace("§ET§", CefForm.etLicense).Replace("§K§", CefForm.kLicense).Replace("§VS§", CefForm.vsLicense).Replace("§CH§", CefForm.chLicense).Replace("§CEF§", CefForm.cefLicense).Replace("§ST§", CefForm.specialThanks));
                }
                else if (request.Url.StartsWith("korot://error/?e="))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                    return ResourceHandler.FromString(Properties.Resources.errorpage.Replace("§OVERLAY§", GetOverlay()).Replace("§BACKSTYLE2§", GetBackStyle2()).Replace("§BACKSTYLE§", GetBackStyle()).Replace("§TITLE§", CefForm.ErrorPageTitle).Replace("§KT§", CefForm.KT).Replace("§ET§", CefForm.ET).Replace("§E1§", CefForm.E1).Replace("§E2§", CefForm.E2).Replace("§E3§", CefForm.E3).Replace("§E4§", CefForm.E4).Replace("§RT§", CefForm.RT).Replace("§R1§", CefForm.R1).Replace("§R2§", CefForm.R2).Replace("§R3§", CefForm.R3).Replace("§R4§", CefForm.R4).Replace("§ERROR§", x));
                }
                else if (request.Url == "korot://dad/")
                {
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com \" />");
                }
                else if (request.Url == "korot://me/")
                {
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com/Korot.html \" />");
                }
                else if (request.Url == "korot://sister/")
                {
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://haltroy.com/playtroy.html \" />");
                }
                else if (request.Url == "korot://links/")
                {
                    return ResourceHandler.FromString(Properties.Resources.korotlinks.Replace("§BACKSTYLE2§", GetBackStyle2()));
                }
                else if (request.Url == "korot://blocked/")
                {
                    return ResourceHandler.FromString("<head><title>Korot</title></head><body><h1>BLOCKED</h1></body>");
                }
                else if (request.Url.StartsWith("korot://extension/"))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("/",11) + 1);
                    // "<meta http-equiv=\"Refresh\" content=\"0; url =" + x + "\" />"
                    if (x.Count(i => (i == '/')) >= 4)
                    {
                        string codename = x.Substring(0, x.IndexOf("/"));
                        if (CefForm.Settings.Extensions.Exists(codename))
                        {
                            Extension cext = CefForm.Settings.Extensions.GetExtensionByCodeName(codename);
                            if (x.Count(i => (i == '/')) >= 5)
                            {
                                string fileLoc = x.Substring(x.IndexOf(codename + "/"));
                                if (cext.FileExists(fileLoc))
                                {
                                    return ResourceHandler.FromString(HTAlt.Tools.ReadFile(cext.Folder + fileLoc,Encoding.UTF8));
                                }
                            }else
                            {
                                return ResourceHandler.FromString(HTAlt.Tools.ReadFile(cext.Popup, Encoding.UTF8));
                            }
                        }
                    }
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://blocked/ \" />");
                }
                else if (request.Url == "korot://refresh/")
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
                else if (request.Url == "korot://folder/" || request.Url == "korot://root/")
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
    }
}
