using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CampaginCommands
{
    public class GetAllOrSpecificCampaignsList : CommandManager, ICommand
    {
        public GetAllOrSpecificCampaignsList(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
           
                Logger.LogEvent("get All/Specific Campaign list from the DB ", LoggingLibrary.LogLevel.Event);
                try
                {
                    if ((string)param[0] == null)
                    {
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getCampaignsFromDBInDIctionary());
                    }
                    else
                    {
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getSpecificCampaignsFromDB((string)param[0]));
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);

                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
          

        
    }
}
