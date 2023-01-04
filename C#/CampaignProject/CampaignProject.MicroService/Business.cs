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

namespace CampaignProject.MicroService
{
    public static class Business
    {
        [FunctionName("Business")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","delete", Route = "Business/{action}/{IdNumber?}")] HttpRequest req,
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
                case "GETMYPRODUCTS":

                    var id = MainManager.Instance.Business.getIDS(IdNumber, "");
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getProductsFromDB(int.Parse(id[0]))));
                    break;
                case "DELETEAPRODUCT":


                    //In the case of the POST request, the request body is being sent as a JSON string, so it can be deserialized directly using the JsonSerializer. However, in the case of the DELETE request, when you are trying to send the product object in the request body, rather than as a JSON string.In this case, you need to serialize the object into a string before sending it in the request body with ReadToEndAsync().

                    try
                    {
                        Model.Product productToDelete = new Model.Product();
                        string requestBodyToDelete = await new StreamReader(req.Body).ReadToEndAsync();
                        productToDelete = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestBodyToDelete);
                        MainManager.Instance.Product.DeleteAProduct(productToDelete.productName,productToDelete.businessID);
                    }
                    catch (Exception x)
                    {
                        Console.WriteLine(x.Message);
                    }
                    break;



                case "UPLOADPRODUCT":
         

                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject<JObject>(requestBody);
                    string EmailToSearch = data.Value<string>("variable1");
                    string CampaignToSearch = data.Value<string>("variable2");

                    //I have th businessRep email and the campaign name and now i need their id's to enter to the product Model
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
