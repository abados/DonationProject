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
using System.Diagnostics;
using CampaignProject.Model;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using Microsoft.Extensions.FileSystemGlobbing;
using System.Linq;
using LoggingLibrary;

namespace CampaignProject.MicroService
{
    public static class Business
    {
        [FunctionName("Business")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","delete", Route = "Business/{action}/{Identifier?}/{specificAction?}")] HttpRequest req,
            string action, string Identifier, string specificAction, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string dictionaryKey = "Business." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.commandManager.CommandList[dictionaryKey];

            if (commmand != null)
            {

                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(Identifier, requestBody, specificAction));
            }
            else
            {

                MainManager.Instance.myLogger.LogError("Problam Was Found", LoggingLibrary.LogLevel.Error);
                return new BadRequestObjectResult("Problam Was Found");
            }

        }
    }
}
