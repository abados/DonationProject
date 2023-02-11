using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommanClasses.NonProfitCommands
{
    public class FindTheBusinnesUser : CommandManager, ICommand
    {

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try //check if the user allready sign as a role
                {

                    MainManager.Instance.myLogger.LogEvent("Search user in the DB: ", LoggingLibrary.LogLevel.Event);
                    string answer = MainManager.Instance.Business.FindTheUser((string)param[0]);
                    return JsonSerializer.Serialize(answer);
                    

                }
                catch (Exception ex)
                {
                    MainManager.Instance.myLogger.LogException(ex.ToString(), ex);

                    return JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                MainManager.Instance.myLogger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return "Faild Request";
            }

        }
    }
}
