using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class MainManager
    {
        private MainManager() {

            init();
        }

        public void init()
        {
            Logger.LogItemsQueue = new Queue<LogItem>();
            Logger myLogger = new Logger("File");
            //Logger.LogItemsQueue = new Queue<LogItem>();
            Logger.LogEvent("program has started", LogLevel.Event);

       
        }

        // Singleton variable
        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance
        {
            get { return instance; }
        }


        // Instance of ActivistUser
        // Because of it I can access to its function
        public ActivistManager Activist = new ActivistManager();

        // Instance of BusinessUser
        // Because of it I can access to its function
        public BusinessManager Business = new BusinessManager();

        // Instance of NonProfitUser
        // Because of it I can access to its function
        public NonProfitManager NonProfit = new NonProfitManager();

        // Instance of OwnerUser
        // Because of it I can access to its function
        public OwnerManager Owner = new OwnerManager();

        // Instance of Campaign
        // Because of it I can access to its function
        public CampaignManager Campaign = new CampaignManager();

        // Instance of Product
        // Because of it I can access to its function
        public ProductManager Product = new ProductManager();

        public TwitterManager twitterManager = new TwitterManager();
    }
}
