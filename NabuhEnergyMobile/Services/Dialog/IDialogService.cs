using System.Threading.Tasks;

namespace NabuhEnergyMobile.Services.Dialog
{
    public interface IDialogService
    {
        Task ShowAlertAsync(string message, string title, string buttonLabel);

        void ShowToast(string message, int duration = 5000);

        Task<bool> ShowConfirmAsync(string message, string title, string okLabel, string cancelLabel);

        void ShowLoading(string message = "Loading");

        void HideLoading();

        void ShowSuccess(string message = "Success", int duration = 5000);

        void ShowError(string message = "Error. Something went wrong\nPlease try again later", int duration = 5000);
    }
}
