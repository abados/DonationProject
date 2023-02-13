using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using CampaignProject.Entity;
using System.Collections.Generic;
using LoggingLibrary;
using Org.BouncyCastle.Utilities.Encoders;


namespace CampaignProject.MicroService
{
    public static class Owner
    {

        [FunctionName("Owner")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Owner/{action}/{Identifier?}")] HttpRequest req,
            string action, string Identifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string dictionaryKey = "Owner." + action;
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
                MainManager.Instance.myLogger.LogException("Problam Was Found in Owners Azure File", ex) ;
                return new BadRequestObjectResult("Problam Was Found");
            }

        }
    }
}