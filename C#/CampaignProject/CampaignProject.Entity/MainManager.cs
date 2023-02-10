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
        private MainManager()
        {
            init();
        }

        public void init()
        {
            Logger.LogItemsQueue = new Queue<LogItem>();
            myLogger = new Logger(Environment.GetEnvironmentVariable("LogConfig"));
            myLogger.LogEvent("program has started", LogLevel.Event);

            Activist = new ActivistManager(myLogger);
            Business = new BusinessManager(myLogger);
            NonProfit = new NonProfitManager(myLogger);
            Owner = new OwnerManager(myLogger);
            Campaign = new CampaignManager(myLogger);
            Product = new ProductManager(myLogger);
            twitterManager = new TwitterManager(myLogger);
            commandManager = new CommandManager();
        }

        // Singleton variable
        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance
        {
            get { return instance; }
        }



        public Logger myLogger { get; set; }

        // Instance of ActivistUser
        // Now I'm able to access to its function
        public ActivistManager Activist { get; set; }
        

        // Instance of BusinessUser
        // Now I'm able to access to its function
        public BusinessManager Business { get; set; }

        // Instance of NonProfitUser
        // Now I'm able to access to its function
        public NonProfitManager NonProfit { get; set; }

        // Instance of OwnerUser
        // Now I'm able to access to its function
        public OwnerManager Owner { get; set; }

        // Instance of Campaign
        // Now I'm able to access to its function
        public CampaignManager Campaign { get; set; }

        // Instance of Product
        // Now I'm able to access to its function
        public ProductManager Product { get; set; }

        // Instance of Twitter
        //Now I'm able to access to its function
        public TwitterManager twitterManager { get; set; }

        // Instance of CommandManager
        //Now I'm able to access to its function
        public CommandManager commandManager { get; set; }
    }
}
