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
                
                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(Identifier, requestBody));
            }
            else
            {

                MainManager.Instance.myLogger.LogError("Problam Was Found", LoggingLibrary.LogLevel.Error);
                return new BadRequestObjectResult("Problam Was Found");
            }


          

            return new OkObjectResult("here for checking");
        }
    }
}