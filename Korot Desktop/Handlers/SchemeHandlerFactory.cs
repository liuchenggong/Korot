using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            if (schemeName == "korot")
            {
                if (request.Url == "korot://newtab/")
                {
                    return ResourceHandler.FromString(anaform.NewTabHTML.Replace("§BACKSTYLE§",Properties.Settings.Default.BackStyle));
                }else if (request.Url.StartsWith("korot://search/?"))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("?") + 1);
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url =" + Properties.Settings.Default.SearchURL + x +"\" />");
                }
                else if (request.Url == "korot://settings/")
                {
                    string x = "<head><title>Korot Settings</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a> Homepage: " + Properties.Settings.Default.Homepage + "</a>" +
                        "<a> Window : ( Height:" + Properties.Settings.Default.WindowSizeH + " Width:" + Properties.Settings.Default.WindowSizeW + " X:" + Properties.Settings.Default.WindowPosX + " Y:" + Properties.Settings.Default.WindowPosY + ")</a>" +
                        "<a> SearchUrl: " + Properties.Settings.Default.SearchURL + "</a>" +
                        "<a> Download: ( OpenFile:" + Properties.Settings.Default.downloadOpen + " CloseForm:" + Properties.Settings.Default.downloadClose + ")</a>" +
                        "<a> LangFile: " + Properties.Settings.Default.LangFile + "</a>" +
                        "<a> Color: (BackColor:" + Properties.Settings.Default.BackColor.ToArgb().ToString() + " OverlayColor:" + Properties.Settings.Default.OverlayColor.ToArgb().ToString() + " BackStyle:" + Properties.Settings.Default.BackStyle + ")</a>" +
                        "<a> Theme: " + Properties.Settings.Default.ThemeFile + "</a></body>";
                    return ResourceHandler.FromString(x);

                }
                else if (request.Url == "korot://settings/history/")
                {
                    string x = "<head><title>Korot Settings - History</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a>" + Properties.Settings.Default.History + "</a></body>";
                    return ResourceHandler.FromString(x);
                }
                else if (request.Url == "korot://settings/download/")
                {
                    string x = "<head><title>Korot Settings - Download History</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a>" + Properties.Settings.Default.DowloadHistory + "</a></body>";
                    return ResourceHandler.FromString(x);
                }
                else if (request.Url == "korot://settings/favorites/")
                {
                    string x = "<head><title>Korot Settings - Favorites</title></head><body><h1>" + Properties.Settings.Default.LastUser + "</h1>" +
                        "<a>" + Properties.Settings.Default.Favorites + "</a></body>";
                    return ResourceHandler.FromString(x);
                }
                else if (request.Url == "korot://defaultsettings/")
                {
                    string x = "<head><title>Korot Settings</title></head><body><h1>user0</h1>" +
    "<a> Homepage: korot://newtab</a>" +
    "<a> Window : ( Height:0 Width:0 X:0 Y:0)</a>" +
    "<a> SearchUrl: https://www.google.com/search?q=</a>" +
    "<a> Download: ( OpenFile:false CloseForm:false)</a>" +
    "<a> LangFile: English.lang</a>" +
    "<a> Color: (BackColor:White OverlayColor:DodgerBlue BackStyle:background-color: #ffffff)</a>" +
    "<a> Theme: Korot Light.ktf</a>" +
    "<a> Empty Lists: History , Download History, Favorites</body>";
                    return ResourceHandler.FromString(x);
                }
                else if (request.Url.StartsWith("korot://error/?"))
                {
                    string x = request.Url.Substring(request.Url.IndexOf("?") + 1);
                    return ResourceHandler.FromString(anaform.ErrorHTML + "<a style=\"font - family: Modern, Arial; \">" + x + " </a></body>");
                }else if (request.Url == "korot://100m/")
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
                    // request.Url = "https://www.youtube.com/watch?v=zlPxW56jfLU";
                    return ResourceHandler.FromString("<meta http-equiv=\"Refresh\" content=\"0; url = https://www.youtube.com/watch?v=zlPxW56jfLU \" />");

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
