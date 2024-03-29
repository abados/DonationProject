﻿using CampaignProject.DAL;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class BusinessData: BaseDataSql
    {
        public BusinessData(Logger log) : base(log)
        {
            DAL.SqlQuery.logger = Logger;
        }
        public CampaignProject.Model.BusinessUser ReadOneFromDb(SqlDataReader reader)
        {

            CampaignProject.Model.BusinessUser Business = new CampaignProject.Model.BusinessUser();
            try { 
            while (reader.Read())
            {
                Business.fullName = reader.GetString(3);
                Business.email = reader.GetString(4);
                Business.cellPhone = reader.GetString(5);

            }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return Business;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Businesses where Email=" + "'" + userEmail + "'" + ") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            try {
                object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery);
            return retObject;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;


        }

        public string[] sqlQuertyToSearchIDS(string userEmail, string campaignName)
        {
            
            string SqlQueryForBusinessID = "select id from Businesses where  Email=" + "'" + userEmail + "'" + "";
            try {
                string retObject = DAL.SqlQuery.getOneDataFromDBInString(SqlQueryForBusinessID);

            if (campaignName == "")
            {
                string[] arrayWithOne = new string[2] { retObject, retObject };

                return arrayWithOne;
            }

            string SqlQueryForCampaignID = "select CampaignId from Campaigns where  CampaignName=" + "'" + campaignName + "'" + "";
            
                string retObjec2 = DAL.SqlQuery.getOneDataFromDBInString(SqlQueryForCampaignID);
            string[] array = new string[2] { retObject, retObjec2 };

            return array;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;


        }

        Model.BusinessUser newUser = new Model.BusinessUser();

      

        public void SendSqlQueryToInsertToDB(Model.BusinessUser NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Businesses values('" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.cellPhone + "','" + NewUser.businessName + "','" + userID + "')";
            try {
                DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
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

        public void DeliveredTheItem(int productID)
        {
            string uploadNewUserQuery = "update Products set IsDelivered=1 where id="+productID+"";
            try {
              
                DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
        }
    }
}
