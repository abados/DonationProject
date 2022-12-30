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
        {
            Data.Sql.ActivistData Activist = new Data.Sql.ActivistData();
            return (string)Activist.SendSqlQueryToReadFromDBForOneUser(UserEmail);

        }

        public void SendNewInputToDataLayer(Model.ActivistUser newOwner)
        {
            Data.Sql.ActivistData user = new Data.Sql.ActivistData();
            string userType = "Activist";

            string userID = user.AddNewUser(userType);

            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));

        }

    }
}
