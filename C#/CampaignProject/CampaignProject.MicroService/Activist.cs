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
using Newtonsoft.Json.Linq;
using Tweetinvi;
using LoggingLibrary;
using CampaignProject.Model;
using iTextSharp.text;
using System.Collections.Generic;

namespace CampaignProject.MicroService
{
    public static class Activist
    {
        [FunctionName("Activist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Activist/{action}/{Identifier?}/{SecondIdentifier?}")] HttpRequest req,
            string action, string Identifier, string SecondIdentifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string dictionaryKey = "Activist." + action;
            string requestBody;

            ICommand commmand = MainManager.Instance.commandManager.CommandList[dictionaryKey];

            if (commmand != null)
            {

                requestBody = await req.ReadAsStringAsync();
                return new OkObjectResult(commmand.ExecuteCommand(Identifier, requestBody, SecondIdentifier));
            }
            else
            {

                MainManager.Instance.myLogger.LogError("Problam Was Found", LoggingLibrary.LogLevel.Error);
                return new BadRequestObjectResult("Problam Was Found");
            }

        }
    }
}
