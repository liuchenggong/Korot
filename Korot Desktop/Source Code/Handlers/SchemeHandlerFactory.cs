using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CefSharp;

namespace Korot
{
    class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        frmMain anaform;
        frmCEF CefForm;
        public SchemeHandlerFactory(frmMain _anaForm,frmCEF _CefForm)
        {
            anaform = _anaForm;
            CefForm = _CefForm;
        }
        public static bool ValidHttpURL(string s)
        {
            string Pattern = @"^(?:about)|(?:about)|(?:file)|(?:korot)|(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(s);
        }
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (schemeName == "korot")
            {
                if (request.Url == "korot://newtab/")
                {
                    return ResourceHandler.FromString(Properties.Resources.newtab.Replace("§BACKSTYLE§", Properties.Settings.Default.BackStyle).Replace("§SEARCHHELP§", CefForm.SearchHelpText).Replace("§SEARCH§", CefForm.Search).Replace("§DAYS§", CefForm.DayNames).Replace("§MONTHS§", CefForm.MonthNames).Replace("§TITLE§", CefForm.NewTabtitle));
                }else if (request.Url.StartsWith("korot://search/?q="))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                    if (ValidHttpURL(x))
                    {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" +  x + "\" />");
                    } else {
                        return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + Properties.Settings.Default.SearchURL + x + "\" />");
                    }
                    }
                else if (request.Url == "korot://empty/")
                {
                    return ResourceHandler.FromString("");
                }
                else if (request.Url == "korot://licenses/")
                {
                    return ResourceHandler.FromString(Properties.Resources.licenses);
                }
                else if (request.Url == "korot://settings/")
                {
                    if(frame.IsMain)
                    {
                        string x = "<head><title>Korot Settings</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" + Environment.NewLine +
                        "<a> Homepage: " + Properties.Settings.Default.Homepage + "</a>" + Environment.NewLine +
                    "<a> Window : ( Height:" + Properties.Settings.Default.WindowSizeH + " Width:" + Properties.Settings.Default.WindowSizeW + " X:" + Properties.Settings.Default.WindowPosX + " Y:" + Properties.Settings.Default.WindowPosY + ")</a>" + Environment.NewLine +
                        "<a> SearchUrl: " + Properties.Settings.Default.SearchURL + "</a>" + Environment.NewLine +
                        "<a> Download: ( OpenFile:" + Properties.Settings.Default.downloadOpen + " CloseForm:" + Properties.Settings.Default.downloadClose + ")</a>" + Environment.NewLine +
                    "<a> LangFile: " + Properties.Settings.Default.LangFile + "</a>" + Environment.NewLine +
                        "<a> Color: (BackColor:" + Properties.Settings.Default.BackColor.ToString() + " OverlayColor:" + Properties.Settings.Default.OverlayColor.ToString() + " BackStyle:" + Properties.Settings.Default.BackStyle + ")</a>" + Environment.NewLine +
                        "<a> Theme: " + Properties.Settings.Default.ThemeFile + "</a></body>";
                        return ResourceHandler.FromString(x);

                    }else
                    {
                        return ResourceHandler.FromString("This feature is not allowed to show in this kind of frame. Please visit https://www.github.com/haltroy/korot/issues/20 for more information");
                    }
                }
                else if (request.Url == "korot://settings/history/")
                {
                    if (frame.IsMain)
                    {
                        string x = "<head><title>Korot Settings - History</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a>" + Properties.Settings.Default.History + "</a></body>";
                    return ResourceHandler.FromString(x);
                    }
                    else
                    {
                        return ResourceHandler.FromString("This feature is not allowed to show in this kind of frame. Please visit https://www.github.com/haltroy/korot/issues/20 for more information");
                    }
                }
                else if (request.Url == "korot://settings/download/")
                {
                        if (frame.IsMain)
                        {
                            string x = "<head><title>Korot Settings - Download History</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a>" + Properties.Settings.Default.DowloadHistory + "</a></body>";
                    return ResourceHandler.FromString(x);
                    }
                    else
                    {
                        return ResourceHandler.FromString("This feature is not allowed to show in this kind of frame. Please visit https://www.github.com/haltroy/korot/issues/20 for more information");
                    }
                }
                else if (request.Url == "korot://settings/favorites/")
                {
                            if (frame.IsMain)
                            {
                                string x = "<head><title>Korot Settings - Favorites</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a>" + Properties.Settings.Default.Favorites + "</a></body>";
                    return ResourceHandler.FromString(x);
                    }
                    else
                    {
                        return ResourceHandler.FromString("This feature is not allowed to show in this kind of frame. Please visit https://www.github.com/haltroy/korot/issues/20 for more information");
                    }
                }
               
                else if (request.Url.StartsWith("korot://error/?e="))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                    return ResourceHandler.FromString(Properties.Resources.errorpage.Replace("§TITLE§",CefForm.ErrorPageTitle).Replace("§KT§",CefForm.KT).Replace("§ET§", CefForm.ET).Replace("§E1§", CefForm.E1).Replace("§E2§", CefForm.E2).Replace("§E3§", CefForm.E3).Replace("§E4§", CefForm.E4).Replace("§RT§", CefForm.RT).Replace("§R1§", CefForm.R1).Replace("§R2§", CefForm.R2).Replace("§R3§", CefForm.R3).Replace("§R4§", CefForm.R4) + "<a>" + x + " </a></body>");
                }else if (request.Url == "korot://pdp/")
                {
                    Output.WriteLine("yyyyyyyyyyyyyyyyyyssoo+++/////+++++oosyyyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyyyyso+/:-.......................-:/oyyyyyyy");
                    Output.WriteLine("yyyyyo/:-.......-://+oossyyyyyyyssoo+/-...../syyyy");
                    Output.WriteLine("yys:.....-/osyhdmmmmmmdddddmmmmmmmmmmmmds/....+yyy");
                    Output.WriteLine("yy:...:ydmmmmdhhddmdhsooooosyyyyyssyyhdmmmh/.../yy");
                    Output.WriteLine("yy/...-mmmmhsoooooooooooooooooooooooooohmmmd/...+y");
                    Output.WriteLine("yyo....dmmdooooooooooooooooooooooooooooodmmmh...-y");
                    Output.WriteLine("yyy....ymmdoooooooooooooooooooooooooooooymmmm:...o");
                    Output.WriteLine("yyy/...+mmdoooooooooooooooooooysooooooooymmmm/...+");
                    Output.WriteLine("yyyo...-dmdooooooooohsooooooooyhoooooooosmmmm/...+");
                    Output.WriteLine("yyyy-...ymdooooooooohyoooooooosdooooooooymmmm:...+");
                    Output.WriteLine("yyyy+.../mdooooooooohdooooooooodsooooooodmmmh....s");
                    Output.WriteLine("yyyyy....dmyoooooooosmsooooooooddsooooydmmmm/.../y");
                    Output.WriteLine("yyyyy/...omdhsooooooymdyoooooshmmmdddmmmmmdo...-yy");
                    Output.WriteLine("yyyyys...-dmmmddhhhhysoosyyyydmmmmmmmmmmdy:...-syy");
                    Output.WriteLine("yyyyyy:...smmmdsoooooooooooooosdmmmmmdyo-..../yyyy");
                    Output.WriteLine("yyyyyys...-dmmdsoooooooooooooosdmds+/-....-+syyyyy");
                    Output.WriteLine("yyyyyyy:...ommmdyooooosssyyyhdmmmd....-:+syyyyyyyy");
                    Output.WriteLine("yyyyyyys....hmmmmmmmmmmmmmmmmmmmmm-...syyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyy/...:dmmmmmmmmmmmmmmmmmmmm-...+yyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyyy-...smmmmmmmmmmmmmmmmmmmh....+yyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyyyo....hmmmmmmmmmddhyso+:-....:yyyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyyyy+...-/////::-..........-:/syyyyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyyyyy/............--::/+oosyyyyyyyyyyyyyyyyy");
                    Output.WriteLine("yyyyyyyyyyyysooooooosssyyyyyyyyyyyyyyyyyyyyyyyyyyy");
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://www.youtube.com/channel/UC-lHJZR3Gqxm24_Vd_AJ5Yw \" />");

                }
                else if (request.Url == "korot://dad/")
                {
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMNdmmmmmNMMMMMMNmmmmmmmmmmdddddddddddddddddddddMM");
                    Output.WriteLine("MMNs++++++dMMMMMMy+++++++++++++++++++++++++++++yMM");
                    Output.WriteLine("MMMMs++++++hMMMMMMy+++++++++++++++++++++++++++sMMM");
                    Output.WriteLine("MMMMMs++++++hMMMMMMyooooooooooooo+++++++ooooosMMMM");
                    Output.WriteLine("MMMMMMy++++++hMMMMMMMMMMMMMMMMMMNs++++++sNMMMMMMMM");
                    Output.WriteLine("MMMMMMMy++++++yMMMMMMyssssssdMMMMNs++++++omMMMMMMM");
                    Output.WriteLine("MMMMMMMMy++++++yMMMMMNy++++++hMMMMMh+++++oNMMMMMMM");
                    Output.WriteLine("MMMMMMMMMy++++++yMMMMMMy++++++dMMMMMh+++sNMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMy++++++sMMMMds+++++++dMMMMMdoyMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMh++++++ydyo++++++++++dMMMMMNMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMh++++++++++++++++++++dMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMh++++++++++oydh++++++hMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMh+++++++hNMMMMd+++++sMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMh++++++sNMMMMMd+++hMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMd++++++oNMMMMMdomMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMdo+++++oNMMMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMd++++++oNMMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMmo+++++omMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMm++++++oMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMMmo+++oNMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMMMmo+sNMMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMmyNMMMMMMMMMMMMMMMMMMMMMMMM");
                    Output.WriteLine("MMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMM");
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://haltroy.com \" />");
                }
                else if (request.Url == "korot://me/")
                {
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot.haltroy.com \" />");
                }
                else if (request.Url == "korot://sister/")
                {
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://playtroy.haltroy.com \" />");
                }else if (request.Url == "korot://links/")
                {
                    return ResourceHandler.FromString(Properties.Resources.korotlinks);
                }
                else
                {
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = http://korot://error/?FILE_NOT_FOUND \" />");
                }
                
            }
            return new ResourceHandler();
        }
    }
}
