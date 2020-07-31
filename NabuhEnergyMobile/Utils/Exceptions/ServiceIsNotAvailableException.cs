using System;

namespace NabuhEnergyMobile.Utils.Exceptions
{
	public class ServiceIsNotAvailableException : Exception
	{
		public ServiceIsNotAvailableException()
		{
		}

		public ServiceIsNotAvailableException(string message) : base(message)
		{

		}

		public ServiceIsNotAvailableException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
