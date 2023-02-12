using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CampaginCommands
{
    public class GetRole : CommandManager, ICommand
    {
        public GetRole(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                Logger.LogEvent("search a role in auth0 ", LoggingLibrary.LogLevel.Event);
                try
                {
                    string Auth0ApiQuery = MainManager.Instance.Campaign.GetDataFromConfig("Auth0ApiQuery");
                    var urlGetRole = $"{Auth0ApiQuery}{(string)param[0]}/roles";
                    var client = new RestClient(urlGetRole);
                    var request = new RestRequest("", Method.Get);

                    request.AddHeader("authorization", "" + MainManager.Instance.Campaign.GetDataFromConfig("Bearer"));
                    var response = client.Execute(request);

                    if (response.IsSuccessful)
                    {
                        var Json = JArray.Parse(response.Content);
                        return Json;
                    }
                    else
                    {
                        return new NotFoundResult();
                    }
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
