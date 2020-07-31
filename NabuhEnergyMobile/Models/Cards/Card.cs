using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Cards
{
	public class Card
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("dateCreated")]
		public string DateCreated { get; set; }

		[JsonProperty("cardType")]
		public string CardType { get; set; }

		[JsonProperty("last4Digits")]
		public string Last4Digits { get; set; }

		[JsonProperty("cardHash")]
		public string CardHash { get; set; }

		[JsonProperty("cardReference")]
		public string CardReference { get; set; }

		[JsonProperty("cvv")]
		public string Cvv { get; set; }

		[JsonProperty("preferred")]
		public bool Preferred { get; set; }
	}
}
