using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class MainManager
    {
        private MainManager() { }

        // Singleton variable
        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance
        {
            get { return instance; }
        }
    }
}
