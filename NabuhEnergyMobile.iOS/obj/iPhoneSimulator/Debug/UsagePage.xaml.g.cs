//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("NabuhEnergyMobile.iOS.UsagePage.xaml", "UsagePage.xaml", typeof(global::NabuhEnergyMobile.Views.UsagePage))]

namespace NabuhEnergyMobile.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("/Volumes/Data/Development/LiveProject/Nabuh/Code/Nabuh/Github_clone/Natrix-Nabuh-" +
        "App/NabuhEnergyMobile/Views/UsagePage.xaml")]
    public partial class UsagePage : global::NabuhEnergyMobile.Views.Base.BaseTabView {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.SfChart.XForms.SplineSeries GasChart;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Syncfusion.SfChart.XForms.SplineSeries EnergyChart;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(UsagePage));
            GasChart = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.SfChart.XForms.SplineSeries>(this, "GasChart");
            EnergyChart = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Syncfusion.SfChart.XForms.SplineSeries>(this, "EnergyChart");
        }
    }
}
