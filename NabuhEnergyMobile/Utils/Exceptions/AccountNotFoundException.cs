using System;

namespace NabuhEnergyMobile.Utils.Exceptions
{
	public class AccountNotFoundException : Exception
	{
		public string Content { get; }

		public AccountNotFoundException()
		{

		}

		public AccountNotFoundException(string content) : base(content)
		{
			Content = content;
		}

		public AccountNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
