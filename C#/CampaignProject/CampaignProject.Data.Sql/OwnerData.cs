using CampaignProject.DAL;
using CampaignProject.Model;
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

        public  CampaignProject.Model.Owner ReadOneFromDb(SqlDataReader reader)
        {

            CampaignProject.Model.Owner owner = new CampaignProject.Model.Owner();
            while (reader.Read())
            {

                owner.fullName = reader.GetString(3);
                owner.email = reader.GetString(4);
                owner.cellPhone = reader.GetString(5);
            

            }
            return owner;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Owner where Email="+"'"+userEmail+"'"+") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery, ReadOneFromDb);
            return retObject;


        }

        Model.Owner newUser = new Model.Owner();
        public void SendSqlQueryToInsertToDB(Model.Owner NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Owner values('" + userID + "','" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.cellPhone + "')";
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
