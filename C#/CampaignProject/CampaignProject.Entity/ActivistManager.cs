using CampaignProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class ActivistManager
    {
        public string getProductByIDFromDB(string UserEmail)
        {//chack if the userEmail is allready in the sql to know if the user signUp
            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData();
            return (string)Activist.SendSqlQueryToReadFromDBForOneUser(UserEmail);

        }

        public string getEarningsByIDFromDB(string UserEmail)
        {//chack if the userEmail is allready in the sql to know if the user signUp
            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData();
            return Activist.getActivistUserEarnings(UserEmail);

        }

        public void SendNewInputToDataLayer(Model.ActivistUser newOwner)
        {//insert of a new user to both tables - users and activists after the user signUp
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            string userType = "Activist";
            string userID = user.AddNewUser(userType);
            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));

        }


        public List<NonProfitUser> nonProfitsList = new List<NonProfitUser>();

        public List<NonProfitUser> getNonProfitListFromDB()
        {
            Data.Sql.ActivistData activist = new Data.Sql.ActivistData();
            nonProfitsList = (List<NonProfitUser>)activist.bringOrganizationsFromDB();
            return nonProfitsList;
        }

        public void makeAPurchesChanges(string productName, decimal productPrice, string userEmail)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            
            user.makeAPurchesInTheDB(productName, productPrice, userEmail);

        }

    }
}
