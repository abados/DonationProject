using CampaignProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class NonProfitManager
    {
        public string getProductByIDFromDB(string UserEmail)
        {
            Data.Sql.NonProfitData NonProfit = new Data.Sql.NonProfitData();
            return (string)NonProfit.SendSqlQueryToReadFromDBForOneUser(UserEmail);
        }

        public void SendNewInputToDataLayer(Model.NonProfitUser newOwner)
        {
            Data.Sql.NonProfitData user = new Data.Sql.NonProfitData();
            string userType = "NonProfit";

            string userID = user.AddNewUser(userType);

            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));

        }
    }
}
