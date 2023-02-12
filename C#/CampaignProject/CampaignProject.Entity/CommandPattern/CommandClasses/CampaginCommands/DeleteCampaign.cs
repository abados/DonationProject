using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CampaginCommands
{
    public class DeleteCampaign : CommandManager, ICommand
    {
        public DeleteCampaign(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                Logger.LogEvent("delete a campaign called ", LoggingLibrary.LogLevel.Event);
                try
                {
                    MainManager.Instance.Campaign.DeleteACampaingByName((string)param[0]);
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
