using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CampaignProject.Model;
using LoggingLibrary;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Tweetinvi;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.ActivistCommands
{
    public class PurchesAProduct : CommandManager, ICommand
    {
        public PurchesAProduct(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            //Making a purches: Tweet about it, pay , make it status change
           
            if (param[1] != null)
            {
                try
                {
                    dynamic data = JsonConvert.DeserializeObject<JObject>((string)param[1]);
                    string productName = data.Value<string>("variable1");
                    decimal productPrice = data.Value<decimal>("variable2");
                    string userEmail = data.Value<string>("variable3");
                    Logger.LogEvent("proccesing a Purches of " + productName + "by" + userEmail, LoggingLibrary.LogLevel.Event);

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
                    TweetAboutPurches(hiddenEmail, productName);

                    return System.Text.Json.JsonSerializer.Serialize("Succes Request");
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);
                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                Logger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
            }

        }

        public async void TweetAboutPurches(string hiddenEmail, string productName)
        {
            ConfigData config = new ConfigData();

            config = MainManager.Instance.Activist.getTwitterKeys();

            var userClient = new TwitterClient(config.CONSUMER_KEY, config.CONSUMER_SECRET, config.ACCESS_TOKEN, config.ACCESS_TOKEN_SECRET);
            var user = await userClient.Users.GetAuthenticatedUserAsync();

            var tweet = await userClient.Tweets.PublishTweetAsync("the user:" + hiddenEmail + " just bought: " + productName + "");

        }
    }
}