using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using NabuhEnergyMobile.Models.Usage;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Usage;

using Xamarin.Forms;


namespace NabuhEnergyMobile.ViewModels
{
    public class UsageViewModel : BaseViewModel
    {
        #region Services
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public IUsageService UsageService => DependencyService.Get<IUsageService>() ?? new UsageService();
        #endregion

       
        #region Init

       
        public override async Task InitializeAsync()
        {
            //Debug.WriteLine("Initialize Edit Profile View");

           // await GetUsageAsync();

            await base.InitializeAsync();
        }

        #region Properties

        private ObservableCollection<DailyReportData> _gasUsagesList = new ObservableCollection<DailyReportData>();
        public ObservableCollection<DailyReportData> GasUsagesList
        {
            get
            {
                return _gasUsagesList;

            }
            set
            {
                _gasUsagesList = value;
                OnPropertyChanged(nameof(GasUsagesList));
            }
        }

        private ObservableCollection<DailyReportData> _ElectricityUsagesList = new ObservableCollection<DailyReportData>();
        public ObservableCollection<DailyReportData> ElectricityUsagesList
        {
            get
            {
                return _ElectricityUsagesList;

            }
            set
            {
                _ElectricityUsagesList = value;
                OnPropertyChanged(nameof(ElectricityUsagesList));
            }
        }

        
        #endregion Properties

        #endregion Init

        public async Task GetUsageAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                DialogServiceDep.ShowLoading();

               var UsagesList = await UsageService.GetUsageHistoryAsync();

               
                GasUsagesList.Clear();
                ElectricityUsagesList.Clear();
                if (UsagesList!=null && UsagesList.Count > 0)
                {


                    foreach (var item in UsagesList)
                    {
                        if (item.FuelType.Equals("gas"))
                        {
                            var month = item.ReportDate.ToString("MMM, yy");

                            GasUsagesList.Add(new DailyReportData
                            {
                                workedDate = month,
                                GasCumulative = Convert.ToDouble(item.GasCumulative),
                                ElectricCumulative0 = Convert.ToDouble(item.ElectricCumulative0)

                            });
                        }
                        else if (item.FuelType.Equals("energy"))
                        {
                            var month = item.ReportDate.ToString("MMM, yy");

                            ElectricityUsagesList.Add(new DailyReportData
                            {
                                workedDate = month,
                                GasCumulative = Convert.ToDouble(item.GasCumulative),
                                ElectricCumulative0 = Convert.ToDouble(item.ElectricCumulative0)

                            });
                        }
                    }
                }
               
                DialogServiceDep.HideLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }

        }

    }
}
