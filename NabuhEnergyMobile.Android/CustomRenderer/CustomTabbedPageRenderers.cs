using System;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Text.Style;
using Android.Views;
using Android.Widget;
using NabuhEnergyMobile.Controls;
using NabuhEnergyMobile.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof(CustomTabbedPageRenderers))]
namespace NabuhEnergyMobile.Droid.CustomRenderer
{
    public class CustomTabbedPageRenderers : TabbedPageRenderer
    {
        private Android.Views.View formViewPager = null;
        private TabLayout tabLayout = null;

       

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            this.tabLayout = (TabLayout)this.GetChildAt(1);

            changeTabsFont();

        }
        private void changeTabsFont()
        {
            ViewGroup vg = (ViewGroup)tabLayout.GetChildAt(0);
            int tabsCount = vg.ChildCount;
            for (int j = 0; j < tabsCount; j++)
            {
                ViewGroup vgTab = (ViewGroup)vg.GetChildAt(j);
                int tabChildsCount = vgTab.ChildCount;
                for (int i = 0; i < tabChildsCount; i++)
                {
                    Android.Views.View tabViewChild = vgTab.GetChildAt(i);
                    if (tabViewChild is TextView)
                    {
                        //((TextView)tabViewChild).Typeface = font;
                        ((TextView)tabViewChild).TextSize = 6;

                    }
                }
            }

        }
    }
}
