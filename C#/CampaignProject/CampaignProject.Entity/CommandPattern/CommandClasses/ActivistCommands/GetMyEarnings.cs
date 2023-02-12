using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class GetMyEarnings : CommandManager, ICommand
    {
        public GetMyEarnings(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                Logger.LogEvent("getEarningsByIDFromDB called", LoggingLibrary.LogLevel.Event);
                try
                {
                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getEarningsByIDFromDB((string)param[0]));
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