using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace CampaignProject.Entity
{
    public class TwitterManager
    {
        Dictionary<int, Model.ActiveCampaigns> activeCampaignList;
        DateTime yesterday;
        DateTime today;
        string lastDay;
        string currentDay;
        string start_time;
        string end_time;

        public void defineVariablesAndStart()
        {
            activeCampaignList = MainManager.Instance.Owner.bringDataAboutCampaignsActivity();
            yesterday = DateTime.Today.AddDays(-1); ;
            today = DateTime.Today;
            lastDay = yesterday.ToString("yyyy-MM-dd");
            currentDay = today.ToString("yyyy-MM-dd");
            start_time = lastDay + "T00:00:00Z";
            end_time = currentDay + "T00:00:00Z";
          

        }

        public string getUrlApiTwitter(Model.ActiveCampaigns value)
        {
            defineVariablesAndStart();
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
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
