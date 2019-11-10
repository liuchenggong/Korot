using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace Korot
{
    class ResReqHandler : IResourceRequestHandler
    {
        frmMain anaform;
        frmCEF Cefform;
        public ResReqHandler(frmMain _anaform,frmCEF _Cefform)
        {
            anaform = _anaform;
            Cefform = _Cefform;
        }
        public ICookieAccessFilter GetCookieAccessFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return new CookieAccessFilter(anaform,Cefform);
        }

        public IResourceHandler GetResourceHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return null;
        }

        public IResponseFilter GetResourceResponseFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            var url = new Uri(request.Url);
            if (url.Scheme == "korot")
            {
                //Only called for our customScheme
                memoryStream = new MemoryStream();
                return new StreamResponseFilter(memoryStream);
            }

            //return new PassThruResponseFilter();
            return null;
        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            Uri url;
            if (Uri.TryCreate(request.Url, UriKind.Absolute, out url) == false)
            {
                //If we're unable to parse the Uri then cancel the request
                // avoid throwing any exceptions here as we're being called by unmanaged code
                return CefReturnValue.Cancel;
            }

            //Example of how to set Referer
            // Same should work when setting any header

            // For this example only set Referer when using our custom scheme
            if (url.Scheme == "korot")
            {
                //Referrer is now set using it's own method (was previously set in headers before)
                request.SetReferrer("http://google.com", ReferrerPolicy.Default);
            }

            //Example of setting User-Agent in every request.
            //var headers = request.Headers;

            //var userAgent = headers["User-Agent"];
            //headers["User-Agent"] = userAgent + " CefSharp";

            //request.Headers = headers;

            //NOTE: If you do not wish to implement this method returning false is the default behaviour
            // We also suggest you explicitly Dispose of the callback as it wraps an unmanaged resource.
            //callback.Dispose();
            //return false;

            //NOTE: When executing the callback in an async fashion need to check to see if it's disposed
            if (!callback.IsDisposed)
            {
                using (callback)
                {
                    if (request.Method == "POST")
                    {
                        using (var postData = request.PostData)
                        {
                            if (postData != null)
                            {
                                var elements = postData.Elements;

                                var charSet = request.GetCharSet();

                                foreach (var element in elements)
                                {
                                    if (element.Type == PostDataElementType.Bytes)
                                    {
                                        var body = element.GetBody(charSet);
                                    }
                                }
                            }
                        }
                    }

                    //Note to Redirect simply set the request Url
                    //if (request.Url.StartsWith("https://www.google.com", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    request.Url = "https://github.com/";
                    //}

                    //Callback in async fashion
                    //callback.Continue(true);
                    //return CefReturnValue.ContinueAsync;
                }
            }

            return CefReturnValue.Continue;
        }

        public bool OnProtocolExecution(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request)
        {
            return request.Url.StartsWith("mailto");
        }
        
        private MemoryStream memoryStream;
        public void OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            var url = new Uri(request.Url);
            if (url.Scheme == "korot" && memoryStream != null)
            {
//TODO: Do something with the data here
                var data = memoryStream.ToArray();
                var dataLength = data.Length;
                //NOTE: You may need to use a different encoding depending on the request
                var dataAsUtf8String = Encoding.UTF8.GetString(data);
            }
        }

        public void OnResourceRedirect(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
        }

        public bool OnResourceResponse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return true;
        }
    }
}
