using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class GetAllOrganizations : CommandManager, ICommand
    {
        public GetAllOrganizations(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("getNonProfitListFromDB called: ", LoggingLibrary.LogLevel.Event);
                try
                {
                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getNonProfitListFromDB());
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