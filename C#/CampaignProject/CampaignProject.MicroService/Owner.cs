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
                case "Find":

                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getProductByIDFromDB(Identifier)));

                    break;
                case "ADD":
                             Model.Owner owner = new Model.Owner();
                             owner = System.Text.Json.JsonSerializer.Deserialize<Model.Owner>(req.Body);
                             MainManager.Instance.Owner.SendNewInputToDataLayer(owner);
                    
                    break;
                case "TWEET":

                    //Dictionary<int, Model.ActiveCampaigns> activeCampaignList = MainManager.Instance.Owner.bringDataAboutCampaignsActivity();

                    //DateTime currentDate = DateTime.Today;
                    //DateTime dateOfTomorrow = DateTime.Today.AddDays(1);
                    //string currentDay = currentDate.ToString("yyyy-MM-dd");
                    //string tomorrow = dateOfTomorrow.ToString("yyyy-MM-dd");

                    //foreach (KeyValuePair<int, Model.ActiveCampaigns> pair in activeCampaignList)
                    //{
                    //    int key = pair.Key;
                    //    Model.ActiveCampaigns value = pair.Value;
                    //    string startDate = "" + currentDay;
                    //    string endDate = "" + tomorrow;

                    //    Console.WriteLine("" + value.id + " " + value.campaignId + " " + value.campaignName + " " + value.campaignHashtag + " " + value.activeUserId + " " + value.ActiveUserName + "" + value.TwitterAcount);


                    //    string urlTweets2 = $"https://api.twitter.com/1.1/search/tweets.json?q=from%3A{value.TwitterAcount}%20since%3A{startDate}%20until%3A{endDate}%20{value.campaignHashtag}result_type=recent&count=10";



                    //}

                    DateTime currentDate = DateTime.Today;
                    DateTime dateOfTomorrow = DateTime.Today.AddDays(1);
                    string currentDay = currentDate.ToString("yyyy-MM-dd");
                    string tomorrow = dateOfTomorrow.ToString("yyyy-MM-dd");
                    string username = "@HayAbados";
                    string startDate = "" + tomorrow;
                    string endDate = "" + tomorrow;
                    string HashTag = "#helpimgEachOther ";

                    string urlTweets = $"https://api.twitter.com/1.1/search/tweets.json?q=from%3A{username}%20since%3A{startDate}%20until%3A{endDate}%20{HashTag}result_type=recent&count=10";

              

                    var client = new RestClient(urlTweets);
                    var request = new RestRequest("", Method.Get);
                    // Our bearer token for twitter
                    request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAENakwEAAAAAMheCg%2FAIMZdIWvt6anqHp%2B46MN8%3DQhqomMVza0yUop6wV4YkrataIc2PfPHRPGTQ3rxydLSdahTn4Q");

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
                            int x = 7; //  found tweet
                        }
                        //return new OkObjectResult(json);
                    }
                    else
                    {
                        return new NotFoundResult();
                    }


                    string responseMessage = "";

                    return new OkObjectResult(responseMessage);


                    break;
              
                default:
                    break;

            }

            return new OkObjectResult("here for checking");
        }
    }
}
