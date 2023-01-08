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
using static System.Net.WebRequestMethods;

namespace CampaignProject.MicroService
{
    public static class Tweets
    {
        [FunctionName("Tweets")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "twitter")] HttpRequest req,
            ILogger log)
        {
            // The url to get the tweets from we can add a query string to the url to filter the tweets, or to get the variables from react and insert them into the url here. like the following example :
            // https://api.twitter.com/1.1/search/tweets.json?q=from%3A{username}%20{query}&result_type=recent&count=10


            var urlTweets2 = "https://api.twitter.com/2/tweets/search/recent?expansions=author_id&query=%23eose&tweet.fields=lang,created_at,entities";

            string username = "@HayAbados";
            string startDate = "2023-01-05";
            string endDate = "2023-01-09";
            string query = "#helpimgEachOther ";

            string urlTweets = $"https://api.twitter.com/1.1/search/tweets.json?q=from%3A{username}%20since%3A{startDate}%20until%3A{endDate}%20{query}&result_type=recent&count=10";

            var client = new RestClient(urlTweets);
            var request = new RestRequest("", Method.Get);
            // Our bearer token for twitter
            request.AddHeader("authorization", "Bearer AAAAAAAAAAAAAAAAAAAAAENakwEAAAAAMheCg%2FAIMZdIWvt6anqHp%2B46MN8%3DQhqomMVza0yUop6wV4YkrataIc2PfPHRPGTQ3rxydLSdahTn4Q");

            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                // Still need to understand how and if to parse the response
                var json = JObject.Parse(response.Content);
                return new OkObjectResult(json);
            }
            else
            {
                return new NotFoundResult();
            }


            string responseMessage = "";

            return new OkObjectResult(responseMessage);
        }
    }
}
