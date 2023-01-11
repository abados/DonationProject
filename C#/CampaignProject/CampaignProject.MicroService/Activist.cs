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

            ConfigData config = new ConfigData();

            config = MainManager.Instance.Activist.getTwitterKeys();

            var userClient = new TwitterClient(config.CONSUMER_KEY, config.CONSUMER_SECRET, config.ACCESS_TOKEN, config.ACCESS_TOKEN_SECRET);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            
            switch (action)
            {
                case "Find":
                    //check if the user allready sign as a role
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.FindTheUser(Identifier)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }

                    break;
                case "ADD":
                    //adding the user to the users table and to activist table
                    try { 
                    Model.ActivistUser owner = new Model.ActivistUser();
                    owner = System.Text.Json.JsonSerializer.Deserialize<Model.ActivistUser>(req.Body);
                    MainManager.Instance.Activist.SendNewInputToDataLayer(owner);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GETORGANIZATIONS":
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getNonProfitListFromDB()));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "PURCHES":
                    try { 
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject<JObject>(requestBody);
                    string productName = data.Value<string>("variable1");
                    decimal productPrice = data.Value<decimal>("variable2");
                    string userEmail = data.Value<string>("variable3");
                    MainManager.Instance.Activist.makeAPurchesChanges(productName, productPrice, userEmail);

                    var tweet = await userClient.Tweets.PublishTweetAsync("the user:"+ userEmail+" just bought an:"+ productName+"");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "SIGNCAMPAIGN":
                    try { 
                    string requestBodySIGN = await new StreamReader(req.Body).ReadToEndAsync();
                    dynamic dataSIGN = JsonConvert.DeserializeObject<JObject>(requestBodySIGN);
                    string campaignName = dataSIGN.Value<string>("variable1");
                    string signThisUserEmail = dataSIGN.Value<string>("variable2");
                    
                    MainManager.Instance.Activist.signActivistToCampaign(campaignName, signThisUserEmail);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GETMYPRODUCTS":
                    try { 
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getPurchesProductsOFromDB(Identifier)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GETEARNINGS":
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getEarningsByIDFromDB(Identifier)));
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
