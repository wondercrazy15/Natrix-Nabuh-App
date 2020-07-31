using System;

namespace NabuhEnergyMobile.Models.Account
{
	public class AccessToken
	{
		public string Token { get; set; }

		public DateTimeOffset DateTimeCreated { get; set; }
	}
}
