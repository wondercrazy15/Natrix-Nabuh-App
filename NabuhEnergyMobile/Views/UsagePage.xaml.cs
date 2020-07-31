using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using NabuhEnergyMobile.ViewModels;
using NabuhEnergyMobile.Views.Base;

using Xamarin.Forms;


namespace NabuhEnergyMobile.Views
{
    public partial class UsagePage : BaseTabView
    {
        private readonly UsageViewModel usageViewModel;

      

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                await usageViewModel.GetUsageAsync();

                GasChart.ItemsSource = usageViewModel.GasUsagesList;
                EnergyChart.ItemsSource = usageViewModel.ElectricityUsagesList;
            }
            catch (Exception)
            {

            }

        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }

        public UsagePage()
        {
            try
            {
                InitializeComponent();

                base.BindingContext = usageViewModel = new UsageViewModel();
                
               
            }
            catch (Exception)
            {

            }

           
        }
    }
}
