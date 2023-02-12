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
    public class FindTheBusinessUser : CommandManager, ICommand
    {
        public FindTheBusinessUser(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try //check if the user allready sign as a role
                {

                    Logger.LogEvent("Search user in the DB: ", LoggingLibrary.LogLevel.Event);
                    string answer = MainManager.Instance.Business.FindTheUser((string)param[0]);
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
                Logger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return "Faild Request";
            }

        }
    }
}
