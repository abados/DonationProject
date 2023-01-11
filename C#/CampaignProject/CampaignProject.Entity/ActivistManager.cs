using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class ActivistManager
    {
        public string FindTheUser(string UserEmail)
        {//chack if the userEmail is allready in the sql to know if the user signUp

            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData();
            try { 
            return (string)Activist.SendSqlQueryToReadFromDBForOneUser(UserEmail);
                } catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }

        }

        public string getEarningsByIDFromDB(string UserEmail)
        {//chack if the userEmail is allready in the sql to know if the user signUp
            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData();
            try
            {
                return Activist.getActivistUserEarnings(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }
        }

        public void SendNewInputToDataLayer(Model.ActivistUser newOwner)
        {//insert of a new user to both tables - users and activists after the user signUp
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            string userType = "Activist";
            try { 
            string userID = user.AddNewUser(userType);
            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                
            }
        }


        public List<NonProfitUser> nonProfitsList = new List<NonProfitUser>();

        public List<NonProfitUser> getNonProfitListFromDB()
        {
            Data.Sql.ActivistData activist = new Data.Sql.ActivistData();
            try
            {
                nonProfitsList = (List<NonProfitUser>)activist.bringOrganizationsFromDB();
                return nonProfitsList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }
}

        public void makeAPurchesChanges(string productName, decimal productPrice, string userEmail)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            try { 
            user.makeAPurchesInTheDB(productName, productPrice, userEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);

            }
        }

        public void signActivistToCampaign(string campaignName, string userEmail)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            try
            {
                user.signActivistToCampaignInTheDB(campaignName, userEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);

            }
        }

        public List<ActivistUser> ActivistUserList = new List<ActivistUser>();
        public List<ActivistUser> getAllOrNotActiveUsers(string allOrNot)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            try { 
                 ActivistUserList= (List<ActivistUser>)user.getActivistsList(allOrNot);
                return ActivistUserList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }
            

        }

        public ConfigData getTwitterKeys()
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            ConfigData config = (ConfigData)user.bringTwitterKeysFromDB();
            return config;

        }



    }
}
