using CefSharp;
using System;
using System.Text.RegularExpressions;

namespace Korot
{
    class SchemeHandlerFactory : ISchemeHandlerFactory
    {
        frmMain anaform;
        frmCEF CefForm;
        public SchemeHandlerFactory(frmMain _anaForm, frmCEF _CefForm)
        {
            anaform = _anaForm;
            CefForm = _CefForm;
        }
        public string GetBackStyle()
        {
            if (Properties.Settings.Default.BackStyle == "BACKCOLOR")
            {
                return "background-color: rgb(" + Properties.Settings.Default.BackColor.R + " ," + Properties.Settings.Default.BackColor.G + " , " + Properties.Settings.Default.BackColor.B + ");";
            }
            else
            {
                return "background-image: url(\"" + Properties.Settings.Default.BackStyle.Replace("\\", "/") + "\");";
            }
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
                    
                    return ResourceHandler.FromString(Properties.Resources.newtab.Replace("§BACKSTYLE§", GetBackStyle()).Replace("§SEARCHHELP§", CefForm.SearchHelpText).Replace("§SEARCH§", CefForm.Search).Replace("§DAYS§", CefForm.DayNames).Replace("§MONTHS§", CefForm.MonthNames).Replace("§TITLE§", CefForm.NewTabtitle));
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
                else if (request.Url.StartsWith("korot://error/?e="))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("=") + 1);
                    return ResourceHandler.FromString(Properties.Resources.errorpage.Replace("§BACKSTYLE§",GetBackStyle()).Replace("§TITLE§", CefForm.ErrorPageTitle).Replace("§KT§", CefForm.KT).Replace("§ET§", CefForm.ET).Replace("§E1§", CefForm.E1).Replace("§E2§", CefForm.E2).Replace("§E3§", CefForm.E3).Replace("§E4§", CefForm.E4).Replace("§RT§", CefForm.RT).Replace("§R1§", CefForm.R1).Replace("§R2§", CefForm.R2).Replace("§R3§", CefForm.R3).Replace("§R4§", CefForm.R4) + "<a>" + x + " </a></body>");
                }
                else if (request.Url == "korot://pdp/")
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
