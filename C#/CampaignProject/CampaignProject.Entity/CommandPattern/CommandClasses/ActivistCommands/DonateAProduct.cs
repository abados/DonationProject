using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi.Core.Events;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class DonateAProduct : CommandManager, ICommand
    {
        public DonateAProduct(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null && param[2] != null)
            {
                Logger.LogEvent("DonateByActivist called", LoggingLibrary.LogLevel.Event);
                try
                {

                    MainManager.Instance.Activist.DonateByActivist(int.Parse((string)param[2]), (string)param[0]);
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
                Logger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");

            }
        }
    }
}