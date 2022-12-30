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

namespace CampaignProject.MicroService
{
    public static class Activist
    {
        [FunctionName("Activist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Activist/{action}/{IdNumber?}")] HttpRequest req,
            string action, string IdNumber, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (action)
            {
                case "Find":

                    string isExist = MainManager.Instance.Activist.getProductByIDFromDB(IdNumber);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(isExist));

                    break;
                case "ADD":
                    Model.ActivistUser owner = new Model.ActivistUser();
                    owner = System.Text.Json.JsonSerializer.Deserialize<Model.ActivistUser>(req.Body);
                    MainManager.Instance.Activist.SendNewInputToDataLayer(owner);

                    break;
                case "GETONE":

                    //Model.Product p = MainManager.Instance.product.getProductByIDFromDB(IdNumber);
                    //return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(p));

                    break;

                default:
                    break;

            }

            return new OkObjectResult("here for checking");
        }
    }
}
