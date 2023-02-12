using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class GetMyActiveCampaigns : CommandManager, ICommand
    {
        public GetMyActiveCampaigns(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try
                {
                    var activeList = MainManager.Instance.Activist.getActiveCampaignsOfUserFromDB((string)param[0]);
                    return System.Text.Json.JsonSerializer.Serialize(activeList);
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

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
            }

        }
    }
}