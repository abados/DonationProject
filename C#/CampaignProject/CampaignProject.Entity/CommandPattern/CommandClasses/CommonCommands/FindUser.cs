using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CommonCommands
{
    public class FindUser : CommandManager, ICommand
    {
        public FindUser(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[3] != null && param[0] != null)
            {
                try //check if the user allready sign as a role
                {
                    string answer="";
                    Logger.LogEvent("Search user in the DB:", LoggingLibrary.LogLevel.Event);
                    if (param[3].Equals("Activist"))
                    {
                        answer = MainManager.Instance.Activist.FindTheUser((string)param[0]);
                    }
                    else if (param[3].Equals("NonProfit"))
                    {
                        answer = MainManager.Instance.NonProfit.FindTheUser((string)param[0]);
                    }
                    else if (param[3].Equals("Business"))
                    {
                        answer = MainManager.Instance.Business.FindTheUser((string)param[0]);
                    }
                    else 
                    {
                        answer = MainManager.Instance.Owner.FindTheUser((string)param[0]);
                    }


                    return System.Text.Json.JsonSerializer.Serialize(answer);


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
