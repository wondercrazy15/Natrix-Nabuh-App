//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("NabuhEnergyMobile.iOS.AddCardPopupView.xaml", "AddCardPopupView.xaml", typeof(global::NabuhEnergyMobile.Views.Popup.AddCardPopupView))]

namespace NabuhEnergyMobile.Views.Popup {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("/Volumes/Data/Development/LiveProject/Nabuh/Code/Nabuh/Github_clone/Natrix-Nabuh-" +
        "App/NabuhEnergyMobile/Views/Popup/AddCardPopupView.xaml")]
    public partial class AddCardPopupView : global::Rg.Plugins.Popup.Pages.PopupPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry CardNumberEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry ExpirationEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry CardCvvEntry;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AddCardPopupView));
            CardNumberEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "CardNumberEntry");
            ExpirationEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "ExpirationEntry");
            CardCvvEntry = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "CardCvvEntry");
        }
    }
}
