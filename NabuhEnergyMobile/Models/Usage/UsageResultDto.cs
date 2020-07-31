using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Models.History;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Usage
{
    public class UsageResultDto
    {
        [JsonProperty("usageList")]
        public IList<UserUsage> UsageList { get; set; }

      
    }
}
