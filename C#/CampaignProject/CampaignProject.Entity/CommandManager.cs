using CampaignProject.Entity.CommanClasses;
using CampaignProject.Entity.CommanClasses.NonProfitCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public interface ICommand
    {
        object ExecuteCommand(params object[] param);
    }

    public class CommandManager
    {

        public CommandManager()
        {

        }

        private Dictionary<string, ICommand> _CommandList;

        public Dictionary<string, ICommand> CommandList
        {
            get
            {
                if (_CommandList == null) Init();
                return _CommandList;
            }
        }

        private void Init()
        {
            try
            {
                //MainManager.Instance.Log.AddLogItemToQueue("Command List Initialization", null, "Event");

                _CommandList = new Dictionary<string, ICommand>
                {
                    
                    // NonProfit
                    { "NonProfit.ADD", new InsertNewNonProfitUser() },
                    { "NonProfit.Find", new FindTheUser() },
                  //  { "Campaign.DELETE", new DeleteOrDeactivateCampagin() },
                  //  { "Campaign.Update",new UpdateCampagin() },
                  //  { "Campaign.GETCAMPAIGNID", new GetCampaignID() },
                  //  { "Campaign.DONATIONAMOUNT",new CampaginDonationAmount() }

                };

            }
            catch (Exception ex)
            {

                //MainManager.Instance.Log.AddLogItemToQueue("Faild To Initialie Command List", ex, "Error");
            }

        }

    }

}