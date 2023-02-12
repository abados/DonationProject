using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CampaginCommands
{
    public class UpdateCampaign : CommandManager, ICommand
    {
        public UpdateCampaign(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("Update a campaign called ", LoggingLibrary.LogLevel.Event);
                try
                {
                  
                    Model.Campaign campaignTOupdate = System.Text.Json.JsonSerializer.Deserialize<Model.Campaign>((string)param[1]);
                    MainManager.Instance.Campaign.UpdateAProductInDb((string)param[0], campaignTOupdate);
                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");

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
