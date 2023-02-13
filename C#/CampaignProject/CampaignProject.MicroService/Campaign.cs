using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json.Linq;
using CampaignProject.Entity;
using LoggingLibrary;

namespace CampaignProject.MicroService
{
    public static class Campaign
    {
        [FunctionName("ProLobby")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","delete", Route = "Campaigns/{action}/{Identifier?}")] HttpRequest req,
            string action, string Identifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string dictionaryKey = "Campaign." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.commandManager.CommandList[dictionaryKey];
            try
            { 
                if (commmand != null)
                {

                    requestBody = await req.ReadAsStringAsync();
                    return new OkObjectResult(commmand.ExecuteCommand(Identifier, requestBody));
                }
                else
                {

                    MainManager.Instance.myLogger.LogError("Problam Was Found With the Command", LoggingLibrary.LogLevel.Error);
                    return new BadRequestObjectResult("Problam Was Found");
                }
            }
            catch(Exception ex)
            {
                MainManager.Instance.myLogger.LogException("Problam Was Found in Campaign Azure File", ex) ;
                return new BadRequestObjectResult("Problam Was Found");
            }


}
    }
}
