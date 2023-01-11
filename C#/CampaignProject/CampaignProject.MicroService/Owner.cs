using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using CampaignProject.Entity;
using CampaignProject.Model;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Linq;
using static System.Net.WebRequestMethods;
using Tweetinvi;
using LoggingLibrary;
using iTextSharp.text.pdf;
using iTextSharp.text;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Net.Http;
using System.Net;
using Microsoft.Extensions.Configuration;
using CampaignProject.DAL;

namespace CampaignProject.MicroService
{
    public static class Owner
    {

        [FunctionName("Owner")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Owner/{action}/{Identifier?}")] HttpRequest req,
            string action, string Identifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            switch (action)
            {
                case "Find"://Check if the user is in the DB
                    try
                    {
                        //Logger.Log("looking for a user in the DB", LoggingLibrary.LogLevel.Event);
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.FindTheUser(Identifier)));

                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "ADD"://ADD ths user to the DB
                    Logger.Log("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                    try
                    {
                        Model.Owner owner = new Model.Owner();
                        owner = System.Text.Json.JsonSerializer.Deserialize<Model.Owner>(req.Body);
                        MainManager.Instance.Owner.InsertNewItem(owner);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "TWEET"://A call to a function that checks tweets of specific people and specific tags and gives value accordingly
                    Logger.Log("giveCreditOnActions called: ", LoggingLibrary.LogLevel.Event);
                    try
                    {//bring all of the active campaigns and the people that are sign to them, those are the activist we will check for TWEETS
                        Dictionary<int, Model.ActiveCampaigns> activeCampaignList = MainManager.Instance.Owner.bringDataAboutCampaignsActivity();

                        DateTime currentDate = DateTime.Today.AddDays(-1); ;
                        DateTime dateOfTomorrow = DateTime.Today;
                        string currentDay = currentDate.ToString("yyyy-MM-dd");
                        string tomorrow = dateOfTomorrow.ToString("yyyy-MM-dd");

                        foreach (KeyValuePair<int, Model.ActiveCampaigns> pair in activeCampaignList)
                        {
                            int key = pair.Key;
                            Model.ActiveCampaigns value = pair.Value;
                            string startDate = "" + currentDay;
                            string endDate = "" + tomorrow;
                            //search url that check if there are tweets of specific user, with specific hashtag on the last day
                            string urlTweets = $"https://api.twitter.com/1.1/search/tweets.json?q=from%3A{value.TwitterAcount}%20since%3A{startDate}%20until%3A{endDate}%20{value.campaignHashtag}result_type=recent&count=10";


                            var client = new RestClient(urlTweets);
                            var request = new RestRequest("", Method.Get);
                            // Our bearer token for twitter
                            request.AddHeader("authorization", "" + MainManager.Instance.Campaign.GetBearer("TweetBearer"));

                            var response = client.Execute(request);
                            if (response.IsSuccessful)
                            {
                                // Still need to understand how and if to parse the response
                                JObject json = JObject.Parse(response.Content);
                                if (((JArray)json["statuses"]).Count == 0)
                                {
                                    int x = 1; // didnt found tweet
                                }
                                else
                                {
                                    int tweetCount = ((JArray)json["statuses"]).Count; ;
                                    MainManager.Instance.Owner.giveCreditOnActions(tweetCount, value.activeUserId);

                                }
                                //return new OkObjectResult(json);
                            }
                            else
                            {
                                return new NotFoundResult();
                            }

                        }
                        string responseMessage = "";

                        return new OkObjectResult(responseMessage);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }

                    break;
                case "REPORT"://A report page, it gets what type on information the owner seeks and make the specific query for it.
                    Logger.Log("report called: ", LoggingLibrary.LogLevel.Event);
                    try
                    {
                        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                        dynamic data = JsonConvert.DeserializeObject<JObject>(requestBody);
                        string Table = data.Value<string>("variable1");
                        string Search = data.Value<string>("variable2");
                        //string TypeOfFile = data.Value<string>("variable3");

                        switch (Table)
                        {
                            case "Products":
                                try
                                {
                                    if (Search == "All products")
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getAllProductsForReport()));
                                    }
                                    else if (Search == "Bought products")
                                    {

                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getBoughtProductsFromDB()));
                                    }
                                    else
                                    { //Bought and not deliverd products

                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getBoughtAndNotDeliverdProductsFromDB()));
                                    }
                                }

                                catch (Exception ex)
                                {
                                    Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                                }
                                break;
                            case "Users":
                                try
                                {
                                    if (Search == "Business users")
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getBusinessUsers()));
                                    }
                                    else if (Search == "Nonprofits users")
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getNonProfitUsers()));
                                    }
                                    else if (Search == "Activists users")
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getActivistUsers()));
                                    }
                                    else
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.UsersEarningsSum()));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                                }
                                break;
                            case "Campaigns":
                                try
                                {
                                    if (Search == "All campaigns")
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getCampaignsFromDBInList()));
                                    }
                                    else
                                    {
                                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.bringDataAboutCampaignsActivity()));
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }


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