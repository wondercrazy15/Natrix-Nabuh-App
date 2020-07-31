using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Cards
{
	public class CreateCardDto
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("cardType")]
		public string CardType { get; set; }

		[JsonProperty("cardNumber")]
		public string CardNumber { get; set; }

		[JsonProperty("dateFromMM")]
		public string DateFromMM { get; set; }

		[JsonProperty("datefromYY")]
		public string DatefromYY { get; set; }

		[JsonProperty("dateToMM")]
		public string DateToMM { get; set; }

		[JsonProperty("dateToYY")]
		public string DateToYY { get; set; }

		[JsonProperty("issueNumber")]
		public string IssueNumber { get; set; }

		[JsonProperty("cvv")]
		public string Cvv { get; set; }

		[JsonProperty("houseNumber")]
		public string HouseNumber { get; set; }

		[JsonProperty("postcode")]
		public string Postcode { get; set; }

		[JsonProperty("surname")]
		public string Surname { get; set; }

		[JsonProperty("dob")]
		public string Dob { get; set; }
	}
}
