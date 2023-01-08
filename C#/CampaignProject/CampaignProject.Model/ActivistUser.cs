﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class ActivistUser:User
    {
        public int id { get; set; }
        public string address { get; set; }

        public string TwitterAcount { get; set; }

        public decimal Earnings { get; set; }

        public int[] ChosenProducts { get; set; }

        public int[] ChosenCampaings { get; set; }
	
	 
    }
}
