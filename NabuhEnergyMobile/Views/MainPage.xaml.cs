using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using NabuhEnergyMobile.Views.Base;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace NabuhEnergyMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        private bool _isInitialized;

        public MainPage()
        {

            InitializeComponent();
           
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
          


            this.CurrentPage = this.Children[2];
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            try
            {
                if (_isInitialized)
                {
                    var currentPage = ((NavigationPage)this.CurrentPage).CurrentPage as BaseTabView;
                    currentPage.InitViewModel();
                }
                else
                {
                    _isInitialized = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}