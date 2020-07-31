using Akavache;

namespace NabuhEnergyMobile.Services.DataStore
{
	public interface IPasswordProtectedEncryptionProvider : IEncryptionProvider
	{
		void SetPassword();

		void SetPassword(string password);
	}
}
