using System.Threading.Tasks;
using Acr.UserDialogs;

namespace NabuhEnergyMobile.Services.Dialog
{
    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel) => UserDialogs.Instance.AlertAsync(message, title, buttonLabel);

        public Task<bool> ShowConfirmAsync(string message, string title, string okLabel, string cancelLabel) => UserDialogs.Instance.ConfirmAsync(message, title, okLabel, cancelLabel);

        public void ShowLoading(string message = "Loading") => UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);

        public void HideLoading() => UserDialogs.Instance.HideLoading();

        public void ShowToast(string message, int duration = 5000)
        {
            var toastConfig = new ToastConfig(message);

            toastConfig.SetDuration(duration);

            toastConfig.Position = ToastPosition.Bottom;

            //toastConfig.SetMessageTextColor(System.Drawing.Color.White);

            //toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(255, 0, 0));

            UserDialogs.Instance.Toast(toastConfig);
        }
        
        public void ShowSuccess(string message = "Success", int duration = 5000)
        {
            UserDialogs.Instance.HideLoading();

            var toastConfig = new ToastConfig(message);

            toastConfig.SetDuration(duration);

            toastConfig.Position = ToastPosition.Bottom;

            toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(1, 172, 221));

            UserDialogs.Instance.Toast(toastConfig);
        }

        public void ShowError(string message = "Error. Something went wrong\nPlease try again later", int duration = 5000)
        {
            UserDialogs.Instance.HideLoading();

            var toastConfig = new ToastConfig(message);

            toastConfig.SetDuration(duration);

            toastConfig.Position = ToastPosition.Bottom;

            toastConfig.SetBackgroundColor(System.Drawing.Color.FromArgb(236, 31, 40));

            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}
