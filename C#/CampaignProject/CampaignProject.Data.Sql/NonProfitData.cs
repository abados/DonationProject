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
    public class NonProfitData : BaseDataSql
    {
        public NonProfitData(Logger log) : base(log)
        {
            DAL.SqlQuery.logger = Logger;
        }


        public CampaignProject.Model.NonProfitUser ReadOneFromDb(SqlDataReader reader)
        {

            CampaignProject.Model.NonProfitUser NonProfit = new CampaignProject.Model.NonProfitUser();
            try
            {
                while (reader.Read())
                {

                    NonProfit.fullName = reader.GetString(3);
                    NonProfit.email = reader.GetString(4);
                    NonProfit.cellPhone = reader.GetString(5);


                }
                return NonProfit;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from NonProfits where Email=" + "'" + userEmail + "'" + ") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            try
            {
                
                object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery);
                return retObject;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;


        }

        Model.NonProfitUser newUser = new Model.NonProfitUser();

        

        public void SendSqlQueryToInsertToDB(Model.NonProfitUser NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into NonProfits values('" + userID + "','" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.cellPhone + "','" + NewUser.organizationUrl + "','" + NewUser.organizationName + "','" + NewUser.organizationDescription + "')";
            try
            {
                
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
    }
}
