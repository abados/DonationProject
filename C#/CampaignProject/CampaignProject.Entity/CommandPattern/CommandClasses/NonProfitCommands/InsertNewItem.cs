using CampaignProject.Model;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;
using System;



namespace CampaignProject.Entity.CommanClasses
{
    public class InsertNewNonProfitUser : CommandManager, ICommand
    {
        public InsertNewNonProfitUser(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                try
                {
                    NonProfitUser newUser = new NonProfitUser();
                    newUser = System.Text.Json.JsonSerializer.Deserialize<NonProfitUser>((string)param[1]);
                    MainManager.Instance.NonProfit.InsertNewItem(newUser);
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
                Logger.LogError("ID Parameter was not found in GetCampaignID class", LoggingLibrary.LogLevel.Error);

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
            }

        }
    }
}
