using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Profile;

namespace NabuhEnergyMobile.Services.EditProfile
{
	public interface IEditProfileService 
	{
		Task<bool> UpdateUserAsync(EditProfileDto updateAccount);
	}
}
