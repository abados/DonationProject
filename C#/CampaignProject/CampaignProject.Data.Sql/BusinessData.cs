using CampaignProject.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class BusinessData
    {
        public CampaignProject.Model.BusinessUser ReadOneFromDb(SqlDataReader reader)
        {

            CampaignProject.Model.BusinessUser Business = new CampaignProject.Model.BusinessUser();
            while (reader.Read())
            {

                Business.fullName = reader.GetString(3);
                Business.email = reader.GetString(4);
                Business.cellPhone = reader.GetString(5);


            }
            return Business;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Businesses where Email=" + "'" + userEmail + "'" + ") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery, ReadOneFromDb);
            return retObject;


        }

        Model.BusinessUser newUser = new Model.BusinessUser();
        public void SendSqlQueryToInsertToDB(Model.BusinessUser NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Businesses values('" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.cellPhone + "','" + NewUser.businessName + "','" + userID + "')";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);

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
