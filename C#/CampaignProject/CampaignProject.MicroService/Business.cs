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


               
                case "SEARCHID":
                    //Model.Product product = new Model.Product();

                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject<JObject>(requestBody);
                    string EmailToSearch = data.Value<string>("variable1");
                    string CampaignToSearch = data.Value<string>("variable2");
                    var ids =MainManager.Instance.Business.getIDS(EmailToSearch, CampaignToSearch);


                    Model.Product product = new Model.Product();
                    product.businessID = int.Parse(ids[0]);
                    product.campaignID= int.Parse(ids[1]);
                   product.productName = data.Value<string>("variable3");

                    string priceToPrase = data.Value<string>("variable4");

                    product.price = decimal.Parse(priceToPrase);

                    product.IsBought = data.Value<bool>("variable5");
                    product.IsDelivered = data.Value<bool>("variable6");
                    product.ActivistBuyerID = data.Value<int>("variable7");

                    MainManager.Instance.Product.SendNewInputToDataLayer(product);


                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Business.getIDS(EmailToSearch, CampaignToSearch)));
                    break;



                case "GET_CAMPAINGS":
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getCampaignsFromDB()));
                    break;
            

                default:
                    break;

            }

            return new OkObjectResult("here for checking");
        }
    }
}
