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
    public class OwnerData
    {
        public Dictionary<int, Model.ActiveCampaigns> ReadTweetActivityFromDb(SqlDataReader reader)
        {
            Dictionary<int, Model.ActiveCampaigns> activeCampaignsList = new Dictionary<int, Model.ActiveCampaigns>();

    
            activeCampaignsList.Clear();
            try { 
            while (reader.Read())
            {
                Model.ActiveCampaigns activeCampaign = new Model.ActiveCampaigns();
                activeCampaign.id = reader.GetInt32(0);
                activeCampaign.campaignId = reader.GetInt32(1);
                activeCampaign.campaignName = reader.GetString(2);
                activeCampaign.campaignHashtag = reader.GetString(3);
                activeCampaign.activeUserId = reader.GetInt32(4);
                activeCampaign.ActiveUserName = reader.GetString(5);
                activeCampaign.TwitterAcount= reader.GetString(6);



                //Cheking If Hashtable contains the key
                if (activeCampaignsList.ContainsKey(activeCampaign.id))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    activeCampaignsList.Add(activeCampaign.id, activeCampaign);
                }

            }
            return activeCampaignsList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }


        public  CampaignProject.Model.Owner ReadOneFromDb(SqlDataReader reader)
        {

            CampaignProject.Model.Owner owner = new CampaignProject.Model.Owner();
            try { 
            while (reader.Read())
            {

                owner.fullName = reader.GetString(3);
                owner.email = reader.GetString(4);
                owner.cellPhone = reader.GetString(5);
            

            }
            return owner;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }

        public List<BusinessUser> ReadBusinessUsersFromDb(SqlDataReader reader)
        {
            List<BusinessUser> businessUsers = new List<BusinessUser>();
            try { 
            while (reader.Read())
            {
                CampaignProject.Model.BusinessUser Buser = new CampaignProject.Model.BusinessUser();
                Buser.fullName = reader.GetString(1);
                Buser.email = reader.GetString(2);
                Buser.cellPhone = reader.GetString(3);
                Buser.businessName= reader.GetString(4);

                businessUsers.Add(Buser);

            }
            return businessUsers;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }


        public List<NonProfitUser> ReadProfitUsersFromDb(SqlDataReader reader)
        {
            List<NonProfitUser> NonProfitUsers = new List<NonProfitUser>();
            try { 
            while (reader.Read())
            {
                CampaignProject.Model.NonProfitUser Nuser = new CampaignProject.Model.NonProfitUser();
                Nuser.fullName = reader.GetString(2);
                Nuser.email = reader.GetString(3);
                Nuser.cellPhone = reader.GetString(4);
                Nuser.organizationUrl = reader.GetString(5);
                Nuser.organizationName = reader.GetString(6);
                Nuser.organizationDescription = reader.GetString(7);

                NonProfitUsers.Add(Nuser);

            }
            return NonProfitUsers;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }


        public List<ActivistUser> ReadActivistUsersFromDb(SqlDataReader reader)
        {
            List<ActivistUser> ActivistUsers = new List<ActivistUser>();
           

            ActivistUsers.Clear();
            try { 
            while (reader.Read())
            {
                CampaignProject.Model.ActivistUser Auser = new CampaignProject.Model.ActivistUser();
                Auser.fullName = reader.GetString(2);
                Auser.email = reader.GetString(3);
                Auser.address = reader.GetString(4);
                Auser.cellPhone = reader.GetString(5);
                Auser.TwitterAcount = reader.GetString(6);
                Auser.Earnings = reader.GetDecimal(7);

                ActivistUsers.Add(Auser);
            }

            return ActivistUsers;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Owner where Email="+"'"+userEmail+"'"+") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            try { 
            object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery);
                Logger.Log("check if the user exsist as Owner in the DB", LoggingLibrary.LogLevel.Event);
                return retObject;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }



            return null;

        }

   
        public void SendSqlQueryToInsertToDB(Model.Owner NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Owner values('" + userID + "','" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.cellPhone + "')";
            try
            {
                DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
                
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }

        }

        public void SendSqlQueryToPay(int ActivityCount,int activistID)
        {
            string uploadNewUserQuery = "update Activists set Earnings = Earnings + (5 * "+ ActivityCount+ ") where ActivistUsersID = " + activistID+"";
            try
            {
                DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
                
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

        public object bringActiveCampaignsTable()
        {
            string SqlQuery = "SELECT AC.*, A.TwitterAcount FROM ActiveCampaigns AC INNER JOIN Activists A ON AC.ActivistBuyerID = A.ActivistUsersID";
            object retDict = null;
            try
            {
                retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadTweetActivityFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        ////////Report functions/////
        
        public object bringAallBusinessUsers()
        {
            string SqlQuery = "SELECT * from Businesses";
            object retDict = null;
            try
            {
                retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadBusinessUsersFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object bringAallNonProfitUsers()
        {
            string SqlQuery = "SELECT * from NonProfits";
            object retDict = null;
            try
            {
                retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadProfitUsersFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object bringAllActivistUsers()
        {
            string SqlQuery = "SELECT * from Activists";
            object retDict = null;
            try
            {
                retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadActivistUsersFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public string bringEarningSumUp()
        {
            string SqlQuery = "SELECT SUM(Earnings) as TotalEarnings FROM Activists;";
            string EarningsSum = null;
            try
            {
                EarningsSum = DAL.SqlQuery.getOneDataFromDBInString(SqlQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return EarningsSum;
        }

    }
}
