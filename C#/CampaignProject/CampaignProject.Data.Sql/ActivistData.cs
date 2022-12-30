using CampaignProject.DAL;
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
        public CampaignProject.Model.ActivistUser ReadOneFromDb(SqlDataReader reader)
        {

            CampaignProject.Model.ActivistUser Activist = new CampaignProject.Model.ActivistUser();
            while (reader.Read())
            {

                Activist.fullName = reader.GetString(3);
                Activist.email = reader.GetString(4);
                Activist.cellPhone = reader.GetString(5);


            }
            return Activist;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Activists where Email=" + "'" + userEmail + "'" + ") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery, ReadOneFromDb);
            return retObject;


        }

        Model.ActivistUser newUser = new Model.ActivistUser();
        public void SendSqlQueryToInsertToDB(Model.ActivistUser NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Activists values('" + userID + "','" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.address + "','" + NewUser.cellPhone + "','" + NewUser.TwitterAcount + "','" + NewUser.Earnings + "',0,0)";
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
