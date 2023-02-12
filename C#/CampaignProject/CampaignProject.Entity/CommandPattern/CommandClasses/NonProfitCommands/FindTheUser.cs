using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommanClasses.NonProfitCommands
{
    public class FindTheUser : CommandManager, ICommand
    {
        public FindTheUser(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try
                {

                    Logger.LogEvent("Search user in the DB: ", LoggingLibrary.LogLevel.Event);
                    string answer = MainManager.Instance.NonProfit.FindTheUser((string)param[0]);
                    return JsonSerializer.Serialize(answer);
                    

                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);

                    return JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                Logger.LogError("ID Parameter was not found in GetCampaignID class", LoggingLibrary.LogLevel.Error);

                return JsonSerializer.Serialize("Faild Request");
            }

        }
    }
}
