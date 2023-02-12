using LoggingLibrary;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class SignMeToCampaign : CommandManager, ICommand
    {
        public SignMeToCampaign(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
               Logger.LogEvent("sign user to campaign", LoggingLibrary.LogLevel.Event);
                try
                {
                    
                    dynamic dataSIGN = JsonConvert.DeserializeObject<JObject>((string)param[1]);
                    string campaignName = dataSIGN.Value<string>("variable1");
                    string signThisUserEmail = dataSIGN.Value<string>("variable2");

                    MainManager.Instance.Activist.signActivistToCampaign(campaignName, signThisUserEmail);
                    
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
