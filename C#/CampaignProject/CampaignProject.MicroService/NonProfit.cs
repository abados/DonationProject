using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CampaignProject.Entity;
using CampaignProject.Model;
using LoggingLibrary;

namespace CampaignProject.MicroService
{
    public static class NonProfit
    {
        [FunctionName("NonProfit")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "NonProfit/{action}/{Identifier?}")] HttpRequest req,
            string action, string Identifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string dictionaryKey = "NonProfit." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.commandManager.CommandList[dictionaryKey];

            if (commmand != null)
            {
                //NonProfitUser newUser = new NonProfitUser();
                //newUser = System.Text.Json.JsonSerializer.Deserialize<NonProfitUser>(req.Body);
                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(Identifier, requestBody));
            }
            else
            {

                MainManager.Instance.myLogger.LogError("Problam Was Found", LoggingLibrary.LogLevel.Error);
                return new BadRequestObjectResult("Problam Was Found");
            }


            switch (action)
            {
                case "Find": //check if the user allready sign as a role
                    try
                    {
                        MainManager.Instance.myLogger.LogEvent("Search user in the DB: ", LoggingLibrary.LogLevel.Event);
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.NonProfit.FindTheUser(Identifier)));
                    
                    }
                    catch (Exception ex)
                    {
                         MainManager.Instance.myLogger.LogException(ex.ToString(), ex);
                    }   
            break;
                case "ADD"://adding the user to the users table and to NonProfit table
                    MainManager.Instance.myLogger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                    try { 
                    NonProfitUser newUser = new NonProfitUser();
                    newUser = System.Text.Json.JsonSerializer.Deserialize<NonProfitUser>(req.Body);
                    MainManager.Instance.NonProfit.InsertNewItem(newUser);
                    }
                    catch (Exception ex)
                    {
                        MainManager.Instance.myLogger.LogException(ex.ToString(), ex);
                    }
                    break;
                default:
                    break;

            }

            return new OkObjectResult("here for checking");
        }
    }
}