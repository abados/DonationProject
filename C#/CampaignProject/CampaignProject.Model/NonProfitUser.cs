using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class NonProfitUser:User
    {
        public string organizationUrl { get; set; }

        public string organizationDescription { get; set; }

        public string organizationName { get; set; }

    }
}
