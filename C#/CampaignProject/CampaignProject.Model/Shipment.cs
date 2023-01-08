using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class Shipment
    {
        public string productName { get; set; }
        public decimal price { get; set; }

        public int ActivistBuyerID { get; set; }

        public string fullName { get; set; }
        public string addressToShip { get; set; }

    }
}
