using System;
using Foundation;
using WebKit;

namespace NabuhEnergyMobile.iOS.CustomRenderer
{
    public class MyNavigationDelegate : WKNavigationDelegate
    {
        private readonly MyWebViewRenderer _renderer;

        public MyNavigationDelegate(MyWebViewRenderer renderer)
        {
            _renderer = renderer;
        }

        public override void DidFailProvisionalNavigation(WKWebView webView, WKNavigation navigation, NSError error)
        {
            // call methods of your renderer or its properties like
            //_renderer.Element.OnNavigating(webView.Url);
        }
    }
}