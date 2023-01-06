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

namespace CampaignProject.MicroService
{
    public static class Activist
    {
        [FunctionName("Activist")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Activist/{action}/{Identifier?}")] HttpRequest req,
            string action, string Identifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (action)
            {
                case "Find":
                    //check if the user allready sign as a role
                    
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getProductByIDFromDB(Identifier)));
                    break;
                case "ADD":
                    //adding the user to the users table and to activist table
                    Model.ActivistUser owner = new Model.ActivistUser();
                    owner = System.Text.Json.JsonSerializer.Deserialize<Model.ActivistUser>(req.Body);
                    MainManager.Instance.Activist.SendNewInputToDataLayer(owner);

                    break;
                case "GETORGANIZATIONS":
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getNonProfitListFromDB()));
                    break;
                case "PURCHES":


                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject<JObject>(requestBody);
                    string productName = data.Value<string>("variable1");
                    decimal productPrice = data.Value<decimal>("variable2");
                    string userEmail = data.Value<string>("variable3");
                    MainManager.Instance.Activist.makeAPurchesChanges(productName, productPrice, userEmail);

                    break;
                case "GETMYPRODUCTS":
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getPurchesProductsOFromDB(Identifier)));
                    break;
                case "GETEARNINGS":
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getEarningsByIDFromDB(Identifier)));
                default:
                    break;

            }

            return new OkObjectResult("here for checking");
        }
    }
}
