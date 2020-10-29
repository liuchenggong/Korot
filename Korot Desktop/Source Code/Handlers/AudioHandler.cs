/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/

using CefSharp;
using CefSharp.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korot
{

    // TODO: Work on these 
    // This Interface: http://cefsharp.github.io/api/85.3.x/html/T_CefSharp_IAudioHandler.htm
    // DevTools.WebAudio: http://cefsharp.github.io/api/85.3.x/html/N_CefSharp_DevTools_WebAudio.htm
    // Example: https://github.com/cefsharp/CefSharp/blob/master/CefSharp.Example/Handlers/AudioHandler.cs
    class AudioHandler : IAudioHandler
    {
        public bool GetAudioParameters(IWebBrowser chromiumWebBrowser, IBrowser browser, ref AudioParameters parameters)
        {
            throw new NotImplementedException("0.8.5.0 FEATURE");
        }

        public void OnAudioStreamError(IWebBrowser chromiumWebBrowser, IBrowser browser, string errorMessage)
        {
            throw new NotImplementedException("0.8.5.0 FEATURE");
        }

        public void OnAudioStreamPacket(IWebBrowser chromiumWebBrowser, IBrowser browser, IntPtr data, int noOfFrames, long pts)
        {
            
        }

        public void OnAudioStreamStarted(IWebBrowser chromiumWebBrowser, IBrowser browser, AudioParameters parameters, int channels)
        {
            throw new NotImplementedException("0.8.5.0 FEATURE");
        }

        public void OnAudioStreamStopped(IWebBrowser chromiumWebBrowser, IBrowser browser)
        {
            throw new NotImplementedException("0.8.5.0 FEATURE");
        }
    }
}
