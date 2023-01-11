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

            switch (action)
            {
                case "Find": //check if the user allready sign as a role
                    Logger.Log("looking for a user in the DB", LoggingLibrary.LogLevel.Event);
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Business.FindTheUser(Identifier)));
                    }
                    catch(Exception ex)
                    {                                            
                            Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);     
                    }
                    break;
                case "ADD"://adding the user to the users table and to Business table
                    Logger.Log("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                    try
                    {
                        Model.BusinessUser business = new Model.BusinessUser();
                        business = System.Text.Json.JsonSerializer.Deserialize<Model.BusinessUser>(req.Body);
                        MainManager.Instance.Business.InsertNewMember(business);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GETMYPRODUCTS"://get products of the specific Business user
                    Logger.Log("business man called prodcuts: ", LoggingLibrary.LogLevel.Event);
                    try
                    {
                        if (Identifier.Contains("@") && specificAction == null)
                        {//if we came from a business user path and we send business data
                            var id = MainManager.Instance.Business.getIDS(Identifier, "");
                            return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getUnBoughtProductsOfSpecificBusinessFromDB(int.Parse(id[0]))));
                        }
                        else if (specificAction.Equals("trackShipment"))
                        {//for shipment tracking page
                            var id = MainManager.Instance.Business.getIDS(Identifier, "");
                            return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getBoughtProductsOfSpecificBusinessFromDB(int.Parse(id[0]))));
                        }
                        else
                        {//if we came from a activist user path and we send campaign data
                            return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getProductsOfSpecificCampaignFromDB(Identifier)));
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "DELETEAPRODUCT"://the user can see only products that are available and didnt ordered
                    //In the case of the POST request, the request body is being sent as a JSON string, so it can be deserialized directly using the JsonSerializer. However, in the case of the DELETE request, when you are trying to send the product object in the request body, rather than as a JSON string.In this case, you need to serialize the object into a string before sending it in the request body with ReadToEndAsync().
                    Logger.Log("DeleteAProduct called", LoggingLibrary.LogLevel.Event);
                    try
                    {
                        Model.Product productToDelete = new Model.Product();
                        string requestBodyToDelete = await new StreamReader(req.Body).ReadToEndAsync();
                        productToDelete = System.Text.Json.JsonSerializer.Deserialize<Model.Product>(requestBodyToDelete);
                        MainManager.Instance.Product.DeleteAProduct(productToDelete.productName,productToDelete.businessID);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "UPLOADPRODUCT"://upload a new product from a specific user to a specific Campaign
                    Logger.Log("InsertNewProduct called", LoggingLibrary.LogLevel.Event);
                    try { 
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

                    MainManager.Instance.Product.InsertNewItem(product);


                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Business.getIDS(EmailToSearch, CampaignToSearch)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }                   
                    break;
                case "SHIPIT"://the business man can see orders that didn't completed, if he psuh "send" we get here to finish them
                    Logger.Log("SendTheItems called", LoggingLibrary.LogLevel.Event);
                    try { 
                    MainManager.Instance.Business.SendTheItems(int.Parse(Identifier));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }                    
                    break;

                default:
                    break;

            }

            return new OkObjectResult("here for checking");
        }
    }
}
