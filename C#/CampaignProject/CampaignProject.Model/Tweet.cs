using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Model
{
    public class Tweet
    {

        public int id { get; set; }

        public string TweetID { get; set; }

        public int activeUserId { get; set; }
        public int campaignId { get; set; }

        public string campaignName { get; set; }

        public string TweetHashtag { get; set; }

        public string TweetText { get; set; }

        public string ActiveUserName { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }
    }
}
