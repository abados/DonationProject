using CampaignProject.Entity.CommanClasses;
using CampaignProject.Entity.CommanClasses.NonProfitCommands;
using CampaignProject.Entity.CommandClasses.BusinessCommands;
using CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands;
using CampaignProject.Entity.CommandPattern.CommandClasses.CampaginCommands;
using CampaignProject.Entity.CommandPattern.CommandClasses.OwnerCommands;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public interface ICommand
    {
        object ExecuteCommand(params object[] param);
    }

    public class CommandManager:BaseEntity
    {

        public CommandManager(Logger log) : base(log)
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
                Logger.LogEvent("Command List Initialization", LoggingLibrary.LogLevel.Event);

                _CommandList = new Dictionary<string, ICommand>
                {
                    // MutualCommands
                    //{"Mutual.ADD", new InsertNewUser() },
                    //{"Mutual.Find", new FindUserInDB() },


                    // NonProfitUser
                    { "NonProfit.ADD", new InsertNewNonProfitUser(Logger) },
                    { "NonProfit.Find", new FindTheUser(Logger) },
                    // BusinessUser
                    { "Business.ADD", new InsertNewBusinessUser(Logger) },
                    { "Business.Find", new FindTheBusinessUser(Logger) },
                    { "Business.GETMYPRODUCTS", new GetProductsIdonated(Logger) },
                    { "Business.DELETEAPRODUCT", new DeleteProduct(Logger) },
                    { "Business.UPLOADPRODUCT", new UpluadNewProduct(Logger) },
                    { "Business.SHIPIT", new ShipmentManagment(Logger) },
                    // Activist
                    { "Activist.ADD", new InsertNewActivistUser(Logger) },
                    { "Activist.Find", new FindTheActivistUser(Logger) },
                    { "Activist.GETORGANIZATIONS", new GetAllOrganizations (Logger) },
                    { "Activist.PURCHES", new PurchesAProduct (Logger) },
                    { "Activist.SIGNCAMPAIGN", new SignMeToCampaign (Logger) },
                    { "Activist.GETMYPRODUCTS", new GetMyProducts(Logger) },
                    { "Activist.GETEARNINGS", new GetMyEarnings (Logger) },
                    { "Activist.Donate", new DonateAProduct (Logger) },
                    { "Activist.GETACTIVECAMPAIGN", new GetMyActiveCampaigns(Logger) },
                    // Owner
                     { "Owner.ADD", new InsertNewOwner(Logger) },
                    { "Owner.Find", new FindOwnerUser(Logger) },
                    { "Owner.REPORT", new OwnerReports(Logger) },
                    // Campaign
                    { "Campaign.ADD", new AddCampaign(Logger)},
                    { "Campaign.GET", new GetAllOrSpecificCampaignsList(Logger)},
                    { "Campaign.UPDATE", new UpdateCampaign(Logger)},
                    { "Campaign.DELETE", new DeleteCampaign(Logger)},
                    { "Campaign.ROLE", new GetRole(Logger)},

                 
                };

            }
            catch (Exception ex)
            {

                //MainManager.Instance.Log.AddLogItemToQueue("Faild To Initialie Command List", ex, "Error");
            }

        }

    }

}