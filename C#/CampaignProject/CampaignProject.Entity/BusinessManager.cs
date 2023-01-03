using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class BusinessManager
    {
        public string getProductByIDFromDB(string UserEmail)
        {
            Data.Sql.BusinessData Business = new Data.Sql.BusinessData();
            return (string)Business.SendSqlQueryToReadFromDBForOneUser(UserEmail);


        }

        public string[] getIDS(string userEmail, string campaignName)
        {
            Data.Sql.BusinessData Business = new Data.Sql.BusinessData();
            return Business.sqlQuertyToSearchIDS(userEmail, campaignName);


        }

        public void SendNewInputToDataLayer(Model.BusinessUser newOwner)
        {
            Data.Sql.BusinessData user = new Data.Sql.BusinessData();
            string userType = "Business";

            string userID = user.AddNewUser(userType);

            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));

        }

    }
}
