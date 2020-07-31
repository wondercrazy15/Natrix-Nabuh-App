using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Cards;

namespace NabuhEnergyMobile.Services.Cards
{
	public interface ICardService //: IAccessTokenService
	{
		Task<List<Card>> GetListOfCardsAsync();

		Task<List<Card>> DeleteCardAsync(int cardId);

		Task<List<Card>> CreateCardAsync(CreateCardDto card);

		Task<List<Card>> SetPreferredCardAsync(int preferredCardId);
	}
}
