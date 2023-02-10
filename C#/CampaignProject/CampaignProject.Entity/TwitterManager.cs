using CampaignProject.Model;
using LoggingLibrary;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace CampaignProject.Entity
{
    public class TwitterManager : BaseEntity
    {
        public TwitterManager(Logger log) : base(log)
        {
            // Start the task that runs ManageTweetSearch
            Task.Run(() => ManageTweetSearchTask());
        }

        public void ManageTweetSearchTask()
        {
            while (true)
            {
                ManageTweetSearch();
                Thread.Sleep(3600000); // sleep for 1 hour (3600000 milliseconds)
            }
        }

        DateTime yesterday;
        string lastDateCheck;
        string lastTimeCheck;
        DateTime today;
        string lastDay;
        string currentDay;
        string start_time;
        string end_time;


        public void ManageTweetSearch()
        {
            OwnerManager owner = new OwnerManager(Logger);
            Dictionary<int, Model.ActiveCampaigns> activeCampaignList = owner.bringDataAboutCampaignsActivity();
            
            //bring all of the active campaigns and the people that are sign to them, those are the activist we will check for TWEETS
           
            defineVariablesAndStart();
            var keys = activeCampaignList.Keys;
            for (int i = 0; i < activeCampaignList.Count; i++)
            {
                int key = keys.ElementAt(i);
                Model.ActiveCampaigns value = activeCampaignList[key];
                string urlTweets = getUrlApiTwitter(value);

                var client = new RestClient(urlTweets);
                var request = new RestRequest("", Method.Get);
                CampaignManager campaign = new CampaignManager(Logger);
                request.AddHeader("authorization", "" + campaign.GetBearer("TweetBearer"));

                var response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    int tweetCount = 0;
                    JObject json = JObject.Parse(response.Content);

                    checkTwwets(json, value);
                }
            }
        }
        

        public void defineVariablesAndStart()
        {
            OwnerManager owner = new OwnerManager(Logger);
            string lastDateAndTime = owner.getLastTweetDateAndTime(); //bring the last time and date of the tweet that was found

            //editing the date and time format to fit the Twitter API Query

            //the date and time of the last check
            int separatorIndex = lastDateAndTime.IndexOf("TT");
            lastDateCheck = lastDateAndTime.Substring(0, separatorIndex); ;
            lastTimeCheck = lastDateAndTime.Substring(separatorIndex + 2);
            string format = "HH:mm:ss'Z'";
            DateTime newTime = DateTime.ParseExact(lastTimeCheck, format, CultureInfo.InvariantCulture);
            newTime = newTime.AddHours(-2);
            string timeToStart = newTime.ToString(format);
            start_time = lastDateCheck + "T" + timeToStart;

            //the date and time of the current check
            today = DateTime.Today;
            DateTime now = DateTime.UtcNow;
            string timeNow = now.ToString("THH:mm:ssZ");
            currentDay = today.ToString("yyyy-MM-dd");
            end_time = currentDay + timeNow;

        }

        public string getUrlApiTwitter(Model.ActiveCampaigns value)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            string urlTweets = $"{Owner.bringTwitterQuery()}start_time={start_time}&end_time={end_time}&query=from:{value.TwitterAcount}";
            return urlTweets;
        }

        public void checkTwwets(JObject json, Model.ActiveCampaigns value)
        {
            
            int tweetCount = 0;
            int resultCount = (int)json["meta"]["result_count"];
                    if (resultCount != 0)
                    {
                        foreach (var tweet in json["data"])
                        {
                            if (tweet["text"].ToString().Contains(value.campaignHashtag))
                            {
                                tweetCount++;

                                string id = (string)json["data"][0]["id"];
                                string text = (string)json["data"][0]["text"];
                                string[] tweetContant = text.Split(new string[] { "\n" }, StringSplitOptions.None);
                                MainManager.Instance.Owner.InsertNewTweet(id, tweetContant[0], tweetContant[1], value);
                            }
                        }
                    }

            if (tweetCount > 0)
            {
                MainManager.Instance.Owner.giveCreditOnActions(tweetCount, value.activeUserId);
            }
        }
              
        

    }
}
