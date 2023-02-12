using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.OwnerCommands
{
    public class InsertNewOwner : CommandManager, ICommand
    {
        public InsertNewOwner(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                
                try
                {
                    Model.Owner owner = new Model.Owner();
                    owner = System.Text.Json.JsonSerializer.Deserialize<Model.Owner>((string)param[1]);
                    MainManager.Instance.Owner.InsertNewItem(owner);
                    return System.Text.Json.JsonSerializer.Serialize("Task Completed");
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);
                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                Logger.LogError("ID Parameter was not found in GetCampaignID class", LoggingLibrary.LogLevel.Error);

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
            }

        }
    }
}
