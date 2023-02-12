using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    internal class FindTheActivistUser : CommandManager, ICommand
    {
        public FindTheActivistUser(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try //check if the user allready sign as a role
                {

                    Logger.LogEvent("Search user in the DB: ", LoggingLibrary.LogLevel.Event);
                    string answer = MainManager.Instance.Activist.FindTheUser((string)param[0]);
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

                return "Faild Request";
            }

        }
    }
}

