using System.Threading.Tasks;

namespace NabuhEnergyMobile.Services.DataStore
{
    public interface ISettingsDeserializer
    {
        Task SaveDataAsync<T>(T data, string key);

        Task<T> GetDataAsync<T>(string key);

        Task EraseDataAsync(params string[] keys);

        Task<bool> ContainsKey(string key);

        void Shutdown();
    }
}
