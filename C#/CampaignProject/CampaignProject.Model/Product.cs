using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class Product
    {
        public string productName { get; set; }
        public decimal price { get; set; }

        public int businessID { get; set; }

        public int campaignID { get; set; }

        public bool IsBought { get; set; }

        public bool IsDelivered { get; set; }

        public int ActivistBuyerID { get; set; }
        

    }
}
