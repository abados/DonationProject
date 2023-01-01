using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class Campaign
    {
        public string campaignName { get; set; }
        public string campaignInfo { get; set; }

        public string campaignHashtag { get; set; }
        public string campaignUrl { get; set; }

        public decimal donationAmount { get; set; }
    }
}
