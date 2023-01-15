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

            ConfigData config = new ConfigData();

            config = MainManager.Instance.Activist.getTwitterKeys();

            var userClient = new TwitterClient(config.CONSUMER_KEY, config.CONSUMER_SECRET, config.ACCESS_TOKEN, config.ACCESS_TOKEN_SECRET);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            
            switch (action)
            {
                case "Find":  //check if the user allready sign as a role
                    //Logger.Log("looking for a user in the DB", LoggingLibrary.LogLevel.Event);
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.FindTheUser(Identifier)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }

                    break;
                   
                case "ADD"://adding the user to the users table and to activist table
                    Logger.Log("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                    try { 
                    Model.ActivistUser activist = new Model.ActivistUser();
                        activist = System.Text.Json.JsonSerializer.Deserialize<Model.ActivistUser>(req.Body);
                    MainManager.Instance.Activist.InsertNewMember(activist);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GETORGANIZATIONS": //geting All the Organization/NonProfit users 
                    Logger.Log("getNonProfitListFromDB called: ", LoggingLibrary.LogLevel.Event);
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getNonProfitListFromDB()));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "PURCHES"://Making a purches: Tweet about it, pay , make it status change
                    
                    try { 
                    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    dynamic data = JsonConvert.DeserializeObject<JObject>(requestBody);
                    string productName = data.Value<string>("variable1");
                    decimal productPrice = data.Value<decimal>("variable2");
                    string userEmail = data.Value<string>("variable3");
                     Logger.Log("proccesing a Purches of "+ productName+"by"+userEmail, LoggingLibrary.LogLevel.Event);

                        MainManager.Instance.Activist.makeAPurchesChanges(productName, productPrice, userEmail);
                        int emailLength = userEmail.IndexOf("@");
                        string hiddenEmail;
                        if (emailLength > 3)
                        {
                            hiddenEmail = userEmail.Substring(0, emailLength - 3) + new string('*', 3) + userEmail.Substring(emailLength);
                        }
                        else
                        {
                            hiddenEmail = new string('*', emailLength) + userEmail.Substring(emailLength);
                        }
                        var tweet = await userClient.Tweets.PublishTweetAsync("the user:"+ hiddenEmail + " just bought: "+ productName+"");
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "SIGNCAMPAIGN"://SignUp new activist to Campaign
                    Logger.Log("sign user to campaign", LoggingLibrary.LogLevel.Event);
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
                case "GETMYPRODUCTS"://get products of the specific activist user
                    Logger.Log("getPurchesProductsOFromDB called", LoggingLibrary.LogLevel.Event);
                    try { 
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getPurchesProductsOFromDB(Identifier)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GETEARNINGS"://get the specific activist user Money
                    Logger.Log("getEarningsByIDFromDB called", LoggingLibrary.LogLevel.Event);
                    try { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Activist.getEarningsByIDFromDB(Identifier)));
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "Donate"://Making a donor by the activist:  pay by the user
                    Logger.Log("DonateByActivist called", LoggingLibrary.LogLevel.Event);
                    try
                    {
                        
                        MainManager.Instance.Activist.DonateByActivist(int.Parse(SecondIdentifier), Identifier);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break; 

                case "GETACTIVECAMPAIGN"://Bring the Active Campaigns of the user
                   
                    try
                    {
                        var activeList = MainManager.Instance.Activist.getActiveCampaignsOfUserFromDB(Identifier);
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(activeList));
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
