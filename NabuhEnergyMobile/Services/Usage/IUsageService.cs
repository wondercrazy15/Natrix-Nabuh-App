using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Usage;

namespace NabuhEnergyMobile.Services.Usage
{
    public interface IUsageService
    {
        Task<List<UserUsage>> GetUsageHistoryAsync();
    }
}
