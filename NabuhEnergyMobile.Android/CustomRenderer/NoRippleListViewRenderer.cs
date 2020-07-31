using System;
using NabuhEnergyMobile.Controls;
using NabuhEnergyMobile.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomListview), typeof(NoRippleListViewRenderer))]
namespace NabuhEnergyMobile.Droid.CustomRenderer
{
    public class NoRippleListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            Control.SetSelector(Resource.Layout.no_selector);
        }
    }
}
