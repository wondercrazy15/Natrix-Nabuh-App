extern alias SplatAlias;
using Akavache;
using System;
using System.Reactive.Linq;
using Akavache.Sqlite3;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using NabuhEnergyMobile.Values;
using SplatAlias::Splat;

namespace NabuhEnergyMobile.Services.DataStore
{
    public class SettingsDeserializer : ISettingsDeserializer
    {
        private IBlobCache _cache;
        private IFilesystemProvider _filesystemProvider;
        private CustomEncryptionProvider _customEncryptionProvider;

        IBlobCache NewBlobCache()
        {
            Akavache.BlobCache.ApplicationName = GlobalStrings.DataStore;

            _filesystemProvider = SplatAlias::Splat.Locator.Current.GetService<IFilesystemProvider>();

            var cacheFolder = _filesystemProvider.GetDefaultSecretCacheDirectory();

            _filesystemProvider.CreateRecursive(cacheFolder).SubscribeOn(BlobCache.TaskpoolScheduler).Wait();

            _customEncryptionProvider = new CustomEncryptionProvider();

            _customEncryptionProvider.SetPassword();

            //Splat.Locator.CurrentMutable.Register(() => _customEncryptionProvider, typeof(Akavache.IEncryptionProvider));

            return new SQLiteEncryptedBlobCache(Path.Combine(cacheFolder, $"{GlobalStrings.DataStore}.db"), _customEncryptionProvider, BlobCache.TaskpoolScheduler);
        }

        public SettingsDeserializer()
        {
            _cache = NewBlobCache();
        }

        public void Shutdown()
        {
            if (_cache != null)
            {
                _cache.Dispose();
                _cache.Shutdown.Wait();
                _cache = NewBlobCache();
            }
        }

        public async Task<bool> ContainsKey(string key)
        {
            var keys = await _cache.GetAllKeys();

            return keys.Contains(key);
        }

        public async Task SaveDataAsync<T>(T data, string key)
        {
            _customEncryptionProvider.SetPassword();

            await _cache.InsertObject(key, data);
        }

        public async Task<T> GetDataAsync<T>(string key)
        {
            _customEncryptionProvider.SetPassword();

            T data = await _cache.GetObject<T>(key);

            return data;
        }

        public async Task EraseDataAsync(params string[] keys)
        {
            _customEncryptionProvider.SetPassword();

            foreach (var currentKey in keys)
            {
                await _cache.Invalidate(currentKey);
            }
        }
    }
}
