using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class ActivistManager:BaseEntity
    {
        public ActivistManager(Logger log) : base(log)
        {

        }
        public string FindTheUser(string UserEmail)
        {//chack if the userEmail is allready in the sql to know if the user signUp

            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData(Logger);
            try { 
            return (string)Activist.SendSqlQueryToReadFromDBForOneUser(UserEmail);
                } catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }

        }

        public string getEarningsByIDFromDB(string UserEmail)
        {//chack if the userEmail is allready in the sql to know if the user signUp
            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData(Logger);
            try
            {
                return Activist.getActivistUserEarnings(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
        }

        public void InsertNewMember(Model.ActivistUser newActivist)
        {//insert of a new user to both tables - users and activists after the user signUp
            Data.Sql.ActivistData user = new Data.Sql.ActivistData(Logger);
            string userType = "Activist";
            try { 
            string userID = user.AddNewUser(userType);
            user.SendSqlQueryToInsertToDB(newActivist, int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                
            }
        }


        public List<NonProfitUser> nonProfitsList = new List<NonProfitUser>();

        public List<NonProfitUser> getNonProfitListFromDB()
        {
            Data.Sql.ActivistData activist = new Data.Sql.ActivistData(Logger);
            try
            {
                nonProfitsList = (List<NonProfitUser>)activist.bringOrganizationsFromDB();
                return nonProfitsList;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
}
        public List<ActiveCampaigns> ActiveCampaignsLise = new List<ActiveCampaigns>();

        

        public List<ActiveCampaigns> getActiveCampaignsOfUserFromDB(string Email)
        {
            Data.Sql.ActivistData activist = new Data.Sql.ActivistData(Logger);
            try
            {
                ActiveCampaignsLise = (List<ActiveCampaigns>)activist.bringActiveCampaignOfUsrFromDB(Email);
                return ActiveCampaignsLise;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
        }

        public void makeAPurchesChanges(string productName, decimal productPrice, string userEmail)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData(Logger);
            try { 
            user.makeAPurchesInTheDB(productName, productPrice, userEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);

            }
        }

        public void DonateByActivist(decimal productPrice, string userEmail)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData(Logger);
            try
            {
                user.makeADonateInTheDB(productPrice,userEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);

            }
        }

        

        public void signActivistToCampaign(string campaignName, string userEmail)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData(Logger);
            try
            {
                user.signActivistToCampaignInTheDB(campaignName, userEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);

            }
        }



        public ConfigData getTwitterKeys()
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData(Logger);
            try { 
            ConfigData config = (ConfigData)user.bringTwitterKeysFromDB();
                return config;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
            

        }



    }
}
