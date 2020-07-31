using System;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Token
{
    public class UpdateTokenDto
    {
		[JsonProperty("fireBaseToken")]
		public string fireBaseToken { get; set; }
	}
}
