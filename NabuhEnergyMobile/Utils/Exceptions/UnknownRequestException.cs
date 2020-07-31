using System;

namespace NabuhEnergyMobile.Utils.Exceptions
{
	public class UnknownRequestException : Exception
	{
		public UnknownRequestException()
		{
		}

		public UnknownRequestException(string message) : base(message)
		{

		}

		public UnknownRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
