using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class ActiveCampaigns
    {   
        public int id { get; set; }

        public int campaignId { get; set; }

        public string campaignName { get; set; }

        public string campaignHashtag { get; set; }

        public int activeUserId { get; set; }

        public string ActiveUserName{ get; set; }

        public string TwitterAcount { get; set; }

        public string UserEmail { get; set; }

    }
}
