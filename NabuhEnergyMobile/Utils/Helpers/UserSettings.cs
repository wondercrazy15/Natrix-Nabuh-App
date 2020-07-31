using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace NabuhEnergyMobile.Utils.Helpers
{
    public class UserSettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string AccesToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(nameof(AccesToken), string.Empty);
            }
            set
            {
                AppSettings.AddOrUpdateValue(nameof(AccesToken), value);
            }
        }
    }
}
