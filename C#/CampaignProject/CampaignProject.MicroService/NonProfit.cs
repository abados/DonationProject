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

            string azureFuncName = "NonProfit";
            string dictionaryKey = azureFuncName + "." + action;
            string requestBody;
            ICommand commmand;
            if (action.Equals("ADD") || action.Equals("Find"))
            {
                dictionaryKey = "Mutual." + action;
                commmand = MainManager.Instance.commandManager.CommandList[dictionaryKey];
            }
            else
            {
                commmand = MainManager.Instance.commandManager.CommandList[dictionaryKey];
            }
            try 
            { 
                if (commmand != null)
                {
                
                    requestBody = await req.ReadAsStringAsync();
                    return new OkObjectResult(commmand.ExecuteCommand(Identifier, requestBody,0, azureFuncName));
                }
                else
                {

                    MainManager.Instance.myLogger.LogError("Problam Was Found With the Command", LoggingLibrary.LogLevel.Error);
                    return new BadRequestObjectResult("Problam Was Found");
                }
            }
            catch(Exception ex)
            {
                MainManager.Instance.myLogger.LogException("Problam Was Found in NonProfit Azure File", ex) ;
                return new BadRequestObjectResult("Problam Was Found");
            }



}
    }
}