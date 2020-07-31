using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Cards;

namespace NabuhEnergyMobile.ViewModels.Base
{
	public interface ITopupViewModel
	{
		void ApplySelectedCard(AttachedCards attachedCard);

		Task MakeTopup(string CVC);

		void OtherAmountChoosen(double otherAmount);

		void ResetOtherAmount();

		void ResetSelectedCard();
	}
}
