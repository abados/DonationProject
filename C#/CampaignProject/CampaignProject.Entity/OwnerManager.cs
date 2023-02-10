using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class OwnerManager : BaseEntity
    {
        public OwnerManager(Logger log) : base(log)
        {
        }

        public string FindTheUser(string UserEmail)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try { 
            return (string)Owner.SendSqlQueryToReadFromDBForOneUser(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }

        }

        public void InsertNewItem(Model.Owner newOwner)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            string userType = "Admin";
            try { 
            string userID = Owner.AddNewUser(userType);
            Owner.SendSqlQueryToInsertToDB(newOwner,int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
        }

        public void giveCreditOnActions(int ActivityCount, int activistID)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try { 
            Owner.SendSqlQueryToPay(ActivityCount, activistID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
        }

        public Dictionary<int, Model.ActiveCampaigns> bringDataAboutCampaignsActivity()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            Dictionary<int, Model.ActiveCampaigns> activeCampaignList;
            try { 
            activeCampaignList = (Dictionary<int, Model.ActiveCampaigns>)Owner.bringActiveCampaignsTable();
            return activeCampaignList;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
        }

     ///////////Report Function////////
     
        public List<BusinessUser> getBusinessUsers()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try { 
            return (List<BusinessUser>)Owner.bringAallBusinessUsers();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }


        public List<NonProfitUser> getNonProfitUsers()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try { 
            return (List<NonProfitUser>)Owner.bringAallNonProfitUsers();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }

        public List<ActivistUser> getActivistUsers()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try { 
            return (List<ActivistUser>)Owner.bringAllActivistUsers();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }

        public List<Tweet> getTweets()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try
            {
                return (List<Tweet>)Owner.bringAllTweets();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }

        public string UsersEarningsSum()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            try { 
            return Owner.bringEarningSumUp();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;

        }

        public void InsertNewTweet(string tweetID, string tweetHashtag, string tweetText, Model.ActiveCampaigns active)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);

            try
            {
                Owner.InsertTweetsintoDB(tweetID, tweetHashtag, tweetText, active);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
        }

        public string getLastTweetDateAndTime ()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData(Logger);
            string result=null;
            try
            {
                result = Owner.bringLastTweetDateAndTime();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return result;
        }
    }

  
}
