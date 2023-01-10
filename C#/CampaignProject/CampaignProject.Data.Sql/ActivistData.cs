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
    public class ActivistData
    {

        public List<Model.NonProfitUser> ReadOrganizationFromDb(SqlDataReader reader)
        {
            List<Model.NonProfitUser> OrganizationList = new List<Model.NonProfitUser>();

            //Clear Hashtable Before Inserting Information From Sql Server
            OrganizationList.Clear();

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
            return OrganizationList;
        }


        public CampaignProject.Model.ActivistUser ReadOneFromDb(SqlDataReader reader)
        {


            CampaignProject.Model.ActivistUser Activist = new CampaignProject.Model.ActivistUser();

            while (reader.Read())
            {

                Activist.fullName = reader.GetString(2);
                Activist.email = reader.GetString(3);
                
                Activist.cellPhone = reader.GetString(5);
                Activist.Earnings = reader.GetDecimal(7);
                


            }
            return Activist;
        }

        public List<Model.ActivistUser> ReadListOfActivistsFromDb(SqlDataReader reader)
        {


            List<Model.ActivistUser> ActivistList = new List<Model.ActivistUser>();

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
            return ActivistList;
        }

        public object SendSqlQueryToReadFromDBForOneUser(string userEmail)
        {
            string SqlQuery = "declare @answer varchar(100)\n if exists (select * from Activists where Email=" + "'" + userEmail + "'" + ") begin select @answer = 'true' end else begin select @answer = 'false' end select @answer";
            object retObject = DAL.SqlQuery.getOneDataFromDB(SqlQuery);

           
            return retObject;

        }

        public string getActivistUserEarnings(string userEmail)
        {
            string SqlQuery = "select Earnings from Activists where Email=" + "'" + userEmail + "'";
            string retObject = (string)DAL.SqlQuery.getOneDataFromDB(SqlQuery);
            
            return retObject;


        }

        public object getActivistsList(string allOrNot)
        {
            object retObject=null;

            if (allOrNot.Equals("NOT"))
            {
                string SqlQuery = " select * from Activists where ChosenProducts != 0";
                retObject = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadListOfActivistsFromDb);
            }
            else
            {
                string SqlQuery = "select * from Activists";
                 retObject = (string)DAL.SqlQuery.getDataFromDB(SqlQuery, ReadListOfActivistsFromDb);
            }

          

            return retObject;


        }

        Model.ActivistUser newUser = new Model.ActivistUser();
        public void SendSqlQueryToInsertToDB(Model.ActivistUser NewUser, int userID)
        {
            string uploadNewUserQuery = "insert into Activists values('" + userID + "','" + NewUser.fullName + "','" + NewUser.email + "','" + NewUser.address + "','" + NewUser.cellPhone + "','" + NewUser.TwitterAcount + "','" + NewUser.Earnings + "',0,0)";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);

        }

        public void makeAPurchesInTheDB(string productName, decimal productPrice, string userEmail)
        {
            string uploadNewUserQuery = "UPDATE Activists SET Earnings = Earnings - " + productPrice + " where Email = '"+userEmail+"'" +
                "UPDATE Products SET IsBought = 1, ActivistBuyerID = (select id from Activists where Email = '"+ userEmail + "') where ProductName = '"+ productName + "'\n update Campaigns set DonationsAmount = DonationsAmount - " + productPrice + " where CampaignId =(select Campaign from Products where ProductName='"+ productName + "') update Activists set ChosenProducts = ChosenProducts +1 where Email='"+userEmail+"'";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewUserQuery);

        }

        public void signActivistToCampaignInTheDB(string campaignName, string userEmail)
        {

            string uploadActiveCampaignQuery = "insert into ActiveCampaigns select CampaignId, '"+ campaignName + "', CampaignHashtag, ActivistUsersID, FullName from Campaigns, Activists where Campaigns.CampaignName='"+ campaignName + "' and Activists.Email='"+ userEmail + "' and not exists (select * from ActiveCampaigns where CampaignId = (select CampaignId from Campaigns where CampaignName='" + campaignName + "') and Email='"+ userEmail + "')";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadActiveCampaignQuery);

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
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadOrganizationFromDb);
            return retDict;
        }
    }
}
