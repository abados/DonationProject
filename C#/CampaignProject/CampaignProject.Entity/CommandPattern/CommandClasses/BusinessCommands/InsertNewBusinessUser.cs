using CampaignProject.Model;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;
using System;



namespace CampaignProject.Entity.CommanClasses
{
    public class InsertNewBusinessUser : CommandManager, ICommand
    {
        public InsertNewBusinessUser(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                try//adding the user to the users table and to Business table
                {
                    Model.BusinessUser business = new Model.BusinessUser();
                    business = System.Text.Json.JsonSerializer.Deserialize<Model.BusinessUser>((string)param[1]);
                    MainManager.Instance.Business.InsertNewMember(business);

                    return "Succes Request";
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);
                    return "Faild Request";
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
