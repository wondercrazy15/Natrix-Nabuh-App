using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Services.AccessToken;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Services.Cards
{
    public class CardService : AccessTokenService, ICardService
    {
        public new IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        public async Task<List<Card>> CreateCardAsync(CreateCardDto card)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var attachedCardsResult = await RequestProviderDep.PostAsync<CreateCardDto, AttachedCardsDto>(UriHelper.BuildRequestUri(GlobalService.CreateCardEndpoint), card, validAccessToken);

            return attachedCardsResult.Cards.ToList();
        }

        public async Task<List<Card>> DeleteCardAsync(int cardId)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var attachedCardsResult = await RequestProviderDep.DeleteAsync<AttachedCardsDto>(UriHelper.BuildRequestUri(GlobalService.DeleteCardEndpoint, $"CardId={cardId}"), validAccessToken);

            return attachedCardsResult.Cards.ToList();
        }

        public async Task<List<Card>> GetListOfCardsAsync()
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var attachedCardsResult = await RequestProviderDep.GetAsync<AttachedCardsDto>(UriHelper.BuildRequestUri(GlobalService.ListOfCardsEndpoint), validAccessToken);

            return attachedCardsResult.Cards.ToList();
        }

        public async Task<List<Card>> SetPreferredCardAsync(int preferredCardId)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var attachedCardsResult = await RequestProviderDep.GetAsync<AttachedCardsDto>(UriHelper.BuildRequestUri(GlobalService.SetPreferredCardEndpoint, $"tCardId={preferredCardId}"), validAccessToken);

            return attachedCardsResult.Cards.ToList();
        }
    }
}
