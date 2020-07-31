namespace NabuhEnergyMobile.Utils.Helpers
{
    internal static class Utils
    {
        public enum TimeDuration
        {
            Short = 1000,
            Medium = 2500,
            Long = 5000
        }

        public static int GetTimeDuration(TimeDuration time) => (int)time;
    }
}
