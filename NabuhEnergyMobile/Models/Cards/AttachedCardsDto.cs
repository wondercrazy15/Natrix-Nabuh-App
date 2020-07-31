using System.Collections.Generic;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Cards
{
    public class AttachedCardsDto
    {
		[JsonProperty("cards")]
		public IList<Card> Cards { get; set; }
	}
}
