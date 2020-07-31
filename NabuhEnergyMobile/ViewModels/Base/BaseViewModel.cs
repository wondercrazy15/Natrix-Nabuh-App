using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using NabuhEnergyMobile.Models;
using NabuhEnergyMobile.Services;
using NabuhEnergyMobile.Services.Navigation;
using System.Threading.Tasks;
using NabuhEnergyMobile.Services.Exceptions;
using NabuhEnergyMobile.Services.Login;

namespace NabuhEnergyMobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IHandleExceptionService HandleExceptionServiceDep => DependencyService.Get<IHandleExceptionService>() ?? new HandleExceptionService();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public virtual Task InitializeAsync()
        {
            return Task.FromResult(false);
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        protected Task<string> HandleExceptionAsync(Exception exception,
                                            bool isRequiredToLogout = false) => HandleExceptionServiceDep.HandleExceptionAsync(exception, isRequiredToLogout);


    }
}
