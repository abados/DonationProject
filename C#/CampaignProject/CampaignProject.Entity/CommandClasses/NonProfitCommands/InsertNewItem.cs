using CampaignProject.Model;
using Microsoft.AspNetCore.Mvc;
using System;



namespace CampaignProject.Entity.CommanClasses
{
    public class InsertNewNonProfitUser : CommandManager, ICommand
    {

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                MainManager.Instance.myLogger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                try
                {
                    NonProfitUser newUser = new NonProfitUser();
                    newUser = System.Text.Json.JsonSerializer.Deserialize<NonProfitUser>((string)param[1]);
                    MainManager.Instance.NonProfit.InsertNewItem(newUser);
                    return "Succes Request";
                }
                catch (Exception ex)
                {
                    MainManager.Instance.myLogger.LogException(ex.ToString(), ex);
                    return "Faild Request";
                }
            }
            else
            {
                MainManager.Instance.myLogger.LogError("ID Parameter was not found in GetCampaignID class", LoggingLibrary.LogLevel.Error);

                return "Faild Request";
            }

        }
    }
}
