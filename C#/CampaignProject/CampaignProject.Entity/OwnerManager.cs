using CampaignProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class OwnerManager
    {
        public string getProductByIDFromDB(string UserEmail)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            return (string)Owner.SendSqlQueryToReadFromDBForOneUser(UserEmail);


        }

        public void SendNewInputToDataLayer(Model.Owner newOwner)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            string userType = "Admin";

            string userID = Owner.AddNewUser(userType);

            Owner.SendSqlQueryToInsertToDB(newOwner,int.Parse(userID));

        }

        public Dictionary<int, Model.ActiveCampaigns> bringDataAboutCampaignsActivity()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            Dictionary<int, Model.ActiveCampaigns> activeCampaignList;
            activeCampaignList = (Dictionary<int, Model.ActiveCampaigns>)Owner.bringActiveCampaignsTable();
            return activeCampaignList;

        }


    }
}
