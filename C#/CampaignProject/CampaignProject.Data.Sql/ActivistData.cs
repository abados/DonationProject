using CampaignProject.DAL;
using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class ActivistData
    {

        public List<Model.NonProfitUser> ReadOrganizationFromDb(SqlDataReader reader)
        {
            List<Model.NonProfitUser> OrganizationList = new List<Model.NonProfitUser>();

            //Clear Hashtable Before Inserting Information From Sql Server
            OrganizationList.Clear();
            try { 
            while (reader.Read())
            {
                Model.NonProfitUser nonProfitUser = new Model.NonProfitUser();
                nonProfitUser.fullName = reader.GetString(2);
                nonProfitUser.email = reader.GetString(3);
                nonProfitUser.cellPhone = reader.GetString(4);
                nonProfitUser.organizationUrl = reader.GetString(5);
                nonProfitUser.organizationName = reader.GetString(6);
                nonProfitUser.organizationDescription = reader.GetString(7);

                OrganizationList.Add(nonProfitUser);

                //Cheking If Hashtable contains the key


            }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return OrganizationList;
        }


        public CampaignProject.Model.ActivistUser ReadOneFromDb(SqlDataReader reader)
        {


            CampaignProject.Model.ActivistUser Activist = new CampaignProject.Model.ActivistUser();
            try
            {
                while (reader.Read())
                {
                    Activist.fullName = reader.GetString(2);
                    Activist.email = reader.GetString(3);

                    Activist.cellPhone = reader.GetString(5);
                    Activist.Earnings = reader.GetDecimal(7);

                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return Activist;
        }


        public CampaignProject.Model.ConfigData ReadTwitterKeysFromDb(SqlDataReader reader)
        {


            CampaignProject.Model.ConfigData TwitterKeys = new CampaignProject.Model.ConfigData();
            try { 
            while (reader.Read())
            {

                string key = reader.GetString(0);
                string value = reader.GetString(1);
                if (key == "CONSUMER_KEY")
                {
                    if (value != null)
                        TwitterKeys.CONSUMER_KEY = value;
                }
                else if (key == "CONSUMER_SECRET")
                {
                    if (value != null)
                        TwitterKeys.CONSUMER_SECRET = value;
                }
                else if (key == "ACCESS_TOKEN")
                {
                    if (value != null)
                        TwitterKeys.ACCESS_TOKEN = value;
                }
                else if (key == "ACCESS_TOKEN_SECRET")
                {
                    if (value != null)
                        TwitterKeys.ACCESS_TOKEN_SECRET = value;

                }
            }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return TwitterKeys;
        }


        public List<Model.ActivistUser> ReadListOfActivistsFromDb(SqlDataReader reader)
        {


            List<Model.ActivistUser> ActivistList = new List<Model.ActivistUser>();
            try { 
            while (reader.Read())
            {
                Model.ActivistUser Activist = new Model.ActivistUser();
                Activist.id = reader.GetInt32(0);
                Activist.fullName = reader.GetString(2);
                Activist.email = reader.GetString(3);
                Activist.address = reader.GetString(4);
                Activist.cellPhone = reader.GetString(5);
                Activist.Earnings = reader.GetDecimal(7);
                ActivistList.Add(Activist);


            }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return ActivistList;
        }

        public List<Model.ActiveCampaigns> ReadListOfActivistCampaignsFromDb(SqlDataReader reader)
        {


            List<Model.ActiveCampaigns> ActivistList = new List<Model.ActiveCampaigns>();
            try
            {
                while (reader.Read())
                {
                    Model.ActiveCampaigns Activist = new Model.ActiveCampaigns();
                    Activist.id = reader.GetInt32(0);
                    Activist.campaignId = reader.GetInt32(1);
                    Activist.campaignName = reader.GetString(2);
                    Activist.campaignHashtag = reader.GetString(3);
                    Activist.activeUserId = reader.GetInt32(4);
                    Activist.ActiveUserName = reader.GetString(5);
                    ActivistList.Add(Activist);


                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return ActivistList;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Activists where Email=" + "'" + userEmail + "'" + ") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            try { 
            object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery);
            return retObject;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;

        }

        public string getActivistUserEarnings(string userEmail)
        {
            string SqlQuery = "select Earnings from Activists where Email=" + "'" + userEmail + "'";
            try { 
            string retObject = (string)DAL.SqlQuery.getOneDataFromDB(SqlQuery);
            return retObject;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;

        }


        Model.ActivistUser newUser = new Model.ActivistUser();
        public void SendSqlQueryToInsertToDB(Model.ActivistUser NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Activists values('" + userID + "','" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.address + "','" + NewUser.cellPhone + "','" + NewUser.TwitterAcount + "','" + NewUser.Earnings + "',0,0)";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }

        }

        public void makeAPurchesInTheDB(string productName, decimal productPrice, string userEmail)
        {
            string uploadNewUserQuery = "UPDATE Activists SET Earnings = Earnings - " + productPrice + " where Email = '"+userEmail+"'" +
                "UPDATE Products SET IsBought = 1, ActivistBuyerID = (select id from Activists where Email = '"+ userEmail + "') where ProductName = '"+ productName + "'\n update Campaigns set DonationsAmount = DonationsAmount - " + productPrice + " where CampaignId =(select Campaign from Products where ProductName='"+ productName + "') update Activists set ChosenProducts = ChosenProducts +1 where Email='"+userEmail+"'";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }

        }


        

        public void makeADonateInTheDB( decimal productPrice, string userEmail)
        {
            string uploadNewUserQuery = "UPDATE Activists SET Earnings = Earnings - " + productPrice + " where Email = '" + userEmail + "'";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
        }

        public void signActivistToCampaignInTheDB(string campaignName, string userEmail)
        {

            string uploadActiveCampaignQuery = "insert into ActiveCampaigns select CampaignId, '"+ campaignName + "', CampaignHashtag, ActivistUsersID, FullName from Campaigns, Activists where Campaigns.CampaignName='"+ campaignName + "' and Activists.Email='"+ userEmail + "' and not exists (select * from ActiveCampaigns where CampaignId = (select CampaignId from Campaigns where CampaignName='" + campaignName + "') and Email='"+ userEmail + "')";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadActiveCampaignQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
        }



        public string AddNewUser(string userType)
        {
            object userID = SqlQuery.insertIntoConnectedTable("INSERT INTO Users ([UserType]) VALUES ('" + userType + "') SELECT @@IDENTITY");
            // return his identity
            if (userID != null)
            {
                return userID.ToString();
            }
            else
            {
                return "1";
            }
        }

        public object bringOrganizationsFromDB()
        {
            string SqlQuery = "select * from NonProfits";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadOrganizationFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object bringActiveCampaignOfUsrFromDB(string Email)
        {
            string SqlQuery = "SELECT AC.* FROM ActiveCampaigns AC INNER JOIN Activists A ON AC.ActivistBuyerID = A.ActivistUsersID where A.Email='"+ Email+"'";
            object retDict = null;
            try
            {
                retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadListOfActivistCampaignsFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object bringTwitterKeysFromDB()
        {
            string SqlQuery = "select * from Config where [KEY]!='Bearer'";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadTwitterKeysFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }
    }
}
