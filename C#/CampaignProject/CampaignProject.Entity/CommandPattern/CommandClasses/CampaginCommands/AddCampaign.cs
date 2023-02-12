using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CampaginCommands
{
    public class AddCampaign : CommandManager, ICommand
    {
        public AddCampaign(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null && param[0] != null)
            {
                Logger.LogEvent("adding campaign to the DB ", LoggingLibrary.LogLevel.Event);
                try
                {
                    Model.Campaign campaign = new Model.Campaign();
                    campaign = (Model.Campaign)param[1];
                    MainManager.Instance.Campaign.InsertNewItem(campaign, (string)param[0]);
                    return System.Text.Json.JsonSerializer.Serialize("Task Completed");
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);

                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                Logger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return "Faild Request";
            }

        }
    }
}
