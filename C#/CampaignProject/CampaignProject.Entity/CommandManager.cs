using CampaignProject.Entity.CommanClasses;
using CampaignProject.Entity.CommanClasses.NonProfitCommands;
using CampaignProject.Entity.CommandClasses.BusinessCommands;
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
                    // MutualCommands
                    //{"Mutual.ADD", new InsertNewUser() },
                    //{"Mutual.ADD", new FindUserInDB() },


                    // NonProfitUser
                    { "NonProfit.ADD", new InsertNewNonProfitUser() },
                    { "NonProfit.Find", new FindTheUser() },
                    // BusinessUser
                    { "Business.ADD", new InsertNewBusinessUser() },
                    { "Business.Find", new FindTheBusinnesUser() },
                    { "Business.GETMYPRODUCTS", new GetProductsIdonated() },
                    { "Business.DELETEAPRODUCT", new DeleteProduct() },
                    { "Business.UPLOADPRODUCT", new UpluadNewProduct() },
                    { "Business.SHIPIT", new ShipmentManagment() },
                     

                  //  { "Campaign.DELETE", new DeleteOrDeactivateCampagin() },
                  //  { "Campaign.Update",new UpdateCampagin() },
                  //  { "Campaign.GETCAMPAIGNID", new GetCampaignID() },
                  //  { "Campaign.DONATIONAMOUNT",new CampaginDonationAmount() }

                };

                int x = 5;

            }
            catch (Exception ex)
            {

                //MainManager.Instance.Log.AddLogItemToQueue("Faild To Initialie Command List", ex, "Error");
            }

        }

    }

}