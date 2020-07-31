using System;
using CoreGraphics;
using Foundation;
using NabuhEnergyMobile.Controls;
using NabuhEnergyMobile.iOS.CustomRenderer;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyWebView), typeof(MyWebViewRenderer))]
namespace NabuhEnergyMobile.iOS.CustomRenderer
{
    public class MyWebViewRenderer: ViewRenderer<MyWebView, WKWebView>
    {
        WKWebView webView;
 //       private NSMutableUrlRequest webRequest;

        protected override void OnElementChanged(ElementChangedEventArgs<MyWebView> e)
        {
            base.OnElementChanged(e);
            try
            {

                if (Control == null)
                {

                    var preferences = new WKPreferences();
                    preferences.JavaScriptEnabled = true;
                    preferences.JavaScriptCanOpenWindowsAutomatically = true;

                    var configuration = new WKWebViewConfiguration();
                    configuration.Preferences = preferences;
                    configuration.AllowsInlineMediaPlayback = true;
                    configuration.MediaPlaybackRequiresUserAction = true;
                    configuration.RequiresUserActionForMediaPlayback = true;

                    CGRect frame = new CGRect(0, 0, 200, 200);
                    webView = new WKWebView(frame, configuration);
                    
                    //webView.NavigationDelegate = new DisplayLinkWebViewDelegate(Element, webRequest);
                    SetNativeControl(webView);
                }
                if (e.NewElement != null)
                {

                    Control.LoadRequest(new NSUrlRequest(new NSUrl(Element.Url)));

                    //SetNativeControl(webView);
                }

            }
            catch (Exception)
            {

            }

        }



        

    }

}

    