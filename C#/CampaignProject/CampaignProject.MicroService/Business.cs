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
    public static class Business
    {
        [FunctionName("Business")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Business/{action}/{IdNumber?}")] HttpRequest req,
            string action, string IdNumber, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (action)
            {
                case "Find":

                    string isExist = MainManager.Instance.Business.getProductByIDFromDB(IdNumber);
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(isExist));

                    break;
                case "ADD":
                    Model.BusinessUser business = new Model.BusinessUser();
                    business = System.Text.Json.JsonSerializer.Deserialize<Model.BusinessUser>(req.Body);
                    MainManager.Instance.Business.SendNewInputToDataLayer(business);

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
