using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class InsertNewActivistUser : CommandManager, ICommand
    {
            public InsertNewActivistUser(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                try//adding the user to the users table and to Business table
                {
                    Model.ActivistUser Activist = new Model.ActivistUser();
                    Activist = System.Text.Json.JsonSerializer.Deserialize<Model.ActivistUser>((string)param[1]);
                    MainManager.Instance.Activist.InsertNewMember(Activist);

                    return System.Text.Json.JsonSerializer.Serialize("Succes Request");
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