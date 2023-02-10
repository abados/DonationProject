using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class BasePromotionSystem
    {
        public BasePromotionSystem(Logger log) { Logger = log; }
        public Logger Logger;
    }
}
